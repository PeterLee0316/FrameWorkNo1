using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Diagnostics;

using static Core.Layers.DEF_Thread;
using static Core.Layers.DEF_Thread.EWindowMessage;
using static Core.Layers.DEF_Thread.EThreadMessage;
using static Core.Layers.DEF_Thread.EThreadChannel;
using static Core.Layers.DEF_Error;
using static Core.Layers.DEF_Common;
using static Core.Layers.DEF_DataManager;
using static Core.Layers.DEF_MTrsInterface;

using Core.UI;

namespace Core.Layers
{
    public class DEF_MTrsInterface
    {
        public const int ERR_TRS_AUTO_MANAGER_LOG_NULL_POINTER     = 1;
        public const int ERR_TRS_AUTO_MANAGER_NULL_DATA            = 2;
        public const int ERR_TRS_AUTO_MANAGER_CANNOT_EXECUTE_CMD   = 3;
        public const int ERR_TRS_AUTO_MANAGER_ESTOP_PRESSED        = 4;
        public const int ERR_TRS_AUTO_MANAGER_MELSEC_REOPEN_FAILED = 5;

        public class CTrsInterfaceRefComp
        {    
                        

            //public CTrsInterfaceRefComp(MCtrlLoader ctrlLoader)
            //{
            //    this.ctrlLoader = ctrlLoader;
            //}
            public override string ToString()
            {
                return $"CTrsInterfaceRefComp : {this}";
            }
        }

        public class CTrsAutoManagerData
        {
            public bool UseVIPMode;

        }
    }

    public class MTrsInterface : MWorkerThread
    {
        private CTrsInterfaceRefComp m_RefComp;
        private CTrsAutoManagerData m_Data;

        // Thread가 해당 상태로 전환했는지 확인하기 위한 테이블
        //  Thread에게 명령을 내려 보내기전에 Clear 한다. 
        EAutoRunStatus[] m_ThreadStatusArray = new EAutoRunStatus[(int)EThreadChannel.MAX];

        // switch status
        bool m_bStartPressed;
        bool m_bStepStopPressed;
        bool m_bResetPressed;
        bool m_bEStopPressed;

        // Alarm Logging을 위한 Path 지정
        string m_strLogPath;
        // Alarm 정보를 읽어오기 위한 Path 지정
        string m_strDataPath;
        
        MTickTimer m_ResetTimer = new MTickTimer();

        public bool IsInitState;          // 원점잡기나, 초기화 실행하고 있는 상태이면 true

        // Message 변수
        bool m_bPushPull_RequestUnloading;
        bool m_bPushPull_StartLoading;
        bool m_bPushPull_CompleteLoading;

        public static CThreadInterface TInterface = new CThreadInterface();
        protected MTickTimer TTimer = new MTickTimer(); // interface timer
        protected int TOpponent;    // handshake opponent


        public MTrsInterface(CObjectInfo objInfo, EThreadChannel SelfChannelNo, MDataManager DataManager, 
            CTrsInterfaceRefComp refComp, CTrsAutoManagerData data)
            : base(objInfo, SelfChannelNo, DataManager)
        {
            m_RefComp = refComp;
            SetData(data);

            SetSystemStatus(EAutoRunStatus.STS_MANUAL);	// System의 상태를 EAutoRunStatus.STS_MANUAL 상태로 전환한다.
            TSelf = (int)EThreadUnit.AUTOMANAGER;
        }

        #region Common : Manage Data, Position, Use Flag and Initialize
        public int SetData(CTrsAutoManagerData source)
        {
            m_Data = ObjectExtensions.Copy(source);
            return SUCCESS;
        }

        public int GetData(out CTrsAutoManagerData target)
        {
            target = ObjectExtensions.Copy(m_Data);

            return SUCCESS;
        }

        int InitializeAllThread()
        {
            int iResult = SUCCESS;


            return iResult;
        }

        public int InitializeMsg()
        {
            m_bPushPull_RequestUnloading = false;
            m_bPushPull_StartLoading = false;
            m_bPushPull_CompleteLoading = false;

            return SUCCESS;
        }

        private int InitializeInterface()
        {

            return SUCCESS;
        }
        #endregion

        protected override void ThreadProcess()
        {
            int iResult = SUCCESS;
            bool bStatus, bStatus1, bStatus2;


            // timer start if it is needed.

            while (true)
            {
                // if thread has been suspended
                if (IsAlive == false)
                {
                    Sleep(ThreadSuspendedTime);
                    continue;
                }

                // 160905 MainUI가 Load된 후에 process 동작 및 에러보고 할 수 있도록
                if (CMainFrame.IsFormLoaded == false)
                {
                    Sleep(ThreadSleepTime);
                    continue;
                }

                // check message from other thread
                CheckMsg(1);

                switch (RunStatus)
                {
                    case EAutoRunStatus.STS_MANUAL: // Manual Mode
                        break;

                    case EAutoRunStatus.STS_ERROR_STOP: // Error Stop
                        break;

                    case EAutoRunStatus.STS_STEP_STOP: // Step Stop
                        break;

                    case EAutoRunStatus.STS_RUN_READY: // Run Ready
                        break;

                    case EAutoRunStatus.STS_CYCLE_STOP: // Cycle Stop
                        break;

                    case EAutoRunStatus.STS_RUN: // auto run

                        // Do Thread Step
                        switch (ThreadStep)
                        {
                            default:
                                break;
                        }
                        break;
                }

                ProcessInterface();

                Sleep(ThreadSleepTime);
                //Debug.WriteLine(ToString() + " Thread running..");
            }

        }

        protected override int ProcessMsg(MEvent evnt)
        {
              Debug.WriteLine($"[{ToString()}] received message : {evnt.ToThreadMessage()}");

            // check message is valid
            //if(Enum.TryParse())
            //if (Enum.IsDefined(typeof(EThreadMessage), evnt.Msg) == false)
            //    return SUCCESS;

            EThreadMessage msg = EThreadMessage.NONE;
            try
            {
                msg = (EThreadMessage)Enum.Parse(typeof(EThreadMessage), evnt.Msg.ToString());
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
            }
            switch (msg)
            {
                case EThreadMessage.MSG_PROCESS_ALARM:
                    //if (AfxGetApp()->GetMainWnd() != NULL)
                    //{
                    //    if (((CMainFrame*)AfxGetApp()->GetMainWnd())->m_pErrorDlg == NULL)
                    //        return processAlarm(evnt);  // process에서 올라온 알람메세지의 처리
                    //}

                    return ProcessAlarm(evnt);  // process에서 올라온 알람메세지의 처리
                    break;

                // MSG_MANUAL COMMAND에 대한 Thread의 응답
                case EThreadMessage.MSG_MANUAL_CNF:
                    SetThreadStatus(evnt.wParam, EAutoRunStatus.STS_MANUAL); // 메세지를 보낸 Thread를 EAutoRunStatus.STS_MANUAL 상태로 놓는다.
                    if (CheckAllThreadStatus(EAutoRunStatus.STS_MANUAL))       // 모든 Thread가 EAutoRunStatus.STS_MANUAL 상태인지 확인한다.
                    {
                        SetSystemStatus(EAutoRunStatus.STS_MANUAL);
                        // m_bAlarmProcFlag = false;

                        //setVelocityMode(VELOCITY_MODE_SLOW);	// Manual일 때 느린 속도로 이동

                        SendMsgToMainWnd(WM_START_MANUAL_MSG);
                        CMainFrame.mCore.SetAutoManual(EAutoManual.MANUAL);
                    }
                    break;

                // 화면으로 부터의 START_RUN Command (화면 Start 버튼을 누름)
                case EThreadMessage.MSG_READY_RUN_CMD:

                    OnRunReady();

                    break;

                // MSG_START_RUN COMMAND에 대한 Thread의 응답
                case EThreadMessage.MSG_READY_RUN_CNF:
                    SetThreadStatus(evnt.wParam, EAutoRunStatus.STS_RUN_READY);  // 메세지를 보낸 Thread를 EAutoRunStatus.STS_RUN_READY 상태로 놓는다.
                    if (CheckAllThreadStatus(EAutoRunStatus.STS_RUN_READY))        // 모든 Thread가 EAutoRunStatus.STS_RUN_READY 상태인지 확인한다.
                    {
                        SetSystemStatus(EAutoRunStatus.STS_RUN_READY);

                        CMainFrame.mCore.SetAutoManual(EAutoManual.AUTO);  
                        SendMsgToMainWnd(WM_START_READY_MSG);

                    }
                    break;

                // MSG_START_CMD에 대한 Thread의 응답
                case EThreadMessage.MSG_START_CNF:
                    SetThreadStatus(evnt.wParam, EAutoRunStatus.STS_RUN);    // 메세지를 보낸 Thread를 EAutoRunStatus.STS_RUN 상태로 놓는다.
                    if (CheckAllThreadStatus(EAutoRunStatus.STS_RUN))      // 모든 Thread가 EAutoRunStatus.STS_RUN 상태인지 확인한다.
                    {
                        SetSystemStatus(EAutoRunStatus.STS_RUN);

                        CMainFrame.mCore.SetAutoManual(EAutoManual.AUTO);

                        SendMsgToMainWnd(WM_START_RUN_MSG);
                    }
                    break;

                // MSG_STEP_STOP_CMD에 대한 Thread의 응답
                case EThreadMessage.MSG_STEP_STOP_CNF: // STEP_STOP CMD에 대한 Thread들의 EAutoRunStatus.STS_STEP_STOP 확인 메세지
                    SetThreadStatus(evnt.wParam, EAutoRunStatus.STS_STEP_STOP);  // 메세지를 보낸 Thread를 EAutoRunStatus.STS_STEP_STOP 상태로 놓는다.
                    if (CheckAllThreadStatus(EAutoRunStatus.STS_STEP_STOP))        // 모든 Thread가 EAutoRunStatus.STS_RUN 상태인지 확인한다.
                    {
                        if (RunStatus == EAutoRunStatus.STS_RUN)
                        {
                            /*	clearAllThreadStatus();	// 확인을 위한 Thread의 상태를 Clear한다.
                            BroadcastMsg(MSG_READY_RUN_CMD);

                            SetSystemStatus(EAutoRunStatus.STS_RUN_READY);	// System의 상태를 EAutoRunStatus.STS_RUN_READY 상태로 전환한다.
                            }
                            else 
                            { 임시로 주석 테스트 반드시 필요..*/
                            ClearAllThreadStatus(); // 확인을 위한 Thread의 상태를 Clear한다.
                                                    //BroadcastMsg(MSG_MANUAL_CMD);
                                                    //SetSystemStatus(EAutoRunStatus.STS_MANUAL);
                            BroadcastMsg(MSG_STEP_STOP_CMD);
                            SetSystemStatus(EAutoRunStatus.STS_STEP_STOP);
                        }
                    }
                    break;

                // MSG_ERROR_STOP_CMD에 대한 Thread의 응답
                case EThreadMessage.MSG_ERROR_STOP_CNF:    // MSG_ERROR_STOP CMD에 대한 Thread들의 ERROR_STEP_STOP 확인 메세지
                    SetThreadStatus(evnt.wParam, EAutoRunStatus.STS_ERROR_STOP); // 메세지를 보낸 Thread를 EAutoRunStatus.STS_RUN 상태로 놓는다.
                    if (CheckAllThreadStatus(EAutoRunStatus.STS_ERROR_STOP))       // 모든 Thread가 EAutoRunStatus.STS_RUN 상태인지 확인한다.
                    {
                        //			clearAllThreadStatus();	// 확인을 위한 Thread의 상태를 Clear한다.
                        //			BroadcastMsg(MSG_MANUAL_CMD);
                    }
                    break;




                case EThreadMessage.MSG_PANEL_OUTPUT:  // Panel이 Output.
                    if (RunStatus == EAutoRunStatus.STS_RUN)
                    {
                    }
                    break;
            }

            return SUCCESS;
        }


        void SetThreadStatus(int iIndex, EAutoRunStatus status)
        {
            m_ThreadStatusArray[iIndex] = status;
        }

        void ClearAllThreadStatus()
        {
            for (int iIndex = 1; iIndex <= GetThreadsCount(); iIndex++)
            {
                m_ThreadStatusArray[iIndex] = EAutoRunStatus.NONE;
            }
        }

        bool CheckAllThreadStatus(EAutoRunStatus status)
        {
            // self channel 은 제외하고
            // for (int iIndex = 1; iIndex <= GetThreadsCount() ; iIndex++)
            for (int iIndex = 1; iIndex < (int)EThreadChannel.MAX; iIndex++)
            {
                //if (iIndex == (int)EThreadChannel.TrsAutoManager) continue;
                if ((int)GetSelfChannelNo(iIndex) == (int)EThreadChannel.TrsAutoManager) continue;

                if (m_ThreadStatusArray[iIndex] != status)
                {
                    return false;
                }
            }

            return true;
        }

        void SetSystemStatus(EAutoRunStatus status)
        {
            if (SetRunStatus(status) == false) return;

            bool bStatus;

            if (RunStatus == EAutoRunStatus.STS_RUN)
            {
                // 설비가 Live 상태임을 알리는 oUpper_Alive 신호는 On
                //m_RefComp.m_pC_InterfaceCtrl.SendInterfaceOnMsg(PRE_EQ, oUpper_Alive);
                //m_RefComp.m_pC_InterfaceCtrl.SendInterfaceOnMsg(NEXT_EQ, oLower_Alive);
            }
            else
            {
                if (RunStatus_Old == EAutoRunStatus.STS_RUN)
                {
                    //// 설비가 Live 상태임을 알리는 oUpper_Alive 신호는 On
                    //m_RefComp.m_pC_InterfaceCtrl.SendInterfaceOffMsg(PRE_EQ, oUpper_Alive);
                    //m_RefComp.m_pC_InterfaceCtrl.SendInterfaceOffMsg(NEXT_EQ, oLower_Alive);

                }
            }
        }

        /// <summary>
        /// System이 RUN이 되기위한 조건을 체크한다.
        /// </summary>
        /// <returns></returns>
        bool CheckReadyRunCondition()
        {
            if (RunStatus == EAutoRunStatus.STS_MANUAL) return true;
            else return false;
        }

        /// <summary>
        /// System이 RUN이 되기위한 조건을 체크한다.
        /// </summary>
        /// <returns></returns>
        bool CheckRunCondition()
        {
            if (RunStatus == EAutoRunStatus.STS_RUN_READY || RunStatus == EAutoRunStatus.STS_STEP_STOP)
                return true;
            else
                return false;
        }

        /// <summary>
        /// System이 STEP_STOP이 되기위한 조건을 체크한다.
        /// </summary>
        /// <returns></returns>
        bool CheckStepStopCondition()
        {
            if ((RunStatus == EAutoRunStatus.STS_RUN) ||
                (RunStatus == EAutoRunStatus.STS_RUN_READY) ||
                (RunStatus == EAutoRunStatus.STS_ERROR_STOP)//) ||
                                                 //		(RunStatus == EAutoRunStatus.STS_STEP_STOP)
                )
                return true;
            else
                return false;
        }

        /// <summary>
        /// System이 ERROR_STOP이 되기위한 조건을 체크한다.
        /// </summary>
        /// <returns></returns>
        bool CheckErrorStopCondition()
        {
            if ((RunStatus != EAutoRunStatus.STS_MANUAL) &&
                (RunStatus != EAutoRunStatus.STS_ERROR_STOP))
                return true;
            else
                return false;
        }

        /// <summary>
        /// System이 CYCLE_STOP이 되기위한 조건을 체크한다.
        /// </summary>
        /// <returns></returns>
        bool CheckCycleStopCondition()
        {
            if (RunStatus == EAutoRunStatus.STS_RUN || RunStatus == EAutoRunStatus.STS_CYCLE_STOP)
                return true;
            else
                return false;
        }

        /// <summary>
        /// button들이 눌렸는지를 확인한 후에, button이 눌린쪽의 터치 스크린을 활성화 시켜준다.
        /// </summary>
        /// <param name="dSts"></param>
        void ChangeMonitorTouchControl()
        {

        }


        int ProcessInterface()
        {
            int iResult = SUCCESS;
            bool bStatus;
            // 설비가 Auto Run 상태인지 Manual, Stop 상태 인지 알림.
            switch (RunStatus)
            {
                // 수동 모드일 경우 아무것도 안함
                case EAutoRunStatus.STS_MANUAL:

                    break;

                // Error Stop Status에서는 Stage Auto Run 작업정지
                case EAutoRunStatus.STS_ERROR_STOP:
                    break;

                // Step Stop Status에서는 Stage Auto Run 작업정지
                case EAutoRunStatus.STS_STEP_STOP:
                    break;

                // Start Run Status에서는 Stage Auto Run 작업개시
                case EAutoRunStatus.STS_RUN_READY:

                    break;

                // Cycle Stop Status에서는 Cell Loading 까지 작업을 완료함
                case EAutoRunStatus.STS_CYCLE_STOP:
                    break;

                // Run Status에서는
                case EAutoRunStatus.STS_RUN:
                    break;
            }

            // 상류, 하류 설비에 MelsecNet, EStop, 유닛 포지션에 따라 IO를 이용한 신호 전송

            return SUCCESS;
        }

        /// <summary>
        /// Process에서 발생한 알람을 처리한다.
        /// </summary>
        /// <returns></returns>
        private int ProcessAlarm(MEvent evAlarm)
        {
            // View에 메세지를 보내서 처리하게 한다.
            bool bErrorStop;

            if (RunStatus == EAutoRunStatus.STS_RUN) bErrorStop = true;
            else bErrorStop = false;

            if (bErrorStop == false) return SUCCESS;

            // display dialog
            SendMsgToMainWnd(EWindowMessage.WM_ALARM_MSG, evAlarm.wParam, evAlarm.lParam);

            return SUCCESS;
        }

        /// <summary>
        /// RunReady 요건 발생시 처리
        /// </summary>
        void OnRunReady()
        {
            if (CheckReadyRunCondition())
            {
                ClearAllThreadStatus(); // 확인을 위한 Thread의 상태를 Clear한다.
                BroadcastMsg(MSG_READY_RUN_CMD);

                SetSystemStatus(EAutoRunStatus.STS_RUN_READY);
            }
        }

        /// <summary>
        /// Run 요건 발생시 처리
        /// </summary>
        void OnRun()
        {
            if (CheckRunCondition())    // Run Condition을 만족할 경우만 처리한다.
            {
                ClearAllThreadStatus(); // 확인을 위한 Thread의 상태를 Clear한다.
                BroadcastMsg(MSG_START_CMD);
                SetSystemStatus(EAutoRunStatus.STS_RUN);

                SendMsgToMainWnd(WM_START_RUN_MSG);
            }
        }
 

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

using static Core.Layers.DEF_Thread;
using static Core.Layers.DEF_Thread.EThreadStep;
using static Core.Layers.DEF_Thread.EThreadMessage;
using static Core.Layers.DEF_Thread.EThreadChannel;
using static Core.Layers.DEF_Error;
using static Core.Layers.DEF_Common;

namespace Core.Layers
{
    public class CTrsStage1RefComp
    {

        public override string ToString()
        {
            return "A";
        }
    }

    public class CTrsStage1Data
    {
        public bool ThreadHandshake_byOneStep = true;
    }

    public class MTrsStage1 : MWorkerThread
    {
        private CTrsStage1RefComp m_RefComp;
        private CTrsStage1Data m_Data;

        // Message 변수
        bool m_bWorkbench_LoadRequest;
        bool m_bWorkbench_LoadComplete;

        bool m_bAuto_PanelSupplyStop;

        public MTrsStage1(CObjectInfo objInfo, EThreadChannel SelfChannelNo, MDataManager DataManager,  
            CTrsStage1RefComp refComp, CTrsStage1Data data)
             : base(objInfo, SelfChannelNo, DataManager)
        {
            m_RefComp = refComp;
            SetData(data);
            TSelf = (int)EThreadUnit.STAGE1;
        }

        #region Common : Manage Data, Position, Use Flag and Initialize
        public int SetData(CTrsStage1Data source)
        {
            m_Data = ObjectExtensions.Copy(source);
            return SUCCESS;
        }

        public int GetData(out CTrsStage1Data target)
        {
            target = ObjectExtensions.Copy(m_Data);

            return SUCCESS;
        }

        public override int Initialize()
        {
            // Do initialize
            InitializeMsg();
            InitializeInterface();


            EThreadStep iStep1 = STEP_NONE;

            // finally
            SetStep(iStep1);

            return base.Initialize();
        }

        public int InitializeMsg()
        {
            m_bWorkbench_LoadComplete = false;
            m_bWorkbench_LoadRequest = false;

            return SUCCESS;
        }

        private int InitializeInterface()
        {

            return SUCCESS;
        }
        #endregion

        protected override int ProcessMsg(MEvent evnt)
        {
            Debug.WriteLine($"[{ToString()}] received message : {evnt.ToThreadMessage()}");
            switch (evnt.Msg)
            {
                // if need to change response for common message, then add case state here.
                default:
                    base.ProcessMsg(evnt);
                    break;

                case (int)MSG_PANEL_SUPPLY_STOP:
                    m_bAuto_PanelSupplyStop = true;
                    break;

                case (int)MSG_PANEL_SUPPLY_START:
                    m_bAuto_PanelSupplyStop = false;
                    break;

            }
            return 0;
        }

        protected override void ThreadProcess()
        {
            int iResult = SUCCESS;
            bool bStatus, bStatus1, bStatus2;

            while (true)
            {
                // if thread has been suspended
                if (IsAlive == false)
                {
                    Sleep(ThreadSuspendedTime);
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
                        //if (ThreadStep1 == TRS_STAGE1_MOVETO_LOAD_POS)
                        break;

                    case EAutoRunStatus.STS_RUN: // auto run

                        // Do Thread Step
                        switch (ThreadStep)
                        {


                            ///////////////////////////////////////////////////////////////////
                            // process load with handler
                            case STEP_NONE:
                                // branch
                                if (m_Data.ThreadHandshake_byOneStep == false)
                                {
                                    SetStep(STEP_NONE);
                                    break;
                                }




                                SetStep(STEP_NONE);
                                break;
                                               

                            default:
                                break;
                        }
                        break;

                    default:
                        break;
                }

                Sleep(ThreadSleepTime);
            }

        }
    }

}

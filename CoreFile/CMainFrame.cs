using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Configuration;

using Core.Layers;
using Core.UI;

using static Core.Layers.DEF_Thread;
using static Core.Layers.DEF_Thread.EThreadMessage;
using static Core.Layers.DEF_Thread.EWindowMessage;
using static Core.Layers.DEF_Error;
using static Core.Layers.DEF_Common;
using static Core.Layers.DEF_SerialPort;
using static Core.Layers.DEF_Vision;
using static Core.Layers.DEF_UI;

//#pragma warning disable CS0219

namespace Core.UI
{
    public partial class CMainFrame : Form
    {
        public static MSysCore mCore = new MSysCore(new CObjectInfo());
        public static MDataManager DataManager;
        public static CDBInfo DBInfo;

        public static CMainFrame MainFrame = null;
        public static bool IsFormLoaded = false;
        public static bool IsAlarmPopup = false;    // 알람 대화상자가 popup 된 상태인듯

        public static EFormType PrevScreen;
        public CDisplayManager DisplayManager = new CDisplayManager();

        public FormTopScreen TopScreen;
        public FormBottomScreen BottomScreen;

        // 공용으로 사용할 ActionTimer
        private static MTickTimer ActionTimer = new MTickTimer();
        
        // Main Form 구성
        private Form[] MainFormArray = new Form[(int)EFormType.MAX];
        
        public static Color BtnBackColor_On = Color.LawnGreen;
        public static Color BtnBackColor_Off = Color.LightGray;
        
        private FormMsgStart m_StartMsgDlg = new FormMsgStart();

        public CMainFrame()
        {
            // Initialize System
            bool bRtn = InitializeCore();
            if(bRtn == false)
            {
            }

            InitializeComponent();

            InitializeForm();
            CreateForms();
            InitScreen();

            MainFrame = this;
            IsFormLoaded = true;
            m_StartMsgDlg.Hide();

        }

        public void InitScreen()
        {
            AttachEventHandlers();

            TopScreen.Show();
            BottomScreen.Show();
            SelectFormChange(EFormType.AUTO);
        }
        
        private void AttachEventHandlers()
        {
            DisplayManager.FormHandler += new FormSelectEventHandler(SelectFormChange);
        }

        private void DetachEventHandlers()
        {
            DisplayManager.FormHandler -= new FormSelectEventHandler(SelectFormChange);
        }

        private void SelectFormChange(EFormType type)
        {
            MainFormArray[(int)PrevScreen].Hide();
            MainFormArray[(int)type].Top = TopScreen.Location.Y + TopScreen.Height + 2;

            MainFormArray[(int)type].Show();
            PrevScreen = type;
        }

        public void ProcessMsg(MEvent evnt)
        {
            string msg = "MainFrame got message from control : " + evnt.ToWindowMessage();
            Debug.WriteLine("===================================================");
            Debug.WriteLine(msg);
            Debug.WriteLine("===================================================");

            var form = MainFormArray[(int)EFormType.AUTO];

            // 변수 선언 및 작업의 편리성을 위해서 일부러 switch대신에 if/else if 구문을 사용함
            if (false)
            {

            }
            else if(evnt.Msg == (int)EWindowMessage.WM_START_READY_MSG)
            {
                ((FormAutoScreen)form).WindowProc(evnt);
                TopScreen.EnableBottomPage(false);
                m_StartMsgDlg.SetMode(FormMsgStart.EStartMsgMode.START_READY);
                m_StartMsgDlg.TopMost = true;
                m_StartMsgDlg.StartPosition = FormStartPosition.CenterParent;
                m_StartMsgDlg.Show();
            }
            else if (evnt.Msg == (int)EWindowMessage.WM_START_RUN_MSG)
            {
                ((FormAutoScreen)form).WindowProc(evnt);
                TopScreen.EnableBottomPage(false);
                m_StartMsgDlg.Hide();
            }
            else if (evnt.Msg == (int)EWindowMessage.WM_START_MANUAL_MSG)
            {
                ((FormAutoScreen)form).WindowProc(evnt);
                TopScreen.EnableBottomPage(true);
                m_StartMsgDlg.Hide();
            }
            else if (evnt.Msg == (int)EWindowMessage.WM_STEPSTOP_MSG)
            {
                ((FormAutoScreen)form).WindowProc(evnt);
                TopScreen.EnableBottomPage(false);
                m_StartMsgDlg.SetMode(FormMsgStart.EStartMsgMode.STEP_STOP);
                m_StartMsgDlg.TopMost = true;
                m_StartMsgDlg.StartPosition = FormStartPosition.CenterParent;
                m_StartMsgDlg.Show();
            }
            else if (evnt.Msg == (int)EWindowMessage.WM_ALARM_MSG)
            {
                DisplayAlarm_Modeless(evnt.lParam, evnt.wParam);
            }
        }

        protected virtual void InitializeForm()
        {
            Screen[] sc = System.Windows.Forms.Screen.AllScreens;

            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;




            if (sc.Length > 1)
            {
                // this.DesktopLocation = new Point(FORM_POS_X + sc[0].Bounds.Width, FORM_POS_Y); // 다중 모니터
                this.DesktopLocation = new Point(FORM_POS_X + sc[1].Bounds.Location.X, FORM_POS_Y); // 다중 모니터
            }
            else
            {
                this.DesktopLocation = new Point(FORM_POS_X, FORM_POS_Y); // 기본 모니터
            }

            //this.DesktopLocation = new Point(FORM_POS_X, FORM_POS_Y); // 기본 모니터

            //this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            this.FormBorderStyle = FormBorderStyle.None;

            this.Size = new Size(FORM_SIZE_WIDTH, FORM_SIZE_HEIGHT);
            this.IsMdiContainer = true;
            
        }

        private void CreateForms()
        {
            TopScreen = new FormTopScreen();
            BottomScreen = new FormBottomScreen();
            

            MainFormArray[(int)EFormType.AUTO]   = new FormAutoScreen();
            MainFormArray[(int)EFormType.MANUAL] = new FormManualScreen();
            MainFormArray[(int)EFormType.DATA]   = new FormDataScreen();
            MainFormArray[(int)EFormType.TEACH]  = new FormTeachScreen();
            MainFormArray[(int)EFormType.LOG]    = new FormLogScreen();
            MainFormArray[(int)EFormType.HELP]   = new FormHelpScreen();

            SetProperty(TopScreen);
            SetProperty(BottomScreen);

            foreach(Form form in MainFormArray)
            {
                SetProperty(form);
            }
        }

        private void SetProperty(Form form)
        {
            form.TopLevel = true;
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            form.MdiParent = this;
        }

        public bool InitializeCore()
        {
            //int iResult = Core.Initialize(MainFrame);
            int iResult = mCore.Initialize(this, out DataManager);
            if (iResult != SUCCESS)
            {
                // Show Error Message & 프로그램 종료?
                mCore.ShowAlarmWhileInit(iResult);
                return false;
            }

            //DataManager = Core.m_DataManager;            

            return true;
        }

        public static void StartTimer()
        {
            ActionTimer.StartTimer();
        }

        public static double GetElapsedTime(ETimeType type = ETimeType.SECOND)
        {
            return ActionTimer.GetElapsedTime(type);
        }

        public static string GetElapsedTIme_Text(bool bShowUnit = true, ETimeType type = ETimeType.SECOND)
        {
            return ActionTimer.GetElapsedTime_Text(bShowUnit, type);
        }

        static public void DisplayAlarm_Modeless(int alarmcode, int pid = 0, bool saveLog = true)
        {
            if (IsAlarmPopup) return; // popup 된 alarm이 확인완료될때까지 기다림
            if (alarmcode == SUCCESS)
            {
                DisplayMsg("요청한 작업이 성공했습니다.");
            }
            else
            {
                CAlarm alarm = mCore.GetAlarmInfo(alarmcode, pid, saveLog);
                //var dlg = new FormAlarmDisplay(alarm);
                //dlg.TopMost = true;
                //dlg.Show();
            }
        }

        static public void DisplayAlarm(int alarmcode, int pid = 0, bool saveLog = true)
        {
            if (IsAlarmPopup) return; // popup 된 alarm이 확인완료될때까지 기다림
            if (alarmcode == SUCCESS)
            {
                DisplayMsg("요청한 작업이 성공했습니다.");
            }
            else
            {
                CAlarm alarm = mCore.GetAlarmInfo(alarmcode, pid, saveLog);
                //var dlg = new FormAlarmDisplay(alarm);
                //dlg.TopMost = true;
                //dlg.ShowDialog();
            }
        }

        static public void DisplayAlarmOnly(int alarmcode, int pid = 0, bool saveLog = true)
        {
            if (IsAlarmPopup) return; // popup 된 alarm이 확인완료될때까지 기다림
            if (alarmcode == SUCCESS)
            {
                //DisplayMsg("요청한 작업이 성공했습니다.");
            }
            else
            {
                CAlarm alarm = mCore.GetAlarmInfo(alarmcode, pid, saveLog);
                //var dlg = new FormAlarmDisplay(alarm);
                //dlg.TopMost = true;
                //dlg.ShowDialog();
            }
        }

        static public bool DisplayMsg(string strMsg, string strTitle = "")
        {
            var dlg = new FormMessageBox();
            dlg.Text = strTitle;
            dlg.SetMessage(strMsg, EMessageType.OK);
            dlg.TopMost = true;
            dlg.ShowDialog();

            if (dlg.DialogResult == DialogResult.OK)
                return true;
            else return false;
        }

        static public bool InquireMsg(string strMsg, string strTitle = "")
        {
            var dlg = new FormMessageBox();
            dlg.Text = strTitle;
            dlg.SetMessage(strMsg, EMessageType.OK_CANCEL);
            dlg.TopMost = true;
            dlg.ShowDialog();

            if (dlg.DialogResult == DialogResult.OK)
                return true;
            else return false;
        }
        

        static public bool GetKeyPad(string strCurrent, out string strModify)
        {
            var dlg = new FormKeyPad(strCurrent);
            dlg.TopMost = true;
            dlg.ShowDialog();

            if (dlg.DialogResult == DialogResult.OK)
            {
                if (dlg.ModifyNo.Text == "")
                {
                    strModify = "0";
                }
                else
                {
                    strModify = dlg.ModifyNo.Text;
                }
            }
            else
            {
                strModify = strCurrent;
                dlg.Dispose();
                return false;
            }
            dlg.Dispose();
            return true;
        }

        static public bool GetKeyboard(out string strModify, string title = "Input", bool SecretMode = false)
        {
            var dlg = new FormKeyBoard(title, SecretMode);
            dlg.TopMost = true;
            dlg.ShowDialog();

            if (dlg.DialogResult == DialogResult.OK)
            {
                strModify = dlg.m_strInput;
                return true;
            }
            else
            {
                strModify = "";
                return false;
            }
        }

        public IEnumerable<Control> GetAllControl(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAllControl(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        private void CMainFrame_Load(object sender, EventArgs e)
        {

        }

    }

    public delegate void FormSelectEventHandler(EFormType type);

    public class CDisplayManager
    {
        public event FormSelectEventHandler FormHandler = null;

        public void FormSelectChange(EFormType type)
        {
            if (FormHandler != null)
            {
                FormHandler(type);
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Core.Layers;
using static Core.Layers.DEF_System;
using static Core.Layers.DEF_Common;
using static Core.Layers.DEF_UI;
using Core;

namespace Core.UI
{
    public partial class FormTopScreen : Form
    {
        public static FormTopScreen TopMenu = null;

        enum EBtnOption
        {
            Enable,
            Click,
            Max,
        }
        public FormTopScreen()
        {
            InitializeComponent();

            InitializeForm();

            ResouceMapping();

            TopMenu = this;
            lblSwVersion.Text = SYSTEM_VER;

            BtnPlayback.FlatStyle = FlatStyle.Flat;
        }

        protected virtual void InitializeForm()
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.DesktopLocation = new Point(DEF_UI.TOP_POS_X, DEF_UI.TOP_POS_Y);
            this.Size = new Size(DEF_UI.TOP_SIZE_WIDTH, DEF_UI.TOP_SIZE_HEIGHT);
            this.FormBorderStyle = FormBorderStyle.None;

            TimerUI.Interval = UITimerInterval;
            TimerUI.Enabled = true;
            TimerUI.Start();
        }

        private void TimerUI_Tick(object sender, EventArgs e)
        {
            //TextTime.Text = DateTime.Now.ToString("yyyy-MM-dd [ddd] <tt> HH:mm:ss");
            
            lblDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd [ HH:mm:ss ]");

            //LabelCurUser.Text = $"Current User : {CMainFrame.DataManager.LoginInfo.User.Name}";
            //BtnUserLogin.Text = $"Login : {CMainFrame.DataManager.LoginInfo.User.Name}";            
        }

        public void SetMessage(string strMsg)
        {
            //TextMessage.Text = strMsg;
        }

        private void BtnUserLogin_Click(object sender, EventArgs e)
        {
            FormUserLogin dlg = new FormUserLogin();
            dlg.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (CMainFrame.InquireMsg("Exit System?"))
            {
                CMainFrame.mCore.CloseSystem();
                
                try
                {                    
                    System.Diagnostics.Process.GetCurrentProcess().Kill();

                    Environment.Exit(0);
                }
                catch
                {

                }                
            }
        }

        Button[] BtnPage = new Button[6];

        private EFormType CurrentPage = EFormType.NONE;


        private void ResouceMapping()
        {
            BtnPage[0] = BtnMainPage;
            BtnPage[1] = BtnManualPage;
            BtnPage[2] = BtnDataPage;
            BtnPage[3] = BtnTeachPage;
            BtnPage[4] = BtnLogPage;
            BtnPage[5] = BtnHelpPage;

            SelectPage(EFormType.AUTO);
        }

        void SelectPage(EFormType index)
        {
            if (CurrentPage == index) return;
            CurrentPage = index;
            for (int i = 0; i < (int)EFormType.MAX; i++)
            {
                if (i == (int)CurrentPage) continue;
                ButtonDisplay(i, EBtnOption.Enable);

                //BtnPage[i].FlatStyle = FlatStyle.Flat;
            }

            ButtonDisplay((int)index, EBtnOption.Click);
            CMainFrame.MainFrame?.DisplayManager.FormSelectChange(index);
        }

        public void EnableBottomPage(bool bEnable)
        {
            for (int i = 0; i < (int)EFormType.MAX; i++)
            {
                if (i == (int)CurrentPage) continue;
                if (bEnable) ButtonDisplay(i, EBtnOption.Enable);
                else ButtonDisplay(i, EBtnOption.Enable);
                BtnPage[i].Enabled = bEnable;
            }
        }

        private void ButtonDisplay(int BtnNo, EBtnOption Option)
        {
            BtnPage[BtnNo].BackgroundImage = ImgList.Images[(int)Option + (BtnNo * 2)];
        }


        private void FormTopScreen_Load(object sender, EventArgs e)
        {

        }


        private void BtnMainPage_Click(object sender, EventArgs e)
        {
            SelectPage(EFormType.AUTO);
        }

        private void BtnManualPage_Click(object sender, EventArgs e)
        {
            SelectPage(EFormType.MANUAL);
        }

        private void BtnTeachPage_Click(object sender, EventArgs e)
        {
            SelectPage(EFormType.TEACH);
        }

        private void BtnDataPage_Click(object sender, EventArgs e)
        {
            SelectPage(EFormType.DATA);
        }

        private void BtnLogPage_Click(object sender, EventArgs e)
        {
            SelectPage(EFormType.LOG);
        }

        private void BtnHelpPage_Click(object sender, EventArgs e)
        {
            SelectPage(EFormType.HELP);
        }
    }
}

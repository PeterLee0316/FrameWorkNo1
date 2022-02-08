using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using Core.Layers;
using static Core.Layers.DEF_Common;
using static Core.Layers.DEF_Thread;
using static Core.Layers.DEF_Error;
using static Core.Layers.DEF_DataManager;

namespace Core.UI
{
    public partial class FormAutoScreen : Form
    {
        int m_nStartReady = 0;      // 0:Off, 1:Ready, 2:Run

        
        public FormAutoScreen()
        {
            InitializeComponent();
            InitializeForm();

            //timer1.Interval = UITimerInterval;
            TimerUI.Interval = 10;
            TimerUI.Enabled = true;
            TimerUI.Start();

        }
        protected virtual void InitializeForm()
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.DesktopLocation = new Point(DEF_UI.MAIN_POS_X, DEF_UI.MAIN_POS_Y);
            this.Size = new Size(DEF_UI.MAIN_SIZE_WIDTH, DEF_UI.MAIN_SIZE_HEIGHT);
            this.FormBorderStyle = FormBorderStyle.None;            
        }


        private void FormAutoScreen_Activated(object sender, EventArgs e)
        {
           
        }

        protected override void WndProc(ref Message wMessage)
        {
            switch (wMessage.Msg)
            {
                default:
                    break;
            }

            base.WndProc(ref wMessage);
        }

        public void WindowProc(MEvent evnt)
        {
            string msg = "FormAutoScreen got message from MainFrame : " + evnt.ToWindowMessage();
            Debug.WriteLine("===================================================");
            Debug.WriteLine(msg);
            Debug.WriteLine("===================================================");

            // 변수 선언 및 작업의 편리성을 위해서 일부러 switch대신에 if/else if 구문을 사용함
            if (false)
            {

            }
            else if (evnt.Msg == (int)EWindowMessage.WM_START_READY_MSG)
            {
                //m_dlgStart.ShowWindow(SW_SHOW);
                //m_dlgErrorStop.ShowWindow(SW_HIDE);
                //m_dlgStepStop.ShowWindow(SW_HIDE);

                // Button Disable 
                m_nStartReady = 1;
                SetButtonStatus(true);
            }
            else if (evnt.Msg == (int)EWindowMessage.WM_START_RUN_MSG)
            {
                //m_dlgStart.ShowWindow(SW_HIDE);
                //m_dlgErrorStop.ShowWindow(SW_HIDE);
                //m_dlgStepStop.ShowWindow(SW_HIDE);

                // Button Disable 
                m_nStartReady = 2;
                SetButtonStatus(true);
            }
            else if (evnt.Msg == (int)EWindowMessage.WM_START_MANUAL_MSG)
            {
                //m_dlgStart.ShowWindow(SW_HIDE);
                //m_dlgErrorStop.ShowWindow(SW_HIDE);
                //m_dlgStepStop.ShowWindow(SW_HIDE);

                // Button Disable 
                m_nStartReady = 0;
                SetButtonStatus(false);
            }
            else if (evnt.Msg == (int)EWindowMessage.WM_STEPSTOP_MSG)
            {
                //m_dlgStart.ShowWindow(SW_HIDE);
                //m_dlgErrorStop.ShowWindow(SW_HIDE);
                //m_dlgStepStop.ShowWindow(SW_SHOW);

                SetButtonStatus(true);
            }
            else if (evnt.Msg == (int)EWindowMessage.WM_ERRORSTOP_MSG)
            {
                //m_dlgStart.ShowWindow(SW_HIDE);
                //m_dlgErrorStop.ShowWindow(SW_SHOW);
                //m_dlgStepStop.ShowWindow(SW_HIDE);

            }
            else if (evnt.Msg == (int)EWindowMessage.WM_ALARM_MSG)
            {
            }
            else if (evnt.Msg ==(int)EWindowMessage.WM_DISP_PANEL_DISTANCE_MSG1)
            {
                //m_sMeasuredDistCell1.SetWindowText(m_pTrsStage1->GetAlignResult());
            }
            else if (evnt.Msg ==(int)EWindowMessage.WM_DISP_TACTTIME_MSG)
            {
                //// EQ Tact Time 기록하기 
                //str.Format("%.2f", *(double*)wParam);
                //m_LblEqTactTime.SetWindowText(str);

                //// Line Tact Time 기록하기 
                //str.Format("%.2f", *(double*)lParam);
                //m_LblLineTactTime.SetWindowText(str);
            }
            else if (evnt.Msg ==(int)EWindowMessage.WM_DISP_PRODUCT_IN_MSG)
            {
                //m_ProductData.uiProductQuantity_forIn++;
                //str.Format("IN %d, OUT %d", m_ProductData.uiProductQuantity_forIn, m_ProductData.uiProductQuantity_forOut);
                //m_LblProductCnt.SetWindowText(str);
                //MSiSystem.SaveProductData(m_ProductData);
            }         

            else
            {
                msg = "FormAutoScreen unknown message : " + evnt;
                Debug.WriteLine("***************************************************");
                Debug.WriteLine(msg);
                Debug.WriteLine("***************************************************");
            }
        }



        void SetButtonStatus(bool bRunStart)
        {
            bool bEnable = bRunStart ? false : true;
            if(bRunStart)
            {
                //BtnStart.Text = "Stop AutoRun";

            }
            else
            {
                //BtnStart.Text = "Start AutoRun";

            }
   
        }

        private void TimerUI_Tick(object sender, EventArgs e)
        {

        }

        private void FormAutoScreen_Load(object sender, EventArgs e)
        {

        }
        

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

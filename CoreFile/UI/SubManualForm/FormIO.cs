using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Core.Layers;

using static Core.Layers.DEF_System;
using static Core.Layers.DEF_Common;

namespace Core.UI
{
    public partial class FormIO : Form
    {
        private const int MaxRowSize = 16;
        private const int MaxPageSize = 15;

        private Label[] X_Title = new Label[MaxRowSize];
        private Label[] X_Name  = new Label[MaxRowSize];
        private Label[] Y_Title = new Label[MaxRowSize];
        private Label[] Y_Name  = new Label[MaxRowSize];

        private int nIOPage = 0;

        public FormIO()
        {
            InitializeComponent();

            ResouceMapping();
        }

        private void TimerUI_Tick(object sender, EventArgs e)
        {
            string str;
            int addr;
            bool bStatus;

        }

        private void UpdateIO(int nBoardNo)
        {
            string hex;
            int nNo = 0;

            for (int i = 0; i < MaxRowSize; i++)
            {
                if (nBoardNo > 0)
                {
                    nNo = i + (nBoardNo * MaxRowSize);
                }
                else
                {
                    nNo = i;
                }
            }
        }

        private void ResouceMapping()
        {
            X_Title[0]  = lblInputX1;
            X_Title[1]  = lblInputX2;
            X_Title[2]  = lblInputX3;
            X_Title[3]  = lblInputX4;
            X_Title[4]  = lblInputX5;
            X_Title[5]  = lblInputX6;
            X_Title[6]  = lblInputX7;
            X_Title[7]  = lblInputX8;
            X_Title[8]  = lblInputX9;
            X_Title[9]  = lblInputX10;
            X_Title[10] = lblInputX11;
            X_Title[11] = lblInputX12;
            X_Title[12] = lblInputX13;
            X_Title[13] = lblInputX14;
            X_Title[14] = lblInputX15;
            X_Title[15] = lblInputX16;

            X_Name[0]   = lblInputName1;
            X_Name[1]   = lblInputName2;
            X_Name[2]   = lblInputName3;
            X_Name[3]   = lblInputName4;
            X_Name[4]   = lblInputName5;
            X_Name[5]   = lblInputName6;
            X_Name[6]   = lblInputName7;
            X_Name[7]   = lblInputName8;
            X_Name[8]   = lblInputName9;
            X_Name[9]   = lblInputName10;
            X_Name[10]  = lblInputName11;
            X_Name[11]  = lblInputName12;
            X_Name[12]  = lblInputName13;
            X_Name[13]  = lblInputName14;
            X_Name[14]  = lblInputName15;
            X_Name[15]  = lblInputName16;

            Y_Title[0]  = lblOutputY1;
            Y_Title[1]  = lblOutputY2;
            Y_Title[2]  = lblOutputY3;
            Y_Title[3]  = lblOutputY4;
            Y_Title[4]  = lblOutputY5;
            Y_Title[5]  = lblOutputY6;
            Y_Title[6]  = lblOutputY7;
            Y_Title[7]  = lblOutputY8;
            Y_Title[8]  = lblOutputY9;
            Y_Title[9]  = lblOutputY10;
            Y_Title[10] = lblOutputY11;
            Y_Title[11] = lblOutputY12;
            Y_Title[12] = lblOutputY13;
            Y_Title[13] = lblOutputY14;
            Y_Title[14] = lblOutputY15;
            Y_Title[15] = lblOutputY16;

            Y_Name[0]   = lblOutputName1;
            Y_Name[1]   = lblOutputName2;
            Y_Name[2]   = lblOutputName3;
            Y_Name[3]   = lblOutputName4;
            Y_Name[4]   = lblOutputName5;
            Y_Name[5]   = lblOutputName6;
            Y_Name[6]   = lblOutputName7;
            Y_Name[7]   = lblOutputName8;
            Y_Name[8]   = lblOutputName9;
            Y_Name[9]   = lblOutputName10;
            Y_Name[10]  = lblOutputName11;
            Y_Name[11]  = lblOutputName12;
            Y_Name[12]  = lblOutputName13;
            Y_Name[13]  = lblOutputName14;
            Y_Name[14]  = lblOutputName15;
            Y_Name[15]  = lblOutputName16;
        }

        private void FormIO_Load(object sender, EventArgs e)
        {
            TimerUI.Enabled = true;
            TimerUI.Interval = UITimerInterval;
            TimerUI.Start();

            UpdateIO(nIOPage);
        }

        private void BtnPrev_Click(object sender, EventArgs e)
        {
            if (nIOPage != 0)
            {
                nIOPage--;
            }
            UpdateIO(nIOPage);
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (nIOPage < MaxPageSize)
            {
                nIOPage++;
            }

            UpdateIO(nIOPage);
        }

        private void IO_Y_Click(object sender, EventArgs e)
        {
            

            //int nNo = Convert.ToInt16(OutPut.Tag);

            ////strText = string.Format("{0:s} 출력을 강제로 Toggle 하시겠습니까?", OutPut.Text);
            //if (!CMainFrame.InquireMsg("Toggle Ouput?"))
            //{
            //    return;
            //}

            //// Output 출력
            //string str = OutPut.Text.Substring(1);
            //int addr = OUTPUT_ORIGIN + Convert.ToInt32(str, 16);
            //CMainFrame.mCore.m_IO.OutputToggle(addr);
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormIO_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            nIOPage = 0;
            UpdateIO(nIOPage);
        }

        private void BtnEnd_Click(object sender, EventArgs e)
        {
            nIOPage = MaxPageSize - 1;
            UpdateIO(nIOPage);
        }
    }
}

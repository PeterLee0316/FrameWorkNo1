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
using static Core.Layers.DEF_DataManager;
using static Core.Layers.DEF_Thread;
using static Core.Layers.DEF_MeStage;


namespace Core.UI
{
    public partial class FormScannerTeach : Form
    {

        private int m_nSelectedPos = 0;

        public bool Type_Fixed; // 고정좌표, 옵셋좌표 구분


        public FormScannerTeach()
        {
            InitializeComponent();            
        }

        private void FormScannerTeach_Load(object sender, EventArgs e)
        {
            this.DesktopLocation = new Point(10, 128);

            this.Text = "Scanner Part Teaching Screen";
        }

        private void FormScannerTeach_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

        private void TimerUI_Tick(object sender, EventArgs e)
        {
            // Current Position Display
            string strCurPos = string.Empty;
            double dValue = 0, dCurPos = 0, dTargetPos = 0;
            
        }
        
    }
}

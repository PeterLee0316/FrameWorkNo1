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
    public partial class FormWorkStageTeach : Form
    {

        private int m_nSelectedPos_Stage = 0;

        public bool Type_Fixed; // 고정좌표, 옵셋좌표 구분

        public FormWorkStageTeach()
        {
            InitializeComponent();
            
        }

        private void FormWorkStageTeach_Load(object sender, EventArgs e)
        {
            this.DesktopLocation = new Point(10, 128);

            this.Text = "Work Stage Part Teaching Screen";
        }

        private void FormWorkStageTeach_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}

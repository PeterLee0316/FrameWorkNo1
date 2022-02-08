using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static Core.Layers.DEF_Vision;
using static Core.Layers.DEF_Common;
using static Core.Layers.DEF_DataManager;
using static Core.Layers.DEF_System;

namespace Core.UI
{
    public partial class FormVisionData : Form
    {

        int iCurrentView = 0;
        int iCurrentMode = 0;
        int iHairLineWidth = 100;
        Size PatternRec = new Size(100, 100);

        public FormVisionData()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormVisionData_Load(object sender, EventArgs e)
        {
            this.DesktopLocation = new Point(10, 128);
            
                        

        }


        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }

        private void FormVisionData_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
        


        private void btnCameraDataLoad_Click(object sender, EventArgs e)
        {
        }

        private void btnCameraDataSave_Click(object sender, EventArgs e)
        {
            string strData = string.Empty;
            string strMsg = "Save Camera data?";
            if (!CMainFrame.InquireMsg(strMsg)) return;
            
            CMainFrame.DataManager.SaveSystemData(null, null);
            
        }
    }
}

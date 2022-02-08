using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Core.Layers;

using static Core.Layers.DEF_Thread;
using static Core.Layers.DEF_System;

namespace Core.UI
{
    public partial class FormTeachScreen : Form
    {
        public FormTeachScreen()
        {
            InitializeComponent();

            InitializeForm();
        }
        protected virtual void InitializeForm()
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.DesktopLocation = new Point(DEF_UI.MAIN_POS_X, DEF_UI.MAIN_POS_Y);
            this.Size = new Size(DEF_UI.MAIN_SIZE_WIDTH, DEF_UI.MAIN_SIZE_HEIGHT);
            this.FormBorderStyle = FormBorderStyle.None;
        }
        

        private void BtnTeachStage_Click(object sender, EventArgs e)
        {
            var dlg = new FormWorkStageTeach();
            dlg.Type_Fixed = true;
            dlg.ShowDialog();
        }

        private void BtnCameraScanner_Click(object sender, EventArgs e)
        {
            var dlg = new FormCameraTeach();
            dlg.Type_Fixed = true;
            dlg.ShowDialog();
        }

        private void BtnScanner_Click(object sender, EventArgs e)
        {
            var dlg = new FormScannerTeach();
            dlg.Type_Fixed = true;
            dlg.ShowDialog();
        }
        

        private void BtnModelStage_Click(object sender, EventArgs e)
        {
            var dlg = new FormWorkStageTeach();
            dlg.Type_Fixed = false;
            dlg.ShowDialog();
        }

        private void BtnCamSetCalib_Click(object sender, EventArgs e)
        {
            var dlg = new FormCameraTeach();
            dlg.Type_Fixed = false;
            dlg.ShowDialog();
        }
    }
}

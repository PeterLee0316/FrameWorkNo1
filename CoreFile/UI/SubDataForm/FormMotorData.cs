using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static Core.Layers.DEF_DataManager;
using static Core.Layers.DEF_System;

namespace Core.UI
{
    public partial class FormMotorData : Form
    {

        public FormMotorData()
        {

            InitializeComponent();
            InitializeForm();
        }

        protected virtual void InitializeForm()
        {
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormMotorData_Load(object sender, EventArgs e)
        {
            this.DesktopLocation = new Point(10, 128);
            
        }

        private void FormMotorData_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!CMainFrame.InquireMsg("Save data?"))
            {
                return;
            }

            // Motor Data Sheet
            int gridIndex = 1;
        }

        private void checkBoxAll_CheckStateChanged(object sender, EventArgs e)
        {

        }
    }
}

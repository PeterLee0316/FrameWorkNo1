using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static Core.Layers.DEF_Error;
using static Core.Layers.DEF_Common;
using static Core.Layers.DEF_DataManager;

namespace Core.UI
{
    public partial class FormModelData : Form
    {
        private CModelData m_ModelData;

        public FormModelData()
        {
            InitializeComponent();

            m_ModelData = ObjectExtensions.Copy(CMainFrame.DataManager.ModelData);
            
            this.Text = $"Model Data [ Current Model : {m_ModelData.Name} ]";
        }

        private void FormModelData_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!CMainFrame.InquireMsg("Save data?"))
            {
                return;
            }
            
            CMainFrame.mCore.SaveModelData(m_ModelData);
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

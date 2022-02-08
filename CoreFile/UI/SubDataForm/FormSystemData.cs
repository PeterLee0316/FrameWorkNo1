using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static Core.Layers.DEF_Common;
using static Core.Layers.DEF_DataManager;

namespace Core.UI
{
    public partial class FormSystemData : Form
    {
        private int SelLanguage;
        private CSystemData SystemData;

        public FormSystemData()
        {
            SystemData = ObjectExtensions.Copy(CMainFrame.DataManager.SystemData);
            InitializeComponent();
            
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormSystemData_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!CMainFrame.InquireMsg("Save data?"))
            {
                return;
            }

            // Data Reflesh
            SystemData.Language = (ELanguage)SelLanguage;
            SystemData.data1 = Convert.ToDouble(label1.Text);

            CMainFrame.mCore.SaveSystemData(SystemData);
        }

        private void FormSystemData_Load(object sender, EventArgs e)
        {
            label1.Text = Convert.ToString(SystemData.data1);

        }

        private void ComboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ComboLanguage = (ComboBox)sender;

            SelLanguage = (int)ComboLanguage.SelectedIndex;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Label data = sender as Label;

            string strCurrent, strModify;

            strCurrent = data.Text;

            if (!CMainFrame.GetKeyPad(strCurrent, out strModify))
            {
                return;
            }

            data.Text = strModify;
            data.ForeColor = Color.Red;
        }
    }
}

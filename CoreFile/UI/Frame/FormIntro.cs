using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Layers;

namespace Core.UI
{
    public partial class FormIntro : Form
    {
        public FormIntro()
        {
            InitializeComponent();
            CUtils.AnimateEffect.AnimateWindow(this.Handle, 1000, CUtils.AnimateEffect.AW_ACTIVATE | CUtils.AnimateEffect.AW_BLEND);


        }

        public void SetStatus(string strText, int nProgress)
        {
            StatusBar.Value = nProgress;
            LabelStatus.Text = strText;

            Update();
        }

        private void FormIntro_Load(object sender, EventArgs e)
        {
            
            StatusBar.Value = 5;
            LabelStatus.Text = "Start System Program";
            LabelStatus.ForeColor = Color.White;
            LabelStatus.Visible = true;


        }

    }
}

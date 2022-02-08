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

namespace Core.UI
{
    public partial class FormKeyBoard : Form
    {
        public string m_strInput = "";
        bool SecretMode;

        int InputLimit = 70;

        public FormKeyBoard(string title, bool SecretMode = false)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.Fixed3D;

            MaximizeBox = false;
            MinimizeBox = false;

            CUtils.AnimateEffect.AnimateWindow(this.Handle, 300, CUtils.AnimateEffect.AW_ACTIVATE | CUtils.AnimateEffect.AW_BLEND);

            if (String.IsNullOrWhiteSpace(title)) BtnTitle.Visible = false;
            BtnTitle.Text = title;
            this.SecretMode = SecretMode;
        }

        private void BtnNo_Click(object sender, EventArgs e)
        {
            if (m_strInput.Length > InputLimit) return;
            Button Btn = sender as Button;
            m_strInput = m_strInput + Btn.Text;
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            string str = m_strInput;
            if(SecretMode)
            {
                str = "";
                for (int i = 0; i < m_strInput.Length; i++)
                    str += "*";
            }
            PresentNo.Text = str;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (m_strInput == "") return;

            int nNo = m_strInput.Length - 1;
            m_strInput = m_strInput.Remove(nNo, 1);
            UpdateDisplay();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            m_strInput = "";
            UpdateDisplay();
        }

        private void FormKeyBoard_KeyPress(object sender, KeyPressEventArgs e)
        {
            //int a = Convert.ToInt32(e.KeyChar);
            //Debug.WriteLine($"char : {e.KeyChar}, key : {a}");
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) // number
                || (e.KeyChar >= 97 && e.KeyChar <= 122) // lower char
                || (e.KeyChar >= 65 && e.KeyChar <= 90) // upper char
                || e.KeyChar == 45 // -
                || e.KeyChar == 95 // _
                || e.KeyChar == 61 // =
                || e.KeyChar == 92 // \
                || e.KeyChar == 36 // $
                || e.KeyChar == 58 // :
                || e.KeyChar == 46 // .
                )
            {
                if (m_strInput.Length > InputLimit) return;
                m_strInput = m_strInput + e.KeyChar;
                UpdateDisplay();
                return;
            }
            else if (e.KeyChar == 8) // backspace
            {
                BtnBack.PerformClick();
            }
        }
    }
}

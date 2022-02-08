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
    public partial class FormKeyPad : Form
    {
        string m_strInput = "";
        string m_strCurrent = "";

        int InputLimit = 15;
        bool m_bPlus = false;
        bool m_bMinus = false;

        public FormKeyPad(string strValue)
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            MinimizeBox = false;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            CUtils.AnimateEffect.AnimateWindow(this.Handle, 300, CUtils.AnimateEffect.AW_ACTIVATE | CUtils.AnimateEffect.AW_BLEND);

            m_strCurrent = strValue;
            UpdateDisplay();
        }

        private void BtnNo_Click(object sender, EventArgs e)
        {
            if (m_strInput.Length > InputLimit) return;

            Button btn = sender as Button;
            m_strInput = m_strInput + btn.Tag;
            UpdateDisplay();
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

        private void UpdateDisplay()
        {
            PresentNo.Text = m_strCurrent;
            ModifyNo.Text = m_strInput;

            if (m_bPlus) BtnPlus.BackColor = CMainFrame.BtnBackColor_On;
            else BtnPlus.BackColor = CMainFrame.BtnBackColor_Off;
            if (m_bMinus) BtnMinus.BackColor = CMainFrame.BtnBackColor_On;
            else BtnMinus.BackColor = CMainFrame.BtnBackColor_Off;
        }

        private void BtnComma_Click(object sender, EventArgs e)
        {
            if (m_strInput.Length > InputLimit) return;

            m_strInput = m_strInput.Replace(".", "");
            m_strInput = m_strInput + ".";
            UpdateDisplay();
        }

        private void BtnSign_Click(object sender, EventArgs e)
        {
            if (m_strInput.Length > InputLimit) return;
            if (m_strInput == "") return;

            if (m_strInput[0] == '-') m_strInput = m_strInput.Replace("-", "");
            else m_strInput = "-" + m_strInput;
            UpdateDisplay();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {

        }

        private void FormKeyPad_Load(object sender, EventArgs e)
        {
        }

        private void BtnPlus_Click(object sender, EventArgs e)
        {
            m_bPlus = !m_bPlus;
            m_bMinus = false;
            if (m_bPlus) m_strInput = "";
            UpdateDisplay();
        }

        private void BtnMinus_Click(object sender, EventArgs e)
        {
            m_bPlus = false;
            m_bMinus = !m_bMinus;
            if (m_bMinus) m_strInput = "";
            UpdateDisplay();
        }

        private void BtnCalc_Click(object sender, EventArgs e)
        {
            if ((m_bPlus | m_bMinus) == false) return;

            try
            {
                double d1 = Convert.ToDouble(m_strCurrent);
                double d2 = Convert.ToDouble(m_strInput);

                if (m_bPlus) d1 += d2;
                if (m_bMinus) d1 -= d2;
                m_strInput = $"{d1}";
                m_bPlus = m_bMinus = false;
                UpdateDisplay();
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void BtnSet_Click(object sender, EventArgs e)
        {
            m_strCurrent = m_strInput;
            m_strInput = "";
            UpdateDisplay();
        }

        private void FormKeyPad_KeyPress(object sender, KeyPressEventArgs e)
        {
            //int a = Convert.ToInt32(e.KeyChar);
            //Debug.WriteLine($"char : {e.KeyChar}, key : {a}");
            if (e.KeyChar >= 48 && e.KeyChar <= 57) // number
            {
                if (m_strInput.Length > InputLimit) return;
                m_strInput = m_strInput + e.KeyChar;
                UpdateDisplay();
                return;
            } else if (e.KeyChar == 8) // backspace
            {
                BtnBack.PerformClick();
            } else 
            {
                switch(e.KeyChar)
                {
                    case '.':
                        BtnComma.PerformClick();
                        break;

                    case '+':
                        BtnPlus.PerformClick();
                        break;

                    case '-':
                        BtnMinus.PerformClick();
                        break;

                    case '=':
                        BtnCalc.PerformClick();
                        break;
                }
            }
        }
    }
}

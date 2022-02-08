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
using static Core.Layers.DEF_Common;

namespace Core.UI
{
    public partial class FormChangePassword : Form
    {
        string UserName;
        public FormChangePassword(string name)
        {
            UserName = name;
            InitializeComponent();
        }

        private void BtnChange_Click(object sender, EventArgs e)
        {
            CUserInfo info;
            CMainFrame.DataManager.LoadUserInfo(UserName, out info);
            if (info.Password != CurrentPW.Text)
            {
                CMainFrame.DisplayMsg("current password is wrong");
                return;
            }


            if (NewPW1.Text != NewPW2.Text)
            {
                CMainFrame.DisplayMsg("new password are not matched.");
                return;
            }

            if (String.IsNullOrWhiteSpace(NewPW1.Text))
            {
                CMainFrame.DisplayMsg("input new password.");
                return;
            }

            if (!CMainFrame.InquireMsg("Do you like to change password?"))
            {
                return;
            }

            info.Password = NewPW2.Text;
            int iResult = CMainFrame.mCore.SaveUserData(info);
            CMainFrame.DisplayAlarm(iResult);
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormInputPassword_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void CurrentPW_Click(object sender, EventArgs e)
        {
            string strModify;

            if (!CMainFrame.GetKeyboard(out strModify, "Input Current Password", true))
            {
                //CurrentPW.Text = ""; NewPW1.Text = ""; NewPW2.Text = "";
                return;
            }

            CurrentPW.Text = strModify;
        }

        private void NewPW1_Click(object sender, EventArgs e)
        {
            string strModify;

            //if(CurrentPW.Text == "" || CMainFrame.DataManager.LoginInfo.User.Password != CurrentPW.Text)
            //{
            //    NewPW1.Text = "";
            //    return;
            //}

            if (!CMainFrame.GetKeyboard(out strModify, "Input New Password", true))
            {
                NewPW1.Text = "";
                return;
            }

            NewPW1.Text = strModify;
        }

        private void NewPW2_Click(object sender, EventArgs e)
        {
            string strModify;

            //if (CurrentPW.Text == "" || NewPW1.Text == "")
            //{
            //    NewPW2.Text = "";
            //    return;
            //}

            if (!CMainFrame.GetKeyboard(out strModify, "Confirm New Password", true))
            {
                NewPW2.Text = "";
                return;
            }

            NewPW2.Text = strModify;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

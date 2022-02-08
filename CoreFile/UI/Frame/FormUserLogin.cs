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
using static Core.Layers.DEF_Error;
using static Core.Layers.DEF_DataManager;

namespace Core.UI
{
    public partial class FormUserLogin : Form
    {
        private List<CListHeader> InfoHeaderList;
        private string[] UserList;


        public FormUserLogin()
        {
            InitializeComponent();

            InfoHeaderList = CMainFrame.DataManager.UserInfoHeaderList;
            UserList = new string[InfoHeaderList.Count];

            // add user
            int nCount=0;
            AddUserToList(ref nCount, ELoginType.OPERATOR.ToString());
            AddUserToList(ref nCount, ELoginType.ENGINEER.ToString());
            AddUserToList(ref nCount, ELoginType.MAKER.ToString());

            // Default Operator
            if (CMainFrame.DataManager.LoginInfo.User.Name == NAME_DEFAULT_OPERATOR)
            {
                BtnLogout.Enabled = false;
            }
            else
            {
                BtnLogout.Enabled = true;
            }


            //this.Text = $"User Login [ Current Model : {CMainFrame.DataManager.ModelData.Name} ]";
        }

        private void AddUserToList(ref int nCount, string strParent)
        {
            string strName;

            foreach (CListHeader info in InfoHeaderList)
            {
                if (info.Parent != strParent) continue;
                if (info.IsFolder == false)
                {
                    strName = $"[{info.Parent}] : {info.Name}";

                    ComboUser.Items.Add(strName);
                    UserList[nCount] = info.Name;
                    nCount++;

                    if (CMainFrame.DataManager.LoginInfo.User.Name == info.Name)
                    {
                        ComboUser.SelectedIndex = nCount - 1;
                    }
                }
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (!CMainFrame.InquireMsg("Login selected user?"))
            {
                return;
            }

            // Password Check
            string strModify;

            if (!CMainFrame.GetKeyboard(out strModify, $"Input Password", true))
            {
                return;
            }

            CUserInfo info;
            int iResult = CMainFrame.DataManager.LoadUserInfo(UserList[ComboUser.SelectedIndex], out info);
            string password = info.Name == NAME_MAKER ? info.GetMakerPassword() : info.Password;
            if (strModify != password)
            {
                CMainFrame.DisplayMsg("You have inputted wrong password.");
                return;
            }

            CMainFrame.DataManager.Login(info.Name);

            this.Close();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ComboUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (CListHeader info in InfoHeaderList)
            {
                if (info.IsFolder == false)
                {
                    if(UserList[ComboUser.SelectedIndex] == info.Name)
                    {
                        LabelComment.Text = info.Comment;
                        LabelType.Text = info.Parent;

                        // Maker
                        if(info.Name == NAME_MAKER || info.Name == NAME_DEFAULT_OPERATOR)
                        {
                            BtnChangePW.Enabled = false;
                        } else
                        {
                            BtnChangePW.Enabled = true;
                        }

                        // Current User
                        if(CMainFrame.DataManager.LoginInfo.User.Name == info.Name)
                        {
                            BtnLogin.Enabled = false;
                        } else
                        {
                            BtnLogin.Enabled = true;
                        }

                        break;
                    }
                }
            }
        }

        private void FormUserLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void BtnChangePW_Click(object sender, EventArgs e)
        {

            //FormChangePassword dlg = new FormChangePassword(UserList[ComboUser.SelectedIndex]);
            //dlg.ShowDialog();
            CUserInfo info;
            CMainFrame.DataManager.LoadUserInfo(UserList[ComboUser.SelectedIndex], out info);

            string strModify;

            // check current. if maker, not check current
            if(CMainFrame.DataManager.LoginInfo.User.Type != ELoginType.MAKER)
            {
                if (!CMainFrame.GetKeyboard(out strModify, "Input Current Password", true))
                    return;
                if (info.Password != strModify)
                {
                    CMainFrame.DisplayMsg("current password is wrong");
                    return;
                }
            }

            // check new
            if (!CMainFrame.GetKeyboard(out strModify, "Input New Password", true))
                return;

            if(string.IsNullOrWhiteSpace(strModify))
            {
                CMainFrame.DisplayMsg("input password is null or white space");
                return;
            }
            info.Password = strModify;

            // check confirm
            if (!CMainFrame.GetKeyboard(out strModify, "Confirm New Password", true))
                return;

            if (info.Password != strModify)
            {
                CMainFrame.DisplayMsg("new password are not matched.");
                return;
            }

            // save
            int iResult = CMainFrame.mCore.SaveUserData(info);
            CMainFrame.DisplayAlarm(iResult);

        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            CMainFrame.DataManager.Logout();
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.Security;
using PEA.BPM.ToolModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.ToolModule.Interface.Constants;

namespace PEA.BPM.ToolModule
{
    public partial class CreateUserView : Form
    {
        private RoleUserSwitchViewPresenter _presenter;
        private User _user;

        public CreateUserView()
        {
            InitializeComponent();
            scopeCBox.SelectedIndex = 0;
            cancelBt.Focus();
        }

        public User ChosenUser
        {
            set { _user = value; }
            get { return _user; }
        }

        public RoleUserSwitchViewPresenter Presenter
        {
            set { _presenter = value; }
        }

        private void okBt_Click(object sender, EventArgs e)
        {
            try
            {
                if (loginNameText.Text == string.Empty)
                {
                    MessageBox.Show(null, "กรุณาเลือกรายการพนักงาน", "กรุณาเลือก", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }

                if (pwdText.Text != rePwdText.Text)
                {
                    MessageBox.Show(null, "ป้อนรหัสผ่านไม่ตรงกัน กรุณาป้อนใหม่", "ข้อมูลผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }

                _user.ScopeId = GetScopeId();
                _user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(pwdText.Text, "SHA1");
                _user.ModifiedBy = Session.User.Id;
                _presenter.CreateUser(_user);

                MessageBox.Show(null, "สร้างผู้ใช้งานใหม่เรียบร้อยแล้ว", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                try
                {
                    if (!Session.Branch.OnlineConnection && Session.IsNetworkConnectionAvailable) //has branch server
                        PEA.BPM.Architecture.ArchitectureTool.Security.Authorization.SignalSyncup(LocalSettingNames.DL070_TECHNICAL_BATCH);  //syncdown
                }
                catch (Exception k)
                {
                    //ignored
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private string GetScopeId()
        {
            string scopeId = null;
            if (scopeCBox.SelectedIndex == 0)
                scopeId = "B";
            else if (scopeCBox.SelectedIndex == 1)
                scopeId = "R";
            else
                scopeId = "A";

            return scopeId;
        }

        private void pwdText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (pwdText.Text.Trim() == String.Empty)
                {
                    MessageBox.Show("กรุณาป้อนรหัสผ่านใหม่", "ข้อมูลไม่ครบถ้วน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    pwdText.Focus();
                }
                else
                    rePwdText.Focus();
            }
        }

        private void rePwdText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if ((pwdText.Text.Trim() == String.Empty) || (rePwdText.Text.Trim() == String.Empty))
                {
                    MessageBox.Show("กรุณาป้อนรหัสผ่านใหม่(ทวน)", "ข้อมูลไม่ครบถ้วน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rePwdText.Focus();
                    return;
                }

                if (pwdText.Text != rePwdText.Text)
                {
                    MessageBox.Show("รหัสผ่านใหม่และรหัสผ่านทวนไม่ถูกต้อง", "ข้อมูลไม่ครบถ้วน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rePwdText.ResetText();
                    rePwdText.Focus();
                }
                else
                    okBt.Focus();
            }
        }

        private void findBt_Click(object sender, EventArgs e)
        {
            AddUserView addUserView = new AddUserView();
            addUserView.Presenter = _presenter;
            addUserView.UserMode = false; //create user by using employee 

            if (addUserView.ShowDialog() == DialogResult.OK)
            {
                _user = addUserView.ChosenUser;
                loginNameText.Text = _user.UserId;
                userNameText.Text = _user.FullName;
                departmentText.Text = _user.Department;
                pwdText.Enabled = true;
                rePwdText.Enabled = true;
                pwdText.Focus();
            }
        }

        private void rePwdText_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(pwdText.Text.Trim()))
            {
                MessageBox.Show("กรุณาป้อนรหัสผ่านในช่องแรกก่อน", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pwdText.Focus();
                return;
            }
        }

        private void pwdText_TextChanged(object sender, EventArgs e)
        {
            if (pwdText.Text.Length > 0 && pwdText.Text == rePwdText.Text)
                okBt.Enabled = true;
            else
                okBt.Enabled = false;
        }

        private void rePwdText_TextChanged(object sender, EventArgs e)
        {
            if (rePwdText.Text.Length > 0 && pwdText.Text == rePwdText.Text)
                okBt.Enabled = true;
            else
                okBt.Enabled = false;
        }

        private void loginNameText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(loginNameText.Text.Trim()))
                {
                    List<User> userList = _presenter.ListEmployee(loginNameText.Text);
                    if (userList.Count > 0)
                    {
                        _user = userList[0];
                        loginNameText.Text = _user.UserId;
                        userNameText.Text = _user.FullName;
                        departmentText.Text = _user.Department;
                        pwdText.Enabled = true;
                        rePwdText.Enabled = true;
                        pwdText.Focus();
                    }
                    else
                    {
                        _user = null;
                        userNameText.Text = "";
                        departmentText.Text = "";
                        loginNameText.Focus();
                        loginNameText.SelectAll();
                    }
                }
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.Architecture.CommonUtilities;
using System.Web.Security;
using PEA.BPM.ToolModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureTool.ErrorHandling;

namespace PEA.BPM.ToolModule
{
    public partial class UserPwdChangeView : Form
    {
        private User _user;
        private RoleUserSwitchViewPresenter _presenter;

        public UserPwdChangeView()
        {
            InitializeComponent();
        }

        public User ChosenUser
        {
            get { return _user; }
            set 
            { 
                _user = value;
                userNameText.Text = string.Format("{0}: {1}", _user.UserId, _user.FullName);
            }
        }

        public RoleUserSwitchViewPresenter Presenter
        {
            set { _presenter = value; }
        }

        private void okBt_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                if ((pwdText.Text.Trim() == String.Empty) || (rePwdText.Text.Trim() == String.Empty))
                {
                    MessageBox.Show("กรุณาป้อนรหัสผ่านใหม่/รหัสผ่านใหม่(ทวน)", "ข้อมูลไม่ครบถ้วน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    pwdText.ResetText();
                    rePwdText.ResetText();
                    pwdText.Focus();
                    return;
                }

                if (pwdText.Text != rePwdText.Text)
                {
                    MessageBox.Show("รหัสผ่านใหม่และรหัสผ่านทวนไม่ถูกต้อง", "ข้อมูลไม่ครบถ้วน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    pwdText.ResetText();
                    rePwdText.ResetText();
                    pwdText.Focus();
                    return;
                }

                //////////// DCR 67-020 : StrongPassword ////////////
                _user.PwdState = 4;
                _user.NewPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(pwdText.Text, "SHA1");
                _user.ModifiedBy = Session.User.Id;
                _presenter.EditUser(_user);

                MessageBox.Show("เปลี่ยนแปลงรหัสผ่านเรียบร้อยแล้ว", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Tools, ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
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
                    pwdText.ResetText();
                    rePwdText.ResetText();
                    pwdText.Focus();
                }
                else
                    okBt.Focus();
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
            if (pwdText.Text.Length > 0 && pwdText.Text == rePwdText.Text)
                okBt.Enabled = true;
            else
                okBt.Enabled = false;
        }

    }
}

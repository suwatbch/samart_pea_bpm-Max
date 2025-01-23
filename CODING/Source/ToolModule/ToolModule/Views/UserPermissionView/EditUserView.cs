using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.Security;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.ToolModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureTool;

namespace PEA.BPM.ToolModule
{
    public partial class EditUserView : Form
    {
        private User _user;
        private RoleUserSwitchViewPresenter _presenter;
        private const string hint = "<พิมพ์เพื่อเปลี่ยนรหัสผ่าน>";
        private bool loaded = false;
        private bool scopeChanged = false;
        private int originalScopeIndex;

        public EditUserView()
        {
            InitializeComponent();

            OutFocusTextBox(pwdText, hint);
            OutFocusTextBox(rePwdText, hint);
        }

        public RoleUserSwitchViewPresenter Presenter
        {
            set { _presenter = value; }
        }

        public void OutFocusTextBox(TextBox textBox, string hint)
        {
            if (string.IsNullOrEmpty(textBox.Text.Trim()))
            {
                textBox.PasswordChar = '\0';
                textBox.ForeColor = Color.Silver;
                textBox.Text = hint;
            }
        }

        public void InFocusTextBox(TextBox textBox)
        {
            textBox.ForeColor = Color.Black;
            textBox.Text = "";
            textBox.PasswordChar = '*';
            textBox.Focus();
        }

        public User UserInfo
        {
            set
            {
                _user = value;
                userNameText.Text = string.Format("{0}: {1}", _user.UserId, _user.FullName);
                departmentText.Text = _user.Department;

                if(string.IsNullOrEmpty(_user.BranchId))
                    branchTxt.Text = "ไม่ระบุ";
                else
                    branchTxt.Text = string.Format("{0}: {1}", _user.BranchId, _user.BranchName);

                if (_user.ScopeId == "B")
                {
                    scopeCBox.SelectedIndex = 0;
                    originalScopeIndex = 0;
                }
                else if (_user.ScopeId == "R")
                {
                    scopeCBox.SelectedIndex = 1;
                    originalScopeIndex = 1;
                }
                else
                {
                    scopeCBox.SelectedIndex = 2;originalScopeIndex = 0;
                    originalScopeIndex = 2;
                }

                loaded = true;
                cancelBt.Focus();
            }
        }

        private void okBt_Click(object sender, EventArgs e)
        {
            try
            {
                if (pwdText.Text != rePwdText.Text)
                {
                    MessageBox.Show("รหัสผ่านที่ป้อนไม่ตรงกัน", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    rePwdText.Focus();
                    return;
                }

                _user.ChangerId = Session.User.Id;
                _user.UserId = _user.UserId;
                _user.ScopeId = GetScopeId();

                //detect password changed
                if (pwdText.PasswordChar == '*' && pwdText.Text.Length > 0)
                {
                    _user.NewPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(pwdText.Text, "SHA1");
                    _user.PwdState = 4;
                }

                //TO DO - validate user scope
                if (!_presenter.ValidateUserScope(_user)) return;

                _presenter.EditUser(_user);

                MessageBox.Show("แก้ไขข้อมูลเรียบร้อยแล้ว", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "เกิดข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                this.DialogResult = DialogResult.OK;
                this.Close();
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

        private void pwdText_Enter(object sender, EventArgs e)
        {
            InFocusTextBox(pwdText);
        }

        private void pwdText_Leave(object sender, EventArgs e)
        {
            OutFocusTextBox(pwdText, hint);
        }

        private void rePwdText_Enter(object sender, EventArgs e)
        {
            if (pwdText.PasswordChar != '*') //not enter first textbox yet
            {
                MessageBox.Show("กรุณาป้อนรหัสผ่านในช่องแรกก่อน", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pwdText.Focus();
                return;
            }

            InFocusTextBox(rePwdText);
        }

        private void rePwdText_Leave(object sender, EventArgs e)
        {
            OutFocusTextBox(rePwdText, hint);
        }

        private void scopeCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded && scopeCBox.SelectedIndex != originalScopeIndex)
            {
                okBt.Enabled = true;
                scopeChanged = true;
            }
            else
            {
                if (pwdText.PasswordChar != '*')
                {
                    okBt.Enabled = false;
                    scopeChanged = false;
                }
            }
        }

        private void pwdText_TextChanged(object sender, EventArgs e)
        {
            if (pwdText.PasswordChar == '*' && pwdText.Text.Length > 0 && pwdText.Text == rePwdText.Text)
                okBt.Enabled = true;
            else
            {
                if (!scopeChanged)
                    okBt.Enabled = false;
            }

        }

        private void pwdText_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                rePwdText.Focus();
        }

        private void rePwdText_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                okBt.Focus();
        }

        private void rePwdText_TextChanged(object sender, EventArgs e)
        {
            if (rePwdText.PasswordChar == '*' && rePwdText.Text.Length > 0 && pwdText.Text == rePwdText.Text)
                okBt.Enabled = true;
            else
            {
                if (!scopeChanged)
                    okBt.Enabled = false;
            }
        }

    }
}

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

namespace PEA.BPM.Architecture.ArchitectureTool.Security
{
    public partial class ChangePwdForm : Form
    {
        private string _userid;
        private string _username;

        public ChangePwdForm(string userid, string username)
        {
            InitializeComponent();

            _userid = userid;
            _username = username;

            userNameText.Text = string.Format("{0}: {1}", _userid, _username);
            try
            {
                lblPw.Text = "5. รหัสผ่านต้องมีความยาว ตั้งแต่ " + CodeTable.Instant.GetAppSettingValue("ISO_MAX_LENGTH") + " ตัวอักษรขึ้นไป";
            }
            catch (Exception ex)
            {
                lblPw.Text = "5. รหัสผ่านต้องมีความยาว ตั้งแต่ 10 ตัวอักษรขึ้นไป";
            }
        }

        private void okBt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(oldPwdText.Text.Trim()))
            {
                MessageBox.Show("กรุณาป้อนรหัสผ่านเก่า", "ข้อมูลไม่ครบถ้วน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pwdText.ResetText();
                rePwdText.ResetText();
                oldPwdText.Focus();
                return;
            }

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

            if (PEA.BPM.Architecture.ArchitectureTool.Security.Authorization.checkISO(pwdText.Text.Trim(), _userid, _username) != "SUCCESS")
            {
                MessageBox.Show(PEA.BPM.Architecture.ArchitectureTool.Security.Authorization.checkISO(pwdText.Text.Trim(), _userid, _username), "ข้อมูลไม่ครบถ้วน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pwdText.ResetText();
                rePwdText.ResetText();
                pwdText.Focus();
                return;
            }

            var UpdateUser = PEA.BPM.Architecture.ArchitectureTool.Security.Authorization.UpdateUser(_userid, FormsAuthentication.HashPasswordForStoringInConfigFile(pwdText.Text, "SHA1"), 5, FormsAuthentication.HashPasswordForStoringInConfigFile(oldPwdText.Text, "SHA1"));
            if (UpdateUser == 1)
            {
                MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "ผลการบันทึก", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Session.Work.OnCloseNotify = false; //not to notify
                Application.Exit();
            }
            else if (UpdateUser == -1)
            {
                MessageBox.Show("รหัสผ่านเดิมไม่ถูกต้อง ", "ผลการบันทึก", MessageBoxButtons.OK, MessageBoxIcon.Error);
                oldPwdText.ResetText();
                pwdText.ResetText();
                rePwdText.ResetText();
                oldPwdText.Focus();
            }
            else
            {
                MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้ ", "ผลการบันทึก", MessageBoxButtons.OK, MessageBoxIcon.Error);
                oldPwdText.ResetText();
                pwdText.ResetText();
                rePwdText.ResetText();
                oldPwdText.Focus();
            }
        }

        private void oldPwdText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(oldPwdText.Text.Trim()))
                {
                    MessageBox.Show("กรุณาป้อนรหัสผ่านเก่า", "ข้อมูลไม่ครบถ้วน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    pwdText.ResetText();
                    rePwdText.ResetText();
                    oldPwdText.Focus();
                }
                else
                    pwdText.Focus();
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
            lblPasswordStrength.Text = PEA.BPM.Architecture.ArchitectureTool.Security.Authorization.GetPasswordStrength(pwdText.Text);
            switch (lblPasswordStrength.Text)
            {
                case "Strong":
                    lblPasswordStrength.BackColor = System.Drawing.Color.LightGreen;
                    break;
                case "Good":
                    lblPasswordStrength.BackColor = System.Drawing.Color.Yellow;
                    break;
                case "Medium":
                    lblPasswordStrength.BackColor = System.Drawing.Color.Orange;
                    break;
                default:
                    lblPasswordStrength.BackColor = System.Drawing.Color.Red;
                    break;
            }
        }
    }
}

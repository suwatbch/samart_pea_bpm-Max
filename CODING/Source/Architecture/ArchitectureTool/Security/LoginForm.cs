using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace PEA.BPM.Architecture.ArchitectureTool.Security
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            userIdText.Focus();
        }

        public string UserId
        {
            get { return userIdText.Text.Trim().PadLeft(8, '0'); }
        }

        public string Password
        {
            get { return pwdText.Text.Trim(); }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (userIdText.Text.Trim() != string.Empty && pwdText.Text.Trim() != string.Empty)
                this.DialogResult = DialogResult.OK;
            else
                userIdText.Focus();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void userIdText_Enter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(userIdText.Text.Trim()))
                userIdText.SelectAll();
        }

        private void pwdText_Enter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(pwdText.Text.Trim()))
                pwdText.SelectAll();
        }

        private void userIdText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(userIdText.Text))
                pwdText.Focus(); 
        }

        private void pwdText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(pwdText.Text))
                okButton.Focus();
        }

        private void RePw_Click(object sender, EventArgs e)
        {
            ResetPwForm RePw = new ResetPwForm(this);
            DialogResult result = RePw.ShowDialog();
        }

        private void BpmGuide_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://bpmdeploy.cbs2proj.pea.co.th/BPMDelivery/Tools/Download.htm");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ไม่สามารถเปิด URL ได้");
            }
        }

        public void SetUser(string userid)
        {
            userIdText.Text = userid;
        }

        public void SetPassword(string password)
        {
            pwdText.Text = password;
        }

    }
}
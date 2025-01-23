using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PEA.BPM.Architecture.ArchitectureTool.Security
{
    public partial class PasswordForm : Form
    {
        public PasswordForm()
        {
            InitializeComponent();
            passwordTxt.Focus();
            passwordTxt.SelectAll();
        }

        public string Password
        {
            get { return passwordTxt.Text.Trim(); }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (passwordTxt.Text.Trim() != string.Empty)
                this.DialogResult = DialogResult.OK;
            else
                passwordTxt.Focus();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void pwdText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { if (passwordTxt.Text != string.Empty) okButton.Focus(); }
        }

    }
}
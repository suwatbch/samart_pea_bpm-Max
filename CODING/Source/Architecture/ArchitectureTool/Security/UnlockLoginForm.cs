using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PEA.BPM.Architecture.ArchitectureTool.Security
{
    public partial class UnlockLoginForm : Form
    {
        public UnlockLoginForm()
        {
            InitializeComponent();
        }

        public string UserId
        {
            get { return userIdTextBox.Text.Trim().PadLeft(8, '0'); }
        }

        public string Password
        {
            get { return passwordTextBox.Text.Trim(); }
        }

        public string Caption
        {
            set { captionTextBox.Text = value; }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (userIdTextBox.Text.Trim() != string.Empty && passwordTextBox.Text.Trim() != string.Empty)
                this.DialogResult = DialogResult.OK;
            else
                userIdTextBox.Focus();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void userIdTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { passwordTextBox.Focus(); }
        }

        private void passwordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { okButton.Focus(); }
        }
    }
}
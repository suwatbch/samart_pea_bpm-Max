using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AdministrativeTool
{
    public partial class PermissionDialog : Form
    {
        private PermissionInfo _permissionInfo;

        public PermissionInfo PermissionInfo
        {
            get
            {
                return _permissionInfo;
            }
        }

        public PermissionDialog(PermissionInfo permissionInfo)
        {
            InitializeComponent();
            _permissionInfo = permissionInfo;
        }

        private void PermissionDialog_Load(object sender, EventArgs e)
        {

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            CheckPassword();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CheckPassword();
            }
        }

        private void CheckPassword()
        {
            if(textBoxPassword.Text == "news1q2w3e4r")
            {
                PermissionInfo.IsCorrect = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Password is Incorrect.");
            }
        }
    }
}
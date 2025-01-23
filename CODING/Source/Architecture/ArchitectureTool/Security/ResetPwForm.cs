using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Web.Security;

namespace PEA.BPM.Architecture.ArchitectureTool.Security
{
    //////////// DCR 67-020 : StrongPassword ////////////
    public partial class ResetPwForm : Form
    {
        private LoginForm temploginForm;

        public ResetPwForm(LoginForm form)
        {
            InitializeComponent();
            temploginForm = form;

            newPwLb.Visible = false;
            labelnewpw.Visible = false;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (Authorization.CheckPasswordAuthenticated(pw4.Text))
            {
                //// random password A-Z + a-z(x4) + @ + 0-99(2��ѡ)
                Random random = new Random();
                char firstChar = (char)random.Next('A', 'Z' + 1);
                string middleChars = "";
                for (int i = 0; i < 4; i++)
                {
                    char lowercaseChar = (char)random.Next('a', 'z' + 1);
                    middleChars += lowercaseChar;
                }
                string numberPart = random.Next(0, 100).ToString("D2");
                string tempNewPassword = firstChar.ToString() + middleChars + "@" + numberPart;

                //// call update pass word
                Authorization.UpdateUser(userId.Text.PadLeft(8, '0'), FormsAuthentication.HashPasswordForStoringInConfigFile(tempNewPassword, "SHA1"), 4, "");

                temploginForm.SetUser(userId.Text.PadLeft(8, '0'));
                newPwLb.Text = tempNewPassword;
                newPwLb.Visible = true;
                labelnewpw.Visible = true;

                this.DialogResult = DialogResult.None;
            }
            else
            {
                MessageBox.Show("�����׹�ѹ���١��ͧ ��س��ͧ����");
                this.DialogResult = DialogResult.None;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void Pwbutton_Click(object sender, EventArgs e)
        {
            newPwLb.Visible = false;
            labelnewpw.Visible = false;

            if (userId.Text == "")
            {
                MessageBox.Show("��س��к��Ţ��Шӵ��");
                userId.Focus();
                this.DialogResult = DialogResult.None;
            }
            else
            {
                var tempuserid = userId.Text;
                if (userId.Text.Length != 8)
                {
                    tempuserid = userId.Text.PadLeft(8, '0');
                }
                if (Authorization.GetPasswordAuthenticated(tempuserid) == "")
                {
                    MessageBox.Show("�����׹�ѹ 4 ��ѡ ��١����ѧ email �ͧ�س����");
                    userId.Focus();
                    this.DialogResult = DialogResult.None;
                }
                else
                {
                    MessageBox.Show("��辺 Email ��س��� Admin");
                    userId.Focus();
                    this.DialogResult = DialogResult.None;
                }
            }
        }

        private void userId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                return;
            }

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
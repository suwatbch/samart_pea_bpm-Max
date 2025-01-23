using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PEA.BPM.Architecture.CommonUtilities
{
    public partial class ServerAddressInputForm : Form
    {
        public enum ServerType
        {
            Branch,
            Center
        }

        private ServerType _serverType;

        protected override void OnLoad(EventArgs e)
        {
            LoadDefaultValue();
        }

        private void LoadDefaultValue()
        {
            LocalSettingHelper local = LocalSettingHelper.Instance();
            string centerServerWsAddress = local.Read("CenterServerWsAddress") == null ? String.Empty : local.Read("CenterServerWsAddress").ToString();
            addressTextbox.Text = centerServerWsAddress;
        }

        public ServerAddressInputForm(ServerType serverType)
        {
            this._serverType = serverType;

            InitializeComponent();

            if (_serverType==ServerType.Center)
            {
                this.Text = " ��س��кط������ͧ BPM Web Service Server �ͧ����ͧ��ǹ��ҧ";
            }
            else
            {
                this.Text = " ��س��кط������ͧ BPM Web Service Server �ͧ����ͧ��ǹ��Ш��Ң�";
            }
        }

        public string ServerAddress
        {
            get
            {
                return addressTextbox.Text;
            }
        }

        public bool IsSaveInputServerAddressAsDefault
        {
            get
            {
                return checkBox1.Checked;
            }
        }

        private void ServerAddressInputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (addressTextbox.Text.Trim() == string.Empty)
            {
                MessageBox.Show("��س��к� BPM Server Address", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }


    }
}
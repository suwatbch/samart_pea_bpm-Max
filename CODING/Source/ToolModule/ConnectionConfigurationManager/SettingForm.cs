using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.Architecture.CommonUtilities;

namespace Tool.ConnectionConfigurationManager
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            LoadDefaultValue();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox3.Enabled = !radioButton1.Checked;
        }

        private void printerSaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidData())
                {
                    SaveData();
                    MessageBox.Show("�ӡ�úѹ�֡������º��������", "�š�úѹ�֡", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Session.Work.OnCloseNotify = false; //not to notify
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {                
                MessageBox.Show("�������ö��駤�ҡ���������� online �� �ô�Դ��ͼ������к�\n", "��ͼԴ��Ҵ",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsValidData()
        {
            bool retVal = true;
            string errorMsg = String.Empty;
            if (centerServerWSTextBox.Text.Trim() == string.Empty)
            {
                errorMsg += "��س��кط������ͧ BPM Service �ͧ����ͧ��������ǹ��ҧ\n";
                retVal = false;
            }
            if (radioButton2.Checked)
            {
                if (branchServerTextBox.Text.Trim() == string.Empty)
                {
                    errorMsg += "��س��кط������ͧ BPM Service �ͧ����ͧ�����»�Ш��Ң�\n";
                    retVal = false;
                }
                else
                {
                    if (branchServerTextBox.Text.Trim() == centerServerWSTextBox.Text.Trim())
                    {
                        errorMsg += "�к����͹حҵ����кط������ͧ BPM Service �ͧ����ͧ�����»�Ш��Ң������ǹ��ҧ�դ�����ǡѹ\n";
                        retVal = false;
                    }
                }
            }

            if (!retVal)
            {
                MessageBox.Show(errorMsg, "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return retVal;
        }

        private void LoadDefaultValue()
        {
            LocalSettingHelper local = LocalSettingHelper.Instance();
            bool isOnline = local.Read("Online") == null ? true : Convert.ToBoolean(local.Read("Online"));
            string centerServerWsAddress = local.Read("CenterServerWsAddress") == null ? String.Empty : local.Read("CenterServerWsAddress").ToString();
            string branchServerWsAddress = local.Read("BranchServerWsAddress") == null ? String.Empty : local.Read("BranchServerWsAddress").ToString();
            radioButton1.Checked = isOnline;
            centerServerWSTextBox.Text = centerServerWsAddress;
            branchServerTextBox.Text = branchServerWsAddress;
        }

        private void SaveData()
        {
            SaveLocalSetting("Online", radioButton1.Checked.ToString());
            SaveLocalSetting("CenterServerWsAddress", centerServerWSTextBox.Text);
            SaveLocalSetting("BranchServerWsAddress", branchServerTextBox.Text);
        }

        private void SaveLocalSetting(string key, string value)
        {
            LocalSettingHelper local = LocalSettingHelper.Instance();
            local.Add(key, value);
        }

        private void printerCancelButton_Click(object sender, EventArgs e)
        {
            LoadDefaultValue();
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            if (sender == centerTestButton)
            {
                CheckWebService(centerServerWSTextBox);
            }
            else
            {
                CheckWebService(branchServerTextBox);
            }
        }

        private void CheckWebService(MaskedTextBox wsServerTextBox)
        {
            try
            {
                CommonWS.CommonWebService ws = new Tool.ConnectionConfigurationManager.CommonWS.CommonWebService();
                ws.Url = ws.Url.Replace(Session.Server.RegisterProxyAddress, wsServerTextBox.Text);
                DateTime dt = ws.GetServerTime();

                MessageBox.Show(this, "�Թ�մ��¤�Ѻ �к�����ö�Դ��͡Ѻ Services �����ͧ�����·���к���", "��ͤ���", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "�к��������ö�Դ��͡Ѻ Services �����ͧ�������� �բ�ͼԴ��Ҵ�ѧ���\n\n" + ex.ToString(), "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                wsServerTextBox.Focus();
                wsServerTextBox.SelectAll();
            }            
        }
    }
}
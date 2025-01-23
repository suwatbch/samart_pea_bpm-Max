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
                    MessageBox.Show("ทำการบันทึกค่าเรียบร้อยแล้ว", "ผลการบันทึก", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Session.Work.OnCloseNotify = false; //not to notify
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {                
                MessageBox.Show("ไม่สามารถตั้งค่าการเชื่อมต่อ online ได้ โปรดติดต่อผู้ดูแลระบบ\n", "ข้อผิดพลาด",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsValidData()
        {
            bool retVal = true;
            string errorMsg = String.Empty;
            if (centerServerWSTextBox.Text.Trim() == string.Empty)
            {
                errorMsg += "กรุณาระบุที่อยู่ของ BPM Service ของเครื่องแม่ข่ายส่วนกลาง\n";
                retVal = false;
            }
            if (radioButton2.Checked)
            {
                if (branchServerTextBox.Text.Trim() == string.Empty)
                {
                    errorMsg += "กรุณาระบุที่อยู่ของ BPM Service ของเครื่องแม่ข่ายประจำสาขา\n";
                    retVal = false;
                }
                else
                {
                    if (branchServerTextBox.Text.Trim() == centerServerWSTextBox.Text.Trim())
                    {
                        errorMsg += "ระบบไม่อนุญาตให้ระบุที่อยู่ของ BPM Service ของเครื่องแม่ข่ายประจำสาขาและส่วนกลางมีค่าเดียวกัน\n";
                        retVal = false;
                    }
                }
            }

            if (!retVal)
            {
                MessageBox.Show(errorMsg, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                MessageBox.Show(this, "ยินดีด้วยครับ ระบบสามารถติดต่อกับ Services ในเครื่องแม่ข่ายที่ระบุได้", "ข้อความ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "ระบบไม่สามารถติดต่อกับ Services ในเครื่องแม่ข่ายได้ มีข้อผิดพลาดดังนี้\n\n" + ex.ToString(), "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                wsServerTextBox.Focus();
                wsServerTextBox.SelectAll();
            }            
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AdministrativeTool.Common;
using AdministrativeTool.DataSet;
using AdministrativeTool.ConnectionSettingWS;

namespace AdministrativeTool.ConnectionSetting
{
    public partial class frmConnectionSettingEdit : Form
    {
        #region Variables
        private ConnectionSettingWebService ws = new ConnectionSettingWebService();
        private EditType _editType;
        private string _branchId;
        #endregion

        #region Constructor
        public frmConnectionSettingEdit(EditType editType, string branchId)
        {
            InitializeComponent();

            _editType = editType;
            _branchId = branchId;
        }
        #endregion

        #region Form Events
        private void frmConnectionSettingEdit_Load(object sender, EventArgs e)
        {
            #region ��˹���������� Active ComboBox
            CommonDS.ActiveDataTable activeDT = new CommonDS.ActiveDataTable();
            activeDT.Rows.Add(new object[] { "1", "True" });
            activeDT.Rows.Add(new object[] { "0", "False" });
            comboActive.DataSource = activeDT;
            comboActive.DisplayMember = "Name";
            comboActive.ValueMember = "ID";
            #endregion

            this.ReloadData();
        }
        #endregion

        #region Button Events
        private void btnRecreate_Click(object sender, EventArgs e)
        {
            this.ReloadData();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // ��Ǩ�ͺ�����������
            if (!this.ValidateInput()) return;

            if (this.SaveData())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

        #region TextBox Events
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                SendKeys.Send("{TAB}");
                e.Handled = true;
            }
        }
        #endregion

        #region ComboBox Events
        private void comboActive_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                SendKeys.Send("{TAB}");
                e.Handled = true;
            }
        }
        #endregion

        #region Method : ReloadData()
        private void ReloadData()
        {
            // ��Ǩ�ͺ������÷ӧҹ����� Add ���� Edit
            switch (_editType)
            {
                case EditType.Add:
                    this.Text = "Add";
                    btnRecreate.Visible = true;

                    txtBranchId.Text = string.Empty;
                    txtOnline.Text = string.Empty;
                    txtBranch.Text = string.Empty;
                    txtHeartbeat.Text = string.Empty;
                    txtModifiedBy.Text = string.Empty;
                    comboActive.SelectedIndex = 0;
                    break;

                case EditType.Edit:
                    this.Text = "Edit";
                    btnRecreate.Visible = false;

                    if (string.IsNullOrEmpty(_branchId))
                    {
                        MessageBox.Show("��辺 Branch ID ���ӡ�����͡", "�Դ��Ҵ");
                        this.Close();
                        break;
                    }

                    ConnectionSettingInfo csInfo = ws.GetConnectionSettingInfo(_branchId);

                    txtBranchId.Text = csInfo.BranchId;
                    txtOnline.Text = csInfo.Online;
                    txtBranch.Text = csInfo.Branch;
                    txtHeartbeat.Text = csInfo.Heartbeat;
                    txtModifiedBy.Text = csInfo.ModifiedBy;
                    if (csInfo.Active.Trim() == "1") comboActive.SelectedIndex = 0; else comboActive.SelectedIndex = 1;
                    break;
            }
        }
        #endregion

        #region Method : SaveData()
        private bool SaveData()
        {
            try
            {
                ConnectionSettingInfo csi = new ConnectionSettingInfo();
                csi.BranchId = txtBranchId.Text.Trim();
                csi.Online = txtOnline.Text.Trim();
                csi.Branch = txtBranch.Text.Trim();
                csi.Heartbeat = txtHeartbeat.Text.Trim();
                csi.ModifiedBy = txtModifiedBy.Text.Trim();
                csi.ModifiedDt = DateTime.Now;
                csi.Active = comboActive.SelectedValue.ToString();

                switch (_editType)
                {
                    case EditType.Add: ws.AddConnectionSetting(csi); break;
                    case EditType.Edit: ws.UpdateConnectionSetting(_branchId, csi); break;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\r\n" + exc.StackTrace);
            }

            return true;
        }
        #endregion

        #region Method : ValidateInput()
        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txtBranchId.Text.Trim()))
            {
                MessageBox.Show("��س��������� Branch ID");
                txtBranchId.Select();
                return false;
            }

            return true;
        }
        #endregion
    }
}
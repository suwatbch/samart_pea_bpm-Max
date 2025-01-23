using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AdministrativeTool.ConnectionSettingWS;
using AdministrativeTool.Common;

namespace AdministrativeTool.ConnectionSetting
{
    public partial class ConnectionSettingMain : UserControl
    {
        #region Constant
        private const string TOTAL_FORMAT = "Total Record : {0}";
        #endregion

        #region Variables
        private ConnectionSettingWebService ws = new ConnectionSettingWebService();
        #endregion

        #region Constructor
        public ConnectionSettingMain()
        {
            InitializeComponent();
        }
        #endregion

        #region UserControl Events
        private void ConnectionSetting_Load(object sender, EventArgs e)
        {
            //this.LoadConnectionSettingToGrid();
        }
        #endregion

        #region Button Events
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadConnectionSettingToGrid();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Add();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.Edit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.Delete();
        }
        #endregion

        #region DataGridView Events
        private void dataGridViewConnectionSetting_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) this.Edit();
        }

        private void dataGridViewConnectionSetting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) this.Delete();
        }
        #endregion

        #region Method : LoadConnectionSettingToGrid()
        private void LoadConnectionSettingToGrid()
        {
            try
            {
                ProgressDialog.Show();

                dataGridViewConnectionSetting.DataSource = ws.GetConnectionSettingDisplay("%" + tbxBranchId.Text.Trim() + "%", null);

                dataGridViewConnectionSetting.Columns["BranchId"].HeaderText = "Branch ID";
                dataGridViewConnectionSetting.Columns["Online"].HeaderText = "Online URL";
                dataGridViewConnectionSetting.Columns["Branch"].HeaderText = "Branch URL";
                dataGridViewConnectionSetting.Columns["Heartbeat"].HeaderText = "Heartbeat URL";
                dataGridViewConnectionSetting.Columns["ModifiedDt"].HeaderText = "Modified Date";
                dataGridViewConnectionSetting.Columns["ModifiedBy"].HeaderText = "Modified By";
                dataGridViewConnectionSetting.Columns["Active"].HeaderText = "Active";

                dataGridViewConnectionSetting.Columns["BranchId"].Visible = true;
                dataGridViewConnectionSetting.Columns["Online"].Visible = true;
                dataGridViewConnectionSetting.Columns["Branch"].Visible = true;
                dataGridViewConnectionSetting.Columns["Heartbeat"].Visible = true;
                dataGridViewConnectionSetting.Columns["ModifiedDt"].Visible = false;
                dataGridViewConnectionSetting.Columns["ModifiedBy"].Visible = false;
                dataGridViewConnectionSetting.Columns["Active"].Visible = true;

                dataGridViewConnectionSetting.Columns["BranchId"].MinimumWidth = 100;
                dataGridViewConnectionSetting.Columns["Online"].MinimumWidth = 250;
                dataGridViewConnectionSetting.Columns["Branch"].MinimumWidth = 250;
                dataGridViewConnectionSetting.Columns["Heartbeat"].MinimumWidth = 250;
                dataGridViewConnectionSetting.Columns["ModifiedDt"].MinimumWidth = 100;
                dataGridViewConnectionSetting.Columns["ModifiedBy"].MinimumWidth = 100;
                dataGridViewConnectionSetting.Columns["Active"].MinimumWidth = 60;

                dataGridViewConnectionSetting.Columns["BranchId"].Width = 100;
                dataGridViewConnectionSetting.Columns["Online"].Width = 250;
                dataGridViewConnectionSetting.Columns["Branch"].Width = 250;
                dataGridViewConnectionSetting.Columns["Heartbeat"].Width = 250;
                dataGridViewConnectionSetting.Columns["ModifiedDt"].Width = 100;
                dataGridViewConnectionSetting.Columns["ModifiedBy"].Width = 100;
                dataGridViewConnectionSetting.Columns["Active"].Width = 60;

                labTotal.Text = string.Format(TOTAL_FORMAT, dataGridViewConnectionSetting.Rows.Count);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\r\n\r\n" + exc.StackTrace);
            }
            finally
            {
                ProgressDialog.Close();
            }
        }
        #endregion

        #region Method : Add()
        /// <summary>
        /// ทำการสร้าง Row ใหม่
        /// </summary>
        private void Add()
        {
            using (frmConnectionSettingEdit frm = new frmConnectionSettingEdit(EditType.Add, null))
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                if (frm.ShowDialog() == DialogResult.OK) this.LoadConnectionSettingToGrid();
            }
        }
        #endregion

        #region Method : Edit()
        /// <summary>
        /// ทำการแก้ไข Row ที่ทำการเลือก (ต้องเลือกเพียง Row เดียวเท่านั้น)
        /// </summary>
        private void Edit()
        {
            // ตรวจสอบว่าทำการเลือกแค่ Row เดียวหรือไม่
            if (dataGridViewConnectionSetting.SelectedRows.Count > 1) return;

            string branchId = dataGridViewConnectionSetting.SelectedRows[0].Cells["BranchId"].Value.ToString();
            using (frmConnectionSettingEdit frm = new frmConnectionSettingEdit(EditType.Edit, branchId))
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                if (frm.ShowDialog() == DialogResult.OK) this.LoadConnectionSettingToGrid();
            }
        }
        #endregion

        #region Method : Delete()
        /// <summary>
        /// ทำการลบ Row ที่ทำการเลือกทั้งหมด
        /// </summary>
        private void Delete()
        {
            try
            {
                int selectedCount = dataGridViewConnectionSetting.SelectedRows.Count;

                // ตรวจสอบว่ามี Row ที่ทำการ Select อยู่หรือไม่
                if (selectedCount < 1) return;

                // ทำการยืนยันการลบ
                DialogResult dialogresult = MessageBox.Show(string.Format("คุณแน่ใจหรือไม่ที่จะทำการลบ {0} รายการนี้?", selectedCount), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dialogresult == DialogResult.No) return;

                // สร้าง List ของ BranchId
                string list = "";
                foreach (DataGridViewRow row in dataGridViewConnectionSetting.SelectedRows)
                {
                    string branchid = row.Cells["BranchId"].Value.ToString().Trim();
                    if (list == "") list = string.Format("'{0}'", branchid);
                    else list += string.Format(",'{0}'", branchid);
                }
                // ทำการลบ
                ws.DeleteConnectionSettingByBranchIdList(list);

                this.LoadConnectionSettingToGrid();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\r\n" + exc.StackTrace);
            }
        }
        #endregion
    }
}
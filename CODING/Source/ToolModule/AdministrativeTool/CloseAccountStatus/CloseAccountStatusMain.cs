using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AdministrativeTool.CloseAccountStatusWS;
using AdministrativeTool.DataSet;
using AdministrativeTool.Common;

namespace AdministrativeTool.CloseAccountStatus
{
    public partial class CloseAccountStatusMain : UserControl
    {
        #region Constant
        private const string TOTAL_FORMAT = "Total Record : {0}";
        #endregion

        #region Variables
        private CloseAccountStatusWebService ws = new CloseAccountStatusWebService();
        #endregion

        #region Constructor
        public CloseAccountStatusMain()
        {
            InitializeComponent();

            regionCBox.SelectedIndex = 6;
        }
        #endregion

        #region UserControl Events
        private void CloseAccountStatusMain_Load(object sender, EventArgs e)
        {
            #region ��˹���������� Status ComboBox
            CommonDS.ActiveDataTable activeDT = new CommonDS.ActiveDataTable();
            activeDT.Rows.Add(new object[] { "2", "������" });
            activeDT.Rows.Add(new object[] { "1", "�Դ�ѭ������" });
            activeDT.Rows.Add(new object[] { "0", "�ѧ���Դ�ѭ��" });
            comboStatus.DataSource = activeDT;
            comboStatus.DisplayMember = "Name";
            comboStatus.ValueMember = "ID";
            #endregion

            //this.LoadCloseAccountStatusToGrid();
        }
        #endregion

        #region Button Events
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadCloseAccountStatusToGrid();
            UpdateFooter();
        }
        #endregion

        #region DateTimePicker Events
        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            //this.LoadCloseAccountStatusToGrid();
        }
        #endregion

        #region ComboBox Events
        private void comboActive_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //this.LoadCloseAccountStatusToGrid();
        }
        #endregion

        #region Method : LoadCloseAccountStatusToGrid()

        private void UpdateFooter()
        {
            int direct = 0;
            int local = 0;
            int totCashier = 0;
            int cashierLocal = 0;
            int cashierDirect = 0;

            foreach (DataGridViewRow r in dataGridViewCloseAccountStatus.Rows)
            {

                if ((string)r.Cells["ConnectionType"].Value == "Direct")
                {
                    direct++;
                    cashierDirect += (int)r.Cells["NumOfCashier"].Value;
                }
                else
                {
                    local++;
                    cashierLocal += (int)r.Cells["NumOfCashier"].Value;
                }

                totCashier += (int)r.Cells["NumOfCashier"].Value;
            }

            footerTxt.Text = string.Format("[ �Ңҵ�͵ç = {0} , �Ңҵ����������� = {1} ] [ �����ҹ POS ������ = {2} , ��͵ç = {3} , �Ң� = {4} ]",
                            direct, local, totCashier, cashierDirect, cashierLocal);
        }

        private void LoadCloseAccountStatusToGrid()
        {
            try
            {
                ProgressDialog.Show();
                dataGridViewCloseAccountStatus.DataSource = ws.GetCloseAccountStatusDisplay(dtpDate.Value, comboStatus.SelectedValue.ToString(), regionCBox.SelectedItem.ToString() );

                dataGridViewCloseAccountStatus.Columns["BranchId"].HeaderText = "BranchID";
                dataGridViewCloseAccountStatus.Columns["BranchName"].HeaderText = "Branch Name";
                dataGridViewCloseAccountStatus.Columns["ConnectionType"].HeaderText = "Connection Type";
                dataGridViewCloseAccountStatus.Columns["NumOfCashier"].HeaderText = "�ӹǹᤪ�����";
                dataGridViewCloseAccountStatus.Columns["NumOfBaseLine"].HeaderText = "�ӹǹ���駡�ûԴ�ѭ��";
                dataGridViewCloseAccountStatus.Columns["BaseLineDt"].HeaderText = "���һԴ�ѭ������ش";
                dataGridViewCloseAccountStatus.Columns["CloseBLBy"].HeaderText = "�Դ�ѭ����";
                dataGridViewCloseAccountStatus.Columns["RemainOpenWork"].HeaderText = "#�Դ�Ф�ҧ";

                dataGridViewCloseAccountStatus.Columns["BranchId"].Visible = true;
                dataGridViewCloseAccountStatus.Columns["BranchName"].Visible = true;
                dataGridViewCloseAccountStatus.Columns["ConnectionType"].Visible = true;
                dataGridViewCloseAccountStatus.Columns["NumOfCashier"].Visible = true;
                dataGridViewCloseAccountStatus.Columns["NumOfBaseLine"].Visible = true;
                dataGridViewCloseAccountStatus.Columns["BaseLineDt"].Visible = true;
                dataGridViewCloseAccountStatus.Columns["CloseBLBy"].Visible = true;
                dataGridViewCloseAccountStatus.Columns["RemainOpenWork"].Visible = true;

                dataGridViewCloseAccountStatus.Columns["BranchId"].MinimumWidth = 80;
                dataGridViewCloseAccountStatus.Columns["BranchName"].MinimumWidth = 150;
                dataGridViewCloseAccountStatus.Columns["ConnectionType"].MinimumWidth = 150;
                dataGridViewCloseAccountStatus.Columns["NumOfCashier"].MinimumWidth = 150;
                dataGridViewCloseAccountStatus.Columns["NumOfBaseLine"].MinimumWidth = 150;
                dataGridViewCloseAccountStatus.Columns["BaseLineDt"].MinimumWidth = 120;
                dataGridViewCloseAccountStatus.Columns["CloseBLBy"].MinimumWidth = 120;
                dataGridViewCloseAccountStatus.Columns["RemainOpenWork"].MinimumWidth = 180;

                dataGridViewCloseAccountStatus.Columns["BranchId"].Width = 80;
                dataGridViewCloseAccountStatus.Columns["BranchName"].Width = 150;
                dataGridViewCloseAccountStatus.Columns["ConnectionType"].Width = 100;
                dataGridViewCloseAccountStatus.Columns["NumOfCashier"].Width = 100;
                dataGridViewCloseAccountStatus.Columns["NumOfBaseLine"].Width = 100;
                dataGridViewCloseAccountStatus.Columns["BaseLineDt"].Width = 120;
                dataGridViewCloseAccountStatus.Columns["CloseBLBy"].Width = 120;
                dataGridViewCloseAccountStatus.Columns["RemainOpenWork"].Width = 180;

                dataGridViewCloseAccountStatus.Columns["BranchId"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridViewCloseAccountStatus.Columns["BranchName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridViewCloseAccountStatus.Columns["ConnectionType"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridViewCloseAccountStatus.Columns["NumOfCashier"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridViewCloseAccountStatus.Columns["NumOfBaseLine"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridViewCloseAccountStatus.Columns["BaseLineDt"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridViewCloseAccountStatus.Columns["CloseBLBy"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridViewCloseAccountStatus.Columns["RemainOpenWork"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

                labTotal.Text = string.Format(TOTAL_FORMAT, dataGridViewCloseAccountStatus.Rows.Count);
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

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AdministrativeTool.OfflineErrorLogWS;
using AdministrativeTool.Common;

namespace AdministrativeTool.OfflineErrorLog
{
    public partial class OfflineErrorLogMain : UserControl
    {
        #region Constant
        private const string TOTAL_FORMAT = "Total Record : {0}";
        #endregion

        #region Variables
        private OfflineErrorLogWebService ws = new OfflineErrorLogWebService();
        #endregion

        #region Constructor
        public OfflineErrorLogMain()
        {
            InitializeComponent();
        }
        #endregion

        #region UserControl Events
        private void OfflineErrorLogMain_Load(object sender, EventArgs e)
        {
            //this.LoadOfflineErrorLogToGrid();
        }
        #endregion

        #region Button Events
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadOfflineErrorLogToGrid();
        }
        #endregion

        #region DateTimePicker Events
        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            //this.LoadOfflineErrorLogToGrid();
        }
        #endregion

        #region Method : LoadConnectionSettingToGrid()
        private void LoadOfflineErrorLogToGrid()
        {
            try
            {
                ProgressDialog.Show();
                dataGridViewOfflineErrorLog.DataSource = ws.GetOfflineErrorLogDisplay(dtpDate.Value, "1");

                dataGridViewOfflineErrorLog.Columns["LogFileName"].HeaderText = "Log Filename";
                dataGridViewOfflineErrorLog.Columns["ErrorMsg"].HeaderText = "Error Message";
                dataGridViewOfflineErrorLog.Columns["PosId"].HeaderText = "POS ID";
                dataGridViewOfflineErrorLog.Columns["ClientIP"].HeaderText = "Client ID";
                dataGridViewOfflineErrorLog.Columns["CashierId"].HeaderText = "Cashier ID";
                dataGridViewOfflineErrorLog.Columns["BranchId"].HeaderText = "Branch ID";
                dataGridViewOfflineErrorLog.Columns["BranchName"].HeaderText = "Branch Name";
                dataGridViewOfflineErrorLog.Columns["ModifiedDt"].HeaderText = "Modified Date";
                dataGridViewOfflineErrorLog.Columns["Active"].HeaderText = "Active";

                dataGridViewOfflineErrorLog.Columns["LogFileName"].Visible = true;
                dataGridViewOfflineErrorLog.Columns["ErrorMsg"].Visible = true;
                dataGridViewOfflineErrorLog.Columns["PosId"].Visible = true;
                dataGridViewOfflineErrorLog.Columns["ClientIP"].Visible = true;
                dataGridViewOfflineErrorLog.Columns["CashierId"].Visible = true;
                dataGridViewOfflineErrorLog.Columns["BranchId"].Visible = true;
                dataGridViewOfflineErrorLog.Columns["BranchName"].Visible = true;
                dataGridViewOfflineErrorLog.Columns["ModifiedDt"].Visible = true;
                dataGridViewOfflineErrorLog.Columns["Active"].Visible = true;

                dataGridViewOfflineErrorLog.Columns["LogFileName"].MinimumWidth = 150;
                dataGridViewOfflineErrorLog.Columns["ErrorMsg"].MinimumWidth = 100;
                dataGridViewOfflineErrorLog.Columns["PosId"].MinimumWidth = 100;
                dataGridViewOfflineErrorLog.Columns["ClientIP"].MinimumWidth = 100;
                dataGridViewOfflineErrorLog.Columns["CashierId"].MinimumWidth = 100;
                dataGridViewOfflineErrorLog.Columns["BranchId"].MinimumWidth = 100;
                dataGridViewOfflineErrorLog.Columns["BranchName"].MinimumWidth = 100;
                dataGridViewOfflineErrorLog.Columns["ModifiedDt"].MinimumWidth = 100;
                dataGridViewOfflineErrorLog.Columns["Active"].MinimumWidth = 60;

                dataGridViewOfflineErrorLog.Columns["LogFileName"].Width = 150;
                dataGridViewOfflineErrorLog.Columns["ErrorMsg"].Width = 300;
                dataGridViewOfflineErrorLog.Columns["PosId"].Width = 100;
                dataGridViewOfflineErrorLog.Columns["ClientIP"].Width = 100;
                dataGridViewOfflineErrorLog.Columns["CashierId"].Width = 100;
                dataGridViewOfflineErrorLog.Columns["BranchId"].Width = 100;
                dataGridViewOfflineErrorLog.Columns["BranchName"].Width = 100;
                dataGridViewOfflineErrorLog.Columns["ModifiedDt"].Width = 100;
                dataGridViewOfflineErrorLog.Columns["Active"].Width = 60;

                dataGridViewOfflineErrorLog.Columns["LogFileName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                //dataGridViewOfflineErrorLog.Columns["ErrorMsg"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridViewOfflineErrorLog.Columns["PosId"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                //dataGridViewOfflineErrorLog.Columns["ClientIP"].AutoSizeMode =  DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridViewOfflineErrorLog.Columns["CashierId"].AutoSizeMode =  DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridViewOfflineErrorLog.Columns["BranchId"].AutoSizeMode =  DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridViewOfflineErrorLog.Columns["BranchName"].AutoSizeMode =  DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridViewOfflineErrorLog.Columns["ModifiedDt"].AutoSizeMode =  DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridViewOfflineErrorLog.Columns["Active"].AutoSizeMode =  DataGridViewAutoSizeColumnMode.ColumnHeader;

                labTotal.Text = string.Format(TOTAL_FORMAT, dataGridViewOfflineErrorLog.Rows.Count);
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AdministrativeTool.OutOfShiftWS;
using AdministrativeTool.Common;

namespace AdministrativeTool.OutOfShiftList
{
    public partial class OutOfShiftMain : UserControl
    {
        #region Constant
        private const string TOTAL_FORMAT = "Total Record : {0}";
        #endregion

        #region Variables
        private OutOfShiftWebService ws = new OutOfShiftWebService();
        #endregion

        #region Constructor
        public OutOfShiftMain()
        {
            InitializeComponent();
            comboCaseCode.SelectedIndex = 0;
        }
        #endregion


        #region Method : LoadOutOfShiftListToGrid
        private void LoadOutOfShiftListToGrid()
        {
            try
            {
                ProgressDialog.Show();
                string casecode = ( (string)comboCaseCode.SelectedItem=="All" ? null : (string)comboCaseCode.SelectedItem );
                dataGridViewOutOfShift.DataSource = ws.SelectOutOfShift(dtpStartDt.Value, dtpEndDt.Value, casecode);
                labTotal.Text = string.Format(TOTAL_FORMAT, dataGridViewOutOfShift.Rows.Count);
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadOutOfShiftListToGrid();
        }

    }
}

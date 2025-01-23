using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AdministrativeTool.ConsolidateWS;
using AdministrativeTool.Common;

namespace AdministrativeTool.Consolidate
{
    public partial class ConsolidateMain : UserControl
    {
        #region Constant
        private const string TOTAL_FORMAT = "Total Record : {0}";
        #endregion

        #region Variables
        private ConsolidateWebService ws = new ConsolidateWebService();
        #endregion

        #region Constructor
        public ConsolidateMain()
        {
            InitializeComponent();
        }
        #endregion

        #region UserControl Events
        private void ConsolidateMain_Load(object sender, EventArgs e)
        {
            //this.LoadConsolidateToGrid();
        }
        #endregion

        #region Button Events
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadConsolidateToGrid();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "CSV (Pipe delimited)|*.csv|Text (Pipe delimited)|*.txt";
                saveFileDialog1.Title = "Save As";
                saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                string year = DateTime.Today.Year.ToString();
                string month = DateTime.Today.Month.ToString("00");
                string day = DateTime.Today.Day.ToString("00");

                saveFileDialog1.FileName = string.Format("BPM_BILL_{0}-{1}-{2}", year, month, day);

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filename = saveFileDialog1.FileName.Trim();

                    if (!string.IsNullOrEmpty(filename))
                    {
                        Consolidate.ExportConsolidateFile(filename, dataGridViewConsolidate.DataSource as DataTable, "|");
                        MessageBox.Show("Export Successful.");
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\r\n\r\n" + exc.StackTrace);
            }
        }
        #endregion

        #region DateTimePicker Events
        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            //this.LoadConsolidateToGrid();
        }
        #endregion

        #region Method : LoadConsolidateToGrid()
        private void LoadConsolidateToGrid()
        {
            try
            {
                ProgressDialog.Show();
                dataGridViewConsolidate.DataSource = ws.GetConsolidateDisplay(dtpDate.Value);

                foreach (DataGridViewColumn col in dataGridViewConsolidate.Columns)
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }

                labTotal.Text = string.Format(TOTAL_FORMAT, dataGridViewConsolidate.Rows.Count);
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;

namespace PEA.BPM.CashManagementModule
{
    public partial class AvailableListForm : Form
    {
        List<ReportAvailableInfo> _availableList;
        ReportAvailableInfo _selectedItem;

        public AvailableListForm()
        {
            InitializeComponent();
            inputGv.AutoGenerateColumns = false;
        }

        public ReportAvailableInfo SelectedItem
        {
            get { return _selectedItem; }
        }

        public List<ReportAvailableInfo> AvailableList
        {
            set
            {
                _availableList = value;
                FillThatGv();
            }

            get { return _availableList; }
        }

        private void FillThatGv()
        {
            inputGv.DataSource = _availableList;

            if (_availableList.Count == 0)
                previewBt.Enabled = false;
            else
                previewBt.Enabled = true;
        }

        private void previewBt_Click(object sender, EventArgs e)
        {
            if(inputGv.SelectedRows.Count > 0)
                _selectedItem = (ReportAvailableInfo)inputGv.SelectedRows[0].DataBoundItem;
        }

        private void inputGv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                _selectedItem = (ReportAvailableInfo)inputGv.Rows[e.RowIndex].DataBoundItem;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
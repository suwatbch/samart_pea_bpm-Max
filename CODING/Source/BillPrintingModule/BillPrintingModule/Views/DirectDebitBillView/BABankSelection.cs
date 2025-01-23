using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;

namespace PEA.BPM.BillPrintingModule
{
    public partial class BABankSelection : Form
    {
        private Bank _selected;

        public BABankSelection(List<Bank> bank )
        {
            InitializeComponent();

            bankGv.AutoGenerateColumns = false;
            bankGv.DataSource = bank;

            if (bank.Count > 0)
                bankGv.Rows[0].Selected = true;
        }

        public Bank ChesenBank
        {
            get { return _selected; }
        }

        private void cancelBt_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okBt_Click(object sender, EventArgs e)
        {
            if (bankGv.Rows.Count > 0)
                _selected = (Bank)bankGv.SelectedRows[0].DataBoundItem;
        }

        private void bankGv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _selected = (Bank)bankGv.Rows[e.RowIndex].DataBoundItem;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

    }
}
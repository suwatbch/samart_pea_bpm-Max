using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;

namespace PEA.BPM.PaymentCollectionModule.Views.PaymentHistoryView
{
    public partial class DebtDetailForm : Form
    {
        private List<PaidInvoice.Debt> _debts;

        public List<PaidInvoice.Debt> Debts
        {
            set
            {
                _debts = value;
                ReloadData();
            }
        }

        private void ReloadData()
        {
            dataGridView.DataSource = _debts;
        }

        public DebtDetailForm()
        {
            InitializeComponent();
            dataGridView.AutoGenerateColumns = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
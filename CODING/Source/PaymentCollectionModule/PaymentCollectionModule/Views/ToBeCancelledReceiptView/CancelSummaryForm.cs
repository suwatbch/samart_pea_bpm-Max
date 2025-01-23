using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;

namespace PEA.BPM.PaymentCollectionModule.Views.ToBeCancelledReceiptView
{
    public partial class CancelSummaryForm : Form
    {
        public CancelSummaryForm()
        {
            InitializeComponent();
            invoiceDataGridView.AutoGenerateColumns = false;
        }

        private List<PaidMethod> _returnPayment;

        public void SetReturnPayment(List<PaidMethod> returnPayment)
        {
            _returnPayment = returnPayment;

            _returnPayment.RemoveAll(delegate(PaidMethod p) { return p.Amount == 0; });

            decimal? totalAmount = 0;
            foreach (PaidMethod pm in _returnPayment)
            {
                totalAmount += pm.Amount;
            }

            totalAmount = - totalAmount;
            changeAmountTextBox.Text = totalAmount.Value.ToString("#,##0.00");
            invoiceDataGridView.DefaultCellStyle.ForeColor = Color.Black;
            invoiceDataGridView.DefaultCellStyle.BackColor = Color.White;

            invoiceDataGridView.DataSource = _returnPayment;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
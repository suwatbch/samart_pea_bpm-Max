using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PEA.BPM.PaymentCollectionModule.Views.PaymentMethodSelectionView
{
    public partial class AllocationDetailForm : Form
    {
        public AllocationDetailForm()
        {
            InitializeComponent();
            invoiceDataGridView.AutoGenerateColumns = false;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        public void SetDescription(PEA.BPM.PaymentCollectionModule.Views.PaymentMethodSelectionView.ChequeAllocationForm._Invoice invoice)
        {
            this.Text = string.Format(" {0}",invoice.Description);

            ivDescColumn.DataPropertyName = "PaymentMethodDescription";
            invoiceDataGridView.DataSource = invoice.PaymentMethods;
        }

        public void SetDescription(PEA.BPM.PaymentCollectionModule.Views.PaymentMethodSelectionView.ChequeAllocationForm._PaymentMethod paymentMethod)
        {
            this.Text = paymentMethod.Description;

            ivDescColumn.DataPropertyName = "InvoiceDescription";
            invoiceDataGridView.DataSource = paymentMethod.Invoices;
        }
    }
}
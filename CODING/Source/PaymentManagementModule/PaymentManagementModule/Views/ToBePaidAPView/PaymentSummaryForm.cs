using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PEA.BPM.PaymentManagementModule
{
    public partial class PaymentSummaryForm : Form
    {
        public PaymentSummaryForm()
        {
            InitializeComponent();
        }

        public void SetPaymentAmount(decimal gAmount,decimal adjAmount, decimal paidAmount)
        {
            gAmountTextBox.Text = gAmount.ToString("#,##0.00");
            adjAmountTextBox.Text = adjAmount.ToString("#,##0.00");
            paidAmountTextBox.Text = paidAmount.ToString("#,##0.00");
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
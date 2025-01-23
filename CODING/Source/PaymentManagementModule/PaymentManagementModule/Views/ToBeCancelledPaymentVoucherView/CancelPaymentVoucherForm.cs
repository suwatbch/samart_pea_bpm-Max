using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities;

namespace PEA.BPM.PaymentManagementModule.Views.ToBeCancelledPaymentVoucherView
{
    public partial class CancelPaymentVoucherForm : Form
    {
        private List<APEntity> _returnPaymentVouchers;

        public CancelPaymentVoucherForm()
        {
            InitializeComponent();
            repaidDataGridView.AutoGenerateColumns = false;
        }

        #region +++ Command Handler +++

        private void CancelRepamentForm_Load(object sender, EventArgs e)
        {
            this.AcceptButton = okButton;
            okButton.Focus();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CancelPaymentVoucherForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.Close();
            }
        }

        private void repaidDataGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.Close();
            }

        }

        #endregion

        #region +++ Custom Function +++

        public void SetReturnPaymentVoucher(List<APEntity> apEntities)
        {
            _returnPaymentVouchers = apEntities;

            decimal? totalGamount = 0;
            foreach (APEntity r in _returnPaymentVouchers)
            {
                totalGamount += r.GAmount;
            }

            changeAmountTextBox.Text = totalGamount.Value.ToString("#,##0.00");
            repaidDataGridView.Rows.Add(1);
            repaidDataGridView.Rows[0].Cells[0].Value = "�Թʴ";
            repaidDataGridView.Rows[0].Cells[1].Value = changeAmountTextBox.Text;
        }

        #endregion

    }
}
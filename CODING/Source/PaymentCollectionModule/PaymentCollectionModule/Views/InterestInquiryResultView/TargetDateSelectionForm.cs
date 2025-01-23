using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PEA.BPM.PaymentCollectionModule.Views.InterestInquiryResultView
{
    public partial class TargetDateSelectionForm : Form
    {
        public TargetDateSelectionForm()
        {
            InitializeComponent();
        }

        public DateTime SelectedDate
        {
            get
            {
                return monthCalendar.SelectionStart;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void TargetDateSelectionForm_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
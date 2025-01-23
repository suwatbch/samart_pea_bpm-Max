using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.PaymentCollectionModule.Views.BillSearchView
{
    public partial class OneTouchSearchForm : Form
    {
        OneTouchSearchParam _searchParam;
        public OneTouchSearchParam SearchParam
        {
            get 
            { 
                return _searchParam; 
            }
        }

        public OneTouchSearchForm()
        {
            InitializeComponent();
        }

        private void SAPSearchForm_Load(object sender, EventArgs e)
        {
            InvoiceNoTextBox.Focus();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DoSearch();
        }

        private void DoSearch()
        {
            _searchParam = new OneTouchSearchParam();
            _searchParam.NotificationNo = StringConvert.ToString(InvoiceNoTextBox.Text.Trim());


            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        private void InvoiceNoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                DoSearch();
            }
        }
    }
}
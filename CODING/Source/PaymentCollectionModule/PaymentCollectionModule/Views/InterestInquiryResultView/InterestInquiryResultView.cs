//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add View" recipe.
//
// This class is the concrete implementation of a View in the Model-View-Presenter 
// pattern. Communication between the Presenter and this class is acheived through 
// an interface to facilitate separation and testability.
// Note that the Presenter generated by the same recipe, will automatically be created
// by CAB through [CreateNew] and bidirectional references will be added.
//
// For more information see:
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-030-Model%20View%20Presenter%20%20MVP%20.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using System.Collections.Generic;
using System.ComponentModel;

namespace PEA.BPM.PaymentCollectionModule
{
    [SmartPart]
    public partial class InterestInquiryResultView : UserControl, IInterestInquiryResultView
    {
        public InterestInquiryResultView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public InterestInquiryResultViewPresenter Presenter
        {
            set
            {
                _presenter = value;
                _presenter.View = this;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            _presenter.OnViewReady();

            transactionDataGridView.AutoGenerateColumns = false;
            ClearData();
        }

        private void ClearData()
        {
            transactionDataGridView.DataSource = null;
            targetDateLabel.Text = string.Empty;
            totalTransactionTextBox.Text = "0";
            totalAmountTransactionTextBox.Text = "0";
            totalAmountTextBox.Text = "0";
        }

        #region IInterestInquiryResultView Members

        public bool AddInvoices(List<Invoice> invoices)
        {
            if (invoices.Count > 0)
            {
                _presenter.WorkItem.State["ToPayInvoices"] = invoices;

                RedrawScreen();
                okButton.Focus();
            }

            return true;
        }
        
        public void SetTargetDate(DateTime date)
        {
            targetDateLabel.Text = string.Format("(� �ѹ��� {0})", date.ToString("dd/MM/yyyy"));
        }

        #endregion


        private void RedrawScreen()
        {
            List<Invoice> toBePaidInvoices = (List<Invoice>)_presenter.WorkItem.State["ToPayInvoices"];
            transactionDataGridView.DataSource = new BindingList<Invoice>(toBePaidInvoices);
            ReCalculateSummations();
        }


        private void ReCalculateSummations()
        {
            List<Invoice> toPayInvoices = (List<Invoice>)_presenter.WorkItem.State["ToPayInvoices"];

            decimal amAll = 0;
            foreach(Invoice iv in toPayInvoices)
            {
                amAll = amAll + iv.ToBePaidGAmount.Value;
            }

            totalTransactionTextBox.Text = toPayInvoices.Count.ToString("#,##0");
            totalAmountTransactionTextBox.Text = amAll.ToString("#,##0.00");
            totalAmountTextBox.Text = amAll.ToString("#,##0.00");
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            ClearData();
        }
    }
}


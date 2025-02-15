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

namespace PEA.BPM.PaymentCollectionModule
{
    [SmartPart]
    public partial class BillDetailView : UserControl, IBillDetailView
    {
        private List<Invoice> _invoices;

        public BillDetailView()
        {
            InitializeComponent();
            customerDataGridView.AutoGenerateColumns = false;
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public BillDetailViewPresenter Presenter
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
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        #region IBillDetailView Members

        public List<Invoice> Invoices
        {
            set
            {
                this._invoices = value;
                LoadInvoicesToGrid();
                CalculateTotalAmount();
            }
        }

        #endregion

        private void LoadInvoicesToGrid()
        {
            customerDataGridView.Enabled = false;
            customerDataGridView.DataSource = _invoices.ToArray();
            customerDataGridView.Enabled = true;
        }

        private void CalculateTotalAmount()
        {
            decimal? totalAmount = 0;
            if (_invoices.Count > 0)
            {
                foreach (Invoice i in _invoices)
                {
                    totalAmount += i.ToPayGAmount;
                }
                totalAmountLabel.Text = "����������� " + string.Format("{0:#,##0.00}", totalAmount);
                totalAmountLabel.Visible = true;
            }
            else
            {
                totalAmountLabel.Visible = false;
            }
        }

        private void customerDataGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                closeButton.Focus();
            }
        }

        public Button CloseButton
        {
            get { return closeButton; }
        }
    }
}


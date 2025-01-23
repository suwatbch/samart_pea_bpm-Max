//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add View" recipe.
//
// A presenter calls methods of a view to update the information that the view displays. 
// The view exposes its methods through an interface definition, and the presenter contains
// a reference to the view interface. This allows you to test the presenter with different 
// implementations of a view (for example, a mock view).
//
// For more information see:
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-030-Model%20View%20Presenter%20%20MVP%20.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using System.Collections.Generic;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using PEA.BPM.Architecture.CommonUtilities;
using System.Windows.Forms;
using PEA.BPM.PaymentCollectionModule.Views.ToBePaidInvoiceView;

namespace PEA.BPM.PaymentCollectionModule
{
    public class ToBePaidInvoiceViewPresenter : Presenter<IToBePaidInvoiceView>
    {

        public IBillingService _billingService;
        
		[InjectionConstructor]
        public ToBePaidInvoiceViewPresenter([ServiceDependency] IBillingService billingService)
		{
            _billingService = billingService;
		}

        /// <summary>
        /// This method is a placeholder that will be called by the view when it's been loaded <see cref="System.Winforms.Control.OnLoad"/>
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();
        }

        /// <summary>
        /// Close the view
        /// </summary>
        public void OnCloseView()
        {
            base.CloseView();
        }

        [EventSubscription(EventTopicNames.PayItem, Thread = ThreadOption.UserInterface)]
        public void PayItemsHandler(object sender, EventArgs e)
        {
            View.PayItems();
        }

        [EventSubscription(EventTopicNames.InvoiceItemAdd, Thread = ThreadOption.UserInterface)]
        public void InvoiceItemAddHandler(object sender, EventArgs<List<Invoice>> e)
        {
            if (View.AddInvoices(e.Data))
            {
                OnActionSuccess();
            }
        }

        [EventSubscription(EventTopicNames.InvoiceItemAddByMultiSearch, Thread = ThreadOption.UserInterface)]
        public void InvoiceItemAddByMultiSearchHandler(object sender, EventArgs<List<Invoice>> e)
        {
            if (View.AddInvoicesByMultiSearch(e.Data))
            {
                OnActionSuccess();
            }
        }

        [EventPublication(EventTopicNames.ActionSuccess, PublicationScope.Global)]
        public event EventHandler<EventArgs> ActionSuccess;
        internal void OnActionSuccess()
        {
            try
            {
                if (ActionSuccess != null)
                    ActionSuccess(this, new EventArgs());
            }
            catch { }
        }

        [EventSubscription(EventTopicNames.CancelTransaction, Thread = ThreadOption.UserInterface)]
        public void CancelTransactionHandler(object sender, EventArgs e)
        {
            View.ClearData();
        }

        [EventSubscription(EventTopicNames.ShowingChangeAmount, Thread = ThreadOption.UserInterface)]
        public void ShowingChangeAmountHandler(object sender, EventArgs<decimal> e)
        {
            View.ChangeAmount = e.Data;
        }

        [EventPublication(EventTopicNames.ViewHistoryClick, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<HistoryViewParam>> ViewHistoryClick;
        public void OnViewHistoryClick(HistoryViewParam param)
        {
            if (ViewHistoryClick != null)
                ViewHistoryClick(this, new EventArgs<HistoryViewParam>(param));
        }

        [EventPublication(EventTopicNames.ViewInvoiceDetailClick, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<List<Bill>>> ViewInvoiceDetailClick;
        public void OnViewInvoiceDetailClick(List<Bill> bills)
        {
            if (ViewInvoiceDetailClick != null)
                ViewInvoiceDetailClick(this, new EventArgs<List<Bill>>(bills));
        }

        [EventPublication(EventTopicNames.InvoicePaymentMethodClick, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<List<Invoice>>> OnInvoicePaymentMethodClicked;
        internal void InvoicePaymentMethodClicked(List<Invoice> payingItems)
        {
            if (OnInvoicePaymentMethodClicked != null)
                OnInvoicePaymentMethodClicked(this, new EventArgs<List<Invoice>>(payingItems));
        }

        [EventPublication(EventTopicNames.StatusUpdate, PublicationScope.Global)]
        public event EventHandler<EventArgs<string>> StatusUpdate;
        public void StatusUpdateHandler(string status)
        {
            if (StatusUpdate != null)
                StatusUpdate(this, new EventArgs<string>(status));
        }

        [EventPublication(EventTopicNames.SlipHeaderUpdate, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<Invoice>> SlipHeaderUpdate;
        internal void OnHeaderModify(Invoice invoice)
        {
            if (SlipHeaderUpdate != null)
                SlipHeaderUpdate(this, new EventArgs<Invoice>(invoice));
        }

        [EventPublication(EventTopicNames.BillSearchedByCustomerDetail, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<CustomerSearchParam>> SearchedByCustomerDetail;
        public void BillSearchedByDetail(CustomerSearchParam param)
        {
            if (SearchedByCustomerDetail != null)
                SearchedByCustomerDetail(this, new EventArgs<CustomerSearchParam>(param));
        }

        [EventPublication(EventTopicNames.ReMeterItemAdd, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<string>> ReMeterItemAdd;
        internal void OnReMeterItemAdd(string disconnectStrLine_reconnectStrLine)
        {
            if (ReMeterItemAdd != null)
                ReMeterItemAdd(this, new EventArgs<string>(disconnectStrLine_reconnectStrLine));
        }

        public List<DisconnectionDoc> SearchDisconnectionStatusByDiscNo(string discNo)
        {
            return _billingService.SearchDisconnectionStatusByDiscNo(discNo);
        }

        [EventSubscription(EventTopicNames.OnCloseAllViews, Thread = ThreadOption.UserInterface)]
        public void OnCloseAllViewsHandler(object sender, EventArgs e)
        {
            OnCloseView();
        }

        [EventPublication(EventTopicNames.FocusOnSearchById, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs> FocusOnSearchById;
        internal void OnFocusOnSearchById()
        {
            if (FocusOnSearchById != null)
                FocusOnSearchById(this, new EventArgs());
        }

        #region +++ Cash Management +++

        [EventPublication(EventTopicNames.CashierOpenWork, PublicationScope.Global)]
        public event EventHandler<EventArgs<string>> CashierOpenWorkHandler;
        public void OnCashierOpenWork(string tmp)
        {
            if (CashierOpenWorkHandler != null)
                CashierOpenWorkHandler(this, new EventArgs<string>(tmp));
        }

        [EventPublication(EventTopicNames.ClosePaymentView, PublicationScope.Global)]
        public event EventHandler<EventArgs> ClosePaymentView;
        public void OnClosePaymentView()
        {
            if (ClosePaymentView != null)
                ClosePaymentView(this, new EventArgs());
        }

        [EventSubscription(EventTopicNames.EnablePOSSaveButton, Thread = ThreadOption.UserInterface)]
        public void EnablePOSSaveButtonHandler(object sender, EventArgs<bool> e)
        {
            View.EnableSaveButton(e.Data);
        }

        [EventSubscription(EventTopicNames.OnlineStatus, Thread = ThreadOption.UserInterface)]
        public void OnlineStatusHandler(object sender, EventArgs<bool> e)
        {
            if ((View.ValidatePermission() && !Session.IsNetworkConnectionAvailable) || (Session.IsNetworkConnectionAvailable && View.ValidatePermission() && Session.Work.Id != null))
                View.EnableSaveButton(true);
            else
                View.EnableSaveButton(false);
        }

        [EventSubscription(EventTopicNames.SearchResultByCaIdOnInvoice, Thread = ThreadOption.UserInterface)]
        public void OnSearchResultByCaIdOnInvoiceHandler(object sender, EventArgs<SearchByCaIdNoInvoiceParam> param) 
        {
            // DCR 67-020 
            // ����� Invoice �ҡ��ä��� ����� Invoice �ͧ Caid ������ӡ�� Remove �͡
            View.OnSearchResultByCaIdNoInvoice(param.Data.CustomerId);
        }
        
        #endregion
    }
}


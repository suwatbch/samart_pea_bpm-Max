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
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using PEA.BPM.PaymentCollectionModule.Interface.Services;

namespace PEA.BPM.PaymentCollectionModule
{
    public class BookSearchViewPresenter : Presenter<IBookSearchView>
    {
        private IBillingService _billingService;

        public BookSearchViewPresenter([ServiceDependency] IBillingService billingService)
        {
            _billingService = billingService;
        }

        [EventPublication(EventTopicNames.BillSearchedByBillBookId, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<string>> SearchedByBillBookId;
        internal void OnBillSearchedByBillBookId(string billBookId)
        {
            if (SearchedByBillBookId != null)
                SearchedByBillBookId(this, new EventArgs<string>(billBookId));
        }

        [EventPublication(EventTopicNames.BillSearchedByAgent, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<AgencySearchParam>> BillSearchedByAgent;
        internal void OnBillSearchedByAgent(AgencySearchParam param)
        {
            if (BillSearchedByAgent != null)
                BillSearchedByAgent(this, new EventArgs<AgencySearchParam>(param));
        }

        [EventPublication(EventTopicNames.NewPaymentItemAdd, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs> ItemAdded;
        internal void PaymentLineAdded()
        {
            if (ItemAdded != null)
                ItemAdded(this, new EventArgs());
        }

        [EventSubscription(EventTopicNames.OnlineStatus, Thread = ThreadOption.UserInterface)]
        public void OnlineStatusHandler(object sender, EventArgs<bool> e)
        {
            View.EnablePOSPanel(e.Data);
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

        [EventPublication(EventTopicNames.AgAdvPaymentAdd, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs> AgAdvPaymentAdd;
        internal void OnAgAdvPaymentAdd()
        {
            if (AgAdvPaymentAdd != null)
                AgAdvPaymentAdd(this, new EventArgs());
        }

        [EventSubscription(EventTopicNames.OnCloseAllViews, Thread = ThreadOption.UserInterface)]
        public void OnCloseAllViewsHandler(object sender, EventArgs e)
        {
            OnCloseView();
        }
    }
}


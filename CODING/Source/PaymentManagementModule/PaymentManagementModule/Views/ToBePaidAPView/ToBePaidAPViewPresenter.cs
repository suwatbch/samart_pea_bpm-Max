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
using PEA.BPM.PaymentManagementModule.Interface.Constants;
using System.Collections.Generic;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities;
using PEA.BPM.PaymentManagementModule.Interface.Services;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.PaymentManagementModule
{
    public class ToBePaidAPViewPresenter : Presenter<IToBePaidAPView>
    {
        private IPaymentMntService _PaymentMntService;

        [InjectionConstructor]
        public ToBePaidAPViewPresenter([ServiceDependency] IPaymentMntService PaymentMntService)
        {
            _PaymentMntService = PaymentMntService;
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

        [EventSubscription(EventTopicNames.AccountPayablePayment, Thread = ThreadOption.UserInterface)]
        public void AddedAccountPayablePaymentHandler(object sender, EventArgs<APInfo> e)
        {
            e.Data.CaName = _PaymentMntService.GetCaNameByPaymentVoucher(e.Data.CaId);
            View.AddItems(e.Data);
        }

        [EventSubscription(EventTopicNames.OnCloseAllViews, Thread = ThreadOption.UserInterface)]
        public void OnCloseAllViewsHandler(object sender, EventArgs e)
        {
            OnCloseView();
        }

        [EventPublication(EventTopicNames.LoadCashInTray, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<string>> LoadCashInTrayHandler;
        public void GetLeftAmount(string workId)
        {
            if (LoadCashInTrayHandler != null)
                LoadCashInTrayHandler(this, new EventArgs<string>(workId));
        }

        public bool PayAP(List<APInfo> ap)
        {
            return _PaymentMntService.PayAP(ap, Session.BpmDateTime.Now, Session.Terminal.Id, Session.Terminal.Code, Session.User.Id, Session.User.Name, Session.Branch.Id, Session.Branch.Name);
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

        #endregion
    }
}


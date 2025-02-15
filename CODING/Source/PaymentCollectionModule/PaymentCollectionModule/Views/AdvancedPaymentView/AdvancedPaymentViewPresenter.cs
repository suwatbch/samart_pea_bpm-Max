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
using System.Windows.Forms;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using PEA.BPM.Infrastructure.Library.UI;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using System.Collections.Generic;

namespace PEA.BPM.PaymentCollectionModule
{
    public class AdvancedPaymentViewPresenter : Presenter<IAdvancedPaymentView>
    {
        private IBillingService _billingService;

        public IBillingService BillingService
        {
            get { return _billingService; }
        }

        [InjectionConstructor]
        public AdvancedPaymentViewPresenter([ServiceDependency] IBillingService billingService)
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

        [EventSubscription(Constants.EventTopicNames.AgAdvPaymentAdd, Thread = ThreadOption.UserInterface)]
        public void AgAdvPaymentAddHandler(object sender, EventArgs e)
        {
            ShowView();
        }

        private void ShowView()
        {
            PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
            info.Modal = true;
            info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
            info.Keys.Add(WindowWorkspaceSetting.CancelButton, View.CancelButton); 
            info.MaximizeBox = false;
            info.MinimizeBox = false;
            info.Title = " �Ѻ�����Թ��ǧ˹�� 30% �ҡ���᷹";
            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(View, info);
        }

        public BillBook GetBillBookDetail(string billBookId)
        {
            return _billingService.GetBillBookDetail(billBookId, "N");
        }

        [EventPublication(EventTopicNames.PaymentItemAdd, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<List<Bill>>> BillsAddedToPayingList;
        public void BillsAddedToList(List<Bill> bills)
        {
            if (BillsAddedToPayingList != null)
                BillsAddedToPayingList(this, new EventArgs<List<Bill>>(bills));
        }

        [EventSubscription(EventTopicNames.ActionSuccess, Thread = ThreadOption.UserInterface)]
        public void ActionSuccessHandler(object sender, EventArgs e)
        {
            if (WorkItem.Workspaces[WorkspaceNames.ModalWindows].ActiveSmartPart == View)
            {
                ((UserControl)View).ParentForm.Close();
            }
        }

    }
}


using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Interface.Constants;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.Infrastructure.Interface.Services;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Library.UI;
using System.Windows.Forms;
using PEA.BPM.ePaymentsModule.Interface.Services;

namespace PEA.BPM.ePaymentsModule
{
    class ePaymentCollectionController : WorkItemController
    {

        private ILayoutService _layoutService;
        private IBillingService _billingService;

        [InjectionConstructor]
        public ePaymentCollectionController([ServiceDependency] ILayoutService layoutService, [ServiceDependency] IBillingService billingService)
        {
            _layoutService = layoutService;
            _billingService = billingService;
        }

        public void Run(int type)
        {
            LoadDisplayedViews(type);
            LoadRequiredViews();
        }

        private void LoadDisplayedViews(int type)
        {
            _layoutService.LoadHorizontalLayout(230);

            switch (type)
            {
                case 1:
                    IUploadFileView lView1;
                    SetWindowsTitle(Properties.Resources.UploadPaymentFile);
                    if (WorkItem.Items.Contains("IUploadFileView"))
                    {
                        lView1 = WorkItem.Items.Get<IUploadFileView>("IUploadFileView");
                    }
                    else
                    {
                        lView1 = WorkItem.Items.AddNew<UploadFileView>("IUploadFileView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(lView1);

                    IFileResultView rView1;
                    if (WorkItem.Items.Contains("IFileResultView"))
                    {
                        rView1 = WorkItem.Items.Get<IFileResultView>("IFileResultView");
                    }
                    else
                    {
                        rView1 = WorkItem.Items.AddNew<FileResultView>("IFileResultView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].Show(rView1);
                    break;
                case 2:
                    IVendorSearchView lView2;
                    SetWindowsTitle(Properties.Resources.VendorPayment);
                    if (WorkItem.Items.Contains("IVenderSearchView"))
                    {
                        lView2 = WorkItem.Items.Get<IVendorSearchView>("IVendorSearchView");
                    }
                    else
                    {
                        lView2 = WorkItem.Items.AddNew<VendorSearchView>("IVendorSearchView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(lView2);

                    IVendorPaymentView rView2;
                    if (WorkItem.Items.Contains("IVendorPaymentView"))
                    {
                        rView2 = WorkItem.Items.Get<IVendorPaymentView>("IVendorPaymentView");
                    }
                    else
                    {
                        rView2 = WorkItem.Items.AddNew<VendorPaymentView>("IVendorPaymentView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].Show(rView2);
                    break;
                case 3:
                    IDebtSearchView lView3;
                    SetWindowsTitle(Properties.Resources.DebtClearify);
                    if (WorkItem.Items.Contains("IDebtSearchView"))
                    {
                        lView3 = WorkItem.Items.Get<IDebtSearchView>("IDebtSearchView");
                    }
                    else
                    {
                        lView3 = WorkItem.Items.AddNew<DebtSearchView>("IDebtSearchView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(lView3);

                    IDebtClearifyView rView3;
                    if (WorkItem.Items.Contains("IDebtClearifyView"))
                    {
                        rView3 = WorkItem.Items.Get<IDebtClearifyView>("IDebtClearifyView");
                    }
                    else
                    {
                        rView3 = WorkItem.Items.AddNew<DebtClearifyView>("IDebtClearifyView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].Show(rView3);
                    break;
                case 4:
                    ICancelPaymentSearchView lView4;
                    SetWindowsTitle(Properties.Resources.CancelPayment);
                    if (WorkItem.Items.Contains("ICancelPaymentSearchView"))
                    {
                        lView4 = WorkItem.Items.Get<ICancelPaymentSearchView>("ICancelPaymentSearchView");
                    }
                    else
                    {
                        lView4 = WorkItem.Items.AddNew<CancelPaymentSearchView>("ICancelPaymentSearchView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(lView4);

                    ICancelPaymentResultView rView4;
                    if (WorkItem.Items.Contains("ICancelPaymentResultView"))
                    {
                        rView4 = WorkItem.Items.Get<ICancelPaymentResultView>("ICancelPaymentResultView");
                    }
                    else
                    {
                        rView4 = WorkItem.Items.AddNew<CancelPaymentResultView>("ICancelPaymentResultView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].Show(rView4);
                    break;
                default:
                    break;
            }
        }

        private void LoadRequiredViews()
        {
            if (!WorkItem.Items.Contains("IUploadFileView"))
            {
                WorkItem.Items.AddNew<UploadFileView>("IUploadFileView");
            }
            if (!WorkItem.Items.Contains("IDebtDetailView"))
            {
                WorkItem.Items.AddNew<DebtDetailView>("IDebtDetailView");
            }
            if (!WorkItem.Items.Contains("IDebtSearchResultView"))
            {
                WorkItem.Items.AddNew<DebtSearchResultView>("IDebtSearchResultView");
            }
            if (!WorkItem.Items.Contains("IDebtSearchPopUpView"))
            {
                WorkItem.Items.AddNew<DebtSearchPopUpView>("IDebtSearchPopUpView");
            }

        }

    }
}

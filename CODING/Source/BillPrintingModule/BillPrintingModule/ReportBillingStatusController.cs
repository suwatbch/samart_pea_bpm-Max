using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Interface.Services;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.BillPrintingModule.Constants;
using PEA.BPM.BillPrintingModule.Interface.Services;

namespace PEA.BPM.BillPrintingModule
{
    public class ReportBillingStatusController : WorkItemController
    {
        private ILayoutService _layoutService;
        private IControlServices _controlServices;
        private ReportBillStatusView sView;

        [InjectionConstructor]
        public ReportBillingStatusController([ServiceDependency] ILayoutService layoutService, IControlServices controlServices)
        {
            _layoutService = layoutService;
            _controlServices = controlServices;
        }

        public override void Run()
        {
            try
            {
                ShellWaitCursor(true);
                CloseAllViews();
                SetWindowsTitle(Properties.Resources.BillingStatusReport);
                if (WorkItem.Items.Contains("IReportBillStatusView"))
                {
                    sView = WorkItem.Items.Get<ReportBillStatusView>("IReportBillStatusView");
                }
                else
                {
                    sView = WorkItem.Items.AddNew<ReportBillStatusView>("IReportBillStatusView");
                }

                _layoutService.LoadHorizontalLayout(250);
                WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(sView);

                if (!WorkItem.Items.Contains("IReportContainerView"))
                {
                    ReportContainerView rView = WorkItem.Items.AddNew<ReportContainerView>("IReportContainerView");
                }

                Object obj = WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].ActiveSmartPart;
                if (obj != null)
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].Close(obj);

                ShellWaitCursor(false);
            }
            catch
            {
                ShellWaitCursor(false);
                throw;
            }
        }

        [EventSubscription(EventTopicNames.GetPortion, Thread = ThreadOption.UserInterface)]
        public void GetPortionHandler(object sender, EventArgs<string> e)
        {
            sView.Portion = _controlServices.GetPortion(e.Data);
        }

        [EventPublication(EventTopicNames.ShellWaitCursor, PublicationScope.Global)]
        public event EventHandler<EventArgs<bool>> ShellWaitCursorHandler;
        public void ShellWaitCursor(bool wait)
        {
            if (ShellWaitCursorHandler != null)
                ShellWaitCursorHandler(this, new EventArgs<bool>(wait));
        }
    }
}

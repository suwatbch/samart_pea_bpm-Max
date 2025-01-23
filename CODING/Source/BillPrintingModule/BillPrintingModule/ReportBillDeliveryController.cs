using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Interface.Services;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.BillPrintingModule.Constants;

namespace PEA.BPM.BillPrintingModule
{
    public class ReportBillDeliveryController : WorkItemController
    {
        private ILayoutService _layoutService;
        private ReportBillDeliveryView _sView;
        private ReportContainerView _rView;

        [InjectionConstructor]
        public ReportBillDeliveryController([ServiceDependency] ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public override void Run()
        {
            try
            {
                ShellWaitCursor(true);
                CloseAllViews();
                
                SetWindowsTitle(Properties.Resources.BillDeliveryReport);
                if (WorkItem.Items.Contains("IReportBillDeliveryView"))
                    _sView = WorkItem.Items.Get<ReportBillDeliveryView>("IReportBillDeliveryView");
                else
                    _sView = WorkItem.Items.AddNew<ReportBillDeliveryView>("IReportBillDeliveryView");

                _layoutService.LoadHorizontalLayout(250);
                WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(_sView);

                if (!WorkItem.Items.Contains("IReportContainerView"))
                {
                    _rView = WorkItem.Items.AddNew<ReportContainerView>("IReportContainerView");
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

        [EventPublication(EventTopicNames.ShellWaitCursor, PublicationScope.Global)]
        public event EventHandler<EventArgs<bool>> ShellWaitCursorHandler;
        public void ShellWaitCursor(bool wait)
        {
            if (ShellWaitCursorHandler != null)
                ShellWaitCursorHandler(this, new EventArgs<bool>(wait));
        }
    }
}

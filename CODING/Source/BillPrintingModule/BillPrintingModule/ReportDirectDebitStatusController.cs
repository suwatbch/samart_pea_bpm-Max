using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Interface.Services;
using PEA.BPM.BillPrintingModule.Interface.Constants;
using Microsoft.Practices.CompositeUI.EventBroker;

namespace PEA.BPM.BillPrintingModule
{
    public class ReportDirectDebitStatusController : WorkItemController
    {
        private ILayoutService _layoutService;

        [InjectionConstructor]
        public ReportDirectDebitStatusController([ServiceDependency] ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public override void Run()
        {
            try
            {
                ShellWaitCursor(true);
                CloseAllViews();
                ReportDirectDebitStatusView sView;
                SetWindowsTitle(Properties.Resources.DirectDebitStatusReport);
                if (WorkItem.Items.Contains("IReportDirectDebitStatusView"))
                {
                    sView = WorkItem.Items.Get<ReportDirectDebitStatusView>("IReportDirectDebitStatusView");
                }
                else
                {
                    sView = WorkItem.Items.AddNew<ReportDirectDebitStatusView>("IReportDirectDebitStatusView");
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

        [EventPublication(EventTopicNames.ShellWaitCursor, PublicationScope.Global)]
        public event EventHandler<EventArgs<bool>> ShellWaitCursorHandler;
        public void ShellWaitCursor(bool wait)
        {
            if (ShellWaitCursorHandler != null)
                ShellWaitCursorHandler(this, new EventArgs<bool>(wait));
        }
    }
}

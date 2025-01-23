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
    public class ReportPrintGreenByBankController : WorkItemController
    {
        private ILayoutService _layoutService;

        [InjectionConstructor]
        public ReportPrintGreenByBankController([ServiceDependency] ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public override void Run()
        {
            try
            {
                ShellWaitCursor(true);
                CloseAllViews();
                ReportPrintGreenByBankView sView;
                SetWindowsTitle(Properties.Resources.PrintGreenByBankReport);
                if (WorkItem.Items.Contains("IReportPrintGreenByBankView"))
                {
                    sView = WorkItem.Items.Get<ReportPrintGreenByBankView>("IReportPrintGreenByBankView");
                }
                else
                {
                    sView = WorkItem.Items.AddNew<ReportPrintGreenByBankView>("IReportPrintGreenByBankView");
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

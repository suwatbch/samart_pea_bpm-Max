using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Interface.Services;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.BillPrintingModule.Interface.Constants;
using Microsoft.Practices.CompositeUI.EventBroker;


namespace PEA.BPM.BillPrintingModule
{
    public class ReportGrpInvSummaryController : WorkItemController
    {
        private ILayoutService _layoutService;

        [InjectionConstructor]
        public ReportGrpInvSummaryController([ServiceDependency] ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public override void Run()
        {
            try
            {
                ShellWaitCursor(true);
                CloseAllViews();
                ReportGrpInvSummaryView sView;
                SetWindowsTitle(Properties.Resources.GrpInvSummaryReport);
                if (WorkItem.Items.Contains("IReportGrpInvSummaryView"))
                {
                    sView = WorkItem.Items.Get<ReportGrpInvSummaryView>("IReportGrpInvSummaryView");
                }
                else
                {
                    sView = WorkItem.Items.AddNew<ReportGrpInvSummaryView>("IReportGrpInvSummaryView");
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

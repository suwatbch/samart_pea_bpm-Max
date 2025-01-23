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
    public class ReportDailyUnprintController : WorkItemController
    {
        private ILayoutService _layoutService;

        [InjectionConstructor]
        public ReportDailyUnprintController([ServiceDependency] ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public override void Run()
        {
            try
            {
                ShellWaitCursor(true);
                CloseAllViews();
                ReportDailyUnprintView sView;
                SetWindowsTitle(Properties.Resources.DailyUnprintReport);
                if (WorkItem.Items.Contains("IReportDailyUnprintView"))
                {
                    sView = WorkItem.Items.Get<ReportDailyUnprintView>("IReportDailyUnprintView");
                }
                else
                {
                    sView = WorkItem.Items.AddNew<ReportDailyUnprintView>("IReportDailyUnprintView");
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

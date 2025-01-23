using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface.Services;
using PEA.BPM.ToolModule.Interface.Constants;
using PEA.BPM.ToolModule.Interface.Services;
using Microsoft.Practices.CompositeUI.EventBroker;


namespace PEA.BPM.ToolModule
{
    public class ReportUnlockingLogController : WorkItemController
    {
        private ILayoutService _layoutService;

        [InjectionConstructor]
        public ReportUnlockingLogController([ServiceDependency] ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public override void Run()
        {
            ShellWaitCursor(true);
            ReportUnlockingLogView sView;
            SetWindowsTitle(Properties.Resources.UnlockingLogReport);
            if (WorkItem.Items.Contains("IReportUnlockingLogView"))
            {
                sView = WorkItem.Items.Get<ReportUnlockingLogView>("IReportUnlockingLogView");
            }
            else
            {
                sView = WorkItem.Items.AddNew<ReportUnlockingLogView>("IReportUnlockingLogView");
            }

            _layoutService.LoadHorizontalLayout(250);
            WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(sView);

            //ReportContainerView rView;
            //if (WorkItem.Items.Contains("IReportContainerView"))
            //{
            //    rView = WorkItem.Items.Get<ReportContainerView>("IReportContainerView");
            //}
            //else
            //{
            //    rView = WorkItem.Items.AddNew<ReportContainerView>("IReportContainerView");
            //}

            //WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].Show(rView);

            if (!WorkItem.Items.Contains("IReportContainerView"))
            {
                WorkItem.Items.AddNew<ReportContainerView>("IReportContainerView");
            }


            ShellWaitCursor(false);
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

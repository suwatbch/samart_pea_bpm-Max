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
    public class GreenReceiptReprintController : WorkItemController
    {
        private ILayoutService _layoutService;

        [InjectionConstructor]
        public GreenReceiptReprintController([ServiceDependency] ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public override void Run()
        {
            try
            {
                ShellWaitCursor(true);
                CloseAllViews();
                GreenReceiptReprintView sView;
                SetWindowsTitle(Properties.Resources.GreenReceiptReprint);
                if (WorkItem.Items.Contains("IGreenReceiptReprintView"))
                    sView = WorkItem.Items.Get<GreenReceiptReprintView>("IGreenReceiptReprintView");
                else
                    sView = WorkItem.Items.AddNew<GreenReceiptReprintView>("IGreenReceiptReprintView");

                _layoutService.LoadHorizontalLayout(250);
                WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(sView);

                BillProcessingListView rView;
                if (!WorkItem.Items.Contains("IBillProcessingListView"))
                    rView = WorkItem.Items.AddNew<BillProcessingListView>("IBillProcessingListView");

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

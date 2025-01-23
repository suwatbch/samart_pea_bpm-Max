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
    public class GreenReceiptController : WorkItemController
    {
        private ILayoutService _layoutService;

        [InjectionConstructor]
        public GreenReceiptController([ServiceDependency] ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public override void Run()
        {
            try
            {
                ShellWaitCursor(true);
                CloseAllViews();
                GreenReceiptView sView;
                SetWindowsTitle(Properties.Resources.GreenReceipt);
                if (WorkItem.Items.Contains("IGreenReceiptView"))
                    sView = WorkItem.Items.Get<GreenReceiptView>("IGreenReceiptView");
                else
                    sView = WorkItem.Items.AddNew<GreenReceiptView>("IGreenReceiptView");

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

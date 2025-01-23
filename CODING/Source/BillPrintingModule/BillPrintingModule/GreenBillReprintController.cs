using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Interface.Services;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.BillPrintingModule.Constants;
using PEA.BPM.Architecture.CommonUtilities;
using Microsoft.Practices.CompositeUI.EventBroker;

namespace PEA.BPM.BillPrintingModule
{
    public class GreenBillReprintController : WorkItemController
    {
        private ILayoutService _layoutService;

        [InjectionConstructor]
        public GreenBillReprintController([ServiceDependency] ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public override void Run()
        {
            try
            {
                ShellWaitCursor(true);

                CloseAllViews();
                GreenBillReprintView sView;
                SetWindowsTitle(Properties.Resources.GreenBillReprint);
                if (WorkItem.Items.Contains("IGreenBillReprintView"))
                {
                    sView = WorkItem.Items.Get<GreenBillReprintView>("IGreenBillReprintView");
                }
                else
                {
                    sView = WorkItem.Items.AddNew<GreenBillReprintView>("IGreenBillReprintView");
                }

                _layoutService.LoadHorizontalLayout(250);
                WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(sView);

                if (!WorkItem.Items.Contains("IBillProcessingListView"))
                {
                    BillProcessingListView rView = WorkItem.Items.AddNew<BillProcessingListView>("IBillProcessingListView");
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

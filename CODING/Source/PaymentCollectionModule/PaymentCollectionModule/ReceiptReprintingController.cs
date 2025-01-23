using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Interface.Services;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using PEA.BPM.Architecture.CommonUtilities;
using Microsoft.Practices.CompositeUI.EventBroker;

namespace PEA.BPM.PaymentCollectionModule
{
    public class ReceiptReprintingController : WorkItemController
    {
        private ILayoutService _layoutService;

        [InjectionConstructor]
        public ReceiptReprintingController([ServiceDependency] ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public override void Run()
        {
            ShellWaitCursor(true);
            string workDayCount = string.Format(" - กำลังทำงานกะที่ ({0})", Session.Work.DayCount == 0 ? "ข้ามวัน" : Session.Work.DayCount.ToString());
            WorkItem.State["Function"] = "Reprinting";
            WorkItem.State["ToBeReprintedReceipts"] = new List<ToBeReprintedReceipt>();
            SetWindowsTitle(Properties.Resources.ReceiptReprinting + workDayCount);

            LoadDisplayedViews();
            LoadRequiredViews();
            ShellWaitCursor(false);
        }

        private void LoadDisplayedViews()
        {
            _layoutService.LoadHorizontalLayout(230);

            IReceiptSearchView lView;
            string lViewName = typeof(IReceiptSearchView).ToString();
            if (WorkItem.Items.Contains(lViewName))
            {
                lView = WorkItem.Items.Get<IReceiptSearchView>(lViewName);
            }
            else
            {
                lView = WorkItem.Items.AddNew<ReceiptSearchView>(lViewName);
            }
            WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(lView);

            IToBeReprintedView rView;
            string rViewName = typeof(IToBeReprintedView).ToString();
            if (WorkItem.Items.Contains(rViewName))
            {
                rView = WorkItem.Items.Get<IToBeReprintedView>(rViewName);
            }
            else
            {
                rView = WorkItem.Items.AddNew<ToBeReprintedView>(rViewName);
            }
            WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].Show(rView);
        }

        private void LoadRequiredViews()
        {
            string pViewName = typeof(IReceiptSearchResultView).ToString();
            if (!WorkItem.Items.Contains((pViewName)))
            {
                WorkItem.Items.AddNew<ReceiptSearchResultView>(pViewName);
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

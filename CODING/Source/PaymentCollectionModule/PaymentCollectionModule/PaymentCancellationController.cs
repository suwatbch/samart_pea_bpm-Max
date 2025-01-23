using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Interface.Constants;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.Infrastructure.Interface.Services;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Library.UI;
using System.Windows.Forms;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.PaymentCollectionModule
{
    public class PaymentCancellationController : WorkItemController
    {
        private ILayoutService _layoutService;

        [InjectionConstructor]
        public PaymentCancellationController([ServiceDependency] ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public override void Run()
        {
            ShellWaitCursor(true);
            string workDayCount = string.Format(" - กำลังทำงานกะที่ ({0})", Session.Work.DayCount == 0 ? "ข้ามวัน" : Session.Work.DayCount.ToString());
            WorkItem.State["Function"] = "Cancelling";
            WorkItem.State["ToBeCancelledReceipts"] = new List<ToBeCancelledReceipt>();
            SetWindowsTitle(Properties.Resources.PaymentCancellation + workDayCount);

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

            IToBeCancelledReceiptView rView;
            if (WorkItem.Items.Contains("IToBeCancelledPaymentView"))
            {
                rView = WorkItem.Items.Get<IToBeCancelledReceiptView>("IToBeCancelledReceiptView");
            }
            else
            {
                rView = WorkItem.Items.AddNew<ToBeCancelledReceiptView>("IToBeCancelledReceiptView");
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

        #region Event Handler

        [EventPublication(EventTopicNames.ShellWaitCursor, PublicationScope.Global)]
        public event EventHandler<EventArgs<bool>> ShellWaitCursorHandler;
        public void ShellWaitCursor(bool wait)
        {
            if (ShellWaitCursorHandler != null)
                ShellWaitCursorHandler(this, new EventArgs<bool>(wait));
        }

        #endregion
    }
}

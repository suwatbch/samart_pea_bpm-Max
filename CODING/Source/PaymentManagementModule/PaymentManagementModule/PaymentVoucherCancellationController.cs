using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Interface.Constants;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.Infrastructure.Interface.Services;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Library.UI;
using System.Windows.Forms;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.PaymentManagementModule
{
    public class PaymentVoucherCancellationController : WorkItemController
    {
        private ILayoutService _layoutService;

        [InjectionConstructor]
        public PaymentVoucherCancellationController([ServiceDependency] ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public override void Run()
        {
            ShellWaitCursor(true);
            string workDayCount = string.Format(" - กำลังทำงานกะที่ ({0})", Session.Work.DayCount == 0 ? "ข้ามวัน" : Session.Work.DayCount.ToString());
            WorkItem.State["Function"] = "Cancelling";
            //WorkItem.State["ToBeCancelledPaymentVouchers"] = new List<PEA.BPM.PaymentManagementModule.ToBeCancelledPaymentVoucherView ToBeCancelledPaymentVouchers>();
            SetWindowsTitle(Properties.Resources.PaymentVoucherCancellation + workDayCount);

            LoadDisplayedViews();
            LoadRequiredViews();
            ShellWaitCursor(false);
        }

        private void LoadDisplayedViews()
        {
            _layoutService.LoadHorizontalLayout(230);

            IPaymentVoucherSearchView lView;
            string lViewName = typeof(IPaymentVoucherSearchView).ToString();
            if (WorkItem.Items.Contains(lViewName))
            {
                lView = WorkItem.Items.Get<IPaymentVoucherSearchView>(lViewName);
            }
            else
            {
                lView = WorkItem.Items.AddNew<PaymentVoucherSearchView>(lViewName);
            }
            WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(lView);

            IToBeCancelledPaymentVoucherView rView;
            if (WorkItem.Items.Contains("IToBeCancelledPaymentVoucherView"))
            {
                rView = WorkItem.Items.Get<IToBeCancelledPaymentVoucherView>("IToBeCancelledPaymentVoucherView");
            }
            else
            {
                rView = WorkItem.Items.AddNew<ToBeCancelledPaymentVoucherView>("IToBeCancelledPaymentVoucherView");
            }
            WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].Show(rView);
        }

        private void LoadRequiredViews()
        {
            string pViewName = typeof(IPaymentVoucherSearchResultView).ToString();
            if (!WorkItem.Items.Contains((pViewName)))
            {
                WorkItem.Items.AddNew<PaymentVoucherSearchResultView>(pViewName);
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
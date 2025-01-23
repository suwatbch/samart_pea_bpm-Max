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
using PEA.BPM.PaymentManagementModule.Interface.Services;

namespace PEA.BPM.PaymentManagementModule
{
    public class AccountPayablePaymentController : WorkItemController
    {
        private ILayoutService _layoutService;
        private IPaymentMntService _paymentMntService;
        private IToBePaidAPView rView;

        [InjectionConstructor]
        public AccountPayablePaymentController([ServiceDependency] ILayoutService layoutService,
                                        IPaymentMntService paymentMntService)
        {
            _layoutService = layoutService;
            _paymentMntService = paymentMntService;
        }

        public override void Run()
        {
            ShellWaitCursor(true);
            string workDayCount = string.Format(" - กำลังทำงานกะที่ ({0})", Session.Work.DayCount == 0 ? "ข้ามวัน" : Session.Work.DayCount.ToString());
            WorkItem.State["ToBePaidAP"] = new List<ToBePaidAP>();
            SetWindowsTitle(Properties.Resources.AccountPayablePayment + workDayCount);

            LoadDisplayedViews();
            LoadRequiredViews();
            ShellWaitCursor(false);
        }

        private void LoadDisplayedViews()
        {
            _layoutService.LoadHorizontalLayout(230);

            IPaymentView lView;
            string lViewName = typeof(IPaymentView).ToString();
            if (WorkItem.Items.Contains(lViewName))
            {
                lView = WorkItem.Items.Get<IPaymentView>(lViewName);
            }
            else
            {
                lView = WorkItem.Items.AddNew<PaymentView>(lViewName);
            }
            WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(lView);

            if (WorkItem.Items.Contains("IToBePaidAPView"))
            {
                rView = WorkItem.Items.Get<IToBePaidAPView>("IToBePaidAPView");
            }
            else
            {
                rView = WorkItem.Items.AddNew<ToBePaidAPView>("IToBePaidAPView");
            }
            WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].Show(rView);
        }

        [EventSubscription(EventTopicNames.LoadCashInTray, Thread = ThreadOption.UserInterface)]
        public void LoadCashInTrayHandler(object sender, EventArgs<string> e)
        {
            try
            {
                rView.LeftAmount = _paymentMntService.GetMoneyInTray(e.Data);
            }
            catch (Exception)
            {
                //do nothing.
            }
        }

        private void LoadRequiredViews()
        {
            string pViewName = typeof(IPaymentVoucherSearchResultView).ToString();
            if (!WorkItem.Items.Contains((pViewName)))
            {
                WorkItem.Items.AddNew<PaymentVoucherSearchResultView>(pViewName);
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

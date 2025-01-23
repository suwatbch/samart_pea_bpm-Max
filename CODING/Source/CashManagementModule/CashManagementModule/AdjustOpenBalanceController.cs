using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Interface.Services;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.CashManagementModule.Interface.Constants;
using PEA.BPM.Infrastructure.Library.UI;
using System.Windows.Forms;
using PEA.BPM.Architecture.CommonUtilities;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.CashManagementModule.Interface.Services;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;

namespace PEA.BPM.CashManagementModule
{
    public class AdjustOpenBalanceController : WorkItemController
    {
        private ILayoutService _layoutService;
        private ICashManagementServices _service;
        private IAdjustOpenBalanceView _sView;

        [InjectionConstructor]
        public AdjustOpenBalanceController([ServiceDependency] ILayoutService layoutService,
                                                                ICashManagementServices service)
        {
            _layoutService = layoutService;
            _service = service;
        }

        public override void Run()
        {
            ShellWaitCursor(true);
            SetWindowsTitle(Properties.Resources.AdjustOpeningBalance);
            if (WorkItem.Items.Contains("IAdjustOpenBalanceView"))
                _sView = WorkItem.Items.Get<IAdjustOpenBalanceView>("IAdjustOpenBalanceView");
            else
                _sView = WorkItem.Items.AddNew<AdjustOpenBalanceView>("IAdjustOpenBalanceView");

            //_layoutService.LoadHorizontalLayout(250);            
            WorkItem.Workspaces[WorkspaceNames.CenterWorkspace].Show(_sView);
            ShellWaitCursor(false);
        }


        [EventSubscription(EventTopicNames.LoadAdjustOpenBalance, Thread = ThreadOption.UserInterface)]
        public void LoadAdjustOpenBalanceHandler(object sender, EventArgs<string> e)
        {
            List<PaymentMethodInfo> paymentList = _service.LoadAdjustOpenBalance(e.Data);
            _sView.PaymentList = paymentList;
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

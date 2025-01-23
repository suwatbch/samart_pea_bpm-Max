using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface.Services;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.CashManagementModule.Interface.Constants;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.CashManagementModule.Interface.Services;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.CashManagementModule
{
    public class CancelBankDeliveryController : WorkItemController
    {
        private ILayoutService _layoutService;
        private ICashManagementServices _cashManagementService;
        private ICancelBankDeliveryView _sView;

        [InjectionConstructor]
        public CancelBankDeliveryController([ServiceDependency] ILayoutService layoutService,
                                                           ICashManagementServices cashManagmentService)
        {
            _layoutService = layoutService;
            _cashManagementService = cashManagmentService;
        }

        public override void Run()
        {
            ShellWaitCursor(true);
            string dayCountTxt = Session.Work.DayCount == 0 ? "ข้ามวัน" : Session.Work.DayCount.ToString();
            SetWindowsTitle(Properties.Resources.CancelBankDeliveryMenuTitle + " - กำลังทำงานกะที่ (" + dayCountTxt + ")");
            if (WorkItem.Items.Contains("ICancelBankDeliveryView"))
                _sView = WorkItem.Items.Get<ICancelBankDeliveryView>("ICancelBankDeliveryView");
            else
                _sView = WorkItem.Items.AddNew<CancelBankDeliveryView>("ICancelBankDeliveryView");

            //_layoutService.LoadHorizontalLayout(250);            
            WorkItem.Workspaces[WorkspaceNames.CenterWorkspace].Show(_sView);
            ShellWaitCursor(false);
        }

        [EventSubscription(EventTopicNames.ListBankDelivery, Thread = ThreadOption.UserInterface)]
        public void ListBankDeliveryHandler(object sender, EventArgs<string> e)
        {
            try
            {
                List<BankDeliveryInfo> blList = _cashManagementService.ListBankDelivery(e.Data); //workId
                _sView.BankDeliveryList = blList;
            }
            catch (Exception ex)
            {
                ServiceHelper.TransformErrorMessage(ex);
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

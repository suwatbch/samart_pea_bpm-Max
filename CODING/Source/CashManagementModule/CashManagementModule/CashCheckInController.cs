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

namespace PEA.BPM.CashManagementModule
{
    public class CashCheckInController : WorkItemController
    {
        private ILayoutService _layoutService;

        [InjectionConstructor]
        public CashCheckInController([ServiceDependency] ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public override void Run()
        {
            ShellWaitCursor(true);
            IMoneyCheckInView sView;
            string dayCountTxt = Session.Work.DayCount == 0 ? "ข้ามวัน" : Session.Work.DayCount.ToString();
            SetWindowsTitle(Properties.Resources.CashCheckingInMenuTitle + " - กำลังทำงานกะที่ (" + dayCountTxt + ")");
            if (WorkItem.Items.Contains("IMoneyCheckInView"))
                sView = WorkItem.Items.Get<IMoneyCheckInView>("IMoneyCheckInView");
            else
                sView = WorkItem.Items.AddNew<MoneyCheckInView>("IMoneyCheckInView");

            //_layoutService.LoadHorizontalLayout(250);            
            WorkItem.Workspaces[WorkspaceNames.CenterWorkspace].Show(sView);
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

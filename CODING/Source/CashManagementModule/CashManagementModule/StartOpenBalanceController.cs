using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Interface.Services;
using PEA.BPM.CashManagementModule.Interface.Constants;
using PEA.BPM.Infrastructure.Library.UI;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.EventBroker;

namespace PEA.BPM.CashManagementModule
{
    public class StartOpenBalanceController : WorkItemController
    {
        private ILayoutService _layoutService;
        private WindowSmartPartInfo info;

        [InjectionConstructor]
        public StartOpenBalanceController([ServiceDependency] ILayoutService layoutService)
        {
            _layoutService = layoutService;

            info = new WindowSmartPartInfo();
            info.MaximizeBox = false;
            info.MinimizeBox = false;
            info.Modal = true;
            info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedDialog);
            info.Title = Properties.Resources.StartOpeningBalance;
        }

        public override void Run()
        {
            ShellWaitCursor(true);
            IStartOpenBalanceView sView;
            SetWindowsTitle(Properties.Resources.StartOpeningBalance);
            if (WorkItem.Items.Contains("IStartOpenBalanceView"))
                sView = WorkItem.Items.Get<IStartOpenBalanceView>("IStartOpenBalanceView");
            else
                sView = WorkItem.Items.AddNew<StartOpenBalanceView>("IStartOpenBalanceView");

            //_layoutService.LoadHorizontalLayout(250);            
            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(sView, info);
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

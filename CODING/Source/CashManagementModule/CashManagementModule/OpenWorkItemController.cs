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
using Microsoft.Practices.CompositeUI.EventBroker;

namespace PEA.BPM.CashManagementModule
{
    public class OpenWorkItemController : WorkItemController
    {
        private ILayoutService _layoutService;
        private WindowSmartPartInfo info;

        [InjectionConstructor]
        public OpenWorkItemController([ServiceDependency] ILayoutService layoutService)
        {
            _layoutService = layoutService;

            info = new WindowSmartPartInfo();
            info.MaximizeBox = false;
            info.MinimizeBox = false;
            info.Modal = true;
            info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedDialog);
            info.Title = Properties.Resources.OpenWorkMenuTitle;
        }

        public override void Run()
        {
            ShellWaitCursor(true);
            IOpenWorkItemView sView;
            SetWindowsTitle(Properties.Resources.OpenWorkMenuTitle);
            if (WorkItem.Items.Contains("IOpenWorkItemView"))
                sView = WorkItem.Items.Get<IOpenWorkItemView>("IOpenWorkItemView");
            else
                sView = WorkItem.Items.AddNew<OpenWorkItemView>("IOpenWorkItemView");

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

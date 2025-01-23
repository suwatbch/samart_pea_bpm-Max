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
    public class TransferResponseController : WorkItemController
    {
        private ILayoutService _layoutService;
        private WindowSmartPartInfo info;
        private bool _showDialogResult = true;
        private ITransferResponseView _sView;

        [InjectionConstructor]
        public TransferResponseController([ServiceDependency] ILayoutService layoutService)
        {
            _layoutService = layoutService;

            info = new WindowSmartPartInfo();
            info.MaximizeBox = false;
            info.MinimizeBox = false;
            info.Modal = true;
            info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedDialog);
            info.Title = Properties.Resources.TransferStatusMonitorMenuTitle;
        }

        public void ShowDialogResult(bool show)
        {
            _showDialogResult = show;
        }

        public override void Run()
        {
            ShellWaitCursor(true);
            //SetWindowsTitle(Properties.Resources.DailyPrintReport);
            if (WorkItem.Items.Contains("ITransferResponseView"))
                _sView = WorkItem.Items.Get<TransferResponseView>("ITransferResponseView");
            else
                _sView = WorkItem.Items.AddNew<TransferResponseView>("ITransferResponseView");

            _sView.ShowDialogResult(_showDialogResult);
            //_layoutService.LoadHorizontalLayout(250);       
            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_sView, info);
            ShellWaitCursor(false);
        }

        [EventSubscription(EventTopicNames.ShowTransferResponse, Thread = ThreadOption.UserInterface)]
        public void ShowTransferResponseHandler(object sender, EventArgs<string> e)
        {
            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_sView, info);
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

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface.Services;
using PEA.BPM.CashManagementModule.Interface.Services;
using PEA.BPM.Infrastructure.Library.UI;
using PEA.BPM.Infrastructure.Interface;
using System.Windows.Forms;
using PEA.BPM.CashManagementModule.Interface.Constants;
using Microsoft.Practices.CompositeUI.EventBroker;

namespace PEA.BPM.CashManagementModule
{
    public class ReportDailyPayInController : WorkItemController
    {
        private ILayoutService _layoutService;
        private WindowSmartPartInfo _modalProperty;
        private ICashManagementServices _cashManagementService;
        private IReportPayInDailyView _reportPayInDailyView;

        [InjectionConstructor]
        public ReportDailyPayInController([ServiceDependency] ILayoutService layoutService,
                                        ICashManagementServices cashManagementService)
        {
            _layoutService = layoutService;
            _cashManagementService = cashManagementService;

            _modalProperty = new WindowSmartPartInfo();
            _modalProperty.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            _modalProperty.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedDialog);
            _modalProperty.MaximizeBox = false;
            _modalProperty.MinimizeBox = false;
            _modalProperty.Modal = true;
        }

        public override void Run()
        {
            ShellWaitCursor(true);
            if (WorkItem.Items.Contains("IReportPayInDailyView"))
                WorkItem.Items.Remove(_reportPayInDailyView);

            SetWindowsTitle(Properties.Resources.PayInDailyReportMenuTitle);
            _reportPayInDailyView = WorkItem.Items.AddNew<ReportPayInDailyView>("IReportPayInDailyView");
            _modalProperty.Title = Properties.Resources.PayInDailyReportMenuTitle;
            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_reportPayInDailyView, _modalProperty);
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

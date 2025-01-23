using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Interface.Services;
using PEA.BPM.Infrastructure.Library.UI;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.CashManagementModule.Interface.Services;
using PEA.BPM.CashManagementModule.Interface.Constants;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.EventBroker;

namespace PEA.BPM.CashManagementModule
{
    public class ReportSumCloseWorkController : WorkItemController
    {
        private ILayoutService _layoutService;
        private WindowSmartPartInfo _modalProperty;
        private ICashManagementServices _cashManagementService;
        private IReportSummaryCloseWorkView _reportSummaryCloseWorkView;

        [InjectionConstructor]
        public ReportSumCloseWorkController([ServiceDependency] ILayoutService layoutService,
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
            if (WorkItem.Items.Contains("IReportSummaryCloseWorkView"))
                WorkItem.Items.Remove(_reportSummaryCloseWorkView);

            SetWindowsTitle(Properties.Resources.SummaryCloseWorkReportMenuTitle);
            _reportSummaryCloseWorkView = WorkItem.Items.AddNew<ReportSummaryCloseWorkView>("IReportSummaryCloseWorkView");
            _modalProperty.Title = Properties.Resources.SummaryCloseWorkReportMenuTitle;
            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_reportSummaryCloseWorkView, _modalProperty);
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

using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface.Services;
using PEA.BPM.CashManagementModule.Interface.Services;
using PEA.BPM.Infrastructure.Library.UI;
using PEA.BPM.CashManagementModule.Interface.Constants;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.CashManagementModule
{
    public class ReportMoneyFlowController : WorkItemController
    {
        private ILayoutService _layoutService;
        private ICashManagementServices _cashManagementService;
        private WindowSmartPartInfo _modalProperty;
        private IReportMoneyFlowView _reportMoneyFlowView;

        [InjectionConstructor]
        public ReportMoneyFlowController([ServiceDependency] ILayoutService layoutService,
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
            if (WorkItem.Items.Contains(""))
                WorkItem.Items.Remove(_reportMoneyFlowView);

            SetWindowsTitle(Properties.Resources.MoneyFlowReportMenuTitle);
            _reportMoneyFlowView = WorkItem.Items.AddNew<ReportMoneyFlowView>("IReportMoneyFlowView");
            _modalProperty.Title = Properties.Resources.MoneyFlowReportMenuTitle;
            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_reportMoneyFlowView, _modalProperty);
            ShellWaitCursor(false);
        }

        [EventSubscription(EventTopicNames.LoadCashier, Thread = ThreadOption.UserInterface)]
        public void LoadCashierHandler(object sender, EventArgs<string> e)
        {
            try
            {
                CashierPosIdInfo cashierPos = _cashManagementService.LoadCashierPosId(e.Data);
                _reportMoneyFlowView.CashierList = cashierPos.CashierList;
                _reportMoneyFlowView.OnWaitCursor(false);
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

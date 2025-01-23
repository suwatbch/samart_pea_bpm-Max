using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface.Services;
using PEA.BPM.CashManagementModule.Interface.Services;
using PEA.BPM.Infrastructure.Library.UI;
using PEA.BPM.CashManagementModule.Interface.Constants;
using PEA.BPM.Infrastructure.Interface;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;

namespace PEA.BPM.CashManagementModule
{
    public class ReportDailyRemainController : WorkItemController
    {
        private ILayoutService _layoutService;
        private ICashManagementServices _cashManagementService;
        private WindowSmartPartInfo _modalProperty;
        private IReportDailyRemainView _reportDailyRemainView;

        [InjectionConstructor]
        public ReportDailyRemainController([ServiceDependency] ILayoutService layoutService,
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
            if (WorkItem.Items.Contains("IReportDailyRemainView"))
                WorkItem.Items.Remove(_reportDailyRemainView);

            SetWindowsTitle(Properties.Resources.DailyRemainReportMenuTitle);
            _reportDailyRemainView = WorkItem.Items.AddNew<ReportDailyRemainView>("IReportDailyRemainView");
            _modalProperty.Title = "รายงานบันทึกการตรวจนับเงินคงเหลือ";
             WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_reportDailyRemainView, _modalProperty);
             ShellWaitCursor(false);
        }


        [EventSubscription(EventTopicNames.LoadBaseline, Thread = ThreadOption.Publisher)]
        public void LoadBaselineHandler(object sender, EventArgs<DateTime> e) 
        {
            try
            {
                List<BaselineInfo> reportData = _cashManagementService.GetBaseline(Session.Branch.Id, e.Data);
                _reportDailyRemainView.BaselineList = reportData;
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

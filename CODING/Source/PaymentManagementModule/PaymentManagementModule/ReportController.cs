using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Interface.Constants;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.Infrastructure.Interface.Services;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Library.UI;
using System.Windows.Forms;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities.Reports;

namespace PEA.BPM.PaymentManagementModule
{
    public class ReportController : WorkItemController
    {
        private ILayoutService _layoutService;

        [InjectionConstructor]
        public ReportController([ServiceDependency] ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public void Run(ReportName report)
        {
            ShellWaitCursor(true);
            LoadDisplayedViews(report);
            LoadRequiredViews(report);
            ShellWaitCursor(false);
        }

        private void LoadDisplayedViews(ReportName report)
        {
            _layoutService.LoadHorizontalLayout(230);

            switch (report)
            {
                case ReportName.AP:

                    SetWindowsTitle(Properties.Resources.RpAP);

                    IAPCriteriaView AP_View;
                    if (WorkItem.Items.Contains("IAPCriteriaView"))
                    {
                        AP_View = WorkItem.Items.Get<IAPCriteriaView>("IAPCriteriaView");
                    }
                    else
                    {
                        AP_View = WorkItem.Items.AddNew<APCriteriaView>("IAPCriteriaView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(AP_View);

                    break;
            }

            IResultView rView;
            if (WorkItem.Items.Contains("IResultView"))
            {
                rView = WorkItem.Items.Get<IResultView>("IResultView");
            }
            else
            {
                rView = WorkItem.Items.AddNew<ResultView>("IResultView");
            }
            WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].Show(rView);
        }

        private void LoadRequiredViews(ReportName report)
        {
            //
        }

        #region Event Handler

        [EventPublication(EventTopicNames.ShellWaitCursor, PublicationScope.Global)]
        public event EventHandler<EventArgs<bool>> ShellWaitCursorHandler;
        public void ShellWaitCursor(bool wait)
        {
            if (ShellWaitCursorHandler != null)
                ShellWaitCursorHandler(this, new EventArgs<bool>(wait));
        }

        #endregion
    }
}

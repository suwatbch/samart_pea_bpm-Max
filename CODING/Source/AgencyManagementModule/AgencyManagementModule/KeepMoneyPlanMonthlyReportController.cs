using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Library.UI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using PEA.BPM.AgencyManagementModule.ReportViews;
using PEA.BPM.Infrastructure.Interface.Constants;
using PEA.BPM.Architecture.ArchitectureTool.Security;

namespace PEA.BPM.AgencyManagementModule
{
    class KeepMoneyPlanMonthlyReportController : WorkItemController
    {
        private IKeepElectricPlanReoprtView _keepElectricPlanReoprtView;
        private WindowSmartPartInfo _modalProperty;

        [InjectionConstructor]
        public KeepMoneyPlanMonthlyReportController()
		{
            _modalProperty = new WindowSmartPartInfo();
            _modalProperty.MaximizeBox = false;
            _modalProperty.MinimizeBox = false;
            _modalProperty.Modal = true;
            _modalProperty.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            _modalProperty.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedDialog);   
		}

        public override void Run()
        {
            ShellWaitCursor(true);
            if (Authorization.IsAuthorized(PEA.BPM.AgencyManagementModule.Interface.Constants.SecurityNames.AgentMoneyCollectPlan, "Agency Planing Report", true))
            {
                if (WorkItem.Items.Contains("IKeepElectricPlanReoprtView"))
                {
                    _keepElectricPlanReoprtView = WorkItem.Items.Get<IKeepElectricPlanReoprtView>("IKeepElectricPlanReoprtView");
                }
                else
                {
                    _keepElectricPlanReoprtView = WorkItem.Items.AddNew<KeepElectricPlanReoprtView>("IKeepElectricPlanReoprtView");
                }

                _modalProperty.Title = "หน้าจอรายงานแผนการเก็บเงินค่าไฟฟ้า";
                WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_keepElectricPlanReoprtView, _modalProperty);
                ShellWaitCursor(false);
            }
        }

        #region "Event Handler"

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

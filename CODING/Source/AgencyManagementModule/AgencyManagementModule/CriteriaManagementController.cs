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
using PEA.BPM.AgencyManagementModule.Interface.Constants;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureTool.ErrorHandling;


namespace PEA.BPM.AgencyManagementModule
{
    class CriteriaManagementController : WorkItemController
    {
        private IAgencyModuleConfigService _agencyModuleConfig;
        private ICriteriaManagementView _criteriaManagementView;
        private WindowSmartPartInfo _modalProperty;

        [InjectionConstructor]
        public CriteriaManagementController([ServiceDependency] IAgencyModuleConfigService agencyModuleConfig)
		{
            _agencyModuleConfig = agencyModuleConfig;
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
            //WorkItem.State["AgentPlanning"] = _portionSearch;
            
            if (WorkItem.Items.Contains("ICriteriaManagementView"))
            {
                _criteriaManagementView = WorkItem.Items.Get<ICriteriaManagementView>("ICriteriaManagementView");
            }
            else
            {
                _criteriaManagementView = WorkItem.Items.AddNew<CriteriaManagementView>("ICriteriaManagementView");
            }

            //_modalProperty.Title = "ปรับแต่งค่าของระบบตัวแทน";
            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_criteriaManagementView, _modalProperty);
            SetWindowsTitle("ปรับแต่งค่าของระบบตัวแทน");
            ShellWaitCursor(false);
        }

        #region subscription event

        [EventSubscription(EventTopicNames.FeeBaseGridView, Thread = ThreadOption.UserInterface)]
        public void FeeBaseGridViewLoadedHandler(object sender, EventArgs e)
        {
            try
            {
                _criteriaManagementView.FeeBase = _agencyModuleConfig.GetBaseCommissionRate(Session.Branch.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [EventSubscription(EventTopicNames.SaveCommissionRate, Thread = ThreadOption.UserInterface)]
        public void SaveCommissionRateClickedHandler(object sender, EventArgs<FeeBaseElement> e)
        {
            try
            {
                bool success = _agencyModuleConfig.UpdateCommissionRate(e.Data);
                if (success)
                {
                    MessageBox.Show(null, "บันทึกข้อมูลเรียบร้อยแล้ว", "เสร็จสิ้น", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.AGENCY, "ตั้งค่าระบบตัวแทนเก็บเงิน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

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

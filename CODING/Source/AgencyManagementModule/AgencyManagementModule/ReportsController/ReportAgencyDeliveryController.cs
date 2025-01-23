using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI;

using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Library.UI;

using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.AgencyManagementModule.Interface.Services;

namespace PEA.BPM.AgencyManagementModule.ReportsController
{
    class ReportAgencyDeliveryController : WorkItemController
    {
        //private IReportAgentDeliveryPopupView _reportAgentDeliveryPopupView;
        //private WindowSmartPartInfo _modalProperty;

        //[InjectionConstructor]
        //public ReportAgencyDeliveryController([ServiceDependency] IReportMgtService reportMgtService)
        //{        
        //    _modalProperty = new WindowSmartPartInfo();
        //    _modalProperty.Title = "รายงานตรวจสอบการนำส่งเงินของตัวแทนเก็บเงิน";
        //    _modalProperty.MaximizeBox = false;
        //    _modalProperty.MinimizeBox = false;
        //    _modalProperty.Modal = true;
        //    _modalProperty.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
        //    _modalProperty.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedDialog);
        //}

        //public override void Run()
        //{
        //    if (WorkItem.Items.Contains("IReportAgentDeliveryPopupView"))
        //    {
        //        _reportAgentDeliveryPopupView = WorkItem.Items.Get<ReportAgentDeliveryPopupView>("IReportAgentDeliveryPopupView");
        //    }
        //    else 
        //    {
        //        _reportAgentDeliveryPopupView = WorkItem.Items.AddNew<ReportAgentDeliveryPopupView>("IReportAgentDeliveryPopupView");

        //    }
        //    WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_reportAgentDeliveryPopupView, _modalProperty);
        //}      
    }

     
}

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Interface.Services;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.BillPrintingModule.Constants;
using PEA.BPM.Infrastructure.Library.UI;

namespace PEA.BPM.BillPrintingModule
{
    public class ApproverController : WorkItemController
    {
        private WindowSmartPartInfo info;

        public ApproverController()
        {
            info = new WindowSmartPartInfo();
            info.MaximizeBox = false;
            info.MinimizeBox = false;
            info.Modal = true;
            info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedDialog);
            info.Title = "รายการผู้อนุมัติหนังสือแจ้งค่าไฟฟ้า";
        }
        
        public override void Run()
        {
            ApproverView sView;
            
            if (WorkItem.Items.Contains("IApproverView"))
            {
                sView = WorkItem.Items.Get<ApproverView>("IApproverView");
            }
            else
            {
                sView = WorkItem.Items.AddNew<ApproverView>("IApproverView");
            }
            SetWindowsTitle(Properties.Resources.AuthorizedPerson);
            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(sView, info);         
        }

    }
}
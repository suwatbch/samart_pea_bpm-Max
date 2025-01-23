using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Library.UI;
using System.Windows.Forms;
using PEA.BPM.BillPrintingModule.Constants;

namespace PEA.BPM.BillPrintingModule
{
    public class ManageBarcodeController : WorkItemController
    {
          private WindowSmartPartInfo info;

        public ManageBarcodeController()
        {
            info = new WindowSmartPartInfo();
            info.MaximizeBox = false;
            info.MinimizeBox = false;
            info.Modal = true;
            info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedDialog);
            info.Title = Properties.Resources.ManageBarcode;
        }
        
        public override void Run()
        {
            ManageBarcodeView sView;
            
            if (WorkItem.Items.Contains("IManageBarcodeView"))
            {
                sView = WorkItem.Items.Get<ManageBarcodeView>("IManageBarcodeView");
            }
            else
            {
                sView = WorkItem.Items.AddNew<ManageBarcodeView>("IManageBarcodeView");
            }
            SetWindowsTitle(Properties.Resources.ManageBarcode);
            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(sView, info);         
        }
    }
}

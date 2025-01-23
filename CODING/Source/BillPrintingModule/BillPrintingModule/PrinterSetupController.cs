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
    public class PrinterSetupController : WorkItemController
    {
        private WindowSmartPartInfo info;

        public PrinterSetupController()
        {
            info = new WindowSmartPartInfo();
            info.MaximizeBox = false;
            info.MinimizeBox = false;
            info.Modal = true;
            info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedDialog);
            info.Title = "กำหนดปลายทางของการพิมพ์ใบแจ้งค่าไฟฟ้า";
        }

        public override void Run()
        {
            PrintTargetView sView;

            if (WorkItem.Items.Contains("IPrintTargetView"))
            {
                sView = WorkItem.Items.Get<PrintTargetView>("IPrintTargetView");
            }
            else
            {
                sView = WorkItem.Items.AddNew<PrintTargetView>("IPrintTargetView");
            }
            SetWindowsTitle(Properties.Resources.AuthorizedPerson);
            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(sView, info);
        }
    }
}

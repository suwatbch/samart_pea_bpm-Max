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
    public class InvoiceHistoryController : WorkItemController
    {
        private WindowSmartPartInfo info;

        public InvoiceHistoryController()
        {
            info = new WindowSmartPartInfo();
            info.MaximizeBox = false;
            info.MinimizeBox = false;
            info.Modal = true;
            info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedDialog);
            info.Title = Properties.Resources.InvoiceHistory;
        }
        
        public override void Run()
        {
            InvoiceHistoryView sView;

            if (WorkItem.Items.Contains("IInvoiceHistoryView"))
            {
                sView = WorkItem.Items.Get<InvoiceHistoryView>("IInvoiceHistoryView");
            }
            else
            {
                sView = WorkItem.Items.AddNew<InvoiceHistoryView>("IInvoiceHistoryView");
            }
            SetWindowsTitle(Properties.Resources.InvoiceHistory);
            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(sView, info);         
        }

    }
}
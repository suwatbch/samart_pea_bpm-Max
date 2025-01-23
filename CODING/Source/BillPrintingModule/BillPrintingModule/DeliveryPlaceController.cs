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
    public class DeliveryPlaceController : WorkItemController
    {
        private WindowSmartPartInfo info;

        public DeliveryPlaceController()
        {
            info = new WindowSmartPartInfo();
            info.MaximizeBox = false;
            info.MinimizeBox = false;
            info.Modal = true;
            info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedDialog);
            info.Title = "กำหนดสถานที่นำส่งใบแจ้งค่าไฟฟ้า";
        }
        
        public override void Run()
        {
            DeliveryPlaceView sView;
            
            if (WorkItem.Items.Contains("IDeliveryPlaceView"))
            {
                sView = WorkItem.Items.Get<DeliveryPlaceView>("IDeliveryPlaceView");
            }
            else
            {
                sView = WorkItem.Items.AddNew<DeliveryPlaceView>("IDeliveryPlaceView");
            }
            SetWindowsTitle(Properties.Resources.DeliveryPlace);
            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(sView, info);         
        }

    }
}
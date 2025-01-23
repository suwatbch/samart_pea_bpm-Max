using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Interface.Services;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.Infrastructure.Library.UI;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using System.Collections;

namespace PEA.BPM.PaymentCollectionModule
{
    public class NewPaymentItemController : WorkItemController
    {
        private WindowSmartPartInfo info;

        public NewPaymentItemController()
        {
            info = new WindowSmartPartInfo();
            info.MaximizeBox = false;
            info.MinimizeBox = false;
            info.Modal = true;
            info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedDialog);
            //info.Title = " รับชำระเงินจากหนี้ที่ไม่มีในระบบหรือกรณีเครือข่ายมีปัญหา";
            //แก้ไขข้อความ เกิดความผิดพลาดของเครือข่าย 19-08-2558  
            info.Title = " รับชำระเงินจากหนี้ที่ไม่มีในระบบหรือใน Mode Offline";
        }

        public void Run(object data)
        {
            ShellWaitCursor(true);
            NewPaymentItemView sView;

            ArrayList a = (ArrayList)data;
            object b = a[0];

            if (WorkItem.Items.Contains("INewPaymentItemView"))
            {
                sView = WorkItem.Items.Get<NewPaymentItemView>("INewPaymentItemView");
            }
            else
            {
                sView = WorkItem.Items.AddNew<NewPaymentItemView>("INewPaymentItemView");
            }

            sView.IsElectricOtherBranch = (bool)b;


            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(sView, info);
            ShellWaitCursor(false);
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
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
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;

using System.Collections;

namespace PEA.BPM.PaymentCollectionModule
{
    public class RemeterController : WorkItemController
    {
        private WindowSmartPartInfo info;

        public RemeterController()
        {
            info = new WindowSmartPartInfo();
            info.MaximizeBox = false;
            info.MinimizeBox = false;
            info.Modal = true;
            info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedDialog);
            info.Title = " รับชำระเงินค่าต่อกลับมิเตอร์";
        }

        public void Run(object data)
        {
            ShellWaitCursor(true);
            RemeterView sView;

            ArrayList a = (ArrayList)data;
            object b = a[0];

            string[] tmp = a[1].ToString().Split(new char[] { '|' });
          
            if (WorkItem.Items.Contains("IRemeterView"))
            {
                sView = WorkItem.Items.Get<RemeterView>("IRemeterView");
            }
            else
            {
                sView = WorkItem.Items.AddNew<RemeterView>("IRemeterView");
            }

            sView.disconnectStrLine = tmp[0].ToString();
            sView.reconnectStrLine = tmp[1].ToString();
            sView.TmpList = (List<ToBePaidInvoice>)b;

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
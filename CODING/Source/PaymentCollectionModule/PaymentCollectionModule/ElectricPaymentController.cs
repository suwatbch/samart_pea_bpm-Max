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
    public class ElectricPaymentController : WorkItemController
    {
        private WindowSmartPartInfo info;

        public ElectricPaymentController()
        {
            info = new WindowSmartPartInfo();
            info.Modal = true;
            info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
            info.MaximizeBox = false;
            info.MinimizeBox = false;
            //info.Title = " �Ѻ�����Թ���俿��㹡óշ������ա�����ҧ˹����к��������͢����ջѭ��";
            //��䢢�ͤ��� �Դ�����Դ��Ҵ�ͧ���͢��� 19-08-2558 
            info.Title = " �Ѻ�����Թ���俿��㹡óշ������ա�����ҧ˹����к������ Mode Offline";
        }

        public void Run()
        {
            ShellWaitCursor(true);
            ElectricPaymentView sView;

            if (WorkItem.Items.Contains("IElectricPaymentView"))
            {
                sView = WorkItem.Items.Get<ElectricPaymentView>("IElectricPaymentView");
            }
            else
            {
                sView = WorkItem.Items.AddNew<ElectricPaymentView>("IElectricPaymentView");
            }

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
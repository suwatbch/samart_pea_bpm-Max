using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.ToolModule.Interface.Services;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Library.UI;
using System.Windows.Forms;
using PEA.BPM.ToolModule.Interface.Constants;
using Microsoft.Practices.CompositeUI.EventBroker;

namespace PEA.BPM.ToolModule
{
    public class ChangePasswordController : WorkItemController
    {
        private WindowSmartPartInfo _modalProperty;

        public ChangePasswordController()
        {
            _modalProperty = new WindowSmartPartInfo();
            _modalProperty.MaximizeBox = false;
            _modalProperty.MinimizeBox = false;
            _modalProperty.Modal = true;
            _modalProperty.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            _modalProperty.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
        }

        public override void Run()
        {
            ShellWaitCursor(true);
            IChangePwdView changePwdView = null;
            if (WorkItem.Items.Contains("IChangePwdView"))
                changePwdView = WorkItem.Items.Get<IChangePwdView>("IChangePwdView");
            else
                changePwdView = WorkItem.Items.AddNew<ChangePwdView>("IChangePwdView");

            _modalProperty.Title = "เปลี่ยนแปลงรหัสผู้ใช้งานระบบ";
            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(changePwdView, _modalProperty);
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

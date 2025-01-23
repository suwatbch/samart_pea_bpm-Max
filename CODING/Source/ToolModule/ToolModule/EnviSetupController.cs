using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface.Services;
using PEA.BPM.ToolModule.Interface.Constants;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.ToolModule.Interface.BusinessEntities;
using PEA.BPM.ToolModule.Interface.Services;
using PEA.BPM.Infrastructure.Library.UI;
using System.Windows.Forms;
using PEA.BPM.Architecture.CommonUtilities;



namespace PEA.BPM.ToolModule
{
    public class EnviSetupController : WorkItemController
    {
        private IAzManService _iAzManService;
        private IRegisterView _registerView;
        private IEnviSetupView _enviSetupView;
        private IServerConfigView _serverConfigView;
        private ILayoutService _layoutService;        
        private IPEACodeSearchView _peaCodeSearchView;
        private WindowSmartPartInfo _modalProperty;        

        private bool _isRegisterTabActive = false;

		[InjectionConstructor]
        public EnviSetupController([ServiceDependency] ILayoutService layoutService, IAzManService iAzManService )
		{
            _layoutService = layoutService;
            _iAzManService =  iAzManService;

            _modalProperty = new WindowSmartPartInfo();
            _modalProperty.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            _modalProperty.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedDialog);
            _modalProperty.MaximizeBox = false;
            _modalProperty.MinimizeBox = false;
            _modalProperty.Modal = true;           
		}

        public override void Run()
        {
            ShellWaitCursor(true);
            SetWindowsTitle(Properties.Resources.Tool);
            LoadDisplayedViews();
            //LoadRequiredViews();
            ShellWaitCursor(false);
        }

        private void LoadRequiredViews()
        {
            //revise
            if (!WorkItem.Items.Contains("IRegisterView"))
                WorkItem.Items.Remove(_registerView);

            if (WorkItem.Items.Contains("IServerConfigView"))
                WorkItem.Items.Remove(_serverConfigView);

            _registerView = WorkItem.Items.AddNew<RegisterView>("IRegisterView");
            _registerView.LoadDefaultValue();
            //_serverConfigView = WorkItem.Items.AddNew<ServerConfigView>("IServerConfigView");
            //_serverConfigView.LoadDefaultValue();
            _enviSetupView.RegisterTab = _registerView;
        }
        private void LoadDisplayedViews()
        {
            _layoutService.LoadPlainLayout();
            
            //if (WorkItem.Items.Contains("IEnviSetupView"))            
            //    WorkItem.Items.Remove("IEnviSetupView");            
            
            //_enviSetupView = WorkItem.Items.AddNew<EnviSetupView>("IEnviSetupView");           
            //WorkItem.Workspaces[WorkspaceNames.PlainLayout.CenterWorkspace].Show(_enviSetupView);

            if (WorkItem.Items.Contains("IRegisterView"))
                WorkItem.Items.Remove("IRegisterView");

            _registerView = WorkItem.Items.AddNew<RegisterView>("IRegisterView");
            WorkItem.Workspaces[WorkspaceNames.PlainLayout.CenterWorkspace].Show(_registerView);
        }

        #region "Subscirption Event"
        //pea text box search
        [EventSubscription(Constants.EventTopicNames.PeaSearchRowSelection, Thread = ThreadOption.UserInterface)]
        public void PeaSearchRowSelectionClickedHandler(object sender, EventArgs<Terminal> e)
        {
            IRegisterView view;
            if (WorkItem.Items.Contains("IRegisterView"))
            {
                view = WorkItem.Items.Get<IRegisterView>("IRegisterView");
            }
            else
            {
                view = WorkItem.Items.AddNew<RegisterView>("IRegisterView");
            }
            view.FocusTerminalInfo = e.Data;
        }

        [EventSubscription(Constants.EventTopicNames.PeaSearchFindButton, Thread = ThreadOption.UserInterface)]
        public void PeaSearchFindButtonHandler(object sender, EventArgs<string> e)
        {
            try
            {
                _peaCodeSearchView.PeaSearchResult = _iAzManService.SearchBranchByKeyword(e.Data, Session.User.Id);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleError(Logger.Module.POS, "ค้นหาการไฟฟ้า", ex);
            }
        }

        [EventSubscription(Constants.EventTopicNames.PeaCodedSearchShowDialog, Thread = ThreadOption.UserInterface)]
        public void PeaCodedSearchShowDialogHandler(object sender, EventArgs e)
        {
            if (WorkItem.Items.Contains("IPEACodeSearchView"))
                WorkItem.Items.Remove(_peaCodeSearchView);

            _peaCodeSearchView = WorkItem.Items.AddNew<PEACodeSearchView>("IPEACodeSearchView");
            _modalProperty.Title = "ค้นหาการไฟฟ้า";
            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_peaCodeSearchView, _modalProperty);
        }

        [EventSubscription(Constants.EventTopicNames.Register, Thread = ThreadOption.UserInterface)]
        public void RegisterHandler(object sender, EventArgs<bool> e)
        {
            bool isRegisterTab = e.Data;

            if (isRegisterTab)
            {
                _enviSetupView.RegisterTab = _registerView;
                _isRegisterTabActive = true;
            }
            else
            {
                _enviSetupView.ServerConfigTab = _serverConfigView;
                _isRegisterTabActive = true;
            }           
        }

        [EventSubscription(Constants.EventTopicNames.EnviSetupCloseView, Thread = ThreadOption.UserInterface)]
        public void EnviSetupCloseViewHandler(object sender, EventArgs e)
        {
            _enviSetupView.CloseView();
        }

        [EventSubscription(Constants.EventTopicNames.SaveEnviSetup, Thread = ThreadOption.UserInterface)]
        public void SaveEnviSetupHandler(object sender, EventArgs e)
        {
            try
            {
                if (_registerView.IsValidData())
                {
                   string terminalCode =  _registerView.SaveData();
                   if (terminalCode != String.Empty)
                   {
                       DialogResult dr = MessageBox.Show("ทำการบันทึกเรียบร้อยแล้ว", "ผลการบันทึก", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       _registerView.CloseView();
                   }
                   else 
                   {                      
                           MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้", "ผลการบันทึก", MessageBoxButtons.OK, MessageBoxIcon.Error);                      
                   }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleError(Logger.Module.POS, "ค้นหาการไฟฟ้า", ex);
            }
        }


        [EventPublication(EventTopicNames.ShellWaitCursor, PublicationScope.Global)]
        public event EventHandler<EventArgs<bool>> ShellWaitCursorHandler;
        public void ShellWaitCursor(bool wait)
        {
            if (ShellWaitCursorHandler != null)
                ShellWaitCursorHandler(this, new EventArgs<bool>(wait));
        }
     
        #endregion
    }
}

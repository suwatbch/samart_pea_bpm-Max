//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add View" recipe.
//
// A presenter calls methods of a view to update the information that the view displays. 
// The view exposes its methods through an interface definition, and the presenter contains
// a reference to the view interface. This allows you to test the presenter with different 
// implementations of a view (for example, a mock view).
//
// For more information see:
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-030-Model%20View%20Presenter%20%20MVP%20.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.ToolModule.Interface.Services;

using PEA.BPM.ToolModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.ToolModule.Interface.Constants;

namespace PEA.BPM.ToolModule
{
    public class RegisterViewPresenter : Presenter<IRegisterView>
    {

        IAzManService _azManService;

        public RegisterViewPresenter([ServiceDependency] IAzManService azManService)
        {

            _azManService = azManService;          
        }

        /// <summary>
        /// This method is a placeholder that will be called by the view when it's been loaded <see cref="System.Winforms.Control.OnLoad"/>
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();
        }

        /// <summary>
        /// Close the view
        /// </summary>
        public void OnCloseView()
        {
            base.CloseView();
        }

        [EventPublication(Constants.EventTopicNames.PeaCodedSearchShowDialog, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs> PeaCodedSearchShowDialogHandler;
        public void PeaCodedSearchShowDialogClicked()
        {
            if (PeaCodedSearchShowDialogHandler != null)
                PeaCodedSearchShowDialogHandler(this, new EventArgs());
        }

        public string RegisterTerminal(Terminal terminal, out string terminalCode)
        {
            return _azManService.RegisterTerminal(null, terminal, out terminalCode);
        }

        public void SaveLocalSetting(string key, string value)
        {
            LocalSettingHelper local = LocalSettingHelper.Instance();
            local.Add(key, value);
        }

        public void UpdateTerminal(Terminal terminal)
        {
            _azManService.UpdateTerminal(terminal);
        }

        public void UpdateLocalSetting(string key, string value)
        {
            LocalSettingHelper local = LocalSettingHelper.Instance();
            local.Update(key, value);
        }

        public Terminal LoadDefaultValue()
        {
            Terminal terminal = new Terminal();
            LocalSettingHelper local = LocalSettingHelper.Instance();
            terminal.BranchId = local.Read("BranchId") == null ? String.Empty : local.Read("BranchId").ToString();
            terminal.BranchLevel = local.Read("BranchLevel") == null ? "0" : local.Read("BranchLevel").ToString();
            terminal.BranchName = local.Read("BranchName") == null ? String.Empty : local.Read("BranchName").ToString();
            terminal.BranchName2 = local.Read("BranchName2") == null ? String.Empty : local.Read("BranchName2").ToString();
            terminal.BranchAddress = local.Read("BranchAddress") == null ? String.Empty : local.Read("BranchAddress").ToString();
            terminal.BranchNo = local.Read("BranchNo") == null ? String.Empty : local.Read("BranchNo").ToString();
            terminal.TerminalId = local.Read("PosId") == null ? String.Empty : local.Read("PosId").ToString();
            terminal.TerminalCode = local.Read("PosNo") == null ? String.Empty : local.Read("PosNo").ToString();
            terminal.TaxCode = local.Read("TaxId") == null ? String.Empty : local.Read("TaxId").ToString();
            terminal.BACode = local.Read("BACode") == null ? String.Empty : local.Read("BACode").ToString();
            return terminal;
        }


        [EventPublication(EventTopicNames.EnviSetupCloseView, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs> EnviSetupCloseViewHandler;
        public void ParentCloseView()
        {
            if (EnviSetupCloseViewHandler != null)
            {
                EnviSetupCloseViewHandler(this, new EventArgs());
            }
        }

        [EventPublication(EventTopicNames.SaveEnviSetup, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs> SaveEnviSetupHandler;
        public void SaveData()
        {
            if (SaveEnviSetupHandler != null)
            {
                SaveEnviSetupHandler(this, new EventArgs());
            }
        }

        [EventSubscription(EventTopicNames.OnCloseViewDisconnect, Thread = ThreadOption.UserInterface)]
        public void OnCloseViewDisconnectHandler(object sender, EventArgs e)
        {
            base.CloseView();
        }

    }
}


using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.Commands;
using PEA.BPM.ToolModule.Services;
using PEA.BPM.ToolModule.Interface.Services;
using PEA.BPM.Architecture.ArchitectureTool;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.ToolModule.Interface.Constants;

namespace PEA.BPM.ToolModule
{
    public class ModuleController : WorkItemController, IDisposable
    {
        private ToolStripMenuItem toolMenuItem;
        private ToolStripMenuItem reportMenuItem;
        #region Run
        public override void Run()
        {
            AddServices();
            ExtendMenu();
            ExtendToolStrip();            
        }

        protected virtual void AddServices()
        {
            WorkItem.Services.AddNew<AzManService, IAzManService>();
        }

        private void ExtendMenu()
        {
            toolMenuItem = new ToolStripMenuItem(Properties.Resources.Tool);
            //exitMenuItem = new ToolStripMenuItem(Properties.Resources.ExitBPM);
            WorkItem.UIExtensionSites[UIExtensionSiteNames.MainMenu].Add(toolMenuItem);
            //WorkItem.UIExtensionSites[UIExtensionSiteNames.MainMenu].Add(exitMenuItem);

            bool enable = Session.Branch.Id != null;

            AddMenuItem(toolMenuItem, Properties.Resources.ManageAz, CommandNames.ManageAz, 0, enable);
            AddMenuItem(toolMenuItem, Properties.Resources.ChangePassword, CommandNames.ChangePassword, 0, enable);
            AddMenuItem(toolMenuItem, Properties.Resources.EnviSetup, CommandNames.EnviSetUp, 0);
            AddMenuItem(toolMenuItem, Properties.Resources.DBSetUp, CommandNames.DBSetUp, 0);
            AddMenuItem(toolMenuItem, Properties.Resources.Configuration, CommandNames.Configuration, 0, enable);
            AddMenuItem(toolMenuItem, Properties.Resources.POSConfig, CommandNames.POSConfig, 0, enable);
            AddMenuItem(toolMenuItem, Properties.Resources.ManageRemark, CommandNames.ManageRemark, 0, enable);

            reportMenuItem = AddGroupMenu(toolMenuItem, Properties.Resources.Report);
            AddMenuItem(reportMenuItem, Properties.Resources.UnlockingLogReport, CommandNames.UnlockingLogReport, 0, enable);
            //AddMenuItem(reportMenuItem, Properties.Resources.DataCrossCheckedReport, CommandNames.DataCrossCheckedReport, 0);

            AddMenuItem(toolMenuItem, Properties.Resources.News, CommandNames.News, 0, enable);
            //exit menu
            //AddMenuItem(exitMenuItem, Properties.Resources.ExitItem, CommandNames.ExitProgram, 0, enable);
        }

        private void ExtendToolStrip()
        {

        }
        #endregion

        #region Command Handler

        [CommandHandler(CommandNames.EnviSetUp)]
        public void EnviSetupAzHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.EnviSetUp, true))
            {
                ControlledWorkItem<EnviSetupController> azRegWorkItem =
                   WorkItem.WorkItems.AddNew<ControlledWorkItem<EnviSetupController>>();
                azRegWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.DBSetUp)]
        public void DBSetUpHandler(object sender, EventArgs e)
        {
            ControlledWorkItem<DBSetUpController> dbSetUpWorkItem =
               WorkItem.WorkItems.AddNew<ControlledWorkItem<DBSetUpController>>();
            dbSetUpWorkItem.Controller.Run();
        }

        [CommandHandler(CommandNames.ManageAz)]
        public void ManageAzHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(PEA.BPM.ToolModule.Interface.Constants.SecurityNames.AzManDoor, true))
            {
                ControlledWorkItem<AzManController> azManWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<AzManController>>();
                azManWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.Configuration)]
        public void ConfigurationHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.Configuration, true))
            {
                ControlledWorkItem<ConfigurationController> configurationWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<ConfigurationController>>();
                configurationWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.ManageRemark)]
        public void ManageRemarkHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.ManageRemark, true))
            {
                ControlledWorkItem<RemarkController> remarkWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<RemarkController>>();
                remarkWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.UnlockingLogReport)]
        public void UnlockingLogReportHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.UnlockingLogReport, true))
            {
                ControlledWorkItem<ReportUnlockingLogController> unlockingLogWorkItem =
                   WorkItem.WorkItems.AddNew<ControlledWorkItem<ReportUnlockingLogController>>();
                unlockingLogWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.ChangePassword)]
        public void ChangePasswordHandler(object sender, EventArgs e)
        {
            ControlledWorkItem<ChangePasswordController> changePwdWorkItem =
                WorkItem.WorkItems.AddNew<ControlledWorkItem<ChangePasswordController>>();
            changePwdWorkItem.Controller.Run();
        }

        [CommandHandler(CommandNames.POSConfig)]
        public void POSConfigHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.POSConfig, true))
            {
                ControlledWorkItem<QueueConnSetupController> queueWorkItem =
                   WorkItem.WorkItems.AddNew<ControlledWorkItem<QueueConnSetupController>>();
                queueWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.News)]
        public void NewsHandler(object sender, EventArgs e)
        {
            ControlledWorkItem<NewsController> newsWorkItem =
               WorkItem.WorkItems.AddNew<ControlledWorkItem<NewsController>>();
            newsWorkItem.Controller.Run();
        }
        //[CommandHandler(CommandNames.ExitProgram)]
        //public void ExitProgramHandler(object sender, EventArgs e)
        //{
            
            //if(dlg == DialogResult.OK)
            //    Application.Exit();
        //}

        [EventSubscription(EventTopicNames.OnlineStatus, Thread = ThreadOption.UserInterface)]
        public void OnlineStatusHandler(object sender, EventArgs<bool> e)
        {
            if (e.Data)
            {
                toolMenuItem.DropDownItems[0].Enabled = true;
                toolMenuItem.DropDownItems[1].Enabled = true;
                toolMenuItem.DropDownItems[2].Enabled = true;
                toolMenuItem.DropDownItems[3].Enabled = true;
                toolMenuItem.DropDownItems[6].Enabled = true;
                toolMenuItem.DropDownItems[7].Enabled = true;
                toolMenuItem.DropDownItems[8].Enabled = true;
                reportMenuItem.Enabled = true;
            }
            else
            {
                toolMenuItem.DropDownItems[0].Enabled = false;
                toolMenuItem.DropDownItems[1].Enabled = false;
                toolMenuItem.DropDownItems[2].Enabled = false;
                toolMenuItem.DropDownItems[3].Enabled = false;
                toolMenuItem.DropDownItems[6].Enabled = false;
                toolMenuItem.DropDownItems[7].Enabled = false;
                toolMenuItem.DropDownItems[8].Enabled = false;
                reportMenuItem.Enabled = false;
            }
        }

        #endregion

        #region Event Handler

        [EventSubscription(PEA.BPM.Infrastructure.Interface.Constants.EventTopicNames.PrintSetupEvent, Thread = ThreadOption.UserInterface)]
        public void PrintSetupEventHandler(object sender, EventArgs e)
        {
            ControlledWorkItem<ConfigurationController> configurationWorkItem =
                 WorkItem.WorkItems.AddNew<ControlledWorkItem<ConfigurationController>>();
            configurationWorkItem.Controller.Run();
        }

        #endregion

        #region Dispose
        ~ModuleController()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }
        #endregion
    }
}
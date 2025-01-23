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
using System.ComponentModel;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureTool.ErrorHandling;
using PEA.BPM.PaymentCollectionModule.Interface.Services;

namespace PEA.BPM.PaymentCollectionModule
{
    public class BillSearchResultController : WorkItemController
    {
        private WindowSmartPartInfo info;

        private IBillingService _billingService;

        [InjectionConstructor]
        public BillSearchResultController([ServiceDependency] IBillingService billingService)
        {
            _billingService = billingService;
        }

        public BillSearchResultController()
        {
            info = new WindowSmartPartInfo();
            info.Modal = true;
            info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
            info.MaximizeBox = false;
            info.MinimizeBox = false;
            info.Title = " ผลการค้นหาข้อมูลหนี้ที่มีในระบบ";
        }

        public void BillSearchResultController_Run(CustomerSearchParam e)
        {
            ShellWaitCursor(true);

            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_BillSearchedByDetailDoWorkController);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_BillSearchedByDetailCompletedController);
            bgWorker.RunWorkerAsync(e);
            WaitingFormHelper.ShowWaitingForm();

            ShellWaitCursor(false);
        }

        void bgWorker_BillSearchedByDetailDoWorkController(object sender, DoWorkEventArgs e)
        {
            try
            {
                CustomerSearchParam param = (CustomerSearchParam)e.Argument;
                List<BillSearchDetail> bills = _billingService.SearchBillByCustomerDetail(param);
                e.Result = bills;
                WaitingFormHelper.HideWaitingForm();
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.POS, "ค้นหาข้อมูลหนี้จากรายละเอียดลูกหนี้", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.POS, ex);
            }
        }

        void bgWorker_BillSearchedByDetailCompletedController(object sender, RunWorkerCompletedEventArgs e)
        {
            if (null != e.Result)
            {
                List<BillSearchDetail> bills = (List<BillSearchDetail>)e.Result;

                if (bills.Count > 0)
                {
                    BillSearchResultView sView;
                    info = new WindowSmartPartInfo();
                    info.Modal = true;
                    info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                    info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                    info.MaximizeBox = false;
                    info.MinimizeBox = false;
                    info.Title = " ผลการค้นหาข้อมูลหนี้ที่มีในระบบ";

                    if (WorkItem.Items.Contains("IBillSearchResultView"))
                    {
                        sView = WorkItem.Items.Get<BillSearchResultView>("IBillSearchResultView");
                    }
                    else
                    {
                        sView = WorkItem.Items.AddNew<BillSearchResultView>("IBillSearchResultView");
                    }

                    sView.Bills = bills;
                    WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(sView, info);
                    ShellWaitCursor(false);


                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูลดังกล่าวอยู่ในระบบ", "ข้อความ",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
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
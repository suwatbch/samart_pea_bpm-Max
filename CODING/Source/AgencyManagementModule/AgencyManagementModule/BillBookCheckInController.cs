using System;
using System.Collections;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Library.UI;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using PEA.BPM.AgencyManagementModule.Interface.Constants;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using System.Collections.Generic;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureTool.ErrorHandling;

namespace PEA.BPM.AgencyManagementModule
{
    public class BillBookCheckInController : WorkItemController
    {
        private WindowSmartPartInfo _modalProperty;
        private IAgencyPlanningService _agencyPlanningService;
        private IAgencyCommonService _agencyCommonService;
        private IBillBookCheckInView _billBookCheckInView;
        private IBillBookUndoSaveCheckInView _billBookUndoSaveCheckInView;
        private IBillBookCheckInSummaryPreView _billBookCheckInSummaryPreView;
        private IBillBookSlipPosingCheckInView _billBookSlipPosingCheckInView;
        private IBillBookCheckInDetailListView _billBookCheckInDetailListView;
        private IBillBookPaymentStatusView _billBookPaymentStatusView;
        //private IBillBookCheckInCancelTypeView _billBookCheckInCancelTypeView;
        private IPaymentMethodView _paymentMethodView;

        [InjectionConstructor]
        public BillBookCheckInController([ServiceDependency] IAgencyPlanningService agencyPlanningService, IAgencyCommonService agencyCommonService)
        {

            _agencyPlanningService = agencyPlanningService;
            _agencyCommonService = agencyCommonService;
            _modalProperty = new WindowSmartPartInfo();
            _modalProperty.MaximizeBox = false;
            _modalProperty.MinimizeBox = false;
            _modalProperty.Modal = true;
            _modalProperty.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            _modalProperty.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedDialog);
        }

        public override void Run()
        {
            ShellWaitCursor(true);
            if (WorkItem.Items.Contains("IBillBookCheckInView"))
            {
                _billBookCheckInView = WorkItem.Items.Get<IBillBookCheckInView>("IBillBookCheckInView");
            }
            else
            {
                _billBookCheckInView = WorkItem.Items.AddNew<BillBookCheckInView>("IBillBookCheckInView");
            }
            WorkItem.Workspaces[WorkspaceNames.CenterWorkspace].Show(_billBookCheckInView);
            //((UserControl)_billBookCheckInView).ParentForm.Text = "ตัดชำระสมุดจ่ายบิล - Billing & Payment Management";
            SetWindowsTitle("ตัดชำระสมุดจ่ายบิล /Group Invoicing");

            ShellWaitCursor(false);
        }

        #region subscription event
        [EventSubscription(EventTopicNames.BillBookCheckInDetailList, Thread = ThreadOption.UserInterface)]
        public void ShowBillBookDetailButtonClickedHandler(object sender, EventArgs<BillBookCheckInInfo> billBookCheckIn)
        {
            if (WorkItem.State["IBillBookCheckInDetailListView"] != null)
            {
                if (WorkItem.Items.Contains("IBillBookCheckInDetailListView"))
                {
                    Object tmp = WorkItem.Items["IBillBookCheckInDetailListView"];
                    WorkItem.Items.Remove(tmp);
                }
                _billBookCheckInDetailListView = WorkItem.Items.AddNew<BillBookCheckInDetailListView>("IBillBookCheckInDetailListView");

                _billBookCheckInDetailListView.AbsList = _agencyCommonService.GetAbsList(String.Empty);
                _billBookCheckInDetailListView.PmList = _agencyCommonService.GetPmList(String.Empty);
                _billBookCheckInDetailListView.SetData(billBookCheckIn.Data);
                _modalProperty.Title = String.Format("รายละเอียดการเก็บเงินของตัวแทนเก็บเงินผู้ใช้ไฟฟ้าของสมุดจ่ายบิล");
                WorkItem.State["IBillBookCheckInDetailListView"] = null;

                WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_billBookCheckInDetailListView, _modalProperty);
            }

        }

        [EventPublication(EventTopicNames.ShellWaitCursor, PublicationScope.Global)]
        public event EventHandler<EventArgs<bool>> ShellWaitCursorHandler;
        public void ShellWaitCursor(bool wait)
        {
            if (ShellWaitCursorHandler != null)
                ShellWaitCursorHandler(this, new EventArgs<bool>(wait));
        }

        [EventSubscription(EventTopicNames.BillBookCheckInFinishButton, Thread = ThreadOption.UserInterface)]
        public void BillBookCheckInFinishButtonClicked(object sender, EventArgs<BillBookCheckInInfo> e)
        {
            if (WorkItem.State["IBillBookSlipPosingCheckInView"] != null)
            {
                if (WorkItem.Items.Contains("IBillBookSlipPosingCheckInView"))
                {
                    Object tmp = WorkItem.Items["IBillBookSlipPosingCheckInView"];
                    WorkItem.Items.Remove(tmp);
                }
                _billBookSlipPosingCheckInView = WorkItem.Items.AddNew<BillBookSlipPosingCheckInView>("IBillBookSlipPosingCheckInView");
                _billBookSlipPosingCheckInView.SetData(e.Data);
                _modalProperty.Title = "บันทึกข้อมูลการตัดชำระสมุดจ่ายบิล เลือกวางบิล";
                WorkItem.State["IBillBookSlipPosingCheckInView"] = null;
                WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_billBookSlipPosingCheckInView, _modalProperty);
            }

        }

        [EventSubscription(EventTopicNames.BillBookSlipPosingRecordButton, Thread = ThreadOption.UserInterface)]
        public void BillBookSlipPosingRecordButtonClicked(object sender, EventArgs<BillBookCheckInInfo> e)
        {
            try
            {
                if (WorkItem.State["IBillBookCheckInSummaryPreView"] != null)
                {
                    if (WorkItem.Items.Contains("IBillBookCheckInSummaryPreView"))
                    {
                        Object tmp = WorkItem.Items["IBillBookCheckInSummaryPreView"];
                        WorkItem.Items.Remove(tmp);
                    }
                    _billBookCheckInSummaryPreView = WorkItem.Items.AddNew<BillBookCheckInSummaryPreView>("IBillBookCheckInSummaryPreView");
                    _billBookCheckInSummaryPreView.SetData(e.Data);
                    _modalProperty.Title = "บันทึกการตัดชำระสมุดจ่ายบิล /Group Invoicing";
                    WorkItem.State["IBillBookCheckInSummaryPreView"] = null;
                    WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_billBookCheckInSummaryPreView, _modalProperty);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.AGENCY, "ตัดชำระสมุดจ่ายบิล /Group Invoicing", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        [EventSubscription(EventTopicNames.BillBookUndoSaveImgButton, Thread = ThreadOption.UserInterface)]
        public void BillBookUndoSaveImgButtonClickedHandler(object sender, EventArgs<string> e)
        {
            if (WorkItem.State["IBillBookUndoSaveCheckInView"] != null)
            {
                if (WorkItem.Items.Contains("IBillBookUndoSaveCheckInView"))
                {
                    Object tmp = WorkItem.Items["IBillBookUndoSaveCheckInView"];
                    WorkItem.Items.Remove(tmp);
                }
                _billBookUndoSaveCheckInView = WorkItem.Items.AddNew<BillBookUndoSaveCheckInView>("IBillBookUndoSaveCheckInView");
                _billBookUndoSaveCheckInView.ClearScreen();
                _billBookUndoSaveCheckInView.BillBookId = (string)e.Data;
                _modalProperty.Title = "ยกเลิกการตัดชำระสมุดจ่ายบิล";
                WorkItem.State["IBillBookUndoSaveCheckInView"] = null;
                WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_billBookUndoSaveCheckInView, _modalProperty);
            }
        }

        [EventSubscription(EventTopicNames.BillBookPaymentStatusButton, Thread = ThreadOption.UserInterface)]
        public void BillBookPaymentStatusButtonClickedHandler(object sender, EventArgs<string> e)
        {
            if (WorkItem.State["IBillBookPaymentStatusView"] != null)
            {
                if (WorkItem.Items.Contains("IBillBookPaymentStatusView"))
                {
                    Object tmp = WorkItem.Items["IBillBookPaymentStatusView"];
                    WorkItem.Items.Remove(tmp);
                }

                _billBookPaymentStatusView = WorkItem.Items.AddNew<BillBookPaymentStatusView>("IBillBookPaymentStatusView");

                _billBookPaymentStatusView.BillBookId = e.Data;
                WorkItem.State["IBillBookPaymentStatusView"] = null;
                WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_billBookPaymentStatusView, _modalProperty);
            }
        }

        private void ProcessAcquireBillBookAmountSummarizedCallback(bool success, BillBookAmountSumInfo billBookAmountSumInfo)
        {
            _billBookCheckInView.ReturnedBillBookAmountSumInfo = billBookAmountSumInfo;
        }

        //saved undo
        [EventSubscription(EventTopicNames.SavedBillBookIdTextCommitted, Thread = ThreadOption.UserInterface)]
        public void SavedBillBookIdTextChangeCommittedHandler(object sender, EventArgs<string> e)
        {
            string billBookId = (string)e.Data;
            //_agencyPlanningService.AcquireBillBookInformation(billBookId, ProcessAcquireSavedBillBookInformationCallback);
            //_agencyPlanningService.AcquireSavedBillBookSearchDetail(billBookId, ProcessAcquireSavedBillBookSearchDetailCallback);
        }

        private void ProcessAcquireSavedBillBookSearchDetailCallback(bool success, List<BillBookSearchSavedDetail> savedDetailList)
        {
            _billBookUndoSaveCheckInView.SavedDetailList = savedDetailList;
        }

        //confirm status before saving
        [EventSubscription(EventTopicNames.ConfirmStatusBillBookIdTextCommitted, Thread = ThreadOption.UserInterface)]
        public void ConfirmStatusBillBookIdTextCommittedHandler(object sender, EventArgs<string> e)
        {
            string billBookId = (string)e.Data;
            //_agencyPlanningService.AcquireBillBookInformation(billBookId, ProcessConfirmStatusBillBookInfoCallback);
            //_agencyPlanningService.AcquireSavedBillBookSearchDetail(billBookId, ProcessConfirmStatusBillBookDetailCallback);
        }

        //private void ProcessConfirmStatusBillBookDetailCallback(bool success, List<BillBookSearchSavedDetail> savedDetailList)
        //{
        //    _billBookCheckInSummaryPreView.SavedDetailList = savedDetailList;
        //}

        //payment status event
        [EventSubscription(EventTopicNames.PaymentStatusBillBookIdTextCommitted, Thread = ThreadOption.UserInterface)]
        public void PaymentStatusBillBookIdTextCommittedHandler(object sender, EventArgs<string> e)
        {
            string billBookId = (string)e.Data;
            //_agencyPlanningService.AcquireBillBookInformation(billBookId, ProcessPaymentStatusBillBookInfoCallback);
            //_agencyPlanningService.AcquireBillBookPaymentDetail(billBookId, ProcessPaymentStatusBillBookDetailCallback);
        }

        //confirm status before saving
        [EventSubscriptionAttribute(EventTopicNames.AgencyBillBookCheckInClearForm, Thread = ThreadOption.UserInterface)]
        public void ClearBillBookCheckInFormHandler(object sender, EventArgs e)
        {
            if (WorkItem.Items.Contains("IBillBookCheckInView"))
            {
                _billBookCheckInView = WorkItem.Items.Get<IBillBookCheckInView>("IBillBookCheckInView");
            }
            else
            {
                _billBookCheckInView = WorkItem.Items.AddNew<BillBookCheckInView>("IBillBookCheckInView");
            }
            //if (WorkItem.Items.Contains("IBillBookCheckInView"))
            //    WorkItem.Items.Remove(_billBookCheckInView);
            //_billBookCheckInView = WorkItem.Items.AddNew<BillBookCheckInView>("IBillBookCheckInView");

            _billBookCheckInView.ClearBillBookCheckInScreen();
            _billBookCheckInView.FocusBillBookId();
            //WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_billBookCheckInView, _modalProperty);
        }

        // show first form when user click edit from BillBookCheckInSummaryPreView in case that user select all item
        [EventSubscription(EventTopicNames.BillBookCheckInFirstButton, Thread = ThreadOption.UserInterface)]
        public void BillBookCheckInFirstButtonClickedHandler(object sender, EventArgs<BillBookCheckInInfo> e)
        {
            if (WorkItem.Items.Contains("IBillBookCheckInView"))
            {
                _billBookCheckInView = WorkItem.Items.Get<BillBookCheckInView>("IBillBookCheckInView");
            }
            else
            {
                _billBookCheckInView = WorkItem.Items.AddNew<BillBookCheckInView>("IBillBookCheckInView");
            }
            _billBookCheckInView.SetData(e.Data);
            _modalProperty.Title = "ตัดชำระสมุดจ่ายบิล /Group Invoicing - Billing & Payment Management";
            WorkItem.Workspaces[WorkspaceNames.CenterWorkspace].Show(_billBookCheckInView);
        }

        [EventSubscription(EventTopicNames.ShowPaymentMethod, Thread = ThreadOption.UserInterface)]
        public void PaymentMethodHandler(object sender, EventArgs<BillBookCheckInInfo> e)
        {
            if (WorkItem.State["IPaymentMethodView"] != null)
            {
                if (WorkItem.Items.Contains("IPaymentMethodView"))
                {
                    Object tmp = WorkItem.Items["IPaymentMethodView"];
                    WorkItem.Items.Remove(tmp);
                }
                _paymentMethodView = WorkItem.Items.AddNew<PaymentMethodView>("IPaymentMethodView");

                _paymentMethodView.SetData(e.Data);
                _modalProperty.Title = "วิธีการชำระเงินของผู้ใช้ไฟ";
                WorkItem.State["IPaymentMethodView"] = null;
                WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_paymentMethodView, _modalProperty);
            }
        }

        #endregion
    }
}




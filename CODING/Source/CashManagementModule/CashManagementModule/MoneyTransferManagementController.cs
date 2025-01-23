using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Interface.Services;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.CashManagementModule.Interface.Constants;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.Infrastructure.Library.UI;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using PEA.BPM.CashManagementModule.Interface.Services;
using PEA.BPM.CashManagementModule.Properties;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.CashManagementModule
{
    public class MoneyTransferManagementController : WorkItemController
    {
        private ICashManagementServices _cashManagementService;
        private ICashReportServices _cashReportService;
        private ILayoutService _layoutService;
        private ICashierSearchBoxView _cashierSearchBoxView;
        private WindowSmartPartInfo _modalProperty;
        private IMoneyTransferManagementView _sView;

        [InjectionConstructor]
        public MoneyTransferManagementController([ServiceDependency] 
                                ILayoutService layoutService,
                                ICashManagementServices cashManagementService,
                                ICashReportServices cashReportService)
        {
            _layoutService = layoutService;
            _cashManagementService = cashManagementService;
            _cashReportService = cashReportService;

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
            string dayCountTxt = Session.Work.DayCount == 0 ? "ข้ามวัน" : Session.Work.DayCount.ToString();
            SetWindowsTitle(Properties.Resources.TransferManagementMenuTitle + " - กำลังทำงานกะที่ (" + dayCountTxt + ")");
            if (WorkItem.Items.Contains("IMoneyTransferManagementView"))
                _sView = WorkItem.Items.Get<IMoneyTransferManagementView>("IMoneyTransferManagementView");
            else
                _sView = WorkItem.Items.AddNew<MoneyTransferManagementView>("IMoneyTransferManagementView");

            //_layoutService.LoadHorizontalLayout(250);   
            WorkItem.Workspaces[WorkspaceNames.CenterWorkspace].Show(_sView);
            ShellWaitCursor(false);
        }

        [EventSubscription(EventTopicNames.CashierSearchBox, Thread = ThreadOption.UserInterface)]
        public void CashierSearchBoxHandler(object sender, EventArgs<List<string>> e)
        {
            if (WorkItem.Items.Contains("ICashierSearchBoxView"))
                WorkItem.Items.Remove(_cashierSearchBoxView);

            _cashierSearchBoxView = WorkItem.Items.AddNew<CashierSearchBoxView>("ICashierSearchBoxView");
            _cashierSearchBoxView.SenderId = e.Data[1];
            _cashierSearchBoxView.ReceiverId = e.Data[2];

            if (e.Data[0] == Resources.SenderSearchBoxTitle)
                _cashierSearchBoxView.IsSender = true;
            else
                _cashierSearchBoxView.IsSender = false;

            _modalProperty.Title = e.Data[0];
            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_cashierSearchBoxView, _modalProperty);
        }

        [EventSubscription(EventTopicNames.GLBankAccount, Thread = ThreadOption.UserInterface)]
        public void GLBankAccountHandler(object sender, EventArgs<string> e)
        {
            List<GLBankInfo> bankList = _cashManagementService.ListGLBank(e.Data);
            _sView.GLBankList = bankList;

            List<GLBankAccountInfo> accList = _cashManagementService.ListGLBankAccount(e.Data, null);
            _sView.GLBankAccountList = accList;
        }

        [EventSubscription(EventTopicNames.CashierSelection, Thread = ThreadOption.UserInterface)]
        public void CashierSelectionHandler(object sender, EventArgs<CashierInfo> e)
        {
            if (e.Data.Flag == "0") //sender
                _sView.SenderHeadline = e.Data;
            else
                _sView.ReceiverHeadline = e.Data;
        }

        [EventSubscription(EventTopicNames.DisplayMoneyInTay, Thread = ThreadOption.UserInterface)]
        public void MoneyInTayHandler(object sender, EventArgs<string> e)
        {
            try
            {
                ////--Old--
                //TrayMoneyInfo trayMoneyInfo = _cashManagementService.GetMoneyInTray(e.Data); //workId
                //_sView.MoneyInTray = trayMoneyInfo;

                ////--New--
                TrayMoneyInfo trayMoneyInfo = _cashManagementService.GetMoneyInTray(e.Data); //workId
                _sView.MoneyInTrayLastRequest = trayMoneyInfo;
                //_sView.MoneyInTrayLastRequest = ServiceHelper.Clone(trayMoneyInfo);
                //copyTrayMoneyInfo(_sView.MoneyInTrayLastRequest, trayMoneyInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.TransformErrorMessage(ex);
            }
        }

        [EventSubscription(EventTopicNames.TransferMoney, Thread = ThreadOption.UserInterface)]
        public void TransferMoneyHandler(object sender, EventArgs<MoneyTransferInfo> e)
        {
            try
            {
                CashierMoneyFlowInfo flowInfo = _cashManagementService.Transfer(null, e.Data);
                //_sView.SetMoneyInTray2MoneyInTrayLastRequest();

                //บังคับโอน ขณะปิดกะ
                if (flowInfo.SpecialTrans)
                {
                    try
                    {
                        //accept and close work
                        List<string> transList = new List<string>();
                        transList.Add(flowInfo.TransferId);
                        _cashManagementService.ResponseTransferedItems(transList, Session.Work.Id, "1",
                                Session.Terminal.Id, Session.Branch.Id, Session.User.Id);

                        //now close it
                        CloseWorkSubmitInfo submitInfo = new CloseWorkSubmitInfo();
                        submitInfo.WorkId = flowInfo.WorkId;
                        submitInfo.CashierId = flowInfo.CashierId;
                        submitInfo.CloseWorkBy = flowInfo.CloseWorkBy;
                        submitInfo.BranchId = Session.Branch.Id;
                        submitInfo.PosId = Session.Terminal.Id;
                        _cashManagementService.CloseWork(submitInfo);
                        _sView.SenderWorkId = flowInfo.WorkId;

                    }
                    catch (Exception ek)
                    {
                        ServiceHelper.TransformErrorMessage(ek);
                        return;
                    }
                }
                //บังคับโอนขณะเปิดกะ ถ้าผู้รับเป็นคนเดียวกับคนที่ทำรายการ ให้ auto accept
                else if (e.Data.IsForceTrans)
                {
                    if (e.Data.Receiver == e.Data.Commander)
                    {
                        List<string> transList = new List<string>();
                        transList.Add(flowInfo.TransferId);
                        _cashManagementService.ResponseTransferedItems(transList, Session.Work.Id, "1",
                                Session.Terminal.Id, Session.Branch.Id, Session.User.Id);
                    }
                }

                if (e.Data.ToBank && flowInfo != null && flowInfo.FlowId != null)  //report only bank
                {
                    ReportBankPayInDetailInfo reportData = _cashManagementService.GetBankPayInDetailForReport(flowInfo);
                    _sView.SetMoneyInTray2MoneyInTrayLastRequest();
                    if (e.Data.PreviewReport)
                    {
                        // backup by tanayoot 12-12-2562
                        PreviewReport(reportData);
                        //if (!e.Data.SepChq)
                        //    PreviewReport(reportData);
                        //else
                        //{
                        //    _sView.ShowSuccess("ทำรายการเสร็จเรียบร้อย \nกรุณากดปุ่ม OK เพื่อทำงานต่อ");
                        //}
                    }

                }
                else
                {
                    _sView.SetMoneyInTray2MoneyInTrayLastRequest();
                    _sView.ShowSuccess("ทำรายการเสร็จเรียบร้อย \nกรุณากดปุ่ม OK เพื่อทำงานต่อ");
                }

                _sView.OnWaitCursor(false); //set to default
            }
            catch (Exception ex)
            {
                ServiceHelper.TransformErrorMessage(ex);
            }
        }

        [EventPublication(EventTopicNames.PreviewBankPayInDetailReport, PublicationScope.Global)]
        public event EventHandler<EventArgs<ReportBankPayInDetailInfo>> PreviewBankPayInDetailReportHandler;
        public void PreviewReport(ReportBankPayInDetailInfo reportData)
        {
            if (PreviewBankPayInDetailReportHandler != null)
                PreviewBankPayInDetailReportHandler(this, new EventArgs<ReportBankPayInDetailInfo>(reportData));
        }

        [EventSubscription(EventTopicNames.RefreshTransferScreen, Thread = ThreadOption.UserInterface)]
        public void RefreshTransferScreenHandler(object sender, EventArgs e)
        {
            _sView.RefreshTrayMoney();
        }

        [EventPublication(EventTopicNames.ShellWaitCursor, PublicationScope.Global)]
        public event EventHandler<EventArgs<bool>> ShellWaitCursorHandler;
        public void ShellWaitCursor(bool wait)
        {
            if (ShellWaitCursorHandler != null)
                ShellWaitCursorHandler(this, new EventArgs<bool>(wait));
        }

        [EventSubscription(EventTopicNames.MoneyInTray, Thread = ThreadOption.UserInterface)]
        public void MoneyInTray(object sender, EventArgs<string> e)
        {
            try
            {
                TrayMoneyInfo trayMoneyInfo = _cashManagementService.GetMoneyInTray(e.Data); //workId
                _sView.MoneyInTrayValidation = trayMoneyInfo;
            }
            catch (Exception ex)
            {
                ServiceHelper.TransformErrorMessage(ex);
            }
        }

    }
}

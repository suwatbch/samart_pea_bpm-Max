using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Interface.Services;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Library.UI;
using PEA.BPM.CashManagementModule.Interface.Constants;
using System.Windows.Forms;
using PEA.BPM.CashManagementModule.Properties;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.CashManagementModule.Interface.Services;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.CashManagementModule
{
    public class ReportController : WorkItemController
    {
        private ILayoutService _layoutService;
        private ICashManagementServices _cashManagementService;
        private ICashReportServices _cashReportService;
        private WindowSmartPartInfo _modalProperty;
        private ReportMoneyFlowView _reportMoneyFlowView;
        private IReportContainerView _sView;
        private IReportContainerView _sView2;

        [InjectionConstructor]
        public ReportController([ServiceDependency] ILayoutService layoutService,
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
            if (WorkItem.Items.Contains("IReportMoneyFlowView"))
                WorkItem.Items.Remove(_reportMoneyFlowView);

            _reportMoneyFlowView = WorkItem.Items.AddNew<ReportMoneyFlowView>("IReportInputBoxView");
            _modalProperty.Title = "ออกรายงานย้อนหลัง";
        }

        public void Show()
        {
            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_reportMoneyFlowView, _modalProperty);
        }

        private void FillZero(ReportDailyRemainInfo reportData)
        {
            //fill count
            //reportData.C1000 = 0;
            //reportData.C500 = 0;
            //reportData.C100 = 0;
            //reportData.C50 = 0;
            //reportData.C20 = 0;
            //reportData.C10 = 0;

            reportData.C1000 = "0";
            reportData.C500 = "0";
            reportData.C100 = "0";
            reportData.C50 = "0";
            reportData.C20 = "0";
            reportData.C10 = "0";

            //fill amount
            reportData.Amt1000 = "0";
            reportData.Amt500 = "0";
            reportData.Amt100 = "0";
            reportData.Amt50 = "0";
            reportData.Amt20 = "0";
            reportData.Amt10 = "0";

            reportData.CoinAmt = "0";
            reportData.CoinAmt_Frag = "00";

        }

        //realtime report
        [EventSubscription(EventTopicNames.WorkFlowReport, Thread = ThreadOption.UserInterface)]
        public void WorkFlowReportHandler(object sender, EventArgs<string> e)
        {
            try
            {
                ReportWorkFlowSummary flowSummary = _cashManagementService.GetWorkFlowReport(e.Data); //workId
                flowSummary.DayCount = (int?)Session.Work.DayCount;

                SetWindowsTitle("รายงานรายละเอียดการรับจ่ายเงิน");
                if (WorkItem.Items.Contains("IReportContainerView"))
                    _sView = WorkItem.Items.Get<IReportContainerView>("IReportContainerView");
                else
                    _sView = WorkItem.Items.AddNew<ReportContainerView>("IReportContainerView");

                _sView.SetLabel = "รายงานรายละเอียดการรับจ่ายเงิน";
                _sView.Preview(flowSummary, "");
                WorkItem.Workspaces[WorkspaceNames.CenterWorkspace].Show(_sView);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        [EventSubscription(EventTopicNames.PrintPayInDetail, Thread = ThreadOption.Publisher)]
        public void PrintPayInDetailHandler(object sender, EventArgs<string> e) //apId
        {
            CashierMoneyFlowInfo flowInfo = new CashierMoneyFlowInfo();
            flowInfo.FlowId = e.Data;
            ReportBankPayInDetailInfo reportData = _cashManagementService.GetBankPayInDetailForReport(flowInfo);
            PreviewBankPayInDetailReport(reportData);
        }


        private void PreviewBankPayInDetailReport(ReportBankPayInDetailInfo reportData)
        {
            try
            {
                if (reportData.ChqList.Count == 0)
                    return;

                SetWindowsTitle("รายงานรายละเอียดประกอบใบเข้าบัญชีธนาคาร");
                if (WorkItem.Items.Contains("IReportContainerView"))
                    _sView = WorkItem.Items.Get<IReportContainerView>("IReportContainerView");
                else
                    _sView = WorkItem.Items.AddNew<ReportContainerView>("IReportContainerView");

                _sView.SetLabel = "รายงานรายละเอียดประกอบใบเข้าบัญชีธนาคาร";
                _sView.Preview(reportData);
                WorkItem.Workspaces[WorkspaceNames.CenterWorkspace].Show(_sView);

            }
            catch (Exception ex)
            {
                ServiceHelper.TransformErrorMessage(ex);
            }
        }


        [EventSubscription(EventTopicNames.PreviewBankPayInDetailReport, Thread = ThreadOption.UserInterface)]
        public void PreviewBankPayInDetailReportHandler(object sender, EventArgs<ReportBankPayInDetailInfo> e)
        {
            PreviewBankPayInDetailReport(e.Data);
        }

        [EventSubscription(EventTopicNames.ReportDailyRemain, Thread = ThreadOption.UserInterface)]
        public void ReportDailyRemainHandler(object sender, EventArgs<ReportParam> e)
        {
            try
            {
                if (WorkItem.Items.Contains("IReportContainerView"))
                    WorkItem.Items.Remove(_sView);

                _sView = WorkItem.Items.AddNew<ReportContainerView>("IReportContainerView");

                string title = "รายงานบันทึกการตรวจนับเงินคงเหลือ ";
                List<ReportDailyRemainInfo> reportData = _cashManagementService.GetHistDailyRemainReport(e.Data);
                if (reportData.Count > 0)
                {
                    if (reportData[0].OverallCashAmt > 0 || reportData[0].OverallChqAmt > 0)
                    {
                        if (reportData[0].OverallCashAmt > 0)
                        {
                            //popup fill money type dialog box
                            FillMoneyTypeForm mtForm = new FillMoneyTypeForm(reportData[0]);
                            DialogResult dlg = mtForm.ShowDialog();
                            if (dlg != DialogResult.OK)
                                return;
                        }

                        //fill default = 0 to money type
                        reportData[0].SumAmtTxt = StringConvert.ConvertAmountToText(reportData[0].SumAmt.Value.ToString());
                        _sView.Preview(reportData);

                        SetWindowsTitle(title);
                        _sView.SetLabel = title;
                        WorkItem.Workspaces[WorkspaceNames.CenterWorkspace].Show(_sView);

                        //remain cheque, print report now!
                        if (reportData[0].RemainCheque.Count > 0)
                        {
                            if (WorkItem.Items.Contains("IReportContainerView2"))
                                WorkItem.Items.Remove(_sView2);

                            _sView2 = WorkItem.Items.AddNew<ReportContainerView>("IReportContainerView2");

                            _sView2.Preview(reportData[0].RemainCheque, reportData[0].CloseWorkDate);
                            title = "รายการเช็คคงค้างประจำวัน";
                            //SetWindowsTitle(title);
                            _sView2.SetLabel = title;
                            _modalProperty.Title = "ตรวจนับเงินคงเหลือ - รายการเช็คคงค้างประจำวัน";
                            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_sView2, _modalProperty);
                        }
                    }
                    else
                    {
                        FillZero(reportData[0]);
                        //reportData[0].SumAmtTxt = StringConvert.ConvertAmountToText("0.00");
                        //_sView.Preview(reportData);
                        reportData[0].SumAmtTxt = StringConvert.ConvertAmountToText(reportData[0].SumAmt.Value.ToString());
                        _sView.Preview(reportData);

                        SetWindowsTitle(title);
                        _sView.SetLabel = title;
                        WorkItem.Workspaces[WorkspaceNames.CenterWorkspace].Show(_sView);
                    }
                }
                else
                {
                    ///... notify user
                    MessageBox.Show("ไม่พบข้อมูลเพื่อออกรายงาน\nกรุณากดปุ่ม OK เพื่อทำงานต่อ", "ไม่พบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                ServiceHelper.TransformErrorMessage(ex);
            }
        }

        [EventPublication(EventTopicNames.MoneyCheckList, PublicationScope.Global)]
        public event EventHandler<EventArgs<string>> MoneyCheckListHandler;
        public void PreviewReport(string param)
        {
            if (MoneyCheckListHandler != null)
                MoneyCheckListHandler(this, new EventArgs<string>(param));
        }


        [EventSubscription(EventTopicNames.ReportMoneyFlow, Thread = ThreadOption.UserInterface)]
        public void ReportMoneyFlowHandler(object sender, EventArgs<ReportParam> e)
        {
            try
            {
                if (WorkItem.Items.Contains("IReportContainerView"))
                    WorkItem.Items.Remove(_sView);

                _sView = WorkItem.Items.AddNew<ReportContainerView>("IReportContainerView");

                string title = "รายงานการรับ-จ่ายเงิน";
                List<ReportAvailableInfo> avList = _cashReportService.GetWorkBetweenDate(e.Data, "0"); //ItemId as workId
                AvailableListForm avForm = new AvailableListForm();
                avForm.AvailableList = avList;
                DialogResult dlg = avForm.ShowDialog();

                if (dlg == DialogResult.OK)
                {
                    if (avForm.SelectedItem == null) return;
                    //check selected item
                    ReportAvailableInfo selectedItem = avForm.SelectedItem;
                    ReportWorkFlowSummary flowSummary = _cashReportService.GetWorkFlowDelayedReport(selectedItem.ItemId); //workId
                    flowSummary.DayCount = selectedItem.DayCount;
                    _sView.Preview(flowSummary, "1.12");
                    SetWindowsTitle(title);
                    _sView.SetLabel = title;
                    WorkItem.Workspaces[WorkspaceNames.CenterWorkspace].Show(_sView);
                }
            }
            catch (Exception ex)
            {
                ServiceHelper.TransformErrorMessage(ex);
            }
        }

        [EventSubscription(EventTopicNames.PayInDailyReport, Thread = ThreadOption.UserInterface)]
        public void PayInDailyReportHandler(object sender, EventArgs<ReportParam> e)
        {
            try
            {
                List<ReportDailyPayInInfo> dailyPayInList = _cashReportService.GetBankPayInDailyForReport(e.Data);
                if (dailyPayInList.Count > 0)
                {
                    if (WorkItem.Items.Contains("IReportContainerView"))
                        WorkItem.Items.Remove(_sView);

                    _sView = WorkItem.Items.AddNew<ReportContainerView>("IReportContainerView");
                    SetWindowsTitle(Properties.Resources.PayInDailyReportMenuTitle);
                    _sView.SetLabel = Properties.Resources.PayInDailyReportMenuTitle;

                    _sView.Preview(dailyPayInList);
                    WorkItem.Workspaces[WorkspaceNames.CenterWorkspace].Show(_sView);
                }
                else 
                {
                    MessageBox.Show("ไม่พบรายการนำฝากธนาคารในวันที่ระบุ", "ไม่พบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                ServiceHelper.TransformErrorMessage(ex);
            }
        }


        [EventSubscription(EventTopicNames.SummaryCloseWorkReport, Thread = ThreadOption.UserInterface)]
        public void SummaryCloseWorkReportHandler(object sender, EventArgs<ReportParam> e)
        {
            try
            {
                List<ReportCloseWorkSummary> cwSumList = _cashReportService.GetCloseWorkSummaryReport(e.Data);
                if (cwSumList.Count > 0)
                {
                    if (WorkItem.Items.Contains("IReportContainerView"))
                        WorkItem.Items.Remove(_sView);

                    _sView = WorkItem.Items.AddNew<ReportContainerView>("IReportContainerView");
                    SetWindowsTitle(Properties.Resources.SummaryCloseWorkReportMenuTitle);
                    _sView.SetLabel = Properties.Resources.SummaryCloseWorkReportMenuTitle;
                    cwSumList[0].ReportCondition = e.Data.ConditionDesc;

                    _sView.Preview(cwSumList);
                    WorkItem.Workspaces[WorkspaceNames.CenterWorkspace].Show(_sView);
                }
                else
                {
                    MessageBox.Show("ไม่พบรายการแคชเชียร์ที่ปิดกะในวันที่กำหนด", "ไม่พบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                ServiceHelper.TransformErrorMessage(ex);
            }
        }

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
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
using PEA.BPM.AgencyManagementModule.ReportViews;
using PEA.BPM.AgencyManagementModule.Interface;
using PEA.BPM.AgencyManagementModule.Interface.Constants;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.Architecture.CommonUtilities;
using System.Globalization;
using PEA.BPM.Architecture.ArchitectureTool.ErrorHandling;

namespace PEA.BPM.AgencyManagementModule.Views
{
    public class ReportManagementController : WorkItemController
    {
        private ModuleController _moduleController = new ModuleController();
        private IReportMgtService _reportMgrService;
        private IAgencyPlanningService _agencyPlaningMgrService;

        private WindowSmartPartInfo _modalProperty;
        private ICAB01_01ReportView _CAB01_01ReportView;
        private IKeepElectricPlanReoprtView _keepElectricPlanReportView;
        private ICommissionDetailReportView _commissionDetailReportView;
        private IEvaluateAgencyAboutKeepElectricView _evaluateAgencyAboutKeepElectricView;
        private ICAB04_01ReportView _CAB04_01ReportView;
        private ICAB04_02ReportView _CAB04_02ReportView;
        private ICAB04_03ReportView _CAB04_03ReportView;
        private ICAB01_02ReportView _CAB01_02ReportView;
        private ICAB03_01ReportView _CAB03_01ReportView;
        private ICAB03_02ReportView _CAB03_02ReportView;
        private ICAB03_03ReportView _CAB03_03ReportView;
        private ICAB03_04ReportView _CAB03_04ReportView;
        private ICAB02_01ReportView _CAB02_01ReportView;
        private ICAB07_01ReportView _CAB07_01ReportView;
        private ICAB08_01ReportView _CAB08_01ReportView;
        private ICAB09_01ReportView _CAB09_01ReportView;
        private ICAB08_02ReportView _CAB08_02ReportView;
        private ICAB10_01ReportView _CAB10_01ReportView;
        private ICAB12_01ReportView _CAB12_01ReportView;
        private ICAB06_01ReportView _CAB06_01ReportView;
        private ICAN34_01ReportView _CAN34_01ReportView;
        private ICAB13_01ReportView _CAB13_01ReportView;

        //private IHistoryForDepositBillStatusReportView _historyForDepositBillStatusReportView;
        private IReportAgentMoneyReturnVerificationPopupView _reportAgentMoneyReturnVerificationPopupView;
        private ICAN34_01ReportPopupView _CAN34_01ReportPopupView;
        private ICAB12_01ReportPopupView _CAB12_01ReportPopupView;
        private ICAB13_01ReportPopupView _CAB13_01ReportPopupView;
        private ICAB05_01ReportPopupView _CAB05_01ReportPopupView;
        private IPA_7034ReportPopupView _PA_7034ReportPopupView;
        private ICAB05_01ReportView _CAB05_01ReportView;
        private IPA_7034ReportView _PA_7034ReportView;

        private IAgentSearchReportPopupView _agentSearchReportPopupView;

        [InjectionConstructor]
        public ReportManagementController([ServiceDependency] IReportMgtService reportMgtService, IAgencyPlanningService agencyPlaningMgtService)
        //public ReportManagementController()
        {
            _reportMgrService = reportMgtService;
            _agencyPlaningMgrService = agencyPlaningMgtService;

            _modalProperty = new WindowSmartPartInfo();
            _modalProperty.MaximizeBox = true;
            _modalProperty.MinimizeBox = true;
            _modalProperty.Modal = true;
            _modalProperty.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            _modalProperty.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.Sizable);

        }

        public override void Run()
        {
            //_billBookPrintPrintView = WorkItem.Items.AddNew<BillBookPrintView>("IBillBookPrintView");            
        }


        #region subscription event

        [EventSubscription(EventTopicNames.ShowCAB01_02Report, Thread = ThreadOption.UserInterface)]
        public void LoadCAB01_02ReportHandler(object sender, EventArgs<BillBookHeaderInfo> e)
        {
            try
            {
                //ReportParameterInfo _p = e.Data as ReportParameterInfo;
                BillBookHeaderInfo _bookHeader = e.Data as BillBookHeaderInfo;
                WaitingFormHelper.ShowWaitingForm();
                List<BillBookInfoDetailReport> _detail = this.GetBillBookDetailReport(_bookHeader);
                WaitingFormHelper.HideWaitingForm();

                _bookHeader.PrintDate = Session.BpmDateTime.Now.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                _bookHeader.BranchName = String.Format("{0}, {1}", Session.Branch.Name, Session.Branch.Id);

                if (WorkItem.Items.Contains("ICAB01_02ReportView"))
                {
                    WorkItem.Items.Remove(_CAB01_02ReportView);
                }

                _CAB01_02ReportView = WorkItem.Items.AddNew<CAB01_02ReportView>("ICAB01_02ReportView");
                _CAB01_02ReportView.ShowReport(_detail, _bookHeader);

                if (_bookHeader.IsPrintPreview)
                {
                    PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                    info.Modal = true;
                    info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                    info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                    info.MaximizeBox = false;
                    info.MinimizeBox = false;
                    //info.Title = ";
                    WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAB01_02ReportView, info);
                    SetWindowsTitle("รายละเอียดประกอบแสดงหมายเลขผู้ใช้ไฟฟ้า และจำนวนเงิน (PA-CAB01_02)");
                }
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "พิมพ์รายละเอียดสมุดจ่ายบิล", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        [EventSubscription(EventTopicNames.ShowCAB01_01Report, Thread = ThreadOption.UserInterface)]
        public void LoadCAB01_01ReportHandler(object sender, EventArgs<BillBookHeaderInfo> e)
        {
            try
            {
                if (WorkItem.Items.Contains("ICAB01_01ReportView"))
                {
                    WorkItem.Items.Remove(_CAB01_01ReportView);
                }

                _CAB01_01ReportView = WorkItem.Items.AddNew<CAB01_01ReportView>("ICAB01_01ReportView");
                BillBookHeaderInfo _bookHeader = e.Data as BillBookHeaderInfo;

                WaitingFormHelper.ShowWaitingForm();
                BillBookInfoMasterReport _master = this.GetBillBookMasterReport(_bookHeader);
                WaitingFormHelper.HideWaitingForm();

                _CAB01_01ReportView.ShowReport(_master, _bookHeader.IsPrintPreview);

                if (_bookHeader.IsPrintPreview)
                {
                    PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                    info.Modal = true;
                    info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                    info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                    info.MaximizeBox = false;
                    info.MinimizeBox = false;
                    info.Title = "หน้าจอแสดงใบปะหน้าของสมุดจ่ายบิล (PA-CAB01_01)";
                    WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAB01_01ReportView, info);
                }
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "พิมพ์สมุดจ่ายบิล", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        [EventSubscription(EventTopicNames.ShowKeepPlanningAgencyView, Thread = ThreadOption.UserInterface)]
        public void ShowKeepPlanningAgencyViewHandler(object sender, EventArgs<String> e)
        {
            if (WorkItem.Items.Contains("IKeepElectricPlanReoprtView"))
            {
                WorkItem.Items.Remove(_keepElectricPlanReportView);
            }

            _keepElectricPlanReportView = WorkItem.Items.AddNew<KeepElectricPlanReoprtView>("IKeepElectricPlanReoprtView");

            PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
            info.Modal = true;
            info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
            info.MaximizeBox = false;
            info.MinimizeBox = false;
            info.Title = "หน้าจอรายงานแผนการเก็บเงิน";

            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_keepElectricPlanReportView, info);

        }

        [EventSubscription(EventTopicNames.ShowEvaluateAgencyAboutKeepElectricView, Thread = ThreadOption.UserInterface)]
        public void ShowEvaluateAgencyAboutKeepElectricViewHandler(object sender, EventArgs e)
        {
            try
            {

                if (WorkItem.Items.Contains("IEvaluateAgencyAboutKeepElectricView") == true)
                {
                    WorkItem.Items.Remove(_evaluateAgencyAboutKeepElectricView);
                }

                _evaluateAgencyAboutKeepElectricView = WorkItem.Items.AddNew<EvaluateAgencyAboutKeepElectricView>("IEvaluateAgencyAboutKeepElectricView");

                PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                info.Modal = true;
                info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                info.MaximizeBox = false;
                info.MinimizeBox = false;
                info.Title = "หน้าจอรายงานประเมินผลการจัดเก็บเงินค่าไฟฟ้าของตัวแทนเก็บเงินในแต่ละเขต /แต่ละการไฟฟ้า";

                WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_evaluateAgencyAboutKeepElectricView, info);
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "หน้าจอรายงานประเมินผลการจัดเก็บเงินค่าไฟฟ้าของตัวแทนเงินในแต่ละเขต /แต่ละการไฟฟ้า", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        ////This Event use for Load View of History For Deposit Bill Status Report
        ////Create By Chettha Pattananitisak 
        //[EventSubscription(EventTopicNames.ShowHistoryForDepositBillStatusView, Thread = ThreadOption.UserInterface)]
        //public void ShowHistoryForDepositBillStatusViewHandler(object sender, EventArgs e)
        //{
        //    if (WorkItem.Items.Contains("IHistoryForDepositBillStatusReportView"))
        //    {
        //        WorkItem.Items.Remove(_historyForDepositBillStatusReportView);
        //    }

        //    _historyForDepositBillStatusReportView = WorkItem.Items.AddNew<HistoryForDepositBillStatusReportView>("IHistoryForDepositBillStatusReportView");

        //    PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
        //    info.Modal = true;
        //    info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
        //    info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
        //    info.MaximizeBox = false;
        //    info.MinimizeBox = false;
        //    info.Title = "หน้าจอรายงานตรวจสอบประวัติผู้ใช้ไฟฟ้าประเภทฝากวางใบแจ้งค่าไฟฟ้า";
        //    WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_historyForDepositBillStatusReportView, info);
        //}

        //This Event use for Load Registry Commission Report Screen
        //Create By Chettha Pattananitisak Date 25/03/2007 Time 01:35
        [EventSubscription(EventTopicNames.ShowCAB04_02Report, Thread = ThreadOption.UserInterface)]
        public void LoadCAB04_02ReportHandler(object sender, EventArgs<CommissionVoucherConditionPrintReport> e)
        {
            try
            {

                CommissionVoucherConditionPrintReport comm = (CommissionVoucherConditionPrintReport)e.Data;
                List<CommissionRegistInfoReport> a = comm.IsBlank ? new List<CommissionRegistInfoReport>() : _reportMgrService.FindAndDisplayAgencyCommissionRegistryReportInfo(comm);

                bool printPreview = comm.PrintPreview;

                if (WorkItem.Items.Contains("ICAB04_02ReportView") == true)
                {
                    WorkItem.Items.Remove(_CAB04_02ReportView);
                }

                _CAB04_02ReportView = WorkItem.Items.AddNew<CAB04_02ReportView>("ICAB04_02ReportView");
                _CAB04_02ReportView.ShowReport(a, printPreview);

                if (printPreview)
                {
                    PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                    info.Modal = true;
                    info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                    info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                    info.MaximizeBox = false;
                    info.MinimizeBox = false;
                    info.Title = "ทะเบียนการจ่ายเงินค่าตอบแทนเก็บเงินค่าไฟฟ้า (PA-CAB04_02)";
                    WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAB04_02ReportView, info);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.AGENCY, "ออกรายงาน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        //This Event use for Load Registry Commission Report Screen
        //Create By Chettha Pattananitisak Date 29/03/2007 Time 17:35
        [EventSubscription(EventTopicNames.ShowCAB04_01Report, Thread = ThreadOption.UserInterface)]
        public void LoadCAB04_01ReportHandler(object sender, EventArgs<CommissionVoucherConditionPrintReport> e)
        {
            try
            {
                CommissionVoucherConditionPrintReport comm = (CommissionVoucherConditionPrintReport)e.Data;
                List<CommissionVoucherInfoReport> a = comm.IsBlank ? new List<CommissionVoucherInfoReport>() : _reportMgrService.FindAndDisplayAgencyCommissionReportInfo(comm);
                bool printPreview = comm.PrintPreview;

                if (WorkItem.Items.Contains("ICAB04_01ReportView") == true)
                {
                    WorkItem.Items.Remove(_CAB04_01ReportView);
                }
                
                _CAB04_01ReportView = WorkItem.Items.AddNew<CAB04_01ReportView>("ICAB04_01ReportView");

                if (a.Count > 0)
                {
                    a[0].PEACode = Session.Branch.Id;
                    a[0].PEAName = Session.Branch.Name;
                }

                _CAB04_01ReportView.ShowReport(a, printPreview);

                if (printPreview)
                {
                    PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                    info.Modal = true;
                    info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                    info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                    info.MaximizeBox = false;
                    info.MinimizeBox = false;
                    info.Title = "ใบสำคัญจ่าย (PA-CAB04_01)";
                    WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAB04_01ReportView, info);
                }

            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.AGENCY, "ออกรายงาน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        //This Event use for Show Quanlity and Amount Of Item Of Bill in BillBook compair between Bill Out With Bill that can keep money  
        //Create By Chettha Pattananitisak Date 18/04/2007 Time 18:30
        [EventSubscription(EventTopicNames.ShowCAB04_03Report, Thread = ThreadOption.UserInterface)]
        public void LoadCAB04_03ReportHandler(object sender, EventArgs<CommissionVoucherConditionPrintReport> e)
        {
            try
            {
                CommissionVoucherConditionPrintReport comm = (CommissionVoucherConditionPrintReport)e.Data;
                CAB04_03HeaderReportInfo header = comm.IsBlank ? new CAB04_03HeaderReportInfo() : _reportMgrService.FindAndDisplayCAB04_03Header(comm);
                List<CAB04_03DetailReportInfo> commissionRegistryDetail = comm.IsBlank ? new List<CAB04_03DetailReportInfo>() : _reportMgrService.FindAndDisplayCAB04_03Detail(comm);

                if (WorkItem.Items.Contains("ICAB04_03ReportView"))
                {
                    WorkItem.Items.Remove(_CAB04_03ReportView);
                }
                header.PrintPreview = comm.PrintPreview;
                _CAB04_03ReportView = WorkItem.Items.AddNew<CAB04_03ReportView>("ICAB04_03ReportView");
                _CAB04_03ReportView.ShowReport(header, commissionRegistryDetail);
                if (header.PrintPreview)
                {
                    PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                    info.Modal = true;
                    info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                    info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                    info.MaximizeBox = false;
                    info.MinimizeBox = false;
                    info.Title = "รายงานรายละเอียดประกอบทะเบียนจ่ายค่าตอบแทนเก็บเงินค่าไฟฟ้า (PA-CAB04_03)";

                    WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAB04_03ReportView, info);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.AGENCY, "ออกรายงาน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        //This Event use for Load Customer don't paid Money by don't pass Agentcy Report Screen
        //Create By Chettha Pattananitisak Date 10/04/2007 Time 11:30
        [EventSubscription(EventTopicNames.ShowCAB03_01Report, Thread = ThreadOption.UserInterface)]
        public void LoadCAB03_01ReportHandler(object sender, EventArgs<CheckInBillBookConditionInfoReport> e)
        {
            try
            {
                CheckInBillBookConditionInfoReport con = (CheckInBillBookConditionInfoReport)e.Data;
                WaitingFormHelper.ShowWaitingForm();
                List<ReturnBillBookBillPasteStatusReportInfo> billPastInfo = _reportMgrService.FindAndDisplayPasteBillStatus(con);
                WaitingFormHelper.HideWaitingForm();
                bool printPreview = ((CheckInBillBookConditionInfoReport)e.Data).PrintPreview;

                if (billPastInfo.Count != 0)
                {
                    while (WorkItem.Items.Contains("ICAB03_01ReportView"))
                    {
                        WorkItem.Items.Remove(_CAB03_01ReportView);
                    }

                    _CAB03_01ReportView = WorkItem.Items.AddNew<CAB03_01ReportView>("ICAB03_01ReportView");

                    if (printPreview)
                    {
                        _CAB03_01ReportView.ShowReport(billPastInfo, printPreview);
                        PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                        info.Modal = true;
                        info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                        info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                        info.MaximizeBox = false;
                        info.MinimizeBox = false;
                        info.Title = "รายงานละเอียดลูกหนี้ที่เก็บเงินไม่ได้ (PA-CAB03_01)";

                        WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAB03_01ReportView, info);
                    }
                }
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "สร้างรายงาน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        [EventSubscription(EventTopicNames.ShowCAB03_03Report, Thread = ThreadOption.UserInterface)]
        public void LoadCAB03_03ReportHandler(object sender, EventArgs<CheckInBillBookConditionInfoReport> e)
        {
            try
            {
                CheckInBillBookConditionInfoReport con = (CheckInBillBookConditionInfoReport)e.Data;
                con.AbsId = AbsIdEnum.PAST;
                con.PmId = String.Empty;

                WaitingFormHelper.ShowWaitingForm();
                CAB03_03ReportInfo reportInfo = _reportMgrService.FindAndDisplayCAB03_03Report(con);
                WaitingFormHelper.HideWaitingForm();
                reportInfo.Header.PrintPreview = ((CheckInBillBookConditionInfoReport)e.Data).PrintPreview;

                if (reportInfo.Details.Count != 0)
                {
                    if (WorkItem.Items.Contains("ICAB03_03ReportView") == true)
                    {
                        WorkItem.Items.Remove(_CAB03_03ReportView);
                    }

                    _CAB03_03ReportView = WorkItem.Items.AddNew<CAB03_03ReportView>("ICAB03_03ReportView");

                    _CAB03_03ReportView.ShowReport(reportInfo.Header, reportInfo.Details);
                    PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                    info.Modal = true;
                    info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                    info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                    info.MaximizeBox = false;
                    info.MinimizeBox = false;
                    info.Title = "รายงานละเอียดลูกหนี้ที่เก็บเงินไม่ได้ (PA-CAB03_03)";

                    if (reportInfo.Header.PrintPreview)
                    {
                        WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAB03_03ReportView, info);
                    }
                }
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "สร้างรายงาน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        [EventSubscription(EventTopicNames.ShowCAB03_04Report, Thread = ThreadOption.UserInterface)]
        public void LoadCAB03_04ReportHandler(object sender, EventArgs<CheckInBillBookConditionInfoReport> e)
        {
            try
            {
                CheckInBillBookConditionInfoReport con = (CheckInBillBookConditionInfoReport)e.Data;
                con.AbsId = AbsIdEnum.COLLECTED;
                con.PmId = PmIdEnum.DOUBLE;

                WaitingFormHelper.ShowWaitingForm();
                CAB03_HeaderReportInfo header = _reportMgrService.FindAndDisplayCAB03_Header(e.Data);
                List<CAB03_DetailReportInfo> details = _reportMgrService.FindAndDisplayCAB03_Detail(e.Data);
                WaitingFormHelper.HideWaitingForm();

                header.PrintPreview = ((CheckInBillBookConditionInfoReport)e.Data).PrintPreview;

                if (details.Count != 0)
                {
                    if (WorkItem.Items.Contains("ICAB03_04ReportView") == true)
                    {
                        WorkItem.Items.Remove(_CAB03_04ReportView);
                    }

                    _CAB03_04ReportView = WorkItem.Items.AddNew<CAB03_04ReportView>("ICAB03_04ReportView");

                    _CAB03_04ReportView.ShowReport(header, details);

                    if (header.PrintPreview)
                    {
                        PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                        info.Modal = true;
                        info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                        info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                        info.MaximizeBox = false;
                        info.MinimizeBox = false;
                        info.Title = "รายงานละเอียดลูกหนี้ที่เก็บเงินไม่ได้ (PA-Rev_303_4)";
                        WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAB03_04ReportView, info);
                    }
                }
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "สร้างรายงาน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        //This Event use for Group Of Bill follow Line Of Agency By Status Of Bill
        //Create By Chettha Pattananitisak Date 23/04/2007 Time 11:25
        [EventSubscription(EventTopicNames.ShowCAB03_02Report, Thread = ThreadOption.UserInterface)]
        public void LoadCAB03_02ReportHandler(object sender, EventArgs<CheckInBillBookConditionInfoReport> e)
        {
            try
            {
                WaitingFormHelper.ShowWaitingForm();
                //Optimize webservice
                CAB03_02ReportInfo reportInfo = _reportMgrService.FindAndDisplayCAB03_02Report(e.Data);
                WaitingFormHelper.HideWaitingForm();

                bool printPreview = ((CheckInBillBookConditionInfoReport)e.Data).PrintPreview;

                if (WorkItem.Items.Contains("ICAB03_02ReportView"))
                {
                    WorkItem.Items.Remove(_CAB03_02ReportView);
                }

                _CAB03_02ReportView = WorkItem.Items.AddNew<CAB03_02ReportView>("ICAB03_02ReportView");

                _CAB03_02ReportView.ShowReport(reportInfo.Header, reportInfo.Detail, printPreview);

                if (printPreview)
                {

                    PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                    info.Modal = true;
                    info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                    info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                    info.MaximizeBox = false;
                    info.MinimizeBox = false;
                    info.Title = "รายงานละเอียดลูกหนี้ที่เก็บเงินไม่ได้ (PA-CAB03_02)";
                    WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAB03_02ReportView, info);
                }
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "สร้างรายงาน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        //This Event use for load Detail Of History Line Of Agency in Each Month
        //Create By Chettha Pattananitisak Date 24/04/2007 Time 15:10
        [EventSubscription(EventTopicNames.ShowCAB07_01Report, Thread = ThreadOption.UserInterface)]
        public void LoadCAB07_01ReportHandler(object sender, EventArgs<KeepMoneyPlaneOfAgencyConditionInfoReport> e)
        {
            try
            {
                KeepMoneyPlaneOfAgencyConditionInfoReport conn = (KeepMoneyPlaneOfAgencyConditionInfoReport)e.Data;
                KeepMoneyPlaneHeaderInfoReport myHeader = new KeepMoneyPlaneHeaderInfoReport();
                List<CAB07_01DetailReportInfo> myDetail = _reportMgrService.FindDetailOfDataIntoRev701460Report(conn);

                if (WorkItem.Items.Contains("ICAB07_01ReportView") == true)
                {
                    WorkItem.Items.Remove(_CAB07_01ReportView);
                }
                // get header info 
                myHeader.PrintDate = Session.BpmDateTime.Now.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                myHeader.BranchMasterId = conn.BaCode;
                myHeader.BranchMasterName = conn.PEAName;
                myHeader.Period = conn.Period;

                myHeader.PrintPreview = conn.PrintPreview;
                _CAB07_01ReportView = WorkItem.Items.AddNew<CAB07_01ReportView>("ICAB07_01ReportView");
                _CAB07_01ReportView.ShowReport(myHeader, myDetail);

                if (conn.PrintPreview)
                {
                    PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                    info.Modal = true;
                    info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                    info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                    info.MaximizeBox = false;
                    info.MinimizeBox = false;
                    info.Title = "รายงานรายละเอียดข้อมูลประวัติสายการเก็บเงิน (PA-CAB07_01)";
                    WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAB07_01ReportView, info);
                }
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "สร้างรายงาน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);

            }
        }

        //This Event use for show Detail Of Keep Money Plan Group by Branch and BillPeriod
        //Create By Chettha Pattananitisak Date 18/05/2007 Time 11:20
        [EventSubscription(EventTopicNames.ShowCAB08_01Report, Thread = ThreadOption.UserInterface)]
        public void LoadCAB08_01ReportHandler(object sender, EventArgs<KeepMoneyPlaneOfAgencyConditionInfoReport> e)
        {
            try
            {
                KeepMoneyPlaneOfAgencyConditionInfoReport conn = (KeepMoneyPlaneOfAgencyConditionInfoReport)e.Data;
                KeepMoneyPlaneHeaderInfoReport myHeader = new KeepMoneyPlaneHeaderInfoReport();
                List<CAB08_01DetailReportInfo> myDetail = _reportMgrService.FindDetailOfDataIntoRev701461Report(conn);
                List<CAB08_01AgencyInfo> myAgentList = _reportMgrService.FindAgentListIntoRev701461Report(conn);
                if (WorkItem.Items.Contains("ICAB08_01ReportView") == true)
                {
                    WorkItem.Items.Remove(_CAB08_01ReportView);
                }

                // get header info 
                myHeader.PrintDate = Session.BpmDateTime.Now.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                myHeader.BranchMasterId = conn.BaCode;
                myHeader.BranchMasterName = conn.PEAName;
                myHeader.Period = conn.Period;

                myHeader.PrintPreview = conn.PrintPreview;
                _CAB08_01ReportView = WorkItem.Items.AddNew<CAB08_01ReportView>("ICAB08_01ReportView");
                _CAB08_01ReportView.ShowReport(myHeader, myDetail, myAgentList);

                if (conn.PrintPreview)
                {
                    PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                    info.Modal = true;
                    info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                    info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                    info.MaximizeBox = false;
                    info.MinimizeBox = false;
                    info.Title = "รายงานรายละเอียดข้อมูลการเก็บเงินค่าไฟฟ้า แยกตามตัวแทน (PA-CAB08_01)";
                    WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAB08_01ReportView, info);
                }
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "สร้างรายงาน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        //This Event use for show Issue Of Keep Money Plan Group by Keep Money Of Period 
        //Create By Chettha Pattananitisak Date 21/05/2007 Time 17:00
        [EventSubscription(EventTopicNames.ShowCAB09_01Report, Thread = ThreadOption.UserInterface)]
        public void LoadCAB09_01ReportHandler(object sender, EventArgs<KeepMoneyPlaneOfAgencyConditionInfoReport> e)
        {
            try
            {
                KeepMoneyPlaneOfAgencyConditionInfoReport conn = (KeepMoneyPlaneOfAgencyConditionInfoReport)e.Data;
                WaitingFormHelper.ShowWaitingForm();
                KeepMoneyPlaneHeaderInfoReport myHeader = new KeepMoneyPlaneHeaderInfoReport();
                List<CAB09_01DetailReportInfo> myDetail = _reportMgrService.FindDetailOfDataIntoRev701462Report(conn);
                WaitingFormHelper.HideWaitingForm();

                if (WorkItem.Items.Contains("ICAB09_01ReportView") == true)
                {
                    WorkItem.Items.Remove(_CAB09_01ReportView);
                }
                // get header info 
                myHeader.PrintDate = Session.BpmDateTime.Now.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                myHeader.BranchMasterId = conn.BaCode;
                myHeader.BranchMasterName = conn.PEAName;
                myHeader.Period = conn.Period;
                myHeader.PrintPreview = conn.PrintPreview;

                _CAB09_01ReportView = WorkItem.Items.AddNew<CAB09_01ReportView>("ICAB09_01ReportView");
                _CAB09_01ReportView.ShowReport(myHeader, myDetail);

                if (conn.PrintPreview)
                {
                    PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                    info.Modal = true;
                    info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                    info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                    info.MaximizeBox = false;
                    info.MinimizeBox = false;
                    info.Title = "รายงานสรุปข้อมูลแผนการเก็บเงินค่าไฟฟ้า แยกตามงวด (PA-CAB09_01)";
                    WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAB09_01ReportView, info);
                }
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "ออกรายงาน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        //This Event use for show Issue Of Keep Money Plan Group by Keep Money Of Paid Date 
        //Create By Chettha Pattananitisak Date 22/05/2007 Time 15:00
        [EventSubscription(EventTopicNames.ShowCAB08_02Report, Thread = ThreadOption.UserInterface)]
        public void LoadCAB08_02ReportHandler(object sender, EventArgs<KeepMoneyPlaneOfAgencyConditionInfoReport> e)
        {
            try
            {
                KeepMoneyPlaneOfAgencyConditionInfoReport conn = (KeepMoneyPlaneOfAgencyConditionInfoReport)e.Data;

                WaitingFormHelper.ShowWaitingForm();
                KeepMoneyPlaneHeaderInfoReport myHeader = new KeepMoneyPlaneHeaderInfoReport();
                List<CAB08_02DetailReportInfo> myDetail = _reportMgrService.FindDetailOfDataIntoRev701463Report(conn);
                WaitingFormHelper.HideWaitingForm();
                if (WorkItem.Items.Contains("ICAB08_02ReportView") == true)
                {
                    WorkItem.Items.Remove(_CAB08_02ReportView);
                }
                // get header info 
                myHeader.PrintDate = Session.BpmDateTime.Now.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                myHeader.BranchMasterId = conn.BaCode;
                myHeader.BranchMasterName = conn.PEAName;
                myHeader.Period = conn.Period;
                myHeader.PrintPreview = conn.PrintPreview;

                _CAB08_02ReportView = WorkItem.Items.AddNew<CAB08_02ReportView>("ICAB08_02ReportView");

                _CAB08_02ReportView.ShowReport(myHeader, myDetail);
                if (myHeader.PrintPreview)
                {
                    PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                    info.Modal = true;
                    info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                    info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                    info.MaximizeBox = false;
                    info.MinimizeBox = false;
                    info.Title = "รายงานสรุปข้อมูลแผนการเก็บเงินค่าไฟฟ้า แยกตามวันที่จ่ายบิล (PA-CAB08_02)";
                    WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAB08_02ReportView, info);
                }
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "ออกรายงาน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        //This Event use for show Issue Period Of AR in Keep Money Plan  
        //Create By Chettha Pattananitisak Date 24/05/2007 Time 11:00
        [EventSubscription(EventTopicNames.ShowCAB10_01Report, Thread = ThreadOption.UserInterface)]
        public void LoadCAB10_01ReportHandler(object sender, EventArgs<KeepMoneyPlaneOfAgencyConditionInfoReport> e)
        {
            try
            {
                KeepMoneyPlaneOfAgencyConditionInfoReport conn = (KeepMoneyPlaneOfAgencyConditionInfoReport)e.Data;
                WaitingFormHelper.ShowWaitingForm();
                KeepMoneyPlaneHeaderInfoReport myHeader = new KeepMoneyPlaneHeaderInfoReport();
                List<CAB10_01DetailReportInfo> myDetail = _reportMgrService.FindDetailOfDataIntoRev701464Report(conn);
                WaitingFormHelper.HideWaitingForm();

                if (WorkItem.Items.Contains("ICAB10_01ReportView") == true)
                {
                    WorkItem.Items.Remove(_CAB10_01ReportView);
                }
                // get header info 
                myHeader.PrintDate = Session.BpmDateTime.Now.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                myHeader.BranchMasterId = conn.BaCode;
                myHeader.BranchMasterName = conn.PEAName;
                myHeader.Period = conn.Period;
                myHeader.PrintPreview = conn.PrintPreview;

                _CAB10_01ReportView = WorkItem.Items.AddNew<CAB10_01ReportView>("ICAB10_01ReportView");
                _CAB10_01ReportView.ShowReport(myHeader, myDetail);

                if (myHeader.PrintPreview)
                {
                    PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                    info.Modal = true;
                    info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                    info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                    info.MaximizeBox = false;
                    info.MinimizeBox = false;
                    info.Title = " รายงานอายุลูกหนี้สรุปตามแผนการเก็บเงินค่าไฟฟ้า ประจำเดือน (PA-CAB10_01)";
                    WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAB10_01ReportView, info);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.AGENCY, "ออกรายงาน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        [EventSubscription(EventTopicNames.ShowCAB12_01Report, Thread = ThreadOption.UserInterface)]
        public void LoadCAB12_01ReportHandler(object sender, EventArgs<CAB12_01ConditionReportInfo> e)
        {
            try
            {                
                    CAB12_01ConditionReportInfo conn = (CAB12_01ConditionReportInfo)e.Data;
                    CAB12_01HeaderReportInfo myHeader = new CAB12_01HeaderReportInfo();

                    List<CAB12_01DetailReportInfo> myDetail = _reportMgrService.FindAndDisplayCAB12_01Detail(conn);
                    List<CAB12_02DetailReportInfo> mySummary = _reportMgrService.FindAndDisplayCAB12_02Detail(conn);
                    myHeader = _reportMgrService.FindAndDisplayCAB12_01Header(myDetail, Session.Branch.Name, Session.Branch.Id);
                    if (WorkItem.Items.Contains("ICAB12_01ReportView") == true)
                    {
                        WorkItem.Items.Remove(_CAB12_01ReportView);
                    }

                    myHeader.PrintPreview = conn.PrintPreview;
                    _CAB12_01ReportView = WorkItem.Items.AddNew<CAB12_01ReportView>("ICAB12_01ReportView");
                    _CAB12_01ReportView.ShowReport(myHeader, myDetail, mySummary);

                    if (myHeader.PrintPreview)
                    {
                        PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                        info.Modal = true;
                        info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                        info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                        info.MaximizeBox = false;
                        info.MinimizeBox = false;
                        info.Title = " รายงานตรวจสอบประวัติผู้ใช้ไฟฟ้าประเภทฝากวางใบแจ้งหนี้ค่าไฟฟ้า";
                        WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAB12_01ReportView, info);
                    }               
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.AGENCY, "ออกรายงาน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        [EventSubscription(EventTopicNames.ShowCAN34_01Report, Thread = ThreadOption.UserInterface)]
        public void LoadCAN34_01ReportHandler(object sender, EventArgs<CAN34_01CondtionReportInfo> e)
        {
            try
            {
                CAN34_01CondtionReportInfo conn = (CAN34_01CondtionReportInfo)e.Data;
                CAN34_01HeaderReportInfo myHeader = new CAN34_01HeaderReportInfo();

                List<CAN34_01DetailReportInfo> myDetail = _reportMgrService.FindAndDisplayCAN34_01Detail(conn);

                myHeader.BranchName = String.Format("{0}, {1}", Session.Branch.Name, Session.Branch.Id);
                myHeader.PrintPreview = conn.PrintPreview;
                myHeader.Period = String.Format("{0} {1}", StringConvert.GetMonthName(Convert.ToInt32(conn.Period.Substring(4, 2))), conn.Period.Substring(0, 4));
                myHeader.PrintDate = Session.BpmDateTime.Now.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));

                if (WorkItem.Items.Contains("ICAN34_01ReportView") == true)
                {
                    WorkItem.Items.Remove(_CAN34_01ReportView);
                }

                _CAN34_01ReportView = WorkItem.Items.AddNew<CAN34_01ReportView>("ICAN34_01ReportView");
                _CAN34_01ReportView.ShowReport(myHeader, myDetail);

                if (myHeader.PrintPreview)
                {
                    PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                    info.Modal = true;
                    info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                    info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                    info.MaximizeBox = false;
                    info.MinimizeBox = false;
                    info.Title = "รายงานปรียบเทียบแผน ผลการจัดหน่วย พิมพ์บิลและเก็บเงินประจำเดือน (PA-CAN34_01)";
                    WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAN34_01ReportView, info);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.AGENCY, "ออกรายงาน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        [EventSubscription(EventTopicNames.ShowCAB13_01Report, Thread = ThreadOption.UserInterface)]
        public void LoadCAB13_01ReportHandler(object sender, EventArgs<CAB13_01ConditionRptInfo> e)
        {
            try
            {
                CAB13_01ConditionRptInfo conn = (CAB13_01ConditionRptInfo)e.Data;

                List<CAB13_01RptInfo> myDetail = _reportMgrService.GetRptCAB13_01RptInfo(conn);

                if (WorkItem.Items.Contains("ICAB13_01ReportView") == true)
                {
                    WorkItem.Items.Remove(_CAB13_01ReportView);
                }

                _CAB13_01ReportView = WorkItem.Items.AddNew<CAB13_01ReportView>("ICAB13_01ReportView");
                _CAB13_01ReportView.ShowReport(conn, myDetail);

                if (conn.PrintPreview)
                {
                    PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                    info.Modal = true;
                    info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                    info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                    info.MaximizeBox = false;
                    info.MinimizeBox = false;
                    info.Title = "สรุปรายงานประเมินผลการจัดเก็บค่าไฟฟ้าของตัวแทนเก็บเงินแต่ละเขต (PA-CAB13_01)";
                    WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAB13_01ReportView, info);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.AGENCY, "ออกรายงาน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        //This Event use for Evaluate about keep Money Of Agency in Keep Money Plan  
        //Create By Chettha Pattananitisak Date 25/05/2007 Time 11:00
        [EventSubscription(EventTopicNames.ShowCAB06_01Report, Thread = ThreadOption.UserInterface)]
        public void LoadCAB06_01ReportHandler(object sender, EventArgs<EvaluateAgencyReportCondition> e)
        {
            try
            {
                EvaluateAgencyReportCondition conn = (EvaluateAgencyReportCondition)e.Data;
                WaitingFormHelper.ShowWaitingForm();
                List<CAB06_01DetailReportInfo> myDetail = _reportMgrService.FindAndDisplayCAB06_01Detail(conn);
                CAB06_01HeaderReportInfo myHeader = _reportMgrService.FindAndDisplayCAB06_01Header(conn, myDetail);
                myHeader.BranchName = String.Format("{0}, {1}", Session.Branch.Name, Session.Branch.Id);
                WaitingFormHelper.HideWaitingForm();

                if (WorkItem.Items.Contains("ICAB06_01ReportView") == true)
                {
                    WorkItem.Items.Remove(_CAB06_01ReportView);
                }

                myHeader.PrintPreview = conn.PrintPreview;
                _CAB06_01ReportView = WorkItem.Items.AddNew<CAB06_01ReportView>("ICAB06_01ReportView");
                _CAB06_01ReportView.ShowReport(myHeader, myDetail);
                if (myHeader.PrintPreview)
                {
                    PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                    info.Modal = true;
                    info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                    info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                    info.MaximizeBox = false;
                    info.MinimizeBox = false;
                    info.Title = " รายงานการประเมินผลการจัดเก็บค่าไฟฟ้าของตัวแทนเก็บเงิน (PA-CAB06_01)";
                    WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAB06_01ReportView, info);
                }
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "ออกรายงาน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        //Create By Chettha Pattananitisak Date 03/04/2007
        //Use Reprint for Report of Return BillBook
        [EventSubscription(EventTopicNames.ReprintReportOfReturnBillBook, Thread = ThreadOption.UserInterface)]
        public void ReprintReportOfReturnBillBookHandler(object sender, EventArgs<CheckInBillBookConditionInfoReport> e)
        {
            try
            {
                if (Authorization.IsAuthorized(SecurityNames.ReprintCheckInReport, "Reprint billbook ID: " + e.Data.BillBookId, true))
                {
                    List<ReturnBillBookBillPasteStatusReportInfo> billPastInfo = _reportMgrService.FindAndDisplayPasteBillStatus(e.Data);
                    CAB03_HeaderReportInfo header = _reportMgrService.FindAndDisplayCAB03_Header(e.Data);
                    CAB03_02DetailReportInfo myDeatil = _reportMgrService.FindDisplayIssueBillARInfoDetail(e.Data);

                    bool printPreview = ((CheckInBillBookConditionInfoReport)e.Data).PrintPreview;
                    // Call View of The First Report
                    if (billPastInfo.Count != 0)
                    {
                        if (WorkItem.Items.Contains("ICustomerPaidNotPassAgencyReportView") == true)
                        {
                            WorkItem.Items.Remove(_CAB03_01ReportView);
                        }

                        _CAB03_01ReportView = WorkItem.Items.AddNew<CAB03_01ReportView>("ICustomerPaidNotPassAgencyReportView");

                        _CAB03_01ReportView.ShowReport(billPastInfo, printPreview);
                        WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAB03_01ReportView, _modalProperty);
                    }

                    // Call View of The Second Report
                    if (WorkItem.Items.Contains("ICAB03_02ReportView") == true)
                    {
                        WorkItem.Items.Remove(_CAB03_02ReportView);
                    }

                    _CAB03_02ReportView = WorkItem.Items.AddNew<CAB03_02ReportView>("ICAB03_02ReportView");
                    _CAB03_02ReportView.ShowReport(header, myDeatil, printPreview);
                    if (printPreview)
                    {
                        PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                        info.Modal = true;
                        info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                        info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                        info.MaximizeBox = false;
                        info.MinimizeBox = false;
                        WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAB03_02ReportView, info);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.AGENCY, "ออกรายงาน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        [EventSubscription(EventTopicNames.LoadBillBookPrintDisplay, Thread = ThreadOption.UserInterface)]
        public void LoadBillBookPrintDisplayHandler(object sender, EventArgs<int> e)
        {
            int keyword = e.Data;
            //_reportMgtService.LoadBillBookMasterDataByAgencyID(keyword, LoadBillBookPrintInformationCallback);
        }

        //show popup window for AgencyMoneyReturnverification condition
        [EventSubscription(EventTopicNames.ShowAgencyMoneyReturnVerificationPopup, Thread = ThreadOption.UserInterface)]
        public void ShowAgencyMoneyVerificationConditionPopUpHandler(object sender, EventArgs e)
        {
            if (WorkItem.Items.Contains("IReportAgentDeliveryPopupView"))
            {
                WorkItem.Items.Remove(_reportAgentMoneyReturnVerificationPopupView);
            }

            _reportAgentMoneyReturnVerificationPopupView = WorkItem.Items.AddNew<ReportAgentMoneyReturnVerificationPopupView>("IReportAgentDeliveryPopupView");

            PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
            info.Modal = true;
            info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
            info.MaximizeBox = false;
            info.MinimizeBox = false;
            info.Title = "รายงานตรวจสอบการนำส่งเงินของตัวแทนเก็บเงิน";
            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_reportAgentMoneyReturnVerificationPopupView, info);
        }


        [EventSubscription(EventTopicNames.ShowCAN34_01ReportPopup, Thread = ThreadOption.UserInterface)]
        public void ShowCAN34_01ReportPopUpHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.CAN34_01Report, "CAN 34 Report ", true))
            {
                if (WorkItem.Items.Contains("ICAN34_01ReportPopupView"))
                {
                    WorkItem.Items.Remove(_CAN34_01ReportPopupView);
                }

                _CAN34_01ReportPopupView = WorkItem.Items.AddNew<CAN34_01ReportPopupView>("ICAN34_01ReportPopupView");


                PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                info.Modal = true;
                info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                info.MaximizeBox = false;
                info.MinimizeBox = false;
                info.Title = "รายงานเปรียบเทียบแผน ผลการจดหน่วย พิมพ์บิล และเก็บเงินประจำเดือน";

                WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAN34_01ReportPopupView, info);
                //((UserControl)_reportAgentMoneyReturnVerificationPopupView).ParentForm.Text = "รายงานตรวจสอบการนำส่งเงินของตัวแทนเก็บเงิน";
            }
        }

        [EventSubscription(EventTopicNames.ShowCAB05_01ReportPopup, Thread = ThreadOption.UserInterface)]
        public void ShowCAB05_01ReportPopUpHandler(object sender, EventArgs e)
        {

            if (Authorization.IsAuthorized(SecurityNames.CAB05_01Report, "CAB05 01Report", true))
            {
                if (WorkItem.Items.Contains("ICAB05_01ReportPopupView"))
                {
                    WorkItem.Items.Remove(_CAB05_01ReportPopupView);
                }

                _CAB05_01ReportPopupView = WorkItem.Items.AddNew<CAB05_01ReportPopupView>("ICAB05_01ReportPopupView");


                PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                info.Modal = true;
                info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                info.MaximizeBox = false;
                info.MinimizeBox = false;
                info.Title = "รายงานการเก็บเงินของตัวแทนเก็บเงิน";
                WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAB05_01ReportPopupView, info);
            }
        }

        [EventSubscription(EventTopicNames.ShowCAB12_01ReportPopup, Thread = ThreadOption.UserInterface)]
        public void ShowCAB12_01ReportPopUpHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.CAB12_01Report, "CAB12 01Report", true))
            {
                if (WorkItem.Items.Contains("ICAB12_01ReportPopupView"))
                {
                    WorkItem.Items.Remove(_CAB12_01ReportPopupView);
                }

                _CAB12_01ReportPopupView = WorkItem.Items.AddNew<CAB12_01ReportPopupView>("ICAB12_01ReportPopupView");

                PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                info.Modal = true;
                info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                info.MaximizeBox = false;
                info.MinimizeBox = false;
                info.Title = "รายงานตรวจสอบประวัติผู้ใช้ไฟฟ้าประเภทฝากวางใบแจ้งหนี้ค่าไฟฟ้า";
                WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAB12_01ReportPopupView, info);
            }
        }

        [EventSubscription(EventTopicNames.ShowCAB13_01ReportPopup, Thread = ThreadOption.UserInterface)]
        public void ShowCAB13_01ReportPopUpHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.CAB13_01Report, "CAB13 01Report", true))
            {
                if (WorkItem.Items.Contains("ICAB13_01ReportPopupView"))
                {
                    WorkItem.Items.Remove(_CAB13_01ReportPopupView);
                }

                _CAB13_01ReportPopupView = WorkItem.Items.AddNew<CAB13_01ReportPopupView>("ICAB13_01ReportPopupView");

                PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                info.Modal = true;
                info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                info.MaximizeBox = false;
                info.MinimizeBox = false;
                info.Title = "สรุปรายงานประเมินผลการจัดเก็บค่าไฟฟ้าของตัวแทนเก็บเงินแต่ละเขต";
                WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAB13_01ReportPopupView, info);
            }
        }

        [EventSubscription(EventTopicNames.ShowPA_7034ReportPopup, Thread = ThreadOption.UserInterface)]
        public void ShowPA_7034ReportPopUpHandler(object sender, EventArgs e)
        {

            if (Authorization.IsAuthorized(SecurityNames.PA_7034Report, "PA 7034Report", true))
            {
                if (WorkItem.Items.Contains("IPA_7034ReportPopupView"))
                {
                    WorkItem.Items.Remove(_PA_7034ReportPopupView);
                }

                _PA_7034ReportPopupView = WorkItem.Items.AddNew<PA_7034ReportPopupView>("IPA_7034ReportPopupView");

                PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                info.Modal = true;
                info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                info.MaximizeBox = false;
                info.MinimizeBox = false;
                info.Title = "รายงานผลการเก็บเงินประจำเดือน ";

                WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_PA_7034ReportPopupView, info);
            }
        }

        //show report AgencyMoneyReturnVerification;
        [EventSubscription(EventTopicNames.ShowCAB02_01Report, Thread = ThreadOption.UserInterface)]
        public void LoadCAB02_01ReportHandler(object sender, EventArgs<ReportParameterInfo> e)
        {
            try  
            {
                ReportParameterInfo p = e.Data as ReportParameterInfo;

                if (WorkItem.Items.Contains("ICAB02_01ReportView"))
                {
                    WorkItem.Items.Remove(_CAB02_01ReportView);
                }

                _CAB02_01ReportView = WorkItem.Items.AddNew<CAB02_01ReportView>("ICAB02_01ReportView");
                CAB02_HederReportInfo data = this.GetAgencyMoneyReturnList(p);
                _CAB02_01ReportView.ShowReport(data);
                if (p.PrintPreview)
                {
                    PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                    info.Modal = true;
                    info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                    info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                    info.MaximizeBox = true;
                    info.MinimizeBox = true;
                    info.Title = "รายงานตรวจสอบการนำส่งของเงินของตัวแทนเก็บเงิน (PA-CAB02_01)";
                    WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAB02_01ReportView, info);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.AGENCY, "ออกรายงาน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        [EventSubscription(EventTopicNames.AgencySearchReportPopUp, Thread = ThreadOption.UserInterface)]
        public void AgencySearchReportPopUpHandler(object sender, EventArgs<int> e)
        {
            try
            {
                if (WorkItem.State["IAgentSearchReportPopupView"] != null)
                {
                    if (WorkItem.Items.Contains("IAgentSearchReportPopupView"))
                    {
                        Object tmp = WorkItem.Items["IAgentSearchReportPopupView"];
                        WorkItem.Items.Remove(tmp);
                    }
                    _agentSearchReportPopupView = WorkItem.Items.AddNew<AgentSearchReportPopupView>("IAgentSearchReportPopupView");
                    _agentSearchReportPopupView.SendType = (int)e.Data;

                    PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                    info.Modal = true;
                    info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                    info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                    info.MaximizeBox = false;
                    info.MinimizeBox = false;
                    info.Title = "ค้นหารหัสตัวแทน";
                    WorkItem.State["IAgentSearchReportPopupView"] = null;
                    WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_agentSearchReportPopupView, _modalProperty);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.AGENCY, "ออกรายงาน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        //serach box
        [EventSubscription(EventTopicNames.AgentSearchButton, Thread = ThreadOption.UserInterface)]
        public void AgentSearchFindButtonHandler(object sender, EventArgs<AgentSearchInfo> e)
        {
            _agentSearchReportPopupView.AgentSearchResult = _agencyPlaningMgrService.AcquireAgentAssetSearchInformation(e.Data);
        }
        //CAB05
        [EventSubscription(EventTopicNames.ShowCAB05_01Report, Thread = ThreadOption.UserInterface)]
        public void LoadCAB05_01ReportHandler(object sender, EventArgs<CAB05_01ConditionReportInfo> e)
        {

            try
            {
                CAB05_01ConditionReportInfo conn = (CAB05_01ConditionReportInfo)e.Data;
                CAB05_01HeaderReportInfo myHeader = new CAB05_01HeaderReportInfo();
                WaitingFormHelper.ShowWaitingForm();
                List<CAB05_01DetailReportInfo> myDetail = _reportMgrService.FindAndDisplayCAB05_01Detail(conn);
                WaitingFormHelper.HideWaitingForm();

                myHeader.BranchName = String.Format("{0}, {1}", Session.Branch.Name, Session.Branch.Id);
                myHeader.PrintDate = Session.BpmDateTime.Now.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                if (WorkItem.Items.Contains("ICAB05_01ReportView") == true)
                {
                    WorkItem.Items.Remove(_CAB05_01ReportView);
                }

                myHeader.PrintPreview = conn.PrintPreview;
                _CAB05_01ReportView = WorkItem.Items.AddNew<CAB05_01ReportView>("ICAB05_01ReportView");
                _CAB05_01ReportView.ShowReport(myHeader, myDetail);
                if (myHeader.PrintPreview)
                {
                    PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                    info.Modal = true;
                    info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                    info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                    info.MaximizeBox = false;
                    info.MinimizeBox = false;
                    info.Title = " รายงานการเก็บเงินของตัวแทน ประจำเดือน (PA-CAB05_01)";
                    WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAB05_01ReportView, info);
                }
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "ออกรายงาน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        //PA_7034
        [EventSubscription(EventTopicNames.ShowPA_7034Report, Thread = ThreadOption.UserInterface)]
        public void LoadPA_7304ReportHandler(object sender, EventArgs<PA_7034ConditionReportInfo> e)
        {
            try
            {
                PA_7034ConditionReportInfo conn = (PA_7034ConditionReportInfo)e.Data;
                PA_7034HeaderReportInfo myHeader = new PA_7034HeaderReportInfo();
                WaitingFormHelper.ShowWaitingForm();
                List<PA_7034DetailReportInfo> myDetail = _reportMgrService.FindAndDisplayPA_7034Detail(conn);
                WaitingFormHelper.HideWaitingForm();
                string billPeriod = conn.Period;

                myHeader.Period = String.Format(" {0} {1}", StringConvert.GetMonthName(Convert.ToInt32(billPeriod.Substring(4, 2))), billPeriod.Substring(0, 4));
                myHeader.BranchName = String.Format("{0}, {1}", Session.Branch.Name, Session.Branch.Id);
                myHeader.PrintDate = Session.BpmDateTime.Now.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                if (WorkItem.Items.Contains("IPA_7034ReportView") == true)
                {
                    WorkItem.Items.Remove(_PA_7034ReportView);
                }

                myHeader.PrintPreview = conn.PrintPreview;
                _PA_7034ReportView = WorkItem.Items.AddNew<PA_7034ReportView>("IPA_7034ReportView");
                _PA_7034ReportView.ShowReport(myHeader, myDetail);
                if (myHeader.PrintPreview)
                {
                    PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
                    info.Modal = true;
                    info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                    info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
                    info.MaximizeBox = false;
                    info.MinimizeBox = false;
                    info.Title = "ผลการจัดเก็บเงิน ประจำเดือน (PA-PA_7034)";
                    WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_PA_7034ReportView, info);
                }
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "ออกรายงาน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        #endregion

        #region "Helper"
        private void LoadBillBookPrintInformationCallback(bool success, List<BillBookInfoMasterReport> BillBookInfoMaster)
        {
            _CAB01_01ReportView.ResultBillBookPrintMasterDisplay = BillBookInfoMaster;
        }
        private BillBookInfoMasterReport GetBillBookMasterReport(BillBookHeaderInfo bookHeader)
        {
            BillBookInfoMasterReport reportInfo = new BillBookInfoMasterReport();

            reportInfo = _reportMgrService.GetBillBookInfoReport(bookHeader);
            reportInfo.BranchName = String.Format("{0}, {1}", _moduleController.RunningBranchName, _moduleController.RunningBranchId);
            //TODO : Change user name replace by create by in billbooktable
            reportInfo.CreatorName = reportInfo.CreatorName;
            return reportInfo;
        }
        private List<BillBookInfoDetailReport> GetBillBookDetailReport(BillBookHeaderInfo bookHeader)
        {
            List<BillBookInfoDetailReport> _detailList = new List<BillBookInfoDetailReport>();
            _detailList = _reportMgrService.GetBillBookInfoDetailReport(bookHeader.BillBookId);
            return _detailList;
        }

        private CAB02_HederReportInfo GetAgencyMoneyReturnList(ReportParameterInfo p)
        {
            string[] pr = p.ParameterValue as string[];

            CAB02_HederReportInfo reportItem = new CAB02_HederReportInfo();
            reportItem.PrintDate = Session.BpmDateTime.Now.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
            reportItem.BranchName = String.Format("{0}, {1}", Session.Branch.Name, Session.Branch.BACode);
            reportItem.AgencyIdFrom = pr[0];
            reportItem.AgencyIdTo = pr[1];
            reportItem.PeriodFrom = pr[2];
            reportItem.PeriodTo = pr[3];
            List<CAB02_DetailReportInfo> dataList = _reportMgrService.GetAgencyMoneyReturnReport(pr[0], pr[1], pr[2], pr[3], pr[4]);
            reportItem.PrintPreview = p.PrintPreview;
            reportItem.DataList = dataList;
            return reportItem;

        }

        #endregion
    }
}

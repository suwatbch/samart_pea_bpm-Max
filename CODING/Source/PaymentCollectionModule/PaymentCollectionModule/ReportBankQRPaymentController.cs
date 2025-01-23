using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Library.UI;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.Infrastructure.Interface.Constants;
using PEA.BPM.PaymentCollectionModule.ReportViews.CAC19CriteriaView;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface.Services;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using PEA.BPM.Architecture.CommonUtilities;
using Microsoft.Reporting.WinForms;
using System.Globalization;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureTool;
using PEA.BPM.PaymentCollectionModule.ReportViews.ResultViewCenter;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;

namespace PEA.BPM.PaymentCollectionModule
{
    public class ReportBankQRPaymentController : WorkItemController
    {
        private ILayoutService _layoutService;
        private WindowSmartPartInfo _modalProperty;
        private ICAC19CriteriaView _CAC19View;
        private IReportService _reportService;
        private IReportContainerView _sView;

        [InjectionConstructor]
        public ReportBankQRPaymentController([ServiceDependency] ILayoutService layoutService, IReportService reportService)
        {
            _layoutService = layoutService;
            _reportService = reportService;

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

            if (WorkItem.Items.Contains("ICAC19CriteriaView"))
                WorkItem.Items.Remove(_CAC19View);

            SetWindowsTitle(Properties.Resources.RpCAC19);
            _CAC19View = WorkItem.Items.AddNew<CAC19CriteriaView>("ICAC19CriteriaView");
            _modalProperty.Title = Properties.Resources.RpCAC19;
            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_CAC19View, _modalProperty);

            ShellWaitCursor(false);
        }

        [EventPublication(EventTopicNames.ShellWaitCursor, PublicationScope.Global)]
        public event EventHandler<EventArgs<bool>> ShellWaitCursorHandler;
        public void ShellWaitCursor(bool wait)
        {
            if (ShellWaitCursorHandler != null)
                ShellWaitCursorHandler(this, new EventArgs<bool>(wait));
        }

        [EventSubscription(EventTopicNames.ShowReportCAC19Click, Thread = ThreadOption.UserInterface)]
        public void CAC19ReportHandler(object sender, EventArgs<ReportParam> e)
        {

            try
            {
                // Get data from report service class.
                CAC19Param param = (CAC19Param)e.Data;

                RPCAC19_Process rpCAC19 = new RPCAC19_Process();
                rpCAC19.Process(param, _reportService);

                CAC19DisplayByDataProcess(rpCAC19, param);

                // Disable for 
                //CAC19DisplayReport(param);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //ClientExceptionController.ShowExceptionDialog(EErrorInModule.POS, ex);
            }
        }

        private void CAC19DisplayByDataProcess(RPCAC19_Process rpCAC19, CAC19Param param)
        {
            ReportParameter[] rParam = new ReportParameter[3];

            DateTime fromDate = param.TransFromDate.Value;
            param.TransToDate = param.TransToDate == null ? param.TransFromDate.Value : param.TransToDate.Value;
            DateTime toDate = param.TransToDate.Value;

            string strSearchBy = "";
            if (param.BranchId != null)
            {
                strSearchBy = "รหัส กฟฟ.:" + param.BranchId;
            }
            if (param.ControllerId != null)
            {
                strSearchBy += " / รหัสผู้รับเงิน:" + param.ControllerId;
            }
            strSearchBy += GetPaymentDate(fromDate, toDate)[1];
            rParam[0] = new ReportParameter("parTransDate", GetPaymentDate(fromDate, toDate)[0]);

            Branch branch = CodeTable.Instant.ListBranches(param.BranchId);
            rParam[1] = new ReportParameter("parBranchDetail", branch.BranchName + " : " + branch.BranchId);
            rParam[2] = new ReportParameter("parSearchBy", "*** ค้นหาโดย " + strSearchBy + " ***");

            if (WorkItem.Items.Contains("IReportContainerView"))
                WorkItem.Items.Remove(_sView);

            _sView = WorkItem.Items.AddNew<ReportContainerView>("IReportContainerView");
            SetWindowsTitle(Properties.Resources.RpCAC19);
            _sView.SetLabel = Properties.Resources.RpCAC19;

            _sView.Preview(rpCAC19.reportCAC19Data, rpCAC19.ReportSummaryDatas, param.TransFromDate.Value, param.TransToDate.Value);
            WorkItem.Workspaces[WorkspaceNames.CenterWorkspace].Show(_sView);

        }

      

        private void CAC19DisplayReport(CAC19Param param)
        {
            try
            {
                string qrNotCompleteText = "รายการที่แตกต่างกับรายงานธนาคาร";
                string qrCompleteText = "รายการที่ตรงกับรายงานธนาคาร";

                DateTime fromDate = param.TransFromDate.Value;
                param.TransToDate = param.TransToDate == null ? param.TransFromDate.Value : param.TransToDate.Value;
                DateTime toDate = param.TransToDate.Value;

                // Convert data string to object. 
                List<CAC19QRPaymentReport> bankQRPayments = new List<CAC19QRPaymentReport>();

                List<CAC19QRPaymentReport> bankQRConvertList = new List<CAC19QRPaymentReport>();
                bankQRConvertList = param.BankQRPayment;

                // จัดการเรื่อง type Credit/Debit. 
                bankQRPayments = (from t in bankQRConvertList
                                  group t by new { t.BranchID, t.QRRef1Bank, t.QRRef2Bank }
                                      into g
                                      select new CAC19QRPaymentReport()
                                     {
                                         BranchID = g.Key.BranchID,
                                         QRRef1Bank = g.Key.QRRef1Bank,
                                         QRRef2Bank = g.Key.QRRef2Bank,
                                         GAmountBank = g.Sum(c => c.GAmountBank)
                                     }).Where(c => c.GAmountBank > 0).ToList();

                // Result value before get data from Service.
                param.BankQRPayment = new List<CAC19QRPaymentReport>();
                List<CAC19Report> reportCAC19Data = _reportService.GetReportCAC19(param);

                // Process ข้อมูล Summary 
                List<CAC19SummaryReport> summaryData = ProcessGroupByBPM(reportCAC19Data, bankQRPayments);

                // Group ข้อมูล bpm payment QR
                var bpmPaymentGroup = (from p in reportCAC19Data
                                       select new
                                       {
                                           Ref1 = p.QRRef1.Trim(),
                                           Ref2 = p.QRRef2.Trim(),
                                           GAmount = p.GAmount
                                       }).ToList();


                // Group ข้อมูล bank payment QR
                var bankPaymentGroup = (from p in bankQRPayments
                                        select new
                                        {
                                            Ref1 = p.QRRef1Bank.Trim(),
                                            Ref2 = p.QRRef2Bank.Trim(),
                                            GAmount = p.GAmountBank
                                        }).ToList();

                // Cross check from BPM side  to Bank QR
                var bpmJoinBankQR = (from b in reportCAC19Data
                                     join p in bankPaymentGroup
                                     on new { Ref1 = b.QRRef1.Trim(), Ref2 = b.QRRef2.Trim(), GAmount = b.GAmount }
                                     equals new { Ref1 = p.Ref1.Trim(), Ref2 = p.Ref2.Trim(), GAmount = p.GAmount }
                                     select new
                                     {
                                         Ref1 = b.QRRef1.Trim(),
                                         Ref2 = b.QRRef2.Trim(),
                                         GAmount = b.GAmount,
                                         CashierId = b.CashierId,
                                         b.ControllerName
                                     }).ToList();

                // Cross check from bank side to 

                if (bpmPaymentGroup.Count() == bpmJoinBankQR.Count() && bpmPaymentGroup.Count() == bankPaymentGroup.Count())
                {
                    // ข้อมูล ตรงกัน message box แจ้ง user และจบกระบวนการ.
                    //MessageBox.Show("ข้อมูลการชำระเงินถูกต้อง  ตรงกับข้อมูลของทางธนาคาร", "ตรวจสอบการชำระเงินกับข้อมูลชำระเงินของธนาคาร", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Count join data. 
                    if (bpmJoinBankQR.Count > 0)
                    {
                        // Join data remove from reportCAC19Data report.  
                        foreach (var joinItem in bpmJoinBankQR)
                        {
                            // Remove from reportCAC19Data
                            var reportData = reportCAC19Data.FirstOrDefault(c => c.QRRef1.Trim() == joinItem.Ref1.Trim() &&
                                c.QRRef2.Trim() == joinItem.Ref2.Trim() && c.GAmount == joinItem.GAmount);
                            if (reportData != null)
                                reportCAC19Data.Remove(reportData);

                            // Remove from text file data. 
                            var bankData = bankQRPayments.FirstOrDefault(c => c.QRRef1Bank.Trim() == joinItem.Ref1.Trim() &&
                                c.QRRef2Bank.Trim() == joinItem.Ref2.Trim() && c.GAmountBank == joinItem.GAmount);
                            if (bankData != null)
                                bankQRPayments.Remove(bankData);
                        }

                        // Summarize data group by CashierId.
                        var joinDataByCashierId = (from b in bpmJoinBankQR
                                                   group b by b.CashierId into g
                                                   orderby g.Key
                                                   select new
                                                   {
                                                       CashierId = g.Key,
                                                       RowCount = g.Count(),
                                                       SumGAmount = g.Sum(c => c.GAmount),
                                                       ControllerName = g.FirstOrDefault().ControllerName
                                                   }).ToList();
                    }

                    // 1. BPM Side :  หาข้อมูล BPM ซึ่งไม่มีข้อมูลจากธนาคาร
                    var bpmLeft = (from b in reportCAC19Data
                                   join p in bankQRPayments
                                   on new { Ref1 = b.QRRef1.Trim(), Ref2 = b.QRRef2.Trim(), GAmount = b.GAmount }
                                   equals new { Ref1 = p.QRRef1Bank.Trim(), Ref2 = p.QRRef2Bank.Trim(), GAmount = p.GAmountBank }
                                   into g
                                   from sub in g.DefaultIfEmpty()
                                   where !string.IsNullOrEmpty(b.QRRef1)
                                   select new
                                   {
                                       Ref1 = b.QRRef1,
                                       Ref2 = b.QRRef2,
                                       GAmount = b.GAmount
                                   }).ToList();

                    // ซ่อมข้อมูลโดย ค้นหาด้วย Ref1, Ref2 จากข้อมูลธนาคาร 
                    foreach (var bpmPay in bpmLeft)
                    {
                        // Get data from bank QRPayment. 
                        var bankPay = (from b in bankQRPayments
                                       where b.QRRef1Bank.Trim() == bpmPay.Ref1 && b.QRRef2Bank.Trim() == bpmPay.Ref2
                                       select b).FirstOrDefault();

                        // Set record ในรายงานเป็นรายการที่ไม่ completed.
                        var reportRecord = reportCAC19Data.FirstOrDefault(c => c.QRRef1 == bpmPay.Ref1 && c.QRRef2 == bpmPay.Ref2);
                        if (reportRecord != null)
                        {
                            reportRecord.QRCompleted = qrNotCompleteText;
                            reportRecord.Record_Qty_New = 1;
                        }

                        if (bankPay != null)
                        {
                            // บันทึกข้อมูลในชุดข้อมูล CAC19Report
                            // เจอข้อมูลในธนาคาร แต่ Amount ไม่ตรง 
                            if (reportRecord != null)
                            {
                                reportRecord.QRRef1Bank = bankPay.QRRef1Bank;
                                reportRecord.QRRef2Bank = bankPay.QRRef2Bank;
                                reportRecord.GAmountBank = bankPay.GAmountBank;
                                reportRecord.QRCompleted = qrNotCompleteText;
                                reportRecord.Note = "จำนวนเงิน";
                            }
                        }
                        else
                        {
                            reportRecord.Note = "ไม่มีใน ธนาคาร";
                        }
                    }

                    // 2. Bank QR Side. 
                    var bankQRLeft = from bk in bankQRPayments
                                     join bpm in reportCAC19Data
                                     on new { Ref1 = bk.QRRef1Bank.Trim(), Ref2 = bk.QRRef2Bank.Trim(), GAmount = bk.GAmountBank }
                                     equals new { Ref1 = bpm.QRRef1.Trim(), Ref2 = bpm.QRRef2.Trim(), GAmount = bpm.GAmount }
                                     into g
                                     from sub in g.DefaultIfEmpty()
                                     select new
                                     {
                                         Ref1 = bk.QRRef1Bank.Trim(),
                                         Ref2 = bk.QRRef2Bank.Trim(),
                                         GAmount = bk.GAmountBank.Value
                                     };

                    // Insert to CAC19 Report
                    foreach (var bkPay in bankQRLeft)
                    {
                        CAC19Report data = new CAC19Report();
                        data.QRRef1Bank = bkPay.Ref1;
                        data.QRRef2Bank = bkPay.Ref2;
                        data.GAmountBank = bkPay.GAmount;
                        data.PaymentDt = null;
                        data.QRCompleted = qrNotCompleteText;
                        data.Record_Qty_New = 1;
                        data.QRRef1 = string.Empty;
                        data.QRRef2 = string.Empty;
                        data.Note = "ไม่มีใน BPM";
                        var dataByRef1 = reportCAC19Data.FirstOrDefault(c => c.QRRef1.Trim() == bkPay.Ref1.Trim());
                        if (dataByRef1 != null)
                            data.ControllerName = dataByRef1.ControllerName;
                        else
                            data.ControllerName = string.Empty;

                        reportCAC19Data.Add(data);
                    }
                }


                ReportParameter[] rParam = new ReportParameter[3];

                string strSearchBy = "";
                if (param.BranchId != null)
                {
                    strSearchBy = "รหัส กฟฟ.:" + param.BranchId;
                }
                if (param.ControllerId != null)
                {
                    strSearchBy += " / รหัสผู้รับเงิน:" + param.ControllerId;
                }
                strSearchBy += GetPaymentDate(fromDate, toDate)[1];
                rParam[0] = new ReportParameter("parTransDate", GetPaymentDate(fromDate, toDate)[0]);

                Branch branch = CodeTable.Instant.ListBranches(param.BranchId);
                rParam[1] = new ReportParameter("parBranchDetail", branch.BranchName + " : " + branch.BranchId);
                rParam[2] = new ReportParameter("parSearchBy", "*** ค้นหาโดย " + strSearchBy + " ***");

                if (bpmPaymentGroup.Count() == bpmJoinBankQR.Count() && bpmPaymentGroup.Count() == bankPaymentGroup.Count())
                {
                    // ข้อมูล ตรงกัน message box แจ้ง user และจบกระบวนการ.
                    //MessageBox.Show("ข้อมูลการชำระเงินถูกต้อง  ตรงกับข้อมูลของทางธนาคาร", "ตรวจสอบการชำระเงินกับข้อมูลชำระเงินของธนาคาร", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reportCAC19Data.Clear();
                }

                if (WorkItem.Items.Contains("IReportContainerView"))
                    WorkItem.Items.Remove(_sView);

                _sView = WorkItem.Items.AddNew<ReportContainerView>("IReportContainerView");
                SetWindowsTitle(Properties.Resources.RpCAC19);
                _sView.SetLabel = Properties.Resources.RpCAC19;

                _sView.Preview(reportCAC19Data, summaryData, param.TransFromDate.Value, param.TransToDate.Value);
                WorkItem.Workspaces[WorkspaceNames.CenterWorkspace].Show(_sView);
                bankQRConvertList.Clear();
                bankQRPayments.Clear();


            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<CAC19SummaryReport> ProcessGroupByBPM(List<CAC19Report> reportCAC19Data, List<CAC19QRPaymentReport> qrPaymentFromText)
        {
            int iRowCount = 0;
            List<CAC19SummaryReport> result = new List<CAC19SummaryReport>();

            result = (from t in reportCAC19Data
                      group t by t.CashierId into g
                      select new CAC19SummaryReport()
                      {
                          CashierId = g.Key,
                          ControllerName = g.FirstOrDefault().ControllerName,
                          BPMCount = g.Count(),
                          BPMTotGAmount = g.Sum(e => e.GAmount.Value),
                          BankCount = 0,
                          BankTotGAmount = 0
                      }).ToList();

            var datas = (from q in qrPaymentFromText
                         group q by q.QRRef1Bank.Trim().Substring(6, 8) into g
                         select new
                         {
                             CashierId = g.Key,
                             BankCount = g.Count(),
                             BankGAmount = g.Sum(e => e.GAmountBank.Value)
                         }).ToList();

            foreach (var bankData in datas)
            {
                // Assign bank summary data to summary data. 
                var sumObject = result.FirstOrDefault(e => e.CashierId == bankData.CashierId);
                if (sumObject == null)
                {
                    // Create new object add to summary. 
                    CAC19SummaryReport sumBankleft = new CAC19SummaryReport();
                    sumBankleft.CashierId = bankData.CashierId;
                    sumBankleft.BankCount = bankData.BankCount;
                    sumBankleft.BankTotGAmount = bankData.BankGAmount;
                    sumBankleft.Note = "ไม่พบรายการใน BPM";
                    result.Add(sumBankleft);
                }
                else
                {
                    sumObject.BankTotGAmount += bankData.BankGAmount;
                }

            }

            foreach (var detail in result.OrderBy(e => e.CashierId).ThenBy(e => e.ControllerName))
            {
                iRowCount += 1;
                detail.RowNumber = iRowCount;

                if (detail.BankCount != detail.BPMCount || detail.BankTotGAmount != detail.BPMTotGAmount)
                    detail.Note = "พบรายการที่แตกต่าง";
            }

            return result;
        }

        private List<string> GetPaymentDate(DateTime fromDate, DateTime toDate)
        {
            List<string> strResult = new List<string>();

            if (fromDate.ToString("yyyy") == toDate.ToString("yyyy"))
            {
                if (fromDate.ToString("MM") == toDate.ToString("MM"))
                {
                    if (fromDate.ToString("dd") == toDate.ToString("dd"))
                    {
                        strResult.Add(fromDate.ToString("d MMMM yyyy", new CultureInfo("th-TH")));
                        strResult.Add(" / วันที่รับชำระ: " + fromDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    }
                    else
                    {
                        strResult.Add(fromDate.Day.ToString() + " - " + toDate.ToString("d MMMM yyyy", new CultureInfo("th-TH")));
                        strResult.Add(" / วันที่รับชำระ: " + fromDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) + " - " + toDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    }
                }
                else
                {
                    strResult.Add(fromDate.ToString("d MMMM", new CultureInfo("th-TH")) + " - " + toDate.ToString("d MMMM yyyy", new CultureInfo("th-TH")));
                    strResult.Add(" / วันที่รับชำระ: " + fromDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) + " - " + toDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                }
            }
            else
            {
                strResult.Add(fromDate.ToString("d MMMM yyyy", new CultureInfo("th-TH")) + " - " + toDate.ToString("d MMMM yyyy", new CultureInfo("th-TH")));
                strResult.Add(" / วันที่รับชำระ: " + fromDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) + " - " + toDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
            }

            return strResult;
        }

    }
}

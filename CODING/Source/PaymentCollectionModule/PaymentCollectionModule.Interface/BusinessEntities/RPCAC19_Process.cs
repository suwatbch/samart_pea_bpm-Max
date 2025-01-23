using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports;
using PEA.BPM.PaymentCollectionModule.Interface.Services;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    public class RPCAC19_Process
    {
        private IReportService _reportService;

        private const string qrNotCompleteText = "รายการที่แตกต่างกับรายงานธนาคาร";
        private const string qrCompleteText = "รายการที่ตรงกับรายงานธนาคาร";

        public List<string> CashierIdBPMDiff { get; set; }
        public List<string> CashierIdBankDiff { get; set; }
        public List<string> CashierIdDiff_All { get; set; }
        public List<string> CashierId_All { get; set; }

        public List<CAC19SummaryReport> ReportSummaryDatas { get; set; }

        public List<string> CashierId_Completed { get; set; }

        /// <summary>
        /// ข้อมูลการชำระจากธนาคาร  จาก Text file (Original data)
        /// </summary>
        public List<CAC19QRPaymentReport> bankQRPayments_Org { get; set; }


        /// <summary>
        /// ข้อมูลการชำระจากธนาคาร ไม่รวม Cashier ที่ข้อมูลชำระมีความถูกต้อง
        /// </summary>
        public List<CAC19QRPaymentReport> bankQRPayments { get; set; }


        /// <summary>
        /// เก็บข้อมูลการชำระเงิน BPM (Original data)
        /// </summary>
        public List<CAC19Report> reportCAC19Data_Org { get; set; }


        /// <summary>
        /// เก็บข้อมูลการชำระเงิน BPM ไม่รวมข้อมูลการชำระเงินของ Cashier 
        /// </summary>
        public List<CAC19Report> reportCAC19Data { get; set; }


        /// <summary>
        /// ข้อมูลการชำระเงิน BPM , Group by Ref1,Ref2
        /// </summary>
        List<PaymentGroup> BPMPaymentGroup { get; set; }

        /// <summary>
        /// ข้อมูล QR Payment จากธนาคาร , Group by Ref1,Ref2
        /// </summary>
        List<PaymentGroup> BankPaymentGroup { get; set; }

        /// <summary>
        /// ข้อมูลการชำระ BPM จับคู่กับธนาคาร (Left join)
        /// </summary>
        List<PaymentLeftJoin> BPMPaymentLeftJoin { get; set; }

        /// <summary>
        /// ข้อมูลธนาคาร จับคู่กับ ข้อมูลชำระเงิน BPM (Left join)
        /// </summary>
        List<PaymentLeftJoin> BankPaymentLeftJoin { get; set; }

        public RPCAC19_Process()
        {
            bankQRPayments_Org = new List<CAC19QRPaymentReport>();

            CashierIdBPMDiff = new List<string>();
            CashierIdBankDiff = new List<string>();
            CashierIdDiff_All = new List<string>();
            CashierId_Completed = new List<string>();
            CashierId_All = new List<string>();

            reportCAC19Data_Org = new List<CAC19Report>();
            reportCAC19Data = new List<CAC19Report>();

            ReportSummaryDatas = new List<CAC19SummaryReport>();

            BPMPaymentGroup = new List<PaymentGroup>();
            BankPaymentGroup = new List<PaymentGroup>();

            BPMPaymentLeftJoin = new List<PaymentLeftJoin>();
            BankPaymentLeftJoin = new List<PaymentLeftJoin>();

        }


        public void Process(CAC19Param param, IReportService reportService)
        {
            _reportService = reportService;

            //RPCAC19_Process dataProcess = new RPCAC19_Process();

            DateTime fromDate = param.TransFromDate.Value;
            param.TransToDate = param.TransToDate == null ? param.TransFromDate.Value : param.TransToDate.Value;
            DateTime toDate = param.TransToDate.Value;


            // ข้อมูลการชำระจากธนาคาร
            List<CAC19QRPaymentReport> bankQRConvertList = new List<CAC19QRPaymentReport>();
            bankQRPayments_Org = param.BankQRPayment;

            // Reset  value before get data from Service.
            param.BankQRPayment = new List<CAC19QRPaymentReport>();


            // ข้อมูลการชำระ BPM.
            reportCAC19Data_Org = _reportService.GetReportCAC19(param);

            // คำนวณ BPM, Bank payment Group by Re1,Re2
            GroupPaymentOrg();

            //1. ตรวจสอบจาก ข้อมูลการชำระเงิน BPM จับคู่กับ ข้อมูลธนาคาร (BPM left join bank)
            //+  1.1 เก็บข้อมูล CashierId ของข้อมูลที่จับคู่ไม่ได้ Distinct  --> CashierIdDiff List<string>
            CrossCheckFromBPMSide_GetCashierIdLeft();

            //2. ตรวจสอบจาก ข้อมูลธนาคาร จับคู่กับ ข้อมูล BPM  (Bank left join BPM)
            //+ 2.1 เก็บข้อมูล CashierId ของข้อมูลที่จับคู่ไม่ได้  Distinct --> CashierIdDiff List<string>
            CrossCheckFromBankSide_GetCashierIdLeft();

            // 3. CashierIDDiff_All เก็บข้อมูล CashierId จากทั้ง 2 แหล่ง 
            GetCashierIDDiff_All();

            // 4. CashierId ทั้งหมด (CashierIDAll List<string>) , CashierId On BPM , CashierId On Bank
            GetCashierID_ALL();

            // 5. ค้นหาข้อมูล CashierId ที่ข้อมูลไม่มีปัญหา (CashierIdAll left join CashierIdDiff_All เฉพาะรายการที่จับคู่ไม่ได้)  --> CashierId_Completed List<string>
            GetCashierId_Completed_Data();

            //6. ข้อมูลการชำระเงิน BPM ให้ Query ข้อมูล Exclude Cashier_Completed.  (เพื่อนำไปแสดงในส่วนรายละเอียด) 
            BPMPaymentReport_Exclude_CashierCompleted();

            //7. ข้อมูลจากธนาคาร ให้ Query ข้อมูล Exclude Cashier_Completed.
            BankQRPayment_Excldue_CashierCompleted();

            // 8. นำข้อมูล  ข้อ 6,7 มาแสดงในส่วนที่ 1 ของรายงาน 

            //8.0 รายการ report ที่ ref1, ref2 match กันต้อง update ข้อมูล Bank. 
            Process_BPMMathcBank();


            // Add bank diff.
            Process_AddBankDiff_ToReport();

            // 8.1 Process Note session 1. 
            Process_Note_session1();

            // 9. Process summary session report. 
            ProcessSummarySession();

            // 9.X Calculate bank side.
            ProcessSummary_BankSide();

            // 9.4 Summary RunningAndNoteProcess
            ProcessSummaryRunningAndNote();

        }

        private void Process_BPMMathcBank()
        {
            try
            {
                var dataMatch = (from t in BPMPaymentLeftJoin where t.BankRef1 != null select t).ToList();
                foreach (var item in dataMatch)
                {
                    var obj = reportCAC19Data.FirstOrDefault(e => e.QRRef1.Trim() == item.BPMRef1.Trim() && e.QRRef2.Trim() == item.BPMRef2);
                    if (obj != null)
                    {
                        obj.QRRef1Bank = item.BankRef1;
                        obj.QRRef2Bank = item.BankRef2;
                        obj.GAmountBank = item.BankGAmount;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void ProcessSummaryRunningAndNote()
        {
            int iCount = 0;
            foreach (var item in this.ReportSummaryDatas.OrderBy(e => e.CashierId).ToList())
            {
                iCount += 1;
                item.RowNumber = iCount;

                // Compare รายการ และจำนวนเงิน
                if (item.BankCount != item.BPMCount || item.BankTotGAmount != item.BPMTotGAmount)
                    item.Note = "พบรายการที่แตกต่าง";

                var cashierDiff = this.CashierIdDiff_All.FirstOrDefault(e => e == item.CashierId);
                if (cashierDiff != null)
                {
                    if (string.IsNullOrEmpty(item.Note))
                        item.Note = "พบรายการที่แตกต่าง";
                }

            }
        }

        private void Process_Note_session1()
        {
            foreach (var item in this.reportCAC19Data)
            {
                if (item.QRRef1Bank == null)
                    item.Note = "ไม่มีในธนาคาร";
            }
        }

        private void ProcessSummary_BankSide()
        {
            try
            {
                //คำนวณข้อมูล Bank QR Payment transacton 
                foreach (var item in this.ReportSummaryDatas)
                {
                    // ข้อมูล QR Payment. 
                    var qrPaymentGroupDatas = this.BankPaymentGroup.Where(e => e.CashierId == item.CashierId).ToList();
                    if (qrPaymentGroupDatas != null)
                    {
                        item.BankCount = qrPaymentGroupDatas.Count;
                        item.BankTotGAmount = qrPaymentGroupDatas.Sum(e => e.GAmount);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void ProcessSummarySession()
        {
            try
            {
                // Prepare summary from BPM side. 
                // ข้อมูล BPM Side จัดรูปแบบ
                var bpmSideSummary = (from t in reportCAC19Data_Org
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


                // Process by cashierId All
                foreach (var _cashierId in this.CashierId_All)
                {
                    // Check data from bpm side
                    var bpmSum = bpmSideSummary.FirstOrDefault(e => e.CashierId == _cashierId);
                    if (bpmSum != null)
                    {
                        this.ReportSummaryDatas.Add(bpmSum);
                    }
                    else
                    {
                        // Create new. 
                        // Create new object add to summary. 
                        CAC19SummaryReport sumBankleft = new CAC19SummaryReport();
                        sumBankleft.CashierId = _cashierId;
                        //sumBankleft.BankCount = bankData.BankCount;
                        //sumBankleft.BankTotGAmount = bankData.BankGAmount;
                        sumBankleft.Note = "ไม่พบรายการใน BPM";
                        this.ReportSummaryDatas.Add(sumBankleft);
                    }




                }

                // คำนวณ Bank Payment ลงใน  Summary bank side. 
                //BankPaymentGroup 




            }
            catch (Exception)
            {
            }
        }

        private void Process_AddBankDiff_ToReport()
        {
            //8. นำข้อมูล  ข้อ 6,7 มาแสดงในส่วนที่ 1 ของรายงาน 
            try
            {

                //this.bankQRPayments 
                //foreach (var bankPay in this.bankQRPayments)
                var bankNoBpm = (from t in this.BankPaymentLeftJoin where t.BPMRef1 == null select t).ToList();

                foreach (var bankPay in bankNoBpm)
                {
                    CAC19Report objBPMPay;
                    // Get BPM Payment by Ref1, Ref2, GAmount. 
                    objBPMPay = this.reportCAC19Data.FirstOrDefault(
                        e => e.QRRef1.Trim() == bankPay.BankRef1.Trim() &&
                        e.QRRef2.Trim() == bankPay.BankRef2.Trim() &&
                        e.GAmount == bankPay.BankGAmount);


                    if (objBPMPay == null)
                    {
                        objBPMPay = this.reportCAC19Data.FirstOrDefault(
                       e => e.QRRef1.Trim() == bankPay.BankRef1.Trim() &&
                       e.QRRef2.Trim() == bankPay.BankRef2.Trim());
                    }

                    // Get cashier name from bpm side. 
                    var cashier = reportCAC19Data_Org.FirstOrDefault(e => e.CashierId == bankPay.BankRef1.Substring(6, 8));


                    if (objBPMPay != null)
                    {
                        // Update bank qr payment 
                        objBPMPay.QRRef1Bank = bankPay.BankRef1;
                        objBPMPay.QRRef2Bank = bankPay.BankRef2;
                        objBPMPay.GAmountBank = bankPay.BankGAmount;

                        if (objBPMPay.GAmount != objBPMPay.GAmountBank)
                            objBPMPay.Note = "จำนวนเงิน";
                    }
                    else
                    {
                        // Add new 

                        CAC19Report data = new CAC19Report();
                        data.CashierId = bankPay.BankRef1.Substring(6, 8);
                        data.ControllerName = cashier != null ? cashier.ControllerName : " "; // Add space.
                        data.QRRef1Bank = bankPay.BankRef1;
                        data.QRRef2Bank = bankPay.BankRef2;
                        data.GAmountBank = bankPay.BankGAmount;
                        data.PaymentDt = null;
                        data.QRCompleted = null;
                        data.Record_Qty_New = 1;
                        data.QRRef1 = string.Empty;
                        data.QRRef2 = string.Empty;
                        data.Note = "ไม่มีใน BPM";

                        reportCAC19Data.Add(data);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void BankQRPayment_Excldue_CashierCompleted()
        {
            try
            {
                //7. ข้อมูลจากธนาคาร ให้ Query ข้อมูล Exclude Cashier_Completed.
                var datas = this.bankQRPayments_Org.Where(p => this.CashierId_Completed.All(p2 => p2 != p.QRRef1Bank.Substring(6, 8)));
                this.bankQRPayments = datas.ToList();
            }
            catch (Exception)
            {
            }
        }

        private void BPMPaymentReport_Exclude_CashierCompleted()
        {
            try
            {
                //var datas = this.reportCAC19Data_Org.Where(p => this.CashierId_Completed.All(p2 => p2 != p.CashierId));
                //this.reportCAC19Data = datas.ToList();

                this.reportCAC19Data = reportCAC19Data_Org.ToList();
            }
            catch (Exception)
            {
            }
        }

        private void GetCashierId_Completed_Data()
        {
            try
            {
                // 5. ค้นหาข้อมูล CashierId ที่ข้อมูลไม่มีปัญหา (CashierIdAll left join CashierIdDiff_All เฉพาะรายการที่จับคู่ไม่ได้)  --> CashierId_Completed List<string>
                var dataJoinLeft = from a in this.CashierId_All
                                   join d in this.CashierIdDiff_All on a equals d
                                       into g
                                   from sub in g.DefaultIfEmpty()
                                   select new
                                   {
                                       CashierIdAll = a,
                                       CashierIdDiff = sub
                                   };

                this.CashierId_Completed = (from t in dataJoinLeft where t.CashierIdDiff == null select t.CashierIdAll).Distinct().ToList();
            }
            catch (Exception)
            {
            }

        }

        private void GetCashierID_ALL()
        {
            try
            {
                var cashierAll = (from b in this.reportCAC19Data_Org select b.CashierId).Union
                    (from bank in this.bankQRPayments_Org select bank.QRRef1Bank.Substring(6, 8));

                this.CashierId_All = cashierAll.ToList();
            }
            catch (Exception)
            {
            }
        }

        private void GetCashierIDDiff_All()
        {
            var cashierData = (from t0 in this.CashierIdBPMDiff select t0).Union
                (from t1 in this.CashierIdBankDiff select t1);

            this.CashierIdDiff_All = cashierData.ToList();
        }

        private void CrossCheckFromBankSide_GetCashierIdLeft()
        {
            //this.BankPaymentLeftJoin = (from bk in bankQRPayments_Org
            //                            join bpm in reportCAC19Data_Org
            //                            on new { Ref1 = bk.QRRef1Bank.Trim(), Ref2 = bk.QRRef2Bank.Trim(), GAmount = bk.GAmountBank }
            //                            equals new { Ref1 = bpm.QRRef1.Trim(), Ref2 = bpm.QRRef2.Trim(), GAmount = bpm.GAmount }
            //                            into g
            //                            from sub in g.DefaultIfEmpty()
            //                            select new PaymentLeftJoin
            //                            {
            //                                BankRef1 = bk.QRRef1Bank.Trim(),
            //                                BankRef2 = bk.QRRef2Bank.Trim(),
            //                                BankGAmount = bk.GAmountBank.Value,
            //                                BPMRef1 =  sub!= null ? sub.QRRef1 : null,
            //                                BPMRef2 =  sub!= null ? sub.QRRef2 : null,
            //                                BPMGAmount = sub!= null ? sub.GAmount.Value : 0
            //                            }).ToList();

            this.BankPaymentLeftJoin = (from bk in BankPaymentGroup
                                        join bpm in reportCAC19Data_Org
                                        on new { Ref1 = bk.Ref1.Trim(), Ref2 = bk.Ref2.Trim(), GAmount = bk.GAmount }
                                        equals new { Ref1 = bpm.QRRef1.Trim(), Ref2 = bpm.QRRef2.Trim(), GAmount = bpm.GAmount.Value }
                                        into g
                                        from sub in g.DefaultIfEmpty()
                                        select new PaymentLeftJoin
                                        {
                                            BankRef1 = bk.Ref1.Trim(),
                                            BankRef2 = bk.Ref2.Trim(),
                                            BankGAmount = bk.GAmount,
                                            BPMRef1 = sub != null ? sub.QRRef1 : null,
                                            BPMRef2 = sub != null ? sub.QRRef2 : null,
                                            BPMGAmount = sub != null ? sub.GAmount.Value : 0
                                        }).ToList();

            //+  1.1 เก็บข้อมูล CashierId ของข้อมูลที่จับคู่ไม่ได้ Distinct  --> CashierIdDiff List<string>
            this.CashierIdBankDiff = (from t in this.BankPaymentLeftJoin where t.BPMRef1 == null select t.BankRef1.Substring(6, 8)).Distinct().ToList();


        }

        private void CrossCheckFromBPMSide_GetCashierIdLeft()
        {
            try
            {
                //this.BPMPaymentLeftJoin = (from b in reportCAC19Data_Org
                //                           join p in bankQRPayments_Org
                //                           on new { Ref1 = b.QRRef1.Trim(), Ref2 = b.QRRef2.Trim(), GAmount = b.GAmount }
                //                           equals new { Ref1 = p.QRRef1Bank.Trim(), Ref2 = p.QRRef2Bank.Trim(), GAmount = p.GAmountBank }
                //                           into g
                //                           from sub in g.DefaultIfEmpty()
                //                           select new PaymentLeftJoin
                //                           {
                //                               BPMRef1 = b.QRRef1,
                //                               BPMRef2 = b.QRRef2,
                //                               BPMGAmount = b.GAmount.Value,
                //                               BankRef1 = sub != null ? sub.QRRef1Bank : null,
                //                               BankRef2 = sub != null ? sub.QRRef2Bank : null,
                //                               BankGAmount = sub != null ? sub.GAmountBank.Value :0
                //                           }).ToList();



                //this.BankPaymentGroup
                this.BPMPaymentLeftJoin = (from b in reportCAC19Data_Org
                                           join p in BankPaymentGroup
                                           on new { Ref1 = b.QRRef1.Trim(), Ref2 = b.QRRef2.Trim(), GAmount = b.GAmount.Value }
                                           equals new { Ref1 = p.Ref1.Trim(), Ref2 = p.Ref2.Trim(), GAmount = p.GAmount }
                                           into g
                                           from sub in g.DefaultIfEmpty()
                                           select new PaymentLeftJoin
                                           {
                                               BPMRef1 = b.QRRef1,
                                               BPMRef2 = b.QRRef2,
                                               BPMGAmount = b.GAmount.Value,
                                               BankRef1 = sub != null ? sub.Ref1 : null,
                                               BankRef2 = sub != null ? sub.Ref2 : null,
                                               BankGAmount = sub != null ? sub.GAmount : 0
                                           }).ToList();



                // + 2.1 เก็บข้อมูล CashierId ของข้อมูลที่จับคู่ไม่ได้  Distinct --> CashierIdDiff List<string>
                // CashierIdBPMDiff = new List<string>();
                this.CashierIdBPMDiff = (from t in this.BPMPaymentLeftJoin where t.BankRef1 == null select t.BPMRef1.Substring(6, 8)).Distinct().ToList();
            }
            catch (Exception)
            {

                throw;
            }



        }

        /// <summary>
        /// Group by Ref1,Ref2  
        /// สำหรับข้อมูลชำระ BPM และ Bank QR Payment. 
        /// </summary>
        private void GroupPaymentOrg()
        {

            // BPM payment group by ref1,ref2
            this.BPMPaymentGroup = (from p in this.reportCAC19Data_Org
                                    select new PaymentGroup
                                    {
                                        CashierId = p.QRRef1.Substring(6, 8),
                                        Ref1 = p.QRRef1.Trim(),
                                        Ref2 = p.QRRef2.Trim(),
                                        GAmount = p.GAmount.Value
                                    }).ToList();

            // Bank qr payment group by ref1,ref2
            // Old
            //this.BankPaymentGroup = (from p in this.bankQRPayments_Org
            //                         select new PaymentGroup
            //                         {
            //                             CashierId = p.QRRef1Bank.Substring(6, 8),
            //                             Ref1 = p.QRRef1Bank.Trim(),
            //                             Ref2 = p.QRRef2Bank.Trim(),
            //                             GAmount = p.GAmountBank.Value
            //                         }).Where( e => e.GAmount > 0).ToList();

            this.BankPaymentGroup = (from t in this.bankQRPayments_Org
                                     group t by new { t.BranchID, t.QRRef1Bank, t.QRRef2Bank }
                                         into g
                                         select new PaymentGroup()
                                         {
                                             CashierId = g.Key.QRRef1Bank.Substring(6, 8),
                                             Ref1 = g.Key.QRRef1Bank.Trim(),
                                             Ref2 = g.Key.QRRef2Bank.Trim(),
                                             GAmount = g.Sum(c => c.GAmountBank.Value)
                                         }).Where(e => e.GAmount > 0).ToList();


        }

    }

    #region Sub class for process LINQ

    public class PaymentGroup
    {
        public string CashierId { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public decimal GAmount { get; set; }
    }

    public class PaymentLeftJoin
    {
        public string CashierId { get; set; }
        public string BPMRef1 { get; set; }
        public string BPMRef2 { get; set; }
        public decimal BPMGAmount { get; set; }

        public string BankRef1 { get; set; }
        public string BankRef2 { get; set; }
        public decimal BankGAmount { get; set; }

    }

    #endregion
}

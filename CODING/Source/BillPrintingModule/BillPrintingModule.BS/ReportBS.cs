using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;
using PEA.BPM.BillPrintingModule.DA;
using PEA.BPM.BillPrintingModule.Interface.Services;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace PEA.BPM.BillPrintingModule.BS
{
    public class ReportBS : IReportServices
    {
        public List<ReportDailyPrint> GetDailyPrintReport(ReportConditionParam param)
        {
            ReportDA da = new ReportDA();
            List<ReportDailyPrint> dpAll = new List<ReportDailyPrint>();
            try
            {
                if (param.PrintingCondition == (int)PrintingCondition.AllPrinting)
                {
                    dpAll.AddRange(da.GetDailyPrintReport(param.PrintDate, null, null, param.PrintBranch, param.PrintType));
                }
                else if (param.PrintingCondition == (int)PrintingCondition.BranchPrinting)
                {
                    foreach (InputParam b in param.InputParamList)
                        dpAll.AddRange(da.GetDailyPrintReport(param.PrintDate, b.InputStr, b.GroupBranch, param.PrintBranch, param.PrintType));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dpAll;
        }

        public List<ReportDailyUnprint> GetDailyUnprintReport(ReportConditionParam param)
        {
            ReportDA da = new ReportDA();
            List<ReportDailyUnprint> duAll = new List<ReportDailyUnprint>();
            try
            {
                if (param.PrintingCondition == (int)PrintingCondition.AllPrinting)  //print all
                {
                    duAll.AddRange(da.GetDailyUnprintReport(param.BillPeriod, param.PortionNo, null, param.PrintBranch));
                }
                else
                {
                    foreach (InputParam ip in param.InputParamList)
                        duAll.AddRange(da.GetDailyUnprintReport(param.BillPeriod, param.PortionNo, ip.InputStr, param.PrintBranch));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return duAll;
        }

        public List<ReportBillDelivery> GetBillDeliveryReport(ReportConditionParam param)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                try
                {
                    ReportDA da = new ReportDA();
                    List<ReportBillDelivery> dpAll = new List<ReportBillDelivery>();

                    foreach (InputParam p in param.InputParamList)
                    {
                        dpAll.AddRange(da.GetBillDeliveryReport(trans, param.ReportType, param.BillPeriod, param.BillPeriodLog, p.InputStr, param.PrintBranch,
                            param.DeliveryPlace, param.PrintedBy, p.PrintType, param.Save, param.IsReprint));
                    }

                    trans.Commit();
                    return dpAll;
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public List<ReportStreetRoute> GetStreetRouteReport(ReportConditionParam param)
        {
            ReportDA da = new ReportDA();
            List<ReportStreetRoute> srAll = new List<ReportStreetRoute>();
            try
            {
                if (param.PrintingCondition == (int)PrintingCondition.AllPrinting)
                {
                    srAll.AddRange(da.GetStreetRouteReport(param.BillPeriod, null, param.PrintBranch, param.PortionNo));
                }
                else
                {
                    foreach (InputParam id in param.InputParamList)
                        srAll.AddRange(da.GetStreetRouteReport(param.BillPeriod, id.InputStr, param.PrintBranch, param.PortionNo));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return srAll;
        }

        public List<ReportStreetRouteReceive> GetStreetRouteReceiveReport(ReportConditionParam param)
        {
            ReportDA da = new ReportDA();
            List<ReportStreetRouteReceive> srrAll = new List<ReportStreetRouteReceive>();
            try
            {
                if (param.PrintingCondition == (int)PrintingCondition.AllPrinting)
                {
                    srrAll.AddRange(da.GetStreetRouteReceiveReport(param, null));
                }
                else
                {
                    foreach (InputParam id in param.InputParamList)
                        srrAll.AddRange(da.GetStreetRouteReceiveReport(param, id.InputStr));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return srrAll;
        }

        //obsolete
        public List<ReportStreetRouteUnreceive> GetStreetRouteUnreceiveReport(ReportConditionParam param)
        {
            throw new Exception("This report is no longer provided.");
            //ReportDA da = new ReportDA();
            //List<ReportStreetRouteUnreceive> suAll = new List<ReportStreetRouteUnreceive>();
            //try
            //{
            //    if (param.PrintingCondition == "AllPrinting")
            //    {
            //        suAll.AddRange(da.GetStreetRouteUnreceiveReport(param.BillPeriod, param.PortionNo, null, param.PrintBranch));
            //    }
            //    else
            //    {
            //        foreach (InputParam id in param.InputParamList)
            //            suAll.AddRange(da.GetStreetRouteUnreceiveReport(param.BillPeriod, param.PortionNo, id.InputStr, param.PrintBranch));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            //return suAll;
        }

        public List<ReportF16> GetF16Report(ReportConditionParam param)
        {
            ReportDA da = new ReportDA();
            List<ReportF16> f16All = new List<ReportF16>();//store all records in one and return kub

            char[] charSeperator = new char[] { '-' };
            string[] tmpId = new string[3];

            try
            {
                foreach (InputParam b in param.InputParamList)
                {
                    if (param.PrintingCondition == (int)PrintingCondition.BranchPrinting)
                        f16All.AddRange(da.GetF16Report(param.BillPeriod, b.InputStr, null, null, param.PrintBranch));
                    else if (param.PrintingCondition == (int)PrintingCondition.MruPrinting)
                    {
                        string branchId = null;
                        string mruId = null;
                        string toMruId = null;

                        tmpId = b.InputStr.Split(charSeperator, StringSplitOptions.RemoveEmptyEntries);
                        if (tmpId[0] != null)
                            branchId = tmpId[0];
                        if (tmpId[1] != null)
                        {
                            mruId = tmpId[1];
                            toMruId = tmpId[1];
                        }
                        //if user key mruId in form of range, our elecIdArray must have length = 3
                        //BranchId - MruId - ToMruId;after split(),length = 3
                        if (tmpId.Length == 3)
                            if (tmpId[2] != null)
                                toMruId = tmpId[2];

                        f16All.AddRange(da.GetF16Report(param.BillPeriod, branchId, mruId, toMruId, param.PrintBranch));
                        // ----------------- grouping, moved from database ------------------//
                    }

                    foreach (ReportF16 f16 in f16All)
                    {
                        f16.CollectType = "9"; //default as การไฟฟ้าเก็บเงินเอง

                        if (f16.W_1960_acct_class.Substring(2, 1) == "B" || f16.W_1990_addition == "X" || f16.W_2000_dispctrl == "A4")
                        {
                            f16.CollectType = "0";
                            f16.Note = "หนังสือแจ้ง";
                        }

                        if (f16.W_1980_spotbill == "" && f16.W_1960_acct_class.Substring(2, 1) == "S" && f16.W_1830_payment_method == "C")
                        {
                            f16.CollectType = "1";
                            f16.Note = "แจ้งหนี้ spot bill ไม่ได้";
                        }

                        if (f16.W_1830_payment_method == "D" || f16.W_1830_payment_method == "E") //ธนาคาร
                        {
                            f16.CollectType = "2";
                            f16.Note = f16.W_1570_account_number;
                        }

                        if (f16.W_1830_payment_method == "F") //ธนาคารฟ้า
                        {
                            f16.CollectType = "3";
                            f16.Note = "หักผ่านธนาคาร(บิลฟ้า)";
                        }

                        if (f16.W_1830_payment_method == "B" && f16.W_1980_spotbill == "")
                        {
                            f16.CollectType = "4";
                            f16.Note = "ตัวแทนเก็บเงิน";
                        }

                        if (f16.W_171_ettat_code == "2125T")  //ทศท.
                        {
                            f16.CollectType = "5";
                            f16.Note = "ทศท.";
                        }

                        if (f16.W_171_ettat_code.Length == 4 && f16.W_171_ettat_code.Substring(0, 1) == "6")
                        {
                            f16.CollectType = "6";
                            f16.Note = "ไฟราชการ";
                        }

                        if (f16.W_171_ettat_code == "6124S")
                        {
                            f16.CollectType = "7";
                            f16.Note = "ไฟสาธารณะ";
                        }

                        if (f16.W_1861_crsg == "Z00000")
                        {
                            f16.CollectType = "8";
                            f16.Note = "ชำระเงินสำนักงานใหญ่";
                        }

                        if (f16.W_1910_org_doc != "")
                            f16.Note = f16.Note + "**";

                        //f16All.AddRange(f16List);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return f16All;
        }

        public List<PrintableId> GetBranchForBillDeliveryReport(ReportConditionParam param)
        {
            ReportDA da = new ReportDA();
            List<PrintableId> ptAll = new List<PrintableId>();
            try
            {
                ptAll.AddRange(da.GetBranchForBillDeliveryReport(param));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ptAll;
        }

        public List<ReportGrpInvSummary> GetGrpInvSummaryReport(ReportConditionParam param)
        {
            ReportDA da = new ReportDA();
            List<ReportGrpInvSummary> reportItem = new List<ReportGrpInvSummary>();
            try
            {
                //print all branch in printbranch
                if (param.PrintingCondition == (int)PrintingCondition.AllPrinting)
                {
                    reportItem.AddRange(da.GetGrpInvSummaryReport(param.PrintingCondition.Value, param.BillPeriod, param.PrintBranch, null, null));
                }
                else if (param.PrintingCondition == (int)PrintingCondition.MtNoPrinting)
                {
                    foreach (InputParam b in param.InputParamList)
                        reportItem.AddRange(da.GetGrpInvSummaryReport(param.PrintingCondition.Value, param.BillPeriod, param.PrintBranch, b.InputStr, null));
                }
                else if (param.PrintingCondition == (int)PrintingCondition.PaidByPrinting)
                {
                    foreach (InputParam b in param.InputParamList)
                        reportItem.AddRange(da.GetGrpInvSummaryReport(param.PrintingCondition.Value, param.BillPeriod, param.PrintBranch, null, b.InputStr));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return reportItem;
        }

        public List<ReportPrintByBank> GetPrintGreenByBankReport(ReportConditionParam param)
        {
            ReportDA da = new ReportDA();
            List<ReportPrintByBank> reportItem = new List<ReportPrintByBank>();
            try
            {
                if (param.ReportType == 0)  //direct debit blue
                {
                    if (param.PrintingCondition == (int)PrintingCondition.AllPrinting)
                        reportItem.AddRange(da.GetPrintGreenByBankReport(param.PrintBranch, param.FromDate, param.ToDate, null, 4));
                    else
                    {
                        foreach (InputParam input in param.InputParamList)
                            reportItem.AddRange(da.GetPrintGreenByBankReport(param.PrintBranch, param.FromDate, param.ToDate, input.InputStr, 4));
                    }
                }
                else //direct debit green (printType = 2)
                {
                    if (param.PrintingCondition == (int)PrintingCondition.AllPrinting)
                        reportItem.AddRange(da.GetPrintGreenByBankReport(param.PrintBranch, param.FromDate, param.ToDate, null, 2));
                    else
                    {
                        foreach (InputParam input in param.InputParamList)
                            reportItem.AddRange(da.GetPrintGreenByBankReport(param.PrintBranch, param.FromDate, param.ToDate, input.InputStr, 2));
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return reportItem;
        }

        public List<ReportBillingStatus> GetBillingStatusReport(ReportConditionParam param)
        {
            ReportDA da = new ReportDA();
            List<ReportBillingStatus> reportItem = new List<ReportBillingStatus>();
            char[] charSeperator = new char[] { '-' };
            string[] tmpId = new string[3];

            try
            {
                if (param.PrintingCondition == (int)PrintingCondition.AllPrinting)
                    reportItem.AddRange(da.GetBillingStatusReport(param.PrintBranch, param.BillPeriod, param.PrintType, null, null, null, param.PortionNo));
                else 
                {
                    foreach (InputParam b in param.InputParamList)
                    {
                        if (param.PrintingCondition == (int)PrintingCondition.BranchPrinting)
                            reportItem.AddRange(da.GetBillingStatusReport(param.PrintBranch, param.BillPeriod, param.PrintType, b.InputStr, null, null, param.PortionNo));
                        else if (param.PrintingCondition == (int)PrintingCondition.MruPrinting)
                        {
                            string branchId = null;
                            string mruId = null;
                            string toMruId = null;

                            tmpId = b.InputStr.Split(charSeperator, StringSplitOptions.RemoveEmptyEntries);
                            if (tmpId[0] != null)
                                branchId = tmpId[0];
                            if (tmpId[1] != null)
                            {
                                mruId = tmpId[1];
                                toMruId = tmpId[1];
                            }
                            //if user key mruId in form of range, our elecIdArray must have length = 3
                            //BranchId - MruId - ToMruId;after split(),length = 3
                            if (tmpId.Length == 3)
                                if (tmpId[2] != null)
                                    toMruId = tmpId[2];

                            reportItem.AddRange(da.GetBillingStatusReport(param.PrintBranch, param.BillPeriod, param.PrintType, branchId, mruId, toMruId, param.PortionNo));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return reportItem;
        }

        public List<ReportCAUnprint> GetCAUnprintReport(ReportConditionParam param)
        {
            ReportDA da = new ReportDA();
            List<ReportCAUnprint> reportItem = null;

            try
            {
                reportItem = da.GetCAUnprintReport(param.BankId, param.BankDueDate, param.MtNo);
            }
            catch (Exception ex)
            {
                throw;
            }

            return reportItem;
        }

        public List<ReportDirectDebitStatus> GetDirectDebitStatusReport(ReportConditionParam param)
        {
            ReportDA da = new ReportDA();
            List<ReportDirectDebitStatus> reportItem = new List<ReportDirectDebitStatus>();

            try
            {
                if (param.PrintingCondition == (int)PrintingCondition.AllPrinting)
                    reportItem.AddRange(da.GetDirectDebitStatusReport(param));
                else if (param.PrintingCondition == (int)PrintingCondition.BranchPrinting)
                {
                    foreach (string b in param.ListElectricId)
                    {
                        param.BranchId = b;
                        reportItem.AddRange(da.GetDirectDebitStatusReport(param));
                    }
                }
                else if (param.PrintingCondition == (int)PrintingCondition.MruPrinting)
                {
                    foreach (string m in param.ListElectricId)
                    {
                        string[] tmpId = m.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                        if (tmpId[0] != null)
                            param.BranchId = tmpId[0];
                        if (tmpId[1] != null)
                            param.MruId = tmpId[1];
                        if (tmpId.Length == 3 && tmpId[2] != null)
                            param.ToMruId = tmpId[2];

                        reportItem.AddRange(da.GetDirectDebitStatusReport(param));

                        tmpId = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return reportItem;
        }


        public List<ReportGroupingCrossCheck> GetGroupingCrossCheckReport(ReportConditionParam param)
        {
            throw new Exception("This report is no longer provided.");
            //ReportDA da = new ReportDA();
            //List<ReportGroupingCrossCheck> reportItem = new List<ReportGroupingCrossCheck>();
            //try
            //{
            //    if (param.PrintingCondition == "All")
            //        reportItem.AddRange(da.GetGroupingCrossCheckReport(param.PrintBranch, param.BillPeriod, null));
            //    else
            //    {
            //        foreach(InputParam p in param.InputParamList)
            //            reportItem.AddRange(da.GetGroupingCrossCheckReport(param.PrintBranch, param.BillPeriod, p.InputStr));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            //return reportItem;
        }

    }
}

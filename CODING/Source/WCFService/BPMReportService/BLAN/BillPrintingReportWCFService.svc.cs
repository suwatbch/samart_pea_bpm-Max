using System.Collections.Generic;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;
using WCFExtras.Soap;
using BPMLINQReport;
using System;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace BPMReportService.BLAN
{
    public class BillPrintingReportWCFService : IBillPrintingReportWCFService
    {
        private BLAReport _bs;
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public BillPrintingReportWCFService()
        {
            _bs = new BLAReport();
        }

        public CompressData GetStreetRouteReport(ReportConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingReportWCFService", "GetStreetRouteReport",
                () =>
                {
                    List<ReportStreetRoute> srAll = new List<ReportStreetRoute>();
                    try
                    {
                        if (param.PrintingCondition == (int)PrintingCondition.AllPrinting)
                        {
                            srAll.AddRange(_bs.GetStreetRouteReport(param.BillPeriod, null, param.PrintBranch, param.PortionNo));
                        }
                        else
                        {
                            foreach (InputParam id in param.InputParamList)
                                srAll.AddRange(_bs.GetStreetRouteReport(param.BillPeriod, id.InputStr, param.PrintBranch, param.PortionNo));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    return ServiceHelper.CompressData<List<ReportStreetRoute>>(srAll);
                });
        }

        public CompressData GetStreetRouteReceiveReport(ReportConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingReportWCFService", "GetStreetRouteReceiveReport",
                () =>
                {
                    List<ReportStreetRouteReceive> srrAll = new List<ReportStreetRouteReceive>();
                    try
                    {
                        if (param.PrintingCondition == (int)PrintingCondition.AllPrinting)
                        {
                            srrAll.AddRange(_bs.GetStreetRouteReceiveReport(param.PrintBranch, null, param.PortionNo, param.DataReceiveDt, param.ToDataReceiveDt, param.FromTime, param.ToTime));
                        }
                        else
                        {
                            foreach (InputParam id in param.InputParamList)
                                srrAll.AddRange(_bs.GetStreetRouteReceiveReport(param.PrintBranch, id.InputStr, param.PortionNo, param.DataReceiveDt, param.ToDataReceiveDt, param.FromTime, param.ToTime));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    return ServiceHelper.CompressData<List<ReportStreetRouteReceive>>(srrAll);
                });
        }

        public CompressData GetDailyPrintReport(ReportConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingReportWCFService", "GetDailyPrintReport",
                () =>
                {
                    List<ReportDailyPrint> dpAll = new List<ReportDailyPrint>();
                    try
                    {
                        if (param.PrintingCondition == (int)PrintingCondition.AllPrinting)
                        {
                            dpAll.AddRange(_bs.GetDailyPrintReport(param.PrintDate, null, param.PrintBranch, param.PrintType));
                        }
                        else if (param.PrintingCondition == (int)PrintingCondition.BranchPrinting)
                        {
                            foreach (InputParam b in param.InputParamList)
                                dpAll.AddRange(_bs.GetDailyPrintReport(param.PrintDate, b.InputStr, param.PrintBranch, param.PrintType));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    return ServiceHelper.CompressData<List<ReportDailyPrint>>(dpAll);
                });
        }

        public CompressData GetDailyUnprintReport(ReportConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingReportWCFService", "GetDailyUnprintReport",
                () =>
                {
                    List<ReportDailyUnprint> duAll = new List<ReportDailyUnprint>();
                    try
                    {
                        if (param.PrintingCondition == (int)PrintingCondition.AllPrinting)  //print all
                        {
                            duAll.AddRange(_bs.GetDailyUnprintReport(param.BillPeriod, param.PortionNo, null, param.PrintBranch));
                        }
                        else
                        {
                            foreach (InputParam ip in param.InputParamList)
                                duAll.AddRange(_bs.GetDailyUnprintReport(param.BillPeriod, param.PortionNo, ip.InputStr, param.PrintBranch));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    return ServiceHelper.CompressData<List<ReportDailyUnprint>>(duAll);
                });
        }

        public List<PrintableId> GetBranchForBillDeliveryReport(ReportConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingReportWCFService", "GetBranchForBillDeliveryReport",
                () =>
                {
                    return _bs.GetBranchForBillDeliveryReport(param);
                });
        }

        public CompressData GetBillDeliveryReport(ReportConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingReportWCFService", "GetBillDeliveryReport",
                () =>
                {
                    DbTransaction trans;
                    Database db = DatabaseFactory.CreateDatabase("POSDatabase");

                    using (DbConnection conn = db.CreateConnection())
                    {
                        conn.Open();
                        trans = conn.BeginTransaction();

                        try
                        {
                            BLAReport da = new BLAReport();
                            List<ReportBillDelivery> dpAll = new List<ReportBillDelivery>();

                            foreach (InputParam p in param.InputParamList)
                            {
                                dpAll.AddRange(da.GetBillDeliveryReport(trans, param.ReportType, param.BillPeriod, param.BillPeriodLog, p.InputStr, param.PrintBranch,
                                    param.DeliveryPlace, param.PrintedBy, p.PrintType, param.Save, param.IsReprint));
                            }

                            trans.Commit();
                            return ServiceHelper.CompressData<List<ReportBillDelivery>>(dpAll);
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                            throw;
                        }
                    }

                });
        }

        public CompressData GetF16Report(ReportConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingReportWCFService", "GetF16Report",
                () =>
                {
                    List<ReportF16> f16All = new List<ReportF16>();//store all records in one and return kub

                    char[] charSeperator = new char[] { '-' };
                    string[] tmpId = new string[3];

                    try
                    {
                        foreach (InputParam b in param.InputParamList)
                        {
                            if (param.PrintingCondition == (int)PrintingCondition.BranchPrinting)
                                f16All.AddRange(_bs.GetF16Report(param.BillPeriod, b.InputStr, null, null, param.PrintBranch));
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

                                f16All.AddRange(_bs.GetF16Report(param.BillPeriod, branchId, mruId, toMruId, param.PrintBranch));
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

                    return ServiceHelper.CompressData<List<ReportF16>>(f16All);
                });
        }

        public CompressData GetGrpInvSummaryReport(ReportConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingReportWCFService", "GetGrpInvSummaryReport",
                () =>
                {
                    List<ReportGrpInvSummary> reportItem = new List<ReportGrpInvSummary>();
                    try
                    {
                        //print all branch in printbranch
                        if (param.PrintingCondition == (int)PrintingCondition.AllPrinting)
                        {
                            reportItem.AddRange(_bs.GetGrpInvSummaryReport(param.PrintingCondition.Value, param.BillPeriod, param.PrintBranch, null, null));
                        }
                        else if (param.PrintingCondition == (int)PrintingCondition.MtNoPrinting)
                        {
                            foreach (InputParam b in param.InputParamList)
                                reportItem.AddRange(_bs.GetGrpInvSummaryReport(param.PrintingCondition.Value, param.BillPeriod, param.PrintBranch, b.InputStr, null));
                        }
                        else if (param.PrintingCondition == (int)PrintingCondition.PaidByPrinting)
                        {
                            foreach (InputParam b in param.InputParamList)
                                reportItem.AddRange(_bs.GetGrpInvSummaryReport(param.PrintingCondition.Value, param.BillPeriod, param.PrintBranch, null, b.InputStr));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    return ServiceHelper.CompressData<List<ReportGrpInvSummary>>(reportItem);
                });
        }

        public CompressData GetPrintGreenByBankReport(ReportConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingReportWCFService", "GetPrintGreenByBankReport",
                () =>
                {
                    List<ReportPrintByBank> reportItem = new List<ReportPrintByBank>();
                    try
                    {
                        if (param.ReportType == 0)  //direct debit blue
                        {
                            if (param.PrintingCondition == (int)PrintingCondition.AllPrinting)
                                reportItem.AddRange(_bs.GetPrintGreenByBankReport(param.PrintBranch, param.FromDate, param.ToDate, null, 4));
                            else
                            {
                                foreach (InputParam input in param.InputParamList)
                                    reportItem.AddRange(_bs.GetPrintGreenByBankReport(param.PrintBranch, param.FromDate, param.ToDate, input.InputStr, 4));
                            }
                        }
                        else //direct debit green (printType = 2)
                        {
                            if (param.PrintingCondition == (int)PrintingCondition.AllPrinting)
                                reportItem.AddRange(_bs.GetPrintGreenByBankReport(param.PrintBranch, param.FromDate, param.ToDate, null, 2));
                            else
                            {
                                foreach (InputParam input in param.InputParamList)
                                    reportItem.AddRange(_bs.GetPrintGreenByBankReport(param.PrintBranch, param.FromDate, param.ToDate, input.InputStr, 2));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    return ServiceHelper.CompressData<List<ReportPrintByBank>>(reportItem);
                });
        }

        public CompressData GetBillingStatusReport(ReportConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingReportWCFService", "GetBillingStatusReport",
                () =>
                {
                    List<ReportBillingStatus> reportItem = new List<ReportBillingStatus>();
                    char[] charSeperator = new char[] { '-' };
                    string[] tmpId = new string[3];

                    try
                    {
                        if (param.PrintingCondition == (int)PrintingCondition.AllPrinting)
                            reportItem.AddRange(_bs.GetBillingStatusReport(param.PrintBranch, param.BillPeriod, param.PrintType, null, null, null, param.PortionNo));
                        else
                        {
                            foreach (InputParam b in param.InputParamList)
                            {
                                if (param.PrintingCondition == (int)PrintingCondition.BranchPrinting)
                                    reportItem.AddRange(_bs.GetBillingStatusReport(param.PrintBranch, param.BillPeriod, param.PrintType, b.InputStr, null, null, param.PortionNo));
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

                                    reportItem.AddRange(_bs.GetBillingStatusReport(param.PrintBranch, param.BillPeriod, param.PrintType, branchId, mruId, toMruId, param.PortionNo));
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    return ServiceHelper.CompressData<List<ReportBillingStatus>>(reportItem);
                });
        }

        public CompressData GetDirectDebitStatusReport(ReportConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingReportWCFService", "GetDirectDebitStatusReport",
                () =>
                {
                    List<ReportDirectDebitStatus> reportItem = new List<ReportDirectDebitStatus>();

                    try
                    {
                        if (param.PrintingCondition == (int)PrintingCondition.AllPrinting)
                            reportItem.AddRange(_bs.GetDirectDebitStatusReport(param.PrintBranch, param.BranchId, param.BillPeriod, param.MruId, param.ToMruId));
                        else if (param.PrintingCondition == (int)PrintingCondition.BranchPrinting)
                        {
                            foreach (string b in param.ListElectricId)
                            {
                                param.BranchId = b;
                                reportItem.AddRange(_bs.GetDirectDebitStatusReport(param.PrintBranch, param.BranchId, param.BillPeriod, param.MruId, param.ToMruId));
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

                                reportItem.AddRange(_bs.GetDirectDebitStatusReport(param.PrintBranch, param.BranchId, param.BillPeriod, param.MruId, param.ToMruId));
                                tmpId = null;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    return ServiceHelper.CompressData<List<ReportDirectDebitStatus>>(reportItem);
                });
        }
    }
}

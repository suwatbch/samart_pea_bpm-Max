using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Globalization;

namespace PEA.BPM.BillPrintingModule.DA
{
    public class ReportDA
    {
        public const int CMD_TIMEOUT = 10000;

        //5.10.1
        public List<ReportStreetRoute> GetStreetRouteReport(string period, string branchId, string printBranch, string portionNo)
        {
            List<ReportStreetRoute> _reportStreetRoute = new List<ReportStreetRoute>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("bp_sel_reportStreetRoute");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "BillPred", DbType.String, period);
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "PrintBranch", DbType.String, printBranch);
                db.AddInParameter(cmd, "PortionNo", DbType.String, portionNo);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    ReportStreetRoute sr = new ReportStreetRoute();
                    sr.BranchId = DaHelper.GetString(dr, "BranchId");
                    sr.BranchName = DaHelper.GetString(dr, "BranchName").Trim();
                    sr.MruId = DaHelper.GetString(dr, "MruId");
                    sr.PortionNo = DaHelper.GetString(dr, "PortionNo");
                    sr.Total = DaHelper.GetInt(dr, "Total");
                    sr.PtcNo = DaHelper.GetString(dr, "PtcNo");
                    sr.ReadPtc = DaHelper.GetInt(dr, "ReadPtc");
                    sr.ReadOther = DaHelper.GetInt(dr, "ReadOther");
                    sr.TotBlue = DaHelper.GetInt(dr, "TotBlue");
                    sr.TotGreen = DaHelper.GetInt(dr, "TotGreen");
                    sr.TotA4 = DaHelper.GetInt(dr, "TotA4");
                    sr.TotNSpotBill = DaHelper.GetInt(dr, "TotNSpotBill");
                    sr.TotOther = DaHelper.GetInt(dr, "TotOther");

                    //sr.GroupBranchId = DaHelper.GetString(dr, "GroupBranchId");
                    //sr.GroupBranchName = DaHelper.GetString(dr, "GroupBranchName");

                    sr.ReadSpotBill = DaHelper.GetInt(dr, "ReadSpotBill");
                    sr.ReadAMR = DaHelper.GetInt(dr, "ReadAMR");
                    sr.TotNoPrint = DaHelper.GetInt(dr, "TotNoPrint");
                    sr.TotA4Addition = DaHelper.GetInt(dr, "TotA4Addition");
                    sr.TotBill = DaHelper.GetInt(dr, "TotBill");

                    _reportStreetRoute.Add(sr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _reportStreetRoute;
        }

        //5.10.2
        public List<ReportStreetRouteReceive> GetStreetRouteReceiveReport(ReportConditionParam param, string branchId)
        {
            List<ReportStreetRouteReceive> _reportStreetRouteReceive = new List<ReportStreetRouteReceive>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("bp_sel_reportStreetRouteReceive");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "DataReceiveDt", DbType.DateTime, param.DataReceiveDt);
                db.AddInParameter(cmd, "ToDataReceiveDt", DbType.DateTime, param.ToDataReceiveDt);
                db.AddInParameter(cmd, "FromTime", DbType.String, param.FromTime);
                db.AddInParameter(cmd, "ToTime", DbType.String, param.ToTime);
                db.AddInParameter(cmd, "PortionNo", DbType.String, param.PortionNo);
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "PrintBranch", DbType.String, param.PrintBranch);



                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    ReportStreetRouteReceive srr = new ReportStreetRouteReceive();
                    srr.BillPred = DaHelper.GetString(dr, "BillPred");
                    //srr.RowId = DaHelper.GetInt(dr, "RowId");
                    srr.BranchId = DaHelper.GetString(dr, "BranchId");
                    srr.BranchName = DaHelper.GetString(dr, "BranchName").Trim();
                    srr.MruId = DaHelper.GetString(dr, "MruId");
                    srr.PortionNo = DaHelper.GetString(dr, "PortionNo");
                    srr.Total = DaHelper.GetInt(dr, "CaTotal");
                    //srr.Received = DaHelper.GetInt(dr, "Received");
                    //srr.UnReceived = DaHelper.GetInt(dr, "UnReceived");
                    //srr.GroupBranchId = DaHelper.GetString(dr, "GroupBranchId");
                    //srr.GroupBranchName = DaHelper.GetString(dr, "GroupBranchName"); 

                    srr.NormalBlue = DaHelper.GetInt(dr, "NBlue");
                    srr.FixBlue = DaHelper.GetInt(dr, "FBlue");
                    srr.TotBlue = DaHelper.GetInt(dr, "TotBlue");
                    srr.NormalGreen = DaHelper.GetInt(dr, "NGreen");
                    srr.FixGreen = DaHelper.GetInt(dr, "FGreen");
                    srr.TotGreen = DaHelper.GetInt(dr, "TotGreen");
                    srr.NormalA4 = DaHelper.GetInt(dr, "NA4");
                    srr.FixA4 = DaHelper.GetInt(dr, "FA4");
                    srr.TotA4 = DaHelper.GetInt(dr, "TotA4");
                    srr.NormalSpotBill = DaHelper.GetInt(dr, "NSpotBill");
                    srr.FixSpotBill = DaHelper.GetInt(dr, "FSpotBill");
                    srr.TotSpotBill = DaHelper.GetInt(dr, "TotSpotBill");
                    srr.NormalA4Add = DaHelper.GetInt(dr, "NA4Add");
                    srr.FixA4Add = DaHelper.GetInt(dr, "FA4Add");
                    srr.TotA4Add = DaHelper.GetInt(dr, "TotA4Add");
                    srr.TotNBill = DaHelper.GetInt(dr, "TotNBill");
                    srr.TotFBill = DaHelper.GetInt(dr, "TotFBill");
                    srr.TotBill = DaHelper.GetInt(dr, "TotBill");

                    _reportStreetRouteReceive.Add(srr);
                }
            }
            catch
            {
                throw;
            }

            return _reportStreetRouteReceive;
        }

        //5.10.3
        public List<ReportDailyPrint> GetDailyPrintReport(DateTime? printDt, string branchId, string groupBranch, string printBranch, int? printType)
        {
            List<ReportDailyPrint> _reportDailyPrint = new List<ReportDailyPrint>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("bp_sel_reportDailyPrint");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintDate", DbType.DateTime, printDt);
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "GroupBranch", DbType.String, groupBranch);
                db.AddInParameter(cmd, "PrintBranch", DbType.String, printBranch);
                db.AddInParameter(cmd, "PrintType", DbType.Int16, printType);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    ReportDailyPrint dp = new ReportDailyPrint();
                    dp.BranchId = DaHelper.GetString(dr, "BranchId");
                    dp.BranchName = DaHelper.GetString(dr, "BranchName").Trim();
                    dp.BillPeriod = DaHelper.GetDate(dr, "PrintDate");
                    dp.MruId = DaHelper.GetString(dr, "MruId");
                    dp.BillSeqNoFrom = DaHelper.GetInt(dr, "BillSeqNoFrom");
                    dp.BillSeqNoTo = DaHelper.GetInt(dr, "BillSeqNoTo");
                    dp.InvoiceFrom = DaHelper.GetString(dr, "InvoiceFrom");
                    dp.InvoiceTo = DaHelper.GetString(dr, "InvoiceTo");
                    dp.TotBill = DaHelper.GetInt(dr, "TotBill");
                    dp.PrintCount = DaHelper.GetInt(dr, "PrintCount");
                    dp.PrintType = DaHelper.GetByte(dr, "PrintType").ToString();
                    dp.BillFlag = DaHelper.GetString(dr, "BillFlag");
                    dp.PrintLog = DaHelper.GetInt(dr, "PrintLog");
                    dp.PrintSubType = DaHelper.GetByte(dr, "PrintSubType").ToString();
                    dp.GroupBranch = DaHelper.GetString(dr, "GroupBranchId");
                    dp.GroupBranchName = DaHelper.GetString(dr, "GroupBranchName");
                    _reportDailyPrint.Add(dp);
                }              
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _reportDailyPrint;
        }

        //5.10.4
        public List<ReportDailyUnprint> GetDailyUnprintReport(string period, string portionId,string branchId, string printBranch)
        {
            List<ReportDailyUnprint> _reportDailyUnprint = new List<ReportDailyUnprint>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("bp_sel_reportDailyUnprint");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "BillPred", DbType.String, period);
                db.AddInParameter(cmd, "PortionId", DbType.String, portionId);
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "PrintBranch", DbType.String, printBranch);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    ReportDailyUnprint du = new ReportDailyUnprint();
                    du.BranchId = DaHelper.GetString(dr, "BranchId");
                    du.BranchName = DaHelper.GetString(dr, "BranchName").Trim();
                    du.PortionNo = DaHelper.GetString(dr, "PortionNo");
                    du.MruId = DaHelper.GetString(dr, "MruId");
                    du.TotBill = DaHelper.GetInt(dr, "TotBill");
                    du.A4Bill = DaHelper.GetInt(dr, "A4Bill");
                    du.BlueBill = DaHelper.GetInt(dr, "BlueBill");
                    du.GreenBill = DaHelper.GetInt(dr, "GreenBill");
                    du.SpotBill = DaHelper.GetInt(dr, "SpotBill");

                    du.A4Addition = DaHelper.GetInt(dr, "A4Addition");
                    du.TotNoPrint = DaHelper.GetInt(dr, "TotNoPrint");

                    _reportDailyUnprint.Add(du);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _reportDailyUnprint;
        }

        //5.10.5
        //see, this is a special report. It has writes in reporting process. so it must be realtime.
        public List<ReportBillDelivery> GetBillDeliveryReport(DbTransaction trans, int? billFlag, string period, int? log, string branchId, string sentBranch, 
                                                string deliveryPlace, string printedBy, int? printType, bool save, bool isReprint)
        {
            DbCommand cmd = null;
            List<ReportBillDelivery> _reportBillDelivery = new List<ReportBillDelivery>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                
                if(printType == 6)
                    cmd = db.GetStoredProcCommand("bp_sel_reportBillDelivery_Receipt");
                else 
                    cmd = db.GetStoredProcCommand("bp_sel_reportBillDelivery");

                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "BillFlag",  DbType.String, billFlag);
                db.AddInParameter(cmd, "SentPred",  DbType.String, period);
                db.AddInParameter(cmd, "SentPred_Log", DbType.Int16, log);
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "SentBranch", DbType.String, sentBranch);
                db.AddInParameter(cmd, "DeliveryPlace", DbType.String, deliveryPlace);
                db.AddInParameter(cmd, "PrintedBy", DbType.String, printedBy);
                db.AddInParameter(cmd, "SaveRecord", DbType.String, save?"1":"0");
                db.AddInParameter(cmd, "PrintType", DbType.Int16, printType); //agency=1,bank=2s 

                db.AddInParameter(cmd, "IsReprint", DbType.Int16, isReprint ? 1 : 0);
                
                if (billFlag == 0)
                {
                    DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        ReportBillDelivery bd = new ReportBillDelivery();
                        bd.BranchId = DaHelper.GetString(dr, "BranchId");
                        bd.BranchName = DaHelper.GetString(dr, "BranchName").Trim();
                        bd.MruId = DaHelper.GetString(dr, "MruId");
                        bd.PrintType = DaHelper.GetByte(dr, "PrintType").ToString();
                        bd.InvoiceFrom = DaHelper.GetString(dr, "InvoiceFrom").Trim();
                        bd.InvoiceTo = DaHelper.GetString(dr, "InvoiceTo").Trim();
                        bd.TotBill = DaHelper.GetInt(dr, "TotBill");
                        bd.TotUnit = DaHelper.GetDecimal(dr, "TotUnit");
                        bd.TotAmt = DaHelper.GetDecimal(dr, "TotAmt");
                        bd.TotVat = DaHelper.GetDecimal(dr, "TotVat");
                        bd.TotFt = DaHelper.GetDecimal(dr, "TotFt");
                        bd.TotGAmt = DaHelper.GetDecimal(dr, "TotGAmt");
                        bd.TotModVat = DaHelper.GetDecimal(dr, "TotModVat");
                        bd.TotDiscount = DaHelper.GetDecimal(dr, "TotDiscount");
                        bd.TotGElecAmt = DaHelper.GetDecimal(dr, "TotGElecAmt");
                        bd.TotFreeUnit = DaHelper.GetDecimal(dr, "TotFreeUnit");
                        bd.BillSeqNoFrom = DaHelper.GetInt(dr, "BillSeqNoFrom");
                        bd.BillSeqNoTo = DaHelper.GetInt(dr, "BillSeqNoTo");
                        bd.NonPrint = DaHelper.GetInt(dr, "NonPrint");
                        bd.SentDt = DaHelper.GetDate(dr, "SentDt");

                        _reportBillDelivery.Add(bd);
                    }
                }
                else
                {
                    DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        ReportBillDelivery bd = new ReportBillDelivery();
                        bd.BranchId = DaHelper.GetString(dr, "BranchId").Trim();
                        bd.BranchName = DaHelper.GetString(dr, "BranchName").Trim();
                        bd.MruId = DaHelper.GetString(dr, "MruId").Trim();
                        bd.PrintType = DaHelper.GetByte(dr, "PrintType").ToString();
                        bd.CaId = DaHelper.GetString(dr, "CaId").Trim();
                        bd.RateType = DaHelper.GetString(dr, "RateType").Trim();
                        bd.BillPred = DaHelper.GetString(dr, "BillPred").Trim();
                        bd.OInvoiceNo = DaHelper.GetString(dr, "oInvoiceNo").Trim();
                        bd.OUnit = DaHelper.GetDecimal(dr, "oUnit");
                        bd.OAmt = DaHelper.GetDecimal(dr, "oAmt");
                        bd.OFt = DaHelper.GetDecimal(dr, "oFt");
                        bd.OVat = DaHelper.GetDecimal(dr, "oVat");
                        bd.OGAmt = DaHelper.GetDecimal(dr, "oGAmt");
                        bd.OModVat = DaHelper.GetDecimal(dr, "oModVat");
                        bd.ODiscount = DaHelper.GetDecimal(dr, "oDiscount");
                        bd.OGElecAmt = DaHelper.GetDecimal(dr, "oGElecAmt");
                        bd.NInvoiceNo = DaHelper.GetString(dr, "nInvoiceNo");
                        bd.NUnit = DaHelper.GetDecimal(dr, "nUnit");
                        bd.NAmt = DaHelper.GetDecimal(dr, "nAmt");
                        bd.NFt = DaHelper.GetDecimal(dr, "nFt");
                        bd.NVat = DaHelper.GetDecimal(dr, "nVat");
                        bd.NGAmt = DaHelper.GetDecimal(dr, "nGAmt");
                        bd.NModVat = DaHelper.GetDecimal(dr, "nModVat");
                        bd.NDiscount = DaHelper.GetDecimal(dr, "nDiscount");
                        bd.NGElecAmt = DaHelper.GetDecimal(dr, "nGElecAmt");

                        _reportBillDelivery.Add(bd);
                    }
                }
            }
            catch
            {
                throw;
            }
        
            return _reportBillDelivery;
        }

       
        private Decimal RoundMoney(Decimal? amt)
        {
            if (amt == 0) return (Decimal)0.00;
            bool signed = false;
            decimal amt2 = Math.Round(amt.Value, 2);
            int prec = Convert.ToInt32((amt2 * 100) % 100);

            if(prec < 0 ) {
                signed = true;
                prec = prec * (-1);  //treat it as +
                amt2 = amt2 * (-1); //make it +
            }

            int amtInt = Convert.ToInt32(Math.Floor((double)amt2));

            if (prec > 0 && prec <= 12)
            {
                amt2 = amtInt * (signed ? -1 : 1);
            }
            else if (prec > 12 && prec <= 37)
            {
                amt2 = (decimal)((amtInt + 0.25) * (signed ? -1 : 1));
            }
            else if (prec > 37 && prec <= 62)
            {
                amt2 = (decimal)((amtInt + 0.50) * (signed ? -1 : 1));
            }
            else if (prec > 62 && prec <= 87)
            {
                amt2 = (decimal)((amtInt + 0.75) * (signed ? -1 : 1));
            }
            else // > 87 , <=100
            {
                amt2 = (amtInt + 1) * (signed ? -1 : 1);
            }

            return amt2;
        }

        //5.10.6
        public List<ReportF16> GetF16Report(string period, string branchId, string mruId, string toMruId, string regBranch)
        {
            List<ReportF16> _reportF16 = new List<ReportF16>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("bp_sel_reportF16");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "BillPred", DbType.String, period);
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "MruId", DbType.String, mruId);
                db.AddInParameter(cmd, "ToMruId", DbType.String, toMruId);
                db.AddInParameter(cmd, "RegBranch", DbType.String, regBranch);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    ReportF16 f16 = new ReportF16();
                    f16.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                    f16.BranchId = DaHelper.GetString(dr, "BranchId");
                    f16.BranchName = DaHelper.GetString(dr, "BranchName");
                    f16.MruId = DaHelper.GetString(dr, "MruId");
                    f16.CaId = DaHelper.GetString(dr, "CaId");
                    f16.DateCur = DaHelper.GetString(dr, "DateCur");
                    f16.EType = DaHelper.GetString(dr, "EType");
                    f16.EPower = DaHelper.GetString(dr, "EPower");
                    f16.UnitCur = DaHelper.GetString(dr, "UnitCur").Trim();
                    f16.UnitPrev = DaHelper.GetString(dr, "UnitPrev").Trim();
                    f16.Unit = DaHelper.GetDecimal(dr, "Unit");
                    f16.BaseAmt = DaHelper.GetDecimal(dr, "BaseAmt");
                    f16.DiscountAmt = DaHelper.GetDecimal(dr, "DiscountAmt");
                    f16.FtAmt = DaHelper.GetDecimal(dr, "FtAmt");
                    f16.Amt = DaHelper.GetDecimal(dr, "Amt");
                    f16.VatAmt = DaHelper.GetDecimal(dr, "VatAmt");
                    f16.GAmt = DaHelper.GetDecimal(dr, "GAmt");
                    f16.GAmtRounded = RoundMoney(f16.GAmt);
                    f16.FreeUnit = DaHelper.GetDecimal(dr, "FreeUnit");
                    f16.W_171_ettat_code = DaHelper.GetString(dr, "w_171_ettat_code");
                    f16.W_1861_crsg = DaHelper.GetString(dr, "w_1861_crsg");
                    f16.W_1570_account_number = DaHelper.GetString(dr, "w_1570_account_number");
                    f16.W_1830_payment_method = DaHelper.GetString(dr, "w_1830_payment_method");
                    f16.W_1910_org_doc = DaHelper.GetString(dr, "w_1910_org_doc");
                    f16.W_1960_acct_class = DaHelper.GetString(dr, "w_1960_acct_class");
                    f16.W_1980_spotbill = DaHelper.GetString(dr, "w_1980_spotbill");
                    f16.W_1990_addition = DaHelper.GetString(dr, "w_1990_addition");
                    f16.W_2000_dispctrl = DaHelper.GetString(dr, "w_2000_dispctrl");

                    //f16.W_1874_green = DaHelper.GetString(dr, "w_1874_green");
                    //f16.W_1930_bank_id = DaHelper.GetString(dr, "w_1930_bank_id");
                    //f16.W_1960_disconnect_flag = DaHelper.GetString(dr, "w_1960_disconnect_flag");
                    //f16.W_1872_A4 = DaHelper.GetString(dr, "w_1872_A4");
                    //f16.W_1873_A4_mass_print = DaHelper.GetString(dr, "w_1873_A4_mass_print");
                    //f16.W_1890_payer = DaHelper.GetString(dr, "w_1890_payer");
                    //f16.W_1870_blue = DaHelper.GetString(dr, "w_1870_blue");
                    //f16.W_1871_blue_mass_print = DaHelper.GetString(dr, "w_1871_blue_mass_print");
                    _reportF16.Add(f16);
                }
            }
            catch
            {
                throw;
            }
            return _reportF16;
        }

        //this is used for billDelivery report which is a realtime report.
        public List<PrintableId> GetBranchForBillDeliveryReport(ReportConditionParam param)
        {
            List<PrintableId> _printableId = new List<PrintableId>();

            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("bp_sel_branchForBilldelivery");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "BillFlag", DbType.Int16, param.ReportType);
                db.AddInParameter(cmd, "SentPred", DbType.String, param.BillPeriod);
                db.AddInParameter(cmd, "SentPred_Log", DbType.Int16, param.BillPeriodLog);
                db.AddInParameter(cmd, "PrintBranch", DbType.String, param.PrintBranch); //printbranch
                db.AddInParameter(cmd, "PrintType", DbType.Int16, param.PrintType);
                
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    PrintableId pt = new PrintableId();
                    pt.BranchId = DaHelper.GetString(dr, "BranchId");
                    pt.BillPeriod = DaHelper.GetString(dr, "SentPred");
                    pt.BillPeriodLog = DaHelper.GetInt(dr, "SentPred_Log");
                    pt.PrintType = DaHelper.GetByte(dr, "PrintType").ToString();
                    _printableId.Add(pt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _printableId;
        }

        //5.10.7
        public List<ReportGrpInvSummary> GetGrpInvSummaryReport(int printCondition, string period, string printBranch, string mtNo, string paidBy)
        {
            List<ReportGrpInvSummary> _reportItem = new List<ReportGrpInvSummary>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("bp_sel_reportGrpInvSummary");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintCondition", DbType.String, printCondition.ToString());
                db.AddInParameter(cmd, "DuePeriod", DbType.String, period);
                db.AddInParameter(cmd, "PrintBranch", DbType.String, printBranch); //printbranch
                db.AddInParameter(cmd, "MtNo", DbType.String, mtNo);
                db.AddInParameter(cmd, "PaidBy", DbType.String, paidBy==null? null: paidBy.PadLeft(12, '0'));
                

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    ReportGrpInvSummary pt = new ReportGrpInvSummary();
                    pt.Payer = DaHelper.GetString(dr, "Payer").TrimStart('0');
                    pt.PayerName = DaHelper.GetString(dr, "PayerName");
                    pt.MtNo = DaHelper.GetString(dr, "MtNo");
                    pt.BranchId = DaHelper.GetString(dr, "BranchId");
                    pt.PrintBranch = DaHelper.GetString(dr, "PrintBranch");
                    pt.BranchName = DaHelper.GetString(dr, "BranchName");
                    pt.MruId = DaHelper.GetString(dr, "MruId");

                    string tmp = DaHelper.GetString(dr, "GroupingDate");
                    tmp = tmp.Substring(6, 2) + tmp.Substring(4, 2) + tmp.Substring(0, 4);
                    pt.GroupDate = tmp;
                    
                    pt.PrintDt = DaHelper.GetDate(dr, "PrintDate");
                    pt.TotAmount = DaHelper.GetDecimal(dr, "TotAmount");
                    pt.TotBill = DaHelper.GetInt(dr, "TotBill");
                    pt.PrintCount = DaHelper.GetInt(dr, "PrintCount");
                    pt.RemainCount = DaHelper.GetInt(dr, "RemainCount");
                    pt.PrintedBy = (DaHelper.GetString(dr, "PrintedBy").TrimStart('0')).ToUpper();
                    _reportItem.Add(pt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _reportItem;
        }

        //5.10.8
        public List<ReportPrintByBank> GetPrintGreenByBankReport(string printBranch, DateTime fromDate, DateTime toDate, string bankId, int? printType )
        {
            List<ReportPrintByBank> _reportItem = new List<ReportPrintByBank>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_sel_reportPrintByBank");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintType", DbType.Int16, printType); //green by bank
            db.AddInParameter(cmd, "PrintBranch", DbType.String, printBranch); //printbranch
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, fromDate); 
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, toDate);
            db.AddInParameter(cmd, "BankId", DbType.String, bankId); 
            DataSet ds = db.ExecuteDataSet(cmd);
            //DataTable dt = null;
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ReportPrintByBank pt = new ReportPrintByBank();
                    pt.BankId = DaHelper.GetString(dr, "BankId");
                    pt.BankKey = DaHelper.GetString(dr, "BankKey");
                    pt.BankName = DaHelper.GetString(dr, "BankName");
                    pt.BranchId = DaHelper.GetString(dr, "BranchId");
                    pt.MruId = DaHelper.GetString(dr, "MruId");
                    pt.BillProcDate = DaHelper.GetDate(dr, "BillProcDate");
                    pt.PrintDt = DaHelper.GetDate(dr, "PrintDate");
                    pt.FromBillSeqNo = DaHelper.GetInt(dr, "FromBillSeqNo");
                    pt.ToBillSeqNo = DaHelper.GetInt(dr, "ToBillSeqNo");
                    pt.BillCount = DaHelper.GetInt(dr, "BillCount");
                    pt.TotalAmount = DaHelper.GetDecimal(dr, "TotalAmount");
                    pt.PrintedBy = (DaHelper.GetString(dr, "PrintedBy").TrimStart('0')).ToUpper();
                    _reportItem.Add(pt);
                }
            }

            return _reportItem;
        }
        
        //5.10.9
        public List<ReportBillingStatus> GetBillingStatusReport(string printBranch, string billPeriod, int? printType, 
                                                                    string branchId, string fromMruId, string toMruId, string portionNo)
        {
            List<ReportBillingStatus> reportItem = new List<ReportBillingStatus>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_sel_reportBillingStatus");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintType", DbType.Int16, printType); 
            db.AddInParameter(cmd, "PrintBranch", DbType.String, printBranch);
            db.AddInParameter(cmd, "BillPred", DbType.String, billPeriod);             
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "FromMruId", DbType.String, fromMruId);
            db.AddInParameter(cmd, "ToMruId", DbType.String, toMruId);
            db.AddInParameter(cmd, "PortionNo", DbType.String, portionNo);
            DataSet ds = db.ExecuteDataSet(cmd);
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ReportBillingStatus pt = new ReportBillingStatus();
                    pt.BranchId = DaHelper.GetString(dr, "BranchId");
                    pt.BranchName = DaHelper.GetString(dr, "BranchName");
                    pt.MruId = DaHelper.GetString(dr, "MruId");
                    pt.PortionId = DaHelper.GetString(dr, "PortionId");
                    //pt.BillPred = DaHelper.GetString(dr, "BillPred");
                    pt.BaseCount = DaHelper.GetInt(dr, "BaseCount");
                    pt.ProcCount = DaHelper.GetInt(dr, "ProcCount");
                    pt.ActReceived = DaHelper.GetInt(dr, "ActReceived");
                    //pt.TypeCount = DaHelper.GetInt(dr, "TypeCount");
                    pt.PrintCount = DaHelper.GetInt(dr, "PrintCount");

                    pt.NBillCount = DaHelper.GetInt(dr, "NBillCount");
                    pt.FBillCount = DaHelper.GetInt(dr, "FBillCount");
                    pt.NA4AddCount = DaHelper.GetInt(dr, "NA4AddCount");
                    pt.FA4AddCount = DaHelper.GetInt(dr, "FA4AddCount");

                    reportItem.Add(pt);
                }
            }

            return reportItem;
        }

        //obsolete
        public List<ReportCAUnprint> GetCAUnprintReport(string bankId, string dueDate, string mtNo)
        {
            List<ReportCAUnprint> reportItem = new List<ReportCAUnprint>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_sel_reportCAUnprint");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "BankId", DbType.String, bankId);
            db.AddInParameter(cmd, "DueDate", DbType.String, dueDate);
            db.AddInParameter(cmd, "MtNo", DbType.String, mtNo);
            DataSet ds = db.ExecuteDataSet(cmd);
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ReportCAUnprint pt = new ReportCAUnprint();
                    pt.BankId = DaHelper.GetString(dr, "BankId");
                    pt.BankName = DaHelper.GetString(dr, "BankName");
                    pt.CaId = DaHelper.GetString(dr, "CaId");
                    pt.Crsg = DaHelper.GetString(dr, "Crsg");
                    pt.DueDate = DaHelper.GetString(dr, "DueDate");
                    pt.FileName = DaHelper.GetString(dr, "FileName");
                    pt.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    pt.MruId = DaHelper.GetString(dr, "MruId");
                    pt.MtNo = DaHelper.GetString(dr, "MtNo");
                    pt.PmId = DaHelper.GetString(dr, "PmId");
                    pt.PrintDoc = DaHelper.GetString(dr, "PrintDoc");
                    pt.TotalAmt = DaHelper.GetDecimal(dr, "TotalAmt");
                    pt.Trsg = DaHelper.GetString(dr, "Trsg");
                    reportItem.Add(pt);
                }
            }

            return reportItem;
        }
        
        //5.10.10
        public List<ReportDirectDebitStatus> GetDirectDebitStatusReport(ReportConditionParam param)
        {
            List<ReportDirectDebitStatus> reportItem = new List<ReportDirectDebitStatus>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_sel_reportDirectDebitStatus");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "pBranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "pPeriod", DbType.String, param.BillPeriod);
            db.AddInParameter(cmd, "pFromMru", DbType.String, param.MruId);
            db.AddInParameter(cmd, "pToMru", DbType.String, param.ToMruId);
            db.AddInParameter(cmd, "pPrintBranch", DbType.String, param.PrintBranch);
            db.AddInParameter(cmd, "pPrintedFlag", DbType.String, param.PrintedFlag);
            DataSet ds = db.ExecuteDataSet(cmd);
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ReportDirectDebitStatus pt = new ReportDirectDebitStatus();
                    pt.ReceiptId = DaHelper.GetString(dr, "ReceiptNo");
                    pt.BranchId = DaHelper.GetString(dr, "BranchId");
                    pt.BranchName = DaHelper.GetString(dr, "BranchName");
                    pt.MruId = DaHelper.GetString(dr, "MruId");
                    pt.CaId = DaHelper.GetString(dr, "CaId");
                    pt.CaName = DaHelper.GetString(dr, "CaName");
                    pt.Period = DaHelper.GetString(dr, "Period");
                    pt.PaymentDate = DaHelper.GetString(dr, "PaymentDate");
                    pt.PrintDt = DaHelper.GetDate(dr, "PrintDt");
                    string tmp = DaHelper.GetString(dr, "TotalAmount");
                    pt.TotalAmount = Convert.ToDecimal(tmp);
                    reportItem.Add(pt);
                }
            }

            return reportItem;
        }     


    }
}

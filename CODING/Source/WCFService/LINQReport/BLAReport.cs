using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using PEA.BPM.Architecture.CommonUtilities;

namespace BPMLINQReport
{
    public class BLAReport
    {
        public const int CMD_TIMEOUT = 360;

        public static DateTime WrapTime(DateTime? date, string time)
        {
            //default to floor
            int hours = 0;
            int minutes = 0;
            int seconds = 0;

            if (time != null)
            {
                try
                {
                    int h = Convert.ToInt32(time.Substring(0, 2));
                    int m = Convert.ToInt32(time.Substring(2, 2));
                    int s = Convert.ToInt32(time.Substring(4, 2));
                    hours = h; minutes = m; seconds = s;
                }
                catch (Exception) { }
            }

            return new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, hours, minutes, seconds, 0);
        }

        //5.10.1
        public List<ReportStreetRoute> GetStreetRouteReport(string period, string branchId, string printBranch, string portionNo)
        {
            List<ReportStreetRoute> report = new List<ReportStreetRoute>();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_sel_reportStreetRouteX");
            db.AddInParameter(cmd, "BillPred", DbType.String, period);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "PrintBranch", DbType.String, printBranch);
            cmd.CommandTimeout = CMD_TIMEOUT;
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable repDs = ds.Tables[0];
            DataTable repDx = ds.Tables[1];

            var repData = from rep in repDs.AsEnumerable()
                          select new
                          {
                              w_01_print_doc = rep.Field<string>("w_01_print_doc"),
                              w_1860_trsg = rep.Field<string>("w_1860_trsg"),
                              w_1840_mru = rep.Field<string>("w_1840_mru"),
                              w_2040_portion = rep.Field<string>("w_2040_portion"),
                              w_2050_mdenr = rep.Field<string>("w_2050_mdenr"),
                              w_1990_addition = rep.Field<string>("w_1990_addition"),
                              PrintType = rep.Field<byte?>("PrintType"),
                              GppPrintType = rep.Field<byte?>("GppPrintType"),
                              PrintDoc = rep.Field<string>("PrintDoc"),
                              GppPrintDoc = rep.Field<string>("GppPrintDoc")
                          };

            var incData = from rep in repDx.AsEnumerable()
                          select new
                          {
                              BranchId = rep.Field<string>("BranchId"),
                              BranchName = rep.Field<string>("BranchName"),
                              MruId = rep.Field<string>("MruId"),
                              Portion = rep.Field<string>("Portion"),
                              CaTotal = rep.Field<int?>("CaTotal"),
                              PtcNo = rep.Field<string>("PtcNo")
                          };


            var repQuery = from m in
                               (
                                             from r in repData
                                             where r.w_2040_portion == (portionNo == null ? r.w_2040_portion : portionNo)
                                             group r by new { r.w_01_print_doc, r.w_1860_trsg, r.w_1840_mru, r.w_2040_portion, r.w_2050_mdenr } into rg
                                             select new
                                             {
                                                 w_01_print_doc = rg.Key.w_01_print_doc,
                                                 w_1860_trsg = rg.Key.w_1860_trsg,
                                                 w_1840_mru = rg.Key.w_1840_mru,
                                                 w_2040_portion = rg.Key.w_2040_portion,
                                                 w_2050_mdenr = rg.Key.w_2050_mdenr
                                             })
                           group m by new { m.w_1860_trsg, m.w_1840_mru, m.w_2040_portion } into mg
                           select new
                           {
                               BranchId = mg.Key.w_1860_trsg,
                               MruId = mg.Key.w_1840_mru,
                               Portion = mg.Key.w_2040_portion,
                               ReadPtc = mg.Sum(m => m.w_2050_mdenr.Substring(0, 1) == "1" ? 1 : 0),
                               ReadSpotBill = mg.Sum(m => m.w_2050_mdenr.Substring(0, 1) == "2" ? 1 : 0),
                               ReadAMR = mg.Sum(m => m.w_2050_mdenr.Substring(0, 1) == "9" ? 1 : 0),
                               ReadOther = mg.Sum(m => (m.w_2050_mdenr.Substring(0, 1) != "1" && m.w_2050_mdenr.Substring(0, 1) != "2" && m.w_2050_mdenr.Substring(0, 1) != "9") ? 1 : 0),
                               TotBill = mg.Count()
                           };


            var nopQuery = from m in
                               (
                                             from r in repData
                                             where r.w_2040_portion == (portionNo == null ? r.w_2040_portion : portionNo)
                                                         && r.PrintDoc == null
                                                         && r.GppPrintDoc == null
                                             group r by new { r.w_01_print_doc, r.w_1860_trsg, r.w_1840_mru, r.w_2040_portion } into rg
                                             select new
                                             {
                                                 w_01_print_doc = rg.Key.w_01_print_doc,
                                                 w_1860_trsg = rg.Key.w_1860_trsg,
                                                 w_1840_mru = rg.Key.w_1840_mru,
                                                 w_2040_portion = rg.Key.w_2040_portion
                                             })
                           group m by new { m.w_1860_trsg, m.w_1840_mru, m.w_2040_portion } into mg
                           select new
                           {
                               BranchId = mg.Key.w_1860_trsg,
                               MruId = mg.Key.w_1840_mru,
                               Portion = mg.Key.w_2040_portion,
                               TotNoPrint = mg.Count()
                           };

            var printQuery = from r in repData
                             where r.w_2040_portion == (portionNo == null ? r.w_2040_portion : portionNo)
                                             && (r.PrintDoc != null || r.GppPrintDoc != null)
                             group r by new { r.w_1860_trsg, r.w_1840_mru, r.w_2040_portion } into rg
                             select new
                             {
                                 BranchId = rg.Key.w_1860_trsg,
                                 MruId = rg.Key.w_1840_mru,
                                 Portion = rg.Key.w_2040_portion,
                                 TotA4 = rg.Sum(r => (r.PrintType == 0 && r.w_1990_addition == "") ? 1 : 0),
                                 TotBlue = rg.Sum(r => r.PrintType == 1 ? 1 : 0),
                                 TotGreen = rg.Sum(r => r.GppPrintType == 2 ? 1 : 0),
                                 TotNSpotBill = rg.Sum(r => r.PrintType == 5 ? 1 : 0),
                                 TotA4Addition = rg.Sum(r => (r.PrintType == 0 && r.w_1990_addition == "X") ? 1 : 0)
                             };

            var repResult = from r in incData
                            join a in repQuery on new { BranchId = r.BranchId, MruId = r.MruId, Portion = r.Portion } equals new { BranchId = a.BranchId, MruId = a.MruId, Portion = a.Portion } into ag
                            join b in nopQuery on new { BranchId = r.BranchId, MruId = r.MruId, Portion = r.Portion } equals new { BranchId = b.BranchId, MruId = b.MruId, Portion = b.Portion } into bg
                            join p in printQuery on new { BranchId = r.BranchId, MruId = r.MruId, Portion = r.Portion } equals new { BranchId = p.BranchId, MruId = p.MruId, Portion = p.Portion } into pg
                            from ia in ag.DefaultIfEmpty()   // left join 
                            from ib in bg.DefaultIfEmpty()
                            from ip in pg.DefaultIfEmpty()
                            where r.Portion == (portionNo == null ? r.Portion : portionNo)
                            orderby r.BranchId, r.MruId, r.Portion
                            select new
                            {
                                r.BranchId,
                                r.BranchName,
                                r.MruId,
                                PortionNo = r.Portion,
                                Total = r.CaTotal,
                                r.PtcNo,
                                ReadPtc = (ia == null ? 0 : ia.ReadPtc),
                                ReadSpotBill = (ia == null ? 0 : ia.ReadSpotBill),
                                ReadAMR = (ia == null ? 0 : ia.ReadAMR),
                                ReadOther = (ia == null ? 0 : ia.ReadOther),
                                TotBlue = (ip == null ? 0 : ip.TotBlue),
                                TotGreen = (ip == null ? 0 : ip.TotGreen),
                                TotA4 = (ip == null ? 0 : ip.TotA4),
                                TotNSpotBill = (ip == null ? 0 : ip.TotNSpotBill),
                                TotNoPrint = (ib == null ? 0 : ib.TotNoPrint),
                                TotA4Addition = (ip == null ? 0 : ip.TotA4Addition),
                                TotBill = (ia == null ? 0 : ia.TotBill),
                            };

            foreach (var q in repResult)
            {
                ReportStreetRoute detail = new ReportStreetRoute();
                detail.BranchId = q.BranchId;
                detail.BranchName = q.BranchName;
                detail.MruId = q.MruId;
                detail.PortionNo = q.PortionNo;
                detail.Total = q.Total;
                detail.PtcNo = q.PtcNo;
                detail.ReadPtc = q.ReadPtc;
                detail.ReadOther = q.ReadOther;
                detail.TotBlue = q.TotBlue;
                detail.TotGreen = q.TotGreen;
                detail.TotA4 = q.TotA4;
                detail.TotNSpotBill = q.TotNSpotBill;
                detail.ReadSpotBill = q.ReadSpotBill;
                detail.ReadAMR = q.ReadAMR;
                detail.TotNoPrint = q.TotNoPrint;
                detail.TotA4Addition = q.TotA4Addition;
                detail.TotBill = q.TotBill;
                report.Add(detail);
            }

            return report;
        }

        //5.10.2
        public List<ReportStreetRouteReceive> GetStreetRouteReceiveReport(string printBranch, string branchId, string portionNo, DateTime? dataReceiveDt, DateTime? toDataReceiveDt, string fromTime, string toTime)
        {
            dataReceiveDt = WrapTime(dataReceiveDt, fromTime);
            toDataReceiveDt = WrapTime(toDataReceiveDt, toTime);

            List<ReportStreetRouteReceive> report = new List<ReportStreetRouteReceive>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_sel_reportStreetRouteReceiveX");
            cmd.CommandTimeout = 360;
            db.AddInParameter(cmd, "DataReceiveDt", DbType.DateTime, dataReceiveDt);
            db.AddInParameter(cmd, "ToDataReceiveDt", DbType.DateTime, toDataReceiveDt);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "PrintBranch", DbType.String, printBranch);
            cmd.CommandTimeout = 360;
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable repDs = ds.Tables[0];

            var repData = from rep in repDs.AsEnumerable()
                          select new
                          {
                              PrintDoc = rep.Field<string>("PrintDoc"),
                              BranchId = rep.Field<string>("BranchId"),
                              BranchName = rep.Field<string>("BranchName"),
                              MruId = rep.Field<string>("MruId"),
                              BillPred = rep.Field<string>("BillPred"),
                              Portion = rep.Field<string>("Portion"),
                              PrintType = rep.Field<byte>("PrintType"),
                              Reverse = rep.Field<byte>("Reverse"),
                              A4Addition = rep.Field<string>("A4Addition").Trim(),
                              CaTotal = rep.Field<int>("CaTotal")
                          };

            var repQuery = from b in repData
                           where b.Portion == (portionNo == null ? b.Portion : portionNo)
                           group b by new { b.BranchId, b.BranchName, b.MruId, b.BillPred, b.Portion } into bg
                           orderby bg.Key.BillPred, bg.Key.BranchId, bg.Key.MruId, bg.Key.Portion
                           select new
                           {
                               bg.Key.BranchId,
                               bg.Key.BranchName,
                               bg.Key.MruId,
                               bg.Key.BillPred,
                               PortionNo = bg.Key.Portion,
                               CaTotal = bg.Min(b => b.CaTotal),
                               NBlue = bg.Sum(b => (b.PrintType == 1 && b.Reverse == 0) ? 1 : 0),
                               FBlue = bg.Sum(b => (b.PrintType == 1 && b.Reverse == 1) ? 1 : 0),
                               TotBlue = bg.Sum(b => (b.PrintType == 1 && b.Reverse == 0) ? 1 : 0) + bg.Sum(b => (b.PrintType == 1 && b.Reverse == 1) ? 1 : 0),
                               NGreen = bg.Sum(b => ((b.PrintType == 2 || b.PrintType == 4) && b.Reverse == 0) ? 1 : 0),
                               FGreen = bg.Sum(b => ((b.PrintType == 2 || b.PrintType == 4) && b.Reverse == 1) ? 1 : 0),
                               TotGreen = bg.Sum(b => ((b.PrintType == 2 || b.PrintType == 4) && b.Reverse == 0) ? 1 : 0) + bg.Sum(b => ((b.PrintType == 2 || b.PrintType == 4) && b.Reverse == 1) ? 1 : 0),
                               NA4 = bg.Sum(b => (b.PrintType == 0 && b.Reverse == 0 && b.A4Addition == "") ? 1 : 0),
                               FA4 = bg.Sum(b => (b.PrintType == 0 && b.Reverse == 1 && b.A4Addition == "") ? 1 : 0),
                               TotA4 = bg.Sum(b => (b.PrintType == 0 && b.Reverse == 0 && b.A4Addition == "") ? 1 : 0) + bg.Sum(b => (b.PrintType == 0 && b.Reverse == 1 && b.A4Addition == "") ? 1 : 0),
                               NSpotBill = bg.Sum(b => (b.PrintType == 5 && b.Reverse == 0) ? 1 : 0),
                               FSpotBill = bg.Sum(b => (b.PrintType == 5 && b.Reverse == 1) ? 1 : 0),
                               TotSpotBill = bg.Sum(b => (b.PrintType == 5 && b.Reverse == 0) ? 1 : 0) + bg.Sum(b => (b.PrintType == 5 && b.Reverse == 1) ? 1 : 0),
                               NA4Add = bg.Sum(b => (b.PrintType == 0 && b.Reverse == 0 && b.A4Addition == "X") ? 1 : 0),
                               FA4Add = bg.Sum(b => (b.PrintType == 0 && b.Reverse == 1 && b.A4Addition == "X") ? 1 : 0),
                               TotA4Add = bg.Sum(b => (b.PrintType == 0 && b.Reverse == 0 && b.A4Addition == "X") ? 1 : 0) + bg.Sum(b => (b.PrintType == 0 && b.Reverse == 1 && b.A4Addition == "X") ? 1 : 0),
                               TotNBill = bg.Sum(b => (b.PrintType == 1 && b.Reverse == 0) ? 1 : 0) + bg.Sum(b => ((b.PrintType == 2 || b.PrintType == 4) && b.Reverse == 0) ? 1 : 0) + bg.Sum(b => (b.PrintType == 0 && b.Reverse == 0 && b.A4Addition == "") ? 1 : 0) + bg.Sum(b => (b.PrintType == 5 && b.Reverse == 0) ? 1 : 0),
                               TotFBill = bg.Sum(b => (b.PrintType == 1 && b.Reverse == 1) ? 1 : 0) + bg.Sum(b => ((b.PrintType == 2 || b.PrintType == 4) && b.Reverse == 1) ? 1 : 0) + bg.Sum(b => (b.PrintType == 0 && b.Reverse == 1 && b.A4Addition == "") ? 1 : 0) + bg.Sum(b => (b.PrintType == 5 && b.Reverse == 1) ? 1 : 0),
                               TotBill = bg.Sum(b => (b.PrintType == 1 && b.Reverse == 0) ? 1 : 0) + bg.Sum(b => ((b.PrintType == 2 || b.PrintType == 4) && b.Reverse == 0) ? 1 : 0) + bg.Sum(b => (b.PrintType == 0 && b.Reverse == 0 && b.A4Addition == "") ? 1 : 0) + bg.Sum(b => (b.PrintType == 5 && b.Reverse == 0) ? 1 : 0) + bg.Sum(b => (b.PrintType == 1 && b.Reverse == 1) ? 1 : 0) + bg.Sum(b => ((b.PrintType == 2 || b.PrintType == 4) && b.Reverse == 1) ? 1 : 0) + bg.Sum(b => (b.PrintType == 0 && b.Reverse == 1 && b.A4Addition == "") ? 1 : 0) + bg.Sum(b => (b.PrintType == 5 && b.Reverse == 1) ? 1 : 0)
                           };


            foreach (var q in repQuery)
            {
                ReportStreetRouteReceive detail = new ReportStreetRouteReceive();
                detail.BillPred = q.BillPred;
                detail.BranchId = q.BranchId;
                detail.BranchName = q.BranchName;
                detail.MruId = q.MruId;
                detail.PortionNo = q.PortionNo;
                detail.Total = q.CaTotal;
                detail.NormalBlue = q.NBlue;
                detail.FixBlue = q.FBlue;
                detail.TotBlue = q.TotBlue;
                detail.NormalGreen = q.NGreen;
                detail.FixGreen = q.FGreen;
                detail.TotGreen = q.TotGreen;
                detail.NormalA4 = q.NA4;
                detail.FixA4 = q.FA4;
                detail.TotA4 = q.TotA4;
                detail.NormalSpotBill = q.NSpotBill;
                detail.FixSpotBill = q.FSpotBill;
                detail.TotSpotBill = q.TotSpotBill;
                detail.NormalA4Add = q.NA4Add;
                detail.FixA4Add = q.FA4Add;
                detail.TotA4Add = q.TotA4Add;
                detail.TotNBill = q.TotNBill;
                detail.TotFBill = q.TotFBill;
                detail.TotBill = q.TotBill;
                report.Add(detail);
            }

            return report;
        }

        //5.10.3
        public List<ReportDailyPrint> GetDailyPrintReport(DateTime? printDt, string branchId, string printBranch, int? printType)
        {
            List<ReportDailyPrint> report = new List<ReportDailyPrint>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_sel_reportDailyPrintX");
            cmd.CommandTimeout = 360;
            db.AddInParameter(cmd, "PrintDate", DbType.DateTime, printDt);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "PrintBranch", DbType.String, printBranch);
            db.AddInParameter(cmd, "PrintType", DbType.Int16, printType);
            DataTable repDs = db.ExecuteDataSet(cmd).Tables[0];

            var repData = from rep in repDs.AsEnumerable()
                          select new
                          {
                              BranchId = rep.Field<string>("BranchId"),
                              BranchName = rep.Field<string>("BranchName"),
                              PrintDate = rep.Field<DateTime?>("PrintDate"),
                              MruId = rep.Field<string>("MruId"),
                              BillSeqNo = rep.Field<int>("BillSeqNo"),
                              PrintType = rep.Field<byte>("PrintType"),
                              PrintSubType = rep.Field<byte>("PrintSubType"),
                              ReceiptNo = rep.Field<string>("ReceiptNo"),
                              PrintLog = rep.Field<int>("PrintLog")
                          };

            var repQuery = from i in
                               (
                                   from b in repData
                                   //where b.PrintType == printType || b.PrintType == subType
                                   group b by new { b.BranchId, b.BranchName, b.MruId, b.PrintDate, b.PrintType, b.PrintSubType, b.PrintLog } into bg
                                   select new
                                   {
                                       bg.Key.BranchId,
                                       bg.Key.BranchName,
                                       bg.Key.PrintDate,
                                       bg.Key.MruId,
                                       BillSeqNoFrom = bg.Min(b => b.BillSeqNo),
                                       BillSeqNoTo = bg.Max(b => b.BillSeqNo),
                                       PrintCount = bg.Count(),
                                       bg.Key.PrintType,
                                       bg.Key.PrintSubType,
                                       InvoiceFrom = bg.Min(b => b.ReceiptNo),
                                       InvoiceTo = bg.Max(b => b.ReceiptNo),
                                       //TotalBill = 0,
                                       BillFlag = "0",
                                       bg.Key.PrintLog
                                   })
                           orderby i.BranchId, i.BillSeqNoFrom, i.MruId, i.PrintType, i.PrintLog
                           select i;

            foreach (var q in repQuery)
            {
                ReportDailyPrint detail = new ReportDailyPrint();
                detail.BranchId = q.BranchId;
                detail.BranchName = q.BranchName;
                detail.BillPeriod = q.PrintDate;
                detail.MruId = q.MruId;
                detail.BillSeqNoFrom = q.BillSeqNoFrom;
                detail.BillSeqNoTo = q.BillSeqNoTo;
                detail.PrintCount = q.PrintCount;
                detail.PrintType = q.PrintType.ToString();
                detail.PrintSubType = q.PrintSubType.ToString();
                detail.InvoiceFrom = q.InvoiceFrom;
                detail.InvoiceTo = q.InvoiceTo;
                //detail.TotBill = q.TotalBill;
                detail.BillFlag = q.BillFlag;
                detail.PrintLog = q.PrintLog;
                report.Add(detail);
            }


            return report;
        }

        //5.10.4
        public List<ReportDailyUnprint> GetDailyUnprintReport(string period, string portionId, string branchId, string printBranch)
        {
            List<ReportDailyUnprint> report = new List<ReportDailyUnprint>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_sel_reportDailyUnprintX");
            cmd.CommandTimeout = 360;
            db.AddInParameter(cmd, "BillPred", DbType.String, period);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "PrintBranch", DbType.String, printBranch);
            DataTable repDs = db.ExecuteDataSet(cmd).Tables[0];

            var repData = from rep in repDs.AsEnumerable()
                          select new
                          {
                              InvoiceNo = rep.Field<string>("PrintDoc"),
                              BranchId = rep.Field<string>("BranchId"),
                              BranchName = rep.Field<string>("BranchName"),
                              MruId = rep.Field<string>("MruId"),
                              Portion = rep.Field<string>("Portion"),
                              PrintType = rep.Field<byte>("PrintType"),
                              TotBill = rep.Field<int>("TotBill"),
                              A4Addition = rep.Field<string>("A4Addition")
                          };

            var repQuery = from m in
                               (
                                           from b in repData
                                           where b.Portion == (portionId == null ? b.Portion : portionId)
                                           //&& b.BranchId == (branchId == null ? b.BranchId : branchId)
                                           group b by new { b.BranchId, b.BranchName, b.MruId, b.Portion } into bg
                                           select new
                                           {
                                               bg.Key.BranchId,
                                               bg.Key.BranchName,
                                               bg.Key.MruId,
                                               PortionNo = bg.Key.Portion,
                                               TotBill = bg.Min(b => b.TotBill),
                                               A4Bill = bg.Sum(b => (b.PrintType == 0 && (b.A4Addition == "" || b.A4Addition == null)) ? 1 : 0),
                                               BlueBill = bg.Sum(b => b.PrintType == 1 ? 1 : 0),
                                               GreenBill = bg.Sum(b => b.PrintType == 2 ? 1 : 0),
                                               SpotBill = bg.Sum(b => b.PrintType == 5 ? 1 : 0),
                                               A4Addition = bg.Sum(b => b.A4Addition == "X" ? 1 : 0),
                                               TotNoPrint = bg.Sum(b => (b.PrintType == 0 && (b.A4Addition == "" || b.A4Addition == null)) ? 1 : 0) + bg.Sum(b => b.PrintType == 1 ? 1 : 0) + bg.Sum(b => b.PrintType == 2 ? 1 : 0) + bg.Sum(b => b.PrintType == 5 ? 1 : 0) + bg.Sum(b => b.A4Addition == "X" ? 1 : 0)
                                           })
                           where m.A4Bill + m.BlueBill + m.GreenBill + m.SpotBill + m.A4Addition > 0
                           orderby m.BranchId, m.MruId, m.PortionNo
                           select m;

            foreach (var q in repQuery)
            {
                ReportDailyUnprint detail = new ReportDailyUnprint();
                detail.BranchId = q.BranchId;
                detail.BranchName = q.BranchName;
                detail.PortionNo = q.PortionNo;
                detail.MruId = q.MruId;
                detail.TotBill = q.TotBill;
                detail.A4Bill = q.A4Bill;
                detail.BlueBill = q.BlueBill;
                detail.GreenBill = q.GreenBill;
                detail.SpotBill = q.SpotBill;
                detail.A4Addition = q.A4Addition;
                detail.TotNoPrint = q.TotNoPrint;
                report.Add(detail);
            }

            return report;
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

                if (printType == 6)
                    cmd = db.GetStoredProcCommand("bp_sel_reportBillDelivery_Receipt");
                else
                    cmd = db.GetStoredProcCommand("bp_sel_reportBillDelivery");

                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "BillFlag", DbType.String, billFlag);
                db.AddInParameter(cmd, "SentPred", DbType.String, period);
                db.AddInParameter(cmd, "SentPred_Log", DbType.Int16, log);
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "SentBranch", DbType.String, sentBranch);
                db.AddInParameter(cmd, "DeliveryPlace", DbType.String, deliveryPlace);
                db.AddInParameter(cmd, "PrintedBy", DbType.String, printedBy);
                db.AddInParameter(cmd, "SaveRecord", DbType.String, save ? "1" : "0");
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

            if (prec < 0)
            {
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
        public List<ReportGrpInvSummary> GetGrpInvSummaryReport(int? printCondition, string period, string printBranch, string mtNo, string paidBy)
        {
            List<ReportGrpInvSummary> report = new List<ReportGrpInvSummary>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_sel_reportGrpInvSummaryX");
            cmd.CommandTimeout = 360;
            //db.AddInParameter(cmd, "PrintCondition", DbType.String, printCondition.ToString());
            db.AddInParameter(cmd, "DuePeriod", DbType.String, period);
            db.AddInParameter(cmd, "PrintBranch", DbType.String, printBranch);
            db.AddInParameter(cmd, "MtNo", DbType.String, mtNo);
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable printedDs = ds.Tables[0];
            DataTable infoDs = ds.Tables[1];

            var repData = from rep in printedDs.AsEnumerable()
                          select new
                          {
                              PrintBranch = rep.Field<string>("PrintBranch"),
                              Payer = rep.Field<string>("Payer"),
                              PayerName = rep.Field<string>("PayerName"),
                              MtNo = rep.Field<string>("MtNo"),
                              BranchId = rep.Field<string>("BranchId"),
                              MruId = rep.Field<string>("MruId"),
                              PrintDate = rep.Field<DateTime?>("PrintDate"),
                              PrintedBy = rep.Field<string>("PrintedBy")
                          };

            var infoData = from rep in infoDs.AsEnumerable()
                           select new
                           {
                               PrintBranch = rep.Field<string>("PrintBranch"),
                               Payer = rep.Field<string>("Payer"),
                               MtNo = rep.Field<string>("MtNo"),
                               BranchId = rep.Field<string>("BranchId"),
                               MruId = rep.Field<string>("MruId"),
                               GroupingDate = rep.Field<string>("GroupingDate"),
                               TotAmount = rep.Field<decimal>("TotAmount")
                           };

            var repQuery = from b in repData
                           group b by new { b.PrintBranch, b.Payer, b.PayerName, b.MtNo, b.BranchId, b.MruId, b.PrintDate, b.PrintedBy } into bg
                           select new
                           {
                               bg.Key.PrintBranch,
                               bg.Key.Payer,
                               bg.Key.PayerName,
                               bg.Key.MtNo,
                               bg.Key.BranchId,
                               bg.Key.MruId,
                               bg.Key.PrintDate,
                               bg.Key.PrintedBy,
                               PrintCount = bg.Count()
                           };

            var infoQuery = from b in infoData
                            group b by new { b.PrintBranch, b.Payer, b.MtNo, b.BranchId, b.MruId, b.GroupingDate } into bg
                            select new
                            {
                                bg.Key.PrintBranch,
                                bg.Key.Payer,
                                bg.Key.MtNo,
                                bg.Key.BranchId,
                                bg.Key.MruId,
                                bg.Key.GroupingDate,
                                TotAmount = bg.Sum(b => b.TotAmount),
                                TotBill = bg.Count()
                            };

            var reportQuery = from p in repQuery
                              join i in infoQuery on new { PrintBranch = p.PrintBranch, BranchId = p.BranchId, MruId = p.MruId, MtNo = p.MtNo } equals new { PrintBranch = i.PrintBranch, BranchId = i.BranchId, MruId = i.MruId, MtNo = i.MtNo }
                              orderby p.MtNo, p.PrintBranch, p.Payer, p.BranchId, p.MruId, i.TotAmount
                              select new
                              {
                                  p.Payer,
                                  p.PayerName,
                                  p.MtNo,
                                  p.BranchId,
                                  p.MruId,
                                  i.GroupingDate,
                                  p.PrintDate,
                                  i.TotAmount,
                                  i.TotBill,
                                  p.PrintCount,
                                  RemainCount = (i.TotBill - p.PrintCount),
                                  p.PrintedBy
                              };


            foreach (var q in reportQuery)
            {
                ReportGrpInvSummary detail = new ReportGrpInvSummary();
                detail.Payer = q.Payer.TrimStart('0');
                detail.PayerName = q.PayerName;
                detail.MtNo = q.MtNo;
                detail.BranchId = q.BranchId;
                //detail.PrintBranch = q.PrintBranch;
                //detail.BranchName = q.BranchName;
                detail.MruId = q.MruId;

                string tmp = q.GroupingDate;
                tmp = tmp.Substring(6, 2) + tmp.Substring(4, 2) + tmp.Substring(0, 4);
                detail.GroupDate = tmp;
                detail.PrintDt = q.PrintDate;
                detail.TotAmount = q.TotAmount;
                detail.TotBill = q.TotBill;
                detail.PrintCount = q.PrintCount;
                detail.RemainCount = q.RemainCount;
                detail.PrintedBy = q.PrintedBy.TrimStart('0').ToUpper();
                report.Add(detail);
            }

            return report;
        }

        //5.10.8
        public List<ReportPrintByBank> GetPrintGreenByBankReport(string printBranch, DateTime? fromDate, DateTime? toDate, string bankId, int? printType)
        {
            List<ReportPrintByBank> report = new List<ReportPrintByBank>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_sel_reportPrintByBankX");
            cmd.CommandTimeout = 360;
            db.AddInParameter(cmd, "PrintType", DbType.Int16, printType); //green by bank
            db.AddInParameter(cmd, "PrintBranch", DbType.String, printBranch); //printbranch
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, fromDate);
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, toDate);
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable repDs = ds.Tables[0];

            var repData = from rep in repDs.AsEnumerable()
                          select new
                          {
                              PrintDoc = rep.Field<string>("PrintDoc"),
                              BankId = rep.Field<string>("BankId"),
                              BankKey = rep.Field<string>("BankKey"),
                              BankName = rep.Field<string>("BankName"),
                              BranchId = rep.Field<string>("BranchId"),
                              MruId = rep.Field<string>("MruId"),
                              BillProcDate = rep.Field<DateTime?>("BillProcDate"),
                              PrintDate = rep.Field<DateTime?>("PrintDate"),
                              BillSeqNo = rep.Field<int>("BillSeqNo"),
                              TotalAmount = rep.Field<decimal?>("TotalAmount"),
                              PrintedBy = rep.Field<string>("PrintedBy")
                          };


            var repQuery = from i in
                               (
                                       from b in repData
                                       where b.BankId == (bankId == null ? b.BankId : bankId)
                                       group b by new { b.BankKey, b.BankId, b.BankName, b.BranchId, b.PrintedBy } into bg
                                       select new
                                       {
                                           bg.Key.BankId,
                                           BankKey = bg.Key.BankKey.Trim(),
                                           bg.Key.BankName,
                                           bg.Key.BranchId,
                                           BillProcDate = bg.Max(b => b.BillProcDate),
                                           PrintDate = bg.Max(b => b.PrintDate),
                                           FromBillSeqNo = bg.Min(b => b.BillSeqNo),
                                           ToBillSeqNo = bg.Max(b => b.BillSeqNo),
                                           BillCount = bg.Count(),
                                           TotalAmount = bg.Sum(b => b.TotalAmount),
                                           bg.Key.PrintedBy
                                       })
                           orderby i.BankKey, i.BranchId, i.FromBillSeqNo, i.ToBillSeqNo, i.BillProcDate, i.PrintDate
                           select i;

            foreach (var q in repQuery)
            {
                ReportPrintByBank detail = new ReportPrintByBank();
                detail.BankId = q.BankId;
                detail.BankKey = q.BankKey;
                detail.BankName = q.BankName;
                detail.BranchId = q.BranchId;
                //detail.MruId = q.MruId;
                detail.BillProcDate = q.BillProcDate;
                detail.PrintDt = q.PrintDate;
                detail.FromBillSeqNo = q.FromBillSeqNo;
                detail.ToBillSeqNo = q.ToBillSeqNo;
                detail.BillCount = q.BillCount;
                detail.TotalAmount = q.TotalAmount;
                detail.PrintedBy = q.PrintedBy.TrimStart('0').ToUpper();
                report.Add(detail);
            };

            return report;
        }

        //5.10.9
        public List<ReportBillingStatus> GetBillingStatusReport(string printBranch, string billPeriod, int? printType, string branchId, string fromMruId, string toMruId, string portionNo)
        {
            string[] exceptPmId = { "D", "E", "F", "G" };

            List<ReportBillingStatus> report = new List<ReportBillingStatus>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_sel_reportBillingStatusX");
            cmd.CommandTimeout = 360;
            db.AddInParameter(cmd, "PrintType", DbType.Int16, printType);
            db.AddInParameter(cmd, "PrintBranch", DbType.String, printBranch);
            db.AddInParameter(cmd, "BillPred", DbType.String, billPeriod);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "FromMruId", DbType.String, fromMruId);
            db.AddInParameter(cmd, "ToMruId", DbType.String, toMruId);
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable repData = ds.Tables[0];
            DataTable addData = ds.Tables[1];

            var a4Data = from add in addData.AsEnumerable()
                         select new
                         {
                             PrintDoc = add.Field<string>("PrintDoc"),
                             BranchId = add.Field<string>("BranchId"),
                             MruId = add.Field<string>("MruId"),
                             Portion = add.Field<string>("Portion"),
                             Reverse = add.Field<byte?>("Reverse")
                         };

            var billData = from rep in repData.AsEnumerable()
                           select new
                           {
                               w_01_print_doc = rep.Field<string>("w_01_print_doc"),
                               w_1861_crsg = rep.Field<string>("w_1861_crsg"),
                               w_1860_trsg = rep.Field<string>("w_1860_trsg"),
                               BranchName = rep.Field<string>("BranchName"),
                               w_1840_mru = rep.Field<string>("w_1840_mru"),
                               //MruInt = rep.Field<int?>("MruInt"),
                               //BillPred = rep.Field<string>("BillPred"),
                               w_2040_portion = rep.Field<string>("w_2040_portion"),
                               w_1960_acct_class = rep.Field<string>("w_1960_acct_class"),
                               w_1830_payment_method = rep.Field<string>("w_1830_payment_method"),
                               w_2000_dispctrl = rep.Field<string>("w_2000_dispctrl"),
                               w_1980_spotbill = rep.Field<string>("w_1980_spotbill"),
                               w_2010_noprnt = rep.Field<string>("w_2010_noprnt"),
                               w_1990_addition = rep.Field<string>("w_1990_addition"),
                               w_1910_org_doc = rep.Field<string>("w_1910_org_doc"),
                               PrintType = rep.Field<byte?>("PrintType"),
                               PrintStatus = rep.Field<byte?>("PrintStatus"),
                               CaTotal = rep.Field<int?>("CaTotal"),
                               ProcCount = rep.Field<int?>("ProcCount"),
                           };

            var addResult = from a in a4Data
                            where a.Portion == (portionNo == null ? a.Portion : portionNo)
                            group a by new { a.BranchId, a.MruId, a.Portion } into ag
                            select new
                            {
                                BranchId = ag.Key.BranchId,
                                MruId = ag.Key.MruId,
                                Portion = ag.Key.Portion,
                                NA4AddCount = ag.Sum(a => a.Reverse == 0 ? 1 : 0),
                                FA4AddCount = ag.Sum(a => a.Reverse == 1 ? 1 : 0)
                            };

            var billResult = from p in billData
                             where p.w_2040_portion == (portionNo == null ? p.w_2040_portion : portionNo)
                             group p by new { p.w_1860_trsg, p.BranchName, p.w_1840_mru, p.w_2040_portion } into bg
                             select new
                             {
                                 BranchId = bg.Key.w_1860_trsg,
                                 BranchName = bg.Key.BranchName,
                                 MruId = bg.Key.w_1840_mru,
                                 PortionId = bg.Key.w_2040_portion,
                                 BaseCount = bg.Min(p => p.CaTotal),
                                 ProcCount = bg.Min(p => p.ProcCount),
                                 ActReceived = (
                                         printType == 0 ? bg.Sum(p => ((p.w_1960_acct_class.Substring(2, 1) == "B" && p.w_1830_payment_method == "C") || (p.w_2000_dispctrl == "A4" && !exceptPmId.Contains(p.w_1830_payment_method))) ? 1 : 0) :
                                         printType == 1 ? bg.Sum(p => (p.w_1830_payment_method == "B" && p.w_1980_spotbill == "" && p.w_2010_noprnt == "" && p.w_2000_dispctrl == "") ? 1 : 0) :
                                         printType == 2 ? bg.Sum(p => (p.w_1830_payment_method == "D" || p.w_1830_payment_method == "E") ? 1 : 0) :
                                         printType == 5 ? bg.Sum(p => (p.w_1830_payment_method == "C" && p.w_1960_acct_class.Substring(2, 1) == "S" && p.w_2000_dispctrl == "") ? 1 : 0) : 0),
                                 PrintCount = bg.Sum(p => (p.PrintType == printType && p.w_1990_addition.Trim() == "") ? 1 : 0),
                                 NBillCount = bg.Sum(p => (p.PrintType == printType && p.PrintStatus == 1 && p.w_1910_org_doc.Trim() == "" && p.w_1990_addition.Trim() == "") ? 1 : 0),
                                 FBillCount = bg.Sum(p => (p.PrintType == printType && p.PrintStatus == 1 && p.w_1910_org_doc.Trim() != "" && p.w_1990_addition.Trim() == "") ? 1 : 0)
                             };

            var repResult = from r in billResult
                            join a in addResult on new { BranchId = r.BranchId, MruId = r.MruId, Portion = r.PortionId } equals new { BranchId = a.BranchId, MruId = a.MruId, Portion = a.Portion } into ag
                            from ix in ag.DefaultIfEmpty()   // left join 
                            where (r.ActReceived + r.PrintCount + r.NBillCount + r.FBillCount + (ix == null ? 0 : ix.NA4AddCount) + (ix == null ? 0 : ix.FA4AddCount)) > 0
                            orderby r.BranchId, r.MruId, r.PortionId
                            select new
                            {
                                r.BranchId,
                                r.BranchName,
                                r.MruId,
                                r.PortionId,
                                r.BaseCount,
                                r.ProcCount,
                                r.ActReceived,
                                r.PrintCount,
                                r.NBillCount,
                                r.FBillCount,
                                NA4AddCount = (ix == null ? 0 : ix.NA4AddCount),
                                FA4AddCount = (ix == null ? 0 : ix.FA4AddCount)
                            };

            foreach (var q in repResult)
            {
                ReportBillingStatus detail = new ReportBillingStatus();
                detail.BranchId = q.BranchId;
                detail.BranchName = q.BranchName;
                detail.MruId = q.MruId;
                detail.PortionId = q.PortionId;
                detail.BaseCount = q.BaseCount;
                detail.ProcCount = q.ProcCount;
                detail.ActReceived = q.ActReceived;
                detail.PrintCount = q.PrintCount;
                detail.NBillCount = q.NBillCount;
                detail.FBillCount = q.FBillCount;
                detail.NA4AddCount = q.NA4AddCount;
                detail.FA4AddCount = q.FA4AddCount;
                report.Add(detail);
            }

            return report;
        }

        //5.10.10
        public List<ReportDirectDebitStatus> GetDirectDebitStatusReport(string printBranch, string branchId, string billPred, string fromMruId, string toMruId)
        {
            int fMruId, tMruId;
            if (fromMruId != null)
                fMruId = Convert.ToInt32(fromMruId);
            else
                fMruId = 0;

            if (toMruId != null)
                tMruId = Convert.ToInt32(toMruId);
            else
                tMruId = 9999;

            List<ReportDirectDebitStatus> report = new List<ReportDirectDebitStatus>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_sel_reportDirectDebitStatusX");
            cmd.CommandTimeout = 360;
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pPeriod", DbType.String, billPred);
            db.AddInParameter(cmd, "pPrintBranch", DbType.String, printBranch);
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable repDs = ds.Tables[0];

            var repData = from rep in repDs.AsEnumerable()
                          select new
                          {
                              ReceiptNo = rep.Field<string>("ReceiptNo"),
                              BranchId = rep.Field<string>("BranchId"),
                              BranchName = rep.Field<string>("BranchName"),
                              MruId = rep.Field<string>("MruId"),
                              MruInt = rep.Field<int>("MruInt"),
                              CaId = rep.Field<string>("CaId"),
                              CaName = rep.Field<string>("CaName"),
                              Period = rep.Field<string>("Period"),
                              PaymentDate = rep.Field<string>("PaymentDate"),
                              PrintDt = rep.Field<DateTime?>("PrintDt"),
                              TotalAmount = rep.Field<string>("TotalAmount")
                          };

            var repQuery = from b in repData
                           where b.MruInt >= fMruId && b.MruInt <= tMruId
                           group b by new { b.ReceiptNo, b.BranchId, b.BranchName, b.MruId, b.CaId, b.CaName, b.Period, b.PaymentDate, b.TotalAmount } into bg
                           orderby bg.Key.BranchId, bg.Key.MruId, bg.Key.CaId, bg.Key.ReceiptNo
                           select new
                           {
                               ReceiptNo = bg.Key.ReceiptNo.Trim(),
                               bg.Key.BranchId,
                               bg.Key.BranchName,
                               bg.Key.MruId,
                               bg.Key.CaId,
                               CaName = bg.Key.CaName.Trim(),
                               bg.Key.Period,
                               bg.Key.PaymentDate,
                               PrintDt = bg.Min(b => b.PrintDt),
                               bg.Key.TotalAmount
                           };

            foreach (var q in repQuery)
            {
                ReportDirectDebitStatus detail = new ReportDirectDebitStatus();
                detail.ReceiptId = q.ReceiptNo;
                detail.BranchId = q.BranchId;
                detail.BranchName = q.BranchName;
                detail.MruId = q.MruId;
                detail.CaId = q.CaId;
                detail.CaName = q.CaName;
                detail.Period = q.Period;
                detail.PaymentDate = q.PaymentDate;
                detail.PrintDt = q.PrintDt;
                detail.TotalAmount = Convert.ToDecimal(q.TotalAmount);
                report.Add(detail);
            }

            return report;
        }
    }
}

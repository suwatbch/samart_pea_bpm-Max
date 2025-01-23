using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Linq.SqlClient;
using System.Globalization;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;

namespace BPMLINQReport
{
    public class POSReport
    {
        public static DateTime WrapTime(string time, bool floor)
        {
            //default to floor
            int hours = 0;
            int minutes = 0;
            int seconds = 1;

            if (!floor)
            {
                hours = 23;
                minutes = 59;
                seconds = 59;
            }

            if (time != null)
            {
                try
                {
                    string[] sp = time.Split(':');
                    int h = Convert.ToInt32(sp[0]);
                    int m = Convert.ToInt32(sp[1]);
                    int s = Convert.ToInt32(sp[2]);
                    hours = h; minutes = m; seconds = s;
                }
                catch (Exception) { }
            }

            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hours, minutes, seconds, 0);
        }

        public List<CAC01Report> GetReportCAC01_1(string pBranchId, string pControllerId, DateTime? pFromDate)
        {
            List<CAC01Report> report = new List<CAC01Report>();
            //pControllerId = Convert.ToInt32(pControllerId).ToString();

            string[] exceptPmId = { "M", "N", "O" };
            string[] includedDtId = { "M01000002", "M90000010", "P00010001", "P00010002", "P00020001", "M02000002" };
            //201801311548 Kanokwan.L แก้ไขให้รายงานแสดงค่า M02000002 การวางบิลครั้งสุดท้าย ลูกหนี้

            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC01_1X");
            db.AddInParameter(cmd, "BranchId", DbType.String, pBranchId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, pFromDate);
            cmd.CommandTimeout = 360;
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable mainRaw = ds.Tables[0];

            var repData = from rep in mainRaw.AsEnumerable()
                          select new
                          {
                              PaymentId = rep.Field<string>("PaymentId"),
                              BranchId = rep.Field<string>("BranchId"),
                              BranchName = rep.Field<string>("BranchName"),
                              RBranchId = rep.Field<string>("RBranchId"),
                              ControllerId = rep.Field<string>("ControllerId"),
                              InvoiceNo = rep.Field<string>("InvoiceNo"),
                              ARPmId = rep.Field<string>("ARPmId"),
                              PmId = rep.Field<string>("PmId"),
                              ReceiptId = rep.Field<string>("ReceiptId"),
                              GAmount = rep.Field<decimal?>("GAmount"),
                              VatAmount = rep.Field<decimal?>("VatAmount"),
                              RGAmount = rep.Field<decimal?>("RGAmount"),
                              RVatAmount = rep.Field<decimal?>("RVatAmount"),
                              FTAmount = rep.Field<decimal?>("FTAmount"),
                              DtId = rep.Field<string>("DtId"),
                              RPaymentId = rep.Field<string>("RPaymentId"),
                              GroupInvoiceNo = rep.Field<string>("GroupInvoiceNo"),
                              Type = rep.Field<string>("Type"),
                              CancelARPmId = rep.Field<string>("CancelARPmId"),
                              CCancelARPmId = rep.Field<string>("CCancelARPmId"),
                              ARPaymentDt = rep.Field<DateTime?>("ARPaymentDt"),
                              CARPaymentDt = rep.Field<DateTime?>("CARPaymentDt"),
                              Active = rep.Field<string>("Active"),
                              Caid = rep.Field<string>("Caid")
                          };

            repData = (from p in repData
                       where p.Caid.Substring(0, 6) != "000004"
                       select p); //ไม่ให้รายงานแสดงค่าพาดสาย กรณีใบเสร็จรวม

            if (repData.Count() == 0) return report;

            DataTable subRaw = ds.Tables[1];

            var subData = from sub in subRaw.AsEnumerable()
                          select new
                          {
                              OriginalPaymentId = sub.Field<string>("OriginalPaymentId"),
                              GroupInvoiceNo = sub.Field<string>("GroupInvoiceNo"),
                              PaidGAmount = sub.Field<decimal?>("PaidGAmount"),
                              FTAmount = sub.Field<decimal?>("FTAmount"),
                              GAmount = sub.Field<decimal?>("GAmount"),
                              DtId = sub.Field<string>("DtId"),
                              ArActive = sub.Field<string>("ArActive"),
                              PaymentId = sub.Field<string>("PaymentId"),
                              PmId = sub.Field<string>("PmId"),
                              Pending = sub.Field<string>("Pending"),
                              Active = sub.Field<string>("Active")
                          };

            var subQuery = from s in subData
                           where /*s.PmId == "G" &&*/ s.Pending == "0" && s.ArActive == "1" && s.Active == "1" && s.DtId != "P00020001" && !s.PaymentId.StartsWith("ZZ")
                           group s by new { s.OriginalPaymentId, s.GroupInvoiceNo } into sg
                           select new
                           {
                               sg.Key.OriginalPaymentId,
                               sg.Key.GroupInvoiceNo,
                               FTAmount = Math.Round((double)((sg.Sum(s => s.PaidGAmount) * sg.Sum(s => s.FTAmount)) / sg.Sum(s => s.GAmount)), 2)
                           };

            var repResult = from rs in
                                (
                                     from m in
                                         (
                                             from o in repData
                                             join i in subQuery on new { PaymentId = o.RPaymentId, GroupInvoiceNo = o.GroupInvoiceNo } equals new { PaymentId = i.OriginalPaymentId, GroupInvoiceNo = i.GroupInvoiceNo } into iso
                                             where ((o.CancelARPmId == null && o.CCancelARPmId == null) || (o.CancelARPmId == null && o.CCancelARPmId != null && SqlMethods.DateDiffDay(o.ARPaymentDt, o.CARPaymentDt) > 0))
                                                     && o.Active == "1"
                                                     && !exceptPmId.Contains(o.PmId)
                                                     && includedDtId.Contains(o.DtId)
                                                     && o.ControllerId == (pControllerId == null ? o.ControllerId : pControllerId)
                                             from i in iso.DefaultIfEmpty()   // left join 
                                             select new
                                             {
                                                 o.PaymentId,
                                                 o.BranchId,
                                                 o.RBranchId,
                                                 o.BranchName,
                                                 o.ControllerId,
                                                 o.ARPmId,
                                                 o.InvoiceNo,
                                                 o.ReceiptId,
                                                 o.DtId,
                                                 o.GAmount,
                                                 IFTAmount = i == null ? 0 : i.FTAmount,
                                                 o.VatAmount,
                                                 o.FTAmount,
                                                 o.RGAmount,
                                                 o.RVatAmount,
                                                 o.Type
                                             })
                                     group m by new { m.PaymentId, m.BranchId, m.RBranchId, m.BranchName, m.ControllerId, m.ARPmId, m.InvoiceNo, m.ReceiptId, m.Type } into og
                                     select new
                                     {
                                         og.Key.PaymentId,
                                         PaidBranchId = og.Key.BranchId,
                                         PaidBranchName = og.Key.BranchName,
                                         og.Key.ControllerId,
                                         Quantity = og.Select(m => m.ARPmId).Distinct().Count(),
                                         BaseAmount = og.Min(m => m.DtId) == "P00020001" ? (decimal)og.Min(m => m.GAmount) - (decimal)og.Min(m => m.IFTAmount) - (decimal)og.Min(m => m.VatAmount) :
                                                                 (decimal)og.Min(m => m.GAmount) - Math.Round((decimal)((og.Min(m => m.GAmount) * (og.Sum(m => m.FTAmount) / og.Count())) / (og.Sum(m => m.RGAmount) / og.Count())), 2) - Math.Round((decimal)((og.Min(m => m.GAmount) * (og.Sum(m => m.VatAmount) / og.Count())) / (og.Sum(m => m.RGAmount) / og.Count())), 2),
                                         FtAmount = og.Min(m => m.DtId) == "P00020001" ? (decimal)og.Min(m => m.IFTAmount) :
                                                                 Math.Round((decimal)((og.Min(m => m.GAmount) * (og.Sum(m => m.FTAmount) / og.Count())) / (og.Sum(m => m.RGAmount) / og.Count())), 2),
                                         VatAmount = og.Min(m => m.VatAmount),
                                         GAmount = og.Min(m => m.GAmount),
                                         og.Key.Type
                                     })
                            group rs by new { rs.PaidBranchId, rs.PaidBranchName, rs.ControllerId, rs.Type } into rg
                            orderby rg.Key.PaidBranchId, rg.Key.PaidBranchName, rg.Key.ControllerId, rg.Key.Type, rg.Min(rs => rs.PaymentId)
                            select new
                            {
                                PaymentId = rg.Min(rs => rs.PaymentId),
                                PaidBranchId = rg.Key.PaidBranchId,
                                PaidBranchName = rg.Key.PaidBranchName,
                                ControllerId = rg.Key.ControllerId != null ? rg.Key.ControllerId == "00000000" ? "0" : rg.Key.ControllerId.TrimStart('0') : rg.Key.ControllerId,
                                Quantity = rg.Sum(rs => rs.Quantity),
                                BaseAmount = rg.Sum(rs => rs.BaseAmount),
                                FtAmount = rg.Sum(rs => rs.GAmount) - rg.Sum(rs => rs.BaseAmount) - rg.Sum(rs => rs.VatAmount),
                                VatAmount = rg.Sum(rs => rs.VatAmount),
                                GAmount = rg.Sum(rs => rs.GAmount),
                                Type = rg.Key.Type
                            };

            foreach (var q in repResult)
            {
                CAC01Report detail = new CAC01Report();
                detail.PaidBranchId = q.PaidBranchId;
                detail.PaidBranchName = q.PaidBranchName;
                //detail.CaId = q.CaId;
                detail.ControllerId = q.ControllerId;
                detail.Quantity = q.Quantity;
                //detail.Period = q.Period;
                detail.BaseAmount = q.BaseAmount;
                detail.FtAmount = q.FtAmount;
                detail.VatAmount = q.VatAmount;
                detail.GAmount = q.GAmount;
                //detail.Remark = q.Remark;
                //detail.BillBookId = q.BillBookId;
                //detail.InstallmentFlag = q.InstallmentFlag;
                //detail.LastInstallmentFlag = q.LastInstallmentFlag;
                detail.Type = q.Type;
                //detail.GroupType = q.GroupType;
                report.Add(detail);
            }


            return report;
        }

        public List<CAC01Report> GetReportCAC01_2(string pBranchId, string pControllerId, DateTime? pFromDate)
        {
            List<CAC01Report> report = new List<CAC01Report>();
            //pControllerId = Convert.ToInt32(pControllerId).ToString();

            string[] exceptPmId = { "M", "N", "O" };
            string[] includedDtId = { "M01000002", "M90000010", "M02000002", "P00010001", "P00010002", "P00020001" };
            //201801311548 Kanokwan.L แก้ไขให้รายงานแสดงค่า M02000002 การวางบิลครั้งสุดท้าย ลูกหนี้

            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC01_2X");
            db.AddInParameter(cmd, "BranchId", DbType.String, pBranchId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, pFromDate);
            cmd.CommandTimeout = 360;
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable mainRaw = ds.Tables[0];

            var repData = from rep in mainRaw.AsEnumerable()
                          select new
                          {
                              BranchId = rep.Field<string>("BranchId"),
                              BranchName = rep.Field<string>("BranchName"),
                              Quantity = rep.Field<decimal?>("Quantity"),
                              PaymentOrderDt = rep.Field<DateTime?>("PaymentOrderDt"),
                              BillBookId = rep.Field<string>("BillBookId"),
                              SpotBillInvoiceNo = rep.Field<string>("SpotBillInvoiceNo"),
                              CaDoc = rep.Field<string>("CaDoc"),
                              MruId = rep.Field<string>("MruId"),
                              InvoiceNo = rep.Field<string>("InvoiceNo"),
                              GroupInvoiceNo = rep.Field<string>("GroupInvoiceNo"),
                              PaymentId = rep.Field<string>("PaymentId"),
                              DtId = rep.Field<string>("DtId"),
                              CaId = rep.Field<string>("CaId"),
                              ControllerId = rep.Field<string>("ControllerId"),
                              ControllerName = rep.Field<string>("ControllerName"),
                              Period = rep.Field<string>("Period"),
                              ReceiptId = rep.Field<string>("ReceiptId"),
                              ExtReceiptId = rep.Field<string>("ExtReceiptId"),
                              CReceiptId = rep.Field<string>("CReceiptId"),
                              FTAmount = rep.Field<decimal?>("FTAmount"),
                              RGAmount = rep.Field<decimal?>("RGAmount"),
                              RVatAmount = rep.Field<decimal?>("RVatAmount"),
                              RBranchId = rep.Field<string>("RBranchId"),
                              PmId = rep.Field<string>("PmId"),
                              GAmount = rep.Field<decimal?>("GAmount"),
                              VatAmount = rep.Field<decimal?>("VatAmount"),
                              ARPmId = rep.Field<string>("ARPmId"),
                              CancelARPmId = rep.Field<string>("CancelARPmId"),
                              CCancelARPmId = rep.Field<string>("CCancelARPmId"),
                              ARPaymentDt = rep.Field<DateTime?>("ARPaymentDt"),
                              CARPaymentDt = rep.Field<DateTime?>("CARPaymentDt"),
                              Active = rep.Field<string>("Active"),
                              CaPayer = rep.Field<string>("CaPayer")
                          };

            repData = (from p in repData
                       where p.CaPayer.Substring(0, 6) != "000004"
                       select p); //ไม่ให้รายงานแสดงค่าพาดสาย กรณีใบเสร็จรวม

            if (repData.Count() == 0) return report;

            DataTable subRaw = ds.Tables[1];

            var subData = from sub in subRaw.AsEnumerable()
                          select new
                          {
                              OriginalPaymentId = sub.Field<string>("OriginalPaymentId"),
                              GroupInvoiceNo = sub.Field<string>("GroupInvoiceNo"),
                              PaidGAmount = sub.Field<decimal?>("PaidGAmount"),
                              FTAmount = sub.Field<decimal?>("FTAmount"),
                              GAmount = sub.Field<decimal?>("GAmount"),
                              DtId = sub.Field<string>("DtId"),
                              ArActive = sub.Field<string>("ArActive"),
                              PaymentId = sub.Field<string>("PaymentId"),
                              PmId = sub.Field<string>("PmId"),
                              Pending = sub.Field<string>("Pending"),
                              Active = sub.Field<string>("Active")
                          };

            var subQuery = from s in subData
                           where /*s.PmId == "G" && */ s.Pending == "0" && s.ArActive == "1" && s.Active == "1" && s.DtId != "P00020001" && !s.PaymentId.StartsWith("ZZ")
                           group s by new { s.OriginalPaymentId, s.GroupInvoiceNo } into sg
                           select new
                           {
                               sg.Key.OriginalPaymentId,
                               sg.Key.GroupInvoiceNo,
                               FTAmount = Math.Round((double)((sg.Sum(s => s.PaidGAmount) * sg.Sum(s => s.FTAmount)) / sg.Sum(s => s.GAmount)), 2)
                           };

            var repResult = from rs in
                                (
                                    from m in
                                        (
                                            from o in repData
                                            join i in subQuery on new { PaymentId = o.PaymentId, GroupInvoiceNo = o.GroupInvoiceNo } equals new { PaymentId = i.OriginalPaymentId, GroupInvoiceNo = i.GroupInvoiceNo } into iso
                                            where ((o.CancelARPmId == null && o.CCancelARPmId == null) || (o.CancelARPmId == null && o.CCancelARPmId != null && SqlMethods.DateDiffDay(o.ARPaymentDt, o.CARPaymentDt) > 0))
                                                    && o.Active == "1"
                                                    && !exceptPmId.Contains(o.PmId)
                                                    && includedDtId.Contains(o.DtId)
                                                    && o.ControllerId == (pControllerId == null ? o.ControllerId : pControllerId)
                                            from i in iso.DefaultIfEmpty()   // left join 
                                            select new
                                            {
                                                o.SpotBillInvoiceNo,
                                                o.BranchId,
                                                o.RBranchId,
                                                o.BranchName,
                                                o.MruId,
                                                o.CaId,
                                                o.CaDoc,
                                                o.Period,
                                                o.PmId,
                                                o.GroupInvoiceNo,
                                                o.ARPaymentDt,
                                                o.CARPaymentDt,
                                                o.ControllerId,
                                                o.ControllerName,
                                                o.ARPmId,
                                                o.InvoiceNo,
                                                o.ReceiptId,
                                                o.ExtReceiptId,
                                                o.CReceiptId,
                                                o.DtId,
                                                o.GAmount,
                                                IFTAmount = i == null ? 0 : i.FTAmount,
                                                o.VatAmount,
                                                o.FTAmount,
                                                o.RGAmount,
                                                o.RVatAmount,
                                                Type = o.BranchId == pBranchId ? "1" : "2",
                                                o.BillBookId,
                                                o.PaymentOrderDt
                                            })
                                    group m by new { m.BranchId, m.RBranchId, m.BranchName, m.MruId, m.CaId, m.ControllerId, m.ControllerName, m.Period, m.BillBookId, m.PmId, m.GroupInvoiceNo, m.PaymentOrderDt, m.ReceiptId, m.ExtReceiptId, m.CReceiptId, m.InvoiceNo, m.ARPaymentDt, m.CARPaymentDt, m.Type } into og
                                    select new
                                    {
                                        PaidBranchId = og.Key.BranchId,
                                        PaidBranchName = og.Key.BranchName,
                                        og.Key.CaId,
                                        og.Key.ControllerId,
                                        og.Key.ControllerName,
                                        Quantity = 1,
                                        og.Key.Period,
                                        Remark = og.Key.PmId == "G" ? og.Key.GroupInvoiceNo :
                                                            og.Key.GroupInvoiceNo != null ? og.Key.GroupInvoiceNo :
                                                            og.Key.GroupInvoiceNo == null && og.Key.BillBookId != null && og.Key.BillBookId.Trim() != "" ? og.Key.BillBookId.Substring(6, 9) :
                                                            og.Min(m => m.CaDoc) == null && og.Min(m => m.SpotBillInvoiceNo) != null ? "Spot Bill" :
                                                            og.Key.PaymentOrderDt != null ? og.Key.ReceiptId + " (ตัดบัญชีธนาคาร)" :
                                                            og.Key.ExtReceiptId != null ? og.Key.ExtReceiptId : og.Key.ReceiptId,
                                        BillBookId = og.Key.GroupInvoiceNo == null && og.Min(m => m.BillBookId) != null ? og.Min(m => m.BillBookId.Trim()) : "",
                                        BaseAmount = og.Min(m => m.DtId) == "P00020001" ? (decimal)og.Min(m => m.GAmount) - (decimal)og.Min(m => m.IFTAmount) - (decimal)og.Min(m => m.VatAmount) :
                                                                (decimal)og.Min(m => m.GAmount) - Math.Round((decimal)((og.Min(m => m.GAmount) * (og.Sum(m => m.FTAmount) / og.Count())) / (og.Sum(m => m.RGAmount) / og.Count())), 2) - Math.Round((decimal)((og.Min(m => m.GAmount) * (og.Sum(m => m.VatAmount) / og.Count())) / (og.Sum(m => m.RGAmount) / og.Count())), 2),
                                        FtAmount = og.Min(m => m.DtId) == "P00020001" ? (decimal)og.Min(m => m.IFTAmount) :
                                                                Math.Round((decimal)((og.Min(m => m.GAmount) * (og.Sum(m => m.FTAmount) / og.Count())) / (og.Sum(m => m.RGAmount) / og.Count())), 2),
                                        VatAmount = og.Min(m => m.VatAmount),
                                        GAmount = og.Min(m => m.GAmount),
                                        og.Key.Type,
                                        GroupType = og.Key.PmId == "G" ? "4" :
                                                                og.Key.GroupInvoiceNo != null ? "4" :
                                                                og.Min(m => m.DtId) == "P00010002" ? "5" : "1"
                                    })
                            group rs by new { rs.PaidBranchId, rs.PaidBranchName, rs.CaId, rs.ControllerId, rs.ControllerName, rs.Period, rs.Remark, rs.BillBookId, rs.Type, rs.GroupType, rs.GAmount } into rg
                            orderby rg.Key.ControllerId, rg.Key.PaidBranchId, rg.Key.CaId, rg.Key.Period, rg.Key.GAmount, rg.Key.Remark, rg.Key.GroupType
                            select new
                            {
                                rg.Key.PaidBranchId,
                                rg.Key.PaidBranchName,
                                rg.Key.CaId,
                                //ControllerId = (rg.Key.ControllerId != null ? rg.Key.ControllerId == "00000000" ? "0" : rg.Key.ControllerId.TrimStart('0') : rg.Key.ControllerId) + rg.Key.ControllerName,
                                //ControllerId = (rg.Key.ControllerId != null ? rg.Key.ControllerId == "00000000" ? "0" : rg.Key.ControllerId.TrimStart('0') : rg.Key.ControllerId) + rg.Key.ControllerName == "" ? null : rg.Key.ControllerName,
                                ControllerId = (rg.Key.ControllerId) == null ? null : (rg.Key.ControllerId != null ? rg.Key.ControllerId == "00000000" ? "0" : rg.Key.ControllerId.TrimStart('0') : rg.Key.ControllerId) + (rg.Key.ControllerName == "" ? null : rg.Key.ControllerName),
                                Quantity = rg.Sum(rs => rs.Quantity),
                                rg.Key.Period,
                                rg.Key.Remark,
                                rg.Key.BillBookId,
                                BaseAmount = rg.Sum(rs => rs.BaseAmount),
                                FtAmount = rg.Sum(rs => rs.GAmount) - rg.Sum(rs => rs.BaseAmount) - rg.Sum(rs => rs.VatAmount),
                                VatAmount = rg.Sum(rs => rs.VatAmount),
                                GAmount = rg.Sum(rs => rs.GAmount),
                                rg.Key.Type,
                                rg.Key.GroupType
                            };

            foreach (var q in repResult)
            {
                CAC01Report detail = new CAC01Report();
                detail.PaidBranchId = q.PaidBranchId;
                detail.PaidBranchName = q.PaidBranchName;
                detail.CaId = q.CaId;
                detail.ControllerId = q.ControllerId;
                detail.Quantity = q.Quantity;
                detail.Period = q.Period;
                detail.BaseAmount = q.BaseAmount;
                detail.FtAmount = q.FtAmount;
                detail.VatAmount = q.VatAmount;
                detail.GAmount = q.GAmount;
                detail.Remark = q.Remark;
                detail.BillBookId = q.BillBookId;
                //detail.InstallmentFlag = q.InstallmentFlag;
                //detail.LastInstallmentFlag = q.LastInstallmentFlag;
                detail.Type = q.Type;
                detail.GroupType = q.GroupType;
                report.Add(detail);
            }

            return report;
        }

        public List<CAC03Report> GetReportCAC03_1(string pBranchId, string pControllerId, DateTime? pFromDate, DateTime? pToDate, string pFromTime, string pToTime, string pBankKey)
        {
            DateTime fromTime = WrapTime(pFromTime, true);
            DateTime toTime = WrapTime(pToTime, false);

            List<CAC03Report> report = new List<CAC03Report>();
            string[] exceptPmId = { "M", "N", "O" };

            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC03_1X");
            db.AddInParameter(cmd, "BranchId", DbType.String, pBranchId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, pFromDate);
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, pToDate);
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable rawData = ds.Tables[0];

            var repData = from rep in rawData.AsEnumerable()
                          select new
                          {
                              GroupBankName = rep.Field<string>("GroupBankName"),
                              BankKey = rep.Field<string>("BankKey"),
                              BankName = rep.Field<string>("BankName"),
                              ChqType = rep.Field<string>("ChqType"),
                              ChqAccNo = rep.Field<string>("ChqAccNo"),
                              ChqNo = rep.Field<string>("ChqNo"),
                              ChqDt = rep.Field<DateTime?>("ChqDt"),
                              PaymentDt = rep.Field<DateTime?>("PaymentDt"),
                              CPaymentDt = rep.Field<DateTime?>("CPaymentDt"),
                              ControllerName = rep.Field<string>("ControllerName"),
                              CashierId = rep.Field<string>("CashierId"),
                              PtId = rep.Field<string>("PtId"),
                              Amount = rep.Field<decimal?>("Amount"),
                              ChangeAmount = rep.Field<decimal?>("ChangeAmount"),
                              ActualAmount = rep.Field<decimal?>("ActualAmount"),
                              FeeAmount = rep.Field<decimal?>("FeeAmount"),
                              PmId = rep.Field<string>("PmId"),
                              CancelARPmId = rep.Field<string>("CancelARPmId"),
                              CCancelARPmId = rep.Field<string>("CCancelARPmId"),
                              Active = rep.Field<string>("Active")
                          };

            if (repData.Count() == 0) return report;

            var repResult = from r in repData
                            where ((r.CancelARPmId == null && r.CCancelARPmId == null) || (r.CancelARPmId == null && r.CCancelARPmId != null && SqlMethods.DateDiffDay(r.PaymentDt, r.CPaymentDt) > 0))
                                    && r.Active == "1"  /*&& r.PtId == "2" */
                                    && !exceptPmId.Contains(r.PmId)
                                    && r.CashierId == (pControllerId == null ? r.CashierId : pControllerId)
                                    && r.BankKey == (pBankKey == null ? r.BankKey : pBankKey)
                                    && (r.PaymentDt.Value.TimeOfDay >= fromTime.TimeOfDay && r.PaymentDt.Value.TimeOfDay <= toTime.TimeOfDay)
                            group r by new { r.GroupBankName, r.BankKey, r.BankName, r.ChqAccNo, r.ChqNo, r.ChqDt, r.ChqType, r.PaymentDt, r.ControllerName } into rg
                            orderby rg.Key.BankKey, rg.Key.ChqAccNo, rg.Key.ChqNo, rg.Key.ChqDt, rg.Key.ChqType, rg.Key.GroupBankName, rg.Key.PaymentDt, rg.Key.ControllerName
                            select new
                            {
                                GroupBankName = rg.Key.GroupBankName,
                                BankKey = rg.Key.BankKey.Trim(),
                                BankName = rg.Key.BankName,
                                ChqType = rg.Key.ChqType,
                                ChqAccNo = rg.Key.ChqAccNo.Trim(),
                                ChqNo = rg.Key.ChqNo.Trim(),
                                ChqDt = rg.Key.ChqDt,
                                PaymentDt = rg.Key.PaymentDt,
                                ControllerName = rg.Key.ControllerName,
                                ChequeAmount = rg.Select(r => r.Amount).Distinct().Sum(),
                                ChangeAmount = rg.Select(r => r.ChangeAmount).Distinct().Sum(),
                                ActualAmount = rg.Select(r => r.Amount).Distinct().Sum(),
                                FeeAmount = rg.Select(r => r.FeeAmount).Distinct().Sum()
                            };

            foreach (var q in repResult)
            {
                CAC03Report detail = new CAC03Report();
                //detail.BranchId = q.BranchId;
                detail.GroupBankName = q.GroupBankName;
                detail.BankKey = q.BankKey;
                detail.BankName = q.BankName;
                detail.ChqType = q.ChqType;
                detail.ChqAccNo = q.ChqAccNo;
                detail.ChqNo = q.ChqNo;
                detail.ChqDt = q.ChqDt;
                detail.PaymentDt = q.PaymentDt;
                detail.ChequeAmount = q.ChequeAmount;
                detail.ActualAmount = q.ActualAmount;
                detail.FeeAmount = q.FeeAmount;
                //detail.ReceiptId = q.ReceiptId;
                //detail.CaId = q.CaId;
                //detail.CaName = q.CaName;
                //detail.DtName = q.DtName;
                detail.ControllerName = q.ControllerName;
                //detail.CaDoc = q.CaDoc;
                //detail.PaymentId = q.PaymentId;
                detail.Quantity = 1;
                detail.IsDuplicate = false;
                report.Add(detail);
            }

            return report;
        }

        public List<CAC03Report> GetReportCAC03_2(string pBranchId, string pControllerId, DateTime? pFromDate, DateTime? pToDate, string pFromTime, string pToTime, string pBankKey)
        {
            DateTime fromTime = WrapTime(pFromTime, true);
            DateTime toTime = WrapTime(pToTime, false);

            List<CAC03Report> report = new List<CAC03Report>();
            string[] exceptDtId = { "MZ9800015", "MZ9800025" };

            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC03_2X");
            cmd.CommandTimeout = 360;
            db.AddInParameter(cmd, "BranchId", DbType.String, pBranchId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, pFromDate);
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, pToDate);
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable rawData = ds.Tables[0];

            var repData = from rep in rawData.AsEnumerable()
                          select new
                          {
                              ControllerName = rep.Field<string>("ControllerName"),
                              CashierId = rep.Field<string>("CashierId"),
                              CashierName = rep.Field<string>("CashierName"),
                              PaymentDt = rep.Field<DateTime?>("PaymentDt"),
                              BranchId = rep.Field<string>("BranchId"),
                              //ExtReceiptId = rep.Field<string>("ExtReceiptId"),
                              ReceiptId = rep.Field<string>("ReceiptId"),
                              MruId = rep.Field<string>("MruId"),
                              CaId = rep.Field<string>("CaId"),
                              CaName = rep.Field<string>("CaName"),
                              DtName = rep.Field<string>("DtName"),
                              CCaId = rep.Field<string>("CCaId"),
                              ActualAmount = rep.Field<decimal?>("ActualAmount"),
                              CaDoc = rep.Field<string>("CaDoc"),
                              DtId = rep.Field<string>("DtId"),
                              ARPtId = rep.Field<string>("ARPtId"),
                              GroupBankName = rep.Field<string>("GroupBankName"),
                              BankKey = rep.Field<string>("BankKey"),
                              BankName = rep.Field<string>("BankName"),
                              ChqAccNo = rep.Field<string>("ChqAccNo"),
                              ChqNo = rep.Field<string>("ChqNo"),
                              ChqDate = rep.Field<string>("ChqDate"),
                              Amount = rep.Field<decimal?>("Amount"),
                              ChangeAmount = rep.Field<decimal?>("ChangeAmount"),
                              FeeAmount = rep.Field<decimal?>("FeeAmount"),
                              PaymentId = rep.Field<string>("PaymentId"),
                              PtId = rep.Field<string>("PtId"),
                              ChqType = rep.Field<string>("ChqType"),
                              //CashierChequeFlag = rep.Field<string>("CashierChequeFlag"),
                              InvoiceNo = rep.Field<string>("InvoiceNo"),
                              CancelARPmId = rep.Field<string>("CancelARPmId"),
                              CCancelARPmId = rep.Field<string>("CCancelARPmId"),
                              ARPaymentDt = rep.Field<DateTime?>("ARPaymentDt"),
                              CARPaymentDt = rep.Field<DateTime?>("CARPaymentDt"),
                              Active = rep.Field<string>("Active")
                          };

            if (repData.Count() == 0) return report;

            var innerResult = from r in repData
                              where ((r.CancelARPmId == null && r.CCancelARPmId == null) || (r.CancelARPmId == null && r.CCancelARPmId != null && SqlMethods.DateDiffDay(r.ARPaymentDt, r.CARPaymentDt) > 0))
                                  /*&& r.PtId == "2" */
                                    && r.Active == "1"
                                    && r.CashierId == (pControllerId == null ? r.CashierId : pControllerId)
                                    && r.BankKey == (pBankKey == null ? r.BankKey : pBankKey)
                              group r by new { r.GroupBankName, r.BankKey, r.BankName, r.ChqAccNo, r.ChqNo, r.ChqDate, r.ChqType, r.ARPtId, r.PaymentId, r.PtId } into rg
                              select new
                              {
                                  rg.Key.ARPtId,
                                  rg.Key.PaymentId,
                                  rg.Key.PtId,
                                  //rg.Key.DraftFlag,
                                  //rg.Key.CashierChequeFlag,
                                  rg.Key.GroupBankName,
                                  rg.Key.BankKey,
                                  rg.Key.BankName,
                                  rg.Key.ChqAccNo,
                                  rg.Key.ChqNo,
                                  rg.Key.ChqDate,
                                  rg.Key.ChqType,
                                  Amount = (decimal?)rg.Select(r => r.Amount).Distinct().Sum(),
                                  ChangeAmount = (decimal?)rg.Select(r => r.ChangeAmount).Distinct().Sum(),
                                  FeeAmount = (decimal?)rg.Select(r => r.FeeAmount).Distinct().Sum()
                              };

            var repResult = from o in repData
                            join i in innerResult on o.ARPtId equals i.ARPtId
                            where (o.CancelARPmId == null && o.CCancelARPmId == null) || (o.CancelARPmId == null && o.CCancelARPmId != null && SqlMethods.DateDiffDay(o.ARPaymentDt, o.CARPaymentDt) > 0)
                                    && o.Active == "1"
                                    && !exceptDtId.Contains(o.DtId)
                                    && o.CashierId == (pControllerId == null ? o.CashierId : pControllerId)
                                    && o.BankKey == (pBankKey == null ? o.BankKey : pBankKey)
                                    && (o.PaymentDt.Value.TimeOfDay >= fromTime.TimeOfDay && o.PaymentDt.Value.TimeOfDay <= toTime.TimeOfDay)
                            group o by new { o.PaymentDt, o.BranchId, i.GroupBankName, i.BankKey, i.BankName, i.ChqAccNo, i.ChqNo, i.ChqDate, i.ChqType, o.ReceiptId, o.MruId, o.CaId, o.CaName, o.CashierId, o.CashierName, o.InvoiceNo, o.ARPaymentDt, o.CARPaymentDt, o.CaDoc } into og
                            orderby og.Key.BankKey, og.Key.ChqAccNo, og.Key.ChqNo, og.Key.ChqDate, og.Key.ChqType, og.Key.GroupBankName, og.Key.ReceiptId, og.Key.InvoiceNo, og.Key.CaDoc
                            select new
                            {
                                PaymentId = og.Key.BankKey + og.Key.ChqAccNo + og.Key.ChqNo + og.Key.ChqDate,
                                PaymentDt = og.Key.PaymentDt,
                                BranchId = og.Key.BranchId,
                                GroupBankName = og.Key.GroupBankName,
                                BankKey = og.Key.BankKey.Trim(),
                                BankName = og.Key.BankName,
                                ChqType = og.Key.ChqType,
                                ChqAccNo = og.Key.ChqAccNo.Trim(),
                                ChqNo = og.Key.ChqNo.Trim(),
                                ChqDate = og.Key.ChqDate,
                                ChequeAmount = og.Max(i => i.Amount),
                                ChangeAmount = og.Max(i => i.ChangeAmount),
                                ActualAmount = og.Select(o => o.ActualAmount).Distinct().Sum(),
                                FeeAmount = og.Max(i => i.FeeAmount),
                                ReceiptId = og.Key.ReceiptId,
                                CaId = og.Min(o => o.CCaId),
                                CaName = og.Key.CaName,
                                DtName = og.Min(o => o.DtName),
                                ControllerName = og.Min(o => o.ControllerName),
                                CaDoc = og.Min(o => o.CaDoc).Trim()
                            };

            foreach (var q in repResult)
            {
                CAC03Report detail = new CAC03Report();
                detail.BranchId = q.BranchId;
                detail.GroupBankName = q.GroupBankName;
                detail.BankKey = q.BankKey;
                detail.BankName = q.BankName;
                detail.ChqType = q.ChqType;
                detail.ChqAccNo = q.ChqAccNo;
                detail.ChqNo = q.ChqNo;
                detail.ChqDt = DateTime.ParseExact(q.ChqDate, "yyyy.MM.dd", new CultureInfo("en-US"));
                detail.PaymentDt = q.PaymentDt;
                detail.ChequeAmount = q.ChequeAmount;
                detail.ActualAmount = q.ActualAmount;
                detail.FeeAmount = q.FeeAmount;
                detail.ReceiptId = q.ReceiptId;
                detail.CaId = q.CaId;
                detail.CaName = q.CaName;
                detail.DtName = q.DtName;
                detail.ControllerName = q.ControllerName;
                detail.CaDoc = q.CaDoc;
                detail.PaymentId = q.PaymentId;
                detail.Quantity = 1;
                detail.IsDuplicate = false;
                report.Add(detail);
            }

            return report;
        }

        public List<CAC04Report> GetReportCAC04_1(string pBranchId, string pControllerId, DateTime? pFromDate, DateTime? pToDate, string pFromTime, string pToTime, string pBankKey)
        {
            DateTime fromTime = WrapTime(pFromTime, true);
            DateTime toTime = WrapTime(pToTime, false);

            List<CAC04Report> report = new List<CAC04Report>();
            string[] exceptPmId = { "M", "N", "O" };

            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC04_1X");
            db.AddInParameter(cmd, "BranchId", DbType.String, pBranchId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, pFromDate);
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, pToDate);
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable rawData = ds.Tables[0];

            var repData = from rep in rawData.AsEnumerable()
                          select new
                          {
                              GroupBankName = rep.Field<string>("GroupBankName"),
                              BankKey = rep.Field<string>("BankKey"),
                              BankName = rep.Field<string>("BankName"),
                              TranfAccNo = rep.Field<string>("TranfAccNo"),
                              ClearingAccNo = rep.Field<string>("ClearingAccNo"),
                              TranfDt = rep.Field<DateTime?>("TranfDt"),
                              //ExtReceiptDt = rep.Field<DateTime?>("ExtReceiptDt"),
                              PaymentDt = rep.Field<DateTime?>("PaymentDt"),
                              ControllerName = rep.Field<string>("ControllerName"),
                              Amount = rep.Field<decimal?>("Amount"),
                              ActualAmount = rep.Field<decimal?>("ActualAmount"),
                              FeeAmount = rep.Field<decimal?>("FeeAmount"),
                              ARPtId = rep.Field<string>("ARPtId"),
                              ARPmId = rep.Field<string>("ARPmId"),
                              PtId = rep.Field<string>("PtId"),
                              CashierId = rep.Field<string>("CashierId"),
                              PmId = rep.Field<string>("PmId"),
                              CancelARPmId = rep.Field<string>("CancelARPmId"),
                              CCancelARPmId = rep.Field<string>("CCancelARPmId"),
                              ARPaymentDt = rep.Field<DateTime?>("ARPaymentDt"),
                              CARPaymentDt = rep.Field<DateTime?>("CARPaymentDt"),
                              Active = rep.Field<string>("Active")
                          };

            if (repData.Count() == 0) return report;

            var innerResult = from r in repData
                              where r.Active == "1"
                              group r by new { r.ARPtId } into rg
                              select new
                              {
                                  ARPtId = (string)rg.Key.ARPtId,
                                  ARPmId = (string)rg.Min(r => r.ARPmId),
                                  ActualAmount = (decimal?)rg.Sum(r => r.ActualAmount)
                              };

            var repResult = from o in repData
                            join i in innerResult on o.ARPtId equals i.ARPtId
                            where /*o.PtId == "3" */
                                    ((o.CancelARPmId == null && o.CCancelARPmId == null) || (o.CancelARPmId == null && o.CCancelARPmId != null && SqlMethods.DateDiffDay(o.ARPaymentDt, o.CARPaymentDt) > 0))
                                    && o.Active == "1"
                                    && !exceptPmId.Contains(o.PmId)
                                    && o.CashierId == (pControllerId == null ? o.CashierId : pControllerId)
                                    && o.BankKey == (pBankKey == null ? o.BankKey : pBankKey)
                                    && (o.PaymentDt.Value.TimeOfDay >= fromTime.TimeOfDay && o.PaymentDt.Value.TimeOfDay <= toTime.TimeOfDay)
                            group o by new { o.GroupBankName, o.BankKey, o.BankName, o.TranfAccNo, o.ClearingAccNo, o.TranfDt, /*o.ExtReceiptDt,*/ o.PaymentDt, o.ControllerName, o.ARPtId } into og
                            orderby og.Key.BankKey, og.Key.GroupBankName, og.Key.BankName, og.Key.TranfAccNo, og.Key.ClearingAccNo, og.Key.TranfDt,
                                    og.Key.PaymentDt, og.Key.ControllerName, og.Key.ARPtId
                            select new
                            {
                                GroupBankName = og.Key.GroupBankName,
                                BankKey = og.Key.BankKey,
                                BankName = og.Key.BankName,
                                TranfAccNo = og.Key.TranfAccNo.Trim(),
                                ClearingAccNo = og.Key.ClearingAccNo.Trim(),
                                TranfDt = og.Key.TranfDt,
                                //PaymentDt = og.Key.ExtReceiptDt == null ? og.Key.PaymentDt : og.Key.ExtReceiptDt,
                                PaymentDt = og.Key.PaymentDt,
                                ControllerName = og.Key.ControllerName,
                                Amount = og.Max(o => o.Amount),
                                ActualAmount = og.Max(i => i.ActualAmount),
                                FeeAmount = og.Max(o => o.FeeAmount)
                            };

            foreach (var q in repResult)
            {
                CAC04Report detail = new CAC04Report();
                //detail.BranchId = 
                detail.GroupBankName = q.GroupBankName;
                detail.BankKey = q.BankKey;
                detail.BankName = q.BankName;
                detail.TranfAccNo = q.TranfAccNo;
                detail.ClearingAccNo = q.ClearingAccNo;
                detail.TranfDt = q.TranfDt;
                detail.PaymentDt = q.PaymentDt;
                detail.Amount = q.Amount;
                detail.ActualAmount = q.ActualAmount;
                detail.FeeAmount = q.FeeAmount;
                //detail.ReceiptId = 
                //detail.CaId = 
                //detail.CaName =
                //detail.DtName = 
                detail.ControllerName = q.ControllerName;
                //detail.CaDoc = ;
                //detail.PaymentId = 
                detail.Quantity = 1;
                detail.IsDuplicate = false;
                report.Add(detail);
            }

            return report;
        }

        public List<CAC04Report> GetReportCAC04_2(string pBranchId, string pControllerId, DateTime? pFromDate, DateTime? pToDate, string pFromTime, string pToTime, string pBankKey)
        {
            DateTime fromTime = WrapTime(pFromTime, true);
            DateTime toTime = WrapTime(pToTime, false);

            List<CAC04Report> report = new List<CAC04Report>();
            string[] exceptPmId = { "M", "N", "O" };

            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC04_2X");
            db.AddInParameter(cmd, "BranchId", DbType.String, pBranchId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, pFromDate);
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, pToDate);
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable rawData = ds.Tables[0];

            var repData = from rep in rawData.AsEnumerable()
                          select new
                          {
                              PaymentId = rep.Field<string>("PaymentId"),
                              OriginalPaymentId = rep.Field<string>("OriginalPaymentId"),
                              PaymentDt = rep.Field<DateTime?>("PaymentDt"),
                              CashierId = rep.Field<string>("CashierId"),
                              BranchId = rep.Field<string>("BranchId"),
                              CaName = rep.Field<string>("CaName"),
                              DtName = rep.Field<string>("DtName"),
                              CaId = rep.Field<string>("CaId"),
                              ARPtId = rep.Field<string>("ARPtId"),
                              GroupBankName = rep.Field<string>("GroupBankName"),
                              BankKey = rep.Field<string>("BankKey"),
                              BankName = rep.Field<string>("BankName"),
                              TranfAccNo = rep.Field<string>("TranfAccNo"),
                              FeeAmount = rep.Field<decimal?>("FeeAmount"),
                              ChangeAmount = rep.Field<decimal?>("ChangeAmount"),
                              TranfDt = rep.Field<DateTime?>("TranfDt"),
                              PtId = rep.Field<string>("PtId"),
                              ClearingAccNo = rep.Field<string>("ClearingAccNo"),
                              ActualAmount = rep.Field<decimal?>("ActualAmount"),
                              ReceiptId = rep.Field<string>("ReceiptId"),
                              ControllerName = rep.Field<string>("ControllerName"),
                              CaDoc = rep.Field<string>("CaDoc"),
                              DtId = rep.Field<string>("DtId"),
                              InvoiceNo = rep.Field<string>("InvoiceNo"),
                              PmId = rep.Field<string>("PmId"),
                              Active = rep.Field<string>("Active"),
                              CancelARPmId = rep.Field<string>("CancelARPmId"),
                              CCancelARPmId = rep.Field<string>("CCancelARPmId"),
                              ARPaymentDt = rep.Field<DateTime?>("ARPaymentDt"),
                              CARPaymentDt = rep.Field<DateTime?>("CARPaymentDt")
                          };

            if (repData.Count() == 0) return report;

            DataTable transPayRaw = ds.Tables[1];

            var transPay = from rep in transPayRaw.AsEnumerable()
                           select new
                           {
                               PaymentId = rep.Field<string>("PaymentId"),
                               PtId = rep.Field<string>("PtId"),
                               TranfAccNo = rep.Field<string>("TranfAccNo"),
                               TranfDt = rep.Field<DateTime?>("TranfDt"),
                               Amount = rep.Field<decimal?>("Amount")
                           };

            var payResult = from p in transPay
                            where p.PtId == "3"
                            group p by new { p.PaymentId } into pg
                            select new
                            {
                                pg.Key.PaymentId,
                                PAmount = pg.Sum(p => p.Amount)
                            };


            var transResult = from t in transPay
                              where t.PtId == "3"
                              group t by new { t.PaymentId, t.TranfAccNo, t.TranfDt } into tg
                              select new
                              {
                                  PaymentId = tg.Min(t => t.PaymentId),
                                  tg.Key.TranfAccNo,
                                  tg.Key.TranfDt,
                                  TAmount = tg.Sum(t => t.Amount)
                              };

            var repResult = from rs in
                                (
                                    from o in repData
                                    join p in payResult on o.PaymentId equals p.PaymentId into ps
                                    join t in transResult on new { PaymentId = o.PaymentId, TranfAccNo = o.TranfAccNo, TranfDt = o.TranfDt } equals new { PaymentId = t.PaymentId, TranfAccNo = t.TranfAccNo, TranfDt = t.TranfDt } into ts
                                    where /*o.PtId == "3" && */ o.Active == "1"
                                            && ((o.CancelARPmId == null && o.CCancelARPmId == null) || (o.CancelARPmId == null && o.CCancelARPmId != null && SqlMethods.DateDiffDay(o.ARPaymentDt, o.CARPaymentDt) > 0))
                                            && !exceptPmId.Contains(o.PmId)
                                            && o.CashierId == (pControllerId == null ? o.CashierId : pControllerId)
                                            && o.BankKey == (pBankKey == null ? o.BankKey : pBankKey)
                                            && (o.PaymentDt.Value.TimeOfDay >= fromTime.TimeOfDay && o.PaymentDt.Value.TimeOfDay <= toTime.TimeOfDay)
                                    from p in ps.DefaultIfEmpty()   // left join 
                                    from t in ts.DefaultIfEmpty()   // left join 
                                    select new
                                    {
                                        o.PaymentId,
                                        o.OriginalPaymentId,
                                        o.ReceiptId,
                                        o.DtId,
                                        o.DtName,
                                        o.PaymentDt,
                                        o.BranchId,
                                        o.GroupBankName,
                                        o.BankKey,
                                        o.BankName,
                                        o.TranfAccNo,
                                        o.ClearingAccNo,
                                        o.TranfDt,
                                        o.ChangeAmount,
                                        o.ActualAmount,
                                        o.FeeAmount,
                                        o.CaId,
                                        o.CaName,
                                        o.CaDoc,
                                        o.CashierId,
                                        o.ControllerName,
                                        o.InvoiceNo,
                                        o.ARPaymentDt,
                                        o.CARPaymentDt,
                                        o.ARPtId,
                                        PAmount = p == null ? 0 : p.PAmount,
                                        TAmount = t == null ? 0 : t.TAmount
                                    })
                            group rs by new
                            {
                                rs.PaymentId,
                                rs.ReceiptId,
                                rs.PaymentDt,
                                rs.BranchId,
                                rs.GroupBankName,
                                rs.BankKey,
                                rs.BankName,
                                rs.TranfAccNo,
                                rs.ClearingAccNo,
                                rs.TranfDt,
                                rs.ChangeAmount,
                                rs.ActualAmount,
                                rs.FeeAmount,
                                rs.CaId,
                                rs.CaName,
                                rs.CashierId,
                                rs.ControllerName,
                                rs.InvoiceNo,
                                rs.ARPaymentDt,
                                rs.CARPaymentDt,
                                rs.ARPtId,
                                rs.PAmount,
                                rs.TAmount
                            } into og
                            orderby og.Key.PaymentDt, og.Key.BankKey, og.Key.TranfAccNo, og.Key.TranfDt, og.Key.ARPtId, og.Key.ReceiptId, og.Key.ActualAmount, og.Key.CaId
                            //orderby og.Key.PaymentDt, og.Key.ReceiptId, og.Key.BankKey, og.Key.TranfAccNo, og.Key.TranfDt, og.Key.ARPtId, og.Key.ActualAmount, og.Key.CaId // back up by tanayoot 11/12/2562
                            select new
                            {
                                PaymentId = og.Key.PaymentId,
                                PaymentDt = og.Key.PaymentDt,
                                BranchId = og.Key.BranchId,
                                GroupBankName = og.Key.GroupBankName,
                                BankKey = og.Key.BankKey,
                                BankName = og.Key.BankName,
                                TranfAccNo = og.Key.TranfAccNo.Trim(),
                                ClearingAccNo = og.Key.ClearingAccNo,
                                TranfDt = og.Key.TranfDt,
                                // 2024-01-25 ใช้ยอดเงิน ตามยอดนำฝาก  เดิมถ้าเป็น Group จะ Return ยอด Sum ตาม PaymentId
                                //Amount = og.Min(rs => rs.OriginalPaymentId) != null ? og.Key.PAmount : og.Key.TAmount,
                                Amount = og.Key.TAmount,
                                ActualAmount = og.Key.ActualAmount,
                                FeeAmount = og.Key.FeeAmount,
                                ReceiptId = og.Key.ReceiptId,
                                CaId = og.Key.CaId,
                                CaName = og.Key.CaName,
                                DtName = og.Min(rs => rs.DtName),
                                ControllerName = og.Key.ControllerName,
                                CaDoc = (og.Min(rs => rs.DtId) == "M01000002" ? (og.Key.InvoiceNo.Trim().Length == 22 ? "" : og.Key.InvoiceNo) : (og.Min(rs => rs.CaDoc) == null ? "" : og.Min(rs => rs.CaDoc))).Trim(),
                                ARPtId = og.Key.ARPtId
                            };

            foreach (var q in repResult)
            {
                CAC04Report detail = new CAC04Report();
                detail.BranchId = q.BranchId;
                detail.GroupBankName = q.GroupBankName;
                detail.BankKey = q.BankKey;
                detail.BankName = q.BankName;
                detail.TranfAccNo = q.TranfAccNo;
                detail.ClearingAccNo = q.ClearingAccNo;
                detail.TranfDt = q.TranfDt;
                detail.PaymentDt = q.PaymentDt;
                detail.Amount = q.Amount;
                detail.ActualAmount = q.ActualAmount;
                detail.FeeAmount = q.FeeAmount;
                detail.ReceiptId = q.ReceiptId;
                detail.CaId = q.CaId;
                detail.CaName = q.CaName;
                detail.DtName = q.DtName;
                detail.ControllerName = q.ControllerName;
                detail.CaDoc = q.CaDoc;
                detail.PaymentId = q.PaymentId;
                detail.Quantity = 1;
                detail.IsDuplicate = false;
                report.Add(detail);
            }

            return report;
        }

        public List<CAC05Report> GetReportCAC05_1(string pBranchId, string pControllerId, DateTime? pFromDate)
        {
            List<CAC05Report> report = new List<CAC05Report>();

            string[] exceptPmId = { "M", "N", "O" };
            string[] exp01 = { "M01000002", "M90000010", "M02000002" };
            string[] exp02 = { "M01000002", "M90000010", "P00010001", "P00010002", "M02000002" };
            //201801311548 Kanokwan.L แก้ไขให้รายงานแสดงค่า M02000002 การวางบิลครั้งสุดท้าย ลูกหนี้
            //string[] exp03 = { "M00172100", "M00178200", "M00172800", "M00172000", "M00178300", "M00173000" }; //ค่าบริการพาดสาย

            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC05_1X");
            db.AddInParameter(cmd, "BranchId", DbType.String, pBranchId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, pFromDate);
            cmd.CommandTimeout = 360;
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable paidBillbook = ds.Tables[0];
            DataTable reportRaw = ds.Tables[1];

            var bData = from inv in paidBillbook.AsEnumerable()
                        select new
                        {
                            BillBookId = inv.Field<string>("BillBookId"),
                            DtId = inv.Field<string>("DtId"),
                            ARActive = inv.Field<string>("ARActive"),
                            //ARActive2 = inv.Field<string>("ARActive2"),
                            //BillBookId2 = inv.Field<string>("BillBookId2"),
                            //DtId2 = inv.Field<string>("DtId2"),
                            GAmount = inv.Field<decimal?>("GAmount"),
                            FTAmount = inv.Field<decimal?>("FTAmount"),
                            ARGAmount = inv.Field<decimal?>("ARGAmount"),
                            VatAmount = inv.Field<decimal?>("VatAmount"),
                            PmId = inv.Field<string>("PmId"),
                            Pending = inv.Field<string>("Pending"),
                            Active = inv.Field<string>("Active"),
                        };

            var reportData = from inv in reportRaw.AsEnumerable()
                             select new
                             {
                                 ControllerName = inv.Field<string>("ControllerName"),
                                 InvoiceNo = inv.Field<string>("InvoiceNo"),
                                 ARPmId = inv.Field<string>("ARPmId"),
                                 PmId = inv.Field<string>("PmId"),
                                 VatAmount = inv.Field<decimal?>("VatAmount"),
                                 AdjAmount = inv.Field<decimal?>("AdjAmount"),
                                 FeeAmount = inv.Field<decimal?>("FeeAmount"),
                                 GAmount = inv.Field<decimal?>("GAmount"),
                                 DtId = inv.Field<string>("DtId"),
                                 CaPayer = inv.Field<string>("CaPayer"),
                                 RGAmount = inv.Field<decimal?>("RGAmount"),
                                 RVatAmount = inv.Field<decimal?>("RVatAmount"),
                                 FTAmount = inv.Field<decimal?>("FTAmount"),
                                 GroupInvoiceNo = inv.Field<string>("GroupInvoiceNo"),
                                 ReceiptId = inv.Field<string>("ReceiptId"),
                                 BillBookId = inv.Field<string>("BillBookId"),
                                 BranchId = inv.Field<string>("BranchId"),
                                 PaymentDt = inv.Field<DateTime?>("PaymentDt"),
                                 PtId = inv.Field<string>("PtId"),
                                 Amount = inv.Field<decimal?>("Amount"),
                                 PaymentId = inv.Field<string>("PaymentId"),
                                 CashierId = inv.Field<string>("CashierId"),
                                 Active = inv.Field<string>("Active"),
                                 CancelARPmId = inv.Field<string>("CancelARPmId"),
                                 CCancelARPmId = inv.Field<string>("CCancelARPmId"),
                                 ARPaymentDt = inv.Field<DateTime?>("ARPaymentDt"),
                                 CARPaymentDt = inv.Field<DateTime?>("CARPaymentDt"),
                             };

            var bookData = from b in bData
                           where /*b.PmId == "G" && */ b.Pending == "0" && b.Active == "1" && b.DtId != "P00020001"
                               //&& b.BillBookId != null && b.BillBookId2 != null && b.DtId2 != "P00020001" && b.ARActive == "1" && b.ARActive2 == "1"
                                       && b.BillBookId != null && b.ARActive == "1"
                           group b by new { b.BillBookId } into bg
                           select new
                           {
                               bg.Key.BillBookId,
                               FTAmount = Math.Round((decimal)((bg.Sum(b => b.GAmount) * bg.Sum(b => b.FTAmount)) / bg.Sum(b => b.ARGAmount)), 2),
                               VatAmount = Math.Round((decimal)(((bg.Sum(b => b.GAmount) / 100 * bg.Sum(b => b.VatAmount) / 100) / bg.Sum(b => b.ARGAmount / 100)) * 100), 2)
                           };

            var repResult = from rs in
                                (
                                    from ix in
                                        (
                                            from da in reportData
                                            join gs in bookData on da.BillBookId equals gs.BillBookId into books
                                            where ((da.CancelARPmId == null && da.CCancelARPmId == null) || (da.CancelARPmId == null && da.CCancelARPmId != null && SqlMethods.DateDiffDay(da.ARPaymentDt, da.CARPaymentDt) > 0))
                                                        && da.Active == "1"
                                                        && !exceptPmId.Contains(da.PmId)
                                                        && da.CashierId == (pControllerId == null ? da.CashierId : pControllerId)
                                            from g in books.DefaultIfEmpty()   // left join 
                                            select new
                                            {
                                                da.ControllerName,
                                                da.InvoiceNo,
                                                da.PaymentId,
                                                da.ReceiptId,
                                                da.RGAmount,
                                                da.RVatAmount,
                                                da.FTAmount,
                                                da.GAmount,
                                                da.VatAmount,
                                                da.AdjAmount,
                                                da.FeeAmount,
                                                da.DtId,
                                                da.CaPayer,
                                                da.ARPmId,
                                                da.PaymentDt,
                                                da.GroupInvoiceNo,
                                                da.BillBookId,
                                                da.PmId,
                                                da.BranchId,
                                                da.PtId,
                                                da.Amount,
                                                IFTAmount = g == null ? 0 : g.FTAmount,
                                                IVatAmount = g == null ? 0 : g.VatAmount
                                            })
                                    group ix by new { ix.BranchId, ix.ARPmId, ix.InvoiceNo, ix.ReceiptId, ix.PtId } into nx
                                    select new
                                    {
                                        ControllerName = nx.Min(ix => ix.ControllerName),
                                        nx.Key.InvoiceNo,
                                        PaymentId = nx.Min(da => da.PaymentId),
                                        nx.Key.ARPmId,
                                        BaseAmount = nx.Sum(ix => ix.RGAmount) == 0 ? (decimal)nx.Min(ix => ix.GAmount) - (decimal)nx.Min(ix => ix.VatAmount) :
                                                                    nx.Min(ix => ix.DtId) == "P00020001" ? (decimal)nx.Min(ix => ix.GAmount) - (decimal)nx.Min(ix => ix.IFTAmount) - (decimal)nx.Min(ix => ix.IVatAmount) :
                                                                    (decimal)nx.Min(ix => ix.GAmount) - Math.Round((decimal)(nx.Min(ix => ix.GAmount) * (nx.Sum(ix => ix.FTAmount) / nx.Count())) / (decimal)(nx.Sum(ix => ix.RGAmount) / nx.Count()), 2) - Math.Round((decimal)(nx.Min(ix => ix.GAmount) * (nx.Sum(ix => ix.RVatAmount) / nx.Count())) / (decimal)(nx.Sum(ix => ix.RGAmount) / nx.Count()), 2),
                                        FTAmount = nx.Sum(ix => ix.RGAmount) == 0 ? 0 :
                                                                    nx.Min(ix => ix.DtId) == "P00020001" ? (decimal)nx.Min(ix => ix.IFTAmount) :
                                                                    Math.Round((decimal)(((nx.Min(ix => ix.GAmount))) * (nx.Sum(ix => ix.FTAmount) / nx.Count())) / (decimal)((nx.Sum(ix => ix.RGAmount)) / nx.Count()), 2),
                                        VatAmount = nx.Sum(ix => ix.RGAmount) == 0 ? nx.Min(ix => ix.GAmount) - nx.Min(ix => ix.VatAmount) :
                                                                    nx.Min(ix => ix.DtId) == "P00020001" ? nx.Min(ix => ix.IVatAmount) :
                                                                    nx.Min(ix => ix.VatAmount),
                                        AdjAmount = nx.Min(ix => ix.AdjAmount),
                                        FeeAmount = nx.Sum(ix => ix.FeeAmount),
                                        GAmount = nx.Min(ix => ix.GAmount),
                                        PaymentDt = nx.Min(ix => ix.PaymentDt),
                                        DtId = exp01.Contains(nx.Min(ix => ix.DtId)) && nx.Min(ix => ix.GroupInvoiceNo) == null && nx.Min(ix => ix.BillBookId) == null ? "1" :
                                                                    exp01.Contains(nx.Min(ix => ix.DtId)) && nx.Min(ix => ix.GroupInvoiceNo) == null && nx.Min(ix => ix.BillBookId) != null && nx.Min(ix => ix.PmId) == "C" ? "1" :
                                                                    nx.Min(ix => ix.DtId) == "P00020001" ? "2" :
                                                                    exp01.Contains(nx.Min(ix => ix.DtId)) && nx.Min(ix => ix.GroupInvoiceNo) != null ? "2" :
                                                                    nx.Min(ix => ix.CaPayer).Substring(0, 6) == "000004" && nx.Min(ix => ix.GroupInvoiceNo) != null ? "2" : //กรณีใบเสร็จรวมค่าบริการพาดสาย
                                                                    (nx.Min(ix => ix.CaPayer).Substring(0, 3) == "091" || nx.Min(ix => ix.CaPayer).Substring(0, 3) == "092") && nx.Min(ix => ix.GroupInvoiceNo) != null ? "2" : //กรณีใบแยกค่าบริการพาดสาย
                                                                    nx.Min(ix => ix.DtId) == "P00010001" ? "3" :
                                                                    nx.Min(ix => ix.DtId) == "P00010002" ? "4" :
                                                                    !exp02.Contains(nx.Min(ix => ix.DtId)) && nx.Min(ix => ix.GroupInvoiceNo) == null ? "5" : null,
                                        Type = (nx.Min(ix => ix.DtId) != null && nx.Min(ix => ix.DtId).StartsWith("MZ") ? "1" :
                                                                    nx.Key.BranchId == pBranchId ? "1" : "2"),
                                        CashAmount = nx.Key.PtId == "1" ? nx.Sum(ix => ix.Amount) : null,
                                        ChequeAmount = nx.Key.PtId == "2" ? nx.Sum(ix => ix.Amount) : null,
                                        TransferAmount = nx.Key.PtId == "3" ? nx.Sum(ix => ix.Amount) : null,
                                        QRPaymentAmount = nx.Key.PtId == "5" ? nx.Sum(ix => ix.Amount) : null, // DCR QR Payment 2023-03 : Report 2.8.1
                                    })
                            group rs by new { rs.ControllerName, rs.InvoiceNo, rs.BaseAmount, rs.FTAmount, rs.VatAmount, rs.AdjAmount, rs.GAmount, rs.DtId, rs.Type, rs.PaymentDt } into gb
                            orderby gb.Key.ControllerName, gb.Key.PaymentDt, gb.Key.InvoiceNo, gb.Key.GAmount
                            select new
                            {
                                ControllerName = gb.Key.ControllerName,
                                InvoiceNo = gb.Key.InvoiceNo,
                                Quantity = 1,
                                BaseAmount = gb.Key.BaseAmount,
                                FtAmount = (gb.Key.GAmount + gb.Key.AdjAmount) - gb.Key.BaseAmount - gb.Key.VatAmount - gb.Key.AdjAmount,
                                VatAmount = gb.Key.VatAmount,
                                AdjAmount = gb.Key.AdjAmount,
                                FeeAmount = gb.Sum(rs => rs.FeeAmount),
                                GAmount = (gb.Key.GAmount + gb.Key.AdjAmount),
                                DtId = gb.Key.DtId,
                                Type = gb.Key.Type,
                                CashAmount = gb.Sum(rs => rs.CashAmount) == 0 ? null : gb.Sum(rs => rs.CashAmount),
                                ChequeAmount = gb.Sum(rs => rs.ChequeAmount) == 0 ? null : gb.Sum(rs => rs.ChequeAmount),
                                TransferAmount = gb.Sum(rs => rs.TransferAmount) == 0 ? null : gb.Sum(rs => rs.TransferAmount),
                                QRPaymentAmount = gb.Sum(rs => rs.QRPaymentAmount) == 0 ? null : gb.Sum(rs => rs.QRPaymentAmount), //DCR QR Payment 2023-03  : Report 2.8.1
                                PaymentDt = gb.Key.PaymentDt
                            };

            foreach (var q in repResult)
            {
                CAC05Report detail = new CAC05Report();
                //detail.PaymentId = q.PaymentId;
                //detail.Mru = q.MruId;
                //detail.CaId = q.CaId;
                //detail.BranchId = q.BranchId;
                //detail.Period = q.Period;
                detail.ControllerName = q.ControllerName;
                //detail.DebtType = q.DtName;
                //detail.ReceiptId = q.ReceiptId;
                detail.BaseAmount = q.BaseAmount;
                detail.FtAmount = q.FtAmount;
                detail.VatAmount = q.VatAmount;
                detail.GAmount = q.GAmount;
                detail.CashAmount = q.CashAmount;
                detail.ChequeAmount = q.ChequeAmount;
                detail.TransferAmount = q.TransferAmount;
                detail.QRPaymentAmount = q.QRPaymentAmount;   //DCR QR Payment 2023-03  : Report 2.8.1
                //detail.TotalAmount = q.TotalAmount;
                detail.AdjAmount = q.AdjAmount;
                detail.FeeAmount = q.FeeAmount;
                detail.Type = q.Type;
                detail.Quantity = q.Quantity;
                detail.DtId = q.DtId;
                detail.PaymentDt = q.PaymentDt;
                //detail.CaId_Qty = q.CaId_Qty;
                report.Add(detail);
            }

            return report;
        }

        public List<CAC05Report> GetReportCAC05_2(string pBranchId, string pControllerId, DateTime? pFromDate)
        {
            List<CAC05Report> report = new List<CAC05Report>();

            string[] exceptDebt = { "P00010001", "P00010002", "P00020001" };
            string[] exceptPmId = { "G", "M", "N", "O" };
            string[] billBookDebt = { "P00010001", "P00010002" };

            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC05_2X");
            cmd.CommandTimeout = 360;
            db.AddInParameter(cmd, "BranchId", DbType.String, pBranchId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, pFromDate);
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable repDs = ds.Tables[0];

            var repData = from rep in repDs.AsEnumerable()
                          select new
                          {
                              PaymentId = rep.Field<string>("PaymentId"),
                              CashierId = rep.Field<string>("CashierId"),
                              ControllerName = rep.Field<string>("ControllerName"),
                              PaymentDt = rep.Field<DateTime?>("PaymentDt"),
                              CaDoc = rep.Field<string>("CaDoc"),
                              InvoiceNo = rep.Field<string>("InvoiceNo"),
                              MruId = rep.Field<string>("MruId"),
                              BillBookId = rep.Field<string>("BillBookId"),
                              GroupInvoiceNo = rep.Field<string>("GroupInvoiceNo"),
                              CaId = rep.Field<string>("CaId"),
                              CCaId = rep.Field<string>("CCaId"),
                              DtId = rep.Field<string>("DtId"),
                              BranchId = rep.Field<string>("BranchId"),
                              Period = rep.Field<string>("Period"),
                              DtName = rep.Field<string>("DtName"),
                              ExtReceiptId = rep.Field<string>("ExtReceiptId"),
                              ExtReceiptDt = rep.Field<DateTime?>("ExtReceiptDt"),
                              ReceiptId = rep.Field<string>("ReceiptId"),
                              CReceiptId = rep.Field<string>("CReceiptId"),
                              PmId = rep.Field<string>("PmId"),
                              ARPaymentDt = rep.Field<DateTime?>("ARPaymentDt"),
                              GAmount = rep.Field<decimal?>("GAmount"),
                              AdjAmount = rep.Field<decimal?>("AdjAmount"),
                              ARPmId = rep.Field<string>("ARPmId"),
                              CancelARPmId = rep.Field<string>("CancelARPmId"),
                              CPaymentDt = rep.Field<DateTime?>("CPaymentDt"),
                              CCancelARPmId = rep.Field<string>("CCancelARPmId"),
                              PtId = rep.Field<string>("PtId"),
                              FeeAmount = rep.Field<decimal?>("FeeAmount"),
                              RiARPmId = rep.Field<string>("RiARPmId"),
                              Amount = rep.Field<decimal?>("Amount"),
                              CashAmount = rep.Field<decimal?>("CashAmount"),
                              ChequeAmount = rep.Field<decimal?>("ChequeAmount"),
                              TransferAmount = rep.Field<decimal?>("TransferAmount"),
                              //CreditDebitAmount = rep.Field<decimal?>("CreditDebitAmount"),
                              QRPaymentAmount = rep.Field<decimal?>("QRPaymentAmount"),         // DCR QR Payment 2023-03 : Reportt 2.8.2
                              ARGroupInvoiceNo = rep.Field<string>("ARGroupInvoiceNo"),
                              Active = rep.Field<string>("Active"),
                              CaId_Qty_New = rep.Field<int?>("caid_qty")
                          };

            //int? Caid_Qty=0;
            //if (repDs.Rows.Count > 0)
            //{
            //     Caid_Qty = (from p in repData select p.Caid_Qty).FirstOrDefault();
            //}

            var elecSec = from pa in
                              (
                                     from p in repData
                                     where !exceptDebt.Contains(p.DtId) && !exceptPmId.Contains(p.PmId) && ((p.CancelARPmId == null && p.CCancelARPmId == null) || (p.CancelARPmId == null && p.CCancelARPmId != null && SqlMethods.DateDiffDay(p.ARPaymentDt, p.CPaymentDt) > 0)) && p.Active == "1"
                                            && p.CashierId == (pControllerId == null ? p.CashierId : pControllerId)
                                     select new
                                     {
                                         p.PaymentId,
                                         p.ControllerName,
                                         p.MruId,
                                         CaId = p.CCaId,
                                         BranchId = p.DtId.Contains("MZ") ? null : p.BranchId,
                                         Period = (p.Period == null ? null : p.Period.Trim()),
                                         p.DtName,
                                         ReceiptId = p.CReceiptId,
                                         p.GAmount,
                                         p.AdjAmount,
                                         TotalAmount = p.GAmount + p.AdjAmount,
                                         PaymentDt = p.ExtReceiptDt == null ? p.PaymentDt : p.ExtReceiptDt,
                                         p.CashAmount,
                                         p.ChequeAmount,
                                         p.TransferAmount,
                                         //p.CreditDebitAmount,
                                         p.QRPaymentAmount,  // DCR QR Payment 2023-03 : Report 2.8.2
                                         CaId_Qty = 0,
                                         p.CaId_Qty_New,
                                         FeeAmount = (decimal?)0,
                                         p.CaDoc,
                                         p.InvoiceNo,
                                         RealReceiptId = p.ReceiptId
                                     })
                          group pa by new { pa.PaymentId, pa.ControllerName, pa.MruId, pa.CaId, pa.BranchId, pa.Period, pa.DtName, pa.ReceiptId, pa.GAmount, pa.AdjAmount, pa.TotalAmount, pa.PaymentDt, pa.FeeAmount, pa.CaDoc, pa.InvoiceNo, pa.RealReceiptId } into bg
                          select new
                          {
                              PaymentId = (string)bg.Key.PaymentId,
                              ControllerName = (string)bg.Key.ControllerName,
                              MruId = (string)bg.Key.MruId,
                              CaId = (string)bg.Key.CaId,
                              BranchId = (string)bg.Key.BranchId,
                              Quantity = (int?)1,
                              Period = (string)bg.Key.Period,
                              DtName = (string)bg.Key.DtName,
                              ReceiptId = (string)bg.Key.ReceiptId,
                              GAmount = (decimal?)bg.Key.GAmount,
                              AdjAmount = (decimal?)bg.Key.AdjAmount,
                              TotalAmount = (decimal?)bg.Key.TotalAmount,
                              PaymentDt = (DateTime?)bg.Key.PaymentDt,
                              CashAmount = (decimal?)(bg.Sum(pa => pa.CashAmount) == 0 ? null : bg.Sum(pa => pa.CashAmount)),
                              ChequeAmount = (decimal?)(bg.Sum(pa => pa.ChequeAmount) == 0 ? null : bg.Sum(pa => pa.ChequeAmount)),
                              TransferAmount = (decimal?)(bg.Sum(pa => pa.TransferAmount) == 0 ? null : bg.Sum(pa => pa.TransferAmount)),
                              //CreditDebitAmount = (decimal?)(bg.Sum(pa => pa.CreditDebitAmount) == 0 ? null : bg.Sum(pa => pa.CreditDebitAmount)),
                              QRPaymentAmount = (decimal?)(bg.Sum(pa => pa.QRPaymentAmount) == 0 ? null : bg.Sum(pa => pa.QRPaymentAmount)),   // DCR QR Payment 2023-03 : Report 2.8.2
                              CaId_Qty = (int?)(bg.Sum(pa => pa.CaId_Qty) == 0 ? 0 : bg.Sum(pa => pa.CaId_Qty)),
                              CaId_Qty_New = (int?)(bg.Sum(pa => pa.CaId_Qty_New) == 0 ? 0 : bg.Sum(pa => pa.CaId_Qty_New)),
                              FeeAmount = (decimal?)(bg.Key.FeeAmount == 0 ? null : bg.Key.FeeAmount),
                              RealReceiptId = (string)bg.Key.RealReceiptId,
                          };

            var billbook = from bb in
                               (
                                     from p in repData // DF#172 Kanokwan.L แก้ไขรายงาน 2.8.2 ให้แสดงรายการยกเลิก Billbook จากทาง SAP 
                                     where (billBookDebt.Contains(p.DtId) && ((p.CancelARPmId == null && p.CCancelARPmId == null) || (p.CancelARPmId == null && p.CCancelARPmId != null && SqlMethods.DateDiffDay(p.ARPaymentDt, p.CPaymentDt) > 0))) && p.Active == "1"
                                         //where (billBookDebt.Contains(p.DtId) && (p.CancelARPmId == null && p.CCancelARPmId == null)) && p.Active == "1"
                                            && p.CashierId == (pControllerId == null ? p.CashierId : pControllerId)
                                     select new
                                     {
                                         p.PaymentId,
                                         p.ControllerName,
                                         MruId = p.BillBookId.Substring(6, 9),
                                         p.CaId,
                                         p.BranchId,
                                         Period = (p.Period == null ? null : p.Period.Trim()),
                                         p.DtName,
                                         p.ReceiptId,
                                         p.GAmount,
                                         p.AdjAmount,
                                         TotalAmount = p.GAmount + p.AdjAmount,
                                         p.PaymentDt,
                                         p.Amount,
                                         CaId_Qty = 1,
                                         p.CaId_Qty_New,
                                         p.CashAmount,
                                         p.ChequeAmount,
                                         p.TransferAmount,
                                         //p.CreditDebitAmount,
                                         p.QRPaymentAmount,   // DCR QR Payment 2023-03 : Report 2.8.2
                                         RealReceiptId = p.ReceiptId,
                                         p.FeeAmount
                                     })
                           group bb by new { bb.PaymentId, bb.ControllerName, bb.MruId, bb.CaId, bb.BranchId, bb.Period, bb.DtName, bb.ReceiptId, bb.GAmount, bb.AdjAmount, bb.TotalAmount, bb.PaymentDt, bb.RealReceiptId, bb.FeeAmount } into bg
                           select new
                           {
                               PaymentId = (string)bg.Key.PaymentId,
                               ControllerName = (string)bg.Key.ControllerName,
                               MruId = (string)bg.Key.MruId,
                               CaId = (string)bg.Key.CaId,
                               BranchId = (string)bg.Key.BranchId,
                               Quantity = (int?)1,
                               Period = (string)bg.Key.Period,
                               DtName = (string)bg.Key.DtName,
                               ReceiptId = (string)bg.Key.ReceiptId,
                               GAmount = (decimal?)bg.Key.GAmount,
                               AdjAmount = (decimal?)bg.Key.AdjAmount,
                               TotalAmount = (decimal?)bg.Key.TotalAmount,
                               PaymentDt = (DateTime?)bg.Key.PaymentDt,
                               CashAmount = (decimal?)(bg.Sum(pa => pa.CashAmount) == 0 ? null : bg.Sum(pa => pa.CashAmount)),
                               ChequeAmount = (decimal?)(bg.Sum(pa => pa.ChequeAmount) == 0 ? null : bg.Sum(pa => pa.ChequeAmount)),
                               TransferAmount = (decimal?)(bg.Sum(pa => pa.TransferAmount) == 0 ? null : bg.Sum(pa => pa.TransferAmount)),
                               //CreditDebitAmount = (decimal?)(bg.Sum(pa => pa.CreditDebitAmount) == 0 ? null : bg.Sum(pa => pa.CreditDebitAmount)),
                               QRPaymentAmount = (decimal?)(bg.Sum(pa => pa.QRPaymentAmount) == 0 ? null : bg.Sum(pa => pa.QRPaymentAmount)),   // DCR QR Payment 2023-03 : Report 2.8.2
                               CaId_Qty = (int?)(bg.Sum(pa => pa.CaId_Qty) == 0 ? 0 : bg.Sum(pa => pa.CaId_Qty)),
                               CaId_Qty_New = (int?)(bg.Sum(pa => pa.CaId_Qty_New) == 0 ? 0 : bg.Sum(pa => pa.CaId_Qty_New)),
                               FeeAmount = (decimal?)(bg.Key.FeeAmount == 0 ? null : bg.Key.FeeAmount),
                               RealReceiptId = (string)bg.Key.RealReceiptId
                           };


            var groupinv = from pi in
                               (
                                    from p in repData
                                    // DF#172 Kanokwan.L แก้ไขรายงาน 2.8.2 ให้แสดงรายการยกเลิก Group จากทาง SAP 
                                    where  // /*p.PmId == "G" && */p.Active == "1" && p.GroupInvoiceNo != null && p.BillBookId != null && ((p.CancelARPmId == null && p.CCancelARPmId == null) && (p.CancelARPmId == null || p.CCancelARPmId != null && SqlMethods.DateDiffDay(p.ARPaymentDt, p.CPaymentDt) > 0))
                                        /*p.PmId == "G" && */p.Active == "1" && p.GroupInvoiceNo != null && p.BillBookId != null && ((p.CancelARPmId == null && p.CCancelARPmId == null) || (p.CancelARPmId == null && p.CCancelARPmId != null && SqlMethods.DateDiffDay(p.ARPaymentDt, p.CPaymentDt) > 0))
                                              && p.CashierId == (pControllerId == null ? p.CashierId : pControllerId)
                                    select new
                                    {
                                        p.PaymentId,
                                        p.ControllerName,
                                        //p.MruId,
                                        MruId = "มท." + p.GroupInvoiceNo,
                                        CaId = p.CCaId,
                                        BranchId = p.DtId.Contains("MZ") ? null : p.BranchId,
                                        //Period = p.Period != null ? p.Period.Trim() == "" ? null : p.Period.Trim() : p.Period,
                                        Period = (p.ReceiptId.Substring(0, 1) == "F" || p.ReceiptId.Substring(0, 1) == "E" || p.ExtReceiptDt != null) ? null : p.Period == null ? null : p.Period.Trim(),
                                        p.DtName,
                                        ReceiptId = p.CReceiptId,
                                        p.GAmount,
                                        p.AdjAmount,
                                        TotalAmount = p.GAmount + p.AdjAmount,
                                        PaymentDt = p.ExtReceiptDt == null ? p.PaymentDt : p.ExtReceiptDt,
                                        p.CashAmount,
                                        p.ChequeAmount,
                                        p.TransferAmount,
                                        //p.CreditDebitAmount,
                                        p.QRPaymentAmount,  // DCR QR Payment 2023-03 : Report 2.8.2
                                        CaId_Qty = 1,
                                        p.CaId_Qty_New,
                                        FeeAmount = (decimal?)0,
                                        p.CaDoc,
                                        p.InvoiceNo,
                                        RealReceiptId = p.ReceiptId
                                    })
                           group pi by new { pi.PaymentId, pi.ControllerName, pi.MruId, pi.CaId, pi.BranchId, pi.Period, pi.DtName, pi.ReceiptId, pi.GAmount, pi.AdjAmount, pi.TotalAmount, pi.PaymentDt, pi.FeeAmount, pi.CaDoc, pi.InvoiceNo, pi.RealReceiptId } into bg
                           select new
                           {
                               PaymentId = (string)bg.Key.PaymentId,
                               ControllerName = (string)bg.Key.ControllerName,
                               MruId = (string)bg.Key.MruId,
                               CaId = (string)bg.Key.CaId,
                               BranchId = (string)bg.Key.BranchId,
                               Quantity = (int?)1,
                               Period = (string)bg.Key.Period == "" ? null : (string)bg.Key.Period,
                               DtName = (string)bg.Key.DtName,
                               ReceiptId = (string)bg.Key.ReceiptId,
                               GAmount = (decimal?)bg.Key.GAmount,
                               AdjAmount = (decimal?)bg.Key.AdjAmount,
                               TotalAmount = (decimal?)bg.Key.TotalAmount,
                               PaymentDt = (DateTime?)bg.Key.PaymentDt,
                               CashAmount = (decimal?)(bg.Sum(pa => pa.CashAmount) == 0 ? null : bg.Sum(pa => pa.CashAmount)),
                               ChequeAmount = (decimal?)(bg.Sum(pa => pa.ChequeAmount) == 0 ? null : bg.Sum(pa => pa.ChequeAmount)),
                               TransferAmount = (decimal?)(bg.Sum(pa => pa.TransferAmount) == 0 ? null : bg.Sum(pa => pa.TransferAmount)),
                               //CreditDebitAmount = (decimal?)(bg.Sum(pa => pa.CreditDebitAmount) == 0 ? null : bg.Sum(pa => pa.CreditDebitAmount)),
                               QRPaymentAmount = (decimal?)(bg.Sum(pa => pa.QRPaymentAmount) == 0 ? null : bg.Sum(pa => pa.QRPaymentAmount)),  // DCR QR Payment 2023-03 : Report 2.8.2
                               CaId_Qty = (int?)(bg.Sum(pa => pa.CaId_Qty) == 0 ? 0 : bg.Sum(pa => pa.CaId_Qty)),
                               CaId_Qty_New = (int?)(bg.Sum(pa => pa.CaId_Qty_New) == 0 ? 0 : bg.Sum(pa => pa.CaId_Qty_New)),
                               FeeAmount = (decimal?)(bg.Key.FeeAmount == 0 ? null : bg.Key.FeeAmount),
                               RealReceiptId = (string)bg.Key.RealReceiptId
                           };

            //var repResult = elecSec.Union(billbook).Union(groupinv).OrderBy(r => r.ControllerName).ThenBy(w => w.RealReceiptId).ThenBy(w => w.PaymentDt).ThenBy(w => w.CaId).ThenBy(w => w.TotalAmount); //tanayoot_backup
            var repResult = elecSec.Union(billbook).Union(groupinv).OrderBy(w => w.PaymentDt).ThenBy(w => w.CaId).ThenBy(w => w.RealReceiptId).ThenBy(r => r.ControllerName).ThenBy(w => w.TotalAmount);

            foreach (var q in repResult)
            {
                CAC05Report detail = new CAC05Report();
                detail.PaymentId = q.PaymentId;
                detail.Mru = q.MruId;
                detail.CaId = q.CaId;
                detail.BranchId = q.BranchId;
                detail.Period = q.Period;
                detail.ControllerName = q.ControllerName;
                detail.DebtType = q.DtName;
                detail.ReceiptId = q.ReceiptId;
                //detail.BaseAmount = q.BaseAmount;
                //detail.FtAmount = q.FtAmount;
                //detail.VatAmount = q.VatAmount;
                detail.GAmount = q.GAmount;
                detail.CashAmount = q.CashAmount;
                detail.ChequeAmount = q.ChequeAmount;
                detail.TransferAmount = q.TransferAmount;
                //detail.CreditDebitAmount = q.CreditDebitAmount;
                detail.QRPaymentAmount = q.QRPaymentAmount;   // DCR QR Payment 2023-03 : Report 2.8.2
                detail.TotalAmount = q.TotalAmount;
                detail.AdjAmount = q.AdjAmount;
                detail.FeeAmount = q.FeeAmount;
                //detail.Type = q.Type;
                detail.Quantity = q.Quantity;
                //detail.DtId = q.DtId;
                detail.PaymentDt = q.PaymentDt;
                detail.CaId_Qty = q.CaId_Qty;
                detail.CaId_Qty_New = q.CaId_Qty_New;
                report.Add(detail);
            }

            return report;
        }

        public List<CAC06Report> GetReportCAC06_1(string pBranchId, string pControllerId, DateTime? pFromDate)
        {
            List<CAC06Report> report = new List<CAC06Report>();
            //pControllerId = Convert.ToInt32(pControllerId).ToString();

            string[] exceptPmId = { "M", "N", "O" };
            string[] includedDtId = { "M01000002", "M90000010", "M02000002" };
            //201801311548 Kanokwan.L แก้ไขให้รายงานแสดงค่า M02000002 การวางบิลครั้งสุดท้าย ลูกหนี้

            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC06_1X");
            cmd.CommandTimeout = 360;
            db.AddInParameter(cmd, "BranchId", DbType.String, pBranchId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, pFromDate);
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable mainRaw = ds.Tables[0];

            var repData = from rep in mainRaw.AsEnumerable()
                          select new
                          {
                              BranchId = rep.Field<string>("BranchId"),
                              RBranchId = rep.Field<string>("RBranchId"),
                              BranchName = rep.Field<string>("BranchName"),
                              TechBranchName = rep.Field<string>("TechBranchName"),
                              CaId = rep.Field<string>("CaId"),
                              PosId = rep.Field<string>("PosId"),
                              CashierId = rep.Field<string>("CashierId"),
                              CaName = rep.Field<string>("CaName"),
                              Period = rep.Field<string>("Period"),
                              PaymentDt = rep.Field<DateTime?>("PaymentDt"),
                              ExtReceiptId = rep.Field<string>("ExtReceiptId"),
                              ReceiptId = rep.Field<string>("ReceiptId"),
                              FTAmount = rep.Field<decimal?>("FTAmount"),
                              RVatAmount = rep.Field<decimal?>("RVatAmount"),
                              RGAmount = rep.Field<decimal?>("RGAmount"),
                              ControllerName = rep.Field<string>("ControllerName"),
                              PaymentId = rep.Field<string>("PaymentId"),
                              GroupInvoiceNo = rep.Field<string>("GroupInvoiceNo"),
                              BillBookId = rep.Field<string>("BillBookId"),
                              DtId = rep.Field<string>("DtId"),
                              CaDoc = rep.Field<string>("CaDoc"),
                              ARPmId = rep.Field<string>("ARPmId"),
                              VatAmount = rep.Field<decimal?>("VatAmount"),
                              AdjAmount = rep.Field<decimal?>("AdjAmount"),
                              PmId = rep.Field<string>("PmId"),
                              GAmount = rep.Field<decimal?>("GAmount"),
                              InvoiceNo = rep.Field<string>("InvoiceNo"),
                              CancelARPmId = rep.Field<string>("CancelARPmId"),
                              CCancelARPmId = rep.Field<string>("CCancelARPmId"),
                              ARPaymentDt = rep.Field<DateTime?>("ARPaymentDt"),
                              CARPaymentDt = rep.Field<DateTime?>("CARPaymentDt"),
                              Active = rep.Field<string>("Active")
                          };

            if (repData.Count() == 0) return report;

            DataTable subRaw = ds.Tables[1];

            var subData = from sub in subRaw.AsEnumerable()
                          select new
                          {
                              OriginalPaymentId = sub.Field<string>("OriginalPaymentId"),
                              GroupInvoiceNo = sub.Field<string>("GroupInvoiceNo"),
                              PaidGAmount = sub.Field<decimal?>("PaidGAmount"),
                              FTAmount = sub.Field<decimal?>("FTAmount"),
                              GAmount = sub.Field<decimal?>("GAmount"),
                              DtId = sub.Field<string>("DtId"),
                              ArActive = sub.Field<string>("ArActive"),
                              PaymentId = sub.Field<string>("PaymentId"),
                              PmId = sub.Field<string>("PmId"),
                              Pending = sub.Field<string>("Pending"),
                              Active = sub.Field<string>("Active")
                          };

            var subQuery = from s in subData
                           where /*s.PmId == "G" && */ s.Pending == "0" && s.ArActive == "1" && s.Active == "1" && s.DtId != "P00020001" && !s.PaymentId.StartsWith("ZZ")
                           group s by new { s.OriginalPaymentId, s.GroupInvoiceNo } into sg
                           select new
                           {
                               sg.Key.OriginalPaymentId,
                               sg.Key.GroupInvoiceNo,
                               FTAmount = Math.Round((double)((sg.Sum(s => s.PaidGAmount) * sg.Sum(s => s.FTAmount)) / sg.Sum(s => s.GAmount)), 2)
                           };

            var repResult = from rs in
                                (
                                    from m in
                                        (
                                            from o in repData
                                            join i in subQuery on new { PaymentId = o.PaymentId, GroupInvoiceNo = o.GroupInvoiceNo } equals new { PaymentId = i.OriginalPaymentId, GroupInvoiceNo = i.GroupInvoiceNo } into iso
                                            where ((o.CancelARPmId == null && o.CCancelARPmId == null) || (o.CancelARPmId == null && o.CCancelARPmId != null && SqlMethods.DateDiffDay(o.ARPaymentDt, o.CARPaymentDt) > 0))
                                                            && o.Active == "1"
                                                            && !exceptPmId.Contains(o.PmId)
                                                            && (o.DtId.StartsWith("MZ") ? o.BranchId : o.RBranchId == null ? o.BranchId : o.RBranchId) == pBranchId
                                                            && o.CashierId == (pControllerId == null ? o.CashierId : pControllerId)
                                            from i in iso.DefaultIfEmpty()   // left join 
                                            select new
                                            {
                                                o.BranchId,
                                                o.RBranchId,
                                                o.BranchName,
                                                o.TechBranchName,
                                                o.CaId,
                                                o.PosId,
                                                o.CaName,
                                                o.Period,
                                                o.PaymentDt,
                                                o.ReceiptId,
                                                o.ExtReceiptId,
                                                o.DtId,
                                                o.GAmount,
                                                o.VatAmount,
                                                o.AdjAmount,
                                                o.FTAmount,
                                                o.RGAmount,
                                                o.RVatAmount,
                                                IFTAmount = i == null ? 0 : i.FTAmount,
                                                o.ARPmId,
                                                o.ControllerName,
                                                o.GroupInvoiceNo,
                                                o.BillBookId,
                                                o.CaDoc,
                                                o.InvoiceNo,
                                                o.ARPaymentDt,
                                                o.CARPaymentDt
                                            })
                                    group m by new { m.BranchId, m.BranchName, m.TechBranchName, m.RBranchId, m.ARPmId, m.InvoiceNo, m.ReceiptId, m.CARPaymentDt, m.ARPaymentDt } into og
                                    select new
                                    {
                                        PaidBranchId = og.Key.BranchId == pBranchId ? (og.Min(m => m.DtId).StartsWith("MZ") ? og.Key.BranchId : og.Key.RBranchId) : og.Key.BranchId,
                                        PaidBranchName = og.Key.BranchId == pBranchId ? (og.Min(m => m.DtId).StartsWith("MZ") ? og.Key.BranchName : og.Key.TechBranchName) : og.Key.BranchName,
                                        CaId = og.Max(m => m.CaId),
                                        PosId = og.Max(m => m.PosId),
                                        CaName = og.Max(m => m.CaName),
                                        Period = og.Max(m => m.Period),
                                        PaymentDt = og.Max(m => m.PaymentDt),
                                        //ReceiptId = og.Max(m => m.CReceiptId),
                                        ReceiptId = (og.Max(m => m.ExtReceiptId) == null ? og.Key.ReceiptId : og.Max(m => m.ExtReceiptId) + ((SqlMethods.DateDiffDay(og.Key.ARPaymentDt, og.Key.CARPaymentDt == null ? og.Key.ARPaymentDt : og.Key.CARPaymentDt) > 0) ? " *" : "")),
                                        BaseAmount = og.Min(m => m.DtId) == "P00020001" ? (decimal)og.Min(m => m.GAmount) - (decimal)og.Min(m => m.IFTAmount) - (decimal)og.Min(m => m.VatAmount) :
                                                                (decimal)og.Min(m => m.GAmount) - Math.Round((decimal)((og.Min(m => m.GAmount) * (og.Sum(m => m.FTAmount) / og.Count())) / (og.Sum(m => m.RGAmount) / og.Count())), 2) - Math.Round((decimal)((og.Min(m => m.GAmount) * (og.Sum(m => m.VatAmount) / og.Count())) / (og.Sum(m => m.RGAmount) / og.Count())), 2),
                                        FtAmount = og.Min(m => m.DtId) == "P00020001" ? (decimal)og.Min(m => m.IFTAmount) :
                                                                Math.Round((decimal)((og.Min(m => m.GAmount) * (og.Sum(m => m.FTAmount) / og.Count())) / (og.Sum(m => m.RGAmount) / og.Count())), 2),
                                        VatAmount = og.Min(m => m.VatAmount),
                                        AdjAmount = og.Min(m => m.AdjAmount),
                                        GAmount = og.Min(m => m.GAmount),
                                        ControllerName = og.Max(m => m.ControllerName),
                                        DtId = includedDtId.Contains(og.Max(m => m.DtId)) && og.Max(m => m.GroupInvoiceNo) == null && og.Max(m => m.BillBookId) == null ? "1" :
                                                    og.Max(m => m.DtId) == "P00020001" ? "2" :
                                                    (og.Max(m => m.DtId) == "M01000002" || og.Max(m => m.DtId) == "M02000002") && og.Max(m => m.GroupInvoiceNo) != null ? "2" : //201801311548 Kanokwan.L แก้ไขให้รายงานแสดงค่า M02000002 การวางบิลครั้งสุดท้าย ลูกหนี้
                                                    og.Max(m => m.DtId) == "P00010001" ? "3" :
                                                    og.Max(m => m.DtId) == "P00010002" ? "4" : "5",
                                        CaDoc = includedDtId.Contains(og.Max(m => m.DtId)) && og.Max(m => m.CaDoc) != null ? og.Max(m => m.CaDoc) : (og.Max(m => m.CaDoc) == null ? og.Key.ReceiptId : og.Max(m => m.CaDoc)),
                                        og.Key.InvoiceNo
                                    })
                            group rs by new { rs.PaidBranchId, rs.PaidBranchName, rs.CaId, rs.PosId, rs.CaName, rs.Period, rs.PaymentDt, rs.ReceiptId, rs.BaseAmount, rs.FtAmount, rs.VatAmount, rs.AdjAmount, rs.GAmount, rs.ControllerName, rs.DtId, rs.CaDoc, rs.InvoiceNo } into rg
                            orderby rg.Key.PaidBranchId, rg.Key.PaidBranchName, rg.Key.CaId, rg.Key.PosId, rg.Key.ReceiptId, rg.Key.CaName, rg.Key.Period, rg.Key.PaymentDt,
                                    rg.Key.BaseAmount, rg.Key.FtAmount, rg.Key.VatAmount, rg.Key.AdjAmount, rg.Key.GAmount, rg.Key.ControllerName, rg.Key.DtId, rg.Key.CaDoc, rg.Key.InvoiceNo
                            select new
                            {
                                PaidBranchId = rg.Key.PaidBranchId,
                                PaidBranchName = rg.Key.PaidBranchName,
                                CaId = rg.Key.CaId,
                                PosId = rg.Key.PosId,
                                CaName = rg.Key.CaName,
                                Period = rg.Key.Period,
                                PaymentDt = rg.Key.PaymentDt,
                                ReceiptId = rg.Key.ReceiptId,
                                BaseAmount = rg.Key.BaseAmount,
                                FtAmount = rg.Key.GAmount - rg.Key.BaseAmount - rg.Key.VatAmount,
                                VatAmount = rg.Key.VatAmount,
                                AdjAmount = rg.Key.AdjAmount,
                                GAmount = rg.Key.GAmount - rg.Key.AdjAmount,
                                ControllerName = rg.Key.ControllerName,
                                DtId = rg.Key.DtId,
                                CaDoc = rg.Key.CaDoc
                            };

            foreach (var q in repResult)
            {
                CAC06Report detail = new CAC06Report();
                detail.PaidBranchId = q.PaidBranchId;
                detail.PaidBranchName = q.PaidBranchName;
                //detail.BranchDetail =
                detail.CaId = q.CaId;
                detail.PosId = q.PosId;
                detail.CaName = q.CaName;
                detail.Period = q.Period;
                detail.ReceiptId = q.ReceiptId;
                detail.PaymentDt = q.PaymentDt;
                detail.BaseAmount = q.BaseAmount;
                detail.FtAmount = q.FtAmount;
                detail.VatAmount = q.VatAmount;
                detail.GAmount = q.GAmount;
                detail.ControllerName = q.ControllerName;
                detail.DtId = q.DtId;
                detail.CaDoc = q.CaDoc;
                report.Add(detail);
            }


            return report;
        }

        public List<CAC06Report> GetReportCAC06_2(string pBranchId, string pControllerId, DateTime? pFromDate)
        {
            List<CAC06Report> report = new List<CAC06Report>();
            // pControllerId = Convert.ToInt32(pControllerId).ToString();

            string[] exceptPmId = { "M", "N", "O" };
            string[] includedDtId = { "M01000002", "M90000010", "M02000002" };
            string[] excludedDtId = { "M01000002", "M90000010", "P00010001", "P00010002", "M02000002" };
            //201801311548 Kanokwan.L แก้ไขให้รายงานแสดงค่า M02000002 การวางบิลครั้งสุดท้าย ลูกหนี้

            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC06_2X");
            cmd.CommandTimeout = 360;
            db.AddInParameter(cmd, "BranchId", DbType.String, pBranchId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, pFromDate);
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable mainRaw = ds.Tables[0];

            var repData = from rep in mainRaw.AsEnumerable()
                          select new
                          {
                              BranchId = rep.Field<string>("BranchId"),
                              RBranchId = rep.Field<string>("RBranchId"),
                              BranchName = rep.Field<string>("BranchName"),
                              TechBranchName = rep.Field<string>("TechBranchName"),
                              CaId = rep.Field<string>("CaId"),
                              CashierId = rep.Field<string>("CashierId"),
                              PosId = rep.Field<string>("PosId"),
                              CaName = rep.Field<string>("CaName"),
                              Period = rep.Field<string>("Period"),
                              PaymentDt = rep.Field<DateTime?>("PaymentDt"),
                              CReceiptId = rep.Field<string>("CReceiptId"),
                              FTAmount = rep.Field<decimal?>("FTAmount"),
                              RVatAmount = rep.Field<decimal?>("RVatAmount"),
                              RGAmount = rep.Field<decimal?>("RGAmount"),
                              ControllerName = rep.Field<string>("ControllerName"),
                              PaymentId = rep.Field<string>("PaymentId"),
                              GroupInvoiceNo = rep.Field<string>("GroupInvoiceNo"),
                              BillBookId = rep.Field<string>("BillBookId"),
                              DtId = rep.Field<string>("DtId"),
                              CaDoc = rep.Field<string>("CaDoc"),
                              ReceiptId = rep.Field<string>("ReceiptId"),
                              ARPmId = rep.Field<string>("ARPmId"),
                              VatAmount = rep.Field<decimal?>("VatAmount"),
                              AdjAmount = rep.Field<decimal?>("AdjAmount"),
                              PmId = rep.Field<string>("PmId"),
                              GAmount = rep.Field<decimal?>("GAmount"),
                              InvoiceNo = rep.Field<string>("InvoiceNo"),
                              CancelARPmId = rep.Field<string>("CancelARPmId"),
                              CCancelARPmId = rep.Field<string>("CCancelARPmId"),
                              ARPaymentDt = rep.Field<DateTime?>("ARPaymentDt"),
                              CARPaymentDt = rep.Field<DateTime?>("CARPaymentDt"),
                              Active = rep.Field<string>("Active")
                          };


            if (repData.Count() == 0) return report;

            DataTable subRaw = ds.Tables[1];

            var subData = from sub in subRaw.AsEnumerable()
                          select new
                          {
                              OriginalPaymentId = sub.Field<string>("OriginalPaymentId"),
                              GroupInvoiceNo = sub.Field<string>("GroupInvoiceNo"),
                              PaidGAmount = sub.Field<decimal?>("PaidGAmount"),
                              FTAmount = sub.Field<decimal?>("FTAmount"),
                              GAmount = sub.Field<decimal?>("GAmount"),
                              DtId = sub.Field<string>("DtId"),
                              ArActive = sub.Field<string>("ArActive"),
                              PaymentId = sub.Field<string>("PaymentId"),
                              PmId = sub.Field<string>("PmId"),
                              Pending = sub.Field<string>("Pending"),
                              Active = sub.Field<string>("Active")
                          };

            var subQuery = from s in subData
                           where /*s.PmId == "G" && */ s.Pending == "0" && s.ArActive == "1" && s.Active == "1" && s.DtId != "P00020001" && !s.PaymentId.StartsWith("ZZ")
                           group s by new { s.OriginalPaymentId, s.GroupInvoiceNo } into sg
                           select new
                           {
                               sg.Key.OriginalPaymentId,
                               sg.Key.GroupInvoiceNo,
                               FTAmount = Math.Round((double)((sg.Sum(s => s.PaidGAmount) * sg.Sum(s => s.FTAmount)) / sg.Sum(s => s.GAmount)), 2)
                           };

            var repResult = from rs in
                                (
                                    from m in
                                        (
                                            from o in repData
                                            join i in subQuery on new { PaymentId = o.PaymentId, GroupInvoiceNo = o.GroupInvoiceNo } equals new { PaymentId = i.OriginalPaymentId, GroupInvoiceNo = i.GroupInvoiceNo } into iso
                                            where ((o.CancelARPmId == null && o.CCancelARPmId == null) || (o.CancelARPmId == null && o.CCancelARPmId != null && SqlMethods.DateDiffDay(o.ARPaymentDt, o.CARPaymentDt) > 0))
                                                            && o.Active == "1"
                                                            && !exceptPmId.Contains(o.PmId)
                                                            && (o.DtId.StartsWith("MZ") ? o.BranchId : o.RBranchId == null ? o.BranchId : o.RBranchId) != pBranchId
                                                            && o.CashierId == (pControllerId == null ? o.CashierId : pControllerId)
                                            from i in iso.DefaultIfEmpty()   // left join 
                                            select new
                                            {
                                                o.BranchId,
                                                o.RBranchId,
                                                o.BranchName,
                                                o.TechBranchName,
                                                o.CaId,
                                                o.PosId,
                                                o.CaName,
                                                Period = (o.Period == null ? null : o.Period.Trim()),
                                                o.PaymentDt,
                                                o.CReceiptId,
                                                o.DtId,
                                                o.GAmount,
                                                o.VatAmount,
                                                o.AdjAmount,
                                                o.FTAmount,
                                                o.RGAmount,
                                                o.RVatAmount,
                                                IFTAmount = i == null ? 0 : i.FTAmount,
                                                o.ARPmId,
                                                o.ControllerName,
                                                o.GroupInvoiceNo,
                                                o.BillBookId,
                                                o.CaDoc,
                                                o.ReceiptId,
                                                o.InvoiceNo,
                                                o.ARPaymentDt,
                                                o.CARPaymentDt
                                            })
                                    group m by new { m.BranchId, m.BranchName, m.TechBranchName, m.RBranchId, m.ARPmId, m.InvoiceNo, m.ReceiptId, m.CARPaymentDt, m.ARPaymentDt } into og
                                    select new
                                    {
                                        PaidBranchId = og.Key.BranchId == pBranchId ? (og.Min(m => m.DtId).StartsWith("MZ") ? og.Key.BranchId : og.Key.RBranchId) : og.Key.BranchId,
                                        PaidBranchName = og.Key.BranchId == pBranchId ? (og.Min(m => m.DtId).StartsWith("MZ") ? og.Key.BranchName : og.Key.TechBranchName) : og.Key.BranchName,
                                        CaId = og.Max(m => m.CaId),
                                        PosId = og.Max(m => m.PosId),
                                        CaName = og.Max(m => m.CaName),
                                        Period = og.Max(m => m.Period),
                                        PaymentDt = og.Max(m => m.PaymentDt),
                                        ReceiptId = og.Max(m => m.CReceiptId),
                                        BaseAmount = og.Min(m => m.DtId) == "P00020001" ? (decimal)og.Min(m => m.GAmount) - (decimal)og.Min(m => m.IFTAmount) - (decimal)og.Min(m => m.VatAmount) :
                                                                (decimal)og.Min(m => m.GAmount) - Math.Round((decimal)((og.Min(m => m.GAmount) * (og.Sum(m => m.FTAmount) / og.Count())) / (og.Sum(m => m.RGAmount) / og.Count())), 2) - Math.Round((decimal)((og.Min(m => m.GAmount) * (og.Sum(m => m.VatAmount) / og.Count())) / (og.Sum(m => m.RGAmount) / og.Count())), 2),
                                        FtAmount = og.Min(m => m.DtId) == "P00020001" ? (decimal)og.Min(m => m.IFTAmount) :
                                                                Math.Round((decimal)((og.Min(m => m.GAmount) * (og.Sum(m => m.FTAmount) / og.Count())) / (og.Sum(m => m.RGAmount) / og.Count())), 2),
                                        VatAmount = og.Min(m => m.VatAmount),
                                        AdjAmount = og.Min(m => m.AdjAmount),
                                        GAmount = og.Min(m => m.GAmount),
                                        ControllerName = og.Max(m => m.ControllerName),
                                        DtId = includedDtId.Contains(og.Max(m => m.DtId)) && og.Max(m => m.GroupInvoiceNo) == null && og.Max(m => m.BillBookId) == null ? "1" :
                                                    og.Max(m => m.DtId) == "P00020001" ? "2" :
                                                    (og.Max(m => m.DtId) == "M01000002" || og.Max(m => m.DtId) == "M02000002") && og.Max(m => m.GroupInvoiceNo) != null ? "2" : //201801311548 Kanokwan.L แก้ไขให้รายงานแสดงค่า M02000002 การวางบิลครั้งสุดท้าย ลูกหนี้
                                                    og.Max(m => m.DtId) == "P00010001" ? "3" :
                                                    og.Max(m => m.DtId) == "P00010002" ? "4" :
                                                    !excludedDtId.Contains(og.Max(m => m.DtId)) && og.Max(m => m.GroupInvoiceNo) == null ? "5" : null,
                                        CaDoc = includedDtId.Contains(og.Max(m => m.DtId)) && og.Max(m => m.CaDoc) != null ? og.Max(m => m.CaDoc) : (og.Max(m => m.CaDoc) == null ? og.Key.ReceiptId : og.Max(m => m.CaDoc)),
                                        og.Key.InvoiceNo
                                    })
                            group rs by new { rs.PaidBranchId, rs.PaidBranchName, rs.CaId, rs.PosId, rs.CaName, rs.Period, rs.PaymentDt, rs.ReceiptId, rs.BaseAmount, rs.FtAmount, rs.VatAmount, rs.AdjAmount, rs.GAmount, rs.ControllerName, rs.DtId, rs.CaDoc, rs.InvoiceNo } into rg
                            orderby rg.Key.ControllerName, rg.Key.ReceiptId, rg.Key.CaId, rg.Key.PosId, rg.Key.CaName, rg.Key.Period, rg.Key.PaymentDt,
                                    rg.Key.BaseAmount, rg.Key.FtAmount, rg.Key.VatAmount, rg.Key.AdjAmount, rg.Key.GAmount, rg.Key.DtId, rg.Key.CaDoc, rg.Key.InvoiceNo
                            select new
                            {
                                BranchDetail = rg.Key.PaidBranchId + " - " + rg.Key.PaidBranchName,
                                CaId = rg.Key.CaId,
                                PosId = rg.Key.PosId,
                                CaName = rg.Key.CaName,
                                Period = rg.Key.Period,
                                PaymentDt = rg.Key.PaymentDt,
                                ReceiptId = rg.Key.ReceiptId,
                                BaseAmount = rg.Key.BaseAmount,
                                FtAmount = rg.Key.GAmount - rg.Key.BaseAmount - rg.Key.VatAmount,
                                VatAmount = rg.Key.VatAmount,
                                AdjAmount = rg.Key.AdjAmount,
                                GAmount = rg.Key.GAmount - rg.Key.AdjAmount,
                                ControllerName = rg.Key.ControllerName,
                                DtId = rg.Key.DtId,
                                CaDoc = rg.Key.CaDoc
                            };

            foreach (var q in repResult)
            {
                CAC06Report detail = new CAC06Report();
                //detail.PaidBranchId = q.PaidBranchId;
                //detail.PaidBranchName = q.PaidBranchName;
                detail.BranchDetail = q.BranchDetail;
                detail.CaId = q.CaId;
                detail.PosId = q.PosId;
                detail.CaName = q.CaName;
                detail.Period = q.Period;
                detail.ReceiptId = q.ReceiptId;
                detail.PaymentDt = q.PaymentDt;
                detail.BaseAmount = q.BaseAmount;
                detail.FtAmount = q.FtAmount;
                detail.VatAmount = q.VatAmount;
                detail.GAmount = q.GAmount;
                detail.ControllerName = q.ControllerName;
                detail.DtId = q.DtId;
                detail.CaDoc = q.CaDoc;
                report.Add(detail);
            }

            report.Sort(new ObjectComparer<CAC06Report>("PosId ASC, ReceiptId ASC, CaDoc ASC, GAmount ASC", true));

            return report;
        }

        public List<CAC06Report> GetReportCAC07(string pBranchId, string pControllerId, DateTime? pFromDate)
        {
            List<CAC06Report> report = new List<CAC06Report>();
            //pControllerId = Convert.ToInt32(pControllerId).ToString();

            string[] exceptPmId = { "M", "N", "O" };
            string[] exceptDtId = { "M01000002", "M90000010", "M02000002" };
            //201801311548 Kanokwan.L แก้ไขให้รายงานแสดงค่า M02000002 การวางบิลครั้งสุดท้าย ลูกหนี้

            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC07X");
            db.AddInParameter(cmd, "BranchId", DbType.String, pBranchId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, pFromDate);
            cmd.CommandTimeout = 360;
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable mainRaw = ds.Tables[0];

            var repData = from rep in mainRaw.AsEnumerable()
                          select new
                          {
                              CaId = rep.Field<string>("CaId"),
                              PosId = rep.Field<string>("PosId"),
                              BranchId = rep.Field<string>("BranchId"),
                              BranchName = rep.Field<string>("BranchName"),
                              RBranchId = rep.Field<string>("RBranchId"),
                              MruId = rep.Field<string>("MruId"),
                              CaName = rep.Field<string>("CaName"),
                              Period = rep.Field<string>("Period"),
                              PaymentDt = rep.Field<DateTime?>("PaymentDt"),
                              ReceiptId = rep.Field<string>("ReceiptId"),
                              FTAmount = rep.Field<decimal?>("FTAmount"),
                              ARPmId = rep.Field<string>("ARPmId"),
                              PmId = rep.Field<string>("PmId"),
                              RGAmount = rep.Field<decimal?>("RGAmount"),
                              VatAmount = rep.Field<decimal?>("VatAmount"),
                              GAmount = rep.Field<decimal?>("GAmount"),
                              ControllerName = rep.Field<string>("ControllerName"),
                              ControllerAccName = rep.Field<string>("ControllerAccName"),
                              DtId = rep.Field<string>("DtId"),
                              GroupInvoiceNo = rep.Field<string>("GroupInvoiceNo"),
                              PaymentId = rep.Field<string>("PaymentId"),
                              CancelARPmId = rep.Field<string>("CancelARPmId"),
                              CCancelARPmId = rep.Field<string>("CCancelARPmId"),
                              ARPaymentDt = rep.Field<DateTime?>("ARPaymentDt"),
                              CARPaymentDt = rep.Field<DateTime?>("CARPaymentDt"),
                              Active = rep.Field<string>("Active")
                          };

            if (repData.Count() == 0) return report;

            DataTable subRaw = ds.Tables[1];

            var subData = from sub in subRaw.AsEnumerable()
                          select new
                          {
                              OriginalPaymentId = sub.Field<string>("OriginalPaymentId"),
                              GroupInvoiceNo = sub.Field<string>("GroupInvoiceNo"),
                              PaidGAmount = sub.Field<decimal?>("PaidGAmount"),
                              FTAmount = sub.Field<decimal?>("FTAmount"),
                              GAmount = sub.Field<decimal?>("GAmount"),
                              DtId = sub.Field<string>("DtId"),
                              ArActive = sub.Field<string>("ArActive"),
                              PaymentId = sub.Field<string>("PaymentId"),
                              PmId = sub.Field<string>("PmId"),
                              Pending = sub.Field<string>("Pending"),
                              Active = sub.Field<string>("Active")
                          };

            var subQuery = from s in subData
                           where /*s.PmId == "G" && */ s.Pending == "0" && s.ArActive == "1" && s.Active == "1" && s.DtId != "P00020001" && !s.PaymentId.StartsWith("ZZ")
                           group s by new { s.OriginalPaymentId, s.GroupInvoiceNo } into sg
                           select new
                           {
                               sg.Key.OriginalPaymentId,
                               sg.Key.GroupInvoiceNo,
                               FTAmount = Math.Round((double)((sg.Sum(s => s.PaidGAmount) * sg.Sum(s => s.FTAmount)) / sg.Sum(s => s.GAmount)), 2)
                           };

            var repResult = from m in
                                (
                                  from o in repData
                                  join i in subQuery on new { PaymentId = o.PaymentId, GroupInvoiceNo = o.GroupInvoiceNo } equals new { PaymentId = i.OriginalPaymentId, GroupInvoiceNo = i.GroupInvoiceNo } into iso
                                  from ix in iso.DefaultIfEmpty()   // left join 
                                  where ((o.CancelARPmId == null && o.CCancelARPmId == null) || (o.CancelARPmId == null && o.CCancelARPmId != null && SqlMethods.DateDiffDay(o.ARPaymentDt, o.CARPaymentDt) > 0)) && !exceptPmId.Contains(o.PmId) && o.BranchId != o.RBranchId
                                         && o.Active == "1"
                                         && o.ControllerAccName == (pControllerId == null ? o.ControllerAccName : pControllerId)
                                  select new { BranchDetail = o.BranchId + " - " + o.BranchName, o.CaId, o.PosId, o.MruId, o.CaName, o.Period, o.PaymentDt, o.ReceiptId, o.DtId, o.GAmount, IFTAmount = ix == null ? 0 : ix.FTAmount, o.FTAmount, o.RGAmount, o.VatAmount, o.ControllerName, o.ControllerAccName }
                                )
                            group m by new { m.BranchDetail, m.CaId, m.PosId, m.MruId, m.CaName, m.Period, m.PaymentDt, m.ReceiptId, m.ControllerName, m.ControllerAccName } into mg
                            orderby mg.Key.PaymentDt, mg.Key.ControllerAccName, mg.Key.MruId, mg.Key.CaId, mg.Key.ReceiptId
                            select new
                            {
                                BranchDetail = mg.Key.BranchDetail,
                                CaId = mg.Key.CaId,
                                PosId = mg.Key.PosId,
                                CaName = mg.Key.CaName,
                                Period = mg.Key.Period,
                                PaymentDt = mg.Key.PaymentDt,
                                ReceiptId = mg.Key.ReceiptId,
                                BaseAmount = mg.Min(m => m.DtId) == "P00020001" ? (decimal)mg.Min(m => m.GAmount) - (decimal)mg.Min(m => m.IFTAmount) - (decimal)mg.Min(m => m.VatAmount) :
                                                        (decimal)mg.Min(m => m.GAmount) - Math.Round((decimal)((mg.Min(m => m.GAmount) * (mg.Sum(m => m.FTAmount) / mg.Count())) / (mg.Sum(m => m.RGAmount) / mg.Count())), 2) - Math.Round((decimal)((mg.Min(m => m.GAmount) * (mg.Sum(m => m.VatAmount) / mg.Count())) / (mg.Sum(m => m.RGAmount) / mg.Count())), 2),
                                FtAmount = (mg.Min(m => m.DtId) == "P00020001" ? (decimal)mg.Min(m => m.FTAmount) :
                                                        Math.Round((decimal)((mg.Min(m => m.GAmount) * (mg.Sum(m => m.FTAmount) / mg.Count())) / (mg.Sum(m => m.RGAmount) / mg.Count())), 2)),
                                VatAmount = (decimal)mg.Min(m => m.VatAmount),
                                GAmount = mg.Min(m => m.GAmount),
                                ControllerName = mg.Key.ControllerName,
                                ControllerAccName = mg.Key.ControllerAccName,
                                DtId = exceptDtId.Contains(mg.Min(m => m.DtId)) ? "1" : "2"
                            };


            foreach (var q in repResult)
            {
                CAC06Report detail = new CAC06Report();
                detail.BranchDetail = q.BranchDetail;
                detail.CaId = q.CaId;
                detail.PosId = q.PosId;
                detail.CaName = q.CaName;
                detail.Period = q.Period;
                detail.ReceiptId = q.ReceiptId;
                detail.PaymentDt = q.PaymentDt;
                detail.BaseAmount = q.BaseAmount;
                detail.FtAmount = q.FtAmount;
                detail.VatAmount = q.VatAmount;
                detail.GAmount = q.GAmount;
                detail.ControllerName = q.ControllerName;
                detail.DtId = q.DtId;
                detail.ControllerAccName = q.ControllerAccName;
                report.Add(detail);
            }

            return report;
        }

        public List<CAC09Report> GetReportCAC09_1(string pBranchId, string pPosId, DateTime? pFromDate)
        {
            List<CAC09Report> report = new List<CAC09Report>();

            string[] exceptPmId = { "M", "N", "O" };
            string[] exceptDtId = { "M01000002", "M02000002", "M90000010", "M90000030", "M00100100" };
            //201801311548 Kanokwan.L แก้ไขให้รายงานแสดงค่า M02000002 การวางบิลครั้งสุดท้าย ลูกหนี้
            //string[] ServiceDtId = { "M00172500", "M00172600", "M00172100", "M00178200", "M00172800", "M00172000", "M00178300", "M00173000" };

            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC09_1X");
            db.AddInParameter(cmd, "BranchId", DbType.String, pBranchId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, pFromDate);
            cmd.CommandTimeout = 360;
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable mainRaw = ds.Tables[0];

            var repData = from rep in mainRaw.AsEnumerable()
                          select new
                          {
                              TerminalCode = rep.Field<string>("TerminalCode"),
                              PosId = rep.Field<string>("PosId"),
                              ReceiptId = rep.Field<string>("ReceiptId"),
                              ExtReceiptId = rep.Field<string>("ExtReceiptId"),
                              ReceiptType = rep.Field<string>("ReceiptType"),
                              DtId = rep.Field<string>("DtId"),
                              CaPayer = rep.Field<string>("CaPayer"),
                              OriginalInvoiceNo = rep.Field<string>("OriginalInvoiceNo"),
                              PaymentId = rep.Field<string>("PaymentId"),
                              GroupInvoiceNo = rep.Field<string>("GroupInvoiceNo"),
                              FTAmount = rep.Field<decimal?>("FTAmount"),
                              RVatAmount = rep.Field<decimal?>("RVatAmount"),
                              RGAmount = rep.Field<decimal?>("RGAmount"),
                              InvoiceNo = rep.Field<string>("InvoiceNo"),
                              ARPmId = rep.Field<string>("ARPmId"),
                              PmId = rep.Field<string>("PmId"),
                              PGAmount = rep.Field<decimal?>("PGAmount"),
                              VatAmount = rep.Field<decimal?>("VatAmount"),
                              AdjAmount = rep.Field<decimal?>("AdjAmount"),
                              GAmount = rep.Field<decimal?>("GAmount"),
                              CancelARPmId = rep.Field<string>("CancelARPmId"),
                              CCancelARPmId = rep.Field<string>("CCancelARPmId"),
                              ARPaymentDt = rep.Field<DateTime?>("ARPaymentDt"),
                              CARPaymentDt = rep.Field<DateTime?>("CARPaymentDt"),
                              Active = rep.Field<string>("Active")
                          };

            if (repData.Count() == 0) return report;

            DataTable subRaw = ds.Tables[1];

            var subData = from sub in subRaw.AsEnumerable()
                          select new
                          {
                              OriginalPaymentId = sub.Field<string>("OriginalPaymentId"),
                              GroupInvoiceNo = sub.Field<string>("GroupInvoiceNo"),
                              PaidGAmount = sub.Field<decimal?>("PaidGAmount"),
                              FTAmount = sub.Field<decimal?>("FTAmount"),
                              GAmount = sub.Field<decimal?>("GAmount"),
                              DtId = sub.Field<string>("DtId"),
                              ArActive = sub.Field<string>("ArActive"),
                              PaymentId = sub.Field<string>("PaymentId"),
                              PmId = sub.Field<string>("PmId"),
                              Pending = sub.Field<string>("Pending"),
                              Active = sub.Field<string>("Active")
                          };

            var subQuery = from s in subData
                           where /*s.PmId == "G" && */ s.Pending == "0" && s.ArActive == "1" && s.Active == "1" && s.DtId != "P00020001" && !s.PaymentId.StartsWith("ZZ")
                           group s by new { s.OriginalPaymentId, s.GroupInvoiceNo } into sg
                           select new
                           {
                               sg.Key.OriginalPaymentId,
                               sg.Key.GroupInvoiceNo,
                               FTAmount = Math.Round((decimal)((sg.Sum(s => s.PaidGAmount) * sg.Sum(s => s.FTAmount)) / sg.Sum(s => s.GAmount)), 2)
                           };

            var repResult = from rs in
                                (
                                    from m in
                                        (
                                            from o in repData
                                            join i in subQuery on new { PaymentId = o.PaymentId, GroupInvoiceNo = o.GroupInvoiceNo } equals new { PaymentId = i.OriginalPaymentId, GroupInvoiceNo = i.GroupInvoiceNo } into iso
                                            where ((o.CancelARPmId == null && o.CCancelARPmId == null) || (o.CancelARPmId == null && o.CCancelARPmId != null && SqlMethods.DateDiffDay(o.ARPaymentDt, o.CARPaymentDt) > 0))
                                                            && o.Active == "1"
                                                            && !exceptPmId.Contains(o.PmId)
                                                            && o.PosId == (pPosId == null ? o.PosId : pPosId)
                                            from g in iso.DefaultIfEmpty()   // left join 
                                            select new
                                            {
                                                o.TerminalCode,
                                                o.ReceiptId,
                                                o.ExtReceiptId,
                                                o.DtId,
                                                o.CaPayer,
                                                IFTAmount = g == null ? 0 : g.FTAmount,
                                                o.GAmount,
                                                o.FTAmount,
                                                o.VatAmount,
                                                o.RGAmount,
                                                o.RVatAmount,
                                                o.PGAmount,
                                                o.AdjAmount,
                                                o.ReceiptType,
                                                o.OriginalInvoiceNo,
                                                o.ARPmId,
                                                o.InvoiceNo,
                                                o.CARPaymentDt,
                                                o.ARPaymentDt
                                            })
                                    group m by new { m.ExtReceiptId, m.ReceiptId, m.ReceiptType, m.TerminalCode, m.OriginalInvoiceNo, m.ARPmId, m.InvoiceNo, m.CARPaymentDt, m.ARPaymentDt } into og
                                    select new
                                    {
                                        og.Key.TerminalCode,
                                        Quantity = og.Select(m => m.ReceiptId).Distinct().Count(),
                                        og.Key.ReceiptId,
                                        og.Key.ExtReceiptId,
                                        ElectricBaseAmount = og.Min(m => m.DtId) == "P00020001" ? (decimal)og.Min(m => m.GAmount) - (decimal)og.Min(m => m.IFTAmount) - (decimal)og.Min(m => m.VatAmount) :
                                                                (decimal)og.Min(m => m.GAmount) - Math.Round((decimal)((og.Min(m => m.GAmount) * (og.Sum(m => m.FTAmount) / og.Count())) / (og.Sum(m => m.RGAmount) / og.Count())), 2) - Math.Round((decimal)((og.Min(m => m.GAmount) * (og.Sum(m => m.VatAmount) / og.Count())) / (og.Sum(m => m.RGAmount) / og.Count())), 2),
                                        FtAmount = og.Min(m => m.DtId) == "P00020001" ? (decimal)og.Min(m => m.IFTAmount) :
                                                                Math.Round((decimal)((og.Min(m => m.GAmount) * (og.Sum(m => m.FTAmount) / og.Count())) / (og.Sum(m => m.RGAmount) / og.Count())), 2),
                                        TotalVatAmount = og.Min(m => m.VatAmount),
                                        RPGAmount = og.Sum(m => m.PGAmount),
                                        VatAmount = og.Min(m => m.VatAmount),
                                        AdjAmount = og.Min(m => m.AdjAmount),
                                        GAmount = og.Min(m => m.GAmount),
                                        Type = exceptDtId.Contains(og.Min(m => m.DtId)) ? "ค่าไฟฟ้า" :
                                                    og.Min(m => m.CaPayer).Substring(0, 6) == "000004" && (og.Min(m => m.RVatAmount) != 0) ? "ค่าอื่นๆ (มีภาษี)" : // กรณีใบเสร็จรวมค่าบริการพาดสาย มี VAT
                                                    og.Min(m => m.CaPayer).Substring(0, 6) == "000004" && (og.Min(m => m.RVatAmount) == 0) ? "ค่าอื่นๆ (ไม่มีภาษี)" : // กรณีใบเสร็จรวมค่าบริการพาดสาย ไม่มี VAT
                                                    (og.Min(m => m.CaPayer).Substring(0, 3) == "091" || og.Min(m => m.CaPayer).Substring(0, 3) == "092") && (og.Min(m => m.RVatAmount) != 0) ? "ค่าอื่นๆ (มีภาษี)" : // กรณีใบเสร็จแยกค่าบริการพาดสาย มี VAT
                                                    (og.Min(m => m.CaPayer).Substring(0, 3) == "091" || og.Min(m => m.CaPayer).Substring(0, 3) == "092") && (og.Min(m => m.RVatAmount) == 0) ? "ค่าอื่นๆ (ไม่มีภาษี)" : // กรณีใบเสร็จแยกค่าบริการพาดสาย ไม่มี VAT
                                                    og.Min(m => m.DtId) == "P00020001" ? "ค่าไฟฟ้า" :
                                                    (og.Key.ReceiptType == "1" || og.Min(m => m.RVatAmount) != 0) ? "ค่าอื่นๆ (มีภาษี)" : "ค่าอื่นๆ (ไม่มีภาษี)"
                                    })
                            orderby rs.ReceiptId, rs.GAmount
                            select new
                            {
                                TerminalCode = rs.TerminalCode,
                                Quantity = rs.Quantity,
                                ReceiptId = rs.ReceiptId,
                                ExtReceiptId = rs.ExtReceiptId,
                                ElectricBaseAmount = rs.ElectricBaseAmount,
                                FtAmount = (rs.GAmount + rs.AdjAmount) - rs.ElectricBaseAmount - rs.VatAmount - rs.AdjAmount,
                                TotalVatAmount = rs.TotalVatAmount,
                                RPGAmount = rs.RPGAmount,
                                VatAmount = rs.VatAmount,
                                AdjAmount = rs.AdjAmount,
                                GAmount = rs.GAmount,
                                Type = rs.Type,
                                TotalAmount = rs.ElectricBaseAmount + ((rs.GAmount + rs.AdjAmount) - rs.ElectricBaseAmount - rs.VatAmount - rs.AdjAmount) + rs.TotalVatAmount
                            };

            foreach (var q in repResult)
            {
                CAC09Report detail = new CAC09Report();
                detail.PosId = q.TerminalCode;
                //detail.CaId = 
                detail.ReceiptId = q.ReceiptId;
                detail.ExtReceiptId = q.ExtReceiptId;
                detail.Quantity = q.Quantity;
                detail.ElectricBaseAmount = q.ElectricBaseAmount;
                //detail.OtherBaseAmount = 
                detail.FtAmount = q.FtAmount;
                detail.TotalVatAmount = q.TotalVatAmount;
                detail.TotalGAmount = q.TotalAmount;
                detail.RPGAmount = q.RPGAmount;
                detail.VatAmount = q.VatAmount;
                detail.AdjAmount = q.AdjAmount;
                detail.GAmount = q.GAmount;
                detail.Type = q.Type;
                report.Add(detail);
            }

            return report;
        }

        public List<CAC09Report> GetReportCAC09_2(string pBranchId, string pPosId, DateTime? pFromDate)
        {
            List<CAC09Report> report = new List<CAC09Report>();

            string[] exceptPmId = { "M", "N", "O" };
            string[] exceptDtId = { "M01000002", "M02000002", "M90000010", "M90000030", "M00100100" };
            string[] includedDtId = { "M01000002", "M02000002", "M90000010", "P00010001", "P00010002", "P00020001" };
            //201801311548 Kanokwan.L แก้ไขให้รายงานแสดงค่า M02000002 การวางบิลครั้งสุดท้าย ลูกหนี้
            //string[] ServiceDtId = { "M00172500", "M00172600", "M00172100", "M00178200", "M00172800", "M00172000", "M00178300", "M00173000" }; ค่าบริการพาดสายต้องเช็คที่ระดับ CA

            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC09_2X");
            db.AddInParameter(cmd, "BranchId", DbType.String, pBranchId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, pFromDate);
            cmd.CommandTimeout = 360;
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable mainRaw = ds.Tables[0];

            var repData = from rep in mainRaw.AsEnumerable()
                          select new
                          {
                              TerminalCode = rep.Field<string>("TerminalCode"),
                              PosId = rep.Field<string>("PosId"),
                              CaId = rep.Field<string>("CaId"),
                              CReceiptId = rep.Field<string>("CReceiptId"),
                              ReceiptId = rep.Field<string>("ReceiptId"),
                              ExtReceiptId = rep.Field<string>("ExtReceiptId"),
                              ReceiptType = rep.Field<string>("ReceiptType"),
                              DtId = rep.Field<string>("DtId"),
                              CaPayer = rep.Field<string>("CaPayer"),
                              //CheckGroupService = rep.Field<string>("CheckGroupService"),
                              OriginalInvoiceNo = rep.Field<string>("OriginalInvoiceNo"),
                              PaymentId = rep.Field<string>("PaymentId"),
                              GroupInvoiceNo = rep.Field<string>("GroupInvoiceNo"),
                              FTAmount = rep.Field<decimal?>("FTAmount"),
                              RVatAmount = rep.Field<decimal?>("RVatAmount"),
                              RGAmount = rep.Field<decimal?>("RGAmount"),
                              InvoiceNo = rep.Field<string>("InvoiceNo"),
                              ARPmId = rep.Field<string>("ARPmId"),
                              PmId = rep.Field<string>("PmId"),
                              PGAmount = rep.Field<decimal?>("PGAmount"),
                              VatAmount = rep.Field<decimal?>("VatAmount"),
                              AdjAmount = rep.Field<decimal?>("AdjAmount"),
                              GAmount = rep.Field<decimal?>("GAmount"),
                              CancelARPmId = rep.Field<string>("CancelARPmId"),
                              CCancelARPmId = rep.Field<string>("CCancelARPmId"),
                              ARPaymentDt = rep.Field<DateTime?>("ARPaymentDt"),
                              CARPaymentDt = rep.Field<DateTime?>("CARPaymentDt"),
                              Active = rep.Field<string>("Active")
                          };


            if (repData.Count() == 0) return report;

            DataTable subRaw = ds.Tables[1];

            var subData = from sub in subRaw.AsEnumerable()
                          select new
                          {
                              OriginalPaymentId = sub.Field<string>("OriginalPaymentId"),
                              GroupInvoiceNo = sub.Field<string>("GroupInvoiceNo"),
                              PaidGAmount = sub.Field<decimal?>("PaidGAmount"),
                              FTAmount = sub.Field<decimal?>("FTAmount"),
                              GAmount = sub.Field<decimal?>("GAmount"),
                              DtId = sub.Field<string>("DtId"),
                              ArActive = sub.Field<string>("ArActive"),
                              PaymentId = sub.Field<string>("PaymentId"),
                              PmId = sub.Field<string>("PmId"),
                              Pending = sub.Field<string>("Pending"),
                              Active = sub.Field<string>("Active")
                          };

            var subQuery = from s in subData
                           where /*s.PmId == "G" && */ s.Pending == "0" && s.ArActive == "1" && s.Active == "1" && s.DtId != "P00020001" && !s.PaymentId.StartsWith("ZZ")
                           group s by new { s.OriginalPaymentId, s.GroupInvoiceNo } into sg
                           select new
                           {
                               sg.Key.OriginalPaymentId,
                               sg.Key.GroupInvoiceNo,
                               FTAmount = Math.Round((double)((sg.Sum(s => s.PaidGAmount) * sg.Sum(s => s.FTAmount)) / sg.Sum(s => s.GAmount)), 2)
                           };

            var repResult = from rs in
                                (
                                    from m in
                                        (
                                            from o in repData
                                            join i in subQuery on new { PaymentId = o.PaymentId, GroupInvoiceNo = o.GroupInvoiceNo } equals new { PaymentId = i.OriginalPaymentId, GroupInvoiceNo = i.GroupInvoiceNo } into iso
                                            where ((o.CancelARPmId == null && o.CCancelARPmId == null) || (o.CancelARPmId == null && o.CCancelARPmId != null && SqlMethods.DateDiffDay(o.ARPaymentDt, o.CARPaymentDt) > 0))
                                                            && o.Active == "1"
                                                            && !exceptPmId.Contains(o.PmId)
                                                            && o.PosId == (pPosId == null ? o.PosId : pPosId)
                                            from g in iso.DefaultIfEmpty()   // left join 
                                            select new
                                            {
                                                o.TerminalCode,
                                                o.CaId,
                                                o.CReceiptId,
                                                o.ReceiptId,
                                                o.ExtReceiptId,
                                                o.DtId,
                                                o.CaPayer,
                                                //o.CheckGroupService,
                                                IFTAmount = g == null ? 0 : g.FTAmount,
                                                o.GAmount,
                                                o.FTAmount,
                                                o.VatAmount,
                                                o.RGAmount,
                                                o.RVatAmount,
                                                o.PGAmount,
                                                o.AdjAmount,
                                                o.ReceiptType,
                                                o.OriginalInvoiceNo,
                                                o.ARPmId,
                                                o.InvoiceNo,
                                                o.CARPaymentDt,
                                                o.ARPaymentDt
                                            }
                                            )
                                    group m by new { m.ExtReceiptId, m.ReceiptId, m.ReceiptType, m.TerminalCode, m.OriginalInvoiceNo, m.ARPmId, m.InvoiceNo, m.CARPaymentDt, m.ARPaymentDt } into og
                                    select new
                                    {
                                        og.Key.TerminalCode,
                                        CaId = og.Min(m => m.CaId),
                                        ReceiptId = og.Min(m => m.CReceiptId),
                                        Quantity = og.Select(m => m.ReceiptId).Distinct().Count(),
                                        BaseAmountFlag = og.Min(m => m.CaPayer).Substring(0, 3) == "091" || og.Min(m => m.CaPayer).Substring(0, 3) == "092" ? 0 : // กรณีใบเสร็จแยกค่าบริการพาดสายจะแสดงค่าอื่นๆ 
                                                            og.Min(m => m.CaPayer).Substring(0, 6) == "000004" ? 0 : //กรณีใบเสร็จรวมค่าบริการพาดสายจะแสดงค่าอื่นๆ 
                                                            includedDtId.Contains(og.Min(m => m.DtId)) ? 1 : 0,
                                        BaseAmount = og.Min(m => m.DtId) == "P00020001" ? (decimal)og.Min(m => m.GAmount) - (decimal)og.Min(m => m.IFTAmount) - (decimal)og.Min(m => m.VatAmount) :
                                                                (decimal)og.Min(m => m.GAmount) - Math.Round((decimal)((og.Min(m => m.GAmount) * (og.Sum(m => m.FTAmount) / og.Count())) / (og.Sum(m => m.RGAmount) / og.Count())), 2) - Math.Round((decimal)((og.Min(m => m.GAmount) * (og.Sum(m => m.VatAmount) / og.Count())) / (og.Sum(m => m.RGAmount) / og.Count())), 2),
                                        FtAmount = og.Min(m => m.DtId) == "P00020001" ? (decimal)og.Min(m => m.IFTAmount) :
                                                                Math.Round((decimal)((og.Min(m => m.GAmount) * (og.Sum(m => m.FTAmount) / og.Count())) / (og.Sum(m => m.RGAmount) / og.Count())), 2),
                                        TotalVatAmount = og.Min(m => m.VatAmount),
                                        RPGAmount = og.Sum(m => m.PGAmount),
                                        VatAmount = og.Min(m => m.VatAmount),
                                        AdjAmount = og.Min(m => m.AdjAmount),
                                        GAmount = og.Min(m => m.GAmount),
                                        Type = (og.Key.ReceiptType == "1" || og.Sum(m => m.RVatAmount) != 0) ? "1" : "2"
                                    })
                            orderby rs.ReceiptId, rs.CaId, rs.GAmount
                            select new
                            {
                                TerminalCode = rs.TerminalCode,
                                CaId = rs.CaId,
                                ReceiptId = rs.ReceiptId,
                                Quantity = rs.Quantity,
                                BaseAmountFlag = rs.BaseAmountFlag,
                                BaseAmount = rs.BaseAmount,
                                FtAmount = (rs.GAmount + rs.AdjAmount) - rs.BaseAmount - rs.VatAmount - rs.AdjAmount,
                                TotalVatAmount = rs.TotalVatAmount,
                                RPGAmount = rs.RPGAmount,
                                VatAmount = rs.VatAmount,
                                AdjAmount = rs.AdjAmount,
                                GAmount = rs.GAmount,
                                Type = rs.Type,
                                TotalAmount = rs.BaseAmount + ((rs.GAmount + rs.AdjAmount) - rs.BaseAmount - rs.VatAmount - rs.AdjAmount) + rs.TotalVatAmount,
                                ElectricBaseAmount = rs.BaseAmountFlag == 1 ? rs.BaseAmount : 0,
                                OtherBaseAmount = rs.BaseAmountFlag == 0 ? rs.BaseAmount : 0
                            };

            foreach (var q in repResult)
            {
                CAC09Report detail = new CAC09Report();
                detail.PosId = q.TerminalCode;
                detail.CaId = q.CaId;
                detail.ReceiptId = q.ReceiptId;
                //detail.ExtReceiptId = 
                detail.Quantity = q.Quantity;
                detail.ElectricBaseAmount = q.ElectricBaseAmount;
                detail.OtherBaseAmount = q.OtherBaseAmount;
                detail.FtAmount = q.FtAmount;
                detail.TotalVatAmount = q.TotalVatAmount;
                detail.TotalGAmount = q.TotalAmount;
                detail.RPGAmount = q.RPGAmount;
                detail.VatAmount = q.VatAmount;
                detail.AdjAmount = q.AdjAmount;
                detail.GAmount = q.GAmount;
                detail.Type = q.Type;
                report.Add(detail);
            }

            return report;
        }

        public List<CAC11Report> GetReportCAC11(string pBranchId, string pPosId, string pControllerId, DateTime? pFromDate, DateTime? pToDate)
        {
            List<CAC11Report> report = new List<CAC11Report>();

            //pControllerId = Convert.ToInt32(pControllerId).ToString();
            string[] exceptPmId = { "M", "N", "O" };

            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC11X");
            cmd.CommandTimeout = 360;
            db.AddInParameter(cmd, "BranchId", DbType.String, pBranchId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, pFromDate);
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, pToDate);
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable mainRaw = ds.Tables[0];

            var repData = from rep in mainRaw.AsEnumerable()
                          select new
                          {
                              RealReceiptId = rep.Field<string>("RealReceiptId"),
                              InvoiceNo = rep.Field<string>("InvoiceNo"),
                              PaymentDt = rep.Field<DateTime?>("PaymentDt"),
                              CancelDt = rep.Field<DateTime?>("CancelDt"),
                              ControllerId = rep.Field<string>("ControllerId"),
                              ReceiptId = rep.Field<string>("ReceiptId"),
                              CollectorName = rep.Field<string>("CollectorName"),
                              PosId = rep.Field<string>("PosId"),
                              RealPosId = rep.Field<string>("RealPosId"),
                              CashierId = rep.Field<string>("CashierId"),
                              BranchId = rep.Field<string>("BranchId"),
                              CaId = rep.Field<string>("CaId"),
                              CaName = rep.Field<string>("CaName"),
                              Period = rep.Field<string>("Period"),
                              DtName = rep.Field<string>("DtName"),
                              Amount = rep.Field<decimal?>("Amount"),
                              VatAmount = rep.Field<decimal?>("VatAmount"),
                              GAmount = rep.Field<decimal?>("GAmount"),
                              CancelReason = rep.Field<string>("CancelReason"),
                              CancelARPmId = rep.Field<string>("CancelARPmId"),
                              PmId = rep.Field<string>("PmId"),
                              ARPmId = rep.Field<string>("ARPmId"),
                              Active = rep.Field<string>("Active"),
                              PtName = rep.Field<string>("PtName")      // DCR QR Payment 2023-03 : Report 2.14
                          };

            if (repData.Count() == 0) return report;

            DataTable subRaw = ds.Tables[1];
            var subData = from sub in subRaw.AsEnumerable()
                          select new
                          {
                              ARPmId = sub.Field<string>("ARPmId"),
                              FullName = sub.Field<string>("FullName"),
                              TerminalCode = sub.Field<string>("TerminalCode"),
                              CancelARPmId = sub.Field<string>("CancelARPmId"),
                              Active = sub.Field<string>("Active")
                          };

            var subQuery = from s in subData
                           where s.Active == "1" && s.CancelARPmId != null
                           group s by new { s.ARPmId, s.FullName, s.TerminalCode } into sg
                           select new { sg.Key.ARPmId, sg.Key.FullName, sg.Key.TerminalCode };

            var repResult = from m in
                                (
                                  from o in repData
                                  join i in subQuery on o.ARPmId equals i.ARPmId into iso
                                  where o.CancelARPmId != null && !exceptPmId.Contains(o.PmId) && o.Active == "1"
                                          && o.CashierId == (pControllerId == null ? o.CashierId : pControllerId)
                                          && o.RealPosId == (pPosId == null ? o.RealPosId : pPosId)
                                  from t in iso.DefaultIfEmpty()   // left join 
                                  select new
                                  {
                                      o.RealReceiptId,
                                      o.InvoiceNo,
                                      o.PaymentDt,
                                      o.CancelDt,
                                      o.ControllerId,
                                      o.ReceiptId,
                                      o.CollectorName,
                                      o.PosId,
                                      IReturnCollectorName = t == null ? null : t.FullName,
                                      ITerminalCode = t == null ? null : t.TerminalCode,
                                      o.BranchId,
                                      o.CaId,
                                      o.CaName,
                                      o.Period,
                                      o.DtName,
                                      o.Amount,
                                      o.VatAmount,
                                      o.GAmount,
                                      CancelReason = o.CancelReason.Trim(),
                                      o.PtName
                                  }
                                    )
                            group m by new { m.InvoiceNo, m.PaymentDt, m.CancelDt, m.ControllerId, m.ReceiptId, m.CollectorName, m.PosId, m.ITerminalCode, m.IReturnCollectorName, m.BranchId, m.CaId, m.CaName, m.Period, m.DtName, m.CancelReason, m.RealReceiptId, m.PtName } into mg
                            orderby mg.Key.CollectorName, mg.Key.RealReceiptId
                            select new
                            {
                                RealReceiptId = mg.Key.RealReceiptId,
                                PaymentDt = mg.Key.PaymentDt,
                                CancelDt = mg.Key.CancelDt,
                                ControllerId = mg.Key.ControllerId,
                                ReceiptId = mg.Key.ReceiptId,
                                CollectorName = mg.Key.CollectorName,
                                PosId = mg.Key.PosId,
                                ReturnCollectorName = mg.Key.IReturnCollectorName,
                                ReturnPosId = mg.Key.ITerminalCode != null ? mg.Key.ITerminalCode.Substring(7, 5) : null,
                                BranchId = mg.Key.BranchId,
                                CaId = mg.Key.CaId,
                                CaName = mg.Key.CaName,
                                Period = mg.Key.Period,
                                DtName = mg.Key.DtName,
                                Amount = mg.Min(m => m.Amount),
                                VatAmount = mg.Min(m => m.VatAmount),
                                GAmount = mg.Min(m => m.GAmount),
                                CancelReason = mg.Key.CancelReason,
                                PtName = mg.Key.PtName
                            };

            foreach (var q in repResult)
            {
                CAC11Report detail = new CAC11Report();
                detail.BranchId = q.BranchId;
                detail.PaymentDt = q.PaymentDt;
                detail.CancelDt = q.CancelDt;
                detail.ControllerId = q.ControllerId;
                detail.ReceiptId = q.ReceiptId;
                detail.CollectorName = q.CollectorName;
                detail.PosId = q.PosId;
                detail.ReturnCollectorName = q.ReturnCollectorName;
                detail.ReturnPosId = q.ReturnPosId;
                detail.CaId = q.CaId;
                detail.CaName = q.CaName;
                detail.Period = q.Period;
                detail.DtName = q.DtName;
                detail.Amount = q.Amount;
                detail.VatAmount = q.VatAmount;
                detail.GAmount = q.GAmount;
                detail.CancelReason = q.CancelReason;
                detail.PtName = q.PtName;
                report.Add(detail);
            }

            return report;
        }

        public List<CAC12Report> GetReportCAC12(string pBranchId, DateTime? pFromDate)
        {
            List<CAC12Report> report = new List<CAC12Report>();

            string[] exceptPmId = { "M", "N", "O" };
            string[] includedDtId = { "M01000002", "M90000010", "P00010001", "P00010002", "P00020001", "M02000002" };
            //201801311548 Kanokwan.L แก้ไขให้รายงานแสดงค่า M02000002 การวางบิลครั้งสุดท้าย ลูกหนี้

            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC12X");
            cmd.CommandTimeout = 360;
            db.AddInParameter(cmd, "BranchId", DbType.String, pBranchId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, pFromDate);
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable mainRaw = ds.Tables[0];

            var repData = (from rep in mainRaw.AsEnumerable()
                           select new
                           {
                               BranchId = rep.Field<string>("BranchId"),
                               PaymentId = rep.Field<string>("PaymentId"),
                               GroupInvoiceNo = rep.Field<string>("GroupInvoiceNo"),
                               ReceiptId = rep.Field<string>("ReceiptId"),
                               RateTypeId = rep.Field<string>("RateTypeId"),
                               FTAmount = rep.Field<decimal?>("FTAmount"),
                               RVatAmount = rep.Field<decimal?>("RVatAmount"),
                               RGAmount = rep.Field<decimal?>("RGAmount"),
                               DtId = rep.Field<string>("DtId"),
                               InvoiceNo = rep.Field<string>("InvoiceNo"),
                               ARPmId = rep.Field<string>("ARPmId"),
                               PmId = rep.Field<string>("PmId"),
                               GAmount = rep.Field<decimal?>("GAmount"),
                               VatAmount = rep.Field<decimal?>("VatAmount"),
                               CancelARPmId = rep.Field<string>("CancelARPmId"),
                               CCancelARPmId = rep.Field<string>("CCancelARPmId"),
                               ARPaymentDt = rep.Field<DateTime?>("ARPaymentDt"),
                               CARPaymentDt = rep.Field<DateTime?>("CARPaymentDt"),
                               Active = rep.Field<string>("Active"),
                               Caid = rep.Field<string>("Caid")
                           });

            repData = (from p in repData
                       where p.Caid.Substring(0, 6) != "000004"
                       select p); //ไม่ให้รายงานแสดงค่าพาดสาย กรณีใบเสร็จรวม


            if (repData.Count() == 0) return report;

            DataTable subRaw = ds.Tables[1];

            var subData = (from sub in subRaw.AsEnumerable()
                           select new
                           {
                               OriginalPaymentId = sub.Field<string>("OriginalPaymentId"),
                               GroupInvoiceNo = sub.Field<string>("GroupInvoiceNo"),
                               PaidGAmount = sub.Field<decimal?>("PaidGAmount"),
                               FTAmount = sub.Field<decimal?>("FTAmount"),
                               GAmount = sub.Field<decimal?>("GAmount"),
                               DtId = sub.Field<string>("DtId"),
                               ArActive = sub.Field<string>("ArActive"),
                               PaymentId = sub.Field<string>("PaymentId"),
                               PmId = sub.Field<string>("PmId"),
                               Pending = sub.Field<string>("Pending"),
                               Active = sub.Field<string>("Active")
                           });

            var subQuery = from s in subData
                           where /*s.PmId == "G" && */ s.Pending == "0" && s.ArActive == "1" && s.Active == "1" && s.DtId != "P00020001" && !s.PaymentId.StartsWith("ZZ")
                           group s by new { s.OriginalPaymentId, s.GroupInvoiceNo } into sg
                           select new
                           {
                               sg.Key.OriginalPaymentId,
                               sg.Key.GroupInvoiceNo,
                               FTAmount = Math.Round((double)((sg.Sum(s => s.PaidGAmount) * sg.Sum(s => s.FTAmount)) / sg.Sum(s => s.GAmount)), 2)
                           };

            var queryResult = from rs in
                                  (
                                       from m in
                                           (
                                               from o in repData
                                               join i in subQuery on new { PaymentId = o.PaymentId, GroupInvoiceNo = o.GroupInvoiceNo } equals new { PaymentId = i.OriginalPaymentId, GroupInvoiceNo = i.GroupInvoiceNo } into iso
                                               where ((o.CancelARPmId == null && o.CCancelARPmId == null) || (o.CancelARPmId == null && o.CCancelARPmId != null && SqlMethods.DateDiffDay(o.ARPaymentDt, o.CARPaymentDt) > 0))
                                                               && o.Active == "1"
                                                               && includedDtId.Contains(o.DtId)
                                                               && !exceptPmId.Contains(o.PmId)
                                               from i in iso.DefaultIfEmpty()   // left join 
                                               select new { o.BranchId, RateTypeId = o.RateTypeId == null ? "" : o.RateTypeId, o.ARPmId, o.InvoiceNo, o.ReceiptId, o.DtId, o.GAmount, IFTAmount = i == null ? 0 : i.FTAmount, o.VatAmount, o.FTAmount, o.RGAmount, o.RVatAmount }
                                               )
                                       group m by new { m.BranchId, m.ARPmId, m.InvoiceNo, m.ReceiptId, m.RateTypeId } into og
                                       select new
                                       {
                                           og.Key.BranchId,
                                           RateTypeId = og.Key.RateTypeId.Trim(),
                                           Quantity = og.Select(m => m.ARPmId).Distinct().Count(),
                                           BaseAmount = og.Min(m => m.DtId) == "P00020001" ? (decimal)og.Min(m => m.GAmount) - (decimal)og.Min(m => m.IFTAmount) - (decimal)og.Min(m => m.VatAmount) :
                                                                   (decimal)og.Min(m => m.GAmount) - Math.Round((decimal)((og.Min(m => m.GAmount) * (og.Sum(m => m.FTAmount) / og.Count())) / (og.Sum(m => m.RGAmount) / og.Count())), 2) - Math.Round((decimal)((og.Min(m => m.GAmount) * (og.Sum(m => m.VatAmount) / og.Count())) / (og.Sum(m => m.RGAmount) / og.Count())), 2),
                                           FtAmount = og.Min(m => m.DtId) == "P00020001" ? (decimal)og.Min(m => m.IFTAmount) :
                                                                   Math.Round((decimal)((og.Min(m => m.GAmount) * (og.Sum(m => m.FTAmount) / og.Count())) / (og.Sum(m => m.RGAmount) / og.Count())), 2),
                                           VatAmount = og.Min(m => m.VatAmount),
                                           GAmount = og.Min(m => m.GAmount)
                                       })
                              group rs by new { rs.BranchId, rs.RateTypeId } into rg
                              orderby rg.Key.RateTypeId, rg.Key.BranchId
                              select new
                              {
                                  BranchId = rg.Key.BranchId.Trim(),
                                  RateTypeId = rg.Key.RateTypeId,
                                  Quantity = rg.Sum(rs => rs.Quantity),
                                  BaseAmount = rg.Sum(rs => rs.BaseAmount),
                                  FtAmount = rg.Sum(rs => rs.GAmount) - rg.Sum(rs => rs.BaseAmount) - rg.Sum(rs => rs.VatAmount),
                                  VatAmount = rg.Sum(rs => rs.VatAmount),
                                  GAmount = rg.Sum(rs => rs.GAmount)
                              };


            foreach (var q in queryResult)
            {
                CAC12Report detail = new CAC12Report();
                detail.BranchId = q.BranchId;
                detail.RateTypeId = q.RateTypeId;
                detail.Quantity = q.Quantity;
                detail.BaseAmount = q.BaseAmount;
                detail.FtAmount = q.FtAmount;
                detail.VatAmount = q.VatAmount;
                detail.GAmount = q.GAmount;
                report.Add(detail);
            }

            return report;
        }

        public List<CAC13Report> GetReportCAC13(string pBranchId, string posId, string pControllerId, DateTime? pFromDate, DateTime? pToDate)
        {
            List<CAC13Report> report = new List<CAC13Report>();
            string[] exceptPmId = { "M", "N", "O" };
            string[] includedDtId = { "M01000002", "M90000010", "P00010001", "P00010002", "P00020001", "M02000002" };
            //201801311548 Kanokwan.L แก้ไขให้รายงานแสดงค่า M02000002 การวางบิลครั้งสุดท้าย ลูกหนี้
            //string[] ServiceDtId = { "M00172500", "M00172600", "M00172100", "M00178200", "M00172800", "M00172000", "M00178300", "M00173000" };

            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC13X");
            cmd.CommandTimeout = 360;
            db.AddInParameter(cmd, "BranchId", DbType.String, pBranchId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, pFromDate);
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, pToDate);
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable mainRaw = ds.Tables[0];

            var repData = from rep in mainRaw.AsEnumerable()
                          select new
                          {
                              PaymentDate = rep.Field<string>("PaymentDate"),
                              TerminalCode = rep.Field<string>("TerminalCode"),
                              PosId = rep.Field<string>("PosId"),
                              CashierId = rep.Field<string>("CashierId"),
                              BranchId = rep.Field<string>("BranchId"),
                              InvoiceNo = rep.Field<string>("InvoiceNo"),
                              ARPmId = rep.Field<string>("ARPmId"),
                              PmId = rep.Field<string>("PmId"),
                              ReceiptId = rep.Field<string>("ReceiptId"),
                              GAmount = rep.Field<decimal?>("GAmount"),
                              VatAmount = rep.Field<decimal?>("VatAmount"),
                              RGAmount = rep.Field<decimal?>("RGAmount"),
                              RVatAmount = rep.Field<decimal?>("RVatAmount"),
                              FTAmount = rep.Field<decimal?>("FTAmount"),
                              DtId = rep.Field<string>("DtId"),
                              CaPayer = rep.Field<string>("CaPayer"),
                              //CheckGroupService = rep.Field<string>("CheckGroupService"),
                              PaymentId = rep.Field<string>("PaymentId"),
                              GroupInvoiceNo = rep.Field<string>("GroupInvoiceNo"),
                              ControllerId = rep.Field<string>("ControllerId"),
                              CancelARPmId = rep.Field<string>("CancelARPmId"),
                              CCancelARPmId = rep.Field<string>("CCancelARPmId"),
                              ARPaymentDt = rep.Field<DateTime?>("ARPaymentDt"),
                              CARPaymentDt = rep.Field<DateTime?>("CARPaymentDt"),
                              Active = rep.Field<string>("Active")
                          };

            if (repData.Count() == 0) return report;

            DataTable subRaw = ds.Tables[1];

            var subData = from sub in subRaw.AsEnumerable()
                          select new
                          {
                              OriginalPaymentId = sub.Field<string>("OriginalPaymentId"),
                              GroupInvoiceNo = sub.Field<string>("GroupInvoiceNo"),
                              PaidGAmount = sub.Field<decimal?>("PaidGAmount"),
                              FTAmount = sub.Field<decimal?>("FTAmount"),
                              GAmount = sub.Field<decimal?>("GAmount"),
                              DtId = sub.Field<string>("DtId"),
                              ArActive = sub.Field<string>("ArActive"),
                              PaymentId = sub.Field<string>("PaymentId"),
                              PmId = sub.Field<string>("PmId"),
                              Pending = sub.Field<string>("Pending"),
                              Active = sub.Field<string>("Active")
                          };

            var subQuery = from s in subData
                           where /*s.PmId == "G" && */ s.Pending == "0" && s.ArActive == "1" && s.Active == "1" && s.DtId != "P00020001" && !s.PaymentId.StartsWith("ZZ")
                           group s by new { s.OriginalPaymentId, s.GroupInvoiceNo } into sg
                           select new
                           {
                               sg.Key.OriginalPaymentId,
                               sg.Key.GroupInvoiceNo,
                               FTAmount = Math.Round((decimal)((sg.Sum(s => s.PaidGAmount) / sg.Sum(s => s.GAmount)) * sg.Sum(s => s.FTAmount)), 2)
                           };

            var repResult = from rs in
                                (
                                    from m in
                                        (
                                            from o in repData
                                            join i in subQuery on new { PaymentId = o.PaymentId, GroupInvoiceNo = o.GroupInvoiceNo } equals new { PaymentId = i.OriginalPaymentId, GroupInvoiceNo = i.GroupInvoiceNo } into iso
                                            where ((o.CancelARPmId == null && o.CCancelARPmId == null) || (o.CancelARPmId == null && o.CCancelARPmId != null && SqlMethods.DateDiffDay(o.ARPaymentDt, o.CARPaymentDt) > 0))
                                                            && o.Active == "1"
                                                            && !exceptPmId.Contains(o.PmId)
                                                            && o.CashierId == (pControllerId == null ? o.CashierId : pControllerId)
                                                            && o.PosId == (posId == null ? o.PosId : posId)
                                            from i in iso.DefaultIfEmpty()   // left join 
                                            select new
                                            {
                                                o.PaymentDate,
                                                o.TerminalCode,
                                                o.ARPmId,
                                                o.InvoiceNo,
                                                o.ReceiptId,
                                                o.DtId,
                                                o.CaPayer,//o.CheckGroupService,
                                                o.GAmount,
                                                IFTAmount = i == null ? 0 : i.FTAmount,
                                                o.VatAmount,
                                                o.FTAmount,
                                                o.RGAmount,
                                                o.RVatAmount,
                                                o.ControllerId,
                                                o.BranchId
                                            }
                                            )
                                    group m by new { m.BranchId, m.ARPmId, m.InvoiceNo, m.ReceiptId } into og
                                    select new
                                    {
                                        PaymentDt = og.Min(m => m.PaymentDate),
                                        PosId = og.Min(m => m.TerminalCode),
                                        Quantity = og.Select(m => m.ARPmId).Distinct().Count(),
                                        ReceiptId = og.Min(m => m.ReceiptId),
                                        BaseAmount = og.Sum(m => m.RGAmount) == 0 ? og.Min(m => m.GAmount) - og.Min(m => m.VatAmount) :
                                                    og.Min(m => m.DtId) == "P00020001" ? (decimal)og.Min(m => m.GAmount) - (decimal)og.Min(m => m.IFTAmount) - (decimal)og.Min(m => m.VatAmount) :
                                                    (decimal)og.Min(m => m.GAmount) - Math.Round((decimal)((og.Min(m => m.GAmount) * (og.Sum(m => m.FTAmount) / og.Count())) / (og.Sum(m => m.RGAmount) / og.Count())), 2) - Math.Round((decimal)((og.Min(m => m.GAmount) * (og.Sum(m => m.VatAmount) / og.Count())) / (og.Sum(m => m.RGAmount) / og.Count())), 2),
                                        FtAmount = og.Sum(m => m.RGAmount) == 0 ? 0 :
                                                    og.Min(m => m.DtId) == "P00020001" ? (decimal)og.Min(m => m.IFTAmount) :
                                                    Math.Round((decimal)((og.Min(m => m.GAmount) * (og.Sum(m => m.FTAmount) / og.Count())) / (og.Sum(m => m.RGAmount) / og.Count())), 2),
                                        VatAmount = og.Sum(m => m.RGAmount) == 0 ? og.Min(m => m.VatAmount) : og.Min(m => m.VatAmount),
                                        GAmount = og.Min(m => m.GAmount),
                                        DtName = og.Min(m => m.CaPayer).Substring(0, 6) == "000004" ? "2" : //กรณีใบเสร็จรวมค่าบริการพาดสายต้องแสดงที่ค่าอื่นๆ 
                                                    og.Min(m => m.CaPayer).Substring(0, 3) == "091" || og.Min(m => m.CaPayer).Substring(0, 3) == "092" ? "2" : //กรณีใบเสร็จแยกค่าบริการพาดสายต้องแสดงที่ค่าอื่นๆ 
                                                    includedDtId.Contains(og.Min(m => m.DtId)) ? "1" : "2",
                                        ControllerId = og.Min(m => m.ControllerId)
                                    })
                            group rs by new { rs.PaymentDt, rs.PosId, rs.DtName, rs.ControllerId } into rg
                            orderby rg.Key.PaymentDt, rg.Key.PosId, rg.Key.DtName, rg.Key.ControllerId
                            select new
                            {
                                PaymentDt = rg.Key.PaymentDt,
                                PosId = rg.Key.PosId,
                                Quantity = rg.Sum(rs => rs.Quantity),
                                BaseAmount = rg.Sum(rs => rs.BaseAmount),
                                FtAmount = rg.Sum(rs => rs.GAmount) - rg.Sum(rs => rs.BaseAmount) - rg.Sum(rs => rs.VatAmount),
                                VatAmount = rg.Sum(rs => rs.VatAmount),
                                GAmount = rg.Sum(rs => rs.GAmount),
                                DtName = rg.Key.DtName,
                                ControllerId = rg.Key.ControllerId
                            };

            foreach (var q in repResult)
            {
                CAC13Report detail = new CAC13Report();
                //detail.BranchId = 
                //detail.BranchName =
                detail.PaymentDt = q.PaymentDt;
                detail.PosId = q.PosId;
                //detail.ReceiptId = 
                detail.DtName = q.DtName;
                detail.ControllerId = q.ControllerId;
                detail.Quantity = q.Quantity;
                detail.BaseAmount = q.BaseAmount;
                detail.FtAmount = q.FtAmount;
                detail.VatAmount = q.VatAmount;
                detail.GAmount = q.GAmount;
                report.Add(detail);
            }

            return report;
        }

        public List<CAC14Report> GetReportCAC14(CAC14Param param)
        {
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC14");
            cmd.CommandTimeout = 360;
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "GroupInvoiceNo", DbType.String, param.GroupInvoiceNo);
            db.AddInParameter(cmd, "PaymentId", DbType.String, param.PaymentId);
            db.AddInParameter(cmd, "ReceiptId", DbType.String, param.ReceiptId);
            DataSet ds = db.ExecuteDataSet(cmd);

            string CAC14HeaderText = pc_get_GroupInvoiceHeaderForReportCAC14(param.GroupInvoiceNo, param.PaymentId, param.ReceiptId);
       

            List<CAC14Report> report = new List<CAC14Report>();

            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                CAC14Report detail = new CAC14Report();
                detail.CaId = DaHelper.GetString(dr, "CaId");
                detail.NameAddress = DaHelper.GetString(dr, "NameAddress");
                detail.Period = DaHelper.GetString(dr, "Period");
                detail.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                detail.PaymentDoc = DaHelper.GetString(dr, "PaymentDoc");
                detail.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                detail.Qty = DaHelper.GetDecimal(dr, "Qty");
                detail.Unit = DaHelper.GetDecimal(dr, "Unit");
                detail.AmountExtVat = DaHelper.GetDecimal(dr, "AmountExtVat");
                detail.Vat = DaHelper.GetDecimal(dr, "Vat");
                detail.GAmount = DaHelper.GetDecimal(dr, "GAmount");
                detail.BranchId = DaHelper.GetString(dr, "BranchId");
                detail.BranchName = DaHelper.GetString(dr, "BranchName");
                detail.CashierId = DaHelper.GetString(dr, "CashierId");
                detail.CA_BranchId = DaHelper.GetString(dr, "CA_BranchId");
                detail.CA_BranchName = DaHelper.GetString(dr, "CA_BranchName");
                detail.CaTaxId = DaHelper.GetString(dr, "CaTaxId");
                detail.CaTaxBranch = DaHelper.GetString(dr, "CaTaxBranch");
                detail.DtId = DaHelper.GetString(dr, "DtId");
                detail.DtName = DaHelper.GetString(dr, "DtName");
                detail.SubGroupInvoiceNo = DaHelper.GetString(dr, "SubGroupInvoiceNo");
                detail.RowNumber = DaHelper.GetInt(dr, "RowNumber");

                
                //DCR GroupInvoiceText 2021-OCT-25 Uthen.P
                detail.GroupInvoiceHeaderText = CAC14HeaderText;

                //savake
                report.Add(detail);
            }

            return report;
        }

        public string pc_get_GroupInvoiceHeaderForReportCAC14(string groupInvoiceNo, string paymentId, string receiptId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_get_GroupInvoiceHeaderForReportCAC14");
            //cmd.CommandTimeout = timeout;
            cmd.CommandTimeout = 600;
            db.AddInParameter(cmd, "GroupInvoiceNo", DbType.String, groupInvoiceNo);
            db.AddInParameter(cmd, "PaymentId", DbType.String, paymentId);
            db.AddInParameter(cmd, "ReceiptId", DbType.String, receiptId);
            DataSet ds = db.ExecuteDataSet(cmd);

            string GroupInvoiceHeaderText = "รายละเอียดใบแนบใบเสร็จรับเงินเลขที่  ";

            try
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    GroupInvoiceHeaderText = DaHelper.GetString(dr, "CAC14HeaderText");
                }
            }
            catch (Exception ex)
            {

            }

            return GroupInvoiceHeaderText;
        }

        public List<CAC18Report> GetReportCAC18(string pBranchId, string pControllerId, DateTime? pFromDate,DateTime? pToDate)
        {
            List<CAC18Report> report = new List<CAC18Report>();

            string[] exceptDebt = { "P00010001", "P00010002", "P00020001" };
            string[] exceptPmId = { "G", "M", "N", "O" };
            string[] billBookDebt = { "P00010001", "P00010002" };

            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC18");
            cmd.CommandTimeout = 360;
            db.AddInParameter(cmd, "BranchId", DbType.String, pBranchId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, pFromDate);
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, pToDate);

            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable repDs = ds.Tables[0];

            var repData = from rep in repDs.AsEnumerable()
                          select new
                          {
                              PaymentId = rep.Field<string>("PaymentId"),
                              CashierId = rep.Field<string>("CashierId"),
                              ControllerName = rep.Field<string>("ControllerName"),
                              PaymentDt = rep.Field<DateTime?>("PaymentDt"),
                              CaDoc = rep.Field<string>("CaDoc"),
                              InvoiceNo = rep.Field<string>("InvoiceNo"),
                              MruId = rep.Field<string>("MruId"),
                              BillBookId = rep.Field<string>("BillBookId"),
                              GroupInvoiceNo = rep.Field<string>("GroupInvoiceNo"),
                              CaId = rep.Field<string>("CaId"),
                              CCaId = rep.Field<string>("CCaId"),
                              DtId = rep.Field<string>("DtId"),
                              BranchId = rep.Field<string>("BranchId"),
                              Period = rep.Field<string>("Period"),
                              DtName = rep.Field<string>("DtName"),
                              ExtReceiptId = rep.Field<string>("ExtReceiptId"),
                              ExtReceiptDt = rep.Field<DateTime?>("ExtReceiptDt"),
                              ReceiptId = rep.Field<string>("ReceiptId"),
                              CReceiptId = rep.Field<string>("CReceiptId"),
                              PmId = rep.Field<string>("PmId"),
                              ARPaymentDt = rep.Field<DateTime?>("ARPaymentDt"),
                              GAmount = rep.Field<decimal?>("GAmount"),
                              AdjAmount = rep.Field<decimal?>("AdjAmount"),
                              ARPmId = rep.Field<string>("ARPmId"),
                              CancelARPmId = rep.Field<string>("CancelARPmId"),
                              CPaymentDt = rep.Field<DateTime?>("CPaymentDt"),
                              CCancelARPmId = rep.Field<string>("CCancelARPmId"),
                              PtId = rep.Field<string>("PtId"),
                              FeeAmount = rep.Field<decimal?>("FeeAmount"),
                              RiARPmId = rep.Field<string>("RiARPmId"),
                              Amount = rep.Field<decimal?>("Amount"),
                              CashAmount = rep.Field<decimal?>("CashAmount"),
                              ChequeAmount = rep.Field<decimal?>("ChequeAmount"),
                              TransferAmount = rep.Field<decimal?>("TransferAmount"),
                              //CreditDebitAmount = rep.Field<decimal?>("CreditDebitAmount"),
                              QRPaymentAmount = rep.Field<decimal?>("QRPaymentAmount"),
                              ARGroupInvoiceNo = rep.Field<string>("ARGroupInvoiceNo"),
                              Active = rep.Field<string>("Active"),
                              CaId_Qty_New = rep.Field<int?>("caid_qty"),
                              QRRef1 = rep.Field<string>("QRRef1"),
                              QRRef2 = rep.Field<string>("QRRef2"),
                              QROnlineFlag = rep.Field<string>("QROnlineFlag"),
                              QRPaymentRefNo = rep.Field<string>("QRPaymentRefNo")
                          };

            var elecSec = from pa in
                              (
                                     from p in repData
                                     where !exceptDebt.Contains(p.DtId) && !exceptPmId.Contains(p.PmId) && ((p.CancelARPmId == null && p.CCancelARPmId == null) || (p.CancelARPmId == null && p.CCancelARPmId != null && SqlMethods.DateDiffDay(p.ARPaymentDt, p.CPaymentDt) > 0)) && p.Active == "1"
                                            && p.CashierId == (pControllerId == null ? p.CashierId : pControllerId)
                                     select new
                                     {
                                         p.PaymentId,
                                         p.ControllerName,
                                         p.MruId,
                                         CaId = p.CCaId,
                                         BranchId = p.DtId.Contains("MZ") ? null : p.BranchId,
                                         Period = (p.Period == null ? null : p.Period.Trim()),
                                         p.DtName,
                                         ReceiptId = p.CReceiptId,
                                         p.GAmount,
                                         p.AdjAmount,
                                         TotalAmount = p.GAmount + p.AdjAmount,
                                         PaymentDt = p.ExtReceiptDt == null ? p.PaymentDt : p.ExtReceiptDt,
                                         p.CashAmount,
                                         p.ChequeAmount,
                                         p.TransferAmount,
                                         //p.CreditDebitAmount,
                                         p.QRPaymentAmount,
                                         CaId_Qty = 0,
                                         p.CaId_Qty_New,
                                         FeeAmount = (decimal?)0,
                                         p.CaDoc,
                                         p.InvoiceNo,
                                         RealReceiptId = p.ReceiptId,
                                         QRRef1 = p.QRRef1,
                                         QRRef2 = p.QRRef2,
                                         p.QRPaymentRefNo,
                                         p.QROnlineFlag
                                     })
                          group pa by new { pa.PaymentId, pa.ControllerName, pa.MruId, pa.CaId, pa.BranchId, pa.Period, pa.DtName, pa.ReceiptId, pa.GAmount, pa.AdjAmount, pa.TotalAmount, pa.PaymentDt, 
                              pa.FeeAmount, pa.CaDoc, pa.InvoiceNo, pa.RealReceiptId, pa.QRPaymentAmount, pa.QRRef1, pa.QRRef2,pa.QRPaymentRefNo,pa.QROnlineFlag } into bg
                          select new
                          {
                              PaymentId = (string)bg.Key.PaymentId,
                              ControllerName = (string)bg.Key.ControllerName,
                              MruId = (string)bg.Key.MruId,
                              CaId = (string)bg.Key.CaId,
                              BranchId = (string)bg.Key.BranchId,
                              Quantity = (int?)1,
                              Period = (string)bg.Key.Period,
                              DtName = (string)bg.Key.DtName,
                              ReceiptId = (string)bg.Key.ReceiptId,
                              GAmount = (decimal?)bg.Key.GAmount,
                              AdjAmount = (decimal?)bg.Key.AdjAmount,
                              TotalAmount = (decimal?)bg.Key.TotalAmount,
                              PaymentDt = (DateTime?)bg.Key.PaymentDt,
                              CashAmount = (decimal?)(bg.Sum(pa => pa.CashAmount) == 0 ? null : bg.Sum(pa => pa.CashAmount)),
                              ChequeAmount = (decimal?)(bg.Sum(pa => pa.ChequeAmount) == 0 ? null : bg.Sum(pa => pa.ChequeAmount)),
                              TransferAmount = (decimal?)(bg.Sum(pa => pa.TransferAmount) == 0 ? null : bg.Sum(pa => pa.TransferAmount)),
                              //CreditDebitAmount = (decimal?)(bg.Sum(pa => pa.CreditDebitAmount) == 0 ? null : bg.Sum(pa => pa.CreditDebitAmount)),
                              CaId_Qty = (int?)(bg.Sum(pa => pa.CaId_Qty) == 0 ? 0 : bg.Sum(pa => pa.CaId_Qty)),
                              CaId_Qty_New = (int?)(bg.Sum(pa => pa.CaId_Qty_New) == 0 ? 0 : bg.Sum(pa => pa.CaId_Qty_New)),
                              FeeAmount = (decimal?)(bg.Key.FeeAmount == 0 ? null : bg.Key.FeeAmount),
                              RealReceiptId = (string)bg.Key.RealReceiptId,
                              QRPaymentAmount = (decimal?)(bg.Sum(pa => pa.QRPaymentAmount) == 0 ? null : bg.Sum(pa => pa.QRPaymentAmount)),
                              InvoiceNo = (string)bg.Key.InvoiceNo,
                              QRRef1 = (string)bg.Key.QRRef1,
                              QRRef2 = (string)bg.Key.QRRef2,
                              QRPaymentRefNo = (string)bg.Key.QRPaymentRefNo,
                              QROnlineFlag = (string)bg.Key.QROnlineFlag
                          };

            var billbook = from bb in
                               (
                                     from p in repData // DF#172 Kanokwan.L แก้ไขรายงาน 2.8.2 ให้แสดงรายการยกเลิก Billbook จากทาง SAP 
                                     where (billBookDebt.Contains(p.DtId) && ((p.CancelARPmId == null && p.CCancelARPmId == null) || (p.CancelARPmId == null && p.CCancelARPmId != null && SqlMethods.DateDiffDay(p.ARPaymentDt, p.CPaymentDt) > 0))) && p.Active == "1"
                                         //where (billBookDebt.Contains(p.DtId) && (p.CancelARPmId == null && p.CCancelARPmId == null)) && p.Active == "1"
                                            && p.CashierId == (pControllerId == null ? p.CashierId : pControllerId)
                                     select new
                                     {
                                         p.PaymentId,
                                         p.ControllerName,
                                         MruId = p.BillBookId.Substring(6, 9),
                                         p.CaId,
                                         p.BranchId,
                                         Period = (p.Period == null ? null : p.Period.Trim()),
                                         p.DtName,
                                         p.ReceiptId,
                                         p.GAmount,
                                         p.AdjAmount,
                                         TotalAmount = p.GAmount + p.AdjAmount,
                                         p.PaymentDt,
                                         p.Amount,
                                         CaId_Qty = 1,
                                         p.CaId_Qty_New,
                                         p.CashAmount,
                                         p.ChequeAmount,
                                         p.TransferAmount,
                                         //p.CreditDebitAmount,
                                         //p.QRPaymentAmount,
                                         RealReceiptId = p.ReceiptId,
                                         p.FeeAmount,
                                         QRPaymentAmount = p.QRPaymentAmount,
                                         QRRef1 = p.QRRef1,
                                         QRRef2 = p.QRRef2,
                                         InvoiceNo = p.InvoiceNo,
                                         p.QRPaymentRefNo,
                                         p.QROnlineFlag
                                     })
                           group bb by new { bb.PaymentId, bb.ControllerName, bb.MruId, bb.CaId, bb.InvoiceNo, bb.BranchId, bb.Period, bb.DtName, bb.ReceiptId, bb.GAmount, bb.AdjAmount, 
                               bb.TotalAmount, bb.PaymentDt, bb.RealReceiptId, bb.FeeAmount, bb.QRPaymentAmount, bb.QRRef1, bb.QRRef2, bb.QRPaymentRefNo, bb.QROnlineFlag } into bg
                           select new
                           {
                               PaymentId = (string)bg.Key.PaymentId,
                               ControllerName = (string)bg.Key.ControllerName,
                               MruId = (string)bg.Key.MruId,
                               CaId = (string)bg.Key.CaId,
                               BranchId = (string)bg.Key.BranchId,
                               Quantity = (int?)1,
                               Period = (string)bg.Key.Period,
                               DtName = (string)bg.Key.DtName,
                               ReceiptId = (string)bg.Key.ReceiptId,
                               GAmount = (decimal?)bg.Key.GAmount,
                               AdjAmount = (decimal?)bg.Key.AdjAmount,
                               TotalAmount = (decimal?)bg.Key.TotalAmount,
                               PaymentDt = (DateTime?)bg.Key.PaymentDt,
                               CashAmount = (decimal?)(bg.Sum(pa => pa.CashAmount) == 0 ? null : bg.Sum(pa => pa.CashAmount)),
                               ChequeAmount = (decimal?)(bg.Sum(pa => pa.ChequeAmount) == 0 ? null : bg.Sum(pa => pa.ChequeAmount)),
                               TransferAmount = (decimal?)(bg.Sum(pa => pa.TransferAmount) == 0 ? null : bg.Sum(pa => pa.TransferAmount)),
                               //CreditDebitAmount = (decimal?)(bg.Sum(pa => pa.CreditDebitAmount) == 0 ? null : bg.Sum(pa => pa.CreditDebitAmount)),
                               CaId_Qty = (int?)(bg.Sum(pa => pa.CaId_Qty) == 0 ? 0 : bg.Sum(pa => pa.CaId_Qty)),
                               CaId_Qty_New = (int?)(bg.Sum(pa => pa.CaId_Qty_New) == 0 ? 0 : bg.Sum(pa => pa.CaId_Qty_New)),
                               FeeAmount = (decimal?)(bg.Key.FeeAmount == 0 ? null : bg.Key.FeeAmount),
                               RealReceiptId = (string)bg.Key.RealReceiptId,
                               QRPaymentAmount = (decimal?)(bg.Sum(pa => pa.QRPaymentAmount) == 0 ? null : bg.Sum(pa => pa.QRPaymentAmount)),
                               InvoiceNo = (string)bg.Key.InvoiceNo,
                               QRRef1 = (string)bg.Key.QRRef1,
                               QRRef2 = (string)bg.Key.QRRef2,
                               QRPaymentRefNo = (string)bg.Key.QRPaymentRefNo,
                               QROnlineFlag = (string)bg.Key.QROnlineFlag
                           };


            var groupinv = from pi in
                               (
                                    from p in repData
                                    // DF#172 Kanokwan.L แก้ไขรายงาน 2.8.2 ให้แสดงรายการยกเลิก Group จากทาง SAP 
                                    where  // /*p.PmId == "G" && */p.Active == "1" && p.GroupInvoiceNo != null && p.BillBookId != null && ((p.CancelARPmId == null && p.CCancelARPmId == null) && (p.CancelARPmId == null || p.CCancelARPmId != null && SqlMethods.DateDiffDay(p.ARPaymentDt, p.CPaymentDt) > 0))
                                        /*p.PmId == "G" && */p.Active == "1" && p.GroupInvoiceNo != null && p.BillBookId != null && ((p.CancelARPmId == null && p.CCancelARPmId == null) || (p.CancelARPmId == null && p.CCancelARPmId != null && SqlMethods.DateDiffDay(p.ARPaymentDt, p.CPaymentDt) > 0))
                                              && p.CashierId == (pControllerId == null ? p.CashierId : pControllerId)
                                    select new
                                    {
                                        p.PaymentId,
                                        p.ControllerName,
                                        //p.MruId,
                                        MruId = "มท." + p.GroupInvoiceNo,
                                        CaId = p.CCaId,
                                        BranchId = p.DtId.Contains("MZ") ? null : p.BranchId,
                                        //Period = p.Period != null ? p.Period.Trim() == "" ? null : p.Period.Trim() : p.Period,
                                        Period = (p.ReceiptId.Substring(0, 1) == "F" || p.ReceiptId.Substring(0, 1) == "E" || p.ExtReceiptDt != null) ? null : p.Period == null ? null : p.Period.Trim(),
                                        p.DtName,
                                        ReceiptId = p.CReceiptId,
                                        p.GAmount,
                                        p.AdjAmount,
                                        TotalAmount = p.GAmount + p.AdjAmount,
                                        PaymentDt = p.ExtReceiptDt == null ? p.PaymentDt : p.ExtReceiptDt,
                                        p.CashAmount,
                                        p.ChequeAmount,
                                        p.TransferAmount,
                                        //p.CreditDebitAmount,
                                        //p.QRPaymentAmount,
                                        CaId_Qty = 1,
                                        p.CaId_Qty_New,
                                        FeeAmount = (decimal?)0,
                                        p.CaDoc,
                                        p.InvoiceNo,
                                        RealReceiptId = p.ReceiptId,
                                        QRPaymentAmount = p.QRPaymentAmount,
                                        QRRef1 = p.QRRef1,
                                        QRRef2 = p.QRRef2,
                                        p.QRPaymentRefNo,
                                        p.QROnlineFlag
                                    })
                           group pi by new { pi.PaymentId, pi.ControllerName, pi.MruId, pi.CaId, pi.BranchId, pi.Period, pi.DtName, pi.ReceiptId, pi.GAmount, pi.AdjAmount, 
                               pi.TotalAmount, pi.PaymentDt, pi.FeeAmount, pi.CaDoc, pi.InvoiceNo, pi.RealReceiptId,pi.QRPaymentAmount, pi.QRRef1,pi.QRRef2,pi.QRPaymentRefNo, pi.QROnlineFlag } into bg
                           select new
                           {
                               PaymentId = (string)bg.Key.PaymentId,
                               ControllerName = (string)bg.Key.ControllerName,
                               MruId = (string)bg.Key.MruId,
                               CaId = (string)bg.Key.CaId,
                               BranchId = (string)bg.Key.BranchId,
                               Quantity = (int?)1,
                               Period = (string)bg.Key.Period == "" ? null : (string)bg.Key.Period,
                               DtName = (string)bg.Key.DtName,
                               ReceiptId = (string)bg.Key.ReceiptId,
                               GAmount = (decimal?)bg.Key.GAmount,
                               AdjAmount = (decimal?)bg.Key.AdjAmount,
                               TotalAmount = (decimal?)bg.Key.TotalAmount,
                               PaymentDt = (DateTime?)bg.Key.PaymentDt,
                               CashAmount = (decimal?)(bg.Sum(pa => pa.CashAmount) == 0 ? null : bg.Sum(pa => pa.CashAmount)),
                               ChequeAmount = (decimal?)(bg.Sum(pa => pa.ChequeAmount) == 0 ? null : bg.Sum(pa => pa.ChequeAmount)),
                               TransferAmount = (decimal?)(bg.Sum(pa => pa.TransferAmount) == 0 ? null : bg.Sum(pa => pa.TransferAmount)),
                               //CreditDebitAmount = (decimal?)(bg.Sum(pa => pa.CreditDebitAmount) == 0 ? null : bg.Sum(pa => pa.CreditDebitAmount)),
                               CaId_Qty = (int?)(bg.Sum(pa => pa.CaId_Qty) == 0 ? 0 : bg.Sum(pa => pa.CaId_Qty)),
                               CaId_Qty_New = (int?)(bg.Sum(pa => pa.CaId_Qty_New) == 0 ? 0 : bg.Sum(pa => pa.CaId_Qty_New)),
                               FeeAmount = (decimal?)(bg.Key.FeeAmount == 0 ? null : bg.Key.FeeAmount),
                               RealReceiptId = (string)bg.Key.RealReceiptId,
                               QRPaymentAmount = (decimal?)(bg.Sum(pa => pa.QRPaymentAmount) == 0 ? null : bg.Sum(pa => pa.QRPaymentAmount)),
                               InvoiceNo = (string)bg.Key.InvoiceNo,
                               QRRef1 = (string)bg.Key.QRRef1,
                               QRRef2 = (string)bg.Key.QRRef2,
                               QRPaymentRefNo = (string)bg.Key.QRPaymentRefNo,
                               QROnlineFlag = (string)bg.Key.QROnlineFlag
                           };

            //var repResult = elecSec.Union(billbook).Union(groupinv).OrderBy(r => r.ControllerName).ThenBy(w => w.RealReceiptId).ThenBy(w => w.PaymentDt).ThenBy(w => w.CaId).ThenBy(w => w.TotalAmount); //tanayoot_backup
            var repResult = elecSec.Union(billbook).Union(groupinv).OrderBy(w => w.PaymentDt).ThenBy(w => w.CaId).ThenBy(w => w.RealReceiptId).ThenBy(r => r.ControllerName).ThenBy(w => w.TotalAmount);

            foreach (var q in repResult)
            {
                CAC18Report detail = new CAC18Report();
                detail.PaymentId = q.PaymentId;
                detail.Mru = q.MruId;
                detail.CaId = q.CaId;
                detail.BranchId = q.BranchId;
                detail.Period = q.Period;
                detail.ControllerName = q.ControllerName;
                detail.DebtType = q.DtName;
                detail.ReceiptId = q.ReceiptId;
                //detail.BaseAmount = q.BaseAmount;
                //detail.FtAmount = q.FtAmount;
                //detail.VatAmount = q.VatAmount;
                detail.GAmount = q.GAmount;
                detail.CashAmount = q.CashAmount;
                detail.ChequeAmount = q.ChequeAmount;
                detail.TransferAmount = q.TransferAmount;
                //detail.CreditDebitAmount = q.CreditDebitAmount;
                detail.QRPaymentAmount = q.QRPaymentAmount;
                detail.TotalAmount = q.TotalAmount;
                detail.AdjAmount = q.AdjAmount;
                detail.FeeAmount = q.FeeAmount;
                //detail.Type = q.Type;
                detail.Quantity = q.Quantity;
                //detail.DtId = q.DtId;
                detail.PaymentDt = q.PaymentDt;
                detail.CaId_Qty = q.CaId_Qty;
                detail.CaId_Qty_New = q.CaId_Qty_New;
                detail.QRRef1 = q.QRRef1;
                detail.QRRef2 = q.QRRef2;
                detail.InvoinceNo = q.InvoiceNo;
                detail.QRPaymentRefNo = q.QRPaymentRefNo;
                detail.QROnlineFlag = q.QROnlineFlag;
                detail.Quantity = q.Quantity;
                report.Add(detail);
            }

            return report;
        }


        public List<CAC19Report> GetReportCAC19(string pBranchId, string pControllerId, DateTime? pFromDate, DateTime? pToDate)
        {
            List<CAC19Report> report = new List<CAC19Report>();

            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC19");
            cmd.CommandTimeout = 360;
            db.AddInParameter(cmd, "BranchId", DbType.String, pBranchId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, pFromDate);

            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable repDs = ds.Tables[0];

            var repData = from rep in repDs.AsEnumerable()
                          select new 
                          {
                              CashierId = rep.Field<string>("CashierId"),
                              ControllerName = rep.Field<string>("CashierName"),
                              PaymentDt = rep.Field<DateTime?>("PaymentDt"),
                              Amount = rep.Field<decimal?>("GAmount"),
                              QRRef1 = rep.Field<string>("QRRef1"),
                              QRRef2 = rep.Field<string>("QRRef2"),
                          };

            foreach (var q in repData)
            {
                CAC19Report detail = new CAC19Report();
                detail.CashierId = q.CashierId;
                detail.ControllerName = q.ControllerName;
                detail.GAmount = q.Amount;
                detail.PaymentDt = q.PaymentDt;
                detail.QRRef1 = q.QRRef1;
                detail.QRRef2 = q.QRRef2;
                report.Add(detail);
            }

            return report;
        }

        //TODO: 2.8.4
        //public List<CAC16Report> GetReportCAC16(string pBranchId, DateTime? pFromDate)
        //{
        //    List<CAC16Report> report = new List<CAC16Report>();

        //    Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
        //    DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC16X");
        //    cmd.CommandTimeout = 360;
        //    db.AddInParameter(cmd, "BranchId", DbType.String, pBranchId);
        //    db.AddInParameter(cmd, "FromDate", DbType.DateTime, pFromDate);
        //    DataSet ds = db.ExecuteDataSet(cmd);
        //    DataTable mainRaw = ds.Tables[0];


        //    var repData = from rep in mainRaw.AsEnumerable()
        //                  select new
        //                  {
        //                      CaDoc = rep.Field<string>("CaDoc"),
        //                      SubCaDoc = rep.Field<string>("SubCaDoc"),
        //                      InvoiceNo = rep.Field<string>("InvoiceNo"),
        //                      PaymentBranchId = rep.Field<string>("PaymentBranchId"),
        //                      TechBranchId = rep.Field<string>("TechBranchId"),
        //                      PaymentDt = rep.Field<DateTime?>("PaymentDt"),
        //                      CashierId = rep.Field<string>("CashierId"),
        //                      CashierName = rep.Field<string>("CashierName")
        //                  };

        //    var queryResult = from r in repData
        //                 orderby r.PaymentDt
        //                 select new
        //                 {
        //                     r.CaDoc,
        //                     r.SubCaDoc,
        //                     r.InvoiceNo,
        //                     r.PaymentBranchId,
        //                     r.TechBranchId,
        //                     r.PaymentDt,
        //                     r.CashierId,
        //                     r.CashierName
        //                 };


        //    foreach (var q in queryResult)
        //    {
        //        CAC16Report detail = new CAC16Report();
        //        detail.CaDoc = q.CaDoc;
        //        detail.SubCaDoc = q.SubCaDoc;
        //        detail.InvoiceNo = q.InvoiceNo;
        //        detail.PaymentBranchId = q.PaymentBranchId;
        //        detail.TechBranchId = q.TechBranchId;
        //        detail.PaymentDt = q.PaymentDt;
        //        detail.CashierId = q.CashierId;
        //        detail.CashierName = q.CashierName;
        //        report.Add(detail);
        //    }

        //    return report;
        //}

    }
}

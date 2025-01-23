using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using PEA.BPM.Architecture.CommonUtilities;
using System.Configuration;

namespace PEA.BPM.PaymentCollectionModule.DA
{
    public class ReportDA
    {
        private int timeout = (ConfigurationSettings.AppSettings["PosTimeOutReport"] == null ? 600 : Convert.ToInt32(ConfigurationSettings.AppSettings["PosTimeOutReport"]));

        public List<CAC01Report> GetReportCAC01(CAC01Param param)
        {
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC01");
            if (param.Report == ReportName.CAC01_1)
            {
                cmd = db.GetStoredProcCommand("pc_sel_RpCAC01-1");
            }
            else if (param.Report == ReportName.CAC01_2)
            {
                cmd = db.GetStoredProcCommand("pc_sel_RpCAC01-2");
            }
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "ControllerId", DbType.String, param.ControllerId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, param.TransFromDate);
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, param.TransToDate);
            DataSet ds = db.ExecuteDataSet(cmd);

            List<CAC01Report> report = new List<CAC01Report>();

            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                CAC01Report detail = new CAC01Report();
                detail.PaidBranchId = DaHelper.GetString(dr, "PaidBranchId");
                detail.PaidBranchName = DaHelper.GetString(dr, "PaidBranchName");
                detail.CaId = DaHelper.GetString(dr, "CaId");
                detail.ControllerId = DaHelper.GetString(dr, "ControllerId");
                detail.Quantity = DaHelper.GetInt(dr, "Quantity");
                detail.Period = DaHelper.GetString(dr, "Period");
                detail.BaseAmount = DaHelper.GetDecimal(dr, "BaseAmount");
                detail.FtAmount = DaHelper.GetDecimal(dr, "FtAmount");
                detail.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                detail.GAmount = DaHelper.GetDecimal(dr, "GAmount");
                detail.Remark = DaHelper.GetString(dr, "Remark");
                detail.BillBookId = DaHelper.GetString(dr, "BillBookId");
                detail.InstallmentFlag = DaHelper.GetString(dr, "InstallmentFlag");
                detail.LastInstallmentFlag = DaHelper.GetString(dr, "LastInstallmentFlag");
                detail.Type = DaHelper.GetString(dr, "Type");
                detail.GroupType = DaHelper.GetString(dr, "GroupType");
                report.Add(detail);
            }

            return report;
        }

        public List<CAC03Report> GetReportCAC03(CAC01Param param)
        {
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC03-1");
            if (param.Report == ReportName.CAC03_1 || param.Report == ReportName.CAC03_2)
            {
                cmd = db.GetStoredProcCommand("pc_sel_RpCAC03-1");
            }
            else if (param.Report == ReportName.CAC03_3 || param.Report == ReportName.CAC03_4)
            {
                cmd = db.GetStoredProcCommand("pc_sel_RpCAC03-2");
            }
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "ControllerId", DbType.String, param.ControllerId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, param.TransFromDate);
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, param.TransToDate);
            db.AddInParameter(cmd, "FromTime", DbType.String, param.FromTime);
            db.AddInParameter(cmd, "ToTime", DbType.String, param.ToTime);
            db.AddInParameter(cmd, "BankKey", DbType.String, param.BankKey);
            DataSet ds = db.ExecuteDataSet(cmd);

            List<CAC03Report> report = new List<CAC03Report>();

            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                CAC03Report detail = new CAC03Report();
                detail.BranchId = DaHelper.GetString(dr, "BranchId");
                detail.GroupBankName = DaHelper.GetString(dr, "GroupBankName");
                detail.BankKey = DaHelper.GetString(dr, "BankKey");
                detail.BankName = DaHelper.GetString(dr, "BankName");
                detail.ChqType = DaHelper.GetString(dr, "ChqType");
                detail.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                detail.ChqNo = DaHelper.GetString(dr, "ChqNo");
                detail.ChqDt = DaHelper.GetDate(dr, "ChqDt");
                detail.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                detail.ChequeAmount = DaHelper.GetDecimal(dr, "ChequeAmount");
                detail.ChangeAmount = DaHelper.GetDecimal(dr, "ChangeAmount");
                detail.ActualAmount = DaHelper.GetDecimal(dr, "ActualAmount");
                detail.FeeAmount = DaHelper.GetDecimal(dr, "FeeAmount");
                detail.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                detail.CaId = DaHelper.GetString(dr, "CaId");
                detail.CaName = DaHelper.GetString(dr, "CaName");
                detail.DtName = DaHelper.GetString(dr, "DtName");
                detail.ControllerName = DaHelper.GetString(dr, "ControllerName");
                detail.CaDoc = DaHelper.GetString(dr, "CaDoc");
                detail.PaymentId = DaHelper.GetString(dr, "PaymentId");
                detail.Quantity = 1;
                detail.IsDuplicate = false;
                report.Add(detail);
            }

            return report;
        }

        public List<CAC04Report> GetReportCAC04(CAC01Param param)
        {
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC04-1");
            if (param.Report == ReportName.CAC04_1 || param.Report == ReportName.CAC04_2)
            {
                cmd = db.GetStoredProcCommand("pc_sel_RpCAC04-1");
            }
            else if (param.Report == ReportName.CAC04_3 || param.Report == ReportName.CAC04_4)
            {
                cmd = db.GetStoredProcCommand("pc_sel_RpCAC04-2");
            }
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "ControllerId", DbType.String, param.ControllerId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, param.TransFromDate);
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, param.TransToDate);
            db.AddInParameter(cmd, "FromTime", DbType.String, param.FromTime);
            db.AddInParameter(cmd, "ToTime", DbType.String, param.ToTime);
            db.AddInParameter(cmd, "BankKey", DbType.String, param.BankKey);
            DataSet ds = db.ExecuteDataSet(cmd);

            List<CAC04Report> report = new List<CAC04Report>();

            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                CAC04Report detail = new CAC04Report();
                detail.BranchId = DaHelper.GetString(dr, "BranchId");
                detail.GroupBankName = DaHelper.GetString(dr, "GroupBankName");
                detail.BankKey = DaHelper.GetString(dr, "BankKey");
                detail.BankName = DaHelper.GetString(dr, "BankName");
                detail.TranfAccNo = DaHelper.GetString(dr, "TranfAccNo");
                detail.ClearingAccNo = DaHelper.GetString(dr, "ClearingAccNo");
                detail.TranfDt = DaHelper.GetDate(dr, "TranfDt");
                detail.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                detail.Amount = DaHelper.GetDecimal(dr, "Amount");
                detail.ActualAmount = DaHelper.GetDecimal(dr, "ActualAmount");
                detail.FeeAmount = DaHelper.GetDecimal(dr, "FeeAmount");
                detail.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                detail.CaId = DaHelper.GetString(dr, "CaId");
                detail.CaName = DaHelper.GetString(dr, "CaName");
                detail.DtName = DaHelper.GetString(dr, "DtName");
                detail.ControllerName = DaHelper.GetString(dr, "ControllerName");
                detail.CaDoc = DaHelper.GetString(dr, "CaDoc");
                detail.PaymentId = DaHelper.GetString(dr, "PaymentId");
                detail.Quantity = 1;
                detail.IsDuplicate = false;
                report.Add(detail);
            }

            return report;
        }

        public List<CAC05Report> GetReportCAC05(CAC06Param param)
        {
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC05");
            if (param.Report == ReportName.CAC05_1)
            {
                cmd = db.GetStoredProcCommand("pc_sel_RpCAC05-1");
            }
            else if (param.Report == ReportName.CAC05_2)
            {
                cmd = db.GetStoredProcCommand("pc_sel_RpCAC05-2");
            }
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "ControllerId", DbType.String, param.ControllerId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, param.TransFromDate);
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, param.TransToDate);
            DataSet ds = db.ExecuteDataSet(cmd);

            List<CAC05Report> report = new List<CAC05Report>();

            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                CAC05Report detail = new CAC05Report();
                detail.PaymentId = DaHelper.GetString(dr, "PaymentId");
                detail.Mru = DaHelper.GetString(dr, "Mru");
                detail.CaId = DaHelper.GetString(dr, "CaId");
                detail.BranchId = DaHelper.GetString(dr, "BranchId");
                detail.Period = DaHelper.GetString(dr, "Period");
                detail.ControllerName = DaHelper.GetString(dr, "ControllerName");
                detail.DebtType = DaHelper.GetString(dr, "DtName");
                detail.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                detail.BaseAmount = DaHelper.GetDecimal(dr, "BaseAmount");
                detail.FtAmount = DaHelper.GetDecimal(dr, "FtAmount");
                detail.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                detail.GAmount = DaHelper.GetDecimal(dr, "GAmount");
                detail.CashAmount = DaHelper.GetDecimal(dr, "CashAmount");
                detail.ChequeAmount = DaHelper.GetDecimal(dr, "ChequeAmount");
                detail.TransferAmount = DaHelper.GetDecimal(dr, "TransferAmount");
                detail.TotalAmount = DaHelper.GetDecimal(dr, "TotalAmount");
                detail.AdjAmount = DaHelper.GetDecimal(dr, "AdjAmount");
                detail.FeeAmount = DaHelper.GetDecimal(dr, "FeeAmount");
                detail.Type = DaHelper.GetString(dr, "Type");
                detail.Quantity = DaHelper.GetInt(dr, "Quantity");
                detail.DtId = DaHelper.GetString(dr, "DtId");
                detail.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                detail.CaId_Qty = DaHelper.GetInt(dr, "CaId_Qty");
                report.Add(detail);
            }

            return report;
        }

        public List<CAC06Report> GetReportCAC06(CAC06Param param)
        {
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC06-1");
            if (param.Report == ReportName.CAC06_1)
            {
                cmd = db.GetStoredProcCommand("pc_sel_RpCAC06-1");
            }
            else if (param.Report == ReportName.CAC06_2 || param.Report == ReportName.CAC06_3)
            {
                cmd = db.GetStoredProcCommand("pc_sel_RpCAC06-2");
            }
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "ControllerId", DbType.String, param.ControllerId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, param.TransFromDate);
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, param.TransToDate);
            DataSet ds = db.ExecuteDataSet(cmd);

            List<CAC06Report> report = new List<CAC06Report>();

            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                CAC06Report detail = new CAC06Report();
                detail.PaidBranchId = DaHelper.GetString(dr, "PaidBranchId");
                detail.PaidBranchName = DaHelper.GetString(dr, "PaidBranchName");
                detail.BranchDetail = DaHelper.GetString(dr, "BranchDetail");
                detail.CaId = DaHelper.GetString(dr, "CaId");
                detail.PosId = DaHelper.GetString(dr, "PosId");
                detail.CaName = DaHelper.GetString(dr, "CaName");
                detail.Period = DaHelper.GetString(dr, "Period");
                detail.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                detail.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                detail.BaseAmount = DaHelper.GetDecimal(dr, "BaseAmount");
                detail.FtAmount = DaHelper.GetDecimal(dr, "FtAmount");
                detail.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                detail.GAmount = DaHelper.GetDecimal(dr, "GAmount");
                detail.ControllerName = DaHelper.GetString(dr, "ControllerName");
                detail.DtId = DaHelper.GetString(dr, "DtId");
                detail.CaDoc = DaHelper.GetString(dr, "CaDoc");
                report.Add(detail);
            }

            return report;
        }

        public List<CAC06Report> GetReportCAC07(CAC06Param param)
        {
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC07");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "ControllerId", DbType.String, param.ControllerId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, param.TransFromDate);
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, param.TransToDate);
            DataSet ds = db.ExecuteDataSet(cmd);

            List<CAC06Report> report = new List<CAC06Report>();

            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                CAC06Report detail = new CAC06Report();
                detail.BranchDetail = DaHelper.GetString(dr, "BranchDetail");
                detail.CaId = DaHelper.GetString(dr, "CaId");
                detail.PosId = DaHelper.GetString(dr, "PosId");
                detail.CaName = DaHelper.GetString(dr, "CaName");
                detail.Period = DaHelper.GetString(dr, "Period");
                detail.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                detail.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                detail.BaseAmount = DaHelper.GetDecimal(dr, "BaseAmount");
                detail.FtAmount = DaHelper.GetDecimal(dr, "FtAmount");
                detail.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                detail.GAmount = DaHelper.GetDecimal(dr, "GAmount");
                detail.ControllerName = DaHelper.GetString(dr, "ControllerName");
                detail.DtId = DaHelper.GetString(dr, "DtId");
                detail.ControllerAccName = DaHelper.GetString(dr, "ControllerAccName");
                report.Add(detail);
            }

            return report;
        }

        public List<CAC09Report> GetReportCAC09(CAC09Param param)
        {
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC09-1");
            if (param.Report == ReportName.CAC09_1)
            {
                cmd = db.GetStoredProcCommand("pc_sel_RpCAC09-1");
            }
            else if (param.Report == ReportName.CAC09_2)
            {
                cmd = db.GetStoredProcCommand("pc_sel_RpCAC09-2");
            }
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "PosId", DbType.String, param.PosId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, param.TransFromDate);
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, param.TransToDate);
            DataSet ds = db.ExecuteDataSet(cmd);

            List<CAC09Report> report = new List<CAC09Report>();

            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                CAC09Report detail = new CAC09Report();
                detail.PosId = DaHelper.GetString(dr, "TerminalCode");
                detail.CaId = DaHelper.GetString(dr, "CaId");
                detail.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                detail.ExtReceiptId = DaHelper.GetString(dr, "ExtReceiptId");
                detail.Quantity = DaHelper.GetInt(dr, "Quantity");
                detail.ElectricBaseAmount = DaHelper.GetDecimal(dr, "ElectricBaseAmount");
                detail.OtherBaseAmount = DaHelper.GetDecimal(dr, "OtherBaseAmount");
                detail.FtAmount = DaHelper.GetDecimal(dr, "FtAmount");
                detail.TotalVatAmount = DaHelper.GetDecimal(dr, "TotalVatAmount");
                detail.TotalGAmount = DaHelper.GetDecimal(dr, "TotalAmount");
                detail.RPGAmount = DaHelper.GetDecimal(dr, "RPGAmount");
                detail.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                detail.AdjAmount = DaHelper.GetDecimal(dr, "AdjAmount");
                detail.GAmount = DaHelper.GetDecimal(dr, "GAmount");
                detail.Type = DaHelper.GetString(dr, "Type");
                report.Add(detail);
            }

            return report;
        }

        public List<CAC11Report> GetReportCAC11(CAC11Param param)
        {
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC11");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "PosId", DbType.String, param.PosId);
            db.AddInParameter(cmd, "ControllerId", DbType.String, param.ControllerId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, param.TransFromDate);
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, param.TransToDate);
            DataSet ds = db.ExecuteDataSet(cmd);

            List<CAC11Report> report = new List<CAC11Report>();

            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                CAC11Report detail = new CAC11Report();
                detail.BranchId = DaHelper.GetString(dr, "BranchId");
                detail.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                detail.CancelDt = DaHelper.GetDate(dr, "CancelDt");
                detail.ControllerId = DaHelper.GetString(dr, "ControllerId");
                detail.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                detail.CollectorName = DaHelper.GetString(dr, "CollectorName");
                detail.PosId = DaHelper.GetString(dr, "PosId");
                detail.ReturnCollectorName = DaHelper.GetString(dr, "ReturnCollectorName");
                detail.ReturnPosId = DaHelper.GetString(dr, "ReturnPosId");
                detail.CaId = DaHelper.GetString(dr, "CaId");
                detail.CaName = DaHelper.GetString(dr, "CaName");
                detail.Period = DaHelper.GetString(dr, "Period");
                detail.DtName = DaHelper.GetString(dr, "DtName");
                detail.Amount = DaHelper.GetDecimal(dr, "Amount");
                detail.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                detail.GAmount = DaHelper.GetDecimal(dr, "GAmount");
                detail.CancelReason = DaHelper.GetString(dr, "CancelReason");
                report.Add(detail);
            }

            return report;
        }

        public List<CAC12Report> GetReportCAC12(CAC09Param param)
        {
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC12");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, param.TransFromDate);
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, param.TransToDate);
            DataSet ds = db.ExecuteDataSet(cmd);

            List<CAC12Report> report = new List<CAC12Report>();

            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                CAC12Report detail = new CAC12Report();
                detail.BranchId = DaHelper.GetString(dr, "BranchId");
                detail.BranchName = DaHelper.GetString(dr, "BranchName");
                detail.RateTypeId = DaHelper.GetString(dr, "RateTypeId");
                detail.Quantity = DaHelper.GetInt(dr, "Quantity");
                detail.BaseAmount = DaHelper.GetDecimal(dr, "BaseAmount");
                detail.FtAmount = DaHelper.GetDecimal(dr, "FtAmount");
                detail.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                detail.GAmount = DaHelper.GetDecimal(dr, "GAmount");
                report.Add(detail);
            }

            return report;
        }

        public List<CAC13Report> GetReportCAC13(CAC11Param param)
        {
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC13");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "PosId", DbType.String, param.PosId);
            db.AddInParameter(cmd, "ControllerId", DbType.String, param.ControllerId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, param.TransFromDate);
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, param.TransToDate);
            DataSet ds = db.ExecuteDataSet(cmd);

            List<CAC13Report> report = new List<CAC13Report>();

            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                CAC13Report detail = new CAC13Report();
                detail.BranchId = DaHelper.GetString(dr, "BranchId");
                detail.BranchName = DaHelper.GetString(dr, "BranchName");
                detail.PaymentDt = DaHelper.GetString(dr, "PaymentDt");
                detail.PosId = DaHelper.GetString(dr, "PosId");
                detail.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                detail.DtName = DaHelper.GetString(dr, "DtName");
                detail.ControllerId = DaHelper.GetString(dr, "ControllerId");
                detail.Quantity = DaHelper.GetInt(dr, "Quantity");
                detail.BaseAmount = DaHelper.GetDecimal(dr, "BaseAmount");
                detail.FtAmount = DaHelper.GetDecimal(dr, "FtAmount");
                detail.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                detail.GAmount = DaHelper.GetDecimal(dr, "GAmount");
                report.Add(detail);
            }

            return report;
        }

        public List<CAC14Report> GetReportCAC14(CAC14Param param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC14");
            cmd.CommandTimeout = timeout;
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
                report.Add(detail);
            }

            return report;
        }

        public string pc_get_GroupInvoiceHeaderForReportCAC14(string groupInvoiceNo, string paymentId, string receiptId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_get_GroupInvoiceHeaderForReportCAC14");
            cmd.CommandTimeout = timeout;
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

        public List<CAC18Report> GetReportCAC18(CAC18Param param)
        {
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC05-2");
            //cmd = db.GetStoredProcCommand("pc_sel_RpCAC05-2");

            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "ControllerId", DbType.String, param.ControllerId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, param.TransFromDate);
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, param.TransToDate);
            DataSet ds = db.ExecuteDataSet(cmd);

            List<CAC18Report> report = new List<CAC18Report>();

            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                CAC18Report detail = new CAC18Report();
                detail.PaymentId = DaHelper.GetString(dr, "PaymentId");
                detail.Mru = DaHelper.GetString(dr, "Mru");
                detail.CaId = DaHelper.GetString(dr, "CaId");
                detail.BranchId = DaHelper.GetString(dr, "BranchId");
                detail.Period = DaHelper.GetString(dr, "Period");
                detail.ControllerName = DaHelper.GetString(dr, "ControllerName");
                detail.DebtType = DaHelper.GetString(dr, "DtName");
                detail.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                detail.BaseAmount = DaHelper.GetDecimal(dr, "BaseAmount");
                detail.FtAmount = DaHelper.GetDecimal(dr, "FtAmount");
                detail.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                detail.GAmount = DaHelper.GetDecimal(dr, "GAmount");
                detail.CashAmount = DaHelper.GetDecimal(dr, "CashAmount");
                detail.ChequeAmount = DaHelper.GetDecimal(dr, "ChequeAmount");
                detail.TransferAmount = DaHelper.GetDecimal(dr, "TransferAmount");
                detail.TotalAmount = DaHelper.GetDecimal(dr, "TotalAmount");
                detail.AdjAmount = DaHelper.GetDecimal(dr, "AdjAmount");
                detail.FeeAmount = DaHelper.GetDecimal(dr, "FeeAmount");
                detail.Type = DaHelper.GetString(dr, "Type");
                detail.Quantity = DaHelper.GetInt(dr, "Quantity");
                detail.DtId = DaHelper.GetString(dr, "DtId");
                detail.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                detail.CaId_Qty = DaHelper.GetInt(dr, "CaId_Qty");
                report.Add(detail);
            }

            return report;
        }

        public List<CAC19Report> GetReportCAC19(CAC19Param param)
        {
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC05-2");
            //cmd = db.GetStoredProcCommand("pc_sel_RpCAC05-2");

            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "ControllerId", DbType.String, param.ControllerId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, param.TransFromDate);
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, param.TransToDate);
            DataSet ds = db.ExecuteDataSet(cmd);

            List<CAC19Report> report = new List<CAC19Report>();

            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                CAC19Report detail = new CAC19Report();
                detail.CashierId = DaHelper.GetString(dr, "CashierId");
                detail.ControllerName = DaHelper.GetString(dr, "CashierName");
                detail.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                detail.QRRef1 = DaHelper.GetString(dr, "QRRef1");
                detail.QRRef2 = DaHelper.GetString(dr, "QRRef2");
                detail.GAmount = DaHelper.GetDecimal(dr, "GAmount");
                report.Add(detail);
            }

            return report;
        }

        //TODO: INSTALLMENT CASE
        //public List<CAC16Report> GetReportCAC16(CAC16Param param)
        //{
        //    Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
        //    DbCommand cmd = db.GetStoredProcCommand("pc_sel_RpCAC16");
        //    cmd.CommandTimeout = timeout;
        //    db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
        //    db.AddInParameter(cmd, "FromDate", DbType.DateTime, param.TransFromDate);
        //    db.AddInParameter(cmd, "ToDate", DbType.DateTime, param.TransToDate);
        //    DataSet ds = db.ExecuteDataSet(cmd);

        //    List<CAC16Report> report = new List<CAC16Report>();

        //    DataTable dt = ds.Tables[0];
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        CAC16Report detail = new CAC16Report();
        //        detail.CaDoc = DaHelper.GetString(dr, "CaDoc");
        //        detail.SubCaDoc = DaHelper.GetString(dr, "SubCaDoc");
        //        detail.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
        //        detail.PaymentBranchId = DaHelper.GetString(dr, "PaymentBranchId");
        //        detail.TechBranchId = DaHelper.GetString(dr, "TechBranchId");
        //        detail.PaymentDt = DaHelper.GetDateTime(dr, "PaymentDt");
        //        detail.CashierId = DaHelper.GetString(dr, "CashierId");
        //        detail.CashierName = DaHelper.GetString(dr, "CashierName");
        //        report.Add(detail);
        //    }

        //    return report;
        //}

    }
}

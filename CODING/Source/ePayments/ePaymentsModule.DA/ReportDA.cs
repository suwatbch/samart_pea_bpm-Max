using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading;
using System.Data.Common;
using System.Globalization;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.ReceiptPrinting;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
namespace PEA.BPM.ePaymentsModule.DA
{
    public class ReportDA
    {
        public const int CMD_TIMEOUT = 36000; 

        public List<GreenReceipt> GetGreenReceiptData(GreenReceiptParam greenRecParam)
        {
            List<GreenReceipt> greenRecList = new List<GreenReceipt>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_get_GreenReceipt");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "CaId", DbType.String, greenRecParam.CaId);
            db.AddInParameter(cmd, "Branch", DbType.String, greenRecParam.Branch);
            db.AddInParameter(cmd, "Period", DbType.String, greenRecParam.Period);
            db.AddInParameter(cmd, "InvoiceNo", DbType.String, greenRecParam.InvoiceNo);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow r in dt.Rows)
            {

                GreenReceipt greenRec = new GreenReceipt();
                greenRec.ReceiptId = "X0804000067";
                greenRec.PostDt = DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "PostDt"));
                greenRec.TaxInvBranch = "0223";
                greenRec.CaName = DaHelper.GetString(r, "CaName");
                greenRec.CaAddress = DaHelper.GetString(r, "CaAddress");
                greenRec.MeterId = DaHelper.GetString(r, "MeterId");
                greenRec.RateType = DaHelper.GetString(r, "RateTypeId");
                greenRec.CaId = DaHelper.GetString(r, "CaId");
                greenRec.Peroid = DaHelper.GetString(r, "Peroid");
                greenRec.MeterReadDt = DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "MeterReadDt"));
                greenRec.LastUnit = DaHelper.GetString(r, "LastUnit");
                greenRec.ReadUnit = DaHelper.GetString(r, "ReadUnit");
                greenRec.Qty = DaHelper.GetString(r, "Qty");
                greenRec.BaseAmount = DaHelper.GetDecimal(r, "BaseAmount");
                greenRec.FtUnitPrice = DaHelper.GetDecimal(r, "FtUnitPrice");
                greenRec.FtAmount = DaHelper.GetDecimal(r, "FtAmount");
                greenRec.TaxRate = DaHelper.GetDecimal(r, "TaxRate");
                greenRec.VatAmount = DaHelper.GetDecimal(r, "VatAmount");
                greenRec.GAmount = DaHelper.GetDecimal(r, "GAmount");
                greenRecList.Add(greenRec);
            }
            return greenRecList;
        }

        public List<RE01_ReportInfo> GetRe01ReportData(RE01Param param)
        {
            List<RE01_ReportInfo> re01List = new List<RE01_ReportInfo>();
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_sel_RE_01");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "AccountClassId", DbType.String, param.AccountClassId);
            db.AddInParameter(cmd, "CompanyId", DbType.String, param.CompanyId);
            db.AddInParameter(cmd, "UploadDt", DbType.DateTime, param.UploadDt);
            db.AddInParameter(cmd, "RunningBranchId", DbType.String, param.RunningBranchId);

            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow r in dt.Rows)
            {

                RE01_ReportInfo re01 = new RE01_ReportInfo();
                re01.BranchId = DaHelper.GetString(r, "BranchId");
                re01.MruId = DaHelper.GetString(r, "MruId");
                re01.CaId = DaHelper.GetString(r, "CaId");
                re01.Period = StringConvert.FormatPeriod(DaHelper.GetString(r, "Period"));
                re01.CaName = DaHelper.GetString(r, "CaName");
                re01.BpmGAmount = DaHelper.GetDecimal(r, "BpmGAmount");
                re01.BpmVatAmount = DaHelper.GetDecimal(r, "BpmVatAmount");
                re01.OutSourceAmount = DaHelper.GetDecimal(r, "OutSourceAmount");
                re01.VatAmount = DaHelper.GetDecimal(r, "VatAmount");
                re01.DebtBalance = DaHelper.GetDecimal(r, "DebtBalance");
                re01.UploadStatus = DaHelper.GetString(r, "UploadStatus");
                re01.PayDt = DateFormatter.ToDateThString(DaHelper.GetDate(r, "PayDt").Value);
                re01.RecNo = DaHelper.GetString(r, "RecNo");
                re01.RefDocNo = DaHelper.GetString(r, "RefDocNo");
                re01.FixedType = DaHelper.GetString(r, "FixedType");
                re01.TotalUpload = DaHelper.GetInt(r, "TotalUpload");
                re01.TotalAmount = DaHelper.GetDecimal(r, "TotalAmount");
                re01.TotalVatAmount = DaHelper.GetDecimal(r, "TotalVatAmount");

                re01List.Add(re01);
            }
            return re01List;
        }

        public List<RE02_ReportInfo> GetRe02ReportData(RE02ParamInfo param)
        {
            List<RE02_ReportInfo> re02List = new List<RE02_ReportInfo>();
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_sel_RE_02");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "AccountClassId", DbType.String, param.AccountClassId);
            db.AddInParameter(cmd, "CompanyId", DbType.String, param.CompanyId);
            db.AddInParameter(cmd, "BeginUploadDt", DbType.DateTime, param.BeginUploadDt);
            db.AddInParameter(cmd, "EndUploadDt", DbType.DateTime, param.EndUploadDt);
            db.AddInParameter(cmd, "RunningBranchId", DbType.String, param.RunningBranchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow r in dt.Rows)
            {

                RE02_ReportInfo re02 = new RE02_ReportInfo();
                re02.BranchId = DaHelper.GetString(r, "BranchId");
                re02.BranchName = DaHelper.GetString(r, "BranchName");
                re02.AccountClassName = DaHelper.GetString(r, "AccountClassName");
                re02.CaName = DaHelper.GetString(r, "CaName");
                re02.MruId = DaHelper.GetString(r, "MruId");
                re02.CaId = DaHelper.GetString(r, "CaId");
                re02.Period = StringConvert.FormatPeriod(DaHelper.GetString(r, "Period"));
                re02.OutSourceAmount = DaHelper.GetDecimal(r, "OutSourceAmount");
                re02.InvoiceNo = DaHelper.GetString(r, "InvoiceNo");
                re02.PayDt = DateFormatter.ToDateThString( DaHelper.GetDate(r, "PayDt").Value);
                re02List.Add(re02);
            }
            return re02List;
        }

        public List<RE03_ReportInfo> GetRe03ReportData(RE03ParamInfo param)
        {
            List<RE03_ReportInfo> re03List = new List<RE03_ReportInfo>();
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_sel_RE_03");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "AccountClassId", DbType.String, param.AccountClassId);
            db.AddInParameter(cmd, "PayDt", DbType.DateTime, param.PayDt);
            db.AddInParameter(cmd, "RunningBranchId", DbType.String, param.RunningBranchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow r in dt.Rows)
            {

                RE03_ReportInfo re03 = new RE03_ReportInfo();
                re03.AccountClassId = DaHelper.GetString(r, "AccountClassId");
                re03.AccountClassName = DaHelper.GetString(r, "AccountClassName");
                re03.BranchId = DaHelper.GetString(r, "BranchId");
                re03.BranchName = DaHelper.GetString(r, "BranchName");
                re03.CaNumber = DaHelper.GetInt(r, "CaNumber");
                re03.Amount = DaHelper.GetDecimal(r, "Amount");
                re03.BranchGroup = DaHelper.GetString(r, "BranchGroup");
                re03List.Add(re03);
            }
            return re03List;
        }

        public List<RE04_ReportInfo> GetRe04ReportData(RE04ParamInfo param)
        {
            List<RE04_ReportInfo> re04List = new List<RE04_ReportInfo>();
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_sel_RE_04");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "AccountClassId", DbType.String, param.AccountClassId);
            db.AddInParameter(cmd, "CompanyId", DbType.String, param.CompanyId);
            db.AddInParameter(cmd, "PayDt", DbType.DateTime, param.PayDt);
            db.AddInParameter(cmd, "RunningBranchId", DbType.String, param.RunningBranchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow r in dt.Rows)
            {

                RE04_ReportInfo re04 = new RE04_ReportInfo();
                re04.AccountClassId = DaHelper.GetString(r, "AccountClassId");
                re04.AccountClassName = DaHelper.GetString(r, "AccountClassName");
                re04.CompanyId = DaHelper.GetString(r, "CompanyId");
                re04.Company = DaHelper.GetString(r, "Company");
                re04.BranchId = DaHelper.GetString(r, "BranchId");
                re04.BranchName = DaHelper.GetString(r, "BranchName");
                re04.CaNumber = DaHelper.GetInt(r, "CaNumber");
                re04.Amount = DaHelper.GetDecimal(r, "Amount");
                re04.BranchGroup = DaHelper.GetString(r, "BranchGroup");
                re04List.Add(re04);
            }
            return re04List;
        }

        public List<RE05_ReportInfo> GetRe05ReportData(RE05ParamInfo param)
        {
            List<RE05_ReportInfo> re05List = new List<RE05_ReportInfo>();
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_sel_RE_05");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "AccountClassId", DbType.String, param.AccountClassId);
            db.AddInParameter(cmd, "CompanyId", DbType.String, param.CompanyId);
            db.AddInParameter(cmd, "PayDate", DbType.DateTime, param.PayDate);
            db.AddInParameter(cmd, "RunningBranchId", DbType.String, param.RunningBranchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow r in dt.Rows)
            {

                RE05_ReportInfo re05 = new RE05_ReportInfo();
                re05.AccountClassId = DaHelper.GetString(r, "AccountClassId");
                re05.AccountClassName = DaHelper.GetString(r, "AccountClassName");
                re05.CompanyId = DaHelper.GetString(r, "CompanyId");
                re05.Company = DaHelper.GetString(r, "Company");
                re05.PayDt = DaHelper.GetDate(r, "PayDt") == null ? String.Empty : DaHelper.GetDate(r, "PayDt").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                re05.CaNumber = DaHelper.GetInt(r, "CaNumber");
                re05.Amount = DaHelper.GetDecimal(r, "Amount");
                re05List.Add(re05);
            }
            return re05List;
        }

        public List<RE06_ReportInfo> GetRe06ReportData(RE06ParamInfo param)
        {
            List<RE06_ReportInfo> re06List = new List<RE06_ReportInfo>();
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_sel_RE_06");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "PayDt", DbType.DateTime, param.PayDt);
            db.AddInParameter(cmd, "AccountClassId", DbType.String, param.AccountClassId);
            db.AddInParameter(cmd, "CompanyId", DbType.String, param.CompanyId);
            db.AddInParameter(cmd, "RunningBranchId", DbType.String, param.RunningBranchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow r in dt.Rows)
            {
                RE06_ReportInfo re06 = new RE06_ReportInfo();
                re06.AccountClassId = DaHelper.GetString(r, "AccountClassId");
                re06.AccountClassName = DaHelper.GetString(r, "AccountClassName");
                re06.MainBranchId = DaHelper.GetString(r, "MainBranchId");
                re06.MainBranchName = DaHelper.GetString(r, "MainBranchName");
                re06.BranchId = DaHelper.GetString(r, "BranchId");
                re06.BranchName = DaHelper.GetString(r, "BranchName");
                re06.CompanyId = DaHelper.GetString(r, "CompanyId");
                re06.CompanyName = DaHelper.GetString(r, "CompanyName");
                re06.CaCount = DaHelper.GetInt(r, "CaCount").Value;
                re06.TotalAmount = DaHelper.GetDecimal(r, "TotalAmount");
                re06.PaidDate = DaHelper.GetDateTime(r, "PayDt") == null ? "" : DaHelper.GetDateTime(r, "PayDt").ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                re06List.Add(re06);
            }
            return re06List;
        }

        public List<RE07_ReportInfo> GetRe07ReportData(RE07ParamInfo param)
        {
            List<RE07_ReportInfo> re07List = new List<RE07_ReportInfo>();
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_sel_RE_07");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "AccountClassId", DbType.String, param.AccountClassId);
            db.AddInParameter(cmd, "CompanyId", DbType.String, param.CompanyId);
            db.AddInParameter(cmd, "UploadDtFrom", DbType.DateTime, param.UploadDtFrom);
            db.AddInParameter(cmd, "UploadDtTo", DbType.DateTime, param.UploadDtTo);
            db.AddInParameter(cmd, "RunningBranchId", DbType.String, param.RunningBranchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow r in dt.Rows)
            {
                RE07_ReportInfo re07 = new RE07_ReportInfo();
                re07.AccountClassId = DaHelper.GetString(r, "AccountClassId");
		        re07.AccountClassName = DaHelper.GetString(r, "AccountClassName");
		        re07.CompanyId = DaHelper.GetString(r, "CompanyId");
                re07.CompanyName = DaHelper.GetString(r, "CompanyName");
		        re07.UploadDate = DaHelper.GetDate(r, "UploadDt") == null ? String.Empty : DaHelper.GetDate(r, "UploadDt").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
		        re07.TotalBillCount = DaHelper.GetInt(r, "TotalBillCount");
		        re07.TotalBillAmount = DaHelper.GetDecimal(r, "TotalBillAmount");
		        re07.ReceiptId = DaHelper.GetString(r,"ReceiptId");
		        re07.BankKey = DaHelper.GetString(r, "BankKey");
		        re07.BankName = DaHelper.GetString(r, "BankName");
		        re07.TranfAccNo = DaHelper.GetString(r, "TranfAccNo");
		        re07.TranfDate = DaHelper.GetDate(r, "TranfDt") == null ? String.Empty : DaHelper.GetDate(r, "TranfDt").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                re07.Amount = DaHelper.GetDecimal(r, "Amount");
                re07List.Add(re07);
            }
            return re07List;
        }

        public List<RE08_ReportInfo> GetRe08ReportData(RE08ParamInfo param)
        {
            List<RE08_ReportInfo> re08List = new List<RE08_ReportInfo>();
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_sel_RE_08");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "AccountClassId", DbType.String, param.AccountClassId);
            db.AddInParameter(cmd, "CompanyId", DbType.String, param.CompanyId);
            db.AddInParameter(cmd, "UploadDtFrom", DbType.DateTime, param.UploadDtFrom);
            db.AddInParameter(cmd, "UploadDtTo", DbType.DateTime, param.UploadDtTo);
            db.AddInParameter(cmd, "RunningBranchId", DbType.String, param.RunningBranchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow r in dt.Rows)
            {
                RE08_ReportInfo re08 = new RE08_ReportInfo();
                re08.AccountClassId = DaHelper.GetString(r, "AccountClassId");
                re08.AccountClassName = DaHelper.GetString(r, "AccountClassName");
                re08.CompanyId = DaHelper.GetString(r, "CompanyId");
                re08.CompanyName = DaHelper.GetString(r, "CompanyName");
                re08.UploadDate = DaHelper.GetDate(r, "UploadDt") == null ? String.Empty : DaHelper.GetDate(r, "UploadDt").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                re08.PayDate = DaHelper.GetDate(r, "PayDt") == null ? String.Empty : DaHelper.GetDate(r, "PayDt").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                re08.BillCount = DaHelper.GetInt(r, "BillCount");
                re08.BillAmount = DaHelper.GetDecimal(r, "BillAmount");
                re08.TotalBillCount = DaHelper.GetInt(r, "TotalBillCount");
                re08.TotalBillAmount = DaHelper.GetDecimal(r, "TotalBillAmount");
                re08List.Add(re08);
            }
            return re08List;
        }

        public List<RE09_ReportInfo> GetRe09ReportData(RE09ParamInfo param)
        {
            List<RE09_ReportInfo> re09List = new List<RE09_ReportInfo>();
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_sel_RE_09");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "AccountClassId", DbType.String, param.AccountClassId);
            db.AddInParameter(cmd, "CompanyId", DbType.String, param.CompanyId);
            db.AddInParameter(cmd, "PayDtFrom", DbType.DateTime, param.PayDtFrom);
            db.AddInParameter(cmd, "PayDtTo", DbType.DateTime, param.PayDtTo);
            db.AddInParameter(cmd, "RunningBranchId", DbType.String, param.RunningBranchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow r in dt.Rows)
            {                
                RE09_ReportInfo re09 = new RE09_ReportInfo();                
                re09.PayDate= DaHelper.GetDate(r, "PayDt") == null ? String.Empty : DaHelper.GetDate(r, "PayDt").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                re09.AccountClassName = DaHelper.GetString(r, "AccountClassName");
                re09.CompanyId = DaHelper.GetString(r, "CompanyId");
                re09.CompanyName = DaHelper.GetString(r, "CompanyName");
                re09.Branchid = DaHelper.GetString(r, "Branchid");
                re09.BranchName = DaHelper.GetString(r, "BranchName");              
                re09.BillCount = DaHelper.GetInt(r, "BillCount");
                re09.TotalAmount = DaHelper.GetDecimal(r, "TotalAmount");                
                re09List.Add(re09);
            }
            return re09List;
        }         
    }
}
   
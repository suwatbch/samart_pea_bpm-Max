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
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports;

namespace PEA.BPM.ePaymentsModule.DA
{
    public class CommonDA
    {
        public const int CMD_TIMEOUT = 36000; // 10 hr
        
        public string VerifyContractorData(string caId, string period, decimal debtAmount)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_get_VerifyContractorInfo");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "CaId", DbType.String, caId.Trim());
            db.AddInParameter(cmd, "Period", DbType.String, period.Trim());
            db.AddInParameter(cmd, "DebtAmount", DbType.Double, debtAmount);
            int ret = (int)db.ExecuteScalar(cmd);

            if (ret > 0) 
                return "0";
            else 
                return "1";
        }

        public List<SearchContractorInfo> SearchContractorData(string caId, string billFlag)
        {
            List<SearchContractorInfo> conInfoList = new List<SearchContractorInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_get_GetBillInformation");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "CaId", DbType.String, caId);
            db.AddInParameter(cmd, "BillFlag", DbType.String, billFlag);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow r in dt.Rows)
            {
                SearchContractorInfo conInfo = new SearchContractorInfo();
                conInfo.BranchId = DaHelper.GetString(r, "BranchId");
                conInfo.CaId = DaHelper.GetString(r, "CaId");
                conInfo.Period = DaHelper.GetString(r, "Period");
                conInfo.GAmount = DaHelper.GetDecimal(r, "GAmount").Value;
                conInfo.FTAmount = DaHelper.GetDecimal(r, "FTAmount") == null ? 0 : DaHelper.GetDecimal(r, "FTAmount").Value;
                conInfo.VatAmount = DaHelper.GetDecimal(r, "VatAmount") == null ? 0 : DaHelper.GetDecimal(r, "VatAmount").Value;
                conInfo.CAPmId = DaHelper.GetString(r, "CAPmId");
                conInfo.AccountClassId = DaHelper.GetString(r, "AccountClassId");
                conInfo.AccountClassName = DaHelper.GetString(r, "AccountClassName");
                conInfo.ModifiedFlag = DaHelper.GetString(r, "ModifiedFlag");
                conInfo.BillBookFlag = DaHelper.GetString(r, "BillBookFlag");
                conInfo.DisconnectionFlag = DaHelper.GetString(r, "DisconnectionFlag");
                conInfo.KeeperFlag = DaHelper.GetString(r, "KeeperFlag");
                conInfo.PaymentOrderFlag = DaHelper.GetString(r, "PaymentOrderFlag");
                conInfo.GroupInvoiceFlag = DaHelper.GetString(r, "GroupInvoiceFlag");
                conInfo.Qty = DaHelper.GetDecimal(r, "Qty") == null ? 0 : DaHelper.GetDecimal(r, "Qty").Value;
                conInfo.ReceiveStatus = DaHelper.GetString(r, "ReceiveStatus");
                conInfo.ARPmId = DaHelper.GetString(r, "ARPmId");
                conInfo.InvoiceNo = DaHelper.GetString(r, "InvoiceNo");
                conInfo.DueDt = DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "DueDt"));
                conInfoList.Add(conInfo);
            }

            return conInfoList;
        }

        public string UpdateBillMarkFlagData(string caId, string invoiceNo)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_get_VerifyContractorInfo");
            cmd.CommandTimeout = CMD_TIMEOUT;

            db.AddInParameter(cmd, "CaId", DbType.String, caId);
            db.AddInParameter(cmd, "InvoiceNo", DbType.String, invoiceNo);
            int ret = (int)db.ExecuteScalar(cmd);

            if (ret > 0)
                return "0";
            else
                return "1";
        }

        public string InsertARPayment(string caId, string invoiceNo, string modifierBy,  string postServerId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_upd_UpdateARPayment");
            cmd.CommandTimeout = CMD_TIMEOUT;
            // Add data to ARPyment 
            db.AddOutParameter(cmd, "pResult", DbType.String, 1);
            db.AddInParameter(cmd, "pCaId", DbType.String, caId);
            db.AddInParameter(cmd, "pInvoiceNo", DbType.String, invoiceNo);
            db.AddInParameter(cmd, "pPostedServerId", DbType.String, postServerId);
            db.AddInParameter(cmd, "pModifiedBy", DbType.String, modifierBy);                       
            db.ExecuteNonQuery(cmd).ToString();

            string result = (string)db.GetParameterValue(cmd, "pResult");
            return result;
        }

        public List<SearchConAccountInfo> SearchConAccountData(string caId)
        {
            List<SearchConAccountInfo> conAccountInfoList = new List<SearchConAccountInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_get_GetContractorInfo");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "CaId", DbType.String, caId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow r in dt.Rows)
            {
                SearchConAccountInfo conAcountInfo = new SearchConAccountInfo();
                conAcountInfo.BranchId= DaHelper.GetString(r, "BranchId");
                conAcountInfo.CaName = DaHelper.GetString(r, "CaName");
                conAcountInfo.CaAddress = DaHelper.GetString(r, "CaAddress");
                conAcountInfo.AccountClassId = DaHelper.GetString(r, "AccountClassId");
                conAcountInfo.AccountClassName = DaHelper.GetString(r, "AccountClassName");
                conAcountInfo.MeterSizeName = DaHelper.GetString(r, "MeterSizeName");
                conAcountInfo.MeterInstallDt = DateFormatter.ToDateThString(DaHelper.GetDateTime(r, "MeterInstallDt"));
                conAccountInfoList.Add(conAcountInfo);
            }

            return conAccountInfoList;
        }

        public List<Agency> GetAgencyAllData(Agency agencyParam)
        {
            List<Agency> agencyList = new List<Agency>();
            Agency agency = new Agency();
            agency.AgencyId = "01";
            agency.AgencyName = "01:mPay";
            agencyList.Add(agency);
            agency = new Agency();
            agency.AgencyId = "02";
            agency.AgencyName = "02:ธ.กรุงศรีอยุธยา จำกัด (มหาชน)";
            agencyList.Add(agency);
            return agencyList;
        }

        public Agency GetCaData(Agency agencyParam)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                Agency agent = new Agency();
                DbCommand cmd = db.GetStoredProcCommand("ePay_get_CaData");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "CaId", DbType.String, agencyParam.AgencyId);
                db.AddInParameter(cmd, "CaName", DbType.String, agencyParam.AgencyName);                
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    agent.AgencyId = DaHelper.GetString(r, "CaId");
                    agent.AgencyName = DaHelper.GetString(r, "CaName");
                    agent.Address = DaHelper.GetString(r, "CaAddress");
                    return agent;
                      
                }
                else
                    return new Agency();
            }
            catch
            {
                throw;
            }
        } 

        #region "BillingDA"
        public List<BPMClearifyInfo> SearchBPMDebtClearify(SearchDebtParam searchDebtParam)
        {
            List<BPMClearifyInfo> clearifyList = new List<BPMClearifyInfo>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ePay_sel_BPMDebtClearify");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "CaId", DbType.String, searchDebtParam.CaId==""?null:searchDebtParam.CaId);
                db.AddInParameter(cmd, "BranchId", DbType.String, searchDebtParam.BranchId==""?null:searchDebtParam.BranchId);
                db.AddInParameter(cmd, "Period", DbType.String, searchDebtParam.Period==""?null:searchDebtParam.Period);
                db.AddInParameter(cmd, "InvoiceNo", DbType.String, searchDebtParam.InvoiceNo==""?null:searchDebtParam.InvoiceNo);
                db.AddInParameter(cmd, "DebtAmount", DbType.Decimal, searchDebtParam.DebtAmount);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow r in dt.Rows)
                {
                    BPMClearifyInfo clearifyItem = new BPMClearifyInfo();
                    clearifyItem.BranchId = DaHelper.GetString(r, "Branchid");
                    clearifyItem.CaId = DaHelper.GetString(r, "CaId");
                    clearifyItem.CaName = DaHelper.GetString(r, "CaName");
                    clearifyItem.DebtAmount = DaHelper.GetDecimal(r, "GAmount") == null ? 0 : DaHelper.GetDecimal(r, "GAmount").Value;
                    clearifyItem.InvoiceNo = DaHelper.GetString(r, "InvoiceNo");
                    clearifyItem.Period = DaHelper.GetBillPeriod(DaHelper.GetString(r, "Period"));
                    clearifyItem.DueDt = DaHelper.GetDate(r, "DueDt");
                    clearifyItem.PaymentDt = DaHelper.GetDate(r, "PaymentDt");
                    clearifyList.Add(clearifyItem);
                }
                return clearifyList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
 
        }

        public List<ClearifyInfo> SearchDebtData(SearchDebtParam searchDebtParam)
        {
            List<ClearifyInfo> clearifyList = new List<ClearifyInfo>();
            try
            {               
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ePay_sel_ClearifyDetail");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "CaId", DbType.String, searchDebtParam.CaId);
                db.AddInParameter(cmd, "BranchId", DbType.String, searchDebtParam.BranchId);
                db.AddInParameter(cmd, "Period", DbType.String, searchDebtParam.Period);
                db.AddInParameter(cmd, "InvoiceNo", DbType.String, searchDebtParam.InvoiceNo);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow r in dt.Rows)
                {
                    ClearifyInfo clearifyItem = new ClearifyInfo();
                    Agency ag = new Agency();
                    ag.AgencyId = DaHelper.GetString(r, "AgencyId");
                    ag.AgencyName = DaHelper.GetString(r, "AgencyName");
                    clearifyItem.agency = ag;
                    clearifyItem.IssueId = DaHelper.GetString(r, "issueId");
                    clearifyItem.CompanyId = DaHelper.GetString(r, "CompanyId");
                    clearifyItem.FilePaymentDt = DaHelper.GetDate(r, "FilePaymentDt");
                    clearifyItem.FileBranchId = DaHelper.GetString(r, "BranchId");
                    clearifyItem.BPMBranchId = DaHelper.GetString(r, "TechBranchId");
                    clearifyItem.BPMCaId = DaHelper.GetString(r, "BPMCaId");
                    clearifyItem.FileCaId = DaHelper.GetString(r, "FileCaId");
                    clearifyItem.CaName = DaHelper.GetString(r, "CaName");
                    clearifyItem.BPMInvoiceNo = DaHelper.GetString(r, "BPMInvoiceNo");
                    clearifyItem.FileInvoiceNo = DaHelper.GetString(r, "InvoiceNo");
                    clearifyItem.BPMPeriod = DaHelper.GetBillPeriod(DaHelper.GetString(r, "ARPeriod"));
                    clearifyItem.FilePeriod = DaHelper.GetBillPeriod(DaHelper.GetString(r, "Period"));
                    clearifyItem.UploadDt = DaHelper.GetDate(r, "UploadDt");
                    clearifyItem.BPMDueDt = DaHelper.GetDate(r, "BPMDueDt");
                    clearifyItem.FileDueDt = DaHelper.GetDate(r, "FileDueDt");
                    clearifyItem.FileAmount = DaHelper.GetDecimal(r, "OutSourceAmount") == null ? 0 : DaHelper.GetDecimal(r, "OutSourceAmount").Value;
                    clearifyItem.BPMAmount = DaHelper.GetDecimal(r, "GAmount") == null ? 0 : DaHelper.GetDecimal(r, "GAmount").Value;
                    clearifyItem.FileVatAmount = DaHelper.GetDecimal(r, "VatAmount") == null ? 0 : DaHelper.GetDecimal(r, "VatAmount").Value;
                    clearifyItem.BPMVatAmount = DaHelper.GetDecimal(r, "ARVatAmount") == null ? 0 : DaHelper.GetDecimal(r, "ARVatAmount").Value;
                    clearifyItem.UploadStatus = DaHelper.GetString(r, "UploadStatus");

                    clearifyList.Add(clearifyItem);
                }                
                return clearifyList;
            }
            catch (Exception ex)
            {
                throw ex;
            }               
        }

        public string GetRefDocNo()
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_get_RefDocNo");
            cmd.CommandTimeout = CMD_TIMEOUT;
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count > 0)
            {
                string refDoc = DaHelper.GetString(dt.Rows[0], "MaxRefDoc");
                return refDoc;
            }
            else 
            {   
                string tempDate = DateTime.Now.AddDays(-1).ToString("yyyyMMdd", new CultureInfo("th-TH"));
                return String.Empty;
            }
        }

        public List<AccountClassInfo> GetAccountClassList(AccountClassInfo acParam)
        {          
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_sel_AccountClass");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "AccountClassId", DbType.String, acParam.AccountClassId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            List<AccountClassInfo> acList = new List<AccountClassInfo>();
            foreach (DataRow r in dt.Rows)
            {
                AccountClassInfo ac = new AccountClassInfo();
                ac.AccountClassId = DaHelper.GetString(r, "AccountClassId");
                ac.AccountClassName = DaHelper.GetString(r, "AccountClassName");
                acList.Add(ac);
            }

            return acList;
        }


        public List<Company> GetCompanyList(CompanyParamInfo acParam)
        {    
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_sel_ReportCompany");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "CompanyId", DbType.String, acParam.CompanyId == "" ? null : acParam.CompanyId);
            db.AddInParameter(cmd, "AccountClassId", DbType.String, acParam.AccountClassId == "0000" ? null : acParam.AccountClassId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            List<Company> acList = new List<Company>();
            foreach (DataRow r in dt.Rows)
            {
                Company ac = new Company();
                ac.CompanyId = DaHelper.GetString(r, "CompanyId");
                ac.CompanyName = DaHelper.GetString(r, "CompanyName");
                acList.Add(ac);
            }

            return acList;
        }


        public List<Company> GetCompanyByUploadDtList(DateTime? uploadDt)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_get_AgentByUploadDt");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "UploadDt", DbType.DateTime, uploadDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            List<Company> acList = new List<Company>();
            if (dt.Rows.Count > 0)
            {
                Company ac = new Company();
                ac.CompanyId = "0000";
                ac.CompanyName = "ทั้งหมด";
                acList.Add(ac);
            }
            foreach (DataRow r in dt.Rows)
            {
                Company ac = new Company();
                ac.CompanyId = DaHelper.GetString(r, "CompanyId");
                ac.CompanyName = DaHelper.GetString(r, "CompanyName");
                acList.Add(ac);
            }

            return acList;
        }


        #endregion

    }
}

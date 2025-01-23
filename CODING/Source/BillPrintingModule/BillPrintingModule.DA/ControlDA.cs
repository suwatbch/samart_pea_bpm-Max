using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
namespace PEA.BPM.BillPrintingModule.DA
{
    public class ControlDA
    {
        public List<DeliveryPlace> GetDeliveryPlace(string createBranchId)
        {
            List<DeliveryPlace> _deliveryPlace = new List<DeliveryPlace>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_get_DeliveryPlace");
            db.AddInParameter(cmd, "CreateBranchId", DbType.String, createBranchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                DeliveryPlace _dp = new DeliveryPlace();
                _dp.Id = DaHelper.GetString(dr, "Id");
                _dp.DeliveryBranchId = DaHelper.GetString(dr, "DeliveryBranchId");
                _dp.PlaceName = DaHelper.GetString(dr, "DeliveryPlaceName").Trim();
                _deliveryPlace.Add(_dp);
            }

            return _deliveryPlace;
        }

        public List<AuthorizedPerson> GetApprover(string createBranchId)
        {
            List<AuthorizedPerson> approverList = new List<AuthorizedPerson>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_get_Approver");
            db.AddInParameter(cmd, "CreateBranchId", DbType.String, createBranchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                AuthorizedPerson approver = new AuthorizedPerson();
                approver.ApproverId = DaHelper.GetString(dr, "ApproverId");
                approver.ApproverName = DaHelper.GetString(dr, "ApproverName");
                approver.Position = DaHelper.GetString(dr, "Position").Trim();
                approverList.Add(approver);
            }

            return approverList;
        }

        public List<Bank> GetBank(string branchId)
        {
            List<Bank> _bank = new List<Bank>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_get_Bank");
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Bank _bk = new Bank();
                _bk.BankKey = DaHelper.GetString(dr, "Bankkey");
                _bk.BankName = DaHelper.GetString(dr, "BankName");
                _bank.Add(_bk);
            }

            return _bank;
        }

        public string InsertDeliveryPlace(DbTransaction trans, DeliveryPlace dp)
        {
            //we should use transaction, plz see an example in Tool project.
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_DeliveryPlace");
            db.AddInParameter(cmd, "IdPrefix", DbType.String, dp.IdPrefix);
            db.AddInParameter(cmd, "DeliveryBranchId", DbType.String, dp.DeliveryBranchId);
            db.AddInParameter(cmd, "CreateBranchId", DbType.String, dp.CreateBranchId);
            db.AddInParameter(cmd, "DeliveryPlaceName", DbType.String, dp.PlaceName);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, dp.ModifiedBy);
            return db.ExecuteScalar(cmd, trans).ToString();
        }

        public string InsertApprover(DbTransaction trans, AuthorizedPerson approver)
        {
            //we should use transaction, plz see an example in Tool project.
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_Approver");
            db.AddInParameter(cmd, "IdPrefix", DbType.String, approver.IdPrefix);
            db.AddInParameter(cmd, "ApproverName", DbType.String, approver.ApproverName);
            db.AddInParameter(cmd, "Position", DbType.String, approver.Position);
            db.AddInParameter(cmd, "CreateBranchId", DbType.String, approver.CreateBranchId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, approver.ModifiedBy);
            return db.ExecuteScalar(cmd, trans).ToString();
        }

        public string UpdateDeliveryPlace(DbTransaction trans, DeliveryPlace dp)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_upd_DeliveryPlace");
            db.AddInParameter(cmd, "DeliveryPlaceId", DbType.String, dp.Id);
            db.AddInParameter(cmd, "DeliveryBranchId", DbType.String, dp.DeliveryBranchId);
            db.AddInParameter(cmd, "CreateBranchId", DbType.String, dp.CreateBranchId);
            db.AddInParameter(cmd, "DeliveryPlaceName", DbType.String, dp.PlaceName);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, dp.ModifiedBy);

            return db.ExecuteNonQuery(cmd, trans).ToString();
        }

        public string UpdateApprover(DbTransaction trans, AuthorizedPerson approver)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_upd_Approver");
            db.AddInParameter(cmd, "ApproverId", DbType.String, approver.ApproverId);
            db.AddInParameter(cmd, "ApproverName", DbType.String, approver.ApproverName);
            db.AddInParameter(cmd, "Position", DbType.String, approver.Position);
            db.AddInParameter(cmd, "CreateBranchId", DbType.String, approver.CreateBranchId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, approver.ModifiedBy);

            return db.ExecuteNonQuery(cmd, trans).ToString();
        }

        public string DeleteDeliveryPlace(DbTransaction trans, DeliveryPlace dp)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_del_DeliveryPlace");
            db.AddInParameter(cmd, "DeliveryPlaceId", DbType.String, dp.Id);
            db.AddInParameter(cmd, "CreateBranchId", DbType.String, dp.CreateBranchId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, dp.ModifiedBy);

            return db.ExecuteNonQuery(cmd, trans).ToString();
        }

        public string DeleteApprover(DbTransaction trans, AuthorizedPerson approver)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_del_Approver");
            db.AddInParameter(cmd, "ApproverId", DbType.String, approver.ApproverId);
            db.AddInParameter(cmd, "CreateBranchId", DbType.String, approver.CreateBranchId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, approver.ModifiedBy);

            return db.ExecuteNonQuery(cmd, trans).ToString();
        }

        public List<Portion> GetPortion(string branchId)
        {
            List<Portion> _pts = new List<Portion>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_get_Portion");
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);

            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Portion _pt = new Portion();
                _pt.PortionKey = Convert.ToInt32(dr["PortionKey"]).ToString();
                _pt.PortionNo = DaHelper.GetString(dr, "PortionNo");
                _pts.Add(_pt);
            }

            return _pts;
        }

        public List<Invoice> GetContractAccountHistory(string caId, string printBranch)
        {
            List<Invoice> invList = new List<Invoice>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_get_InvoiceHistory");
            db.AddInParameter(cmd, "pCaId", DbType.String, caId);
            db.AddInParameter(cmd, "pPrintBranch", DbType.String, printBranch);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Invoice obj = new Invoice();
                obj.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                obj.ReceiptNo = DaHelper.GetString(dr, "ReceiptNo");
                obj.W_01_print_doc = DaHelper.GetString(dr, "w_01_print_doc");
                obj.W_40_sname = DaHelper.GetString(dr, "w_40_sname").Trim();
                obj.CaName = DaHelper.GetString(dr, "CaName");
                obj.W_130_period = DaHelper.GetString(dr, "w_130_period");
                obj.W_150_cont_acct = DaHelper.GetString(dr, "w_150_cont_acct");
                obj.W_800_mr_kw_12_2_txt = DaHelper.GetString(dr, "w_800_mr_kw_12_2_txt").Trim();
                obj.W_790_mr_kw_12_1_txt = DaHelper.GetString(dr, "w_790_mr_kw_12_1_txt").Trim();
                obj.W_190_multi_n = DaHelper.GetString(dr, "w_190_multi_n").Trim();
                obj.PrintDt = DaHelper.GetDate(dr, "PrintDate");
                obj.RePrintDate = DaHelper.GetDate(dr, "RePrintDate");
                obj.PrintLog = DaHelper.GetInt(dr, "PrintLog");
                obj.W_1510_total_amnt_txt = DaHelper.GetString(dr, "w_1510_total_amnt_txt").Trim();
                obj.W_1830_payment_method = DaHelper.GetString(dr, "w_1830_payment_method");
                obj.W_1971_print_time = DaHelper.GetDate(dr, "w_1971_print_time");
                obj.W_171_ettat_code = DaHelper.GetString(dr, "w_171_ettat_code").Trim();
                obj.W_160_device = DaHelper.GetString(dr, "w_160_device").Trim();
                obj.W_180_voltage = DaHelper.GetString(dr, "w_180_voltage").Trim();
                obj.W_200_mr_date = DaHelper.GetString(dr, "w_200_mr_date").Trim();
                obj.W_1960_acct_class = DaHelper.GetString(dr, "w_1960_acct_class").Trim();
                obj.W_1980_spotbill = DaHelper.GetString(dr, "w_1980_spotbill").Trim();
                obj.W_1990_addition = DaHelper.GetString(dr, "w_1990_addition").Trim();
                obj.W_2000_dispctrl = DaHelper.GetString(dr, "w_2000_dispctrl").Trim();
                obj.W_2010_noprnt = DaHelper.GetString(dr, "w_2010_noprnt").Trim();
                obj.W_1910_org_doc = DaHelper.GetString(dr, "w_1910_org_doc").Trim();
                obj.ReverseCount = DaHelper.GetInt(dr, "ReverseCount");
                obj.Active = DaHelper.GetString(dr, "active").Trim();
                obj.W_1860_trsg = DaHelper.GetString(dr, "w_1860_trsg").Trim();
                obj.W_1861_crsg = DaHelper.GetString(dr, "w_1861_crsg").Trim();
                obj.W_10_doc_date = DaHelper.GetString(dr, "w_10_doc_date").Trim();
                
                invList.Add(obj);
            }

            return invList;
        }

        public List<BarcodeMRU> GetBarcodeMRU(ManageBarcodeParam param)
        {
            List<BarcodeMRU> list = new List<BarcodeMRU>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_get_BarcodeMRU");
            db.AddInParameter(cmd, "pBranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "pMruId", DbType.String, param.MruId);
            db.AddInParameter(cmd, "pToMruId", DbType.String, param.ToMruId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                BarcodeMRU obj = new BarcodeMRU();
                obj.BranchId = DaHelper.GetString(dr, "BranchId");
                obj.MruId = DaHelper.GetString(dr, "MruId");
                obj.IsPrinted = DaHelper.GetString(dr, "IsPrinted") == "0" ? false : true;
                list.Add(obj);
            }

            return list;
        }

        public void UpdateBarcodeMRU(DbTransaction trans, BarcodeMRU param, string reset)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_BarcodeMRU");
            db.AddInParameter(cmd, "pBranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "pMruId", DbType.String, param.MruId);
            db.AddInParameter(cmd, "pIsPrinted", DbType.String, param.IsPrinted ? "1" : "0");
            db.AddInParameter(cmd, "pModifiedBy", DbType.String, param.ModifiedBy);
            db.AddInParameter(cmd, "pReset", DbType.String, reset);
            db.ExecuteNonQuery(cmd, trans);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;

namespace PEA.BPM.PaymentManagementModule.DA
{
    public class PaymentMntDA
    {
        private int timeout = 120;

        #region Normal Function

        public string GetAPPmId(string branchId, string posId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ap_get_APPmId");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "posId", DbType.String, posId);
            return db.ExecuteScalar(cmd).ToString();
        }

        public string GetCaNameByPaymentVoucher(string caId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ap_get_CaNameByPaymentVoucher");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "CaId", DbType.String, caId);
            object caName = db.ExecuteScalar(cmd);

            if (caName == null)
            {
                return "";
            }
            else
            {
                return caName.ToString();
            }
        }

        public decimal? GetMoneyInTray(string workId)
        {
            TrayMoneyInfo trayMoneyInfo = new TrayMoneyInfo();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_sel_cashierWorkCash");
            db.AddInParameter(cmd, "WorkId", DbType.String, workId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            DataRow cashRow = dt.Rows[0];
            trayMoneyInfo.CashAmount = DaHelper.GetDecimal(cashRow, "TotalAmount");
            trayMoneyInfo.CashPendingAmount = DaHelper.GetDecimal(cashRow, "PendingAmount");

            return trayMoneyInfo.LeftAmount;
        }

        public List<APInfo> SearchPaidPaymentVoucher(string paymentVoucher)
        {
            List<APInfo> ap = new List<APInfo>();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ap_sel_APByPaymentVoucher");
            db.AddInParameter(cmd, "PaymentVoucher", DbType.String, paymentVoucher);
            cmd.CommandTimeout = timeout;
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                APInfo a = new APInfo();
                a.PaymentVoucher = DaHelper.GetString(dr, "PaymentVoucher");
                a.CaId = DaHelper.GetString(dr, "CaId");
                a.CaName = DaHelper.GetString(dr, "CaName");
                a.GAmount = DaHelper.GetDecimal(dr, "GAmount");

                ap.Add(a);
            }

            return ap;
        }

        public void InsertIntoAP(DbTransaction trans, string apPmId, string paymentVoucher, string caId, string caName, decimal? gAmount, decimal? adjAmount, int? apQty, DateTime paymentDate, string posId, string terminalCode, string cashierId, string cashierName, string branchId, string branchName, string postedServerId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ap_ins_AP");
            cmd.CommandTimeout = timeout;

            db.AddInParameter(cmd, "APPmId", DbType.String, apPmId);
            db.AddInParameter(cmd, "PaymentVoucher", DbType.String, paymentVoucher);
            db.AddInParameter(cmd, "CaId", DbType.String, caId);
            db.AddInParameter(cmd, "CaName", DbType.String, caName);
            db.AddInParameter(cmd, "GAmount", DbType.Decimal, gAmount);
            db.AddInParameter(cmd, "AdjAmount", DbType.Decimal, adjAmount);
            db.AddInParameter(cmd, "APQty", DbType.Int16, apQty);
            db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, paymentDate);
            db.AddInParameter(cmd, "CashierId", DbType.String, cashierId);
            db.AddInParameter(cmd, "CashierName", DbType.String, cashierName);
            db.AddInParameter(cmd, "PosId", DbType.String, posId);
            db.AddInParameter(cmd, "TerminalCode", DbType.String, terminalCode);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, cashierId);
            db.ExecuteNonQuery(cmd, trans);
        }

        public List<APEntity> UpdateAPByStrLineAPId(string strLineAPId, string reason, string cashierId, string cashierName)
        {
            List<APEntity> apEntity = new List<APEntity>();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ap_upd_CancelAPByStrLineAPId");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "StrLineAPId", DbType.String, strLineAPId);
            db.AddInParameter(cmd, "Reason", DbType.String, reason);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, cashierId);
            db.AddInParameter(cmd, "CancelCashierName", DbType.String, cashierName);

            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    APEntity r = new APEntity();
                    r.APId = DaHelper.GetString(dr, "APId");
                    r.APPmId = DaHelper.GetString(dr, "APPmId");
                    r.PaymentVoucher = DaHelper.GetString(dr, "PaymentVoucher");
                    r.CaId = DaHelper.GetString(dr, "CaId");
                    r.CaName = DaHelper.GetString(dr, "CaName");
                    r.GAmount = DaHelper.GetDecimal(dr, "GAmount");
                    r.AdjAmount = DaHelper.GetDecimal(dr, "AdjAmount");
                    r.PaymentDt = DaHelper.GetDateTime(dr, "PaymentDt");
                    r.CancelDt = DaHelper.GetDateTime(dr, "CancelDt");
                    r.CancelReason = DaHelper.GetString(dr, "CancelReason");
                    r.PosId = DaHelper.GetString(dr, "PosId");
                    r.CashierName = DaHelper.GetString(dr, "CashierName");
                    r.BranchId = DaHelper.GetString(dr, "BranchId");

                    apEntity.Add(r);

                }

            }

            return apEntity;
        }

        public List<APEntity> SearchPaymentVoucher(PaymentVoucherSearchParam param)
        {
            List<APEntity> apEntity = new List<APEntity>();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ap_sel_APByDetail");
            cmd.CommandTimeout = timeout;
            db.AddInParameter(cmd, "PaymentVoucher", DbType.String, param.PaymentVoucherId);
            db.AddInParameter(cmd, "CashierName", DbType.String, param.CashierName);
            db.AddInParameter(cmd, "CaId", DbType.String, param.CustomerId);
            db.AddInParameter(cmd, "CaName", DbType.String, param.CustomerName);
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "APPmId", DbType.String, param.APPmId);

            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    APEntity r = new APEntity();
                    r.APId = DaHelper.GetString(dr, "APId");
                    r.APPmId = DaHelper.GetString(dr, "APPmId");
                    r.PaymentVoucher = DaHelper.GetString(dr, "PaymentVoucher");
                    r.CaId = DaHelper.GetString(dr, "CaId");
                    r.CaName = DaHelper.GetString(dr, "CaName");
                    r.GAmount = DaHelper.GetDecimal(dr, "GAmount");
                    r.AdjAmount = DaHelper.GetDecimal(dr, "AdjAmount");
                    r.APQty = DaHelper.GetInt(dr, "APQty");
                    r.PaymentDt = DaHelper.GetDateTime(dr, "PaymentDt");
                    r.CancelDt = DaHelper.GetDateTime(dr, "CancelDt");
                    r.CancelReason = DaHelper.GetString(dr, "CancelReason");
                    r.PosId = DaHelper.GetString(dr, "PosId");
                    r.CashierName = DaHelper.GetString(dr, "CashierName");
                    r.BranchId = DaHelper.GetString(dr, "BranchId"); 

                    apEntity.Add(r);
                    
                }

            }

            return apEntity;
        }

        #endregion

    }
}
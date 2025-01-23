using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities.Reports;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.PaymentManagementModule.DA
{
    public class ReportDA
    {

        private int timeout = 120;

        public List<APReport> GetReportAP(APParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("ReportDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ap_sel_RpAP");
            cmd.CommandTimeout = timeout;

            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "PosId", DbType.String, param.posId);
            db.AddInParameter(cmd, "CashierId", DbType.String, param.cashierId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, param.TransFromDate);
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, param.TransToDate);
            DataSet ds = db.ExecuteDataSet(cmd);

            List<APReport> report = new List<APReport>();

            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                APReport detail = new APReport();
                detail.BranchId = DaHelper.GetString(dr, "BranchId");
                detail.PaymentVoucher = DaHelper.GetString(dr, "PaymentVoucher");
                detail.CaId = DaHelper.GetString(dr, "CaId");
                detail.CaName = DaHelper.GetString(dr, "CaName");
                detail.PosId = DaHelper.GetString(dr, "PosId");
                detail.PosName = DaHelper.GetString(dr, "PosName");

                detail.CashierId = DaHelper.GetString(dr, "CashierId");
                detail.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                detail.GAmount = DaHelper.GetDecimal(dr, "GAmount");
                detail.AdjAmount = DaHelper.GetDecimal(dr, "AdjAmount");

                detail.CancelDt = DaHelper.GetDate(dr, "CancelDt");
                detail.CancelReason = DaHelper.GetString(dr, "CancelReason");
                detail.CancelFlag = DaHelper.GetInt(dr, "CancelFlag") == 1 ? true : false;
                report.Add(detail);
            }

            return report;
        }

    }
}

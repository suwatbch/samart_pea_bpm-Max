using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities.Reports;
using System.Data;

namespace BPMLINQReport
{
    public class PAMReport
    {
        public List<APReport> GetReportPAM33(string pBranchId, string pPosId, string pCashierId, DateTime? pFromDate, DateTime? pToDate)
        {
            List<APReport> report = new List<APReport>();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ap_sel_RpAPX");
            cmd.CommandTimeout = 360;
            db.AddInParameter(cmd, "BranchId", DbType.String, pBranchId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, pFromDate);
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, pToDate);
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable rawData = ds.Tables[0];

            var repData = from rep in rawData.AsEnumerable()
                          select new
                          {
                              RefNo = rep.Field<string>("RefNo"),
                              CaId = rep.Field<string>("CaId"),
                              CaName = rep.Field<string>("CaName"),
                              PosId = rep.Field<string>("PosId"),
                              TerminalCode = rep.Field<string>("TerminalCode"),
                              CashierId = rep.Field<string>("CashierId"),
                              CashierFull = rep.Field<string>("CashierFull"),
                              PaymentDt = rep.Field<DateTime?>("PaymentDt"),
                              CashAmount = rep.Field<decimal?>("CashAmount"),
                              AdjAmount = rep.Field<decimal?>("AdjAmount"),
                              BranchId = rep.Field<string>("BranchId"),
                              CancelDt = rep.Field<DateTime?>("CancelDt"),
                              CancelReason = rep.Field<string>("CancelReason"),
                              CancelCashierId = rep.Field<string>("CancelCashierId"),
                              CancelCashierName = rep.Field<string>("CancelCashierName")
                          };

            var result = from r in repData
                         where r.CashierId == (pCashierId == null ? r.CashierId : pCashierId)
                                     && r.PosId == (pPosId == null ? r.PosId : pPosId)
                         orderby r.PaymentDt, r.RefNo
                         select new
                         {
                             PaymentVoucher = r.RefNo,
                             r.CaId,
                             CaName = r.CaName.Trim(),
                             r.PosId,
                             PosName = r.TerminalCode,
                             CashierId = r.CashierFull,
                             r.PaymentDt,
                             GAmount = r.CancelDt == null ? r.CashAmount : r.CashAmount * (-1),
                             AdjAmount = r.CancelDt == null ? r.AdjAmount : r.AdjAmount * (-1),
                             r.BranchId,
                             r.CancelDt,
                             CancelReason = r.CancelDt == null ? null : r.CancelReason + " / " + Convert.ToInt32(r.CancelCashierId).ToString() + " - " + r.CancelCashierName,
                             CancelFlag = r.CancelDt == null ? false : true
                         };

            foreach (var q in result)
            {
                APReport detail = new APReport();
                detail.BranchId = q.BranchId;
                detail.PaymentVoucher = q.PaymentVoucher;
                detail.CaId = q.CaId;
                detail.CaName = q.CaName;
                detail.PosId = q.PosId;
                detail.PosName = q.PosName;

                detail.CashierId = q.CashierId;
                detail.PaymentDt = q.PaymentDt;
                detail.GAmount = q.GAmount;
                detail.AdjAmount = q.AdjAmount;

                detail.CancelDt = q.CancelDt;
                detail.CancelReason = q.CancelReason;
                detail.CancelFlag = q.CancelFlag;
                report.Add(detail);
            }

            return report;
        }
    }
}

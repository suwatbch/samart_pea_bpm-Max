using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentManagementModule.Interface.Services;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities;
using System.Threading;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PEA.BPM.PaymentManagementModule.DA;
using System.Data.SqlClient;
using System.Data;
using PEA.BPM.PaymentManagementModule.Interface.Constants;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities.Reports;
using System.Configuration;
using System.Globalization;
using System.Collections;


namespace PEA.BPM.PaymentManagementModule.BS
{
    public class PaymentMntBS : IPaymentMntService
    {
        #region IPaymentMntService Members


        public string GetCaNameByPaymentVoucher(string caId)
        {
            try
            {
                PaymentMntDA da = new PaymentMntDA();
                return da.GetCaNameByPaymentVoucher(caId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public decimal? GetMoneyInTray(string workId)
        {
            try
            {
                PaymentMntDA da = new PaymentMntDA();
                return da.GetMoneyInTray(workId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public List<APInfo> SearchPaidPaymentVoucher(string paymentVoucher)
        {
            try
            {
                PaymentMntDA da = new PaymentMntDA();
                return da.SearchPaidPaymentVoucher(paymentVoucher);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public bool PayAP(List<APInfo> ap, DateTime paymentDate, string posId, string terminalCode, string cashierId, string cashierName, string branchId, string branchName)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbTransaction trans;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                try
                {
                    PaymentMntDA da = new PaymentMntDA();

                    string postedServerId = Session.Branch.PostedServerId;
                    string apPmId = da.GetAPPmId(branchId, posId);

                    foreach (APInfo a in ap)
                    {
                        da.InsertIntoAP(trans, apPmId, a.PaymentVoucher, a.CaId, a.CaName, a.GAmount, a.AdjAmount, a.APQty, paymentDate, posId, terminalCode, cashierId, cashierName, branchId, branchName, postedServerId);
                    }

                    trans.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Logger.WriteError(Logger.Module.POS, "PayAP", ex.ToString());
                    return false;
                }
            }
        }


        public List<APEntity> SearchPaymentVoucher(PaymentVoucherSearchParam param)
        {
            try
            {
                PaymentMntDA da = new PaymentMntDA();

                return da.SearchPaymentVoucher(param);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<APEntity> UpdateAPByStrLineAPId(string strLineAPId, string reason, string cashierId, string cashierName)
        {
            try
            {
                PaymentMntDA da = new PaymentMntDA();

                return da.UpdateAPByStrLineAPId(strLineAPId, reason, cashierId, cashierName);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

    }
}

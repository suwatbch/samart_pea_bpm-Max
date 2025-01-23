using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentManagementModule.Interface.Services;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities;
using System.Threading;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Data.Common;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities.Reports;
using PEA.BPM.PaymentManagementModule.SG;

namespace PEA.BPM.PaymentManagementModule.Services
{
    [Service(typeof(IPaymentMntService))]
    public class PaymentMntService : IPaymentMntService
    {
        public PaymentMntService()
		{

        }

        #region Service Factory
        public IPaymentMntService GetService()
        {
            return GetService(false);
        }

        public IPaymentMntService GetService(bool serverService)
        {
            if (serverService || Session.Branch.OnlineConnection)
            {
                return new PaymentMntSG(true);
            }
            else
            {
                return new PaymentMntSG(false);
            }
        }
        #endregion

        #region IPaymentMntService Members

        public string GetCaNameByPaymentVoucher(string caId)
        {
            return GetService().GetCaNameByPaymentVoucher(caId);
        }

        public bool PayAP(List<APInfo> ap, DateTime paymentDate, string posId, string terminalCode, string cashierId, string cashierName, string branchId, string branchName)
        {
            return GetService().PayAP(ap, paymentDate, posId, terminalCode, cashierId, cashierName, branchId, branchName);
        }

        public decimal? GetMoneyInTray(string workId)
        {
            return GetService().GetMoneyInTray(workId);
        }

        public List<APInfo> SearchPaidPaymentVoucher(string paymentVoucher)
        {
            return GetService().SearchPaidPaymentVoucher(paymentVoucher);
        }

        public List<APEntity> SearchPaymentVoucher(PaymentVoucherSearchParam param)
        {
            IPaymentMntService bs = GetService();

            return bs.SearchPaymentVoucher(param);
        }

        public List<APEntity> UpdateAPByStrLineAPId(string strLineAPId, string reason, string cashierId, string cashierName)
        {
            IPaymentMntService bs = GetService();

            return bs.UpdateAPByStrLineAPId(strLineAPId, reason, cashierId, cashierName);
        }
        #endregion
    }
}

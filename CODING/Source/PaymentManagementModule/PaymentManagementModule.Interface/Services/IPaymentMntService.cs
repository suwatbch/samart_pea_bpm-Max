using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities.Reports;
using System.Data;
using System.Data.Common;

namespace PEA.BPM.PaymentManagementModule.Interface.Services
{
    public interface IPaymentMntService
    {
        string GetCaNameByPaymentVoucher(string caId);
        decimal? GetMoneyInTray(string workId);
        bool PayAP(List<APInfo> ap, DateTime paymentDate, string posId, string terminalCode, string cashierId, string cashierName, string branchId, string branchName);
        List<APInfo> SearchPaidPaymentVoucher(string paymentVoucher);      
        List<APEntity> SearchPaymentVoucher(PaymentVoucherSearchParam param);
        List<APEntity> UpdateAPByStrLineAPId(string strLineAPId, string reason, string cashierId, string cashierName);
    }
}

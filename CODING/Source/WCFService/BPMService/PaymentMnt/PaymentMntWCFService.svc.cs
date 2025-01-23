using System;
using System.Collections.Generic;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.PaymentManagementModule.BS;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities;
using PEA.BPM.WebService;
using WCFExtras.Soap;


namespace BPMService.PaymentMnt
{

    public class PaymentMntWCFService : IPaymentMntWCFService
    {
        private PaymentMntBS _bs;
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public PaymentMntWCFService()
        {
            _bs = new PaymentMntBS();
        }

        public CompressData GetMoneyInTray_Compress(string workId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POSจ่ายเงิน, authInfo, "PaymentMntWCFService", "GetMoneyInTray_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<decimal?>(_bs.GetMoneyInTray(workId));
                });
        }

        public CompressData GetCaNameByPaymentVoucher_Compress(string caId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POSจ่ายเงิน, authInfo, "PaymentMntWCFService", "GetCaNameByPaymentVoucher_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<string>(_bs.GetCaNameByPaymentVoucher(caId));
                });
        }

        public CompressData SearchPaidPaymentVoucher_Compress(string paymentVoucher)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POSจ่ายเงิน, authInfo, "PaymentMntWCFService", "SearchPaidPaymentVoucher_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<APInfo>>(_bs.SearchPaidPaymentVoucher(paymentVoucher));
                });
        }

        public CompressData PayAP_Compress(List<APInfo> ap, DateTime paymentDate, string posId, string terminalCode, string cashierId, string cashierName, string branchId, string branchName)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POSจ่ายเงิน, authInfo, "PaymentMntWCFService", "PayAP_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<bool>(_bs.PayAP(ap, paymentDate, posId, terminalCode, cashierId, cashierName, branchId, branchName));
                });
        }

        public CompressData SearchPaymentVoucher(PaymentVoucherSearchParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POSจ่ายเงิน, authInfo, "PaymentMntWCFService", "SearchPaymentVoucher",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<APEntity>>(_bs.SearchPaymentVoucher(param));
                });
        }

        public CompressData UpdateAPByStrLineAPId(string strLineAPId, string reason, string cashierId, string cashierName)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POSจ่ายเงิน, authInfo, "PaymentMntWCFService", "UpdateAPByStrLineAPId",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<APEntity>>(_bs.UpdateAPByStrLineAPId(strLineAPId, reason, cashierId, cashierName));
                });

        }
    }
}

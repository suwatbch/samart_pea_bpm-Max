using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ComponentModel;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using WCFExtras.Soap;


namespace BPMService.PaymentMnt
{
    [SoapHeaders, ServiceContract(Namespace = "http://tempuri.org")]
    public interface IPaymentMntWCFService
    {
        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetMoneyInTray_Compress(string workId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetCaNameByPaymentVoucher_Compress(string caId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData SearchPaidPaymentVoucher_Compress(string paymentVoucher);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData PayAP_Compress(List<APInfo> ap, DateTime paymentDate, string posId, string terminalCode, string cashierId, string cashierName, string branchId, string branchName);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData SearchPaymentVoucher(PaymentVoucherSearchParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData UpdateAPByStrLineAPId(string strLineAPId, string reason, string cashierId, string cashierName);

    }
}

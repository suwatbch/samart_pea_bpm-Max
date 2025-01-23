using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ComponentModel;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using WCFExtras.Soap;


namespace BPMService.ePayment
{
    [SoapHeaders, ServiceContract(Namespace = "http://tempuri.org")]
    public interface IEPaymentWCFService
    {
        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        string VerifyContractorService(string caId, string period, decimal debtAmount);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<SearchContractorInfo> SearchContractorService(string caId, string billFlag);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<SearchConAccountInfo> SearchConAccountService(string caId);

    }
}

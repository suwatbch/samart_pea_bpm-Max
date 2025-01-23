using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ComponentModel;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities.Reports;
using PEA.BPM.Architecture.CommonUtilities;
using WCFExtras.Soap;


namespace BPMReportService.PaymentMnt
{
    [SoapHeaders, ServiceContract(Namespace = "http://tempuri.org")]
    public interface IAPReportWCFService
    {
        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetReportAP_Compress(APParam param);

    }
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ComponentModel;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using WCFExtras.Soap;


namespace BPMService.ARCH
{
    [SoapHeaders, ServiceContract(Namespace = "http://tempuri.org")]
    public interface IErrorHandlingWCFService
    {
        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        ExceptionDebugInfo ReportAndSaveException(BPMApplicationExceptionInfo excpetioninfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        void AcknowledgeException(string debuggingid, string updatestacktrace);

    }
}

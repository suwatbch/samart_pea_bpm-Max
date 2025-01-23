using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ComponentModel;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;


namespace BPMService.ARCH
{
    [ServiceContract(Namespace = "http://tempuri.org")]
    public interface INotificationWCFService
    {
        [OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        void SendNotificationToEmail(string _errorId, string _errorMsg, int _severity, string _jobId, string _interfaceId, int _row, string _branchId, string _suggMsg);
    }
}

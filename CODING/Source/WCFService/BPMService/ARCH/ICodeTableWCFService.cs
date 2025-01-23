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
    public interface ICodeTableWCFService
    {
        [OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetUpdatedData(DateTime lastModifiedDt, string branchId);
    }
}

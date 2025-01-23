using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ComponentModel;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;


namespace BPMService.ARCH
{
    [ServiceContract(Namespace = "http://tempuri.org")]
    public interface ISecurityWCFService
    {
        [OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData ListAuthorizedFunctions(string userId);

        [OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        void SaveUnlockLog(UnlockLog unlockLog);

        [OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        UserProfileExt LoadUserProfile(string userId);

        [OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        string CheckLogInDouble(string UserId, string TerminalIP, int latency, int retrycount);

        [OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        string UpdateCurIPReqFlag(string UserId, string TerminalIP, string ReqFlag);
    }
}

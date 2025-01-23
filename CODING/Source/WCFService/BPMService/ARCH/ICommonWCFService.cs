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
    public interface ICommonWCFService
    {
        [OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        string IsAuthenticated(string userId, string hashPwd, string localip);

        [OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        string IsAuthenticatedInBranch(string userId, string hashPwd, string localip, string regBranchId);

        [OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        string GetToken(string userId, string hashPwd);

        [OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        DateTime GetServerTime();

        [OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        BPMVersion GetVersion();

        [OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        void SignalExport(string batchName, string branchId, string modifiedBy);

        [OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        void SignalExportBillBook(string batchName, string billBookId, string modifiedBy);

        [OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        void SignalSyncup(string batchName);

        [OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        WorkStatus IsForcedToCloseWork(string workId);

        [OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        RegisterProfile LoadRegisterationInfo(string branchId);

        // 2.0.7.4  Add parameter userId 
        [OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        void UpdateBPMClientVersion(string versionNo,string terminalId,string userId);

        //////////// DCR 67-020 : StrongPassword ////////////
        [OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        string ISOCheckPasswordExpried(string userId, int type);

        [OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        string ISOCheckPasswordHistory(string userId, string password);

        [OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        int UpdateUser(string userId, string password, int PwdState, string oldpassword);

        [OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        string SendEmail(string email, string pw4, string userEmail, string passEmail);
        //////////// DCR 67-020 : StrongPassword ////////////
    }
}

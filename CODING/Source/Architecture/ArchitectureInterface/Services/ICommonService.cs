using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;

namespace PEA.BPM.Architecture.ArchitectureInterface.Services
{
    public interface ICommonService
    {
        /// <summary>
        /// Check authentication of user
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="hashPwd">hashPwd</param>
        /// <returns>valid user or not</returns>
        string IsAuthenticated(string userId, string hashPwd, string localip);

        /// <summary>
        /// Verify user and registered branch
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="hashPwd"></param>
        /// <param name="localip"></param>
        /// <param name="regBranchId"></param>
        /// <returns></returns>
        string IsAuthenticated(string userId, string hashPwd, string localip, string regBranchId);

        /// <summary>
        /// Get token for calling webservice
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="hashPwd">hashPwd</param>
        /// <returns>token</returns>
        string GetToken(string userId, string hashPwd);

        /// <summary>
        /// To get current time of the server to set client app time
        /// </summary>
        /// <returns></returns>
        DateTime GetServerTime();

        /// <summary>
        /// To get current BPM's version
        /// </summary>
        /// <returns></returns>
        BPMVersion GetVersion();

        /// <summary>
        /// Sync batch name up to center
        /// </summary>
        /// <returns></returns>
        void SignalSyncup(string batchName);

        /// <summary>
        /// signal to export file to SAP
        /// </summary>
        /// <returns></returns>
        void SignalExport(string batchName, string branchId, string modifiedBy);

        /// <summary>
        /// signal to export BillBook file to SAP
        /// </summary>
        /// <returns></returns>
        void SignalExportBillBook(string batchName, string billBookId, string modifiedBy);

        /// <summary>
        /// validate workId 
        /// </summary>
        /// <returns></returns>
        WorkStatus IsForcedToCloseWork(string workId);
        
        /// <summary>
        /// Update registeration information on start up
        /// </summary>
        /// <returns></returns>
        RegisterProfile LoadRegisterationInfo(string branchId);

        /// <summary>
        /// Update version no from bpmclient.
        /// 2.0.7.4 Add paramter on interface
        /// </summary>
        /// <param name="versionNo"></param>
        void UpdateBPMClientVersion(string versionNo, string terminalId, string userId);

        //////////// DCR 67-020 : StrongPassword ////////////
        string ISOCheckPasswordExpried(string userId, int type);
        string ISOCheckPasswordHistory(string userId, string password);
        int UpdateUser(string userId, string password, int PwdState, string oldpassword);
        string EmpGetEmail(string userId, string url, int timeout);
        string SendEmail(string email, string pw4, string userEmail, string passEmail);
        //////////// DCR 67-020 : StrongPassword ////////////

    }
}

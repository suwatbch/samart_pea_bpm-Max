using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using System.Data.Common;

namespace PEA.BPM.Architecture.ArchitectureInterface.Services
{
    public interface ISecurityService
    {
        /// <summary>
        /// Check whehter token is valid or not
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        string CheckToken(string userId, string token);

        /// <summary>
        /// Get LogIn Status for Double LogIn Case
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="TerminalId">TerminalId</param>
        /// <param name="latency">Network latency</param>
        /// <param name="retrycount">GetServerTime retry counter</param>
        /// <returns>LogInStatus</returns>
        string CheckLogInDouble(string UserId, string TerminalIP, int latency,int retrycount);

        /// <summary>
        /// Update current IP and request status of each UserId
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="TerminalId">TerminalId</param>
        /// <param name="ReqFlag">ReqFlag</param>
        /// <returns>LogInStatus</returns>
        string UpdateCurIPReqFlag(string UserId, string TerminalIP, string ReqFlag);

        UserProfileExt LoadUserProfile(string userId);
        List<Function> ListAuthorizedFunctions(string userId);
        List<Service> ListAuthorizedServices(string userId);
        void SaveUnlockLog(DbTransaction trans, UnlockLog unlockLog);
    }
}

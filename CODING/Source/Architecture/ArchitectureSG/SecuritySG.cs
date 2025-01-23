using System;
using System.Collections.Generic;
using System.Data.Common;
using System.ServiceModel;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using PEA.BPM.Architecture.ArchitectureSG;
using PEA.BPM.Architecture.ArchitectureSG.SecurityWCF;
using PEA.BPM.Architecture.CommonUtilities;

namespace ArchitectureSG
{
    public class SecuritySG: ISecurityService
    {
        private static string _centerabsoluteuri = "";
        private static string _branchabsoluteuri = "";
        private static bool _doesinit = false;

        #region Singleton
        private static readonly object _syncroot = new object();
        private static volatile SecuritySG _centerinstance;
        private static volatile SecuritySG _branchinstance;
        private SecuritySG()
        {
        }
        public static SecuritySG Instance(bool onlineConnection)
        {
            if (onlineConnection)
            {
                if (_centerinstance == null)
                {
                    lock (_syncroot) // for thread safe
                    {
                        if (_centerinstance == null) _centerinstance = new SecuritySG { _isonline = true };
                    }
                }
                return _centerinstance;
            }
            else
            {
                if (_branchinstance == null)
                {
                    lock (_syncroot) // for thread safe
                    {
                        if (_branchinstance == null) _branchinstance = new SecuritySG { _isonline = false };
                    }
                }
                return _branchinstance;
            }
        }
        #endregion

        private bool _isonline;

        private SecurityWCFServiceClient GetServiceInstance()
        {
            if (!_doesinit) // init แค่ครั้งเดียวพอ
            {
                using (SecurityWCFServiceClient initws = new SecurityWCFServiceClient())
                {
                    string absoluteUri = initws.Endpoint.Address.Uri.AbsoluteUri;

                    if (_isonline)
                        _centerabsoluteuri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Center);
                    else
                        _branchabsoluteuri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Branch);
                }
                _doesinit = true;
            }

            SecurityWCFServiceClient ws;
            if (_isonline)
            {
                ws = new SecurityWCFServiceClient("BasicHttpBinding_ISecurityWCFService", new EndpointAddress(_centerabsoluteuri));
                if (Session.User.Token.Center == null) Session.User.Token.Center = CommonSG.Instance(_isonline).GetToken(Session.User.Id, Session.User.Pwd);
            }
            else
            {
                ws = new SecurityWCFServiceClient("BasicHttpBinding_ISecurityWCFService", new EndpointAddress(_branchabsoluteuri));
                if (Session.User.Token.Branch == null) Session.User.Token.Branch = CommonSG.Instance(_isonline).GetToken(Session.User.Id, Session.User.Pwd);
            }            
            ws.InnerChannel.OperationTimeout = new TimeSpan(0, 1, 0);
            return ws;
        }

        public string CheckLogInDouble(string UserId, string TerminalIP, int latency, int retrycount)
        {
            try
            {
                using (var ws = GetServiceInstance())
                {
                    return ws.CheckLogInDouble(UserId, TerminalIP, latency, retrycount);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Security, ex);
            }
        }

        public string UpdateCurIPReqFlag(string UserId, string TerminalIP, string ReqFlag)
        {
            try
            {
                using (var ws = GetServiceInstance())
                {
                    return ws.UpdateCurIPReqFlag(UserId, TerminalIP, ReqFlag);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Security, ex);
            }
        }

        public string CheckToken(string userId, string token)
        {
            throw new Exception("The method or operation is not implemented for client.");
        }

        public UserProfileExt LoadUserProfile(string userId)
        {
            try
            {
                using (var ws = GetServiceInstance())
                {
                    return ws.LoadUserProfile(userId);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Security, ex);
            }
        }

        public List<Function> ListAuthorizedFunctions(string userId)
        {
            try
            {
                using (var ws = GetServiceInstance())
                {
                    return ServiceHelper.DecompressData<List<Function>>(ws.ListAuthorizedFunctions(userId));
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Security, ex);
            }
        }

        public List<Service> ListAuthorizedServices(string userId)
        {
            throw new Exception("The method can't be used at the client side.");
        }

        public void SaveUnlockLog(DbTransaction trans, UnlockLog unlockLog)
        {
            try
            {
                using (var ws = GetServiceInstance())
                {
                    ws.SaveUnlockLog(unlockLog);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Security, ex);
            }
        }

    }
}

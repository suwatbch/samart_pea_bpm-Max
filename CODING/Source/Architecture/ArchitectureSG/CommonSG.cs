using System;
using System.ServiceModel;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using PEA.BPM.Architecture.ArchitectureSG.CommonWCF;
using PEA.BPM.Architecture.CommonUtilities;
using ArchitectureSG;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Net;

namespace PEA.BPM.Architecture.ArchitectureSG
{
    public class CommonSG: ICommonService
    {
        private static string _centerabsoluteuri = "";
        private static string _branchabsoluteuri = "";
        private static bool _doesinit = false;

        #region Singleton
        private static readonly object _syncroot = new object();
        private static volatile CommonSG _centerinstance;
        private static volatile CommonSG _branchinstance;
        private CommonSG()
        {
        }
        public static CommonSG Instance(bool onlineConnection)
        {
            if (onlineConnection)
            {
                if (_centerinstance == null)
                {
                    lock (_syncroot) // for thread safe
                    {
                        if (_centerinstance == null) _centerinstance = new CommonSG { _isonline = true };
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
                        if (_branchinstance == null) _branchinstance = new CommonSG { _isonline = false };
                    }
                }
                return _branchinstance;
            }
        }
        #endregion

        private bool _isonline;

        private CommonWCFServiceClient GetServiceInstance()
        {
            if (!_doesinit) // init แค่ครั้งเดียวพอ
            {
                using (CommonWCFServiceClient initws = new CommonWCFServiceClient())
                {
                    string absoluteUri = initws.Endpoint.Address.Uri.AbsoluteUri;

                    if (_isonline)
                        _centerabsoluteuri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Center);
                    else
                        _branchabsoluteuri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Branch);
                }
                _doesinit = true;
            }

            CommonWCFServiceClient ws;
            if (_isonline) 
                ws = new CommonWCFServiceClient("BasicHttpBinding_ICommonWCFService", new EndpointAddress(_centerabsoluteuri));
            else 
                ws = new CommonWCFServiceClient("BasicHttpBinding_ICommonWCFService", new EndpointAddress(_branchabsoluteuri));
            ws.InnerChannel.OperationTimeout = new TimeSpan(0, 1, 0);
            return ws;
        }

        public RegisterProfile LoadRegisterationInfo(string branchId)
        {
            try
            {
                //acquire for backup line
                CommonWCFServiceClient wsClient = new CommonWCFServiceClient();
                string absoluteUri = wsClient.Endpoint.Address.Uri.AbsoluteUri;
                string uri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.CenterBackup);

                using(CommonWCFServiceClient ws = new CommonWCFServiceClient("BasicHttpBinding_ICommonWCFService", new EndpointAddress(uri)))
                {
                    ws.InnerChannel.OperationTimeout = new TimeSpan(0, 1, 0);
                    return ws.LoadRegisterationInfo(branchId);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Architecture, ex);
            }
        }

        public string IsAuthenticated(string userId, string hashPwd, string localip)
        {
            try
            {
                CommonWCFServiceClient wsClient = new CommonWCFServiceClient();
                string absoluteUri = wsClient.Endpoint.Address.Uri.AbsoluteUri;
                string uri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.CenterBackup);

                using (CommonWCFServiceClient ws = new CommonWCFServiceClient("BasicHttpBinding_ICommonWCFService", new EndpointAddress(uri)))
                {
                    ws.InnerChannel.OperationTimeout = new TimeSpan(0, 2, 0);
                    return ws.IsAuthenticated(userId, hashPwd, localip);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Architecture, ex);
            }
        }

        public string IsAuthenticated(string userId, string hashPwd, string localip, string regBranchId)
        {
            try
            {
                using (var ws = GetServiceInstance())
                {
                    ws.InnerChannel.OperationTimeout = new TimeSpan(0, 2, 0);
                    return ws.IsAuthenticatedInBranch(userId, hashPwd, localip, regBranchId);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Architecture, ex);
            }
        }

        public string GetToken(string userId, string hashPwd)
        {
            try
            {
                using (var ws = GetServiceInstance())
                {
                    return ws.GetToken(userId, hashPwd);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Architecture, ex);
            }
        }

        public DateTime GetServerTime()
        {
            try
            {
                using (var ws = GetServiceInstance())
                {
                    //ws.InnerChannel.OperationTimeout = new TimeSpan(0, 0, 10);
                    return ws.GetServerTime();
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Architecture, ex);
            }
        }

        public BPMVersion GetVersion()
        {
            try
            {
                using (var ws = GetServiceInstance())
                {
                    //ws.InnerChannel.OperationTimeout = new TimeSpan(0, 0, 5);
                    return ws.GetVersion();
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Architecture, ex);
            }
        }

        public void SignalSyncup(string batchName)
        {
            try
            {
                using (var ws = GetServiceInstance())
                {
                    ws.SignalSyncup(batchName);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Architecture, ex);
            }
        }

        public void SignalExport(string batchName, string branchId, string modifiedBy)
        {
            try
            {
                using (var ws = GetServiceInstance())
                {
                    ws.SignalExport(batchName, branchId, modifiedBy);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Architecture, ex);
            }
        }

        public void SignalExportBillBook(string batchName, string billBookId, string modifiedBy)
        {
            try
            {
                using (var ws = GetServiceInstance())
                {
                    ws.SignalExportBillBook(batchName, billBookId, modifiedBy);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Architecture, ex);
            }
        }

        public WorkStatus IsForcedToCloseWork(string workId)
        {
            try
            {
                using (var ws = GetServiceInstance())
                {
                    return ws.IsForcedToCloseWork(workId);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Architecture, ex);
            }
        }


        #region ICommonService Members

        // 2.0.7.4 add paramter userId 
        public void UpdateBPMClientVersion(string versionNo, string terminalId,string userId)
        {
            try
            {
                using (var ws = GetServiceInstance())
                {
                    // Add parameter userId
                    ws.UpdateBPMClientVersion(versionNo,terminalId,userId);
                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        public string ISOCheckPasswordExpried(string userId, int type)
        {
            try
            {
                using (var ws = GetServiceInstance())
                {
                    return ws.ISOCheckPasswordExpried(userId,type);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Architecture, ex);
            }
        }

        public string ISOCheckPasswordHistory(string userId, string password)
        {
            try
            {
                using (var ws = GetServiceInstance())
                {
                    return ws.ISOCheckPasswordHistory(userId, password);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Architecture, ex);
            }
        }

        public int UpdateUser(string userId, string password, int PwdState, string oldpassword)
        {
            try
            {
                using (var ws = GetServiceInstance())
                {
                    return ws.UpdateUser(userId, password, PwdState, oldpassword);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Architecture, ex);
            }
        }

        public string EmpGetEmail(string userId , string url , int timeout)
        {
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                var PeaEmpDataWebService = new PeaEmpDataWebService.PeaGetData();
                PeaEmpDataWebService.Url = url;
                try
                {
                    PeaEmpDataWebService.Timeout = timeout;
                }
                catch
                {
                    PeaEmpDataWebService.Timeout = 6000;
                }
                return PeaEmpDataWebService.GetData(userId);
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Architecture, ex);
            }
        }

        public string SendEmail(string email, string pw4, string userEmail, string passEmail)
        {
            try
            {
                using (var ws = GetServiceInstance())
                {
                    return ws.SendEmail(email,pw4,userEmail,passEmail);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Architecture, ex);
            }
        }
    }
}

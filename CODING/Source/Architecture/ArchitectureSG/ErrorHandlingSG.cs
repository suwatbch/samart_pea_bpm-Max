using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using PEA.BPM.Architecture.ArchitectureSG;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureSG.ErrorHandlingWCF;
using System.ServiceModel;
using WCFExtras.Soap;

namespace ArchitectureSG
{
    public class ErrorHandlingSG : IErrorHandlingService
    {
        private static string _centerabsoluteuri = "";
        private static bool _doseinit = false;

        #region Singleton
        private static readonly object _syncroot = new object();
        private static volatile ErrorHandlingSG _instance;
        private ErrorHandlingSG()
        {
        }
        public static ErrorHandlingSG Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncroot)
                    {
                        if (_instance == null) _instance = new ErrorHandlingSG();
                    }
                }
                return _instance;
            }
        }
        #endregion
        
        private ErrorHandlingWCFServiceClient GetServiceInstance()
        {
            if (!_doseinit)
            {
                using (ErrorHandlingWCFServiceClient initws = new ErrorHandlingWCFServiceClient())
                {
                    string absoluteUri = initws.Endpoint.Address.Uri.AbsoluteUri;

                    _centerabsoluteuri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Center);
                }
                _doseinit = true;
            }

            //-- ส่ง data ไปที่ center เท่านั้น ไม่ว่าจะใช้ Branch หรือไม่ใช้ Branch
            ErrorHandlingWCFServiceClient ws = new ErrorHandlingWCFServiceClient("BasicHttpBinding_IErrorHandlingWCFService", new EndpointAddress(_centerabsoluteuri));

            if (Session.User.Token.Center == null) Session.User.Token.Center = CommonSG.Instance(true).GetToken(Session.User.Id, Session.User.Pwd);
            ws.AuthInfoValue = new AuthInfo();
            ws.AuthInfoValue.UserId = Session.User.Id;
            ws.AuthInfoValue.AuthToken = Session.User.Token.Center;

            ws.InnerChannel.OperationTimeout = new TimeSpan(0, 1, 0);
            return ws;
        }

        public ExceptionDebugInfo ReportAndSaveException(BPMApplicationExceptionInfo excpetioninfo, bool clientack)
        {
            try
            {
                using (var ws = GetServiceInstance())
                {
                    return ws.ReportAndSaveException(excpetioninfo);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Architecture, ex);
            }
        }

        public void AcknowledgeException(string debuggingid, string fullstacktrace)
        {
            try
            {
                using (var ws = GetServiceInstance())
                {
                    ws.AcknowledgeException(debuggingid, fullstacktrace);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Architecture, ex);
            }
        }

    }
}

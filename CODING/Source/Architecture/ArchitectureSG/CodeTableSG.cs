using System;
using System.Data;
using System.ServiceModel;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using PEA.BPM.Architecture.ArchitectureSG.CodeTableWCF;
using PEA.BPM.Architecture.CommonUtilities;


namespace PEA.BPM.Architecture.ArchitectureSG
{
    public class CodeTableSG : ICodeTableService
    {
        private static string _centerabsoluteuri = "";
        private static string _branchabsoluteuri = "";
        private static bool _doesinit = false;

        #region Singleton
        private static readonly object _syncroot = new object();
        private static volatile CodeTableSG _centerinstance;
        private static volatile CodeTableSG _branchinstance;
        private CodeTableSG()
        {
        }
        public static CodeTableSG Instance(bool onlineConnection)
        {
            if (onlineConnection)
            {
                if (_centerinstance == null)
                {
                    lock (_syncroot) // for thread safe
                    {
                        if (_centerinstance == null) _centerinstance = new CodeTableSG { _isonline = true };
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
                        if (_branchinstance == null) _branchinstance = new CodeTableSG { _isonline = false };
                    }
                }
                return _branchinstance;
            }
        }
        #endregion

        private bool _isonline;

        private CodeTableWCFServiceClient GetServiceInstance()
        {
            if (!_doesinit) // init แค่ครั้งเดียวพอ
            {
                using (CodeTableWCFServiceClient initws = new CodeTableWCFServiceClient())
                {
                    string absoluteUri = initws.Endpoint.Address.Uri.AbsoluteUri;

                    if (_isonline)
                        _centerabsoluteuri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Center);
                    else
                        _branchabsoluteuri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Branch);
                }
                _doesinit = true;
            }

            CodeTableWCFServiceClient ws;
            if (_isonline)
                ws = new CodeTableWCFServiceClient("BasicHttpBinding_ICodeTableWCFService", new EndpointAddress(_centerabsoluteuri));
            else
                ws = new CodeTableWCFServiceClient("BasicHttpBinding_ICodeTableWCFService", new EndpointAddress(_branchabsoluteuri));
            ws.InnerChannel.OperationTimeout = new TimeSpan(0, 1, 0);
            return ws;
        }

        public DataSet GetUpdatedData(DateTime lastModifiedDt, string branchId)
        {
            try
            {
                using (var ws = GetServiceInstance())
                {
                    return ServiceHelper.DecompressDataBF<DataSet>(ws.GetUpdatedData(lastModifiedDt, branchId));
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Architecture, ex);
            }
        }
    }
}

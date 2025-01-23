using System;
using System.Collections.Generic;
using System.ServiceModel;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports;
using PEA.BPM.ePaymentsModule.Interface.Services;
using PEA.BPM.ePaymentsModule.SG.EPayReportWCF;

namespace PEA.BPM.ePaymentsModule.SG
{
    public class ReportSG : IReportService
    {
        private EPayReportWCFServiceClient _ws;

        public ReportSG(bool onlineConnection)
        {
            _ws = new EPayReportWCFServiceClient();
            string absoluteUri = _ws.Endpoint.Address.Uri.AbsoluteUri;

            if (onlineConnection)
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Report);
                _ws = new EPayReportWCFServiceClient("BasicHttpBinding_IEPayReportWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Center == null) Authorization.LoadServerToken(true);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Center;
            }
            else
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Branch);
                _ws = new EPayReportWCFServiceClient("BasicHttpBinding_IEPayReportWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Branch == null) Authorization.LoadServerToken(false);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Branch;
            }
            _ws.Endpoint.Binding.SendTimeout = new TimeSpan(5, 0, 0);
            _ws.Endpoint.Binding.ReceiveTimeout = new TimeSpan(5, 0, 0);
            _ws.InnerChannel.OperationTimeout = new TimeSpan(5, 0, 0);
        }

        #region IReportService Members

        public List<RE01_ReportInfo> GetRe01ReportService(RE01Param param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<RE01_ReportInfo>>(_ws.GetRe01ReportService(param));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.EPayment, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<RE02_ReportInfo> GetRe02ReportService(RE02ParamInfo param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<RE02_ReportInfo>>(_ws.GetRe02ReportService(param));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.EPayment, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<RE03_ReportInfo> GetRe03ReportService(RE03ParamInfo param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<RE03_ReportInfo>>(_ws.GetRe03ReportService(param));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.EPayment, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<RE04_ReportInfo> GetRe04ReportService(RE04ParamInfo param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<RE04_ReportInfo>>(_ws.GetRe04ReportService(param));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.EPayment, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<RE05_ReportInfo> GetRe05ReportService(RE05ParamInfo param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<RE05_ReportInfo>>(_ws.GetRe05ReportService(param));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.EPayment, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }
   
        public List<RE06_ReportInfo> GetRe06ReportInfo(RE06ParamInfo param)
        {

            try
            {
                return ServiceHelper.DecompressData<List<RE06_ReportInfo>>(_ws.GetRe06ReportInfo(param));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.EPayment, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }     

        public List<RE07_ReportInfo> GetRe07ReportInfo(RE07ParamInfo param)
        {

            try
            {
                return ServiceHelper.DecompressData<List<RE07_ReportInfo>>(_ws.GetRe07ReportInfo(param));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.EPayment, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }      

        public List<RE08_ReportInfo> GetRe08ReportInfo(RE08ParamInfo param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<RE08_ReportInfo>>(_ws.GetRe08ReportInfo(param));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.EPayment, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<RE09_ReportInfo> GetRe09ReportInfo(RE09ParamInfo param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<RE09_ReportInfo>>(_ws.GetRe09ReportInfo(param));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.EPayment, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        #endregion
    }
}

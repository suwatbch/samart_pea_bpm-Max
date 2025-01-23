using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using System.Data.Common;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using System.Web.Services.Protocols;
using PaymentCollectionModule.SG.ReportWCF;
using System.ServiceModel;
using WCFExtras.Soap;

namespace PaymentCollectionModule.SG
{
    public class ReportSG : IReportService
    {
        private ReportWCFServiceClient _ws;

        public ReportSG(bool onlineConnection)
        {
            _ws = new ReportWCFServiceClient();
            string absoluteUri = _ws.Endpoint.Address.Uri.AbsoluteUri;

            if (onlineConnection)
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Report);
                _ws = new ReportWCFServiceClient("BasicHttpBinding_IReportWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Center == null) Authorization.LoadServerToken(true);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Center;
            }
            else
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Branch);
                _ws = new ReportWCFServiceClient("BasicHttpBinding_IReportWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Branch == null) Authorization.LoadServerToken(false);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Branch;
            }
            _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 0);
        }

        #region IReportService Members

        public List<CAC01Report> GetReportCAC01(CAC01Param param)
        {
            try
            {
                List<CAC01Report> report = ServiceHelper.DecompressData<List<CAC01Report>>(_ws.GetReportCAC01_Compress(param));
                return report;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CAC03Report> GetReportCAC03(CAC01Param param)
        {
            try
            {
                List<CAC03Report> report = ServiceHelper.DecompressData<List<CAC03Report>>(_ws.GetReportCAC03_Compress(param));
                return report;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CAC04Report> GetReportCAC04(CAC01Param param)
        {
            try
            {
                List<CAC04Report> report = ServiceHelper.DecompressData<List<CAC04Report>>(_ws.GetReportCAC04_Compress(param));
                return report;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CAC05Report> GetReportCAC05(CAC06Param param)
        {
            try
            {
                List<CAC05Report> report = ServiceHelper.DecompressData<List<CAC05Report>>(_ws.GetReportCAC05_Compress(param));
                return report;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CAC06Report> GetReportCAC06(CAC06Param param)
        {
            try
            {
                List<CAC06Report> report = ServiceHelper.DecompressData<List<CAC06Report>>(_ws.GetReportCAC06_Compress(param));
                return report;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CAC06Report> GetReportCAC07(CAC06Param param)
        {
            try
            {
                List<CAC06Report> report = ServiceHelper.DecompressData<List<CAC06Report>>(_ws.GetReportCAC07_Compress(param));
                return report;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CAC09Report> GetReportCAC09(CAC09Param param)
        {
            try
            {
                List<CAC09Report> report = ServiceHelper.DecompressData<List<CAC09Report>>(_ws.GetReportCAC09_Compress(param));
                return report;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CAC11Report> GetReportCAC11(CAC11Param param)
        {
            try
            {
                List<CAC11Report> report = ServiceHelper.DecompressData<List<CAC11Report>>(_ws.GetReportCAC11_Compress(param));
                return report;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CAC12Report> GetReportCAC12(CAC09Param param)
        {
            try
            {
                List<CAC12Report> report = ServiceHelper.DecompressData<List<CAC12Report>>(_ws.GetReportCAC12_Compress(param));
                return report;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CAC13Report> GetReportCAC13(CAC11Param param)
        {
            try
            {
                List<CAC13Report> report = ServiceHelper.DecompressData<List<CAC13Report>>(_ws.GetReportCAC13_Compress(param));
                return report;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CAC14Report> GetReportCAC14(CAC14Param param)
        {
            try
            {
                List<CAC14Report> report = ServiceHelper.DecompressData<List<CAC14Report>>(_ws.GetReportCAC14_Compress(param));
                return report;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        //TODO: INSTALLMENT CASE
        //public List<CAC16Report> GetReportCAC16(CAC16Param param)
        //{
        //    try
        //    {
        //        List<CAC16Report> report = ServiceHelper.DecompressData<List<CAC16Report>>(_ws.GetReportCAC16_Compress(param));
        //        return report;
        //    }
        //    catch (Exception ex)
        //    {
        //        _ws.Abort();
        //        throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
        //    }
        //    finally
        //    {
        //        if (_ws.State != CommunicationState.Closed)
        //        {
        //            _ws.Close();
        //        }
        //    }
        //}

        #endregion


        #region IReportService Members


        public List<CAC18Report> GetReportCAC18(CAC18Param param)
        {
            try
            {
                List<CAC18Report> report = ServiceHelper.DecompressData<List<CAC18Report>>(_ws.GetReportCAC18_Compress(param));
                return report;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CAC19Report> GetReportCAC19(CAC19Param param)
        {
            try
            {
                List<CAC19Report> report = ServiceHelper.DecompressData<List<CAC19Report>>(_ws.GetReportCAC19_Compress(param));
                return report;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
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

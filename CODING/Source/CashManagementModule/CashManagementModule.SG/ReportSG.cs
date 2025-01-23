using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEA.BPM.CashManagementModule.Interface.Services;
using PEA.BPM.CashManagementModule.SG.CashierWCF;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using System.ServiceModel;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;

namespace PEA.BPM.CashManagementModule.SG
{
    public class ReportSG : ICashReportServices
    {
        private CashierWCFServiceClient _ws;

        public ReportSG(bool onlineConnection)
        {
            _ws = new CashierWCFServiceClient();
            string absoluteUri = _ws.Endpoint.Address.Uri.AbsoluteUri;

            if (onlineConnection)
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Report);
                _ws = new CashierWCFServiceClient("BasicHttpBinding_ICashierWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Center == null) Authorization.LoadServerToken(true);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Center;
            }
            else
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Branch);
                _ws = new CashierWCFServiceClient("BasicHttpBinding_ICashierWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Branch == null) Authorization.LoadServerToken(false);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Branch;
            }
            _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 1, 0);
        }

        #region ICashReportServices Members

        public List<ReportAvailableInfo> GetPayInOfDate(ReportParam param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<ReportAvailableInfo>>(_ws.GetPayInOfDate(param));

            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<ReportDailyPayInInfo> GetBankPayInDailyForReport(ReportParam param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<ReportDailyPayInInfo>>(_ws.GetBankPayInDailyForReport(param));

            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<ReportAvailableInfo> GetWorkBetweenDate(ReportParam param, string output)
        {
            try
            {
                return ServiceHelper.DecompressData<List<ReportAvailableInfo>>(_ws.GetWorkBetweenDate(param, output));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public ReportWorkFlowSummary GetWorkFlowDelayedReport(string workId)
        {
            try
            {
                int tmp = Convert.ToInt32(_ws.InnerChannel.OperationTimeout.TotalSeconds);
                _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 0);
                ReportWorkFlowSummary ret = ServiceHelper.DecompressData<ReportWorkFlowSummary>(_ws.GetWorkFlowDelayedReport(workId));
                _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 0, tmp);
                return ret;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<ReportAvailableInfo> GetCloseWorkOfDate(ReportParam param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<ReportAvailableInfo>>(_ws.GetCloseWorkOfDate(param));

            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<ReportCloseWorkSummary> GetCloseWorkSummaryReport(ReportParam param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<ReportCloseWorkSummary>>(_ws.GetCloseWorkSummaryReport(param));

            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
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

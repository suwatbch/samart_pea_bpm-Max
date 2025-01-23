using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.BillPrintingModule.Interface.Services;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.BillPrintingModule.SG.BillPrintingReportWCF;
using System.ServiceModel;
using WCFExtras.Soap;


namespace PEA.BPM.BillPrintingModule.SG
{
    public class ReportSG : IReportServices
    {
        private BillPrintingReportWCFServiceClient _ws;

        public ReportSG(bool onlineConnection)
        {
            _ws = new BillPrintingReportWCFServiceClient();
            string absoluteUri = _ws.Endpoint.Address.Uri.AbsoluteUri;

            if (onlineConnection)
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Report);
                _ws = new BillPrintingReportWCFServiceClient("BasicHttpBinding_IBillPrintingReportWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Center == null) Authorization.LoadServerToken(true);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP(); 
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Center;
            }
            else
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Branch);
                _ws = new BillPrintingReportWCFServiceClient("BasicHttpBinding_IBillPrintingReportWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Branch == null) Authorization.LoadServerToken(false);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP(); 
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Branch;
            }
            _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 0);
        }

        #region IReportService Members

        public List<ReportDailyPrint> GetDailyPrintReport(ReportConditionParam param) 
        {
            try 
            {
                return ServiceHelper.DecompressData<List<ReportDailyPrint>>(_ws.GetDailyPrintReport(param));
            }
             catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<ReportDailyUnprint> GetDailyUnprintReport(ReportConditionParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<ReportDailyUnprint>>(_ws.GetDailyUnprintReport(param));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<ReportBillDelivery> GetBillDeliveryReport(ReportConditionParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<ReportBillDelivery>>(_ws.GetBillDeliveryReport(param));
            }
             catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<ReportStreetRoute> GetStreetRouteReport(ReportConditionParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<ReportStreetRoute>>(_ws.GetStreetRouteReport(param));
            }
             catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<ReportStreetRouteReceive> GetStreetRouteReceiveReport(ReportConditionParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<ReportStreetRouteReceive>>(_ws.GetStreetRouteReceiveReport(param));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<ReportStreetRouteUnreceive> GetStreetRouteUnreceiveReport(ReportConditionParam param) 
        {
            try 
            {
                return ServiceHelper.DecompressData<List<ReportStreetRouteUnreceive>>(_ws.GetStreetRouteUnreceiveReport(param));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<ReportF16> GetF16Report(ReportConditionParam param) 
        {
            try 
            {
                return ServiceHelper.DecompressData<List<ReportF16>>(_ws.GetF16Report(param));
            }
             catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<ReportGrpInvSummary> GetGrpInvSummaryReport(ReportConditionParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<ReportGrpInvSummary>>(_ws.GetGrpInvSummaryReport(param));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<ReportPrintByBank> GetPrintGreenByBankReport(ReportConditionParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<ReportPrintByBank>>(_ws.GetPrintGreenByBankReport(param));
            }
             catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<ReportBillingStatus> GetBillingStatusReport(ReportConditionParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<ReportBillingStatus>>(_ws.GetBillingStatusReport(param));
            }
           catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<ReportGroupingCrossCheck> GetGroupingCrossCheckReport(ReportConditionParam param)
        {
            try 
            {
                return new List<ReportGroupingCrossCheck>(_ws.GetGroupingCrossCheckReport(param));
            }
             catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<ReportCAUnprint> GetCAUnprintReport(ReportConditionParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<ReportCAUnprint>>(_ws.GetCAUnprintReport(param));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }     

        public List<ReportDirectDebitStatus> GetDirectDebitStatusReport(ReportConditionParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<ReportDirectDebitStatus>>(_ws.GetDirectDebitStatusReport(param));
            }
             catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<PrintableId> GetBranchForBillDeliveryReport(ReportConditionParam param)
        {
            try 
            {
                return new List<PrintableId>(_ws.GetBranchForBillDeliveryReport(param));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
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

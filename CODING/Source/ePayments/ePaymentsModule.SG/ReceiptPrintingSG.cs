using System;
using System.Collections.Generic;
using System.ServiceModel;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.ReceiptPrinting;
using PEA.BPM.ePaymentsModule.Interface.Services;
using PEA.BPM.ePaymentsModule.SG.ReceiptPrintingWCF;

namespace PEA.BPM.ePaymentsModule.SG
{
    public class ReceiptPrintingSG : IReceiptPrintingService
    {
        private ReceiptPrintingWCFServiceClient _ws;

        public ReceiptPrintingSG(bool onlineConnection)
        {
            _ws = new ReceiptPrintingWCFServiceClient();
            string absoluteUri = _ws.Endpoint.Address.Uri.AbsoluteUri;

            if (onlineConnection)
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Center);
                _ws = new ReceiptPrintingWCFServiceClient("BasicHttpBinding_IReceiptPrintingWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Center == null) Authorization.LoadServerToken(true);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Center;
            }
            else
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Branch);
                _ws = new ReceiptPrintingWCFServiceClient("BasicHttpBinding_IReceiptPrintingWCFService", new EndpointAddress(absoluteUri));
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

        #region IBillPrintingService Members

        public List<Bills> PrintGreenBill(ReceiptConditionParam param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<Bills>>(_ws.PrintGreenBill(param));
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

        #region IReceiptPrintingService Members


        public List<PPrintedReceipt> GetPPrintedService(PPrintedReceiptParam param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<PPrintedReceipt>>(_ws.GetPPrintedService(param));
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

        public List<Company> GetAgentAllService()
        {
            try
            {
                return ServiceHelper.DecompressData<List<Company>>(_ws.GetAgentAllService());
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

        public List<EPayUpload> SearchAgentPaymentNumber(EPayUpload param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<EPayUpload>>(_ws.SearchAgentPaymentNumber(param));
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

        /*   Off Line    */

        public List<PPrintedDeposit> SearchDebtClearify(PPrintedDeposit param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<PPrintedDeposit>>(_ws.SearchDebtClearify(param));
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

        public List<PPrintedDeposit> GetCADepositPPrinted(List<PPrintedDeposit> paramList)
        {
            try
            {
                return ServiceHelper.DecompressData<List<PPrintedDeposit>>(_ws.GetCADepositPPrinted(ServiceHelper.CompressData<List<PPrintedDeposit>>(paramList)));
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

        public List<PPrintedDeposit> GetAgentDepositPPrinted(List<PPrintedDeposit> paramList)
        {
            try
            {
                return ServiceHelper.DecompressData<List<PPrintedDeposit>>(_ws.GetAgentDepositPPrinted(ServiceHelper.CompressData<List<PPrintedDeposit>>(paramList)));
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

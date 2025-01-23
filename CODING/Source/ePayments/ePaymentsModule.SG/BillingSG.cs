using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using PEA.BPM.ePaymentsModule.Interface.Services;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.ReceiptPrinting;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.ePaymentsModule.SG.EPayBillingWCF;
using System.ServiceModel;
using WCFExtras.Soap;

namespace PEA.BPM.ePaymentsModule.SG
{
    public class BillingSG : IBillingService
    {
        private EPayBillingWCFServiceClient _ws;

        public BillingSG(bool onlineConnection)
        {
            _ws = new EPayBillingWCFServiceClient();
            string absoluteUri = _ws.Endpoint.Address.Uri.AbsoluteUri;

            if (onlineConnection)
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Center);
                _ws = new EPayBillingWCFServiceClient("BasicHttpBinding_IEPayBillingWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Center == null) Authorization.LoadServerToken(true);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Center;
            }
            else
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Branch);
                _ws = new EPayBillingWCFServiceClient("BasicHttpBinding_IEPayBillingWCFService", new EndpointAddress(absoluteUri));
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

        //public int PayInvoice(DbTransaction trans, PaymentInfo payment)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        public string UpdateBillMarkFlagService(string caId, string invoiceNo, string modifiedBy, string postServerId)
        {
            try
            {
                return _ws.UpdateBillMarkFlagService(caId, invoiceNo, modifiedBy, postServerId);

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

        public void DelBillMarkFlagService()
        {
            try
            {
                _ws.DelBillMarkFlagService();
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


        public List<EpayUploadItem> GetDebtComparableService(string caInvoceNo)
        {
            try
            {
                return ServiceHelper.DecompressData<List<EpayUploadItem>>(_ws.GetDebtComparableService(caInvoceNo));
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

        public List<Company> SearchCompany(Company compParm)
        {
            try
            {
                return ServiceHelper.DecompressData<List<Company>>(_ws.SearchCompany(compParm));
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

        public List<ClearifyInfo> SearchDebtService(SearchDebtParam searchDebtParam)
        {
            try
            {
                return ServiceHelper.DecompressData<List<ClearifyInfo>>(_ws.SearchDebtService(searchDebtParam));
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

        public void InsertEPayUploadService(List<EPaymentUploadFile> ePayFileList)
        {
            try
            {
                _ws.InsertEPayUploadService(ServiceHelper.CompressData<List<EPaymentUploadFile>>(ePayFileList));
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

        public bool IsUploadFileNameExist(string fileName)
        {
            try
            {
               return _ws.IsUploadFileNameExist(fileName);
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

        public List<BPMClearifyInfo> SearchBPMDebtClearify(SearchDebtParam searchDebtParam)
        {
            try
            {
                return ServiceHelper.DecompressData<List<BPMClearifyInfo>>(_ws.SearchBPMDebtClearify(searchDebtParam));
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

        public List<AgentPayment> GetAgentPaymentService(AgentPayment agentPayment)
        {
            try
            {
                return ServiceHelper.DecompressData<List<AgentPayment>>(_ws.GetAgentPaymentService(agentPayment));
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

        public void InsertAgentPaymentService(List<AgentPayment> agentPayList)
        {
            try
            {
                _ws.InsertAgentPaymentService(ServiceHelper.CompressData<List<AgentPayment>>(agentPayList));
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

        public bool SaveClearify(DbTransaction trans, SaveClearifyInfo saveClearifyItem)
        {
            try
            {
                return _ws.SaveClearify(ServiceHelper.CompressData<SaveClearifyInfo>(saveClearifyItem));
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

        public List<CancelPayment> SearchAgentPayment(CancelPayment payment)
        {
            try
            {
                return ServiceHelper.DecompressData<List<CancelPayment>>(_ws.SearchAgentPayment(payment));
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

        public void InsertCancelPayment(List<CancelPayment> cancelPayment)
        {
            try
            {
                _ws.InsertCancelPayment(ServiceHelper.CompressData<List<CancelPayment>>(cancelPayment));
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

    }

}

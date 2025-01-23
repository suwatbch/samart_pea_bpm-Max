using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using System.Data.Common;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PaymentCollectionModule.SG.PaidBillWCF;
using System.ServiceModel;
using WCFExtras.Soap;

namespace PaymentCollectionModule.SG
{
    public class PaidBillSG: IPaidBillService
    {
        private PaidBillWCFServiceClient _ws;

        public PaidBillSG(bool onlineConnection)
        {
            _ws = new PaidBillWCFServiceClient();
            string absoluteUri = _ws.Endpoint.Address.Uri.AbsoluteUri;

            if (onlineConnection)
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Center);
                _ws = new PaidBillWCFServiceClient("BasicHttpBinding_IPaidBillWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Center == null) Authorization.LoadServerToken(true);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Center;
            }
            else
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Branch);
                _ws = new PaidBillWCFServiceClient("BasicHttpBinding_IPaidBillWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Branch == null) Authorization.LoadServerToken(false);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Branch;
            }
            _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 0);
        }

        #region IPaidBillService Members

        public List<Receipt> SearchPaidBill(PaidBillSearchParam param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<Receipt>>(_ws.SearchPaidBill(param));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS®Ë“¬‡ß‘π, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<Receipt> SearchReceipt(ReceiptSearchParam param)
        {
            try
            {
                return ServiceHelper.DecompressDataBF<List<Receipt>>(_ws.SearchReceipt(param));
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

        public ReceiptDetail GetReceiptDetail(string receiptId)
        {
            try
            {
                return ServiceHelper.DecompressData<ReceiptDetail>(_ws.GetReceiptDetail(receiptId));
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

        public CancelledInfo CancelReceipt(DbTransaction trans, List<string> receiptIds, string reason,
            string reprintReceiptId, string newReceiptId, string posId, string terminalCode, string branchId, string branchName,
            string cashierId, string cashierName, string workId)
        {
            try
            {
                return ServiceHelper.DecompressData<CancelledInfo>(_ws.CancelReceipt(receiptIds.ToArray(), reason,
                    reprintReceiptId, newReceiptId, posId, terminalCode, branchId, branchName, cashierId, cashierName, workId));
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

        public List<PrintingInfo> GetReceiptsForPrint(DbTransaction trans, List<string> receiptIds)
        {
            try
            {
                return ServiceHelper.DecompressData<List<PrintingInfo>>(_ws.GetReceiptsForPrint(receiptIds.ToArray()));
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

        public void IncreaseNoOfReprint(string receiptId)
        {
            try
            {
                _ws.IncreaseNoOfReprint(receiptId);
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

        public List<OneTouchLogInfo> SearchOneTouchPayment(List<string> receiptIds)
        {
            try
            {
                return ServiceHelper.DecompressData<List<OneTouchLogInfo>>(_ws.SearchOneTouchPayment(receiptIds.ToArray()));
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

        public List<string> SearchPaymentTypeQR(List<string> paymentIds)
        {
            try
            {
                return ServiceHelper.DecompressData<List<string>>(_ws.SearchOneSearchPaymentTypeQR(paymentIds.ToArray()));
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

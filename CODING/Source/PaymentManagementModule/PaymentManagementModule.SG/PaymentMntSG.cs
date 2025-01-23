using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentManagementModule.Interface.Services;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using System.Data.Common;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities.Reports;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using System.Web.Services.Protocols;
using PEA.BPM.PaymentManagementModule.SG.PaymentMntWCF;
using System.ServiceModel;
using WCFExtras.Soap;

namespace PEA.BPM.PaymentManagementModule.SG
{
    public class PaymentMntSG : IPaymentMntService
    {
        private bool _onlineConnection;
        private PaymentMntWCFServiceClient _ws;

        public PaymentMntSG(bool onlineConnection)
        {
            _ws = new PaymentMntWCFServiceClient();
            string absoluteUri = _ws.Endpoint.Address.Uri.AbsoluteUri;

            if (onlineConnection)
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Center);
                _ws = new PaymentMntWCFServiceClient("BasicHttpBinding_IPaymentMntWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Center == null) Authorization.LoadServerToken(true);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Center;
            }
            else
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Branch);
                _ws = new PaymentMntWCFServiceClient("BasicHttpBinding_IPaymentMntWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Branch == null) Authorization.LoadServerToken(false);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Branch;
            }
            _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 0);
        }

        #region IPaymentMntService Members

        public decimal? GetMoneyInTray(string workId)
        {
            try
            {
                return ServiceHelper.DecompressData<decimal?>(_ws.GetMoneyInTray_Compress(workId));
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

        public string GetCaNameByPaymentVoucher(string caId)
        {
            try
            {
                return ServiceHelper.DecompressData<string>(_ws.GetCaNameByPaymentVoucher_Compress(caId));
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

        public List<APInfo> SearchPaidPaymentVoucher(string paymentVoucher)
        {
            try
            {
                return ServiceHelper.DecompressData<List<APInfo>>(_ws.SearchPaidPaymentVoucher_Compress(paymentVoucher));
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

        public bool PayAP(List<APInfo> ap, DateTime paymentDate, string posId, string terminalCode, string cashierId, string cashierName, string branchId, string branchName)
        {
            try
            {
                return ServiceHelper.DecompressData<bool>(_ws.PayAP_Compress(ap.ToArray(), paymentDate, posId, terminalCode, cashierId, cashierName, branchId, branchName));
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

        public List<APEntity> SearchPaymentVoucher(PaymentVoucherSearchParam param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<APEntity>>(_ws.SearchPaymentVoucher(param));
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

        public List<APEntity> UpdateAPByStrLineAPId(string strLineAPId, string reason, string cashierId, string cashierName)
        {
            try
            {
                return ServiceHelper.DecompressData<List<APEntity>>(_ws.UpdateAPByStrLineAPId(strLineAPId, reason, cashierId, cashierName));
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
        #endregion
    }
}

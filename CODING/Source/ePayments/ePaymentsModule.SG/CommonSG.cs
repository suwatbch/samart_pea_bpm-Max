using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.ePaymentsModule.Interface.Services;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using System.ComponentModel;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports;
using PEA.BPM.ePaymentsModule.SG.EPayCommonWCF;
using System.ServiceModel;
using WCFExtras.Soap;

namespace PEA.BPM.ePaymentsModule.SG
{
    public class CommonSG : ICommonService
    {
        private EPayCommonWCFServiceClient _ws;
        public CommonSG(bool onlineConnection)
        {
            _ws = new EPayCommonWCFServiceClient();
            string absoluteUri = _ws.Endpoint.Address.Uri.AbsoluteUri;

            if (onlineConnection)
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Center);
                _ws = new EPayCommonWCFServiceClient("BasicHttpBinding_IEPayCommonWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Center == null) Authorization.LoadServerToken(true);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Center;
            }
            else
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Branch);
                _ws = new EPayCommonWCFServiceClient("BasicHttpBinding_IEPayCommonWCFService", new EndpointAddress(absoluteUri));
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

        #region ICommonService Members


        public string VerifyContractorService(string caId, string period, decimal debtAmount)
        {
            _ws.Close();
            throw new Exception("The method or operation is not implemented.");            
        }

        public List<SearchContractorInfo> SearchContractorService(string caId, string billFlag)
        {
            _ws.Close();
            throw new Exception("The method or operation is not implemented.");
        }

        public List<SearchConAccountInfo> SearchConAccountService(string caId)
        {
            _ws.Close();
            throw new Exception("The method or operation is not implemented.");
        }

        public List<Agency> GetAgencyAllService(Agency agency)
        {
            try
            {
                return ServiceHelper.DecompressData<List<Agency>>(_ws.GetAgencyAllService_Compress(agency));
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

        public List<AccountClassInfo> GetAccountClassList(AccountClassInfo acParam)
        {
            try
            {
                return ServiceHelper.DecompressData<List<AccountClassInfo>>(_ws.GetAccountClassList_Compress(acParam));
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
        
        public List<Company> GetCompanyList(CompanyParamInfo comParam)
        {
            try
            {
                return ServiceHelper.DecompressData<List<Company>>(_ws.GetCompanyList_Compress(comParam));
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

        public List<Company> GetCompanyByUploadDtList(DateTime? uploadDt)
        {
            try
            {
                return ServiceHelper.DecompressData<List<Company>>(_ws.GetCompanyByUploadDtList_Compress(uploadDt));
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

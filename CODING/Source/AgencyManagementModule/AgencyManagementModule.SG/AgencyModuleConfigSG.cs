using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.AgencyManagementModule.SG.AgencyConfigWCF;
using System.ServiceModel;
using WCFExtras.Soap;

namespace PEA.BPM.AgencyManagementModule.SG
{
    public class AgencyModuleConfigSG : IAgencyModuleConfigService
    {
        private AgencyConfigWCFServiceClient _ws;

        public AgencyModuleConfigSG(bool onlineConnection)
        {
            _ws = new AgencyConfigWCFServiceClient();
            string absoluteUri = _ws.Endpoint.Address.Uri.AbsoluteUri;

            if (onlineConnection)
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Center);
                _ws = new AgencyConfigWCFServiceClient("BasicHttpBinding_IAgencyConfigWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Center == null) Authorization.LoadServerToken(true);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Center;
            }
            else
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Branch);
                _ws = new AgencyConfigWCFServiceClient("BasicHttpBinding_IAgencyConfigWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Branch == null) Authorization.LoadServerToken(false);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Branch;
            }
            _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 1, 0);
        }

        #region IAgencyModuleConfigService Members

        public FeeBaseElement GetBaseCommissionRate(string branchId)
        {
            try
            {
                return _ws.GetBaseCommissionRate(branchId);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public bool UpdateCommissionRate(FeeBaseElement feeBase)
        {
            try
            {
                return _ws.UpdateCommissionRate(ServiceHelper.CompressData<FeeBaseElement>(feeBase));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
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

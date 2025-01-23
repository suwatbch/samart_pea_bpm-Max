using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using System.ComponentModel;
using PEA.BPM.Architecture.CommonUtilities;
using System.Data.Common;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.AgencyManagementModule.SG.AgencyPlanningWCF;
using System.ServiceModel;
using WCFExtras.Soap;


namespace PEA.BPM.AgencyManagementModule.SG
{
    public class AgencyPlanningSG : IAgencyPlanningService
    {
        private AgencyPlanningWCFServiceClient _ws;

        public AgencyPlanningSG(bool onlineConnection)
        {
            _ws = new AgencyPlanningWCFServiceClient();
            string absoluteUri = _ws.Endpoint.Address.Uri.AbsoluteUri;

            if (onlineConnection)
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Center);
                _ws = new AgencyPlanningWCFServiceClient("BasicHttpBinding_IAgencyPlanningWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Center == null) Authorization.LoadServerToken(true);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Center;
            }
            else
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Branch);
                _ws = new AgencyPlanningWCFServiceClient("BasicHttpBinding_IAgencyPlanningWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Branch == null) Authorization.LoadServerToken(false);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Branch;
            }
            _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 1, 0);
        }

        #region IAgencyPlanningService Members

        public bool SaveAssignedLineofAgent(DbTransaction trans, System.ComponentModel.BindingList<LineInfo> asiLineList)
        {   
            try
            {
                return _ws.SaveAssignedLineofAgent(ServiceHelper.CompressData<BindingList<LineInfo>>(asiLineList));
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

        public System.ComponentModel.BindingList<LineInfo> FindAndDisplayLineOfAgentSearchInfo(string agentId)
        {
            try
            {
                return ServiceHelper.DecompressData<BindingList<LineInfo>>(_ws.FindAndDisplayLineOfAgentSearchInfo_Compress(agentId));
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

        public List<AgentInfo> AcquireAgentAssetSearchInformation(AgentSearchInfo searchInfo)
        {
            try
            {
                return ServiceHelper.DecompressData<List<AgentInfo>>(_ws.AcquireAgentAssetSearchInformation_Compress(ServiceHelper.CompressData<AgentSearchInfo>(searchInfo)));
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

        public List<PeaInfo> FindAndDisplayBranchByKeyword(string keyword, string branchId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<PeaInfo>>(_ws.FindAndDisplayBranchByKeyword_Compress(keyword, branchId));
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

        public BindingList<LineInfo> FindAndDisplayLineByKeyword(LineSearchBoxInfo searchInfo)
        {
            try
            {
                return ServiceHelper.DecompressData<BindingList<LineInfo>>(_ws.FindAndDisplayLineByKeyword_Compress(ServiceHelper.CompressData<LineSearchBoxInfo>(searchInfo)));
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

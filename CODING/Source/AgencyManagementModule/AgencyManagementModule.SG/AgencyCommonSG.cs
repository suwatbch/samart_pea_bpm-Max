using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using System.ComponentModel;
using System.ServiceModel;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.AgencyManagementModule.SG.AgencyCommonWCF;
using WCFExtras.Soap;

namespace PEA.BPM.AgencyManagementModule.SG
{
    public class AgencyCommonSG : IAgencyCommonService
    {
        private AgencyCommonWCFServiceClient _ws;

        public AgencyCommonSG(bool onlineConnection)
        {
            _ws = new AgencyCommonWCFServiceClient();
            string absoluteUri = _ws.Endpoint.Address.Uri.AbsoluteUri;

            if (onlineConnection)
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Center);
                _ws = new AgencyCommonWCFServiceClient("BasicHttpBinding_IAgencyCommonWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Center == null) Authorization.LoadServerToken(true);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Center;
            }
            else
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Branch);
                _ws = new AgencyCommonWCFServiceClient("BasicHttpBinding_IAgencyCommonWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Branch == null) Authorization.LoadServerToken(false);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Branch;
            }
            _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 1, 0);
        }

        #region IAgencyCommonService Members

        public AgentInfo FindAndDisplayAgentSearchInfo(string agentId)
        {
            try
            {
                return ServiceHelper.DecompressData<AgentInfo>(_ws.FindAndDisplayAgentSearchInfo_Compress(agentId));
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

        public PeaInfo FindAndDisplayBranchSearchInfo(string basedBranchId, string branchId)
        {
            try
            {
                return ServiceHelper.DecompressData<PeaInfo>(_ws.FindAndDisplayBranchSearchInfo_Compress(basedBranchId, branchId));
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

        public BindingList<LineInfo> FindAndDisplayLineSearchInfo(string branchId, string lineKey)
        {
            try
            {
                return ServiceHelper.DecompressData<BindingList<LineInfo>>(_ws.FindAndDisplayLineSearchInfo_Compress(branchId, lineKey));
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

        public HashInfoCollection GetPmList(string pmId)
        {
            try
            {
                return ServiceHelper.DecompressData<HashInfoCollection>(_ws.GetPmList_Compress(pmId));
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

        public HashInfoCollection GetAbsList(string absId)
        {
            try
            {
                return ServiceHelper.DecompressData<HashInfoCollection>(_ws.GetAbsList_Compress(absId));
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

        public string GetContractTypeList(string ctId)
        {
            try
            {
                return _ws.GetContractTypeList(ctId);
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

        public HashInfoCollection GetBillStatusList(string bsId)
        {
            try
            {
                return ServiceHelper.DecompressData<HashInfoCollection>(_ws.GetBillStatusList_Compress(bsId));
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


        public List<PeaInfo> GetBranches(string branchId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<PeaInfo>>(_ws.GetBranches_Compress(branchId));
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



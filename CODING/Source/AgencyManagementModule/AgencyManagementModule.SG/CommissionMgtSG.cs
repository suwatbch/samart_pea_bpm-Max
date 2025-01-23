using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.AgencyManagementModule.SG.CommissionMgtWCF;
using System.ServiceModel;
using WCFExtras.Soap;

namespace PEA.BPM.AgencyManagementModule.SG
{
    public class CommissionMgtSG : ICommissionMgtService
    {
        private CommissionMgtWCFServiceClient _ws;

        public CommissionMgtSG(bool onlineConnection)
        {
            _ws = new CommissionMgtWCFServiceClient();
            string absoluteUri = _ws.Endpoint.Address.Uri.AbsoluteUri;

            if (onlineConnection)
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Center);
                _ws = new CommissionMgtWCFServiceClient("BasicHttpBinding_ICommissionMgtWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Center == null) Authorization.LoadServerToken(true);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Center;
            }
            else
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Branch);
                _ws = new CommissionMgtWCFServiceClient("BasicHttpBinding_ICommissionMgtWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Branch == null) Authorization.LoadServerToken(false);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Branch;
            }
            _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 1, 0);
        }

        #region ICommissionMgtService Members

        public CommissionInfo CalculateCommissionAndFine(BookSearchInfo searchInfo, FeeBaseElement comRate, BooniesCommissionInfo hp)
        {
            try
            {
                return ServiceHelper.DecompressData<CommissionInfo>(_ws.CalculateCommissionAndFine_Compress(ServiceHelper.CompressData<BookSearchInfo>(searchInfo), ServiceHelper.CompressData<FeeBaseElement>(comRate), ServiceHelper.CompressData<BooniesCommissionInfo>(hp)));
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

        public string SaveCommission(System.Data.Common.DbTransaction trans, HelpOfferInfo flavour)
        {
            try
            {
                return _ws.SaveCommission(ServiceHelper.CompressData<HelpOfferInfo>(flavour));
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

        public FeeBaseElement LoadCommissionRate(BookSearchInfo searchInfo)
        {
            try
            {
                return ServiceHelper.DecompressData<FeeBaseElement>(_ws.LoadCommissionRate_Compress(ServiceHelper.CompressData<BookSearchInfo>(searchInfo)));
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

        public bool IsCommissionCalculated(BookSearchInfo searchInfo)
        {
            try
            {
                return _ws.IsCommissionCalculated(ServiceHelper.CompressData<BookSearchInfo>(searchInfo));
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

        public List<CalculatedCommissionInfo> GetCalCountOfPeriodByAgentId(BookSearchInfo searchInfo)
        {
            try
            {
                return ServiceHelper.DecompressData<List<CalculatedCommissionInfo>>((_ws.GetCalCountOfPeriodByAgentId_Compress(ServiceHelper.CompressData<BookSearchInfo>(searchInfo))));
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

        public int GetCommissionCountOfPeriod(BookSearchInfo searchInfo)
        {
            try
            {
                return _ws.GetCommissionCountOfPeriod(ServiceHelper.CompressData<BookSearchInfo>(searchInfo));
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

        public decimal? GetAndDisplayAdvPaymentAmount(BookSearchInfo searchInfo)
        {
            try
            {
                return _ws.GetAndDisplayAdvPaymentAmount(ServiceHelper.CompressData<BookSearchInfo>(searchInfo));
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

        public AgentInfo GetAgentHelpInformation(string agentId)
        {
            try
            {
                return ServiceHelper.DecompressData<AgentInfo>(_ws.GetAgentHelpInformation_Compress(agentId));
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

        public List<string> GetListOfCreatedDate(BookSearchInfo searchInfo)
        {
            try
            {
                return ServiceHelper.DecompressData<List<string>>(_ws.GetListOfCreatedDate_Compress(ServiceHelper.CompressData<BookSearchInfo>(searchInfo)));
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

        public List<string> BookListByCreateDate(BookSearchInfo searchInfo)
        {
            try
            {
                return ServiceHelper.DecompressData<List<string>>(_ws.BookListByCreateDate_Compress(ServiceHelper.CompressData<BookSearchInfo>(searchInfo)));
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

        public bool IsBookAvailable(BookSearchInfo searchInfo)
        {
            try
            {
                return _ws.IsBookAvailable(ServiceHelper.CompressData<BookSearchInfo>(searchInfo));
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

        public TravelHelpRate GetTravelHelpRate(TravelHelpRateConditionInfo spcCondition)
        {
            try
            {
                return ServiceHelper.DecompressData<TravelHelpRate>(_ws.GetTravelHelpRate_Compress(ServiceHelper.CompressData<TravelHelpRateConditionInfo>(spcCondition)));
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

        public FeeBaseElement GetFeeBase(string branchId)
        {
            try
            {
                return ServiceHelper.DecompressData<FeeBaseElement>(_ws.GetFeeBase_Compress(branchId));
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

        public bool IsBillBookCalculated(string billbookId)
        {
            try
            {
                return _ws.IsBillBookCalculated(billbookId);
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

        public bool IsBookAlreadyPaid(BookSearchInfo searchInfo)
        {
            try
            {
                return _ws.IsBookAlreadyPaid(ServiceHelper.CompressData<BookSearchInfo>(searchInfo));
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


        public List<CommissionHistoryInfo> GetCommissionHistory(BookSearchInfo searchInfo)
        {
            try
            {
                return ServiceHelper.DecompressData<List<CommissionHistoryInfo>>(_ws.GetCommissionHistory(searchInfo));
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

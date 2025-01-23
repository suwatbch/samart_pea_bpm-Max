using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.NewsBroadcast.Interface.Services;
using PEA.BPM.NewsBroadcast.Interface.BusinessEntities;
using PEA.BPM.NewsBroadcast.SG.BroadcastWCF;
using System.ServiceModel;
using WCFExtras.Soap;

namespace PEA.BPM.NewsBroadcast.SG
{
    public class NewsBroadcastSG : INewsBroadcastService
    {
        private BroadcastWCFServiceClient _ws;

        public NewsBroadcastSG()
        {
            try
            {
                _ws = new BroadcastWCFServiceClient();
                if (!Session.Server.Address.Center.Equals(null) || !Session.Server.Address.Center.Equals(""))
                {
                    string wsUrl = Session.Server.Address.Center + "/TOOL/BroadcastWCFService.svc";
                    _ws = new BroadcastWCFServiceClient("BasicHttpBinding_IBroadcastWCFService", new EndpointAddress(wsUrl));
                    _ws.AuthInfoValue = new AuthInfo();
                    _ws.AuthInfoValue.UserId = Session.User.Id;
                    _ws.AuthInfoValue.AuthToken = Session.User.Token.Center;
                   
                }
                _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 0);
            }
            catch (Exception)
            {
                throw;
            }
           
        }
         /// <summary>
        /// Constructor for Administrative tool
        /// </summary>
        /// <param name="wsUrl">URL of WebService</param>
        public NewsBroadcastSG(string wsUrl)
        {
            try
            {
                _ws = new BroadcastWCFServiceClient();
                _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 0);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                EndpointAddress endPoint = new EndpointAddress(wsUrl);
                _ws.Endpoint.Address = endPoint;
               
            }
            catch (Exception)
            {
                throw;
            }
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select)]
        public List<NewsBroadcastInfo> GetNewsBroadcastNow(DateTime _nowDt, string _userId, int _cmdId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<NewsBroadcastInfo>>(_ws.GetNewsBroadcastNow(_nowDt, _userId, _cmdId));
            }
            catch
            {
                _ws.Abort();
                throw;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select)]
        public List<NewsBroadcastInfo> GetNewsBroadcastHistory(DateTime _nowDt, string _userId, int _cmdId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<NewsBroadcastInfo>>(_ws.GetNewsBroadcastHistory(_nowDt, _userId, _cmdId));
            }
            catch
            {
                _ws.Abort();
                throw;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select)]
        public List<NewsBroadcastSentInfo> GetNewsBroadcastSent(DateTime _sentDt)
        {
            try
            {
                return ServiceHelper.DecompressData<List<NewsBroadcastSentInfo>>(_ws.GetNewsBroadcastSent(_sentDt));
            }
            catch
            {
                _ws.Abort();
                throw;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select)]
        public List<NewsBroadcastSentInfo> GetNewsBroadcastNowForSender(DateTime _nowDt, int _cmdId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<NewsBroadcastSentInfo>>(_ws.GetNewsBroadcastNowForSender(_nowDt,_cmdId));
            }
            catch
            {
                _ws.Abort();
                throw;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select)]
        public List<NewsBroadcastSentInfo> GetNewsBroadcastScheduled(DateTime _nowDt, int _cmdId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<NewsBroadcastSentInfo>>(_ws.GetNewsBroadcastScheduled(_nowDt, _cmdId));
            }
            catch
            {
                _ws.Abort();
                throw;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select)]
        public List<NewsBroadcastSentInfo> GetNewsBroadcastSentMonthYears(DateTime _sentDt)
        {
            try
            {
                return ServiceHelper.DecompressData<List<NewsBroadcastSentInfo>>(_ws.GetNewsBroadcastSentMonthYears(_sentDt));
            }
            catch
            {
                _ws.Abort();
                throw;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select)]
        public List<NewsBroadcastUserInfo> GetNewsBroadcastUser(int _broadcastId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<NewsBroadcastUserInfo>>(_ws.GetNewsBroadcastUser(_broadcastId));
            }
            catch
            {
                _ws.Abort();
                throw;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update)]
        public void UpdateNewsBroadcastUserOpened(int _broadcastId, string _userId)
        {
            try
            {
                _ws.UpdateNewsBroadcastUserOpened(_broadcastId, _userId);
            }
            catch
            {
                _ws.Abort();
                throw;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update)]
        public void UpdateNewsBroadcastUserRead(int _broadcastId, string _userId)
        {
            try
            {
                _ws.UpdateNewsBroadcastUserRead(_broadcastId, _userId);
            }
            catch
            {
                _ws.Abort();
                throw;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select)]
        public List<AreaInfo> GetArea()
        {
            try
            {
                return ServiceHelper.DecompressData<List<AreaInfo>>(_ws.GetArea());
            }
            catch
            {
                _ws.Abort();
                throw;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select)]
        public List<RoleInfo> GetRole()
        {
            try
            {
                return ServiceHelper.DecompressData<List<RoleInfo>>(_ws.GetRole());
            }
            catch
            {
                _ws.Abort();
                throw;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select)]
        public List<BranchInfo> GetBranch(string _areaId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<BranchInfo>>(_ws.GetBranch(_areaId));
            }
            catch
            {
                _ws.Abort();
                throw;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select)]
        public List<UserInfo> GetUser(string _roleId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<UserInfo>>(_ws.GetUser(_roleId));
            }
            catch
            {
                _ws.Abort();
                throw;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert)]
        public void InsertNewsBroadcast(string _broadcastTopic, string _detail, DateTime _sentDt, DateTime _expireDt, int _cmdId)
        {
            try
            {
                _ws.InsertNewsBroadcast(_broadcastTopic, _detail, _sentDt, _expireDt, _cmdId);
            }
            catch
            {
                _ws.Abort();
                throw;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert)]
        public void InsertNewsBroadcastUser(int _broadcastId, string _userId, string _areaId, string _branchId, string _branchName2, string _roleId, string _roleName)
        {
            try
            {
                _ws.InsertNewsBroadcastUser(_broadcastId, _userId, _areaId, _branchId, _branchName2, _roleId, _roleName);
            }
            catch
            {
                _ws.Abort();
                throw;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select)]
        public int GetLastNewsBroadcastId()
        {
            try
            {
                return _ws.GetLastNewsBroadcastId();
            }
            catch
            {
                _ws.Abort();
                throw;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select)]
        public bool IsDuplicateUser(int _broadcastId, string _userId)
        {
            try
            {
                return _ws.IsDuplicateUser(_broadcastId, _userId);
            }
            catch
            {
                _ws.Abort();
                throw;
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

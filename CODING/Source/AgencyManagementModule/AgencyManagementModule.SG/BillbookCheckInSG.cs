using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using System.Data.Common;
using System.ServiceModel;
using PEA.BPM.AgencyManagementModule.SG.BillBookCheckInWCF;
using WCFExtras.Soap;

namespace PEA.BPM.AgencyManagementModule.SG
{
    public class BillbookCheckInSG : IBillbookCheckInService
    {
        private BillbookCheckInWCFServiceClient _ws;

        public BillbookCheckInSG(bool onlineConnection)
        {
            _ws = new BillbookCheckInWCFServiceClient();
            string absoluteUri = _ws.Endpoint.Address.Uri.AbsoluteUri;

            if (onlineConnection)
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Center);
                _ws = new BillbookCheckInWCFServiceClient("BasicHttpBinding_IBillbookCheckInWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Center == null) Authorization.LoadServerToken(true);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Center;
            }
            else
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Branch);
                _ws = new BillbookCheckInWCFServiceClient("BasicHttpBinding_IBillbookCheckInWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Branch == null) Authorization.LoadServerToken(false);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Branch;
            }
            _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 0);
        }

        #region IBillbookCheckInService Members

        public BillBookCheckInInfo GetBillBookCheckInInfo(string billBookId)
        {
            try
            {
                return ServiceHelper.DecompressData<BillBookCheckInInfo>(_ws.GetBillBookCheckInInfo_Compress(billBookId));
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

        public BillBookCheckInInfo GetGroupInvoiceCheckInInfo(string groupIvId, string branchId)
        {
            try
            {
                return ServiceHelper.DecompressData<BillBookCheckInInfo>(_ws.GetGroupInvoiceCheckInInfo_Compress(groupIvId, branchId));
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

        public BillBookCheckInInfo GetBillBookCheckInHistory(string billBookId)
        {
            try
            {
                return ServiceHelper.DecompressData<BillBookCheckInInfo>(_ws.GetBillBookCheckInHistory_Compress(billBookId));
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

        public BillBookCheckInInfo GetGroupInvoiceCheckInHistory(string groupIvId, string branchId)
        {
            try
            {
                return ServiceHelper.DecompressData<BillBookCheckInInfo>(_ws.GetGroupInvoiceCheckInHistory_Compress(groupIvId, branchId));
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

        public BillBookCheckInInfo GetBillBookCheckInCancel(string billBookId)
        {
            try
            {
                return ServiceHelper.DecompressData<BillBookCheckInInfo>(_ws.GetBillBookCheckInCancel_Compress(billBookId));
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

        public bool CreateBillBookCheckIn(BillBookCheckInInfo biilBookCheckIn, string branchId, string terminalId)
        {
            try
            {
                return _ws.CreateBillBookCheckIn(ServiceHelper.CompressData<BillBookCheckInInfo>(biilBookCheckIn), branchId, terminalId);
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

        public bool CancelBillBookCheckIn(BillBookCheckInInfo biilBookCheckIn)
        {
            try
            {
                return _ws.CancelBillBookCheckIn(ServiceHelper.CompressData<BillBookCheckInInfo>(biilBookCheckIn));
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

        public bool CheckIsFullyPaid(BillBookCheckInInfo biilBookCheckIn)
        {
            try
            {
                return _ws.CheckIsFullyPaid(ServiceHelper.CompressData<BillBookCheckInInfo>(biilBookCheckIn));
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

        public bool CheckIsSubmitGroupSameDay(BillBookCheckInInfo biilBookCheckIn)
        {
            try
            {
                return _ws.CheckIsSubmitGroupSameDay(ServiceHelper.CompressData<BillBookCheckInInfo>(biilBookCheckIn));
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

        public List<ChequeInfo> GetChequeList(string billBookId, string invId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<ChequeInfo>>(_ws.GetChequeList_Compress(billBookId, invId));
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

        public decimal GetAdvPaidFromPOS(string billBookId)
        {
            try
            {
                return _ws.GetAdvPaidFromPOS(billBookId);
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

        public BillBookCheckInInfo GetBookCheckInHistory(string billBookId)
        {
            try
            {
                return ServiceHelper.DecompressData<BillBookCheckInInfo>(_ws.GetBookCheckInHistory_Compress(billBookId));
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

        public void BillBookSaveState(BillBookCheckInInfo billBookCheckIn, string modifiedBy)
        {
            try
            {
                _ws.BillBookSaveState(ServiceHelper.CompressData<BillBookCheckInInfo>(billBookCheckIn), modifiedBy);
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

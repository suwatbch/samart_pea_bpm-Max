using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.CashManagementModule.Interface.Services;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using System.Data.Common;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using System.Security;
using PEA.BPM.CashManagementModule.SG.CashierWCF;
using System.ServiceModel;
using WCFExtras.Soap;

namespace PEA.BPM.CashManagementModule.SG
{
    public class CashManagementSG : ICashManagementServices
    {
        private CashierWCFServiceClient _ws;

        public CashManagementSG(bool onlineConnection)
        {
            _ws = new CashierWCFServiceClient();
            string absoluteUri = _ws.Endpoint.Address.Uri.AbsoluteUri;

            if (onlineConnection)
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Center);
                _ws = new CashierWCFServiceClient("BasicHttpBinding_ICashierWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Center == null) Authorization.LoadServerToken(true);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Center;
            }
            else
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Branch);
                _ws = new CashierWCFServiceClient("BasicHttpBinding_ICashierWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Branch == null) Authorization.LoadServerToken(false);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Branch;
            }
            _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 1, 0);
        }
               

        #region ICashManagementServices Members

        public List<CashierWorkStatusInfo> IsOpenedWork(OpenWorkParam param)
        {
            try
            {
                //return ServiceHelper.DecompressData<List<CashierWorkStatusInfo>>(_ws.IsOpenedWork(param));
                List<CashierWorkStatusInfo> temp = ServiceHelper.DecompressData<List<CashierWorkStatusInfo>>(_ws.IsOpenedWork(param));
                return temp;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public bool IsSystemInitial(string branchId, string workId)
        {
            try
            {
                return _ws.IsSystemInitial(branchId, workId);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CashierInfo> ListCashier(string keyword, string branchId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<CashierInfo>>(_ws.ListCashier(keyword, branchId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<GLBankInfo> ListGLBank(string businessPlace)
        {
            try
            {
                return ServiceHelper.DecompressData<List<GLBankInfo>>(_ws.ListGLBank(businessPlace));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<GLBankAccountInfo> ListGLBankAccount(string businessPlace, string bankId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<GLBankAccountInfo>>(_ws.ListGLBankAccount(businessPlace, bankId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public TrayMoneyInfo GetMoneyInTray(string workId)
        {
            try
            {
                int tmp = Convert.ToInt32(_ws.InnerChannel.OperationTimeout.TotalSeconds);
                _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 0);
                TrayMoneyInfo ret = ServiceHelper.DecompressData<TrayMoneyInfo>(_ws.GetMoneyInTray(workId));
                _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 0, tmp);
                return ret;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CashierMoneyTransferInfo> LoadTransferedRequestItem(string cashierId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<CashierMoneyTransferInfo>>(_ws.LoadTransferedRequestItem(cashierId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CashierMoneyTransferInfo> LoadTransferStatusItem(string cashierId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<CashierMoneyTransferInfo>>(_ws.LoadTransferStatusItem(cashierId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public void CancelTransferItem(List<String> list, string modifiedBy)
        {
            try
            {
                _ws.CancelTransferItem(ServiceHelper.CompressData<List<String>>(list), modifiedBy);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public string ResponseTransferedItems(List<String> transferId, string workId, string status, string posId, string branchId, string modifiedBy)
        {
            try
            {
                return _ws.ResponseTransferedItems(ServiceHelper.CompressData<List<String>>(transferId), workId, status, posId, branchId, modifiedBy);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public CashierMoneyFlowInfo Transfer(DbTransaction trans, MoneyTransferInfo transferMoney)
        {
            try
            {
                return ServiceHelper.DecompressData<CashierMoneyFlowInfo>(_ws.Transfer(transferMoney));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public string OpenWork(OpenWorkParam param)
        {
            try
            {
                return _ws.OpenWork(param);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public CloseWorkSummaryInfo GetCloseWorkFlowItem(string workId)
        {
            try
            {
                int tmp = Convert.ToInt32(_ws.InnerChannel.OperationTimeout.TotalSeconds);
                _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 0);
                CloseWorkSummaryInfo ret = ServiceHelper.DecompressData<CloseWorkSummaryInfo>(_ws.GetCloseWorkFlowItem(workId));
                _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 0, tmp);
                return ret;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public void CloseWork(CloseWorkSubmitInfo submitInfo)
        {
            try
            {
                _ws.CloseWork(submitInfo);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }       

        public ReportWorkFlowSummary GetWorkFlowReport(string workId)
        {
            try
            {
                int tmp = Convert.ToInt32(_ws.InnerChannel.OperationTimeout.TotalSeconds);
                _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 0);
                ReportWorkFlowSummary ret = ServiceHelper.DecompressData<ReportWorkFlowSummary>(_ws.GetWorkFlowReport(workId));
                _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 0, tmp);
                return ret;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public ReportBankPayInDetailInfo GetBankPayInDetailForReport(CashierMoneyFlowInfo flowInfo)
        {
            try
            {
                int tmp = Convert.ToInt32(_ws.InnerChannel.OperationTimeout.TotalSeconds);
                _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 0);
                ReportBankPayInDetailInfo ret = ServiceHelper.DecompressData<ReportBankPayInDetailInfo>(_ws.GetBankPayInDetailForReport(flowInfo));
                _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 0, tmp);
                return ret;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public OpenWorkInfo LoadOpeningBalance(string cashierId, string flowType)
        {
            try
            {
                return ServiceHelper.DecompressData<OpenWorkInfo>(_ws.LoadOpeningBalance(cashierId, flowType));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }
              
        public void CheckInMoney(MoneyCheckInInfo param)
        {
            try
            {
                _ws.CheckInMoney(param);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }


        public int CancelMoneyCheckedIn(CancelMoneyCheckedInInfo param)
        {
            try
            {
                return _ws.CancelMoneyCheckedIn(param);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<PaymentMethodInfo> LoadMoneyCheckedIn(string SAPRefNo, string workId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<PaymentMethodInfo>>(_ws.LoadMoneyCheckedIn(SAPRefNo, workId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<ReportDailyRemainInfo> GetHistDailyRemainReport(ReportParam param)
        {
            try
            {
                int tmp = Convert.ToInt32(_ws.InnerChannel.OperationTimeout.TotalSeconds);
                _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 0);
                List<ReportDailyRemainInfo> ret = ServiceHelper.DecompressData<List<ReportDailyRemainInfo>>(_ws.GetHistDailyRemainReport(param));
                _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 0, tmp);
                return ret;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public ReportBankPayInDetailInfo GetHistBankPayInDetailReport(ReportParam param)
        {
            try
            {
                int tmp = Convert.ToInt32(_ws.InnerChannel.OperationTimeout.TotalSeconds);
                _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 0);
                ReportBankPayInDetailInfo ret = ServiceHelper.DecompressData<ReportBankPayInDetailInfo>(_ws.GetHistBankPayInDetailReport(param));
                _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 0, tmp);
                return ret;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public ReportWorkFlowSummary GetHistWorkFlowReport(ReportParam param)
        {
            try
            {
                int tmp = Convert.ToInt32(_ws.InnerChannel.OperationTimeout.TotalSeconds);
                _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 0);
                ReportWorkFlowSummary ret = ServiceHelper.DecompressData<ReportWorkFlowSummary>(_ws.GetHistWorkFlowReport(param));
                _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 0, tmp);
                return ret;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public bool ExistSAPRefNo(string sapRefNo, string workId)
        {
            try
            {
                return _ws.ExistSAPRefNo(sapRefNo, workId);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }       

        public List<BankDeliveryInfo> ListBankDelivery(string workId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<BankDeliveryInfo>>(_ws.ListBankDelivery(workId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public void CancelBankDelivery(DbTransaction trans, BankDeliveryInfo blInfo)
        {
            try
            {
                _ws.CancelBankDelivery(blInfo);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<WorkInfo> ListAllOpenWork(string branchId)
        {
            try
            {
                int tmp = Convert.ToInt32(_ws.InnerChannel.OperationTimeout.TotalSeconds);
                _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 0);
                List<WorkInfo> ret = ServiceHelper.DecompressData<List<WorkInfo>>(_ws.ListAllOpenWork(branchId));
                _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 0, tmp);
                return ret;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public void ForceCloseWork(DbTransaction trans, WorkInfo workInfo)
        {
            try
            {
                _ws.ForceCloseWork(workInfo);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public CashierPosIdInfo LoadCashierPosId(string branchId)
        {
            try
            {
                return _ws.LoadCashierPosId(branchId);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public string IsAllWorkClosed(string workId, string branchId)
        {
            try
            {
                return _ws.IsAllWorkClosed(workId, branchId);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public void SetBaseline(DbTransaction trans, string workId, string branchId)
        {
            try
            {
                _ws.SetBaseline(workId, branchId);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<BaselineInfo> GetBaseline(string branchId, DateTime baselineDt)
        {
            try
            {
                return ServiceHelper.DecompressData<List<BaselineInfo>>(_ws.GetBaseline(branchId, baselineDt));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public string GetWorkPosId(string workId)
        {
            try
            {
                return _ws.GetWorkPosId(workId);
              
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public void SaveStartOpenBalance(MoneyCheckInInfo param)
        {
            try
            {
                _ws.SaveStartOpenBalance(param);
               
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<string> LoadSAPReference(string workId)
        {
            try
            {
                return new List<string>(_ws.LoadSAPReference(workId));
               
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        } 

        public List<ChequeInfo> GetChequeDailyRemainReport(ReportParam param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<ChequeInfo>>(_ws.GetChequeDailyRemainReport(param));
                
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<PaymentMethodInfo> LoadSystemInitial(string workId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<PaymentMethodInfo>>(_ws.LoadSystemInitial(workId));
                
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public void SaveAdjustOpenBalance(MoneyCheckInInfo param)
        {
            try
            {
                _ws.SaveAdjustOpenBalance(param);
                
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<PaymentMethodInfo> LoadAdjustOpenBalance(string workId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<PaymentMethodInfo>>(_ws.LoadAdjustOpenBalance(workId));

            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CashierInfo> GetOpenWorkCashierOfBranch(string branchId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<CashierInfo>>(_ws.GetOpenWorkCashierOfBranch(branchId));
               
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public CashierWorkStatus GetCashierWorkStatus(string workId)
        {
            try
            {
                return _ws.GetCashierWorkStatus(workId);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Cash, ex);
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

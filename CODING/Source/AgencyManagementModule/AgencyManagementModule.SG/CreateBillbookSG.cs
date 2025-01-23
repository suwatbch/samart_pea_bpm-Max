using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using System.ComponentModel;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.AgencyManagementModule.Interface.Constants;
using System.Data.Common;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.AgencyManagementModule.SG.CreateBillbookWCF;
using System.ServiceModel;
using WCFExtras.Soap;


namespace PEA.BPM.AgencyManagementModule.SG
{
    public class CreateBillbookSG : ICreateBillbookService
    {
        private CreateBillbookWCFServiceClient _ws;

        public CreateBillbookSG(bool onlineConnection)
        {
            _ws = new CreateBillbookWCFServiceClient();
            string absoluteUri = _ws.Endpoint.Address.Uri.AbsoluteUri;

            if (onlineConnection)
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Center);
                _ws = new CreateBillbookWCFServiceClient("BasicHttpBinding_ICreateBillbookWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Center == null) Authorization.LoadServerToken(true);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Center;
            }
            else
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Branch);
                _ws = new CreateBillbookWCFServiceClient("BasicHttpBinding_ICreateBillbookWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Branch == null) Authorization.LoadServerToken(false);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Branch;
            }
            _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 0);
        }

        #region ICreateBillbookService Members

        public AgentInfo BookSearchAgenctInfo(string agentId, string period)
        {
            try
            {
                return ServiceHelper.DecompressData<AgentInfo>(_ws.BookSearchAgenctInfo_Compress(agentId, period));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Agency, ex); 
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public BindingList<CallingBillSummaryInfo> DisplayBillBookSummarizeCallingBill(BillBookItemListInputInfo bookItemListInfo)
        {
            try
            {
                return ServiceHelper.DecompressData<BindingList<CallingBillSummaryInfo>>(_ws.DisplayBillBookSummarizeCallingBill_Compress(ServiceHelper.CompressData<BillBookItemListInputInfo>(bookItemListInfo)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Agency, ex); 
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public BindingList<CallingBillInfo> DisplayBillBookCallingBill(BillBookItemListInputInfo bookItemListInfo)
        {
            try
            {
                return ServiceHelper.DecompressData<BindingList<CallingBillInfo>>(_ws.DisplayBillBookCallingBill_Compress(ServiceHelper.CompressData<BillBookItemListInputInfo>(bookItemListInfo)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Agency, ex); 
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public BindingList<CallingBillInfo> DisplayBillBookNoneCallingBill(BillBookItemListInputInfo bookItemListInfo)
        {
            try
            {
                return ServiceHelper.DecompressData<BindingList<CallingBillInfo>>(_ws.DisplayBillBookNoneCallingBill_Compress(ServiceHelper.CompressData<BillBookItemListInputInfo>(bookItemListInfo)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Agency, ex); 
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public BindingList<CallingBillInfo> DisplayBillBookLineBill(BillBookItemListInputInfo bookItemListInfo, List<string> line)
        {
            try
            {
                return ServiceHelper.DecompressData<BindingList<CallingBillInfo>>(_ws.DisplayBillBookLineBill_Compress(ServiceHelper.CompressData<BillBookItemListInputInfo>(bookItemListInfo), ServiceHelper.CompressData<List<string>>(line)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Agency, ex); 
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public BindingList<CallingBillInfo> DisplayBillBookLineNoBill(BillBookItemListInputInfo bookItemListInfo, List<string> line)
        {
            try
            {
                return ServiceHelper.DecompressData<BindingList<CallingBillInfo>>(_ws.DisplayBillBookLineNoBill_Compress(ServiceHelper.CompressData<BillBookItemListInputInfo>(bookItemListInfo), ServiceHelper.CompressData<List<string>>(line)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Agency, ex); 
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public BillBookHeaderInfo CreateBillBookAndSendPrintEvent(DbTransaction trans, BillBookItemListInputInfo billBookParem)
        {
            try
            {
                return ServiceHelper.DecompressData<BillBookHeaderInfo>(_ws.CreateBillBookAndSendPrintEvent_Compress(ServiceHelper.CompressData<BillBookItemListInputInfo>(billBookParem)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Agency, ex); 
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public BillBookItemListInputInfo LoadPastBillBookInfo(string pastBillBookId)
        {
            try
            {
                return ServiceHelper.DecompressData<BillBookItemListInputInfo>(_ws.LoadPastBillBookInfo_Compress(pastBillBookId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Agency, ex); 
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public string CheckExistingReceiveCount(BillBookItemListInputInfo bookItemListInfo)
        {
            try
            {
                return _ws.CheckExistingReceiveCount(ServiceHelper.CompressData<BillBookItemListInputInfo>(bookItemListInfo));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Agency, ex); 
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public bool IsBillBookAlreadyCheckedIn(string bookId)
        {
            try
            {
                return _ws.IsBillBookAlreadyCheckedIn(bookId);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Agency, ex); 
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public AgencyBookDepositAmountInfo GetAgencyBookDepositInfo(string agentId)
        {
            try
            {
                return ServiceHelper.DecompressData<AgencyBookDepositAmountInfo>(_ws.GetAgencyBookDepositInfo_Compress(agentId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Agency, ex); 
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public string[] GetMruByCaId(string caId)
        {
            try
            {
                return _ws.GetMruByCaId(caId);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Agency, ex); 
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public string GetAgentBACode(string agencyTechBranchId)
        {
            try
            {
                return _ws.GetAgentBACode(agencyTechBranchId);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Agency, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<BillbookInfoReprint> LoadBillBookList(BookSearchStatusEnum status, string branchId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<BillbookInfoReprint>>(_ws.LoadBillBookList_Compress(ServiceHelper.CompressData<BookSearchStatusEnum>(status), branchId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Agency, ex); 
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<BillbookInfoReprint> LoadBillBookByIdKeyword(BillbookInfoReprint searchCondition, string branchId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<BillbookInfoReprint>>(_ws.LoadBillBookByIdKeyword_Compress(ServiceHelper.CompressData<BillbookInfoReprint>(searchCondition), branchId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Agency, ex); 
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public DepositBillBookAmountInfo IsOverLimitAgentDeposit(BillBookItemListInputInfo billBookParem)
        {
            try
            {
                return ServiceHelper.DecompressData<DepositBillBookAmountInfo>(_ws.IsOverLimitAgentDeposit_Compress(ServiceHelper.CompressData<BillBookItemListInputInfo>(billBookParem)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Agency, ex); 
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public bool CancelBillBook(string bookId)
        {
            try
            {
                return _ws.CancelBillBook(bookId);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Agency, ex); 
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public decimal? GetUsedDeposit(string agentId)
        {
            try
            {
                return _ws.GetUsedDeposit(agentId);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Agency, ex); 
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public bool IsAlreadyAdvPaid(string billBookId)
        {
            try
            {
                return _ws.IsAlreadyAdvPaid(billBookId);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Agency, ex); 
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public BindingList<CallingBillInfo> RetreiveBookDetail(string billBookId)
        {
            try
            {
                return ServiceHelper.DecompressData<BindingList<CallingBillInfo>>(_ws.RetreiveBookDetail_Compress(billBookId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Agency, ex); 
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public BindingList<CallingBillInfo> RetreiveBookLineDetail(string billBookId, string[] line)
        {
            try
            {
                return ServiceHelper.DecompressData<BindingList<CallingBillInfo>>(_ws.RetreiveBookLineDetail_Compress(billBookId, line));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Agency, ex); 
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public BindingList<CallingBillSummaryInfo> RetreiveBookSummary(string billBookId, string period)
        {
            try
            {
                return ServiceHelper.DecompressData<BindingList<CallingBillSummaryInfo>>(_ws.RetreiveBookSummary_Compress(billBookId, period));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Agency, ex); 
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public AgentInfo EmployeeSearchInfo(string employeeId)
        {
            try
            {
                return ServiceHelper.DecompressData<AgentInfo>(_ws.EmployeeSearchInfo_Compress(employeeId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Agency, ex); 
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<HashInfo> LoadBookItemValidationData(List<string> agentId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<HashInfo>>(_ws.LoadBookItemValidationData_Compress(ServiceHelper.CompressData<List<string>>(agentId)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Agency, ex); 
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public int IsReadyToCancelBillBook(string billBookId)
        {
            try
            {
                return _ws.IsReadyToCancelBillBook(billBookId);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Agency, ex); 
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public string GetNewBillBookId(string runningBranchId)
        {
            try
            {
                return _ws.GetNewBillBookId(runningBranchId);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Agency, ex); 
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

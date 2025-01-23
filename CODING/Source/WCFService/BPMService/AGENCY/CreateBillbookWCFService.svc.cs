using System.Collections.Generic;
using System.ComponentModel;
using PEA.BPM.AgencyManagementModule.BS;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.AgencyManagementModule.Interface.Constants;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.WebService;
using WCFExtras.Soap;


namespace BPMService.AGENCY
{

    public class CreateBillbookWCFService : ICreateBillbookWCFService
    {
        private CreateBillbookService _createBillbookService;   
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public CreateBillbookWCFService()
        {
            _createBillbookService = new CreateBillbookService();
        }

        public AgentInfo BookSearchAgenctInfo(string agentId, string period)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "BookSearchAgenctInfo",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _createBillbookService.BookSearchAgenctInfo(agentId, period);
                });
        }

        public CompressData BookSearchAgenctInfo_Compress(string agentId, string period)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "BookSearchAgenctInfo_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<AgentInfo>(_createBillbookService.BookSearchAgenctInfo(agentId, period));
                });
        }

        public BindingList<CallingBillSummaryInfo> DisplayBillBookSummarizeCallingBill(BillBookItemListInputInfo bookItemListInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "DisplayBillBookSummarizeCallingBill",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _createBillbookService.DisplayBillBookSummarizeCallingBill(bookItemListInfo);
                });
        }

        public CompressData DisplayBillBookSummarizeCallingBill_Compress(CompressData bookItemListInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "DisplayBillBookSummarizeCallingBill_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BindingList<CallingBillSummaryInfo>>(_createBillbookService.DisplayBillBookSummarizeCallingBill(ServiceHelper.DecompressData<BillBookItemListInputInfo>(bookItemListInfo)));
                });
        }

        public BindingList<CallingBillInfo> DisplayBillBookCallingBill(BillBookItemListInputInfo bookItemListInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "DisplayBillBookCallingBill",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _createBillbookService.DisplayBillBookCallingBill(bookItemListInfo);
                });
        }

        public CompressData DisplayBillBookCallingBill_Compress(CompressData bookItemListInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "DisplayBillBookCallingBill_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BindingList<CallingBillInfo>>(_createBillbookService.DisplayBillBookCallingBill(ServiceHelper.DecompressData<BillBookItemListInputInfo>(bookItemListInfo)));
                });
        }

        public BindingList<CallingBillInfo> DisplayBillBookNoneCallingBill(BillBookItemListInputInfo bookItemListInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "DisplayBillBookNoneCallingBill",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _createBillbookService.DisplayBillBookNoneCallingBill(bookItemListInfo);
                });
        }

        public CompressData DisplayBillBookNoneCallingBill_Compress(CompressData bookItemListInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "DisplayBillBookNoneCallingBill_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BindingList<CallingBillInfo>>(_createBillbookService.DisplayBillBookNoneCallingBill(ServiceHelper.DecompressData<BillBookItemListInputInfo>(bookItemListInfo)));
                });
        }

        public BindingList<CallingBillInfo> DisplayBillBookLineBill(BillBookItemListInputInfo bookItemListInfo, List<string> line)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "DisplayBillBookLineBill",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _createBillbookService.DisplayBillBookLineBill(bookItemListInfo, line);
                });
        }

        public CompressData DisplayBillBookLineBill_Compress(CompressData bookItemListInfo, CompressData line)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "DisplayBillBookLineBill_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BindingList<CallingBillInfo>>(_createBillbookService.DisplayBillBookLineBill(ServiceHelper.DecompressData<BillBookItemListInputInfo>(bookItemListInfo), ServiceHelper.DecompressData<List<string>>(line)));
                });
        }

        public BindingList<CallingBillInfo> DisplayBillBookLineNoBill(BillBookItemListInputInfo bookItemListInfo, List<string> line)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "DisplayBillBookLineNoBill",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _createBillbookService.DisplayBillBookLineNoBill(bookItemListInfo, line);
                });
        }

        public CompressData DisplayBillBookLineNoBill_Compress(CompressData bookItemListInfo, CompressData line)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "DisplayBillBookLineNoBill_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BindingList<CallingBillInfo>>(_createBillbookService.DisplayBillBookLineNoBill(ServiceHelper.DecompressData<BillBookItemListInputInfo>(bookItemListInfo), ServiceHelper.DecompressData<List<string>>(line)));
                });
        }

        public BillBookHeaderInfo CreateBillBookAndSendPrintEvent(BillBookItemListInputInfo billBookParem)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "CreateBillBookAndSendPrintEvent",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _createBillbookService.CreateBillBookAndSendPrintEvent(null, billBookParem);
                });
        }

        public CompressData CreateBillBookAndSendPrintEvent_Compress(CompressData billBookParem)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "CreateBillBookAndSendPrintEvent_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BillBookHeaderInfo>(_createBillbookService.CreateBillBookAndSendPrintEvent(null, ServiceHelper.DecompressData<BillBookItemListInputInfo>(billBookParem)));
                });
        }

        public BillBookItemListInputInfo LoadPastBillBookInfo(string pastBillBookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "LoadPastBillBookInfo",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _createBillbookService.LoadPastBillBookInfo(pastBillBookId);
                });
        }

        public CompressData LoadPastBillBookInfo_Compress(string pastBillBookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "LoadPastBillBookInfo_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BillBookItemListInputInfo>(_createBillbookService.LoadPastBillBookInfo(pastBillBookId));
                });
        }

        public string CheckExistingReceiveCount(CompressData bookItemListInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "CheckExistingReceiveCount",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _createBillbookService.CheckExistingReceiveCount(ServiceHelper.DecompressData<BillBookItemListInputInfo>(bookItemListInfo));
                });
        }

        public bool IsBillBookAlreadyCheckedIn(string bookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "IsBillBookAlreadyCheckedIn",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _createBillbookService.IsBillBookAlreadyCheckedIn(bookId);
                });
        }

        public AgencyBookDepositAmountInfo GetAgencyBookDepositInfo(string agentId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "GetAgencyBookDepositInfo",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _createBillbookService.GetAgencyBookDepositInfo(agentId);
                });
        }

        public CompressData GetAgencyBookDepositInfo_Compress(string agentId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "GetAgencyBookDepositInfo_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<AgencyBookDepositAmountInfo>(_createBillbookService.GetAgencyBookDepositInfo(agentId));
                });
        }

        public string[] GetMruByCaId(string caId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "GetMruByCaId",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _createBillbookService.GetMruByCaId(caId);
                });
        }

        public string GetAgentBACode(string agencyTechBranchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "GetAgentBACode",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _createBillbookService.GetAgentBACode(agencyTechBranchId);
                });
        }

        public List<BillbookInfoReprint> LoadBillBookList(BookSearchStatusEnum status, string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "LoadBillBookList",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _createBillbookService.LoadBillBookList(status, branchId);
                });
        }

        public CompressData LoadBillBookList_Compress(CompressData status, string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "LoadBillBookList_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<BillbookInfoReprint>>(_createBillbookService.LoadBillBookList(ServiceHelper.DecompressData<BookSearchStatusEnum>(status), branchId));
                });
        }

        public List<BillbookInfoReprint> LoadBillBookByIdKeyword(BillbookInfoReprint searchCondition, string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "LoadBillBookByIdKeyword",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _createBillbookService.LoadBillBookByIdKeyword(searchCondition, branchId);
                });
        }

        public CompressData LoadBillBookByIdKeyword_Compress(CompressData searchCondition, string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "LoadBillBookByIdKeyword_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<BillbookInfoReprint>>(_createBillbookService.LoadBillBookByIdKeyword(ServiceHelper.DecompressData<BillbookInfoReprint>(searchCondition), branchId));
                });
        }

        public DepositBillBookAmountInfo IsOverLimitAgentDeposit(BillBookItemListInputInfo billBookParem)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "IsOverLimitAgentDeposit",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _createBillbookService.IsOverLimitAgentDeposit(billBookParem);
                });
        }

        public CompressData IsOverLimitAgentDeposit_Compress(CompressData billBookParem)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "IsOverLimitAgentDeposit_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<DepositBillBookAmountInfo>(_createBillbookService.IsOverLimitAgentDeposit(ServiceHelper.DecompressData<BillBookItemListInputInfo>(billBookParem)));
                });
        }

        public bool CancelBillBook(string bookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "CancelBillBook",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _createBillbookService.CancelBillBook(bookId);
                });
        }

        public decimal? GetUsedDeposit(string agentId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "GetUsedDeposit",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _createBillbookService.GetUsedDeposit(agentId);
                });
        }

        public bool IsAlreadyAdvPaid(string billBookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "IsAlreadyAdvPaid",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _createBillbookService.IsAlreadyAdvPaid(billBookId);
                });
        }

        public BindingList<CallingBillInfo> RetreiveBookDetail(string billBookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "RetreiveBookDetail",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _createBillbookService.RetreiveBookDetail(billBookId);
                });
        }

        public CompressData RetreiveBookDetail_Compress(string billBookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "RetreiveBookDetail_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BindingList<CallingBillInfo>>(_createBillbookService.RetreiveBookDetail(billBookId));
                });
        }

        public BindingList<CallingBillInfo> RetreiveBookLineDetail(string billBookId, string[] line)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "RetreiveBookLineDetail",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _createBillbookService.RetreiveBookLineDetail(billBookId, line);
                });
        }

        public CompressData RetreiveBookLineDetail_Compress(string billBookId, string[] line)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "RetreiveBookLineDetail_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BindingList<CallingBillInfo>>(_createBillbookService.RetreiveBookLineDetail(billBookId, line));
                });
        }

        public BindingList<CallingBillSummaryInfo> RetreiveBookSummary(string billBookId, string period)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "RetreiveBookSummary",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _createBillbookService.RetreiveBookSummary(billBookId, period);
                });
        }

        public CompressData RetreiveBookSummary_Compress(string billBookId, string period)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "RetreiveBookSummary_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BindingList<CallingBillSummaryInfo>>(_createBillbookService.RetreiveBookSummary(billBookId, period));
                });
        }

        public AgentInfo EmployeeSearchInfo(string employeeId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "EmployeeSearchInfo",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _createBillbookService.EmployeeSearchInfo(employeeId);
                });
        }

        public CompressData EmployeeSearchInfo_Compress(string employeeId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "EmployeeSearchInfo_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<AgentInfo>(_createBillbookService.EmployeeSearchInfo(employeeId));
                });
        }

        public List<HashInfo> LoadBookItemValidationData(List<string> agentId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "LoadBookItemValidationData",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _createBillbookService.LoadBookItemValidationData(agentId);
                });
        }

        public CompressData LoadBookItemValidationData_Compress(CompressData agentId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "LoadBookItemValidationData_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<HashInfo>>(_createBillbookService.LoadBookItemValidationData(ServiceHelper.DecompressData<List<string>>(agentId)));
                });
        }

        public int IsReadyToCancelBillBook(string billBookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "IsReadyToCancelBillBook",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _createBillbookService.IsReadyToCancelBillBook(billBookId);
                });
        }

        public string GetNewBillBookId(string runningBranchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CreateBillbookWCFService", "GetNewBillBookId",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _createBillbookService.GetNewBillBookId(runningBranchId);
                });
        }


    }
}

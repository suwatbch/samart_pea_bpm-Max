using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ComponentModel;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.AgencyManagementModule.Interface.Constants;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using WCFExtras.Soap;


namespace BPMService.AGENCY
{
    [SoapHeaders, ServiceContract(Namespace = "http://tempuri.org")]
    public interface ICreateBillbookWCFService
    {
        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        AgentInfo BookSearchAgenctInfo(string agentId, string period);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData BookSearchAgenctInfo_Compress(string agentId, string period);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        BindingList<CallingBillSummaryInfo> DisplayBillBookSummarizeCallingBill(BillBookItemListInputInfo bookItemListInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData DisplayBillBookSummarizeCallingBill_Compress(CompressData bookItemListInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        BindingList<CallingBillInfo> DisplayBillBookCallingBill(BillBookItemListInputInfo bookItemListInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData DisplayBillBookCallingBill_Compress(CompressData bookItemListInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        BindingList<CallingBillInfo> DisplayBillBookNoneCallingBill(BillBookItemListInputInfo bookItemListInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData DisplayBillBookNoneCallingBill_Compress(CompressData bookItemListInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        BindingList<CallingBillInfo> DisplayBillBookLineBill(BillBookItemListInputInfo bookItemListInfo, List<string> line);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData DisplayBillBookLineBill_Compress(CompressData bookItemListInfo, CompressData line);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        BindingList<CallingBillInfo> DisplayBillBookLineNoBill(BillBookItemListInputInfo bookItemListInfo, List<string> line);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData DisplayBillBookLineNoBill_Compress(CompressData bookItemListInfo, CompressData line);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        BillBookHeaderInfo CreateBillBookAndSendPrintEvent(BillBookItemListInputInfo billBookParem);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData CreateBillBookAndSendPrintEvent_Compress(CompressData billBookParem);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        BillBookItemListInputInfo LoadPastBillBookInfo(string pastBillBookId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData LoadPastBillBookInfo_Compress(string pastBillBookId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        string CheckExistingReceiveCount(CompressData bookItemListInfo);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        bool IsBillBookAlreadyCheckedIn(string bookId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        AgencyBookDepositAmountInfo GetAgencyBookDepositInfo(string agentId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetAgencyBookDepositInfo_Compress(string agentId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        string[] GetMruByCaId(string caId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        string GetAgentBACode(string agencyTechBranchId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<BillbookInfoReprint> LoadBillBookList(BookSearchStatusEnum status, string branchId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData LoadBillBookList_Compress(CompressData status, string branchId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<BillbookInfoReprint> LoadBillBookByIdKeyword(BillbookInfoReprint searchCondition, string branchId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData LoadBillBookByIdKeyword_Compress(CompressData searchCondition, string branchId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        DepositBillBookAmountInfo IsOverLimitAgentDeposit(BillBookItemListInputInfo billBookParem);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData IsOverLimitAgentDeposit_Compress(CompressData billBookParem);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        bool CancelBillBook(string bookId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        decimal? GetUsedDeposit(string agentId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        bool IsAlreadyAdvPaid(string billBookId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        BindingList<CallingBillInfo> RetreiveBookDetail(string billBookId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData RetreiveBookDetail_Compress(string billBookId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        BindingList<CallingBillInfo> RetreiveBookLineDetail(string billBookId, string[] line);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData RetreiveBookLineDetail_Compress(string billBookId, string[] line);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        BindingList<CallingBillSummaryInfo> RetreiveBookSummary(string billBookId, string period);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData RetreiveBookSummary_Compress(string billBookId, string period);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        AgentInfo EmployeeSearchInfo(string employeeId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData EmployeeSearchInfo_Compress(string employeeId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<HashInfo> LoadBookItemValidationData(List<string> agentId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData LoadBookItemValidationData_Compress(CompressData agentId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        int IsReadyToCancelBillBook(string billBookId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        string GetNewBillBookId(string runningBranchId);

    }
}

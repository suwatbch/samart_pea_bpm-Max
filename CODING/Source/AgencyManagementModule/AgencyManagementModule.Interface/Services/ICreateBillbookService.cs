using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Data.Common;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.AgencyManagementModule.Interface.Constants;

namespace PEA.BPM.AgencyManagementModule.Interface.Services
{

    public interface ICreateBillbookService
    {
        AgentInfo BookSearchAgenctInfo(string agentId, string period);
        AgentInfo EmployeeSearchInfo(string employeeId);
        BindingList<CallingBillSummaryInfo> DisplayBillBookSummarizeCallingBill(BillBookItemListInputInfo bookItemListInfo);
        BindingList<CallingBillInfo> DisplayBillBookCallingBill(BillBookItemListInputInfo bookItemListInfo);
        BindingList<CallingBillInfo> DisplayBillBookNoneCallingBill(BillBookItemListInputInfo bookItemListInfo);
        BindingList<CallingBillInfo> DisplayBillBookLineBill(BillBookItemListInputInfo bookItemListInfo, List<string> line);
        BindingList<CallingBillInfo> DisplayBillBookLineNoBill(BillBookItemListInputInfo bookItemListInfo, List<string> line);
        BillBookHeaderInfo  CreateBillBookAndSendPrintEvent(DbTransaction trans, BillBookItemListInputInfo billBookParem);
        List<HashInfo> LoadBookItemValidationData(List<string> searchConn);
        //int?  GetNewReceiveCount(string agentId, string period);
        BillBookItemListInputInfo LoadPastBillBookInfo(string pastBillBookId);
        string CheckExistingReceiveCount(BillBookItemListInputInfo bookItemListInfo);
        bool IsBillBookAlreadyCheckedIn(string bookId);        
        AgencyBookDepositAmountInfo GetAgencyBookDepositInfo(string agentId);        
        string[] GetMruByCaId(string caId);
        string GetAgentBACode(string agencyTechBranchId);
        List<BillbookInfoReprint> LoadBillBookList(BookSearchStatusEnum status, string runningBranch);
        List<BillbookInfoReprint> LoadBillBookByIdKeyword(BillbookInfoReprint keyword, string branchId);
        DepositBillBookAmountInfo IsOverLimitAgentDeposit(BillBookItemListInputInfo billBookParem);
        bool CancelBillBook(string bookId);
        decimal? GetUsedDeposit(string agentId);
        bool IsAlreadyAdvPaid(string billBookId);
        BindingList<CallingBillInfo> RetreiveBookDetail(string billBookId);
        BindingList<CallingBillInfo> RetreiveBookLineDetail(string billBookId, string[] line);
        BindingList<CallingBillSummaryInfo> RetreiveBookSummary(string billBookId, string period);
        int IsReadyToCancelBillBook(string billBookId);
        string GetNewBillBookId(string runningBranchId);
    }
}

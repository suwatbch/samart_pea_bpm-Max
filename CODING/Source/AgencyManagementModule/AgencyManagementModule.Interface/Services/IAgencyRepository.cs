using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using System.Data;
using PEA.BPM.AgencyManagementModule.Interface.Constants;

namespace PEA.BPM.AgencyManagementModule.Interface.Services
{
    public interface IAgencyRepository
    {
        IAgencyRepository NewInstance();

        #region Get ONE Returned Record/Value Members

        decimal? GetAgentActiveBookAmount(string agentId);
        decimal? GetAgentBookAdvPaidAmount(string agentId);
        bool HasBillbookCaculated(string bookId);
        bool IsInvoiceCancelled(string invoiceNo);
        int? GetBookLotNumber(string agentId, string period);
        FeeBaseElement GetBaseCommissionRate(BookSearchInfo searchInfo);
        FeeBaseElement GetBaseCommissionRate(string branchId);
        string GetAgentIdByBookId(string bookId);
        int GetNewCalCountOfCommissionPeriod(string agentId, string period);
        int GetBillBookCountByCreateDate(string bookPeriod, string agentId, DateTime createDt);
        bool GetBillBookCountNotPaid(string billBookId);
        bool IsAlreadyAdvPaid(string billBookId);
        BindingList<CallingBillInfo> RetreiveBookDetail(string billBookId);
        BindingList<CallingBillInfo> RetreiveBookLineDetail(string billBookId, string[] line);
        BindingList<CallingBillSummaryInfo> RetreiveBookSummary(string billBookId, string period);
        DataTable GetBillBookIdWithCutStatusByAgentId(string bookPeriod, string agentId, DateTime createDt);
        DataTable GetBillBookIdWithCutStatusByAgentIdReprint(string bookPeriod, string agentId, DateTime createDt);
        bool IsBillBookAlreadyCheckedIn(string bookId);
        bool IsBillBookAlreadyCancel(string bookId);
        bool IsBillBookAlreadyCreate(string bookId);
        string GetBusinessPartnerTypeOfAgent(string agentId);
        AgentInfo GetEmployeeInformation(string employeeId);
        AgentInfo GetAgentInformation(string agentId);
        bool IsChildBranch(string parent, string child);
        string IsReceiveCountExist(BillBookHeaderInfo header);
        PeaInfo GetBranchInformation(string branchId);
        string[] GetLineId(string caId);
        LineInfo GetLineInformation(string lineId, string branchId);
        int? GetNewReceiveCountOfAgent(string agentId, string period, int bookLot);
        DataTable GetBillBookHeaderInfo(string billBookId);
        List<LineInfo> GetMaxLineCollecctCount(string agencyId, string period);
        List<LineInfo> GetLineByCreateDate(string agencyId, string Period, DateTime createDate);
        #endregion

        #region Select Table Members

        List<CalculatedCommissionInfo> SelectCalculatedAgentCommission(BookSearchInfo searchInfo);
        List<CommissionHistoryInfo> SelectCommissionHistory(BookSearchInfo searchInfo);
        List<BillbookInfoReprint> SelectBillBookByIdKeyword(string branchId, BillbookInfoReprint searchCondition);
        List<BillbookInfoReprint> SelectBillBookListByStatus(BookSearchStatusEnum status, string runningBranch);
        List<AgencyBillCollectionMasterReport> GetAgencyBillCollection(string period, string branchId, string branchName);
        List<AgencyBillCollectionDetailReport> GetAgencyBillCollectionDetail(string period);
        BindingList<LineInfo> SearchAgencyMru(string branchId, string mruId);
        int GetNumOfMruInBranch(string branchId);
        List<PeaInfo> SelectBranchLevelFour(string branchId);
        List<PeaInfo> SelectBranchByKeyword(string keyword, string branchId);
        List<AgentInfo> SelectAgentsByKeyword(string keyword, int searchType, string branchId);
        List<AgentInfo> SelectAgentsByDepositOperand(decimal deposit, string operand, string branchId);
        DataTable SelectBillbooksByAgentId(string agentId, string status);
        DataTable SelectBillBookInputItemSet(string billBookId);
        DataTable SelectBillBookInputItem(string billBookId, string filterType);
        DataTable SelectBillBookCreateDate(string agentId, string period);
        DateTime SelectBillBookReturnDate(string agentId, string period, DateTime createDt);
        DataTable SelectBillBookCountRange(string agentId, string period, DateTime createDt);
        DataTable SelectAgencyPaymentStatus();
        DataTable SelectCaBillStatusByBookId(string bookId);
        DataTable SelectBillStatusInfoByCaId(string branchId, string mruId, string caId);
        string GetBookCallingStatus(string bookId);
        DataTable SelectBillStatusInfoByCaId(string branchId, string mruId, string caId, string period, bool curMPeriod);
        DataTable SelectBillStatusInfo(string branchId, string mruId, string controllerId);
        DataTable SelectBillStatusInfo(string branchId, string mruId, string period, bool curMPeriod);
        int SelectBookItemPutInvoiceByBookId(string bookId);
        decimal? GetBookTotalAmount(string bookId);
        DataTable SelectARAdvPaymentAmount(string bookId);
        DataTable SelectARBookPaymentAmount(string bookId);
        DataTable GetPaymentDateBook(string bookId);
        int GetBookItemPutInvPastThreeMonths(string bookId, string period);
        int GetBookItemCount(string bookId);
        BindingList<LineInfo> SelectMRUsByAgentId(string agentId);
        BindingList<LineInfo> SelectMRUsByEmpNoAgencyId(List<string> searchConn);

        #endregion

        #region [Insert] Record Members

        void NewEmployeeContractAccount(DbTransaction trans, BillBookHeaderInfo header, string postedServerId);
        void NewEmployeeContractBranch(DbTransaction trans, string caId, string branchId);
        HelpOfferInfo GetCommissionHelpInfo(BookSearchInfo searchInfo);
        bool IsFineEnabled(BookSearchInfo searchInfo);
        string SaveAgencyCommission(DbTransaction trans, decimal? transCost, decimal? fh, decimal? sp, int calCount, string runningBranchId,
                                    decimal? taxAmount, decimal? cmAmount, decimal? fineAmount, decimal? vatAmount, string fineEnabled,
                                    decimal? baseCmAmount, decimal? specialCmAmount, decimal? invCmAmount, bool overNinety, string modifiedBy, string postbranchserverId);
        void InsertCommissionBookItem(DbTransaction trans, string cmId, string bookId, string modifiedBy, string postBranchServerId);
        void DeleteAgencyMru(DbTransaction trans, string agencyId, string branchId, string modifiedBy);
        int InsUpdLineOfAgent(DbTransaction trans, string agentId, string lineId, string branchId, string postedServerId,
                              string modifiedBy);

        string CreateBillBook(DbTransaction trans, BillBookHeaderInfo header, string postedServerId);
        string GetNewBillBookId(string runningBranchId);
        void UpdateMRUPlanCreateBillBook(DbTransaction trans, DateTime createDate, string bookId);
        void UpdateMRUPlanCheckInBillBook(DbTransaction trans, DateTime checkInDate, string bookId);
        void InsertBillBookItem(DbTransaction trans, BillStatusInfo bsInfo, string postedServerId, string modifiedBy);

        void InsertBillBookInputSet(DbTransaction trans, string bookId, string branchId, string mruId,
                                    string periodType, string outType, string postedServerId, string modifiedBy);

        void InsertBillBookInputExtraItem(DbTransaction trans, string bookId, string branchId,
                                          string mruId, string filterType, string caId, string postedServerId,
                                          string modifiedBy);

        #endregion

        #region [Update] Record Members

        void UpdateCommissionRate(DbTransaction trans, FeeBaseElement comRate);

        int UpdateMRUSpecialHelpAndAdvPay(DbTransaction trans, string lineId, string branchId, string advPay,
                                          string modifiedBy, LineInfo line);

        void UpdateBillStatusInfoOfBeingCreatedBook(DbTransaction trans, string invoiceNo, string inBook, string aboId);
        void UpdateAdvanceBookPayment(DbTransaction trans, string bookId, decimal? advPay);
        bool CancelBillBook(string bookId);
        bool CancelBillBook(DbTransaction trans, string bookId);

        #endregion

        #region Delete Record Members
        #endregion

        #region Bill Book Creation Exclusively Used

        DataTable PrepareBillBookItemList(BillBookItemListInputInfo billBookParem, ref DataTable billFilteredOut);
        BindingList<CallingBillSummaryInfo> FillCallingBillSummaryInformation(DataTable dt, string period);
        BindingList<CallingBillInfo> FillCallingBillInfo(DataTable dt);
        BindingList<CallingBillSummaryInfo> FindBookSummarizdInformation(BillBookItemListInputInfo billBookParem);
        BindingList<CallingBillInfo> FindCallingBillInformation(BillBookItemListInputInfo billBookParem);
        BindingList<CallingBillInfo> FindNoneCallingBillInformation(BillBookItemListInputInfo billBookParem);

        BindingList<CallingBillInfo> FindLineCallingBillInformation(BillBookItemListInputInfo billBookParem,
                                                                    List<string> line);

        BindingList<CallingBillInfo> FindLineNoneCallingBillInformation(BillBookItemListInputInfo billBookParem,
                                                                        List<string> line);

        void FilterByPaidByCentralBranch(DataTable billTb, DataTable billNotToBook);
        void FilterByBillCancelStatus(DataTable billTb, DataTable billNotToBook);
        void FilterByInBookFlag(DataTable billTb, DataTable billNotToBook);
        void FilterRepeatedBill(DataTable billTb, char outStatus, DataTable billNotToBook);
        void RemoveExceptionalBill(DataTable billTb, List<CallingBillInfo> exceptionalList, DataTable billNotToBook);
        void FilterBillPaymentStatus(DataTable billTb, char payStatus, bool equal, DataTable billNotToBook);
        void FilterByLineId(DataTable billTb, string peaCode, string lineId);
        DataTable FilterBillPeriod(DataTable billTb, string period, bool rmOldBill);
        DataTable FilterBillPeriod(DataTable billTb, bool rmOldBill);

        void ProcessExtraBookItem(BindingList<BillBookCreationExtraInfo> eInfo, string billPeriod, ref DataTable billTb,
                                  ref DataTable billNotToBook, string period, string line);

        PeaInfo GetBranchInfoByAgencyId(string agencyId);

        #endregion


    }
}

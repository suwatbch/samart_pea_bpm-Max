using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;

namespace PEA.BPM.AgencyManagementModule.Interface.Services
{
    public interface IBillBookRepository
    {
        #region "Select command"

        List<BillBookPrePaidInfo> GetBillBookPrePaid(string billBookId);
        decimal GetAdvancePaymentByMRU(DbTransaction trans, string bookId);
        decimal GetPrePaidAccountReceive(DbTransaction trans, string bookId);
        BillBookCheckInInfo GetBillCheckInCancel(string billBookId);
        BillBookCheckInInfo GetBillCheckInInfomation(string bookId, bool isHistory);
        BillBookCheckInInfo GetGroupInvoiceCheckInInfomation(string groupIvId, string branchId, bool isHistory);
        List<BillBookCheckinDetailInfo> GetBillBookCheckInDetail(string bookId, string period);
        List<BillBookCheckinDetailInfo> GetGroupInvoiceCheckInDetail(string groupIvId, string branchId);
        List<BillBookCheckinDetailInfo> GetBillBookCheckInHistoryDetail(string bookId, string period);
        List<BillBookCheckinDetailInfo> GetGroupInvoiceCheckInHistoryDetail(string groupIvId, string branchId);
        HashInfoCollection GetAbsInformation(string absId);
        HashInfoCollection GetBillStatusInformation(string bsId);
        string GetContractTypeInformation(string ctId);
        HashInfoCollection GetPmInformation(string pmId);
        List<BillBookDetailReportListInfo> GetBillBookDetailReportList(string billBookId);
        List<BillBookInfoDetailReport> GetBillBookInfoDetailReportList(string billBookId);

        List<CAB02_DetailReportInfo> GetAgencyMoneyReturnReportList(string agencyIdFrom, string agencyIdTo,
                                                                    string periodFrom, string periodTo, string branchId);

        List<ChequeInfo> GetChequeInfo(string billBookId, string invoiceNo);
        string GetCaPaidBy(DbTransaction trans, List<BillBookCheckinDetailInfo> billBookDetails);
        decimal? GetTaxRate(string vatCode);
        DateTime? GetGroupInvoiceDueDate(DbTransaction trans, List<BillBookCheckinDetailInfo> billBookDetails);
        string GetTaxCode(DbTransaction trans, List<BillBookCheckinDetailInfo> billBookDetails);
        string GetCaByInvoiceNo(DbTransaction trans, string invoiceNo);
        string GetBillKeeperNameByBillBook(string billBookId);
        #endregion

        #region "Insert command"

        string InsertAccountReceive(DbTransaction trans, AccountReceiveInfo ar, string postedServerId, string posId);
        string InsertPayment(DbTransaction trans, PaymentInfo payment, string postServer);

        string InsertARPaymentType(DbTransaction trans, ARPaymentTypeInfo arPayment, string invoiceNo, string period,
                                   string branchId, string postServerId, string posId);

        int InsertARPayment(DbTransaction trans, string billBookId, string invoiceNo, string period, string modifierBy,
                            string pmId, string branchId, int action, string postServerId, decimal? totalAmount,
                            string posId);

        int InsertGroupInvoiceARPayment(DbTransaction trans, decimal? paidAmount, string billBookId, string invoiceNo,
                                        string period, string modifierBy,
                                        string pmId, string branchId, int action, string postServerId, int isLastrec,
                                        string posId);

        BillBookHeaderInfo GetBillCollectedCountByAgecyCreateDate(string agencyId, string period, DateTime createDate);

        void BillBookSaveState(DbTransaction trans, string billbookId, string invoiceNo, string flag, string modifiedBy);

        #endregion

        #region "Update command"

        int UpdateARStatus(DbTransaction trans, string invoiceNo, string period, string modifierBy, string billBookId,
                           string postServerId);

        int UpdateAgencyBillBookStatus(DbTransaction trans, string modifiedBy,
                                      string billBookId, string aboId, string allowRepeated,
                                      string absId, string invoiceNo, string inBook,
                                      decimal? paidAmount, DateTime? lastPaidDt, DateTime? modifiedDt,
                                      int paidType, string postServerId);

        int UpdateTotalCollected(DbTransaction trans, string billBookId, string modifiedBy);

        #endregion

        #region "Delete command"

        bool DeleteARInformation(DbTransaction trans, BillBookCheckInInfo billBookInfo, string modifiedBy);
        //int DeleteGroupInvoiceARInformation(DbTransaction trans, string groupInviceNo, int returnTime, string modifiedBy);
        bool DeleteBillBookCheckIn(DbTransaction trans, BillBookCheckInInfo billBookCheckIn, string modifiedBy);
        bool DeleteBillBookCheckInDetail(DbTransaction trans, string billBookId, string invoiceNo, string modifiedBy,
                                         DateTime? modifiedDt);

        #endregion

        #region "Helper"

        string ConvertToShortThaiDateTime(DateTime? dateValue);
        #endregion
    }
}

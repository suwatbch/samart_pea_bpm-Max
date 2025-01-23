using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;

namespace PEA.BPM.AgencyManagementModule.Interface.Services
{
    
    public interface IBillbookCheckInService
    {
        BillBookCheckInInfo GetBookCheckInHistory(string billBookId);
        BillBookCheckInInfo GetBillBookCheckInHistory(string billBookId);
        BillBookCheckInInfo GetGroupInvoiceCheckInHistory(string billBookId, string branchId);
        BillBookCheckInInfo GetBillBookCheckInInfo(string billBookId);
        BillBookCheckInInfo GetBillBookCheckInCancel(string billBookId);
        BillBookCheckInInfo GetGroupInvoiceCheckInInfo(string groupIvId, string branchId);
        bool CreateBillBookCheckIn(BillBookCheckInInfo biilBookCheckIn, string branchId, string terminalId);
        bool CheckIsFullyPaid(BillBookCheckInInfo biilBookCheckIn);
        bool CheckIsSubmitGroupSameDay(BillBookCheckInInfo biilBookCheckIn);
        bool CancelBillBookCheckIn(BillBookCheckInInfo biilBookCheckIn);
        List<ChequeInfo> GetChequeList(string billBookId, string invId);                        
        decimal GetAdvPaidFromPOS(string billBookId);
        void BillBookSaveState(BillBookCheckInInfo billBookCheckIn, string modifiedBy);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.AgencyManagementModule.SG;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.AgencyManagementModule.Services
{
    [Service(typeof(IBillbookCheckInService))]
    public class BillbookCheckInServiceSwitcher : IBillbookCheckInService
    {
        public BillbookCheckInServiceSwitcher()
		{
        }

        #region Service Factory
        public IBillbookCheckInService GetService()
         {
            return GetService(false);
        }

        public IBillbookCheckInService GetService(bool serverService)
        {
            if (serverService || Session.Branch.OnlineConnection)
            {
                return new BillbookCheckInSG(true);
            }
            else
            {
                return new BillbookCheckInSG(false);
            }
        }
        #endregion

        #region IBillbookCheckInService Members

        public BillBookCheckInInfo GetBillBookCheckInInfo(string billBookId)
        {
            return GetService().GetBillBookCheckInInfo(billBookId);
        }

        public BillBookCheckInInfo GetGroupInvoiceCheckInInfo(string groupInvoiceNo, string branchId)
        {
            return GetService().GetGroupInvoiceCheckInInfo(groupInvoiceNo, branchId);
        }

        public BillBookCheckInInfo GetBillBookCheckInCancel(string billBookId)
        {
            return GetService().GetBillBookCheckInCancel(billBookId);
        }

        public bool CreateBillBookCheckIn(BillBookCheckInInfo biilBookCheckIn, string branchId, string branchName)
        {
            return GetService().CreateBillBookCheckIn( biilBookCheckIn, branchId, branchName);
        }

        public bool CancelBillBookCheckIn( BillBookCheckInInfo biilBookCheckIn)
        {
            return GetService().CancelBillBookCheckIn( biilBookCheckIn);
        }

        public bool CheckIsFullyPaid(BillBookCheckInInfo biilBookCheckIn)
        {
            return GetService().CheckIsFullyPaid(biilBookCheckIn);
        }

        public bool CheckIsSubmitGroupSameDay(BillBookCheckInInfo biilBookCheckIn)
        {
            return GetService().CheckIsSubmitGroupSameDay(biilBookCheckIn);
        }

        public BillBookCheckInInfo GetBillBookCheckInHistory(string billBookId)
        {
            return GetService().GetBillBookCheckInHistory(billBookId);
        }

        public BillBookCheckInInfo GetGroupInvoiceCheckInHistory(string groupInvoiceNo, string branchId)
        {
            return GetService().GetGroupInvoiceCheckInHistory(groupInvoiceNo, branchId);
        }

        public List<ChequeInfo> GetChequeList(string billBookId, string invId)
        {
            return GetService().GetChequeList(billBookId, invId);
        }
       
        public decimal GetAdvPaidFromPOS(string billBookId)
        {
            return GetService().GetAdvPaidFromPOS(billBookId);
        }
    
        public BillBookCheckInInfo GetBookCheckInHistory(string billBookId)
        {
            return GetService().GetBookCheckInHistory(billBookId);
        }

        public void BillBookSaveState(BillBookCheckInInfo billBookCheckIn, string modifiedBy)
        {
            GetService().BillBookSaveState(billBookCheckIn, modifiedBy);
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using System.ComponentModel;
using PEA.BPM.AgencyManagementModule.SG;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.AgencyManagementModule.Interface.Constants;

namespace PEA.BPM.AgencyManagementModule.Services
{
    [Service(typeof(ICreateBillbookService))]
    public class CreateBillbookServiceSwitcher : ICreateBillbookService
    {
        public CreateBillbookServiceSwitcher()
		{
        }

        #region Service Factory
        public ICreateBillbookService GetService()
        {
            return GetService(false);
        }

        public ICreateBillbookService GetService(bool serverService)
        {
            if (serverService || Session.Branch.OnlineConnection)
            {
                return new CreateBillbookSG(true);
            }
            else
            {
                return new CreateBillbookSG(false);
            }
        }
        #endregion


        #region ICreateBillbookService Members

        public AgentInfo BookSearchAgenctInfo(string agentId, string period)
        {
            return GetService().BookSearchAgenctInfo(agentId, period);
        }

        public BindingList<CallingBillSummaryInfo> DisplayBillBookSummarizeCallingBill(BillBookItemListInputInfo bookItemListInfo)
        {
            return GetService().DisplayBillBookSummarizeCallingBill(bookItemListInfo);
        }

        public BindingList<CallingBillInfo> DisplayBillBookCallingBill(BillBookItemListInputInfo bookItemListInfo)
        {
            return GetService().DisplayBillBookCallingBill(bookItemListInfo);
        }

        public BindingList<CallingBillInfo> DisplayBillBookNoneCallingBill(BillBookItemListInputInfo bookItemListInfo)
        {
            return GetService().DisplayBillBookNoneCallingBill(bookItemListInfo);
        }

        public BindingList<CallingBillInfo> DisplayBillBookLineBill(BillBookItemListInputInfo bookItemListInfo, List<string> line)
        {
            return GetService().DisplayBillBookLineBill(bookItemListInfo, line);
        }

        public BindingList<CallingBillInfo> DisplayBillBookLineNoBill(BillBookItemListInputInfo bookItemListInfo, List<string> line)
        {
            return GetService().DisplayBillBookLineNoBill(bookItemListInfo, line);
        }

        public BillBookHeaderInfo CreateBillBookAndSendPrintEvent(System.Data.Common.DbTransaction trans, BillBookItemListInputInfo billBookParem)
        {            
            return GetService().CreateBillBookAndSendPrintEvent(trans, billBookParem);
        }

        public List<HashInfo> LoadBookItemValidationData(List<string> searchConn)
        {
            return GetService().LoadBookItemValidationData(searchConn);
        }
     

        public BillBookItemListInputInfo LoadPastBillBookInfo(string pastBillBookId)
        {
            return GetService().LoadPastBillBookInfo(pastBillBookId);
        }

        public string CheckExistingReceiveCount(BillBookItemListInputInfo bookItemListInfo)
        {
            return GetService().CheckExistingReceiveCount(bookItemListInfo);
        }

        public bool IsBillBookAlreadyCheckedIn(string bookId)
        {
            return GetService().IsBillBookAlreadyCheckedIn(bookId);
        }

        public AgencyBookDepositAmountInfo GetAgencyBookDepositInfo(string agentId)
        {
            return GetService().GetAgencyBookDepositInfo(agentId);
        }

        public string[] GetMruByCaId(string caId)
        {
            return GetService().GetMruByCaId(caId);
        }

        public string GetAgentBACode(string agencyTechBranchId)
        {
            return GetService().GetAgentBACode(agencyTechBranchId);
        }

        public List<BillbookInfoReprint> LoadBillBookList(BookSearchStatusEnum status, string branchId)
        {
            return GetService().LoadBillBookList(status, branchId);
        }

        public List<BillbookInfoReprint> LoadBillBookByIdKeyword(BillbookInfoReprint searchCondition, string branchId)
        {
            return GetService().LoadBillBookByIdKeyword(searchCondition, branchId);
        }

        public DepositBillBookAmountInfo IsOverLimitAgentDeposit(BillBookItemListInputInfo billBookParem)
        {
            return GetService().IsOverLimitAgentDeposit(billBookParem);
        }

        public bool CancelBillBook(string bookId)
        {
           return GetService().CancelBillBook(bookId);
        }

        public decimal? GetUsedDeposit(string agentId)
        {
            return GetService().GetUsedDeposit(agentId);
        }


        public bool IsAlreadyAdvPaid(string billBookId)
        {
            return GetService().IsAlreadyAdvPaid(billBookId);
        }

        public BindingList<CallingBillInfo> RetreiveBookDetail(string billBookId)
        {
            return GetService().RetreiveBookDetail(billBookId);
        }

        public BindingList<CallingBillInfo> RetreiveBookLineDetail(string billBookId, string[] line)
        {
            return GetService().RetreiveBookLineDetail(billBookId, line);
        }

        public BindingList<CallingBillSummaryInfo> RetreiveBookSummary(string billBookId, string period)
        {
            return GetService().RetreiveBookSummary(billBookId, period);
        }       

        public AgentInfo EmployeeSearchInfo(string employeeNo)
        {
            return GetService().EmployeeSearchInfo(employeeNo);
        }      

        public int IsReadyToCancelBillBook(string billBookId)
        {
            return GetService().IsReadyToCancelBillBook(billBookId);
        }

        public string GetNewBillBookId(string runningBranchId)
        {
            //check max billbookid
            
            string onlineBookId =  GetService(true).GetNewBillBookId(runningBranchId);
            string offlineBookId = String.Empty;
            string retBookId = String.Empty;
            // if local db need to check both online and local
            if (!Session.Branch.OnlineConnection)
            {
                offlineBookId = GetService(false).GetNewBillBookId(runningBranchId);
                if (offlineBookId.Substring(6, offlineBookId.Length - 6).CompareTo(onlineBookId.Substring(6, onlineBookId.Length - 6)) > 0)
                {
                    retBookId = offlineBookId;
                }
                else 
                {
                    retBookId = onlineBookId;
                }
            }
            else 
            {
                retBookId = onlineBookId;
            }
            return retBookId;
        }

        #endregion
    }
}

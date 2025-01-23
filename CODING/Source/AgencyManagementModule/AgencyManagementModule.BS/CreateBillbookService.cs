using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.AgencyManagementModule.BS.Constants;
using PEA.BPM.AgencyManagementModule.DA;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.AgencyManagementModule.Interface.Constants;

namespace PEA.BPM.AgencyManagementModule.BS
{
    public class CreateBillbookService : ICreateBillbookService
    {
        private DateTimeFormatInfo _th_dt;

        public CreateBillbookService()
        {
            CultureInfo th_culture = new CultureInfo("th-TH");
            _th_dt = th_culture.DateTimeFormat;
        }

        public BindingList<CallingBillSummaryInfo> DisplayBillBookSummarizeCallingBill(BillBookItemListInputInfo bookItemListInfo)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                //Iterate every elements in line search list, then display bills of earch element
                BindingList<CallingBillSummaryInfo> sumInfo = agentDa.FindBookSummarizdInformation(bookItemListInfo);
                return sumInfo;
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public BindingList<CallingBillInfo> DisplayBillBookCallingBill(BillBookItemListInputInfo bookItemListInfo)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                //Iterate every elements in line search list, then display bills of earch element
                BindingList<CallingBillInfo> callingInfo = agentDa.FindCallingBillInformation(bookItemListInfo);
                return callingInfo;
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public BindingList<CallingBillInfo> DisplayBillBookLineBill(BillBookItemListInputInfo bookItemListInfo, List<string> line)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                //Iterate every elements in line search list, then display bills of earch element
                BindingList<CallingBillInfo> callingInfo = agentDa.FindLineCallingBillInformation(bookItemListInfo, line);
                return callingInfo;
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public string[] GetMruByCaId(string caId)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                return agentDa.GetLineId(caId);
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public BindingList<CallingBillInfo> DisplayBillBookLineNoBill(BillBookItemListInputInfo bookItemListInfo, List<string> line)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                //Iterate every elements in line search list, then display bills of earch element
                BindingList<CallingBillInfo> callingInfo = agentDa.FindLineNoneCallingBillInformation(bookItemListInfo, line);
                return callingInfo;
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public BindingList<CallingBillInfo> DisplayBillBookNoneCallingBill(BillBookItemListInputInfo bookItemListInfo)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                //Iterate every elements in line search list, then display bills of earch element
                BindingList<CallingBillInfo> callingInfo = agentDa.FindNoneCallingBillInformation(bookItemListInfo);
                return callingInfo;
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public AgentInfo BookSearchAgenctInfo(string agentId, string period)
        {
            AgentInfo agentInfo = null;
            AgencyDataAccess agentDa = new AgencyDataAccess();

            try
            {
                agentInfo = agentDa.GetAgentInformation(agentId);
                if (agentInfo.Id != null)
                {
                    // decreate time from calling web service
                    agentInfo.UseDeposit = GetUsedDeposit(agentId);
                }
                else
                {
                    agentInfo = agentDa.GetEmployeeInformation(agentId);
                }

                if (period != String.Empty)
                {
                    agentInfo.BookLot = GetBookLotNumber(agentId, DaHelper.SetBillPeriod(period)).Value;
                    agentInfo.ReceiveCount = GetNewReceiveCount(agentId, period, agentInfo.BookLot).Value;
                }
            }
            catch (Exception e)
            {                
                throw;
            }

            return agentInfo;

        }

        public AgentInfo EmployeeSearchInfo(string employeeId)
        {
            AgentInfo empInfo = null;
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                empInfo = agentDa.GetEmployeeInformation(employeeId);
            }
            catch (Exception e)
            {

                throw;
            }

            return empInfo;
        }

        public AgencyBookDepositAmountInfo GetAgencyBookDepositInfo(string agentId)
        {
            AgencyBookDepositAmountInfo dpInfo = new AgencyBookDepositAmountInfo();
            AgentInfo agInfo = BookSearchAgenctInfo(agentId, String.Empty);
            decimal? totalAmountInBooks = GetGrandAmountBillbookOfAgent(agentId);
            dpInfo.AgencyDeposit = agInfo.Deposit;
            dpInfo.TotalBookAmount = totalAmountInBooks;
            return dpInfo;
        }

        public string GetAgentBACode(string agencyTechBranchId)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try 
            {
                return agentDa.GetAgentBACode(agencyTechBranchId);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        private decimal? GetGrandAmountBillbookOfAgent(string agentId)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                //only normal status billbook
                DataTable books = agentDa.SelectBillbooksByAgentId(agentId, "N");
                decimal? total = 0;
                decimal? subTotal = 0;
                foreach (DataRow r in books.Rows)
                {
                    string bookId = DaHelper.GetString(r, "BookId");
                    subTotal = agentDa.GetBookTotalAmount(bookId);
                    if (subTotal != null)
                        total += subTotal;
                }

                
                return total;
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        private int? GetNewReceiveCount(string agentId, string period, int bookLot)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            int? rc = 0;

            try
            {
                rc = agentDa.GetNewReceiveCountOfAgent(agentId, period, bookLot);
            }
            catch (Exception e)
            {

                throw;
            }

            return rc;
        }

        //create billbook call function
        private void StoreScreenInput(DbTransaction trans, BillBookItemListInputInfo billBookParem, string bookId)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                BindingList<BillBookCreationInfo> bbCIs = billBookParem.CreationItemList;

                string postedServerId = Session.Branch.PostedServerId;
                string modifiedBy = billBookParem.HeaderInfo.ModifiedBy;
                //Insert screen input item for historical
                foreach (BillBookCreationInfo ci in bbCIs)
                {
                    agentDa.InsertBillBookInputSet(trans, bookId, ci.PeaCode, ci.LineId, ci.BillPeriod,
                        ci.CallingBill, postedServerId, modifiedBy);
                }

                BindingList<BillBookCreationExtraInfo> extraList = billBookParem.BookExtraItems;
                foreach (BillBookCreationExtraInfo item in extraList)
                {
                    agentDa.InsertBillBookInputExtraItem(trans, bookId, item.NPeaCode, item.NLineId,
                        item.FilterType, item.Number, postedServerId, modifiedBy);
                }
                
            }
            catch (Exception e)
            {
                
                throw;
            }


        }

        public bool CancelBillBook(string bookId)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            bool retVal = false;
            try
            {
                retVal = agentDa.CancelBillBook(bookId);
                return retVal;
            }
            catch (Exception e)
            {                
                throw ;
            }
        }

        public bool CancelBillBook(DbTransaction trans, string bookId)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            bool retVal = false;
            try
            {
                retVal = agentDa.CancelBillBook(bookId);
                return retVal;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool IsFirstTimeCallingBook(string bookId)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                string ret = agentDa.GetBookCallingStatus(bookId);
                
                if (ret == "N") return true;
                else return false;
            }
            catch (Exception e)
            {                
                throw;
            }
        }

        private int? GetBookLotNumber(string agentId,string  period)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                return agentDa.GetBookLotNumber(agentId, period);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private decimal GetBillAmount(DataTable billTb)
        {
            decimal? totalBillAmount = 0;
            foreach (DataRow r in billTb.Rows)
            {
                decimal? billAmount = DaHelper.GetDecimal(r, "TotalAmount");
                totalBillAmount += billAmount.Value;
            }

            return totalBillAmount.Value;
        }

        public decimal? GetUsedDeposit(string agentId)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            decimal? totalBookAmount = agentDa.GetAgentActiveBookAmount(agentId);
            decimal? advPaidAmount = agentDa.GetAgentBookAdvPaidAmount(agentId);
            decimal usedDeposit = totalBookAmount.Value - advPaidAmount.Value;
            return usedDeposit;
        }

        public DepositBillBookAmountInfo IsOverLimitAgentDeposit(BillBookItemListInputInfo billBookParem)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                //get all bills of this book - filtered by BillPeriod type
                DataTable billNotToBook = null;
                DataTable billToBook = agentDa.PrepareBillBookItemList(billBookParem, ref billNotToBook);

                DepositBillBookAmountInfo depInfo = new DepositBillBookAmountInfo();
                depInfo.IsOverLimit = false;

                if (billBookParem.HeaderInfo.IsAgency)
                {
                    //agency check total asset, comparing with total amount of the being created billbook
                    decimal? totalBookAmount = agentDa.GetAgentActiveBookAmount(billBookParem.HeaderInfo.AgentId);
                    decimal totalBillAmount = GetBillAmount(billToBook);
                    decimal? advPaidAmount = agentDa.GetAgentBookAdvPaidAmount(billBookParem.HeaderInfo.AgentId);
                    decimal? totalAsset = billBookParem.HeaderInfo.TotalAsset;
                    decimal? grandBookTotal = (totalBillAmount + totalBookAmount) - advPaidAmount; //minus 30%
                    decimal? remainAmount = totalAsset - totalBookAmount;

                    if (grandBookTotal > totalAsset)
                    {
                        depInfo.IsOverLimit = true;
                        depInfo.AdvPaidAmount = advPaidAmount;
                        depInfo.TotalBookAmount = totalBookAmount;
                        depInfo.TotalBillAmount = totalBillAmount;
                        depInfo.TotalAsset = totalAsset;
                        depInfo.GrandBookTotal = grandBookTotal;
                        depInfo.RemainAmount = remainAmount;
                    }
                }                

                return depInfo;
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        private BillBookHeaderInfo CreateBillBook(DbTransaction trans, BillBookItemListInputInfo billBookParem)
        {
            //get all bill from bill printing table
            //get from main condition list - specified by branchId, mruId and period
            AgencyDataAccess agentDa = new AgencyDataAccess();
            BillBookDataAccess billDA = new BillBookDataAccess();
            string postedServerId = Session.Branch.PostedServerId;
            try
            {
                BindingList<BillBookCreationInfo> bbCIs = billBookParem.CreationItemList;
                BillBookHeaderInfo bookHeader = billBookParem.HeaderInfo;
                //get all bills of this book - filtered by BillPeriod type
                DataTable billNotToBook = null;
                DataTable billToBook = agentDa.PrepareBillBookItemList(billBookParem, ref billNotToBook);

                if (billToBook.Rows.Count == 0)
                {
                    bookHeader.BillBookId = null;
                    bookHeader.CreateFailReason = "ไม่พบข้อมูลบิลที่จะทำการสร้างสมุด";
                    return bookHeader;
                }


                decimal totalBillAmount = GetBillAmount(billToBook);
                int totalBillCount = billToBook.Rows.Count;

                int? bookLot = GetBookLotNumber(bookHeader.AgentId, DaHelper.SetBillPeriod(bookHeader.Period));
                bookHeader.BookLot = bookLot;

                //if employee as BookHolder so create CaId for that employee
                if (!bookHeader.IsAgency)
                {
                    //agentDa.NewEmployeeContractAccount(trans, bookHeader, postedServerId);
                    bookHeader.AgentId = String.Format("EMP0{0}", bookHeader.AgentId.PadLeft(8, '0'));
                }
                else
                {
                    bookHeader.AgentId = bookHeader.AgentId.PadLeft(12, '0');
                }
                bookHeader.TotalBillCount = totalBillCount;
                bookHeader.TotalBookAmount = totalBillAmount;                             

                //does not yet calculate advance payment 30%                                                       
                string bookId = agentDa.CreateBillBook(trans, bookHeader, postedServerId);
                StoreScreenInput(trans, billBookParem, bookId);

                //insert each valid bill to book
                BillStatusInfo item = new BillStatusInfo();
                foreach (DataRow bill in billToBook.Rows)
                {
                    item.BillBookId = bookId;
                    item.InvoiceNo = bill["InvoiceNo"].ToString();
                    item.Period = bill["Period"].ToString();
                    item.BranchId = bill["BranchId"].ToString();
                    item.MruId = bill["MruId"].ToString();
                    item.CaId = bill["CaId"].ToString();
                    item.AbsId = bill["AbsId"].ToString();
                    agentDa.InsertBillBookItem(trans, item, postedServerId, bookHeader.ModifiedBy);
                }

                //update advance payment of book ..
                //Get sum of bill amount of every lines that marked as AdvPay               
                decimal? advPay = billDA.GetAdvancePaymentByMRU(trans, bookId);
                agentDa.UpdateAdvanceBookPayment(trans, bookId, advPay);  //30% of line sum

                //update BillStatusInfo
                //Inbook, AboId
                foreach (DataRow bill in billToBook.Rows)
                {
                    string invId = bill["InvoiceNo"].ToString();
                    string aboId = null;
                    if (billBookParem.HeaderInfo.IsFirstPaid)
                        aboId = "N";
                    else
                        aboId = "R";

                    //update Inbook and aboId flag
                    agentDa.UpdateBillStatusInfoOfBeingCreatedBook(trans, invId, "Y", aboId);
                }

                return bookHeader;
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        public string CheckExistingReceiveCount(BillBookItemListInputInfo bookItemListInfo)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            string ret = null;
            try
            {
                ret = agentDa.IsReceiveCountExist(bookItemListInfo.HeaderInfo);
                
            }
            catch (Exception e)
            {
                
                throw;
            }

            return ret;
        }

        public bool IsBillBookAlreadyCheckedIn(string bookId)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                bool ret = agentDa.IsBillBookAlreadyCheckedIn(bookId);
                
                return ret;
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        public bool IsAlreadyCancel(string bookId)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                bool ret = agentDa.IsBillBookAlreadyCancel(bookId);

                return ret;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool IsBillBookCreated(string bookId)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                bool ret = agentDa.IsBillBookAlreadyCreate(bookId);

                return ret;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public BillBookHeaderInfo CreateBillBookAndSendPrintEvent(DbTransaction trans, BillBookItemListInputInfo billBookParem)
        {
            BillBookHeaderInfo header = null;
            try
            {
                if (trans == null)
                {
                    Database db = DatabaseFactory.CreateDatabase("POSDatabase");

                    using (DbConnection conn = db.CreateConnection())
                    {
                        conn.Open();
                        trans = conn.BeginTransaction();
                        bool cancelStatus = true;
                        try
                        {
                            // if update billbook cancel first

                            if (billBookParem.IsEditBillBook)
                            {
                                cancelStatus = CancelBillBook(trans, billBookParem.HeaderInfo.OriginalBillBookId);
                            }

                            header = CreateBillBook(trans, billBookParem);

                            trans.Commit();
                           
                            //Update create date in mruplan
                            DateTime currDate = Session.BpmDateTime.Now;
                            AgencyDataAccess agentDa = new AgencyDataAccess();
                            agentDa.UpdateMRUPlanCreateBillBook(currDate, header.BillBookId);
                            return header;
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            throw;
                        }
                    }
                }
                else
                {
                    header = CreateBillBook(trans, billBookParem);
                    return header;
                }
            }
            catch (Exception e)
            {
                throw;
            }


        }

        public List<HashInfo> LoadBookItemValidationData(List<string> searchConn)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                List<HashInfo> vl = new List<HashInfo>();
                //get branchId
                //AgentInfo agentInfo = agentDa.GetAgentInformation(agentId);
                BindingList<LineInfo> lineList = agentDa.SelectMRUsByEmpNoAgencyId(searchConn);

                foreach (LineInfo line in lineList)
                {
                    HashInfo h = new HashInfo();
                    h.Id = line.BranchId.ToUpper();
                    h.Value = line.LineId;
                    vl.Add(h);
                }

                return vl;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public BillBookItemListInputInfo LoadPastBillBookInfo(string pastBillBookId)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            BillBookItemListInputInfo bookItemList = new BillBookItemListInputInfo();

            try
            {
                pastBillBookId = pastBillBookId.Replace("-", "");
                DataTable bookHeader = agentDa.GetBillBookHeaderInfo(pastBillBookId);
                //fill billbook header
                BillBookHeaderInfo header = new BillBookHeaderInfo();
                if (bookHeader.Rows.Count == 0) return new BillBookItemListInputInfo();

                DataRow h = bookHeader.Rows[0];
                string temp = DaHelper.GetString(h, "BookPeriod");
                header.Period = string.Format("{0}/{1}", temp.Substring(4, 2), temp.Substring(0, 4));
                header.ReturnedBillDt = (DateTime)DaHelper.GetDate(h, "ReturnDueDate");
                header.AdvancePaymentDt = (DateTime)DaHelper.GetDate(h, "AdvPayDueDate");
                header.ControllerId = DaHelper.GetString(h, "BkId");
                header.ReceiveCount = DaHelper.GetByte(h, "ReceiveCount");
                header.BillBookId = pastBillBookId;
                header.AgentId = DaHelper.GetString(h, "BookHolderId");
                header.AdvPayAmount = DaHelper.GetDecimal(h, "AdvPayAmount");
                header.CreatorName = String.Format("{0} {1} {2}", DaHelper.GetString(h, "Createdby"), DaHelper.GetString(h, "FirstName"), DaHelper.GetString(h, "LastName"));
                header.BookLot = DaHelper.GetInt(h, "BookLot");
                
                AgentInfo agentInfo = agentDa.GetAgentInformation(header.AgentId);
                bookItemList.HeaderInfo.ControllerId = DaHelper.GetString(h, "ControllerId");
                bookItemList.AgencyInfo = agentInfo;

                if (agentInfo != null)
                {
                    header.AgentId = header.AgentId.Substring(header.AgentId.Length - ModuleConfigurationNames.AgentIdLength, ModuleConfigurationNames.AgentIdLength);
                    header.AgentName = agentInfo.Name;
                    header.AgentStatus = agentInfo.AgencyStatus;
                    header.AgentType = agentInfo.Type;
                    header.TotalAsset = agentInfo.Deposit;
                    header.IsAgency = true;
                }
                else
                {
                    header.IsAgency = false;
                }


                string aboId = DaHelper.GetString(h, "AboId");
                if (aboId == "N")
                    header.IsFirstPaid = true;
                else
                    header.IsFirstPaid = false;

                bookItemList.HeaderInfo = header;
                
                DataTable bookItemSet = agentDa.SelectBillBookInputItemSet(pastBillBookId);
                BindingList<BillBookCreationInfo> bookCrInfoList = new BindingList<BillBookCreationInfo>();
                foreach (DataRow bset in bookItemSet.Rows)
                {
                    BillBookCreationInfo bookCrInfo = new BillBookCreationInfo();
                    bookCrInfo.PeaCode = DaHelper.GetString(bset, "BranchId");
                    bookCrInfo.LineId = DaHelper.GetString(bset, "MruId");
                    bookCrInfo.BillPeriod = DaHelper.GetString(bset, "BillPeriodType");
                    bookCrInfo.CallingBill = DaHelper.GetString(bset, "BillOutType");
                    bookCrInfoList.Add(bookCrInfo);
                }

                // add default mru 9999
                BillBookCreationInfo mruDefaultInfo = new BillBookCreationInfo();
                mruDefaultInfo.PeaCode = bookCrInfoList[bookCrInfoList.Count - 1].PeaCode;
                mruDefaultInfo.LineId = "9999";
                mruDefaultInfo.BillPeriod = "1";
                mruDefaultInfo.CallingBill = "1";
                bookCrInfoList.Add(mruDefaultInfo);

                //ยกเว้น
                DataTable items = agentDa.SelectBillBookInputItem(pastBillBookId, "2");
                bookItemList.ExtraItemExp = FillExtraItem(items);

                //ออกเก็บ
                items = agentDa.SelectBillBookInputItem(pastBillBookId, "3");
                bookItemList.ExtraItemPlus = FillExtraItem(items);

                //ปัจจุบัน + เดือนเก่า (ป้อนยกเว้น)
                items = agentDa.SelectBillBookInputItem(pastBillBookId, "4");
                bookItemList.ExtraItemCurExp = FillExtraItem(items);

                //ปัจจุบัน + เดือนเก่า (ป้อนออกเก็บ)
                items = agentDa.SelectBillBookInputItem(pastBillBookId, "5");
                bookItemList.ExtraItemCurPlus = FillExtraItem(items);

                bookItemList.CreationItemList = bookCrInfoList;                
            }
            catch (Exception e)
            {

                System.Console.WriteLine(e.Message);
            }

            return bookItemList;

        }

        private BindingList<BillBookCreationExtraInfo> FillExtraItem(DataTable dt)
        {
            BindingList<BillBookCreationExtraInfo> extraInfoList = new BindingList<BillBookCreationExtraInfo>();
            foreach (DataRow it in dt.Rows)
            {
                BillBookCreationExtraInfo extraInfo = new BillBookCreationExtraInfo();
                extraInfo.NPeaCode = DaHelper.GetString(it, "BranchId");
                extraInfo.NLineId = DaHelper.GetString(it, "MruId");
                extraInfo.Number = DaHelper.GetString(it, "CaId");
                extraInfo.FilterType = DaHelper.GetString(it, "FilterType");
                extraInfoList.Add(extraInfo);
            }

            return extraInfoList;
        }

        public List<BillbookInfoReprint> LoadBillBookList(BookSearchStatusEnum status, string runningBranch)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                List<BillbookInfoReprint> billbookList = agentDa.SelectBillBookListByStatus(status, runningBranch);
                
                return billbookList;
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        public List<BillbookInfoReprint> LoadBillBookByIdKeyword(BillbookInfoReprint searchCondition, string branchId)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                List<BillbookInfoReprint> billbookList = agentDa.SelectBillBookByIdKeyword(branchId, searchCondition);
                
                return billbookList;
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        public bool IsAlreadyAdvPaid(string billBookId)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                return agentDa.IsAlreadyAdvPaid(billBookId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int IsReadyToCancelBillBook(string billBookId)
        {
            int status = 0;
            try
            {
                if (IsBillBookAlreadyCheckedIn(billBookId))
                {
                    status = 1;
                }
                else if (IsAlreadyAdvPaid(billBookId)) 
                {
                    status = 2;
                }
                else if (IsAlreadyCancel(billBookId))
                {
                    status = 3;
                }
                else if (!IsBillBookCreated(billBookId))
                {
                    status = 4;
                }
                return status;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public BindingList<CallingBillInfo> RetreiveBookDetail(string billBookId)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                return agentDa.RetreiveBookDetail(billBookId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public BindingList<CallingBillInfo> RetreiveBookLineDetail(string billBookId, string[] line)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                return agentDa.RetreiveBookLineDetail(billBookId, line);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public BindingList<CallingBillSummaryInfo> RetreiveBookSummary(string billBookId, string period)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                return agentDa.RetreiveBookSummary(billBookId, period);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string GetNewBillBookId(string runningBranch)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                return agentDa.GetNewBillBookId(runningBranch);
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}

using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.AgencyManagementModule.DA;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.AgencyManagementModule.Interface.Constants;
using System.Globalization;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace PEA.BPM.AgencyManagementModule.BS
{
    public class CommissionMgtService : ICommissionMgtService
    {
        private SpecialCommissionCalInfo _sumSpecialCommission;
        private List<CommissionBaseInfo> _comBaseList;
        private SaveCommissionInfo _saveInfo;  //global for commission calculating
        private DateTimeFormatInfo _th_dt;
        private DateTimeFormatInfo _us_dt;

        public CommissionMgtService()
        {
            CultureInfo th_culture = new CultureInfo("th-TH");
            CultureInfo us_culture = new CultureInfo("en-US");
            _th_dt = th_culture.DateTimeFormat;
            _us_dt = us_culture.DateTimeFormat;
        }

        public FeeBaseElement LoadCommissionRate(BookSearchInfo searchInfo)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                FeeBaseElement commissionRate = agentDa.GetBaseCommissionRate(searchInfo);
                if (commissionRate == null) // not found
                    commissionRate = agentDa.GetBaseCommissionRate(searchInfo.BranchId);

                return commissionRate;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private void InitiateCustomerType(List<CommissionBaseInfo> cbaseInfoList)
        {
            //better to retreive list from table instead of hard coding - fix me!
            CommissionBaseInfo homeContract = new CommissionBaseInfo();
            CommissionBaseInfo corpContract = new CommissionBaseInfo();
            CommissionBaseInfo govContract = new CommissionBaseInfo();
            CommissionBaseInfo govPaid = new CommissionBaseInfo();

            homeContract.CustomerType = "บ้านพักอาศัย";
            corpContract.CustomerType = "กิจการขนาดเล็ก";
            govContract.CustomerType = "หน่วยงานราชการ";
            govPaid.CustomerType = "รัฐบาลรับภาระ";

            cbaseInfoList.Add(homeContract);
            cbaseInfoList.Add(corpContract);
            cbaseInfoList.Add(govContract);
            cbaseInfoList.Add(govPaid);
        }

        public List<int> GetReceiveRange(string rs)
        {
            List<int> range = new List<int>();
            char[] spliter = { '-' };
            string[] sp = rs.Split(spliter);
            if (sp.Length > 2) return range;

            int start = Convert.ToInt32(sp[0].Replace(" ", ""));
            int end = Convert.ToInt32(sp[1].Replace(" ", ""));
            if (end < start) return range;

            for (int i = start; i <= end; i++)
            {
                range.Add(i);
            }

            return range;
        }

        private void InitiateInBoundCollection(List<InBoundCollectionInfo> bList)
        {
            InBoundCollectionInfo t1 = new InBoundCollectionInfo();
            t1.BillType = "อยู่ในช่วงร้อยละ 75-90";
            InBoundCollectionInfo t2 = new InBoundCollectionInfo();
            t2.BillType = "เกินกว่าร้อยละ 90";
            bList.Add(t1);
            bList.Add(t2);
        }

        private int GetTotalBillCount()
        {
            int count = 0;
            foreach (CommissionBaseInfo cInfo in _comBaseList)
            {
                count += cInfo.BillCountFirstAll + cInfo.BillCountRepeatAll;
            }
            return count;
        }

        private decimal? GetAgencyVatRate(string agencyId)
        {
            decimal? vatRate = 0;
            CommissionDataAccess comDa = new CommissionDataAccess();
            vatRate = comDa.GetAgencyVatRate(agencyId);
            return vatRate;
        }

        private decimal? GetAgencyTaxRate(string agencyId)
        {
            decimal? taxRate = 0;
            CommissionDataAccess comDa = new CommissionDataAccess();
            taxRate = comDa.GetAgencyTaxRate(agencyId);
            return taxRate;
        }

        private InvoiceCommissionInfo CalculateInvoiceCommission(BookSearchInfo searchInfo, FeeBaseElement comRate)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                InvoiceCommissionInfo invInfo = new InvoiceCommissionInfo();
                //better to get this from database
                decimal percent = comRate.MaxInvoicePercent.Value;
                decimal invPayRate = comRate.InvoiceRate.Value;
                decimal past3MonthPayRate = comRate.InvoicePastThreeMonthRate.Value;
                invInfo.Percent = percent;
                //get billbook list that have status 'T' - CUT
                DataTable bookList = null;
                if (searchInfo.IsReprint)
                    bookList = agentDa.GetBillBookIdWithCutStatusByAgentIdReprint(searchInfo.BillPeriod, searchInfo.AgentId, searchInfo.CreateDate);
                else
                    bookList = agentDa.GetBillBookIdWithCutStatusByAgentId(searchInfo.BillPeriod, searchInfo.AgentId, searchInfo.CreateDate);

                //last three month period
                int year = Convert.ToInt32(searchInfo.BillPeriod.Substring(0, 4));
                int month = Convert.ToInt32(searchInfo.BillPeriod.Substring(4, 2));

                #region Three Past Month

                if (month == 2)
                {
                    month = 12;
                    year = -1;
                }
                else if (month == 1)
                {
                    month = 11;
                    year = -1;
                }
                else if (month == 0)
                {
                    month = 10;
                    year = -1;
                }
                else
                {
                    month = month - 2;
                }
                #endregion

                string threeMonthPeriod = year.ToString() + month.ToString().PadLeft(2, '0');

                int actualCount = 0;
                int totalCount = 0;
                int billCountPastthreeMonths = 0;
                foreach (DataRow r in bookList.Rows)
                {
                    string bookId = r["BookId"].ToString();
                    actualCount += agentDa.SelectBookItemPutInvoiceByBookId(bookId);
                    totalCount += agentDa.GetBookItemCount(bookId);
                    billCountPastthreeMonths += agentDa.GetBookItemPutInvPastThreeMonths(bookId, threeMonthPeriod);
                }

                invInfo.PercentSlip = (int)Math.Round((percent * totalCount) / 100, MidpointRounding.AwayFromZero);
                invInfo.ThreeMonthNoPaidSlip = billCountPastthreeMonths;
                invInfo.ActualSlipToCustomer = actualCount - invInfo.ThreeMonthNoPaidSlip;


                if (invInfo.PercentSlip <= invInfo.ThreeMonthNoPaidSlip)
                {
                    invInfo.TotalBill = invInfo.PercentSlip;
                    invInfo.Total = invInfo.PercentSlip * invPayRate;
                }
                else
                {
                    int avi = invInfo.PercentSlip - invInfo.ThreeMonthNoPaidSlip;
                    //available slot is more than ten percent bill
                    if (avi >= invInfo.ActualSlipToCustomer)
                    {
                        invInfo.TotalBill = invInfo.ThreeMonthNoPaidSlip + invInfo.ActualSlipToCustomer;
                        invInfo.Total = (invInfo.ThreeMonthNoPaidSlip * invPayRate) + (invInfo.ActualSlipToCustomer * invPayRate);
                    }
                    else
                    {
                        invInfo.TotalBill = invInfo.ThreeMonthNoPaidSlip + avi; //TenPercentSlip
                        invInfo.Total = (invInfo.ThreeMonthNoPaidSlip * invPayRate) + (avi * invPayRate);
                    }
                }

                return invInfo;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //private bool PayFineDebt(DbTransaction trans, AccountReceiveInfo debt, string billBookId)
        //{
        //    BillBookDataAccess billDa = AgencyDAFactory.GetBillBookDataAccess();
        //    try
        //    {
        //        debt.DtId = ARDeptType.FINEPAID;
        //        debt.BranchId = Session.Branch.Id;
        //        debt.InvoiceNo = "-";              
        //        debt.TaxCode = CodeName.TaxCode.TaxCodeId;
        //        debt.VatAmount = 0;
        //        debt.ControllerId = String.Empty;
        //        debt.PaidAmount = 0;
        //        debt.ModifiedBy = Session.User.Id;
        //        string itemId = billDa.InsertAccountReceive(debt, billBookId);
        //        billDa.Release();
        //        if (itemId == "0") return false;
        //        else return true;
        //    }
        //    catch (Exception e)
        //    {
        //        billDa.Release();
        //        throw;
        //    }
        //}

        private decimal? GetCannotCollectedInvoiceAmount()
        {
            decimal? cannotCollectBillAmount = _sumSpecialCommission.TotalAmount - _sumSpecialCommission.ActualAmount;
            return cannotCollectBillAmount;
        }

        //condition for allowing calculation commission
        //All the books created on the same day must be cheked in already.
        public bool IsBookAvailable(BookSearchInfo searchInfo)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                //checked in billbook
                DataTable bookList = agentDa.GetBillBookIdWithCutStatusByAgentId(searchInfo.BillPeriod, searchInfo.AgentId, searchInfo.CreateDate);

                //not 'cancel', not calculated commission
                int count = agentDa.GetBillBookCountByCreateDate(searchInfo.BillPeriod, searchInfo.AgentId, searchInfo.CreateDate);

                if (bookList.Rows.Count != count)
                    return false;
                else
                    return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //condition for allowing calculation commission
        //All the books created on the same day must be paid 
        public bool IsBookAlreadyPaid(BookSearchInfo searchInfo)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                //checked in billbook
                DataTable bookList = agentDa.GetBillBookIdWithCutStatusByAgentId(searchInfo.BillPeriod, searchInfo.AgentId, searchInfo.CreateDate);
                foreach (DataRow r in bookList.Rows)
                {
                    string billBookId = DaHelper.GetString(r, "BookId");
                    bool notPaid = agentDa.GetBillBookCountNotPaid(billBookId);
                    if (notPaid)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private FineInfo CalculateFine(BookSearchInfo searchInfo, FeeBaseElement comRate)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                FineInfo fineInfo = new FineInfo();
                //keep the detail in case user wanna see it
                FineDetailInfo fineDetailInfo = new FineDetailInfo();
                decimal? finePerBill = comRate.FineRatePerBill;
                fineDetailInfo.FinePerBill = finePerBill;
                fineDetailInfo.AgentId = searchInfo.AgentId;
                fineDetailInfo.Period = searchInfo.BillPeriod;
                fineDetailInfo.CreateDate = searchInfo.CreateDate.ToString("dd/MM/yyyy", _th_dt); ;
                fineDetailInfo.ReturnedInvAmount = GetCannotCollectedInvoiceAmount();

                List<FineBookInfo> fineList = new List<FineBookInfo>();
                decimal? remainAdvPayAmount = 0;
                decimal? remainBookTotalAmount = 0;
                decimal? actAdvPayAmount = 0;

                //Store information for saving to database
                _saveInfo = new SaveCommissionInfo();
                _saveInfo.AgentId = searchInfo.AgentId;
                _saveInfo.Period = searchInfo.BillPeriod;

                //find billBookId by using BookPeriod, agentId and createDate
                //get billbook list that have status 'T' - CUT
                DataTable bookList = null;
                if (searchInfo.IsReprint)
                    bookList = agentDa.GetBillBookIdWithCutStatusByAgentIdReprint(searchInfo.BillPeriod, searchInfo.AgentId, searchInfo.CreateDate);
                else
                    bookList = agentDa.GetBillBookIdWithCutStatusByAgentId(searchInfo.BillPeriod, searchInfo.AgentId, searchInfo.CreateDate);

                int bookCount = 1;

                foreach (DataRow r in bookList.Rows)
                {
                    //calculate fine for overdue of advance money 30%                 
                    string bookId = DaHelper.GetString(r, "BookId");
                    decimal? bookTotalPaid = 0;
                    //find advance payment that have already paid by POS
                    //DataTable arAdvList = agentDa.SelectARAdvPaymentAmount(bookId);
                    DataTable paidList = agentDa.SelectARBookPaymentAmount(bookId);

                    //get all date information from book which are advPayDate, advPayAmount, returnDate
                    DataTable bookDate = agentDa.GetPaymentDateBook(bookId);
                    if (bookDate.Rows.Count == 0) return fineInfo;

                    bool isPrepaid = false;

                    //data from book
                    DateTime? advTemp = DaHelper.GetDate(bookDate.Rows[0], "AdvPayDueDate");
                    DateTime? advDueDt = new DateTime(advTemp.Value.Year, advTemp.Value.Month, advTemp.Value.Day);
                    DateTime? retTemp = DaHelper.GetDate(bookDate.Rows[0], "ReturnDueDate");
                    DateTime? returnDueDt = new DateTime(retTemp.Value.Year, retTemp.Value.Month, retTemp.Value.Day);
                    decimal? advPayAmount = DaHelper.GetDecimal(bookDate.Rows[0], "AdvPayAmount");
                    remainAdvPayAmount = advPayAmount;
                    decimal? bookTotalAmount = agentDa.GetBookTotalAmount(bookId);
                    remainBookTotalAmount = bookTotalAmount - remainAdvPayAmount;
                    actAdvPayAmount = 0;
                    //keep the details - the lastest date will be kept
                    if (fineDetailInfo.AdvPayDate == null)
                        fineDetailInfo.AdvPayDate = advDueDt.Value.ToString("dd/MM/yyyy", _th_dt);

                    if (fineDetailInfo.ReturnDate == null)
                        fineDetailInfo.ReturnDate = returnDueDt.Value.ToString("dd/MM/yyyy", _th_dt); ;

                    if (advPayAmount != null)
                        fineDetailInfo.TotalAdvPayAmount += advPayAmount;

                    if (bookTotalAmount != null)
                        fineDetailInfo.TotalAmount += bookTotalAmount;

                    fineDetailInfo.TotalRemainDebtAmount += (bookTotalAmount.Value - advPayAmount.Value);

                    //Important - we should allow POS to get paid only one book per recepit
                    //every time they pay
                    #region AR Payment
                    foreach (DataRow paid in paidList.Rows)
                    {
                        DateTime? paidTemp = DaHelper.GetDate(paid, "PaymentDt");
                        DateTime? paidDt = new DateTime(paidTemp.Value.Year, paidTemp.Value.Month, paidTemp.Value.Day);

                        decimal? paidAmount = DaHelper.GetDecimal(paid, "PaidAmount");
                        string debtType = DaHelper.GetString(paid, "DtId").Replace(" ", "");
                        string receiptId = DaHelper.GetString(paid, "ReceiptId");

                        if (debtType == ARDeptType.ADVPAID)  // 30%
                        {
                            isPrepaid = true;
                            //calculate fine for 30%    
                            actAdvPayAmount = actAdvPayAmount + paidAmount;
                            if (paidDt > advDueDt)
                            {
                                TimeSpan? extraDt = (paidDt - advDueDt);
                                if (extraDt.Value.Days > 0)
                                {
                                    BookFineDetailInfo bookFineInfo = new BookFineDetailInfo();
                                    bookFineInfo.BookCount = bookCount; bookCount++;
                                    bookFineInfo.BookPaidAmount = paidAmount;
                                    bookFineInfo.PaidDate = paidDt;
                                    bookFineInfo.BookAdvAmount = advPayAmount;
                                    bookFineInfo.BookAmount = bookTotalAmount;
                                    bookFineInfo.BookId = bookId;

                                    decimal? amountToFine = 0;
                                    if (paidAmount > remainAdvPayAmount)
                                    {
                                        amountToFine = remainAdvPayAmount;
                                        remainBookTotalAmount = remainBookTotalAmount - (paidAmount - remainAdvPayAmount);
                                    }
                                    else
                                        amountToFine = paidAmount;

                                    //fineInfo.Amount += extraDt.Value.Days * amountToFine * (finePerBill.Value / 100);
                                    bookFineInfo.BookAdvOverdueDay = extraDt.Value.Days;
                                    bookFineInfo.BookAdvFineAmount = extraDt.Value.Days * amountToFine * (finePerBill.Value / 100);
                                    bookFineInfo.BookTotalFine = bookFineInfo.BookAdvFineAmount;

                                    bookFineInfo.BookRemainDebtAmount = bookTotalAmount - actAdvPayAmount;
                                    fineDetailInfo.BookFineDetail.Add(bookFineInfo);
                                }
                                else
                                {
                                    BookFineDetailInfo bookFineInfo = new BookFineDetailInfo();
                                    bookFineInfo.BookCount = bookCount; bookCount++;
                                    bookFineInfo.BookPaidAmount = paidAmount;
                                    bookFineInfo.PaidDate = paidDt;
                                    bookFineInfo.BookAdvAmount = advPayAmount;
                                    bookFineInfo.BookAmount = bookTotalAmount;
                                    bookFineInfo.BookId = bookId;
                                    bookFineInfo.BookAdvOverdueDay = 0;
                                    bookFineInfo.BookAdvFineAmount = 0;
                                    bookFineInfo.BookTotalFine = bookFineInfo.BookAdvFineAmount;

                                    bookFineInfo.BookRemainDebtAmount = bookTotalAmount - actAdvPayAmount;
                                    fineDetailInfo.BookFineDetail.Add(bookFineInfo);

                                    remainAdvPayAmount = remainAdvPayAmount - paidAmount;
                                }
                            }
                            else
                            {
                                BookFineDetailInfo bookFineInfo = new BookFineDetailInfo();
                                bookFineInfo.BookCount = bookCount; bookCount++;
                                bookFineInfo.BookPaidAmount = paidAmount;
                                bookFineInfo.PaidDate = paidDt;
                                bookFineInfo.BookAdvAmount = advPayAmount;
                                bookFineInfo.BookAmount = bookTotalAmount;
                                bookFineInfo.BookId = bookId;
                                bookFineInfo.BookAdvOverdueDay = 0;
                                bookFineInfo.BookAdvFineAmount = 0;
                                bookFineInfo.BookTotalFine = bookFineInfo.BookAdvFineAmount;

                                bookFineInfo.BookRemainDebtAmount = bookTotalAmount - actAdvPayAmount;
                                fineDetailInfo.BookFineDetail.Add(bookFineInfo);

                                remainAdvPayAmount = remainAdvPayAmount - paidAmount;
                                remainAdvPayAmount = remainAdvPayAmount < 0 ? 0 : remainAdvPayAmount;//Fix remainAdvPayamount < 0
                            }
                        }
                    }


                    bookTotalPaid = actAdvPayAmount;
                    #endregion

                    #region CheckIn Book

                    DateTime? cdt = DaHelper.GetDate(bookDate.Rows[0], "CheckInDate");
                    DateTime? checkInDt = new DateTime(cdt.Value.Year, cdt.Value.Month, cdt.Value.Day);

                    // if not paid 30%
                    if (!isPrepaid)
                    {
                        TimeSpan? extraDt = (checkInDt - advDueDt);
                        if (extraDt.Value.Days > 0)
                        {
                            BookFineDetailInfo bookFineInfo = new BookFineDetailInfo();
                            bookFineInfo.BookCount = bookCount; bookCount++;
                            bookFineInfo.BookPaidAmount = 0;
                            bookFineInfo.PaidDate = checkInDt;
                            bookFineInfo.BookAdvAmount = advPayAmount;
                            bookFineInfo.BookAmount = bookTotalAmount;
                            bookFineInfo.BookId = bookId;

                            decimal? amountToFine = remainAdvPayAmount;

                            //fineInfo.Amount += extraDt.Value.Days * amountToFine * (finePerBill.Value / 100);
                            bookFineInfo.BookAdvOverdueDay = extraDt.Value.Days;
                            bookFineInfo.BookAdvFineAmount = extraDt.Value.Days * amountToFine * (finePerBill.Value / 100);
                            bookFineInfo.BookTotalFine = bookFineInfo.BookAdvFineAmount;

                            bookFineInfo.BookRemainDebtAmount = bookTotalAmount - actAdvPayAmount;
                            fineDetailInfo.BookFineDetail.Add(bookFineInfo);
                        }
                    }

                    remainBookTotalAmount = remainBookTotalAmount > bookTotalAmount - actAdvPayAmount ? (bookTotalAmount - actAdvPayAmount) : remainBookTotalAmount;
                    if (checkInDt > returnDueDt)
                    {
                        //ถ้าจ่าย 30% น้อยกว่าที่กำหนด ให้คิดค่าปรับส่วนที่เหลือ เพราะ 30% คิดค่าปรับไปแล้ว                       
                        TimeSpan? extraDt = (checkInDt - returnDueDt);
                        if (extraDt.Value.Days > 0)
                        {
                            BookFineDetailInfo bookFineInfo = new BookFineDetailInfo();
                            bookFineInfo.BookCount = bookCount; bookCount++;
                            bookFineInfo.BookPaidAmount = remainBookTotalAmount;
                            bookFineInfo.PaidDate = checkInDt;
                            bookFineInfo.BookAdvAmount = 0;
                            bookFineInfo.BookAmount = bookTotalAmount;
                            bookFineInfo.BookId = bookId;

                            //fineInfo.Amount += extraDt.Value.Days * (remainBookTotalAmount) * (finePerBill.Value / 100);
                            bookFineInfo.ReturnOverdueDay = extraDt.Value.Days;
                            bookFineInfo.ReturnBookFineAmount = extraDt.Value.Days * (bookTotalAmount - bookTotalPaid) * (finePerBill.Value / 100);
                            bookFineInfo.BookTotalFine = bookFineInfo.ReturnBookFineAmount;
                            bookFineInfo.BookRemainDebtAmount = 0;
                            fineDetailInfo.BookFineDetail.Add(bookFineInfo);
                        }
                        else
                        {
                            BookFineDetailInfo bookFineInfo = new BookFineDetailInfo();
                            bookFineInfo.BookCount = bookCount; bookCount++;
                            bookFineInfo.BookPaidAmount = remainBookTotalAmount;
                            bookFineInfo.PaidDate = checkInDt;
                            bookFineInfo.BookAdvAmount = 0;
                            bookFineInfo.BookAmount = bookTotalAmount;
                            bookFineInfo.BookId = bookId;
                            bookFineInfo.ReturnOverdueDay = 0;
                            bookFineInfo.ReturnBookFineAmount = 0;
                            bookFineInfo.BookTotalFine = bookFineInfo.ReturnBookFineAmount;
                            bookFineInfo.BookRemainDebtAmount = 0;
                            fineDetailInfo.BookFineDetail.Add(bookFineInfo);
                        }
                    }
                    else
                    {
                        BookFineDetailInfo bookFineInfo = new BookFineDetailInfo();
                        bookFineInfo.BookCount = bookCount; bookCount++;
                        bookFineInfo.BookPaidAmount = remainBookTotalAmount;
                        bookFineInfo.PaidDate = checkInDt;
                        bookFineInfo.BookAdvAmount = 0;
                        bookFineInfo.BookAmount = bookTotalAmount;
                        bookFineInfo.BookId = bookId;
                        bookFineInfo.ReturnOverdueDay = 0;
                        bookFineInfo.ReturnBookFineAmount = 0;
                        bookFineInfo.BookTotalFine = bookFineInfo.ReturnBookFineAmount;
                        bookFineInfo.BookRemainDebtAmount = 0;
                        fineDetailInfo.BookFineDetail.Add(bookFineInfo);
                    }

                    #endregion

                    #region "Old code"
                    /*
                    ////--------------------------------------------
                    //calculate fine for overdue of advance money 30%                 
                    string bookId = DaHelper.GetString(r, "BookId");
                    decimal? bookTotalPaid = 0;

                    //find advance payment that have already paid by POS
                    //DataTable arAdvList = agentDa.SelectARAdvPaymentAmount(bookId);
                    DataTable paidList = agentDa.SelectARBookPaymentAmount(bookId);

                    //get all date information from book which are advPayDate, advPayAmount, returnDate
                    DataTable bookDate = agentDa.GetPaymentDateBook(bookId);
                    if (bookDate.Rows.Count == 0) return fineInfo;

                    DateTime? advTemp = DaHelper.GetDate(bookDate.Rows[0], "AdvPayDueDate");
                    DateTime? advDueDt = new DateTime(advTemp.Value.Year, advTemp.Value.Month, advTemp.Value.Day);
                    DateTime? retTemp = DaHelper.GetDate(bookDate.Rows[0], "ReturnDueDate");
                    DateTime? returnDueDt = new DateTime(retTemp.Value.Year, retTemp.Value.Month, retTemp.Value.Day);
                    decimal? advPayAmount = DaHelper.GetDecimal(bookDate.Rows[0], "AdvPayAmount");
                    decimal? bookTotalAmount = agentDa.GetBookTotalAmount(bookId);

                    //keep the details - the lastest date will be kept
                    if (fineDetailInfo.AdvPayDate == null)
                        fineDetailInfo.AdvPayDate = advDueDt.Value.ToString("dd/MM/yyyy", _th_dt);

                    if (fineDetailInfo.ReturnDate == null)
                        fineDetailInfo.ReturnDate = returnDueDt.Value.ToString("dd/MM/yyyy", _th_dt); ;

                    if (advPayAmount != null)
                        fineDetailInfo.TotalAdvPayAmount += advPayAmount;

                    if (bookTotalAmount != null)
                        fineDetailInfo.TotalAmount += bookTotalAmount;

                    fineDetailInfo.TotalRemainDebtAmount += (bookTotalAmount.Value - advPayAmount.Value);


                    //Important - we should allow POS to get paid only one book per recepit
                    //every time they pay
                    #region AR Payment
                    foreach (DataRow paid in paidList.Rows)
                    {
                        DateTime? paidTemp = DaHelper.GetDate(paid, "PaymentDt");
                        DateTime? paidDt = new DateTime(paidTemp.Value.Year, paidTemp.Value.Month, paidTemp.Value.Day);

                        decimal? paidAmount = DaHelper.GetDecimal(paid, "PaidAmount");
                        string debtType = DaHelper.GetString(paid, "DtId").Replace(" ", "");
                        string receiptId = DaHelper.GetString(paid, "ReceiptId");

                        if (debtType == ARDeptType.ADVPAID)  // 30%
                        {
                            //calculate fine for 30%                        
                            if (paidDt > advDueDt)
                            {
                                TimeSpan? extraDt = (paidDt - advDueDt);
                                if (extraDt.Value.Days > 0)
                                {
                                    BookFineDetailInfo bookFineInfo = new BookFineDetailInfo();
                                    bookFineInfo.BookCount = bookCount; bookCount++;
                                    bookFineInfo.BookPaidAmount = paidAmount;
                                    bookFineInfo.PaidDate = paidDt;
                                    bookFineInfo.BookAdvAmount = advPayAmount;
                                    bookFineInfo.BookAmount = bookTotalAmount;
                                    bookFineInfo.BookId = bookId;

                                    decimal? amountToFine = 0;
                                    if (paidAmount > advPayAmount)
                                        amountToFine = advPayAmount;
                                    else
                                        amountToFine = paidAmount;

                                    fineInfo.Amount += extraDt.Value.Days * amountToFine * (finePerBill.Value / 100);
                                    bookFineInfo.BookAdvOverdueDay = extraDt.Value.Days;
                                    bookFineInfo.BookAdvFineAmount = extraDt.Value.Days * amountToFine * (finePerBill.Value / 100);
                                    bookFineInfo.BookTotalFine = bookFineInfo.BookAdvFineAmount;

                                    bookTotalPaid += paidAmount;
                                    bookFineInfo.BookRemainDebtAmount = bookTotalAmount - bookTotalPaid;
                                    fineDetailInfo.BookFineDetail.Add(bookFineInfo);
                                }
                            }
                        }
                    }

                    #endregion

                    #region CheckIn Book

                    DateTime? cdt = DaHelper.GetDate(bookDate.Rows[0], "CheckInDate");
                    DateTime? checkInDt = new DateTime(cdt.Value.Year, cdt.Value.Month, cdt.Value.Day);

                    if (checkInDt > returnDueDt)
                    {
                        TimeSpan? extraDt = (checkInDt - returnDueDt);
                        if (extraDt.Value.Days > 0)
                        {
                            BookFineDetailInfo bookFineInfo = new BookFineDetailInfo();
                            bookFineInfo.BookCount = bookCount; bookCount++;
                            bookFineInfo.BookPaidAmount = bookTotalAmount - bookTotalPaid;
                            bookFineInfo.PaidDate = checkInDt;
                            bookFineInfo.BookAdvAmount = advPayAmount;
                            bookFineInfo.BookAmount = bookTotalAmount;
                            bookFineInfo.BookId = bookId;

                            fineInfo.Amount += extraDt.Value.Days * (bookTotalAmount - bookTotalPaid) * (finePerBill.Value / 100);
                            bookFineInfo.ReturnOverdueDay = extraDt.Value.Days;
                            bookFineInfo.ReturnBookFineAmount = extraDt.Value.Days * (bookTotalAmount - bookTotalPaid) * (finePerBill.Value / 100);
                            bookFineInfo.BookTotalFine = bookFineInfo.ReturnBookFineAmount;
                            bookFineInfo.BookRemainDebtAmount = 0;
                            fineDetailInfo.BookFineDetail.Add(bookFineInfo);
                        }
                    }

                    #endregion
                    */
                    #endregion

                    FineBookInfo fb = new FineBookInfo();
                    fb.BillBookId = bookId;
                    fineList.Add(fb);

                }
                //get total fine
                foreach (BookFineDetailInfo b in fineDetailInfo.BookFineDetail)
                {
                    fineInfo.Amount += b.BookTotalFine;
                }
                fineInfo.FineList = fineList;
                fineInfo.FineDetail = fineDetailInfo;
                fineInfo.Enabled = true;

                return fineInfo;
            }
            catch (Exception e)
            {
                throw;
            }
        }


        ////To do! 
        //private TaxCalculationInfo CalculateTax(BookSearchInfo searchInfo)
        //{
        //    AgencyDataAccess agentDa = new AgencyDataAccess();
        //    TaxCalculationInfo taxCalculationInfo = null;
        //    try
        //    {
        //        TaxInfo taxInfo = agentDa.GetAgentTaxInformation(searchInfo.AgentId);
        //        taxCalculationInfo = new TaxCalculationInfo();
        //        //Put tax calculation code here     
        //        if (taxInfo != null && taxInfo.AgentStatus)
        //            taxCalculationInfo.TotalTax = searchInfo.TotalCommission * (taxInfo.Percent.Value / 100);
        //        else
        //            taxCalculationInfo.TotalTax = searchInfo.TotalCommission;
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }

        //    return taxCalculationInfo;
        //}

        //private decimal CalculateAgencyTaxAmount(string agentId, decimal amount)
        //{
        //    decimal taxAmount = 0;
        //    //AgencyDataAccess agentDa = new AgencyDataAccess();
        //    CommissionDataAccess commDa = new CommissionDataAccess();
        //    //Fix me! minus by tax 
        //    taxAmount = amount *  ((decimal)commDa.GetTaxRate()/100);            //
        //    return taxAmount;
        //}

        //private decimal CalculateVatAmount(string agentId, decimal amount)
        //{
        //    CommissionDataAccess commDa = new CommissionDataAccess();
        //    decimal vatPercent = commDa.GetVatRateOfVatNotRevenue(agentId) == null ? 0 : (decimal)commDa.GetVatRateOfVatNotRevenue(agentId);
        //    return (amount * vatPercent)/100;
        //}


        private string SaveCommissionAsTransaction(DbTransaction trans, HelpOfferInfo fl)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                //store fineInfo and book list
                DateTime nowDt = Session.BpmDateTime.Now;

                //save tax code here
                int calCount = agentDa.GetNewCalCountOfCommissionPeriod(fl.SaveInfo.AgentId, fl.SaveInfo.Period);
                //TaxInfo taxInfo = agentDa.GetAgentTaxInformation(fl.SaveInfo.AgentId);
                decimal totalTax = (decimal)fl.TaxAmount; //CalculateAgencyTaxAmount(fl.SaveInfo.AgentId, fl.TotalCommission.Value);
                decimal totalVat = (decimal)fl.VatAmount; // CalculateVatAmount(fl.SaveInfo.AgentId, fl.TotalCommission.Value);
                decimal? totalFine = 0;

                //default = enable, if disabled, set fine to zero
                if (fl.EnableFine)
                    totalFine = fl.FineAmount;

                string fineEnalbed = fl.EnableFine ? "1" : "0";
                string cmId = agentDa.SaveAgencyCommission(trans, fl.HelpTransport, fl.HelpFarLand, fl.HelpSpecialMoney, calCount,
                                                fl.RunningBranchId, totalTax, fl.TotalCommission, totalFine, totalVat, fineEnalbed,
                                                fl.BaseCmAmount, fl.SpecialCmAmount, fl.InvCmAmount, fl.OverNinety, fl.ModifiedBy, Session.Branch.PostedServerId);
                string refId = null;

                //fill commission book element
                foreach (string s in fl.BookList)
                {
                    agentDa.InsertCommissionBookItem(trans, cmId, s, fl.ModifiedBy, Session.Branch.PostedServerId);
                }

                //start - end biollbookId
                refId = string.Format("{0}-{1}", fl.BookList[0], fl.BookList[fl.BookList.Count - 1]);

                // Not need add fine and commission to AP And AR
                ////will not go throght this if fine disabled
                //if (totalFine > 0)
                //{
                //    //Insert fine to AR
                //    AccountReceiveInfo bookFine = new AccountReceiveInfo();
                //    bookFine.Description = string.Format("เลขที่ใบสำคัญจ่าย : {0}", cmId);
                //    bookFine.GAmount = totalFine;
                //    bookFine.Amount = totalFine;
                //    bookFine.DueDt = null;
                //    bookFine.CaId = fl.SaveInfo.AgentId.PadLeft(12, '0');
                //    bookFine.Period = fl.SaveInfo.Period;
                //    PayFineDebt(trans, bookFine, null);
                //}

                ////save commission to AP
                //if (fl.TotalCommission > 0)
                //{
                //    agentDa.InsertCommissionItemToAP(trans, fl.SaveInfo.AgentId, refId, fl.TotalCommission, cmId, totalVat, totalTax);
                //}

                return cmId;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string SaveCommission(DbTransaction trans, HelpOfferInfo fl)
        {
            try
            {
                if (trans == null)
                {
                    Database db = DatabaseFactory.CreateDatabase("POSDatabase");

                    using (DbConnection conn = db.CreateConnection())
                    {
                        conn.Open();
                        trans = conn.BeginTransaction();
                        try
                        {
                            string cmId = SaveCommissionAsTransaction(trans, fl);
                            trans.Commit();
                            return cmId;
                        }
                        catch (Exception e)
                        {
                            trans.Rollback();
                            throw;
                        }
                    }
                }
                else
                {
                    string cmId = SaveCommissionAsTransaction(trans, fl);
                    return cmId;
                }
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public List<string> GetListOfCreatedDate(BookSearchInfo searchInfo)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            List<string> createDtList = new List<string>();
            try
            {
                DataTable dt = agentDa.SelectBillBookCreateDate(searchInfo.AgentId, searchInfo.BillPeriod);
                foreach (DataRow r in dt.Rows)
                {
                    DateTime createDt = (DateTime)DaHelper.GetDate(r, "CreateDate");
                    string dtStr = createDt.ToString("dd/MM/yyyy", _th_dt);
                    if (!createDtList.Contains(dtStr))
                        createDtList.Add(dtStr);
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return createDtList;
        }

        public List<string> BookListByCreateDate(BookSearchInfo searchInfo)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            List<string> bookInfoList = new List<string>();
            try
            {
                DateTime retDt = agentDa.SelectBillBookReturnDate(searchInfo.AgentId, searchInfo.BillPeriod, searchInfo.CreateDate);
                string returnDtStr = retDt.ToString("dd/MM/yyyy", _th_dt);
                int? bookLot = 0;
                bookInfoList.Add(returnDtStr);

                //next get billbook range of receive count
                List<int> sortList = new List<int>();
                DataTable dt = agentDa.SelectBillBookCountRange(searchInfo.AgentId, searchInfo.BillPeriod, searchInfo.CreateDate);
                foreach (DataRow r in dt.Rows)
                    sortList.Add((int)DaHelper.GetByte(r, "ReceiveCount"));
                // get booklot
                if (dt.Rows.Count > 0)
                {
                    bookLot = DaHelper.GetInt(dt.Rows[0], "BookLot");
                }

                sortList.Sort();
                string countRange = null;
                if (sortList.Count != 0)
                    countRange = string.Format("{0}/{1}-{2}", bookLot.Value.ToString().PadLeft(2, '0'), sortList[0].ToString().PadLeft(2, '0'), sortList[sortList.Count - 1].ToString().PadLeft(2, '0'));

                bookInfoList.Add(countRange);
            }
            catch (Exception e)
            {
                throw;
            }

            return bookInfoList;
        }


        private SpecialCommissionInfo CalculateSpecialCommission(FeeBaseElement comRate)
        {
            SpecialCommissionInfo comInfo = new SpecialCommissionInfo();
            //comInfo.CompletedBillPercent = calInfo.PercentPaidOfAllBillCollected;  //repeated write- fine
            comInfo.CompletedBillPercent = comRate.BillingHundredPercent.Value;
            decimal lowerBound = 75;
            decimal upperBound = 90;

            if (_sumSpecialCommission != null)
            {
                List<InBoundCollectionInfo> ib = new List<InBoundCollectionInfo>();
                //75-90 , 90-99 %
                InitiateInBoundCollection(ib);
                comInfo.InBoundCollectionInfoList = ib;

                //start calculating
                if (_sumSpecialCommission.IsAllBillCollected)
                {
                    comInfo.CompletedBillTotal = _sumSpecialCommission.TotalBaseCommission * (comRate.BillingHundredPercent.Value / 100); // 20% of base commission                        
                }
                else
                {
                    //logical amount of bill calculated from the lass percent
                    int noPaidBill = (int)Math.Round((lowerBound / 100) * _sumSpecialCommission.TotalBillCount, 0);
                    int billCollected = (int)Math.Round((_sumSpecialCommission.Percent / 100) * _sumSpecialCommission.TotalBillCount, 0);
                    //int seventyFiveBill = (int)Math.Round( * _sumSpecialCommission.TotalBillCount, 0 );
                    int ninetyBill = (int)Math.Round((upperBound / 100) * _sumSpecialCommission.TotalBillCount, 0);
                    int toPaidBill = billCollected - noPaidBill;

                    comInfo.InBoundCollectionInfoList[0].PaidPerBill = _sumSpecialCommission.NinetyPaidPerBill; //mutiple rewrite fine!                    
                    comInfo.InBoundCollectionInfoList[1].PaidPerBill = _sumSpecialCommission.NinetyNinePaidPerBill;

                    if (_sumSpecialCommission.Percent > lowerBound && _sumSpecialCommission.Percent <= upperBound)
                    {
                        ////find number of bills ranage (75-90)
                        //int seventyFivetoNinety = (int)(ninetyBill - noPaidBill);
                        //toPaidBill = toPaidBill - seventyFivetoNinety;

                        //นำจำนวน % ที่แตกต่าง คำนวณเป็นจำนวนเงิน
                        int seventyFivetoNinety = (int)((_sumSpecialCommission.Percent - lowerBound) * _sumSpecialCommission.TotalBillCount / 100);
                        comInfo.InBoundCollectionInfoList[0].BillCount = seventyFivetoNinety;
                        comInfo.InBoundCollectionInfoList[0].Total = seventyFivetoNinety * _sumSpecialCommission.NinetyPaidPerBill;
                    }
                    else if (_sumSpecialCommission.Percent > upperBound)
                    {
                        ////find number of bills range (90-99)
                        //int seventyFivetoNinety = (int)(ninetyBill - noPaidBill);
                        //toPaidBill = toPaidBill - seventyFivetoNinety;
                        //int ninetyUp = toPaidBill;

                        //75-90
                        //นำจำนวน % ที่แตกต่าง คำนวณเป็นจำนวนเงิน
                        int seventyFivetoNinety = Convert.ToInt32(Math.Round(((upperBound - lowerBound) * _sumSpecialCommission.TotalBillCount / 100), 0));
                        comInfo.InBoundCollectionInfoList[0].BillCount = seventyFivetoNinety;
                        comInfo.InBoundCollectionInfoList[0].Total = seventyFivetoNinety * _sumSpecialCommission.NinetyPaidPerBill;

                        //90- <100
                        int ninetyUp = Convert.ToInt32(Math.Round(((_sumSpecialCommission.Percent - (upperBound + (decimal)0.01)) * _sumSpecialCommission.TotalBillCount / 100), 0));
                        comInfo.InBoundCollectionInfoList[1].BillCount = ninetyUp;
                        comInfo.InBoundCollectionInfoList[1].Total = ninetyUp * _sumSpecialCommission.NinetyNinePaidPerBill;
                    }
                    else
                    {
                        //sorry! no paid 
                    }

                    comInfo.InBoundBillTotal = comInfo.InBoundCollectionInfoList[0].Total + comInfo.InBoundCollectionInfoList[1].Total;
                    comInfo.TotalBillAmount = _sumSpecialCommission.TotalAmount;
                }
            }

            return comInfo;
        }

        private List<CommissionBaseInfo> PopulateBaseCommission(BookSearchInfo searchInfo, FeeBaseElement comRate)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            List<CommissionBaseInfo> comBaseList = new List<CommissionBaseInfo>();

            try
            {
                //book holder has been checked when retreive agency info - no need to check here

                //Initiate Customer location type
                InitiateCustomerType(comBaseList);

                //get billbook list that have status 'T' - CUT
                DataTable bookList = null;
                if (searchInfo.IsReprint)
                    bookList = agentDa.GetBillBookIdWithCutStatusByAgentIdReprint(searchInfo.BillPeriod, searchInfo.AgentId, searchInfo.CreateDate);
                else
                    bookList = agentDa.GetBillBookIdWithCutStatusByAgentId(searchInfo.BillPeriod, searchInfo.AgentId, searchInfo.CreateDate);

                #region Get agency type
                string agencyBpType = agentDa.GetBusinessPartnerTypeOfAgent(searchInfo.AgentId);

                //different type different pay               
                comBaseList[0].RegularPerson = comRate.HouseRegRate; //home
                comBaseList[1].RegularPerson = comRate.CorpRegRate; //corp
                comBaseList[2].RegularPerson = comRate.GovRegRate; //gov
                comBaseList[3].RegularPerson = comRate.HouseRegRate; //gov

                comBaseList[0].Corporation = comRate.HouseGrpRate;
                comBaseList[1].Corporation = comRate.CorpGrpRate;
                comBaseList[2].Corporation = comRate.GovGrpRate;
                comBaseList[3].Corporation = comRate.HouseGrpRate;

                #endregion

                List<SpecialCommissionCalInfo> spCalInfoList = new List<SpecialCommissionCalInfo>();

                //find CaId and their bill statuses and then calculate it
                foreach (DataRow r in bookList.Rows)
                {
                    string bookId = r["BookId"].ToString();
                    DateTime tmpRetDt = Convert.ToDateTime(r["ReturnDueDate"]);
                    DateTime chkDt = Convert.ToDateTime(r["CheckInDate"]);

                    DateTime retDueDt = new DateTime(tmpRetDt.Year, tmpRetDt.Month, tmpRetDt.Day);
                    DateTime checkInDt = new DateTime(chkDt.Year, chkDt.Month, chkDt.Day);

                    #region Special Commission

                    SpecialCommissionCalInfo specialInfo = null;
                    bool specialEnable = false;

                    ////micro sec comparasion
                    ////check in on time only
                    //if (retDueDt >= checkInDt)
                    //{
                    specialInfo = new SpecialCommissionCalInfo();
                    specialInfo.BookId = bookId;
                    specialEnable = true;
                    //}

                    #endregion                   

                    #region Base Commission
                    //base commission 
                    DataTable caList = agentDa.SelectCaBillStatusByBookId(bookId);
                    foreach (DataRow ca in caList.Rows)
                    {
                        if (Convert.ToChar(ca["RateCatId"]) == '1') // home - บ้านอยู่อาศัย
                        {
                            #region Home
                            decimal? totalAmount = DaHelper.GetDecimal(ca, "TotalAmount");
                            // รัฐบาลรับภาระ
                            if (totalAmount == 0)
                            {
                                CommissionBaseInfo govPaid = comBaseList[3];
                                if (Convert.ToChar(ca["AboId"]) == 'N') //first time
                                {
                                    //agency - full payment only 
                                    if (Convert.ToChar(ca["AbsId"]) == 'Y' || Convert.ToChar(ca["AbsId"]) == 'D') // เก็บเงินได้ หรือ เก็บซ้ำซ้อน(POS&Agency)
                                    {
                                        govPaid.AmountFirstActual += totalAmount;
                                        govPaid.BillCountFirstActual++;
                                    }
                                    //every bill
                                    govPaid.BillCountFirstAll++;
                                    govPaid.AmountFirstAll += totalAmount;
                                }
                                else if (Convert.ToChar(ca["AboId"]) == 'R') //repeat ทวน
                                {
                                    //agency - full payment only 
                                    if (Convert.ToChar(ca["AbsId"]) == 'Y' || Convert.ToChar(ca["AbsId"]) == 'D') // เก็บเงินได้ หรือ เก็บซ้ำซ้อน(POS&Agency)
                                    {
                                        govPaid.AmountRepearActual += totalAmount;
                                        govPaid.BillCountRepeatActual++;
                                    }
                                    //every bill
                                    govPaid.BillCountRepeatAll++;
                                    govPaid.AmountRepeatAll += totalAmount;
                                }
                            }
                            else
                            {
                                CommissionBaseInfo homeContract = comBaseList[0];

                                if (Convert.ToChar(ca["AboId"]) == 'N') //first time
                                {
                                    //agency - full payment only 
                                    if (Convert.ToChar(ca["AbsId"]) == 'Y' || Convert.ToChar(ca["AbsId"]) == 'D') // เก็บเงินได้ หรือ เก็บซ้ำซ้อน(POS&Agency)
                                    {
                                        homeContract.AmountFirstActual += totalAmount;
                                        homeContract.BillCountFirstActual++;

                                        //forspecial commission
                                        if (specialEnable)
                                        {
                                            specialInfo.ActualAmount += totalAmount;
                                            specialInfo.ActualBillCount++;
                                            specialInfo.HomeBillCount++;
                                        }
                                    }

                                    //every bill
                                    homeContract.BillCountFirstAll++;
                                    homeContract.AmountFirstAll += totalAmount;

                                    //special commission - treat this per billbook
                                    if (specialEnable)
                                    {
                                        specialInfo.TotalBillCount++;
                                        specialInfo.TotalAmount += totalAmount;
                                    }

                                }
                                else if (Convert.ToChar(ca["AboId"]) == 'R') //repeat ทวน
                                {
                                    //agency - full payment only 
                                    if (Convert.ToChar(ca["AbsId"]) == 'Y' || Convert.ToChar(ca["AbsId"]) == 'D') // เก็บเงินได้ หรือ เก็บซ้ำซ้อน(POS&Agency)
                                    {
                                        homeContract.AmountRepearActual += totalAmount;
                                        homeContract.BillCountRepeatActual++;

                                        //verify! - if upperbound % of special payment calculated based on repeat collect based commission
                                        if (specialEnable)
                                            specialInfo.HomeBillCount++;
                                    }

                                    //every bill
                                    homeContract.BillCountRepeatAll++;
                                    homeContract.AmountRepeatAll += totalAmount;

                                    //special commission - treat this per billbook
                                    if (specialEnable)
                                    {
                                        specialInfo.TotalBillCount++;
                                        specialInfo.TotalAmount += totalAmount;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (Convert.ToChar(ca["RateCatId"]) == '2') //small business - กิจการขนาดเล็ก
                        {
                            #region Coperation People
                            CommissionBaseInfo corpContract = comBaseList[1];
                            if (Convert.ToChar(ca["AboId"]) == 'N') //first time
                            {
                                //agency - full payment only 
                                if (Convert.ToChar(ca["AbsId"]) == 'Y' || Convert.ToChar(ca["AbsId"]) == 'D') // เก็บเงินได้ หรือ เก็บซ้ำซ้อน(POS&Agency)
                                {
                                    corpContract.AmountFirstActual += DaHelper.GetDecimal(ca, "TotalAmount");
                                    corpContract.BillCountFirstActual++;

                                    //forspecial commission
                                    if (specialEnable)
                                    {
                                        specialInfo.ActualAmount += DaHelper.GetDecimal(ca, "TotalAmount");
                                        specialInfo.ActualBillCount++;
                                        specialInfo.CorpBillCount++;
                                    }
                                }

                                //every bill
                                corpContract.BillCountFirstAll++;
                                corpContract.AmountFirstAll += DaHelper.GetDecimal(ca, "TotalAmount");

                                //special commission - treat this per billbook
                                if (specialEnable)
                                {
                                    specialInfo.TotalBillCount++;
                                    specialInfo.TotalAmount += DaHelper.GetDecimal(ca, "TotalAmount");
                                }

                            }
                            else if (Convert.ToChar(ca["AboId"]) == 'R') //repeat 
                            {
                                //agency - full payment only 
                                if (Convert.ToChar(ca["AbsId"]) == 'Y' || Convert.ToChar(ca["AbsId"]) == 'D') // เก็บเงินได้ หรือ เก็บซ้ำซ้อน(POS&Agency)
                                {
                                    corpContract.AmountRepearActual += DaHelper.GetDecimal(ca, "TotalAmount");
                                    corpContract.BillCountRepeatActual++;

                                    //verify! - if 20% of special calculated based on repeat collect based commission
                                    if (specialEnable)
                                        specialInfo.CorpBillCount++;
                                }

                                //every bill
                                corpContract.BillCountRepeatAll++;
                                corpContract.AmountRepeatAll += DaHelper.GetDecimal(ca, "TotalAmount");

                                //special commission - treat this per billbook
                                if (specialEnable)
                                {
                                    specialInfo.TotalBillCount++;
                                    specialInfo.TotalAmount += DaHelper.GetDecimal(ca, "TotalAmount");
                                }
                            }
                            #endregion
                        }
                        else if (ca["RateCatId"].ToString() == "6") // gov - สถานที่ราชการ
                        {
                            #region Goverment
                            CommissionBaseInfo govContract = comBaseList[2];
                            if (Convert.ToChar(ca["AboId"]) == 'N') //first time
                            {
                                //agency - full payment only 
                                if (Convert.ToChar(ca["AbsId"]) == 'Y' || Convert.ToChar(ca["AbsId"]) == 'D') // เก็บเงินได้ หรือ เก็บซ้ำซ้อน(POS&Agency)
                                {
                                    govContract.AmountFirstActual += DaHelper.GetDecimal(ca, "TotalAmount");
                                    govContract.BillCountFirstActual++;

                                    //forspecial commission
                                    if (specialEnable)
                                    {
                                        specialInfo.ActualAmount += DaHelper.GetDecimal(ca, "TotalAmount");
                                        specialInfo.ActualBillCount++;
                                        specialInfo.GovBillCount++;
                                    }
                                }

                                //every bill
                                govContract.BillCountFirstAll++;
                                govContract.AmountFirstAll += DaHelper.GetDecimal(ca, "TotalAmount");

                                //special commission - treat this per billbook
                                if (specialEnable)
                                {
                                    specialInfo.TotalBillCount++;
                                    specialInfo.TotalAmount += DaHelper.GetDecimal(ca, "TotalAmount");
                                }

                            }
                            else if (Convert.ToChar(ca["AboId"]) == 'R') //repeat 
                            {
                                //agency - full payment only 
                                if (Convert.ToChar(ca["AbsId"]) == 'Y' || Convert.ToChar(ca["AbsId"]) == 'D') // เก็บเงินได้ หรือ เก็บซ้ำซ้อน(POS&Agency)
                                {
                                    govContract.AmountRepearActual += DaHelper.GetDecimal(ca, "TotalAmount");
                                    govContract.BillCountRepeatActual++;

                                    //verify! - if 20% of special calculated based on repeat collect based commission
                                    if (specialEnable)
                                        specialInfo.GovBillCount++;
                                }

                                //every bill
                                govContract.BillCountRepeatAll++;
                                govContract.AmountRepeatAll += DaHelper.GetDecimal(ca, "TotalAmount");

                                //special commission - treat this per billbook
                                if (specialEnable)
                                {
                                    specialInfo.TotalBillCount++;
                                    specialInfo.TotalAmount += DaHelper.GetDecimal(ca, "TotalAmount");
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            //should not get here 
                            //agency system supports only three types of RateCat of bill
                            //1. home
                            //2. small business 
                            //3. government  
                            //If there is the other type the logic will ignore calculating for commission
                        }

                    }

                    if (specialEnable)
                        spCalInfoList.Add(specialInfo);

                    #endregion

                }

                //find % of collected bill for comparing with % of collected payment
                //this is for calculate Special Commission
                foreach (CommissionBaseInfo com in comBaseList)
                {
                    if (com.AmountFirstAll != 0)
                        com.AmountFirstPercent = (com.AmountFirstActual * 100) / com.AmountFirstAll;
                    if (com.AmountRepeatAll != 0)
                        com.AmountRepeatPercent = (com.AmountRepearActual * 100) / com.AmountRepeatAll;
                    if (com.BillCountFirstAll != 0)
                        com.BillCountFirstPercent = ((decimal)com.BillCountFirstActual * 100) / (decimal)com.BillCountFirstAll;
                    if (com.BillCountRepeatAll != 0)
                        com.BillCountRepeatPercent = ((decimal)com.BillCountRepeatActual * 100) / (decimal)com.BillCountRepeatAll;
                }

                //here we get base commission for any type of agency
                if (agencyBpType == "1") //บุคคลธรรมดา
                {
                    comBaseList[0].TotalBase = (comBaseList[0].BillCountFirstActual + comBaseList[0].BillCountRepeatActual) * comBaseList[0].RegularPerson;
                    comBaseList[1].TotalBase = (comBaseList[1].BillCountFirstActual + comBaseList[1].BillCountRepeatActual) * comBaseList[1].RegularPerson;
                    comBaseList[2].TotalBase = (comBaseList[2].BillCountFirstActual + comBaseList[2].BillCountRepeatActual) * comBaseList[2].RegularPerson;
                    comBaseList[3].TotalBase = (comBaseList[3].BillCountFirstActual + comBaseList[3].BillCountRepeatActual) * comBaseList[3].RegularPerson;
                }
                else if (agencyBpType == "2") /// นิติบุคคล
                {
                    comBaseList[0].TotalBase = (comBaseList[0].BillCountFirstActual + comBaseList[0].BillCountRepeatActual) * comBaseList[0].Corporation;
                    comBaseList[1].TotalBase = (comBaseList[1].BillCountFirstActual + comBaseList[1].BillCountRepeatActual) * comBaseList[1].Corporation;
                    comBaseList[2].TotalBase = (comBaseList[2].BillCountFirstActual + comBaseList[2].BillCountRepeatActual) * comBaseList[2].Corporation;
                    comBaseList[3].TotalBase = (comBaseList[3].BillCountFirstActual + comBaseList[2].BillCountRepeatActual) * comBaseList[3].Corporation;
                }

                #region Calculate special commission - each billBook

                SpecialCommissionCalInfo sumInfo = new SpecialCommissionCalInfo();
                decimal? billPercentCollectCount = (decimal)0.0;
                decimal? billPercentCollectAmount = (decimal)0.0;

                foreach (SpecialCommissionCalInfo calInfo in spCalInfoList)
                {
                    //find all amount and bill count
                    sumInfo.ActualBillCount += calInfo.ActualBillCount;
                    sumInfo.ActualAmount += calInfo.ActualAmount;
                    sumInfo.TotalBillCount += calInfo.TotalBillCount;
                    sumInfo.TotalAmount += calInfo.TotalAmount;

                    //calcualte based commission per collection type for special commission
                    //find total base commission
                    if (agencyBpType == "1")  // บุคคลธรรมดา
                    {
                        sumInfo.TotalBaseCommission += calInfo.HomeBillCount * comBaseList[0].RegularPerson;
                        sumInfo.TotalBaseCommission += calInfo.CorpBillCount * comBaseList[1].RegularPerson;
                        sumInfo.TotalBaseCommission += calInfo.GovBillCount * comBaseList[2].RegularPerson;
                        sumInfo.TotalBaseCommission += calInfo.GovPaidCount * comBaseList[3].RegularPerson;
                    }
                    else if (agencyBpType == "2") // นิติบุคคล
                    {
                        sumInfo.TotalBaseCommission = calInfo.HomeBillCount * comBaseList[0].Corporation;
                        sumInfo.TotalBaseCommission += calInfo.CorpBillCount * comBaseList[1].Corporation;
                        sumInfo.TotalBaseCommission += calInfo.GovBillCount * comBaseList[2].Corporation;
                        sumInfo.TotalBaseCommission += calInfo.GovPaidCount * comBaseList[3].Corporation;
                    }
                }

                //fill commission rate
                sumInfo.PercentPaidOfAllBillCollected = comRate.BillingHundredPercent;
                sumInfo.NinetyPaidPerBill = comRate.BillingNinetyPercent;
                sumInfo.NinetyNinePaidPerBill = comRate.BillingNinetyNinePercent;

                //all billbooks
                if (sumInfo.TotalBillCount != 0)
                    billPercentCollectCount = Math.Round(((decimal)sumInfo.ActualBillCount * 100) / (decimal)sumInfo.TotalBillCount, 2);

                if (sumInfo.TotalAmount != 0)
                    billPercentCollectAmount = Math.Round((decimal)((sumInfo.ActualAmount * 100) / sumInfo.TotalAmount), 2);

                //for calculating Special Commissoin
                //PEA special commission criteria (100% , 99-90% and 90-75% )
                if (billPercentCollectCount > billPercentCollectAmount)
                {
                    if (billPercentCollectAmount == 100)
                        sumInfo.IsAllBillCollected = true;
                    else
                        sumInfo.Percent = (decimal)billPercentCollectAmount;
                }
                else  //count% <= amount% so choose count
                {
                    if (billPercentCollectCount == 100)
                        sumInfo.IsAllBillCollected = true;
                    else
                        sumInfo.Percent = (decimal)billPercentCollectCount;
                }

                #endregion

                //save to global 
                _sumSpecialCommission = sumInfo;
                _comBaseList = comBaseList;


            }
            catch (Exception e)
            {
                throw;
            }

            return comBaseList;
        }


        public AgentInfo GetAgentHelpInformation(string agentId)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                AgentInfo temp = agentDa.GetAgentInformation(agentId);

                return temp;
            }
            catch (Exception e)
            {
                throw;
            }
        }


        public decimal? GetAndDisplayAdvPaymentAmount(BookSearchInfo searchInfo)
        {
            //ONLY BILL STATUS "CUT"
            AgencyDataAccess agentDa = new AgencyDataAccess();

            try
            {
                DataTable bookList = null;
                if (searchInfo.IsReprint)
                    bookList = agentDa.GetBillBookIdWithCutStatusByAgentIdReprint(searchInfo.BillPeriod, searchInfo.AgentId, searchInfo.CreateDate);
                else
                    bookList = agentDa.GetBillBookIdWithCutStatusByAgentId(searchInfo.BillPeriod, searchInfo.AgentId, searchInfo.CreateDate);

                decimal? sum = 0;
                foreach (DataRow r in bookList.Rows)
                {
                    sum += DaHelper.GetDecimal(r, "AdvPayAmount");
                }
                return sum;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private bool IsFineEnabled(BookSearchInfo searchInfo)
        {
            try
            {
                AgencyDataAccess agDa = new AgencyDataAccess();
                return !agDa.GetBaseCommissionRate(searchInfo.BranchId).PenaltyWaive;

                //if (!searchInfo.PenaltyWaiveFlag)
                //{
                //    return searchInfo.PenaltyWaiveFlag;
                //}

                //AgencyDataAccess agentDa = new AgencyDataAccess();
                //return agentDa.IsFineEnabled(searchInfo);
            }
            catch (Exception e)
            {
                throw;
            }
        }


        public HelpOfferInfo GetCommissionHelpInfo(BookSearchInfo searchInfo)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                return agentDa.GetCommissionHelpInfo(searchInfo);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public CommissionInfo CalculateCommissionAndFine(BookSearchInfo searchInfo, FeeBaseElement comRate, BooniesCommissionInfo hp)
        {
            try
            {
                CommissionInfo comInfo = new CommissionInfo();
                comInfo.BaseCommission = PopulateBaseCommission(searchInfo, comRate);
                comInfo.SpecialCommission = CalculateSpecialCommission(comRate);
                comInfo.InvoiceCommission = CalculateInvoiceCommission(searchInfo, comRate);
                comInfo.VatRate = GetAgencyVatRate(searchInfo.AgentId);
                comInfo.TaxRate = GetAgencyTaxRate(searchInfo.AgentId);


                if (IsFineEnabled(searchInfo)) // this case could happen only when reprinting
                {
                    comInfo.FineInfo = CalculateFine(searchInfo, comRate);
                }
                else  //disabled means NO FINE!
                {
                    FineInfo fineInfo = new FineInfo();
                    fineInfo.Enabled = false;
                    fineInfo.Amount = 0;
                    comInfo.FineInfo = fineInfo;

                    _saveInfo = new SaveCommissionInfo();
                    _saveInfo.AgentId = searchInfo.AgentId;
                    _saveInfo.Period = searchInfo.BillPeriod;

                }

                comInfo.HelpInfo = GetCommissionHelpInfo(searchInfo);
                comInfo.FineDetail = comInfo.FineInfo.FineDetail;

                //add list of billbook to helpinfo
                comInfo.HelpInfo.BookList = GetBillBookList(searchInfo);

                decimal? total = 0;
                //base
                foreach (CommissionBaseInfo bInfo in _comBaseList)
                    total += bInfo.TotalBase;

                //special
                //total += (comInfo.SpecialCommission.CompletedBillTotal + comInfo.SpecialCommission.InBoundBillTotal);
                //total += comInfo.InvoiceCommission.Total;
                //total += (hp.ExtraForBoonies + hp.SpecialOffer + hp.Transport);
                //total -= comInfo.FineInfo.Amount;

                searchInfo.TotalCommission = total;
                //exclude tax
                //comInfo.TaxInfo = CalculateTax(searchInfo);
                //comInfo.TaxDetail = GetTaxDetail();
                comInfo.SaveInfo = _saveInfo;
                return comInfo;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<string> GetBillBookList(BookSearchInfo searchInfo)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            List<string> retVals = new List<string>();
            DataTable bookList = new DataTable();
            if (searchInfo.IsReprint)
                bookList = agentDa.GetBillBookIdWithCutStatusByAgentIdReprint(searchInfo.BillPeriod, searchInfo.AgentId, searchInfo.CreateDate);
            else
                bookList = agentDa.GetBillBookIdWithCutStatusByAgentId(searchInfo.BillPeriod, searchInfo.AgentId, searchInfo.CreateDate);

            foreach (DataRow dr in bookList.Rows)
            {
                retVals.Add(DaHelper.GetString(dr, "BookId"));
            }
            return retVals;
        }


        //reprint
        public CommissionInfo CalculateCommissionAndFineReport(BookSearchInfo searchInfo, FeeBaseElement comRate, BooniesCommissionInfo hp)
        {
            try
            {
                CommissionInfo comInfo = new CommissionInfo();
                comInfo.BaseCommission = PopulateBaseCommission(searchInfo, comRate);
                comInfo.SpecialCommission = CalculateSpecialCommission(comRate);
                comInfo.InvoiceCommission = CalculateInvoiceCommission(searchInfo, comRate);

                if (IsFineEnabled(searchInfo)) // this case could happen only when reprinting
                {
                    comInfo.FineInfo = CalculateFine(searchInfo, comRate);
                }
                else  //disabled means NO FINE!
                {
                    FineInfo fineInfo = new FineInfo();
                    fineInfo.Enabled = false;
                    fineInfo.Amount = 0;
                    comInfo.FineInfo = fineInfo;
                }

                comInfo.HelpInfo = GetCommissionHelpInfo(searchInfo);
                comInfo.FineDetail = comInfo.FineInfo.FineDetail;

                decimal? total = 0;
                //base
                foreach (CommissionBaseInfo bInfo in _comBaseList)
                    total += bInfo.TotalBase;

                //special
                //total += (comInfo.SpecialCommission.CompletedBillTotal + comInfo.SpecialCommission.InBoundBillTotal);
                //total += comInfo.InvoiceCommission.Total;
                //total += (hp.ExtraForBoonies + hp.SpecialOffer + hp.Transport);
                //total -= comInfo.FineInfo.Amount;

                searchInfo.TotalCommission = total;
                //exclude tax
                //comInfo.TaxInfo = CalculateTax(searchInfo);
                //comInfo.TaxDetail = GetTaxDetail();
                comInfo.SaveInfo = _saveInfo;
                return comInfo;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool IsCommissionCalculated(BookSearchInfo searchInfo)
        {
            List<CalculatedCommissionInfo> calList = GetCalCountOfPeriodByAgentId(searchInfo);
            if (calList.Count == 0) return false;
            else return true;
        }

        public bool IsBillBookCalculated(string billBookId)
        {
            CommissionDataAccess comDa = new CommissionDataAccess();
            return comDa.IsCommissionCalculated(billBookId);
        }


        public int GetCommissionCountOfPeriod(BookSearchInfo searchInfo)
        {
            CommissionDataAccess comDa = new CommissionDataAccess();
            return comDa.GetMaxCalculatedCommission(searchInfo.AgentId.PadLeft(12, '0'), searchInfo.BillPeriod);
        }

        public List<CalculatedCommissionInfo> GetCalCountOfPeriodByAgentId(BookSearchInfo searchInfo)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            List<CalculatedCommissionInfo> calList = null;

            try
            {
                calList = agentDa.SelectCalculatedAgentCommission(searchInfo);

            }
            catch (Exception e)
            {
                throw;
            }

            return calList;
        }

        public TravelHelpRate GetTravelHelpRate(TravelHelpRateConditionInfo spcCondition)
        {
            CommissionDataAccess comDa = new CommissionDataAccess();
            AgencyModuleConfigService configBs = new AgencyModuleConfigService();
            BillBookDataAccess bookDa = new BillBookDataAccess();

            List<TravelHelpRate> spcRates = null;
            BillBookHeaderInfo bookHeader = new BillBookHeaderInfo();
            TravelHelpRate travelValue = new TravelHelpRate();
            FeeBaseElement feeBase = new FeeBaseElement();
            int maxCalCount = 0;

            try
            {
                if (!spcCondition.IsReprint)
                {
                    spcRates = comDa.GetTravelHelpRate(spcCondition);

                    if (spcRates.Count > 0)
                    {
                        //get totolbill and totalbill collected.
                        bookHeader = bookDa.GetBillCollectedCountByAgecyCreateDate(spcCondition.AgencyId, spcCondition.BookPeriod, spcCondition.CreateDate);
                        // get commission rate
                        feeBase = configBs.GetBaseCommissionRate(spcCondition.BranchId);

                        if (IsFarlandHelp(spcRates, spcCondition))
                        {
                            //get only mru that assign farLand
                            Hashtable collectInfo = GetFarLandCollected(spcCondition);
                            int? totalCount = (int?)collectInfo["TotalCount"];
                            int? collectCount = (int?)collectInfo["CollectCount"];
                            //check rate
                            if (totalCount != null && collectCount != null && totalCount != 0)
                            {
                                decimal? collectPercent = collectCount / (decimal?)totalCount * 100;
                                if (collectPercent >= feeBase.CollectedPercent)
                                {
                                    if (collectCount > feeBase.CaCount)
                                        travelValue.FarlandHelp = feeBase.UpperRate;
                                    else
                                        travelValue.FarlandHelp = feeBase.LowerRate;
                                }
                                else
                                {
                                    travelValue.FarlandHelp = 0;
                                }
                            }
                            else
                            {
                                travelValue.FarlandHelp = 0;
                            }
                        }

                        if (IsWaterHelpRate(spcRates))
                        {
                            //get only mru that assign waterHelp
                            Hashtable collectInfo = GetWaterHelpCollected(spcCondition);
                            int? totalCount = (int?)collectInfo["TotalCount"];
                            int? collectCount = (int?)collectInfo["CollectCount"];
                            if (totalCount != null && collectCount != null && totalCount != 0)
                            {
                                decimal? collectPercent = collectCount / (decimal?)totalCount * 100;
                                if (collectPercent >= feeBase.CollectedPercent)
                                {
                                    //check calculated time 
                                    maxCalCount = comDa.GetMaxCalculatedCommission(spcCondition.AgencyId, spcCondition.BookPeriod);
                                    //limit calculate commission
                                    if ((feeBase.MaxCommissionCalCount == 0) || (maxCalCount < feeBase.MaxCommissionCalCount))
                                    {
                                        //check rate
                                        if (collectCount >= feeBase.CaCount)
                                            travelValue.WaterHelp = feeBase.UpperRate;
                                        else
                                            travelValue.WaterHelp = feeBase.LowerRate;
                                    }
                                    else
                                    {
                                        travelValue.WaterHelp = 0;
                                    }
                                }
                                else
                                {
                                    travelValue.WaterHelp = 0;
                                }
                            }
                        }

                        if (IsTransportRate(spcRates))
                        {
                            //check calculated time 
                            maxCalCount = comDa.GetMaxCalculatedCommission(spcCondition.AgencyId, spcCondition.BookPeriod);
                            if ((feeBase.MaxCommissionCalCount == 0) || (maxCalCount < feeBase.MaxCommissionCalCount && spcRates.Count > 0))
                            {
                                travelValue.TransportHelp = spcRates[0].TransportHelp * 2;
                            }
                        }
                    }
                }
                else
                {
                    travelValue = comDa.GetTravelHelpHistory(spcCondition);
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return travelValue;
        }

        public FeeBaseElement GetFeeBase(string branchId)
        {
            AgencyModuleConfigService configBs = new AgencyModuleConfigService();
            FeeBaseElement feeBase = new FeeBaseElement();
            return configBs.GetBaseCommissionRate(branchId);
        }

        public Hashtable GetFarLandCollected(TravelHelpRateConditionInfo spcCondition)
        {
            CommissionDataAccess comDa = new CommissionDataAccess();
            Hashtable collectInfo = new Hashtable();
            collectInfo = comDa.GetFarLandCollectedCount(spcCondition.AgencyId, spcCondition.BookPeriod, spcCondition.BranchId);
            return collectInfo;
        }

        public Hashtable GetWaterHelpCollected(TravelHelpRateConditionInfo spcCondition)
        {
            CommissionDataAccess comDa = new CommissionDataAccess();
            Hashtable collectInfo = new Hashtable();
            collectInfo = comDa.GetWaterHelpCollectedCount(spcCondition.AgencyId, spcCondition.BookPeriod, spcCondition.BranchId, spcCondition.CreateDate);
            return collectInfo;
        }

        private bool IsFarlandHelp(List<TravelHelpRate> travels, TravelHelpRateConditionInfo spcCondition)
        {
            bool retVal = false;
            foreach (TravelHelpRate t in travels)
            {
                if (t.TravelHelpType == (int)TravelHelpEnum.FARLANDHELP)
                {
                    retVal = isFarLandCalcuate(spcCondition);
                    break;
                }
            }
            return retVal;
        }

        private bool IsWaterHelpRate(List<TravelHelpRate> travels)
        {
            bool retVal = false;
            foreach (TravelHelpRate t in travels)
            {
                if (t.TravelHelpType == (int)TravelHelpEnum.WATERTRAVELHELP)
                {
                    retVal = true;
                    break;
                }
            }
            return retVal;
        }

        private bool IsTransportRate(List<TravelHelpRate> travels)
        {
            bool retVal = false;
            foreach (TravelHelpRate t in travels)
            {
                if (t.TravelHelpType == (int)TravelHelpEnum.NORMALTRAVELHELP)
                {
                    retVal = true;
                    break;
                }
            }
            return retVal;
        }

        // ค่าเดินทางคิดงวดสุดท้ายเท่านั้น
        private bool isFarLandCalcuate(TravelHelpRateConditionInfo spcCondition)
        {
            CommissionDataAccess commDa = new CommissionDataAccess();
            bool maxCollected = false;
            AgencyDataAccess agDa = new AgencyDataAccess();
            bool isFarLandCalculate = commDa.IsFarLandHelpCommissionCalculated(spcCondition.AgencyId, spcCondition.BookPeriod);
            if (!isFarLandCalculate)
            {
                List<LineInfo> lines = agDa.GetMaxLineCollecctCount(spcCondition.AgencyId, spcCondition.BookPeriod);
                List<LineInfo> linesInBook = agDa.GetLineByCreateDate(spcCondition.AgencyId, spcCondition.BookPeriod, spcCondition.CreateDate);
                foreach (LineInfo l1 in lines)
                {
                    foreach (LineInfo l2 in linesInBook)
                    {
                        if (l1.LineId == l2.LineId)
                        {
                            maxCollected = true;
                            break;
                        }
                    }
                    if (maxCollected)
                        break;
                }
            }
            return maxCollected;
        }

        public List<CommissionHistoryInfo> GetCommissionHistory(BookSearchInfo searchInfo)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            List<CommissionHistoryInfo> comList = null;

            try
            {
                comList = agentDa.SelectCommissionHistory(searchInfo);

            }
            catch (Exception e)
            {
                throw;
            }

            return comList;
        }

    }
}

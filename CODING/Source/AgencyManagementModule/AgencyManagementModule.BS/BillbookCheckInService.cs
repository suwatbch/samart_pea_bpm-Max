using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.AgencyManagementModule.Interface.Constants;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using PEA.BPM.AgencyManagementModule.BS.Constants;
using PEA.BPM.AgencyManagementModule.DA;

using PEA.BPM.Architecture.CommonUtilities;
using Microsoft.SqlServer.Server;



namespace PEA.BPM.AgencyManagementModule.BS
{
    public class BillbookCheckInService : IBillbookCheckInService
    {
        public BillBookCheckInInfo GetBillBookCheckInInfo(string billBookId)
        {
            BillBookCheckInInfo _retVal = new BillBookCheckInInfo();
            BillBookDataAccess billDA = new BillBookDataAccess();
            _retVal = billDA.GetBillCheckInInfomation(billBookId, false);
            return _retVal;
        }

        public BillBookCheckInInfo GetGroupInvoiceCheckInInfo(string groupInvoice, string branchId)
        {
            BillBookCheckInInfo _retVal = new BillBookCheckInInfo();
            BillBookDataAccess billDA = new BillBookDataAccess();
            _retVal = billDA.GetGroupInvoiceCheckInInfomation(groupInvoice, branchId, false);
            return _retVal;
        }

        public BillBookCheckInInfo GetGroupInvoiceCheckInHistory(string groupInvoice, string branchId)
        {
            BillBookCheckInInfo _retVal = new BillBookCheckInInfo();
            BillBookDataAccess billDA = new BillBookDataAccess();
            _retVal = billDA.GetGroupInvoiceCheckInInfomation(groupInvoice, branchId, true);
            return _retVal;
        }

        public BillBookCheckInInfo GetBillBookCheckInHistory(string billBookId)
        {
            BillBookCheckInInfo _retVal = new BillBookCheckInInfo();
            BillBookDataAccess billDA = new BillBookDataAccess();
            _retVal = billDA.GetBillCheckInInfomation(billBookId, true);
            return _retVal;
        }

        public BillBookCheckInInfo GetBillBookCheckInCancel(string billBookId)
        {
            BillBookCheckInInfo _retVal = new BillBookCheckInInfo();
            BillBookDataAccess billDA = new BillBookDataAccess();
            _retVal = billDA.GetBillCheckInCancel(billBookId);
            return _retVal;
        }


        #region "Connect TO POS"

        public decimal GetAdvPaidFromPOS(string billBookId)
        {
            BillBookDataAccess billDA = new BillBookDataAccess();
            try
            {
                decimal retVal = billDA.GetPrePaidAccountReceive(null, billBookId);

                return retVal;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ChequeInfo> GetChequeList(string billBookId, string invId)
        {
            BillBookDataAccess billDA = new BillBookDataAccess();
            try
            {
                List<ChequeInfo> retVal = billDA.GetChequeInfo(billBookId, invId);
                return retVal;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        #endregion        

        /// <summary>
        /// Calculate Total Not (GAmount - Prepaid)
        /// Update all CaId in billbook to paid
        /// Update all CaId in POS to paid
        /// Update all CaId in Agency to paid
        /// Create Account Recevie to POS
        /// </summary>
        /// <param name="billBookCheckIn"></param>
        /// <returns></returns>
        public bool CreateBillBookCheckIn(BillBookCheckInInfo billBookCheckIn, string branchId, string terminalId)
        {
            BillBookDataAccess billDA = new BillBookDataAccess();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    if (billBookCheckIn.BookType == (int)BookTypeEnum.BILLBOOK)
                        billDA.CheckInBillBook(billBookCheckIn, branchId, terminalId, trans);
                    else
                        billDA.CheckInGroupInvoice(billBookCheckIn, branchId, terminalId, trans);

                    trans.Commit();

                   
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    throw e;
                }
            }

            //try
            //{
                ///ENQUEUE INTERFACE BILLBOOK EXPORT
                ///--NO USE FOR NOW!!
                //if (billBookCheckIn.BookType == (int)BookTypeEnum.BILLBOOK)
                //new BillBookDataAccess().EnqueueBatchJobBillBook(
                //     PEA.BPM.Infrastructure.Interface.Constants.LocalSettingNames.DL010_EXPORT_BILLBOOK_INVOICE_BATCH
                //    ,new string[] { billBookCheckIn.BookId});
            //}
            //catch (Exception)
            //{ //Silent 
            //}

            return true;

            #region OldCode

            //using (DbConnection conn = db.CreateConnection())
            //{                
            //    conn.Open();
            //    DbTransaction trans = conn.BeginTransaction();
            //    try
            //    {
            //        AccountReceiveInfo ar = new AccountReceiveInfo();

            //        int InvoiceCounter = 0;
            //        string currentUserId = Session.User.Id;
                   
            //        //get total collect
            //        decimal? totalPaidAmount = billBookCheckIn.BillCollectAmount;
            //        int? totalBillCollected = billBookCheckIn.BillCollectCount;
            //        int itemCount = 0;
            //        string paymentId = string.Empty;
            //        string postServerId = Session.Branch.PostedServerId;

            //        ar.BranchId = branchId;
            //        ar.Period = DaHelper.SetBillPeriod(billBookCheckIn.BookPeriod);
            //        ar.UnitPrice = 0;
            //        ar.ControllerId = String.Empty;
            //        ar.PaidAmount = 0;
            //        ar.ModifiedBy = currentUserId;
            //        ar.ModifiedDt = Session.BpmDateTime.Now;
            //        ar.BookType = billBookCheckIn.BookType;

            //        if (billBookCheckIn.BookType == (int)BookTypeEnum.BILLBOOK)
            //        {
            //            //get prepaid from POS
            //            decimal prePaid = this.GetAdvPaidFromPOS(billBookCheckIn.BookId);
            //            decimal? totalNet = totalPaidAmount > prePaid ? totalPaidAmount - prePaid : 0;

            //            ar.BillBookId = billBookCheckIn.BookId;
            //            ar.GroupInvoiceNo = String.Empty;
            //            ar.CaId = billBookCheckIn.BillAgentId;
            //            ar.DtId = ARDeptType.NETPAID;
            //            ar.Description = String.Format("สมุดจ่ายบิลเลขที่ {0}", billBookCheckIn.BookId);
            //            ar.TaxCode = "OX";
            //            ar.DueDt = null;
            //            ar.VatAmount = 0;
            //            ar.Amount = totalNet;
            //            ar.GAmount = totalNet;
            //        }
            //        else //group invoice
            //        {
            //            //get invoice no. to insert to AR incase booktype is GroupInvoice 
            //            ar.BillBookId = String.Empty;
            //            ar.GroupInvoiceNo = billBookCheckIn.BookId;
            //            ar.CaId = billDA.GetCaPaidBy(trans, billBookCheckIn.BillBookCheckInDetail);
            //            ar.DtId = ARDeptType.GROUPINVOICE;
            //            ar.Description = String.Format("ตัดชำระแบบกลุ่มเลขที่ {0}", billBookCheckIn.BookId);
            //            ar.TaxCode = billDA.GetTaxCode(trans, billBookCheckIn.BillBookCheckInDetail);
            //            ar.DueDt = billDA.GetGroupInvoiceDueDate(trans, billBookCheckIn.BillBookCheckInDetail);
            //            ar.VatAmount = billBookCheckIn.TotalVat * totalPaidAmount / billBookCheckIn.TotalAmount;
            //            ar.Amount = totalPaidAmount - ar.VatAmount;
            //            ar.GAmount = totalPaidAmount;
            //        }

            //        //Get New groupInvoice no. if booktype is groupinvoice otherwise return billbook
            //        string outputNo = billDA.InsertAccountReceive(trans, ar, postServerId, terminalId);
            //        if (outputNo == String.Empty)
            //        {
            //            trans.Rollback();
            //            return false;
            //        }

            //        //Update AR by item set billbook = current billbook                    
            //        foreach (BillBookCheckinDetailInfo b in billBookCheckIn.BillBookCheckInDetail)
            //        {
            //            InvoiceCounter += 1;
            //            string billPeriod = DaHelper.SetBillPeriod(b.Period);
            //            //เก็บเงินได้
            //            if (b.AbsId == AbsIdEnum.COLLECTED)
            //            {
            //                itemCount = billDA.UpdateARStatus(trans, b.InvoiceNo, billPeriod, currentUserId, outputNo, postServerId);
            //                if (itemCount <= 0)
            //                {
            //                    trans.Rollback();
            //                    throw new Exception("PEA.BPM.AgencyManagementModule.BS.BillbookCheckInService.CreateBillBookCheckIn:Count of update line in ag_upd_ARStatus is less than or equal to zero. " +
            //                        "|InvoiceNo="+b.InvoiceNo+
            //                        "|Period="+billPeriod+
            //                        "|ModifiedBy="+currentUserId+
            //                        "|BillBookId="+outputNo+
            //                        "|PostServerId="+postServerId);
            //                    //return false;
            //                }
                            
            //                // if nerver checkin
            //                if (!b.IsCheckIn && billBookCheckIn.BookType == (int)BookTypeEnum.BILLBOOK)
            //                {
            //                    itemCount = billDA.InsertARPayment(trans, b.BookId, b.InvoiceNo, billPeriod, currentUserId, b.PmId, branchId, 1, postServerId, b.TotalAmount, terminalId);
            //                }                           
            //                // group invoicing update all collected include partial payment
            //                else if (billBookCheckIn.BookType == (int)BookTypeEnum.GROUP_INVOICE)
            //                {
            //                    itemCount = billDA.InsertGroupInvoiceARPayment(trans, b.PaidAmount, outputNo, b.InvoiceNo, billPeriod, currentUserId, b.PmId, branchId, 1, postServerId, InvoiceCounter == billBookCheckIn.BillBookCheckInDetail.Count ? 1 : 0, terminalId);
            //                }


            //                if (itemCount <= 0)
            //                {
            //                    trans.Rollback();
            //                    throw new Exception("PEA.BPM.AgencyManagementModule.BS.BillbookCheckInService.CreateBillBookCheckIn:Count of update line in ag_upd_UpdateARPayment or ag_upd_UpdateGroupInvoiceARPayment are less than or equal to zero. ");
            //                    //return false;
            //                }
            //                // Insert to payment incase paid by cheque
            //                if (b.PaidType == (int)PaidTypeEnum.CHEQUE)
            //                {
            //                    foreach (ChequeInfo c in b.ChequeList)
            //                    {
            //                        // insert payment at first time
            //                        if (paymentId == String.Empty)
            //                        {
            //                            //Create Payment 
            //                            PaymentInfo payment = new PaymentInfo();
            //                            payment.PaymentDt = Session.BpmDateTime.Now;
            //                            payment.PosId = terminalId; ;
            //                            payment.CashierId = currentUserId;
            //                            payment.BranchId = branchId;
            //                            payment.ModifiedBy = currentUserId;
            //                            paymentId = billDA.InsertPayment(trans, payment, postServerId);
            //                            if (paymentId == String.Empty)
            //                            {
            //                                trans.Rollback();
            //                                throw new Exception("PEA.BPM.AgencyManagementModule.BS.BillbookCheckInService.CreateBillBookCheckIn: paymentId is Empty.");
            //                                //return false;
            //                            }
            //                        }
            //                        //Create ARPaymentType
            //                        string arptId = String.Empty;
            //                        ARPaymentTypeInfo payType = new ARPaymentTypeInfo();
            //                        payType.PaymentId = paymentId;
            //                        payType.Amount = c.ChequeAmount;
            //                        payType.PtId = ((int)ARPaymentTypeEnum.CHEQUE).ToString();
            //                        payType.Description = "From Agecy";
            //                        payType.Amount = c.ChequeAmount;
            //                        payType.BankKey = c.BankKey;
            //                        payType.ChqNo = c.ChequeNo;
            //                        payType.ActualAmount = c.ActualAmount;
            //                        payType.ChqAccNo = c.ChequeAccountNo;
            //                        payType.ChqDt = c.ChequeDt;
            //                        payType.TranfAccNo = String.Empty;
            //                        payType.TranfDt = null;
            //                        payType.ModifiedBy = currentUserId;
            //                        payType.ModifiedDt = Session.BpmDateTime.Now;
            //                        arptId = billDA.InsertARPaymentType(trans, payType, b.InvoiceNo, b.Period, branchId, postServerId, terminalId);
            //                        if (arptId == String.Empty)
            //                        {
            //                            trans.Rollback();
            //                            throw new Exception("PEA.BPM.AgencyManagementModule.BS.BillbookCheckInService.CreateBillBookCheckIn: arptId is Empty.");
            //                            //return false;
            //                        }
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                // previous checkin but now un check -> remove it
            //                if (b.IsCheckIn || InvoiceCounter == billBookCheckIn.BillBookCheckInDetail.Count)
            //                {
            //                    if (b.IsCheckIn)
            //                    {
            //                        if (billBookCheckIn.BookType == (int)BookTypeEnum.BILLBOOK)
            //                            itemCount = billDA.InsertARPayment(trans, b.BookId, b.InvoiceNo, billPeriod, currentUserId, b.PmId, branchId, -1, postServerId, b.TotalAmount, terminalId);
            //                        else
            //                            itemCount = billDA.InsertGroupInvoiceARPayment(trans, b.PaidAmount, outputNo, b.InvoiceNo, billPeriod, currentUserId, b.PmId, branchId, -1, postServerId, InvoiceCounter == billBookCheckIn.BillBookCheckInDetail.Count ? 1 : 0, terminalId);
            //                    }
            //                    else
            //                    {
            //                        if (billBookCheckIn.BookType == (int)BookTypeEnum.GROUP_INVOICE)
            //                        {
            //                            itemCount = billDA.InsertGroupInvoiceARPayment(trans, b.PaidAmount, outputNo, b.InvoiceNo, billPeriod, currentUserId, b.PmId, branchId, -1, postServerId, InvoiceCounter == billBookCheckIn.BillBookCheckInDetail.Count ? 1 : 0, terminalId);

            //                        }
            //                    }
            //                }
            //            }
            //        }

            //        string caId = String.Empty;
            //        if (billBookCheckIn.BookType == (int)BookTypeEnum.BILLBOOK)
            //        {
            //            //update agencyData
            //            foreach (BillBookCheckinDetailInfo b in billBookCheckIn.BillBookCheckInDetail)
            //            {
            //                string allowedReat = String.Empty;
            //                string absId = String.Empty;
            //                string pmId = b.PmId == PmIdEnum.NONE ? null : b.PmId;
            //                string ivId = b.InvoiceNo;
            //                string billId = billBookCheckIn.BookId;
            //                string inBook = b.InBook;
            //                decimal? paidAmount = b.PaidAmount;
            //                DateTime? laspPaidDt = b.LastPaidDt;
            //                int paidType = (int)b.PaidType;

            //                absId = b.AbsId;
            //                if (b.AboId == AboIdEnum.FIRST) // ออกเก็บครั้งแรก
            //                {
            //                    allowedReat = "Y"; // สามารถออกเก็บได้อีกครั้ง
            //                }
            //                else
            //                {
            //                    allowedReat = "N"; // ไม่สามารถออกเก็บได้อีก
            //                }                            

            //                if (b.AbsId == AbsIdEnum.UNCOLLECTED) // วางบิลไม่ได้
            //                {
            //                    inBook = "N"; //สามารถออกบิลได้อีกครั้ง                                
            //                    b.AboId = AboIdEnum.REPEAT; // จัดเก็บไม่ได้ถือเป็นบิลทวน
            //                }
            //                else if (b.AbsId == AbsIdEnum.COLLECTED) // เก็บได้ Update paidamount = gamount
            //                {
            //                    paidAmount = b.PaidAmount + b.TotalAmount;
            //                    laspPaidDt = Session.BpmDateTime.Now;
            //                }
            //                itemCount = billDA.UpdateAgencyBillBookStatus(trans, currentUserId, billId,
            //                                b.AboId, allowedReat, absId, ivId, inBook, paidAmount, laspPaidDt, Session.BpmDateTime.Now, paidType, postServerId);
            //                if (itemCount <= 0)
            //                {
            //                    trans.Rollback();
            //                    throw new Exception("PEA.BPM.AgencyManagementModule.BS.BillbookCheckInService.CreateBillBookCheckIn:Count of update line in ag_upd_AgencyBillBookStatus is less than or equal to zero. ");
            //                    //return false;
            //                }
            //            }
            //        }

            //        // Update total collect item incase book is billbook
            //        if (billBookCheckIn.BookType == (int)BookTypeEnum.BILLBOOK)
            //        {
            //            itemCount = billDA.UpdateTotalCollected(trans, billBookCheckIn.BookId, currentUserId);
            //            if (itemCount <= 0)
            //            {
            //                trans.Rollback();
            //                throw new Exception("PEA.BPM.AgencyManagementModule.BS.BillbookCheckInService.CreateBillBookCheckIn:Count of update line in ag_upd_SumBillBookCheckIn is less than or equal to zero. ");
            //                //return false;
            //            }
            //        }
            //        //Update checkin date in mruplan
            //        if (billBookCheckIn.BookType == (int)BookTypeEnum.BILLBOOK)
            //        {
            //            AgencyDataAccess agentDa = new AgencyDataAccess();
            //            DateTime currDate = Session.BpmDateTime.Now;
            //            agentDa.UpdateMRUPlanCheckInBillBook(trans, currDate, billBookCheckIn.BookId);
            //        }

            //        trans.Commit();

            //    }
            //    catch (Exception ex)
            //    {
            //        trans.Rollback();
            //        throw ex;
            //    }
            //    return true;
            //}
            #endregion
        }

        public bool CancelBillBookCheckIn(BillBookCheckInInfo billBookCheckIn)
        {
            // check biz rules
            string currentUserId = Session.User.Id; ;
            BillBookDataAccess billDA = new BillBookDataAccess();
            CommissionDataAccess commDA = new CommissionDataAccess();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    //cancel item only collected and past 
                    List<BillBookCheckinDetailInfo> validList = new List<BillBookCheckinDetailInfo>();
                    foreach (BillBookCheckinDetailInfo b in billBookCheckIn.BillBookCheckInDetail)
                    {
                        if (b.AbsId == AbsIdEnum.PAST || b.AbsId == AbsIdEnum.COLLECTED || b.AbsId == AbsIdEnum.UNCOLLECTED)
                            validList.Add(b);
                    }

                    billBookCheckIn.BillBookCheckInDetail = validList;
                    billDA.DeleteBillBookCheckIn(trans, billBookCheckIn, currentUserId);
                    trans.Commit();

                   
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }

                //try
                //{
                   ///ENQUEUE INTERFACE BILLBOOK EXPORT
                   ///--NO USE FOR NOW!!
                //   new BillBookDataAccess().EnqueueBatchJobBillBook(
                //   PEA.BPM.Infrastructure.Interface.Constants.LocalSettingNames.DL010_EXPORT_BILLBOOK_INVOICE_BATCH
                //    , new string[] { billBookCheckIn.BookId });
                //}
                //catch (Exception)
                //{ //Silent
                //}

                return true;
            }

           
        }
        public bool CheckIsFullyPaid(BillBookCheckInInfo biilBookCheckIn)
        {
            BillBookDataAccess billDA = new BillBookDataAccess();
            try
            {
                Boolean retVal = billDA.CheckIsFullyPaid(biilBookCheckIn);
                return retVal;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool CheckIsSubmitGroupSameDay(BillBookCheckInInfo biilBookCheckIn)
        {
            BillBookDataAccess billDA = new BillBookDataAccess();
            try
            {
                Boolean retVal = billDA.CheckIsSubmitGroupSameDay(biilBookCheckIn);
                return retVal;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public BillBookCheckInInfo GetBookCheckInHistory(string billBookId)
        {
            BillBookCheckInInfo book = new BillBookCheckInInfo();
            book = GetBillBookCheckInHistory(billBookId);
            //if (book.BookId == null)
            //    book = GetGroupInvoiceCheckInHistory(billBookId);
            return book;
        }

        public void BillBookSaveState(BillBookCheckInInfo billBookCheckIn, string modifiedBy)
        {
            BillBookDataAccess billDA = new BillBookDataAccess();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (BillBookCheckinDetailInfo b in billBookCheckIn.BillBookCheckInDetail)
                    {
                        billDA.BillBookSaveState(trans, billBookCheckIn.BookId, b.InvoiceNo, b.AbsId != "Y" ? "N" : b.AbsId, modifiedBy);
                    }
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }
        }
    }

    
}

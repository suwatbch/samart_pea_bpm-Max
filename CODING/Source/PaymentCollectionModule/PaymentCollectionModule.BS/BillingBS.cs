using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using System.Threading;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PEA.BPM.PaymentCollectionModule.DA;
using System.Data.SqlClient;
using System.Data;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports;
using System.Configuration;
using System.Globalization;
using System.Collections;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using System.Linq;
using AgencyDA=PEA.BPM.AgencyManagementModule.DA;
using AgencyEntities = PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;


namespace PEA.BPM.PaymentCollectionModule.BS
{
    public class BillingBS: IBillingService
    {
        #region IBillingService Members

        public List<Invoice> SearchInvoiceByCustomerId(CustomerSearchParam param)
        {
            try
            {
                BillingDA da = new BillingDA();
                List<Invoice> invoices = da.SearchInvoiceByCustomerId(param.CaId, param.IsSearByBP);

                if (invoices == null) // 20180125 Kanokwan.L แก้ไข Invoiceno = null แล้วระบบแสดง Alert เตือนหน้าบ้านไม่สื่อความหมาย 
                {
                    throw new Exception("ไม่สามารถรับชำระเงินได้เนื่องจาก มีรายการหนี้คงค้างที่ InvoiceNo เป็นค่าว่าง");
                }
                else if (invoices.Count > 0)
                {
                    // 201806281022 Defect#109 แก้ไขการคำนวณดอกเบี้ยแผนผ่อน จะต้องคำนวณดอกเบี้ยแตกรายเดือน ไม่ต้อง Group ค่ารวมกัน
                    //if (invoices[0].Bills[0].ShortDebtId == "M0080")
                    //{
                    //    List<Invoice> interestInvoices = CheckInterestByInvoice(invoices,
                    //          param.ToPayDate == null ? Session.BpmDateTime.Now.Date : param.ToPayDate.Value, false);
                    //    //TODO: INSTALLMENT CASE
                    //    //List<Invoice> interestInvoices = CheckInterestByInvoice(invoices,
                    //    //    param.ToPayDate == null ? Session.BpmDateTime.Now.Date : param.ToPayDate.Value, true);


                    //    List<Invoice> invoiceNo = new List<Invoice>();
                    //    invoiceNo = interestInvoices.FindAll(delegate(Invoice i)
                    //        {
                    //            return i.Bills[0].DebtId == CodeNames.DebtType.Interest.Id;
                    //        });

                    //    if (invoiceNo.Count > 1)
                    //    {
                    //        invoiceNo.RemoveRange(0, invoiceNo.Count - 1);
                    //    }

                    //    foreach (Invoice inv in invoiceNo)
                    //    {
                    //        invoices.Add(inv);
                    //    }
                    //}
                    //else
                    //{
                        invoices = CheckInterestByInvoice(invoices,
                            param.ToPayDate == null ? Session.BpmDateTime.Now.Date : param.ToPayDate.Value, true);
                    //}
                }

                return invoices;

            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "SearchInvoiceByCaId", ex.ToString());
                throw;
            }
        }

        public List<Invoice> SearchInvoiceFromSAP(SAPSearchParam param)
        {
            try
            {   
                /// Load data from SAP & Save to BPM Server
                ArrayList list = GetDataFromSAP(param);
                SaveInvoiceFromSAP(list, Session.Branch.PostedServerId);
                
                string caId = param.CaId;
                if (caId == null)
                {
                    List<ContractAccountInfo> cai = (List<ContractAccountInfo>)list[1];
                    if (cai.Count > 0)
                    {
                        caId = cai[0].CaId;
                    }
                }

                List<Invoice> invoice = SearchInvoiceByCustomerId(new CustomerSearchParam(caId));
                
                return invoice;
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "SearchInvoiceFromSAP", ex.ToString());
                throw;
            }
        }

        //---Real Time POS--Start------------------------------------------------------------------------------------------
        public ActivePayment SearchInvoiceFromSAPForRealTime(SAPSearchParam param)
        {
            try
            {
                /// Load data from SAP & Save to BPM Server
                ArrayList list = GetDataFromSAP(param);
                SaveInvoiceFromSAP(list, Session.Branch.PostedServerId);

                string caId = param.CaId;
                if (caId == null)
                {
                    List<ContractAccountInfo> cai = (List<ContractAccountInfo>)list[1];
                    if (cai.Count > 0)
                    {
                        caId = cai[0].CaId;
                    }
                }

                return GetActivePayment(new SAPSearchParam(caId,"","",""), false);
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "SearchInvoiceFromSAP", ex.ToString());
                throw;
            }
        }

        //---Real Time POS--End------------------------------------------------------------------------------------------

        private List<ARPaymentInfo> GetARPaymentInfo(List<ARInfo> arList)
        {
            try
            {
                BillingDA da = new BillingDA();
                return da.GetARPaymentInfo(arList);
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "GetARPaymentInfo", ex.ToString());
                throw;
            }
        }

        private List<RTARPaymentInfo> GetRTARPaymentInfo(List<ARPaymentInfo> arPmList)
        {
            try
            {
                BillingDA da = new BillingDA();
                return da.GetRTARPaymentInfo(arPmList);
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "GetRTARPaymentInfo", ex.ToString());
                throw;
            }
        }

        private List<ARPaymentTypeInfo> GetPaymentTypeInfo(List<RTARPaymentInfo> rtARPaymentList)
        {
            try
            {
                BillingDA da = new BillingDA();
                return da.GetPaymentTypeInfo(rtARPaymentList);
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "GetPaymentTypeInfo", ex.ToString());
                throw;
            }
        }

        private List<PaymentInfo> GetPaymentInfo(List<ARPaymentTypeInfo> arPtList)
        {
            try
            {
                BillingDA da = new BillingDA();
                return da.GetPaymentInfo(arPtList);
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "GetPaymentInfo", ex.ToString());
                throw;
            }
        }

        private List<ReceiptInfo> GetReceiptInfo(List<PaymentInfo> paymentList)
        {
            try
            {
                BillingDA da = new BillingDA();
                return da.GetReceiptInfo(paymentList);
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "GetReceiptInfo", ex.ToString());
                throw;
            }
        }

        private List<ReceiptItemInfo> GetReceiptItemInfo(List<ReceiptInfo> receiptList)
        {
            try
            {
                BillingDA da = new BillingDA();
                return da.GetReceiptItemInfo(receiptList);
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "GetReceiptItemInfo", ex.ToString());
                throw;
            }
        }

        private ArrayList GetDataFromDb(SAPSearchParam param)
        {
            try
            {
                BillingDA da = new BillingDA();
                return da.GetDataFromDb(param);
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "GetInvoiceFromDatabase", ex.ToString());
                throw;
            }
        }

        //Added June, 9 09
        //branch server only, allowed only caId
        public void SaveActivePayment(ActivePayment acp)
        {
            //To do: put compare debt here
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbTransaction trans;
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                try
                {
                    BillingDA da = new BillingDA();
                    //da.InsertIntoBusinessPartner((List<BusinessPartnerInfo>)list[0], trans);
                    //da.InsertIntoContractAccount((List<ContractAccountInfo>)list[1], trans);
                    //da.InsertIntoAR((List<ARInfo>)list[2], trans, postedServerId);
                    //da.InsertIntoPayFromSAP((List<PayFromSAPInfo>)list[3], trans, postedServerId);
                    //da.InsertIntoDisconnectionDoc((List<DisconnectionDoc>)list[4], trans);
                    //da.InsertIntoRTDisconnectionDocCaDoc(acp.ar, trans);
                    da.InsertIntoBusinessPartnerForRealTime(acp.BP, trans);
                    da.InsertIntoContractAccountForRealTime(acp.CA, trans);
                    da.InsertIntoARForRealTime(acp.AR, trans);
                    da.InsertIntoARPaymentForRealTime(acp.ARPayment, trans);
                    da.InsertIntoRTARPaymentForRealTime(acp.RTARPayment, trans);
                    da.InsertIntoARPaymenttypeForRealTime(acp.PaymentType, trans);
                    da.InsertIntoPaymentForRealTime(acp.Payment, trans);
                    da.InsertIntoReceiptItemForRealTime(acp.ReceiptItem, trans);
                    da.InsertIntoReceiptForRealTime(acp.Receipt, trans);
                    da.InsertIntoDisconnectionDocForRealTime(acp.DisDoc, trans);
                    da.InsertIntoRTDisconnectionDocCaDocForRealTime(acp.RTDisDoc, trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Logger.WriteError(Logger.Module.POS, "SaveInvoiceFromSAP(SAP Connector)", ex.ToString());
                    throw;
                }
            }

        }

        //Added June, 8 09
        //centre server only, allowed only caId
        public ActivePayment GetActivePayment(SAPSearchParam param, bool renew)
        {
            try
            {
                ActivePayment acp = new ActivePayment();
                if (renew)
                {
                    /// Load data from SAP & Save to BPM Server
                    ArrayList list = GetDataFromSAP(param);

                    //save data in center database
                    SaveInvoiceFromSAP(list, Session.Branch.PostedServerId);                    
                }
                
                ArrayList arrList = GetDataFromDb(param);
                //save to entity
                acp.BP = (List<BusinessPartnerInfo>)arrList[0];
                acp.CA = (List<ContractAccountInfo>)arrList[1];
                acp.AR = (List<ARInfo>)arrList[2];
                acp.DisDoc = (List<DisconnectionDoc>)arrList[3];
                acp.RTDisDoc = (List<RTDisconnectionDocCaDoc>)arrList[4];

                //Get payment from AR(s)
                acp.ARPayment = GetARPaymentInfo(acp.AR);
                acp.RTARPayment = GetRTARPaymentInfo(acp.ARPayment);
                acp.PaymentType = GetPaymentTypeInfo(acp.RTARPayment);
                acp.Payment = GetPaymentInfo(acp.PaymentType);
                acp.Receipt = GetReceiptInfo(acp.Payment);
                acp.ReceiptItem = GetReceiptItemInfo(acp.Receipt);

                return acp;
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "GetActivePayment", ex.ToString());
                throw;
            }
        }

        public Invoice SearchOriginalInvoiceByInstallmentItemCaDoc(OriginalInvoiceSearchParam param)
        {
            try
            {
                BillingDA da = new BillingDA();
                return da.SearchOriginalInvoiceByInstallmentItemCaDoc(param.CaDocNo);
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "SearchInvoiceByCaDocNo", ex.ToString());
                throw;
            }
        }

        public List<InstallmentInvoice> SearchInstallmentInvoice(string caDoc)
        {
            try
            {
                BillingDA da = new BillingDA();
                return da.SearchInstallmentInvoice(caDoc);
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "SearchInstallmentInvoice", ex.ToString());
                throw;
            }
        }

        public List<Invoice> SearchInvoiceByGroupInvoiceNo(GroupInvoiceSearchParam param)
        {
            try
            {
                BillingDA da = new BillingDA();
                List<Invoice> invoices = da.SearchInvoiceByGroupInvoiceNo(param.InvoiceNo, param.BranchId);

                if (invoices.Count > 0)
                {
                    if (invoices[0].Bills[0].ShortDebtId == "M0080")
                    {
                        List<Invoice> interestInvoices = CheckInterestByInvoice(invoices,
                            param.ToPayDate == null ? Session.BpmDateTime.Now.Date : param.ToPayDate.Value, false);


                        List<Invoice> invoiceNo = new List<Invoice>();
                        invoiceNo = interestInvoices.FindAll(delegate(Invoice i)
                            {
                                return i.Bills[0].DebtId == CodeNames.DebtType.Interest.GroupInvoice.Id;
                            });

                        if (invoiceNo.Count > 1)
                        {
                            invoiceNo.RemoveRange(0, invoiceNo.Count - 1);
                        }

                        foreach (Invoice inv in invoiceNo)
                        {
                            invoices.Add(inv);
                        }
                    }
                    else
                    {
                        invoices = CheckInterestByInvoice(invoices,
                            param.ToPayDate == null ? Session.BpmDateTime.Now.Date : param.ToPayDate.Value, true);
                    }
                }

                return invoices;

            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "SearchInvoiceByCaId", ex.ToString());
                throw;
            }
        }

        public List<Invoice> SearchInvoiceItemByGroupInvoiceNo(InvoiceItemSearchParam param)
        {
            try
            {
                BillingDA da = new BillingDA();
                List<Invoice> invoices = da.SearchInvoiceItemByGroupInvoiceNo(param.GroupInvoiceNo);

                return invoices;
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "SearchInvoiceByCaId", ex.ToString());
                throw;
            }
        }


        private List<Invoice> CheckInterestByInvoice(List<Invoice> invoices, DateTime toPayDate, bool isAdded)
        {
            List<Invoice> paidInvoices = new List<Invoice>();

            DateTime today = toPayDate; //Session.BpmDateTime.Now.Date;
            foreach (Invoice inv in invoices)
            {
                if (isAdded)
                {
                    paidInvoices.Add(inv);
                }

                string debitNoteDebt = "M00100010,M00100090";

                if (((inv != null && inv.PmId == CodeNames.PaymentMethod.EPayment.Id)
                    //|| (inv.Bills.Count > 0 && inv.Bills[0].GroupInvoiceId != null && inv.Bills[0].IsElectricDebt()) //2018002081423 Kanokwan.L แก้ไขการคิดดอกเบี้ยไมให้คิดกรณีค้นหาหนี้ด้วย Group
                    || (inv.Bills.Count > 0 && inv.Bills[0].GroupInvoiceId != null && inv.Bills[0].PaymentMethodId==CodeNames.PaymentMethod.GroupInvoicing.Id)
                    || (inv.Bills.Count > 0 && debitNoteDebt.Contains(inv.Bills[0].DebtId))) == false)
                {
                    decimal toCalInterestAmount = 0;
                    bool isInstallment = inv.InstallmentPeriod != null;

                    DateTime toCalInterestDate = DateTime.Now;

                    //Check for adding interest
                    foreach (Bill bill in inv.Bills)
                    {
                        Decimal? interestRate = bill.InterestRate;

                        if (interestRate != null && interestRate > 0)
                        {
                            DateTime? dueDate = bill.DueDate;
                            DateTime? deferralDate = bill.DeferralDate;
                            DateTime? originalDueDate = bill.OriginalDueDate;
                            decimal? amount = bill.TaxCode != null ? (bill.TaxCode[0].ToString() == "O" ? bill.GAmount.Value : bill.AmountExVat.Value) : (bill.VatAmount == null ? bill.GAmount.Value : bill.GAmount.Value - bill.VatAmount.Value);

                            string interlockFlag = bill.InterestLockFlag;

                            if (interlockFlag == null)
                            {
                                if (!isInstallment && dueDate < today)
                                {
                                   
                                    if (inv.InvoiceNo.Length == 22 && inv.Bills[0].GroupInvoiceId != null)
                                    {
                                        toCalInterestDate = dueDate.Value;
                                        toCalInterestAmount = 0;
                                    }
                                    else
                                    {
                                        toCalInterestDate = dueDate.Value;
                                        toCalInterestAmount += amount.Value;
                                    }
                                    
                                }
                                else if (isInstallment && originalDueDate < today)
                                {
                                    toCalInterestDate = originalDueDate.Value;
                                    toCalInterestAmount += bill.LeftInstallmentAmount.Value;
                                }
                            }
                            else if (interlockFlag == "X")
                            {
                                if (!isInstallment && deferralDate < today)
                                {
                                    toCalInterestDate = deferralDate.Value;
                                    toCalInterestAmount += amount.Value;
                                }
                                else if (isInstallment && dueDate < today)
                                {
                                    toCalInterestDate = dueDate.Value;
                                    toCalInterestAmount += amount.Value;
                                }
                            }
                        }
                    }

                    if (toCalInterestAmount > 0 || (inv.InvoiceNo.Length == 22 && inv.Bills[0].GroupInvoiceId != null)) // เพิ่มเติมกรณี GroupInvoice
                    {
                        CalendarBS calendarBS = new CalendarBS(Session.Branch.Id);
                        Bill bill = inv.Bills[0];
                        DateTime stDate = toCalInterestDate;
                        stDate = calendarBS.GetFirstWorkingDay(stDate);
                        TimeSpan timespan = toPayDate - stDate;
                        int totalDays = (int)timespan.TotalDays;
                        
                        //-----เพิ่มใหม--------------
                        int totalCalculateDays = 0;
                        int beginYear = stDate.Year;
                        int endYear = toPayDate.Year;
                        //-------------------------

                        if (totalDays > 0)
                        {
                            Bill b = new Bill();
                            b.CustomerId = bill.CustomerId;
                            b.Name = bill.Name;
                            b.Address = bill.Address;
                            b.BranchId = inv.BranchId;
                            b.ContractTypeId = bill.ContractTypeId;

                            if (inv.Bills[0].DebtId == CodeNames.DebtType.AgencyGroupInvoicing.Id)
                            {
                                b.DebtId = CodeNames.DebtType.Interest.GroupInvoice.Id;
                                b.DebtType = CodeNames.DebtType.Interest.GroupInvoice.Name;
                            }
                            else
                            {
                                b.DebtId = CodeNames.DebtType.Interest.Id;
                                b.DebtType = CodeNames.DebtType.Interest.Name;
                            }

                            b.Period = bill.Period;
                            b.Description = string.Format("วันที่ {0} ถึง {1} จำนวน {2} วัน",
                                stDate.AddDays(+1).ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                toPayDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                totalDays);

                            //decimal interest = toCalInterestAmount
                            //    * (Convert.ToDecimal(totalDays) / Convert.ToDecimal(365))
                            //    * (bill.InterestRate.Value / Convert.ToDecimal(100));
                            //interest = Decimal.Round(interest, 2);
                            //b.InterestRate = bill.InterestRate;
                            //b.Qty = totalDays;
                            //b.FullQty = totalDays;


                            //-----เพิ่มใหม--------------
                            decimal interest = 0;
                            int leapDays = 0;
                         
                            if (inv.InvoiceNo.Length == 22 && inv.Bills[0].GroupInvoiceId != null)
                            {
                                // GroupInvoice
                                decimal calInterestByInvoice = 0;
                                AgencyEntities.BillBookCheckInInfo _retVal = new AgencyEntities.BillBookCheckInInfo();
                                AgencyDA.BillBookDataAccess billDA = new AgencyDA.BillBookDataAccess();
                                _retVal = billDA.GetGroupInvoiceCheckInInfomation(inv.Bills[0].GroupInvoiceId, null, false);

                                List<AgencyEntities.BillBookCheckinDetailInfo> oBillbookCheckinDetailInfo = new List<AgencyEntities.BillBookCheckinDetailInfo>();
                                oBillbookCheckinDetailInfo = _retVal.BillBookCheckInDetail;
                                Decimal totalAmountGroupByInvoice = 0;
                                foreach (AgencyEntities.BillBookCheckinDetailInfo obb in oBillbookCheckinDetailInfo)
                                {
                                    totalAmountGroupByInvoice =Convert.ToDecimal(obb.TotalAmount - obb.Vat);

                                    if (toPayDate.Year - stDate.Year == 0)
                                    {
                                        leapDays = DateTime.IsLeapYear(toPayDate.Year) == true ? 366 : 365;

                                        calInterestByInvoice = totalAmountGroupByInvoice
                                            * (Convert.ToDecimal(totalDays) / Convert.ToDecimal(leapDays))
                                            * (bill.InterestRate.Value / Convert.ToDecimal(100));
                                        calInterestByInvoice = Decimal.Round(calInterestByInvoice, 2);

                                        totalCalculateDays = totalDays;
                                        interest += calInterestByInvoice;
                                    }
                                    else
                                    {
                                        int tmpYear = beginYear;
                                        while (tmpYear <= endYear)
                                        {
                                            decimal tmpInterest = 0;
                                            leapDays = DateTime.IsLeapYear(tmpYear) == true ? 366 : 365;
                                            if (tmpYear < endYear && tmpYear == beginYear)
                                            {
                                                totalDays = leapDays - stDate.DayOfYear;
                                            }
                                            else if (tmpYear < endYear && tmpYear > beginYear)
                                            {
                                                totalDays = leapDays;
                                            }
                                            else if (tmpYear == endYear)
                                            {
                                                totalDays = toPayDate.DayOfYear;
                                            }


                                            tmpInterest = totalAmountGroupByInvoice
                                               * (Convert.ToDecimal(totalDays) / Convert.ToDecimal(leapDays))
                                               * (bill.InterestRate.Value / Convert.ToDecimal(100));
                                            tmpInterest = Decimal.Round(tmpInterest, 2);
                                            
                                            interest += tmpInterest;
                                            
                                            if (oBillbookCheckinDetailInfo.First() == obb)
                                            {   
                                                totalCalculateDays += totalDays;
                                            }
                                            tmpYear++;
                                        }
                                        
                                    }
                                    
                                }
                            }
                            else
                            {
                                // !=GroupInvoice
                                if (toPayDate.Year - stDate.Year == 0)
                                {
                                    leapDays = DateTime.IsLeapYear(toPayDate.Year) == true ? 366 : 365;

                                    interest = toCalInterestAmount
                                        * (Convert.ToDecimal(totalDays) / Convert.ToDecimal(leapDays))
                                        * (bill.InterestRate.Value / Convert.ToDecimal(100));
                                    interest = Decimal.Round(interest, 2);

                                    totalCalculateDays = totalDays;
                                }
                                else
                                {
                                    int tmpYear = beginYear;
                                    while (tmpYear <= endYear)
                                    {
                                        decimal tmpInterest = 0;
                                        leapDays = DateTime.IsLeapYear(tmpYear) == true ? 366 : 365;
                                        if (tmpYear < endYear && tmpYear == beginYear)
                                        {
                                            totalDays = leapDays - stDate.DayOfYear;
                                        }
                                        else if (tmpYear < endYear && tmpYear > beginYear)
                                        {
                                            totalDays = leapDays;
                                        }
                                        else if (tmpYear == endYear)
                                        {
                                            totalDays = toPayDate.DayOfYear;
                                        }


                                        tmpInterest = toCalInterestAmount
                                           * (Convert.ToDecimal(totalDays) / Convert.ToDecimal(leapDays))
                                           * (bill.InterestRate.Value / Convert.ToDecimal(100));
                                        tmpInterest = Decimal.Round(tmpInterest, 2);

                                        interest += tmpInterest;
                                        totalCalculateDays += totalDays;
                                        tmpYear++;
                                    }
                                }
                            }
                          
                         


                            b.InterestRate = bill.InterestRate;
                            b.Qty = totalCalculateDays;
                            b.FullQty = totalCalculateDays;
                            //-------------------------


                            b.UnitTypeId = CodeNames.UnitType.Day.Id;
                            b.UnitTypeName = CodeNames.UnitType.Day.Name;
                            b.AmountExVat = interest;
                            b.FullAmount = interest;
                            b.TaxCode = CodeNames.TaxCode.NoTaxCharge.TaxCode;
                            b.TaxRate = StringConvert.ToDecimal(CodeNames.TaxCode.NoTaxCharge.TaxRate);
                            b.GAmount = b.AmountExVat;
                            b.FullGAmount = b.AmountExVat;
                            b.ControllerId = bill.ControllerId;
                            b.DataState = BillDataStage.NewItem;

                            Invoice i = new Invoice();
                            i.InvoiceNo = null;
                            if (inv.InvoiceNo.Length == 22 && inv.Bills[0].GroupInvoiceId != null) // กรณีดอกเบี้ยการชำระแบบกลุ่ม
                            {
                                i.OriginalInvoiceNo = inv.Bills[0].GroupInvoiceId; // แก้ ดอกเบี้ยกลุ่ม
                                //i.OriginalInvoiceNo = inv.InvoiceNo; // แก้ ดอกเบี้ยกลุ่ม
                            }
                            else if (isInstallment && inv.Bills[0].InterestLockFlag == null) // กรณีดอกเบี้ยแผนผ่อน InterestLockFlag=null DF#109 201807191013 Kanokwan.L คิดดอกเบี้ยที่แม่ ต้องให้แสดงรายการดอกเบี้ยแค่รายการเดียว
                            {
                                i.OriginalInvoiceNo = inv.OriginalInvoiceNo;
                            }
                            else
                            {
                                i.OriginalInvoiceNo = inv.InvoiceNo;
                            }
                            i.BranchId = b.BranchId;
                            i.TechBranchName = inv.TechBranchName;
                            i.CommBranchId = inv.CommBranchId;
                            i.CommBranchName = inv.CommBranchName;
                            i.CaId = b.CustomerId;
                            i.Name = b.Name;
                            i.Address = b.Address;
                            i.ControllerId = inv.ControllerId;
                            i.ControllerName = inv.ControllerName;
                            i.MruId = inv.MruId;
                            i.GAmount = b.GAmount;
                            i.PaidVatAmount = 0;
                            i.PaidGAmount = 0;
                            i.ToPayQty = b.Qty;
                            i.ToPayGAmount = i.ToBePaidGAmount;
                            i.ToPayVatAmount = i.ToBePaidVatAmount;
                            i.CaDoc = null;
                            i.DataState = InvoiceDataStage.NewItem;
                            i.Bills = new List<Bill>();
                            i.Bills.Add(b);

                            if (Math.Round((decimal)b.GAmount, 2) != StringConvert.ToDecimal("0.00")) //201802271128 Kanokwan.L Defect#58 แก้ไขการคำนวณดอกเบี้ย 0.00 ไม่ให้แสดงบนหน้าจอ
                            paidInvoices.Add(i);
                            
                        }
                    }
                }


                 ////แผนผ่อน กรณีไม่ได้ mark X ต้องให้เหลือดอกเบี้ยตามรายการของแผนภายใต้ CA ที่ค้นหา
                 //if ()


            }


                    paidInvoices = paidInvoices.GroupBy(n => new { n.CaId,n.InvoiceNo,n.OriginalInvoiceNo, n.DebtType, n.DueDate, n.GAmount }).Select(n => n.FirstOrDefault()).ToList();
                    //201802271128 Kanokwan.L Defect#96 แก้ไขการคำนวณดอกเบี้ยไม่แสดง
            
 


            return paidInvoices;
        }


        //TODO: INSTALLMENT CASE
        //private List<Invoice> CheckInterestByInvoice(List<Invoice> invoices, DateTime toPayDate, bool isAdded)
        //{
        //    List<Invoice> paidInvoices = new List<Invoice>();

        //    DateTime today = Session.BpmDateTime.Now.Date;
        //    foreach (Invoice inv in invoices)
        //    {
        //        if (isAdded)
        //        {
        //            paidInvoices.Add(inv);
        //        }


        //        if (inv.Bills[0].ShortDebtId == "M0080" && paidInvoices.FindAll(delegate(Invoice i)
        //            { return i.Bills[0].DebtId == CodeNames.DebtType.Interest.Id && i.OriginalInvoiceNo == inv.OriginalInvoiceNo; }).Count > 0)
        //        {
        //            //Do Nothing
        //        }
        //        else
        //        {
        //            string debitNoteDebt = "M00100010,M00100090";

        //            if (((inv != null && inv.PmId == CodeNames.PaymentMethod.EPayment.Id)
        //                || (inv.Bills.Count > 0 && inv.Bills[0].GroupInvoiceId != null && inv.Bills[0].IsElectricDebt())
        //                || (inv.Bills.Count > 0 && debitNoteDebt.Contains(inv.Bills[0].DebtId))) == false)
        //            {
        //                decimal toCalInterestAmount = 0;
        //                bool isInstallment = inv.InstallmentPeriod != null;

        //                DateTime toCalInterestDate = DateTime.Now;

        //                //Check for adding interest
        //                foreach (Bill bill in inv.Bills)
        //                {
        //                    Decimal? interestRate = bill.InterestRate;

        //                    if (interestRate != null && interestRate > 0)
        //                    {
        //                        DateTime? dueDate = bill.DueDate;
        //                        DateTime? deferralDate = bill.DeferralDate;
        //                        DateTime? originalDueDate = bill.OriginalDueDate;
        //                        decimal? amount = bill.TaxCode != null ? (bill.TaxCode[0].ToString() == "O" ? bill.GAmount.Value : bill.AmountExVat.Value) : (bill.VatAmount == null ? bill.GAmount.Value : bill.GAmount.Value - bill.VatAmount.Value);

        //                        string interlockFlag = bill.InterestLockFlag;

        //                        if (interlockFlag == null)
        //                        {
        //                            if (!isInstallment && dueDate < today)
        //                            {
        //                                toCalInterestDate = dueDate.Value;
        //                                toCalInterestAmount += amount.Value;
        //                            }
        //                            else if (isInstallment && originalDueDate < today)
        //                            {
        //                                toCalInterestDate = originalDueDate.Value;
        //                                toCalInterestAmount += bill.LeftInstallmentAmount.Value;
        //                            }
        //                        }
        //                    }
        //                }

        //                if (toCalInterestAmount > 0)
        //                {
        //                    CalendarBS calendarBS = new CalendarBS(Session.Branch.Id);

        //                    Bill bill = inv.Bills[0];

        //                    DateTime stDate = toCalInterestDate;
        //                    stDate = calendarBS.GetFirstWorkingDay(stDate);
        //                    TimeSpan timespan = toPayDate - stDate;
        //                    int totalDays = (int)timespan.TotalDays;
        //                    int totalCalculateDays = 0;
        //                    int beginYear = stDate.Year;
        //                    int endYear = toPayDate.Year;

        //                    if (totalDays > 0)
        //                    {
        //                        Bill b = new Bill();

        //                        b.CustomerId = bill.CustomerId;
        //                        b.Name = bill.Name;
        //                        b.Address = bill.Address;
        //                        b.BranchId = inv.BranchId;
        //                        b.ContractTypeId = bill.ContractTypeId;

        //                        if (inv.Bills[0].DebtId == CodeNames.DebtType.AgencyGroupInvoicing.Id)
        //                        {
        //                            b.DebtId = CodeNames.DebtType.Interest.GroupInvoice.Id;
        //                            b.DebtType = CodeNames.DebtType.Interest.GroupInvoice.Name;
        //                        }
        //                        else
        //                        {
        //                            b.DebtId = CodeNames.DebtType.Interest.Id;
        //                            b.DebtType = CodeNames.DebtType.Interest.Name;
        //                        }
                                
        //                        b.Period = bill.Period;

        //                        b.Description = string.Format("วันที่ {0} ถึง {1} จำนวน {2} วัน",
        //                            stDate.AddDays(+1).ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
        //                            toPayDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
        //                            totalDays);

        //                        decimal interest = 0;
        //                        int leapDays = 0;

        //                        if (toPayDate.Year - stDate.Year == 0)
        //                        {
        //                            leapDays = DateTime.IsLeapYear(toPayDate.Year) == true ? 366 : 365;

        //                            interest = toCalInterestAmount
        //                                * (Convert.ToDecimal(totalDays) / Convert.ToDecimal(leapDays))
        //                                * (bill.InterestRate.Value / Convert.ToDecimal(100));
        //                            interest = Decimal.Round(interest, 2);
        //                        }
        //                        else
        //                        {
        //                            int tmpYear = beginYear;
        //                            while (tmpYear <= endYear)
        //                            {
        //                                decimal tmpInterest = 0;
        //                                leapDays = DateTime.IsLeapYear(tmpYear) == true ? 366 : 365;
        //                                if (tmpYear < endYear && tmpYear == beginYear)
        //                                {
        //                                    totalDays = leapDays - stDate.DayOfYear;
        //                                }
        //                                else if (tmpYear < endYear && tmpYear > beginYear)
        //                                {
        //                                    totalDays = leapDays;
        //                                }
        //                                else if (tmpYear == endYear)
        //                                {
        //                                    totalDays = toPayDate.DayOfYear;
        //                                }


        //                                tmpInterest = toCalInterestAmount
        //                                   * (Convert.ToDecimal(totalDays) / Convert.ToDecimal(leapDays))
        //                                   * (bill.InterestRate.Value / Convert.ToDecimal(100));
        //                                tmpInterest = Decimal.Round(tmpInterest, 2);

        //                                interest += tmpInterest;
        //                                totalCalculateDays += totalDays;
        //                                tmpYear++;
        //                            }
        //                        }


        //                        b.InterestRate = bill.InterestRate;
        //                        b.Qty = totalCalculateDays;
        //                        b.FullQty = totalCalculateDays;
        //                        b.UnitTypeId = CodeNames.UnitType.Day.Id;
        //                        b.UnitTypeName = CodeNames.UnitType.Day.Name;
        //                        b.AmountExVat = interest;
        //                        b.FullAmount = interest;
        //                        b.TaxCode = CodeNames.TaxCode.NoTaxCharge.TaxCode;
        //                        b.TaxRate = StringConvert.ToDecimal(CodeNames.TaxCode.NoTaxCharge.TaxRate);
        //                        b.GAmount = b.AmountExVat;
        //                        b.FullGAmount = b.AmountExVat;
        //                        b.ControllerId = bill.ControllerId;
        //                        b.OriginalDtId = inv.OriginalDtId;
        //                        b.DataState = BillDataStage.NewItem;

        //                        Invoice i = new Invoice();
        //                        i.InvoiceNo = null;
        //                        if (inv.InvoiceNo.Length == 22 && inv.Bills[0].GroupInvoiceId != null) // กรณีดอกเบี้ยการชำระแบบกลุ่ม
        //                        {
        //                            i.OriginalInvoiceNo = inv.Bills[0].GroupInvoiceId;
        //                        }
        //                        else if (inv.Bills[0].ShortDebtId == "M0080")
        //                        {
        //                            i.OriginalInvoiceNo = inv.OriginalInvoiceNo;
        //                        }
        //                        else
        //                        {
        //                            i.OriginalInvoiceNo = inv.InvoiceNo;
        //                        }
        //                        i.BranchId = b.BranchId;
        //                        i.TechBranchName = inv.TechBranchName;
        //                        i.CommBranchId = inv.CommBranchId;
        //                        i.CommBranchName = inv.CommBranchName;
        //                        i.CaId = b.CustomerId;
        //                        i.Name = b.Name;
        //                        i.Address = b.Address;
        //                        i.ControllerId = inv.ControllerId;
        //                        i.ControllerName = inv.ControllerName;
        //                        i.MruId = inv.MruId;

        //                        i.GAmount = b.GAmount;
        //                        i.PaidVatAmount = 0;
        //                        i.PaidGAmount = 0;
        //                        i.ToPayQty = b.Qty;
        //                        i.ToPayGAmount = i.ToBePaidGAmount;
        //                        i.ToPayVatAmount = i.ToBePaidVatAmount;
        //                        i.CaDoc = null;                                
        //                        i.DataState = InvoiceDataStage.NewItem;
        //                        i.Bills = new List<Bill>();
        //                        i.Bills.Add(b);

        //                        paidInvoices.Add(i);
        //                    }
        //                }
        //            }
        //        }

        //    }

        //    return paidInvoices;
        //}

        
        public List<BillSearchDetail> SearchBillByCustomerDetail(CustomerSearchParam param)
        {
            BillingDA da = new BillingDA();
            List<BillSearchDetail> bills = da.SearchBillByCustomerDetail(param);

            return bills;
        }
        
        public List<Bill> SearchBillByBillBookId(string billBookId, string billBookStatus)
        {
            BillingDA da = new BillingDA();
            List<Bill> bills = da.SearchBillByBillBookId(billBookId, billBookStatus);
                
            return bills;
        }

        public List<BookSearchDetail> SearchBillByAgent(AgencySearchParam param)
        {
            BillingDA da = new BillingDA();
            return da.SearchBillByAgent(param);
        }

        public List<PaymentMethod> SearchAGPayment(string billBookId)
        {
            BillingDA da = new BillingDA();
            List<PaymentMethod> paymentMethods = da.SearchAGPayment(billBookId);

            return paymentMethods;
        }

        public List<GroupInvoiceItem> GetGroupInvoiceItem(string billBookId)
        {
            BillingDA da = new BillingDA();
            List<GroupInvoiceItem> giis = da.GetGroupInvoiceItem(billBookId);

            return giis;
        }

        public CaIdAndBranchId GetBranchIdByCaId(string caId)
        {
            BillingDA da = new BillingDA();
            return da.GetBranchIdByCaId(caId);
        }

        public Customer GetCustomerDetailOnPaymentHistory(HistoryViewParam param)
        {
            BillingDA da = new BillingDA();                
            return da.GetCustomerDetail(param.CaId);
        }

        public List<PaidInvoice> GetPaymentHistory(HistoryViewParam param)
        {
            BillingDA da = new BillingDA();
            List<PaidInvoice> paidInvoices = da.GetPaymentHistory(param.CaId);

            return paidInvoices;
        }

        public List<BillBook> SearchBillBookByDetail(string billBookId, string agencyId, string agencyName)
        {
            BillingDA da = new BillingDA();
            return da.SearchBillBookByDetail(billBookId, agencyId, agencyName);
        }

        public BillBook GetBillBookDetail(string billBookId, string statusLineStr)
        {
            BillingDA da = new BillingDA();
            return da.GetBillBookDetail(billBookId, statusLineStr);
        }


        public List<AdvancePaymentHistory> SearchAdvancePaymentHistory(string billBookId)
        {
            BillingDA da = new BillingDA();
            return da.SearchAdvancePaymentHistory(billBookId);
        }

        public ResultPayInvoice PayInvoice(DbTransaction trans, List<Invoice> paidInvoices, List<PaymentMethod> paymentMethods,
            List<PrintingReceipt> receipts, List<PrintingReceipt> groupDividualReceipts, ExternalReceipt extReceipt,
            DateTime paymentDate, string posId, string terminalCode, string branchId, string branchName, decimal? payingAmount, string screenType,
            string cashierId, string cashierName, string workId, bool isOffline)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                try
                {
                    BillingDA da = new BillingDA();

                    string postedServerId = Session.Branch.PostedServerId;

                    string paymentId = da.InsertIntoPayment(trans, paymentDate, posId, terminalCode, branchId, branchName, postedServerId, cashierId, cashierName, workId, isOffline);

                    //Insert data to ARPaymentType Table
                    foreach (PaymentMethod pm in paymentMethods)
                    {
                        pm.ARPtId = da.InsertIntoARPaymentType(trans, paymentId, pm, branchId, posId, postedServerId, isOffline);
                    }

                    //Insert new AR record
                    foreach (Invoice inv in paidInvoices)
                    {
                        if (inv.DataState == InvoiceDataStage.NewItem || inv.DataState == InvoiceDataStage.Offline
                            || inv.DataState == InvoiceDataStage.NewItemOneTouch
                            )
                        {
                            string temp = inv.InvoiceNo;

                            //inv.InvoiceNo = da.InsertIntoAR(trans, branchId, posId, inv, postedServerId, isOffline);

                            if (inv.DataState == InvoiceDataStage.NewItemOneTouch)
                            {
                                da.InsertIntoAR(trans, branchId, posId, inv, postedServerId, isOffline);
                            }
                            else
                            {
                                inv.InvoiceNo = da.InsertIntoAR(trans, branchId, posId, inv, postedServerId, isOffline);
                            }


                            if (inv.InvoiceNo.Length == 22)
                            {
                                inv.SpotBillInvoiceNo = temp;
                            }
                        }
                    }

                    //Insert data to ARPayment Table
                    foreach (Invoice inv in paidInvoices)
                    {
                        inv.PaymentId = paymentId;
                        inv.ARPmId = da.InsertIntoARPayment(trans, inv, branchId, CodeNames.PaymentMethod.Counter.Id, paymentDate, posId, postedServerId, isOffline);
                    }

                    // Insert data to RTARPaymentTypeARPayment Table
                    foreach (PaymentMethod pm in paymentMethods)
                    {
                        foreach (InvoicePaymentMethod iv in pm.ToPayInvoices)
                        {
                            da.InsertIntoRTARPaymentTypeARPayment(trans, pm.ARPtId, iv.GetInvoice(paidInvoices).ARPmId, iv.Amount, postedServerId, isOffline);
                        }
                    }

                    // Check paying electric debt by BillBook & GroupInvoice
                    foreach (Invoice piv in paidInvoices)
                    {
                        if (piv.Bills[0].DebtId == "P00010002")
                        {
                            da.UpdateElectricPaymentByBillBook(trans, piv.Bills[0].BillBookId,
                                paymentId, paymentDate, posId, terminalCode, branchId, branchName,
                                cashierId, cashierName, workId, postedServerId, isOffline);
                        }
                        else if (piv.Bills[0].DebtId == "P00020001")
                        {
                            da.UpdateElectricPaymentByGroupInvoice(trans, piv.Bills[0].BillBookId,
                                paymentId, paymentDate, posId, terminalCode, branchId, branchName,
                                cashierId, cashierName, workId, postedServerId, isOffline);
                        }
                    }

                    #region //// NOT USE CODE
                    //if (groupDividualReceipts != null && groupDividualReceipts.Count > 0)  
                    //{
                    //    receipts = new List<PrintingReceipt>(groupDividualReceipts);

                    //    foreach (PrintingReceipt pr in receipts)
                    //    {
                    //        foreach (PrintingInvoice pb in pr.PrintingInvoices)
                    //        {
                    //            pb.PaymentId = paymentId;
                    //            foreach (Invoice inv in paidInvoices)
                    //            {
                    //                if (inv.UiRefId == pb.UiRefId)
                    //                {
                    //                    pb.InvoiceNo = inv.InvoiceNo;
                    //                    pb.ARPmId = inv.ARPmId;
                    //                    break;
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    //For other cases
                    //else  
                    //{
                    //    foreach (PrintingReceipt pr in receipts)
                    //    {
                    //        foreach (PrintingInvoice pb in pr.PrintingInvoices)
                    //        {
                    //            foreach (Invoice inv in paidInvoices)
                    //            {
                    //                if (inv.UiRefId == pb.UiRefId)
                    //                {
                    //                    if (inv.Bills[0].DebtId.Substring(0, 5) != "M0080")
                    //                    {
                    //                        pb.InvoiceNo = inv.InvoiceNo;
                    //                    }                                        
                    //                    pb.PaymentId = paymentId;
                    //                    pb.ARPmId = inv.ARPmId;
                    //                    break;
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    #endregion


                    //Insert key of PaymentId & ARPmId for Receipt & ReceiptItem Table
                    if (groupDividualReceipts != null && groupDividualReceipts.Count > 0)
                    {
                        List<Invoice> groupDividualInvoice = paidInvoices.FindAll(
                                    delegate(Invoice inv)
                                    {
                                        return inv.Bills[0].DebtId == CodeNames.DebtType.AgencyGroupInvoicing.Id && inv.GroupInvoiceReceiptType == CodeNames.GroupInvoiceReceiptType.Dividual;
                                    }
                                    );

                        List<string> invoiceList = new List<string>();

                        foreach (Invoice inv in groupDividualInvoice)
                        {
                            if (inv.Bills[0].BillBookId != null)
                            {
                                invoiceList.AddRange(da.SearchARPmIdByBillBookId(inv.Bills[0].BillBookId, paymentDate));
                            }
                        }

                        receipts = new List<PrintingReceipt>(groupDividualReceipts);

                        foreach (PrintingReceipt pr in receipts)
                        {
                            foreach (PrintingInvoice pb in pr.PrintingInvoices)
                            {
                                pb.PaymentId = paymentId;
                                foreach (Invoice inv in paidInvoices)
                                {
                                    if (inv.UiRefId == pb.UiRefId)
                                    {
                                        pb.InvoiceNo = inv.InvoiceNo;
                                        pb.ARPmId = inv.ARPmId;
                                        break;
                                    }
                                    else if (inv.Bills[0].DebtId == CodeNames.DebtType.AgencyGroupInvoicing.Id
                                                && inv.GroupInvoiceReceiptType == CodeNames.GroupInvoiceReceiptType.Dividual
                                                && pb.Bills[0].BillBookId == inv.Bills[0].BillBookId)
                                    {
                                        foreach (string invoice in invoiceList)
                                        {
                                            if (invoice.IndexOf(pb.InvoiceNo) > -1)
                                            {
                                                pb.ARPmId = invoice.Split(':')[0];
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //For other cases
                    else
                    {
                        foreach (PrintingReceipt pr in receipts)
                        {
                            foreach (PrintingInvoice pb in pr.PrintingInvoices)
                            {
                                foreach (Invoice inv in paidInvoices)
                                {
                                    if (inv.UiRefId == pb.UiRefId)
                                    {
                                        if (inv.Bills[0].DebtId.Substring(0, 5) != "M0080")
                                        {
                                            pb.InvoiceNo = inv.InvoiceNo;
                                        }
                                        pb.PaymentId = paymentId;
                                        pb.ARPmId = inv.ARPmId;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    //Insert data to Receipt & ReceiptItem Table
                    foreach (PrintingReceipt r in receipts)
                    {
                        da.SaveReceipt(trans, r, extReceipt, postedServerId, isOffline);
                    }

                    #region NOT USE CODE
                    //TODO: INSTALLMENT CASE
                    //List<Invoice> pi = paidInvoices.FindAll(delegate(Invoice inv) { return inv.IsInvalidInstment == true; });
                    //if (pi.Count > 0)
                    //{
                    //    List<InterestLog> interestLog = new List<InterestLog>();
                    //    foreach (Invoice i in pi)
                    //    {
                    //        InterestLog il = new InterestLog();

                    //        if (interestLog.FindAll(delegate(InterestLog intLog) { return intLog.CaDoc == i.Bills[0].MainCaDoc; }).Count > 0)
                    //        {
                    //            foreach (InterestLog l in interestLog)
                    //            {
                    //                if (l.CaDoc == i.Bills[0].MainCaDoc)
                    //                {
                    //                    l.SubCaDoc += "," + i.Bills[0].ItemId.TrimEnd();
                    //                }
                    //            }
                    //        }
                    //        else
                    //        {
                    //            il.CaDoc = i.Bills[0].MainCaDoc;
                    //            il.SubCaDoc = i.Bills[0].ItemId.TrimEnd();
                    //            il.InvoiceNo = i.OriginalInvoiceNo;
                    //            il.PaymentBranchId = branchId;
                    //            il.TechBranchId = i.BranchId;
                    //            il.PaymentDt = paymentDate;
                    //            il.CashierId = cashierId;
                    //            il.CashierName = cashierName;
                    //            interestLog.Add(il);
                    //        }
                    //    }

                    //    if (interestLog.Count > 0)
                    //    {
                    //        da.SaveInterestLog(trans, interestLog);
                    //    }
                    //}
                    #endregion                    

                    #region Start DCR รวมใบเสร็จแผนผ่อน 2021AUG (BillingBS) PART (1)
                    //BillingDA GR_DA     = new BillingDA();
                    #region //// STEP 1: CREATE LIST OF INVOICE SEPERATE BY "|"
                    var GR_INVOICENOS   = new StringBuilder();
                    var GR_PAYDATE      = paymentDate;
                    bool GR_INIT        = false;
                    var GR_INV_QTY      = 0;
                    #region //// CREATE LIST OF INVOICE NO.

                    foreach (Invoice inv in paidInvoices)
                    {
                        if (!String.IsNullOrEmpty(inv.InvoiceNo))
                        {
                            if (GR_INIT == false)
                            {
                                GR_INVOICENOS.Append(inv.InvoiceNo);
                                GR_INIT = true;
                                GR_INV_QTY += 1;
                            }
                            else
                            {
                                GR_INVOICENOS.Append("|");
                                GR_INVOICENOS.Append(inv.InvoiceNo);
                                GR_INV_QTY += 1;
                            }
                        }
                    }

                    #endregion

                    var GR_RECEIPTS     = new StringBuilder();
                    bool GR_R_INIT      = false;
                    int GR_RECEIPT_QTY  = 0;

                    #region //// CREATE LIST OF RECEIPT ID.

                    foreach (PrintingReceipt r in receipts)
                    {
                        if (!String.IsNullOrEmpty(r.ReceiptId))
                        {                            
                            if (GR_R_INIT == false)
                            {
                                GR_RECEIPTS.Append(r.ReceiptId);
                                GR_R_INIT = true;
                                GR_RECEIPT_QTY += 1;
                            }
                            else
                            {
                                GR_RECEIPTS.Append("|");
                                GR_RECEIPTS.Append(r.ReceiptId);
                                GR_RECEIPT_QTY += 1;
                            }
                        }
                    }

                    #endregion

                    #region //// STEP 2: CONFIRM AND CREATE GROUP RECEIPT

                    var GR_ITEMS = new List<ReceiptGroupItems>();
                    GR_ITEMS     = da.GetGroupReceiptItems(trans,GR_RECEIPTS.ToString(), GR_INVOICENOS.ToString(), GR_PAYDATE);

                    List<PrintingReceipt> GR_PrintingReceipt     = new List<PrintingReceipt>();
                    List<PrintingReceipt> TMP_GR_PrintingReceipt = new List<PrintingReceipt>();
                    GR_PrintingReceipt      = (from t0 in receipts select t0 ).ToList();
                    TMP_GR_PrintingReceipt  = (from t0 in receipts select t0 ).ToList();

                    decimal tmpAdjChangeAmount = 0m;

                    foreach (var r in GR_PrintingReceipt)
                    {
                        if (r.AdjChangeAmount != 0)
                        {
                            tmpAdjChangeAmount = r.AdjChangeAmount;
                        }
                    }

                    if (GR_ITEMS != null && GR_ITEMS.Count > 0)
                    { 
                        //// แก้เรื่องเลขที่ใบเสร็จกระโดด 2021-09-23 16:38
                        //// สร้าง object ใหม่สำหรับ Re-Assign เลขที่ใบเสร็จใหม่โดยใช้ เลขที่ ExtReceiptId แทน
                        
                        foreach (ReceiptGroupItems t in GR_ITEMS)
                        {
                            var NewExtReceiptData = new PrintingReceipt();
                            if ((t.MainGroupReceiptId == t.ReceiptId) && !String.IsNullOrEmpty(t.ReceiptId) && !String.IsNullOrEmpty(t.InvoiceNo))
                            {
                                foreach (var r in TMP_GR_PrintingReceipt)
                                {
                                    foreach (var v in r.PrintingInvoices)
                                    {
                                        if (v.InvoiceNo == t.InvoiceNo)
                                        {
                                            PrintingReceipt item = new PrintingReceipt();
                                            //item                 = GR_PrintingReceipt.Find(xx => xx.ReceiptId == r.ReceiptId);
                                            try
                                            {
                                                item = TMP_GR_PrintingReceipt.Where(m => m.PrintingInvoices.Any(u => u.InvoiceNo == t.InvoiceNo)).First();

                                                NewExtReceiptData = item;
                                                //// เปลี่ยน ReceiptId จาก ที่ขึ้นต้นด้วย X เป็น ExtReceiptId ที่เป็น Main Group ReceiptId
                                                NewExtReceiptData.ReceiptId = t.MainGroupReceiptId;                                                
                                                //// เพิ่ม Object ที่มี ExtReceiptId เข้าไปในชุดข้อมูลเดิม
                                                GR_PrintingReceipt.Add(NewExtReceiptData);
                                                //// ลบ Object ที่มี ReceiptId ที่เป็น X ออก
                                                GR_PrintingReceipt.Remove(item);

                                            }
                                            catch (Exception ex)
                                            {
                                                string debugText = ex.Message;
                                            }                                            
                                        }
                                    } 
                                }                               
                            }
                        } 
                    }


                    #region Version แรก
                    if (GR_ITEMS != null && GR_ITEMS.Count > 0)
                    {                        
                        #region //// STEP 3: Remove ใบเสร็จ ที่ไม่ใช่ตัวหลัก ออกจากรายการ

                        foreach (ReceiptGroupItems t in GR_ITEMS)
                        {
                            if ((t.MainGroupReceiptId != t.ReceiptId) && !String.IsNullOrEmpty(t.ReceiptId) && !String.IsNullOrEmpty(t.MainGroupReceiptId))
                            {
                                #region 1)
                                string tmpMainReceiptId = t.MainGroupReceiptId;
                                string tmpReceiptId     = t.ReceiptId;

                                try
                                {
                                    PrintingReceipt item = new PrintingReceipt();
                                    item = GR_PrintingReceipt.Find(xx => xx.ReceiptId == tmpReceiptId);
                                    if (item.ReceiptId == tmpReceiptId)
                                    {
                                        GR_PrintingReceipt.Remove(item);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    string debugText = ex.Message;
                                }


                                //// STEP 4: Remove รายการ InvoiceNo ที่ไม่ใช่ตัวหลัก ออกจากรายการ
                                //foreach (var r in GR_PrintingReceipt)
                                //{
                                //    foreach (var inv in r.PrintingInvoices)
                                //    {
                                //        try
                                //        {
                                //            var tmpInvoice = new PrintingInvoice();
                                //            tmpInvoice = r.PrintingInvoices.Find(xx => xx.InvoiceNo == t.InvoiceNo);
                                //            if (tmpInvoice.InvoiceNo == t.InvoiceNo && tmpInvoice != null)
                                //            {
                                //                r.PrintingInvoices.Remove(tmpInvoice);
                                //            }
                                //        }
                                //        catch (Exception ex)
                                //        {
                                //            string debugText = ex.Message;
                                //        }

                                //    }
                                //}
                                #endregion

                                #region 2)
                                ////
                                #endregion
                            }
                        }

                        #endregion

                        #region //// STEP 5: Assign Summary Value to Main Group Receipt
                        foreach (var r in GR_PrintingReceipt)
                        {
                            var data                = new ReceiptGroupMainData();
                            data                    = da.GetGroupReceiptMainData(trans, r.ReceiptId);
                            if (data != null && r.ReceiptId == data.GroupReceiptId)
                            {
                                r.GroupReceiptOrNot         = "Y";
                                r.GroupReceiptAmount        = data.GroupReceiptAmount;
                                r.GroupReceiptQty           = data.GroupReceiptQty;
                                r.GroupReceiptPeriodText    = data.GroupReceiptPeriodText;
                                r.GroupReceiptInstallmentText = data.GroupReceiptInstallmentText;
                                r.GroupReceiptTotal         = data.GroupReceiptTotal;
                                r.GroupReceiptVatTotal      = data.GroupReceiptVatTotal;
                                r.GroupReceiptMeterIdText   = data.GroupReceiptMeterIdText;
                                r.GroupReceiptRateTypeText  = data.GroupReceiptRateTypeText;
                                r.GroupXReceiptId           = data.GroupReceiptXReceiptId;

                                foreach (var inv in r.PrintingInvoices)
                                {
                                    if (inv.InvoiceNo == data.GroupReceiptInvoiceNo)
                                    {
                                        inv.GroupReceiptAmount          = data.GroupReceiptAmount;
                                        inv.GroupReceiptId              = data.GroupReceiptId;
                                        inv.GroupReceiptInstallmentText = data.GroupReceiptInstallmentText;
                                        inv.GroupReceiptInvoiceNo       = data.GroupReceiptInvoiceNo;
                                        inv.GroupReceiptPeriodText      = data.GroupReceiptPeriodText;
                                        inv.GroupReceiptTotal           = data.GroupReceiptTotal;
                                        inv.GroupReceiptVatTotal        = data.GroupReceiptVatTotal;
                                        inv.GroupReceiptMeterIdText     = data.GroupReceiptMeterIdText;
                                        inv.GroupReceiptRateTypeText    = data.GroupReceiptRateTypeText;

                                    }
                                }
                            }
                        }

                        #endregion

                    }
                    #endregion                           
                    
                    //foreach(var g in GR_ITEMS)
                    //{
                    //    //// STEP 2) Remove เลขที่ใบเสร็จที่เหมือน กับ GroupReceipt ให้เหลือแค่ใบเดียว                         
                    //    foreach (var t in TMP_GR_PrintingReceipt)   //// ต้องใช้ TMP เพราะไม่สามารถ Remove ออก ขณะใช้งานได้
                    //    {
                    //        if (t.ReceiptId == g.MainGroupReceiptId)
                    //        {
                    //            foreach (var d in t.PrintingInvoices)
                    //            {
                    //                if (d.InvoiceNo != g.InvoiceNo)
                    //                {
                    //                    GR_PrintingReceipt.Remove(t);   //// Remove  
                    //                }                                    
                    //            }                                    
                    //        }
                    //    }
                    //}                        
                    

                    //// Assign Value ของใบเสร็จหลักให้กับ ReceiptId ที่ตรงกัน
                    //foreach (var g in GR_ITEMS)
                    //{
                        foreach (var r in GR_PrintingReceipt)
                        { 
                            var data    = new ReceiptGroupMainData();
                            data        = da.GetGroupReceiptMainData(trans, r.ReceiptId);
                            if (data != null && r.ReceiptId == data.GroupReceiptId)
                            {
                                r.GroupReceiptOrNot         = "Y";
                                r.GroupReceiptAmount        = data.GroupReceiptAmount;
                                r.GroupReceiptQty           = data.GroupReceiptQty;
                                r.GroupReceiptPeriodText    = data.GroupReceiptPeriodText;
                                r.GroupReceiptInstallmentText = data.GroupReceiptInstallmentText;
                                r.GroupReceiptTotal         = data.GroupReceiptTotal;
                                r.GroupReceiptVatTotal      = data.GroupReceiptVatTotal;
                                r.GroupReceiptMeterIdText   = data.GroupReceiptMeterIdText;
                                r.GroupReceiptRateTypeText  = data.GroupReceiptRateTypeText;
                                r.GroupXReceiptId           = data.GroupReceiptXReceiptId;

                                foreach (var inv in r.PrintingInvoices)
                                {
                                    if (inv.InvoiceNo == data.GroupReceiptInvoiceNo)
                                    {
                                        inv.GroupReceiptAmount          = data.GroupReceiptAmount;
                                        inv.GroupReceiptId              = data.GroupReceiptId;
                                        inv.GroupReceiptInstallmentText = data.GroupReceiptInstallmentText;
                                        inv.GroupReceiptInvoiceNo       = data.GroupReceiptInvoiceNo;
                                        inv.GroupReceiptPeriodText      = data.GroupReceiptPeriodText;
                                        inv.GroupReceiptTotal           = data.GroupReceiptTotal;
                                        inv.GroupReceiptVatTotal        = data.GroupReceiptVatTotal;
                                        inv.GroupReceiptMeterIdText     = data.GroupReceiptMeterIdText;
                                        inv.GroupReceiptRateTypeText    = data.GroupReceiptRateTypeText;

                                    }
                                }
                            }
                        }
                    //}
                    

                    #endregion

                    #endregion

                    #endregion

                    // รวมใบเสร็จแผนผ่อน Get Payment Methods 
                    if (GR_ITEMS != null && GR_ITEMS.Count > 0)
                    {
                        foreach (var r in GR_PrintingReceipt)
                        {                            
                            string receiptId            = r.ReceiptId;
                            //// รวมใบเสร็จแผนผ่อน จัดเรียง PrintingSequence จาก Stored Procedure
                            r.GroupReceiptPrintingSeqTextWithPipe       = da.GetReceiptPrintingSeqTextByReceiptId(trans, r.ReceiptId);
                            List<string> PaymentMethodsText             = new List<string>();
                            PaymentMethodsText                          = da.GetGroupReceiptPaymentMethods(trans, receiptId);
                            if (PaymentMethodsText != null && PaymentMethodsText.Count > 0)
                            {
                                if (PaymentMethodsText[0].ToString() != null && PaymentMethodsText[0].ToString().Length > 1)
                                {
                                    r.GroupReceiptPaymentMethodsWithPipe = PaymentMethodsText[0].ToString();
                                }

                                if (PaymentMethodsText[1].ToString() != null && PaymentMethodsText[1].ToString().Length > 1)
                                {
                                    r.GroupReceiptPaymentMethodsWithPipePOSSlip = PaymentMethodsText[1].ToString(); 
                                }                                                               
                            }                            
                        }
                    }

                    #region DCR GroupInvoiceText START 2021-OCT-25 Uthen.P (BillingBS)
                    if (GR_ITEMS != null && GR_ITEMS.Count > 0)
                    {
                        #region
                        foreach (var r in GR_PrintingReceipt)
                        {
                            foreach (var p in r.PrintingInvoices)
                            {
                                if (p.DisplayCaId.StartsWith("4") == true && p.DisplayInvoiceNo != null)
                                {
                                    string groupInvoiceNo = p.DisplayInvoiceNo;
                                    string tmpResult = da.GetGroupInvoiceDescriptionText(trans, groupInvoiceNo);
                                    if (tmpResult != null)
                                    {
                                        r.GroupInvoiceDescriptionText = tmpResult;
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region
                        foreach (var r in receipts)
                        {
                            foreach (var p in r.PrintingInvoices)
                            {
                                if (p.DisplayCaId.StartsWith("4") == true && p.DisplayInvoiceNo != null)
                                {
                                    string groupInvoiceNo = p.DisplayInvoiceNo;
                                    string tmpResult = da.GetGroupInvoiceDescriptionText(trans, groupInvoiceNo);
                                    if (tmpResult != null)
                                    {
                                        r.GroupInvoiceDescriptionText = tmpResult;
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    #endregion

                    trans.Commit();

                    ResultPayInvoice obj = new ResultPayInvoice();
                    obj.PaidInvoices     = paidInvoices;

                    if (GR_ITEMS != null && GR_ITEMS.Count > 0)
                    {
                        GR_PrintingReceipt = MappingNewPrintingSeq(GR_PrintingReceipt);
                        GR_PrintingReceipt = ReAssignAdjChangeAmount(GR_PrintingReceipt, tmpAdjChangeAmount);
                        GR_PrintingReceipt = (List<PrintingReceipt>)GR_PrintingReceipt.OrderBy(p => p.PrintingSequence).ToList();
                        obj.Receipts       = GR_PrintingReceipt; 
                    }
                    else
                    {
                        //// Original Code
                        obj.Receipts = receipts;
                    }                    
                    
                    obj.OneTouchFlag     = true;   //

                    //Insert Log OneTouch
                    
                    //foreach (PrintingReceipt r in receipts)
                    //{
                    //    if (r.PrintingInvoices[0].DataState == InvoiceDataStage.NewItemOneTouch)
                    //    {

                    //        OneTouchLogInfo OneTouchLog = new OneTouchLogInfo();
                    //        OneTouchLog.NotificationNo = r.PrintingInvoices[0].NotificationNo;
                    //        OneTouchLog.InvoiceNo = r.PrintingInvoices[0].SpotBillInvoiceNo;
                    //        OneTouchLog.DebtId = r.PrintingInvoices[0].Bills[0].DebtId; //เพิ่ม DebTyptId
                    //        OneTouchLog.ReceiptId = r.ReceiptId.Replace("-", "");
                    //        OneTouchLog.Action = "1"; //Add
                    //        OneTouchLog.GAmount = r.PrintingInvoices[0].ToPayGAmount;
                    //        OneTouchLog.VatAmount = r.PrintingInvoices[0].ToPayVatAmount;
                    //        OneTouchLog.ModifiedBy = ((isOffline == true) ? "OFFLINE" : Session.User.Id);

                    //        //Call Web Service OneTouch
                    //        bool flag = FlagOneTouchPayment(OneTouchLog);

                    //        if (flag == false)
                    //        {
                    //            OneTouchLog.SyncFlag = "1";
                    //            obj.OneTouchFlag = false;
                    //        }
                    //        else
                    //        {
                    //            OneTouchLog.SyncFlag = "0";  //ส่งได้
                    //            obj.OneTouchFlag = true;
                    //        }

                    //        //Insert Log OneTouch
                    //        try
                    //        {
                    //            da.SaveOneTouchLog(OneTouchLog);
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            //
                    //        }
                    //    }
                    //}//

                    return obj;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Logger.WriteError(Logger.Module.POS, "PayInvoice", ex.ToString());
                    throw;
                }
            }
        }

        //// รวมใบเสร็จแผนผ่อน ปรับการแสดงลำดับ Printing Sequence ด้วย Store Procedure
        private List<PrintingReceipt> MappingNewPrintingSeq(List<PrintingReceipt> printingReceipts)
        {
            foreach (var r in printingReceipts)
            {
                try
                {
                    if (r.GroupReceiptPrintingSeqTextWithPipe != null)
                    {
                        string printSeqText = r.GroupReceiptPrintingSeqTextWithPipe;
                        string[] subs = printSeqText.Split('|');
                        string tmpReceiptId = subs[0].Trim().ToString();
                        short tmpPrintingSeq = (short)Convert.ToInt32(subs[1].Trim().ToString());
                        short tmpTotalReceipt = (short)Convert.ToInt32(subs[2].Trim().ToString());
                        r.TotalReceipt = tmpTotalReceipt;
                        r.PrintingSequence = tmpPrintingSeq;
                    }
                }
                catch (Exception ex)
                {

                }

            }

            return printingReceipts;
        }

        private List<PrintingReceipt> ReAssignAdjChangeAmount(List<PrintingReceipt> printingReceipts, decimal adjChangeAmount)
        {
            foreach (var r in printingReceipts)
            {
                try
                {
                    if (r.TotalReceipt == r.PrintingSequence)
                    {
                        r.AdjChangeAmount = adjChangeAmount;
                    }                    
                }
                catch (Exception ex)
                {

                }

            }

            return printingReceipts;
        }




        public List<CAC14Report> GetReportCAC14(CAC14Param param)
        {
            ReportDA da = new ReportDA();
            return da.GetReportCAC14(param);
        }


        public DayClosingResult CloseDayPayment(string branchId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<DisconnectionDoc> SearchDisconnectionStatusByDiscNo(string discNo)
        {
            BillingDA da = new BillingDA();
            List<DisconnectionDoc> disconnectiondocs = da.SearchDisconnectionStatusByDiscNo(discNo);
            return disconnectiondocs;
        }

        // obsolete! don't call. use get serverTime() in common instead
        public DateTime GetServerTime()
        {
            return DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
        }

        public List<LastReceiptPayment> GetRepayLastReceiptData(string receiptId)
        {
            BillingDA da = new BillingDA();
            List<LastReceiptPayment> lastReceiptPayments = da.GetRepayLastReceiptData(receiptId);
            return lastReceiptPayments;
        }

        #endregion

        #region SAP Connector

        private ArrayList GetDataFromSAP(SAPSearchParam param)
        {
            string rfcGateWayConn = ConfigurationManager.AppSettings["SAP_GATEWAY"];

            SAPRfcWS.SAPRfcService rfc = new SAPRfcWS.SAPRfcService();
            rfc.Url = rfcGateWayConn;
            DataSet ds = rfc.GetDataFromSAP(param.CaId);

            List<BusinessPartnerInfo> bp = ToBusinessPartner(ds.Tables[0]);
            List<ContractAccountInfo> ca = ToContractAccount(ds.Tables[1]);
            List<ARInfo> ar = ToAR(ds.Tables[2]);
            List<PayFromSAPInfo> ps = ToPayFromSAP(ds.Tables[3]);
            List<DisconnectionDoc> dd = ToDisconnectionDoc(ds.Tables[4]);
            List<RTDisconnectionDocCaDoc> dc = ToRTDisconnectionDocCaDoc(ds.Tables[5]);

            ArrayList list = new ArrayList();
            list.Add(bp);
            list.Add(ca);
            list.Add(ar);
            list.Add(ps);
            list.Add(dd);
            list.Add(dc);

            return list;
        }


        private void SaveInvoiceFromSAP(ArrayList list, string postedServerId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbTransaction trans;
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                try
                {
                    BillingDA da = new BillingDA();
                    da.InsertIntoBusinessPartner((List<BusinessPartnerInfo>)list[0], trans);
                    da.InsertIntoContractAccount((List<ContractAccountInfo>)list[1], trans);
                    da.InsertIntoAR((List<ARInfo>)list[2], trans, postedServerId);
                    da.InsertIntoPayFromSAP((List<PayFromSAPInfo>)list[3], trans, postedServerId);
                    da.InsertIntoDisconnectionDoc((List<DisconnectionDoc>)list[4], trans);
                    da.InsertIntoRTDisconnectionDocCaDoc((List<RTDisconnectionDocCaDoc>)list[5], trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Logger.WriteError(Logger.Module.POS, "SaveInvoiceFromSAP(SAP Connector)", ex.ToString());
                    throw;
                }
            }
        }

        #endregion

        #region Data Mapping Functions

        public List<BusinessPartnerInfo> ToBusinessPartner(DataTable z60)
        {
            IFormatProvider provider = CultureInfo.CreateSpecificCulture("en-EN");
            List<BusinessPartnerInfo> list = new List<BusinessPartnerInfo>();
            foreach (DataRow dr in z60.Rows)
            {
                BusinessPartnerInfo obj = new BusinessPartnerInfo();
                obj.BpId = StringConvert.ToString(dr["Bp_No"].ToString()); //BpId
                obj.BpTypeId = StringConvert.ToString(dr["Bp_Cat"].ToString()); //BpTypeId
                obj.TaxCode = StringConvert.ToString(dr["Id1"].ToString()); //TaxCode
                obj.CitizenId = StringConvert.ToString(dr["Id2"].ToString()); //CitizenId
                obj.PassportNo = StringConvert.ToString(dr["Id3"].ToString()); //PassportNo
                obj.RegisterId = StringConvert.ToString(dr["Id4"].ToString()); //RegisterId
                obj.VatCode = StringConvert.ToString(dr["Id5"].ToString()); //VatCode
                if ((string)dr["Flag"] == "1")//CancelFlag
                    obj.Action = "3";
                obj.SyncFlag = "0";
                obj.ModifiedBy = "RFC_CALL";
                obj.ModifiedDt = DateTime.Now;
                obj.Action = (string)dr["Action"];

                string Id = (string)dr["Id"];
                if (Id == "MTR0060A")
                    obj.FileType = "A";
                else if (Id == "MTR0060B")
                    obj.FileType = "B";
                else
                    obj.FileType = "A";

                list.Add(obj);
            }
            return list;
        }

        public List<ContractAccountInfo> ToContractAccount(DataTable z90)
        {
            IFormatProvider provider = CultureInfo.CreateSpecificCulture("en-EN");
            List<ContractAccountInfo> list = new List<ContractAccountInfo>();
            foreach (DataRow dr in z90.Rows)
            {
                ContractAccountInfo obj = new ContractAccountInfo();
                obj.CaId = StringConvert.ToString(dr["Ca_No"].ToString()); //CaId
                obj.BpId = StringConvert.ToString(dr["Bp_No"].ToString());//BpId
                obj.TechBranchId = StringConvert.ToString(dr["Trsg"].ToString());//TechBranchId
                obj.CommBranchId = StringConvert.ToString(dr["Crsg"].ToString());//CommBranchId
                string mruId = dr["Mru"].ToString();
                if (mruId.Length == 8)
                    obj.MruId = mruId.Substring(4, mruId.Length - 4);//MruId
                else
                    obj.MruId = null;
                obj.CaName = StringConvert.ToString(dr["Bp_Name"].ToString());//CaName
                obj.CaAddress = (string)dr["Bp_Address1"] + (string)dr["Bp_Address2"];//CaAddress
                obj.CtId = StringConvert.ToString(dr["Ca_Cat"].ToString());//CtId
                obj.PmId = StringConvert.ToString(dr["Pay_Method"].ToString()); //PmId
                obj.AccountClassId = StringConvert.ToString(dr["Account_Class"].ToString());//AccountClassId
                obj.SecurityDeposit = StringConvert.ToDecimal(dr["Sec_Deposit"].ToString()); //SecurityDeposit
                obj.MeterInstallDt = StringConvert.ToDateTime(dr["Date_Meter"].ToString(), provider); //MeterInstallDt
                obj.MeterSizeId = StringConvert.ToString(dr["Function_Class"].ToString()); //MeterSizeId
                obj.CollectArea = StringConvert.ToString(dr["Collection_Area"].ToString());//CollectArea
                obj.PaidBy = StringConvert.ToString(dr["Paid_By"].ToString()); //PaidBy
                obj.TransportHelp = StringConvert.ToDecimal(dr["Travel_Fee"].ToString());//TransportHelp
                obj.PenaltyWaiveFlag = StringConvert.ToString(dr["Penalty_Flag"].ToString());//PenaltyWaiveFlag
                obj.FarLandHelp = StringConvert.ToDecimal(dr["Na1"].ToString());//FarLandHelp
                obj.ExtraMoneyHelp = StringConvert.ToDecimal(dr["Na2"].ToString()); //ExtraMoneyHelp
                obj.SignContractDt = StringConvert.ToDateTime(dr["Na3"].ToString(), provider); //SignContractDt
                obj.ContractValidFrom = StringConvert.ToDateTime(dr["Con_Begin"].ToString());//ContractValidFrom
                obj.ContractValidTo = StringConvert.ToDateTime(dr["Con_End"].ToString(), provider); //ContractValidTo
                obj.ReceiptType = StringConvert.ToString(dr["Receipt_Type"].ToString()); //ReceiptType
                obj.ReceiptPrintName = StringConvert.ToString(dr["Payer_Name"].ToString()); //ReceiptPrintName
                obj.InterestKey = StringConvert.ToString(dr["Interest_Key"].ToString()); //InterestKey
                obj.ControllerId = StringConvert.ToString(dr["Bill_Consol"].ToString()); //ControllerId
                obj.SyncFlag = "0";
                obj.ModifiedBy = "RFC_CALL";
                obj.ModifiedDt = DateTime.Now;
                obj.Action = dr["Action"].ToString();

                if (dr["Flag"].ToString() == "0") //Cancel Flag
                    obj.Action = "3";

                string Id = dr["Id"].ToString();
                if (Id == "MTR0090A")
                    obj.FileType = "A";
                else if (Id == "MTR0090B")
                    obj.FileType = "B";
                else if (Id == "MTR0090C")
                    obj.FileType = "C";
                else if (Id == "MTR0090D")
                    obj.FileType = "D";

                obj.CaTaxId = StringConvert.ToString(dr["TaxId"].ToString()); //CaTaxId
                obj.CaTaxBranch = StringConvert.ToString(dr["Branch"].ToString()); //CaTaxBranch

                list.Add(obj);
            }
            return list;
        }

        public List<ARInfo> ToAR(DataTable z10)
        {
            IFormatProvider provider = CultureInfo.CreateSpecificCulture("en-EN");
            List<ARInfo> list = new List<ARInfo>();

            foreach (DataRow dr in z10.Rows)
            {
                string itemId = StringConvert.ToString(dr["Ca_Doc"].ToString());
                ARInfo ar = list.Find(delegate(ARInfo a) { return a.ItemId == itemId; });

                if (ar != null)
                {
                    foreach (ARInfo a in list)
                    {
                        if (a.ItemId == itemId)
                        {
                            a.Amount += StringConvert.ToDecimal(dr["Amt_Tax_Base"].ToString());
                            a.VatAmount += StringConvert.ToDecimal(dr["Amt_Tax"].ToString());
                            a.GAmount += StringConvert.ToDecimal(dr["Amt"].ToString());
                        }
                    }
                }
                else
                {
                    ARInfo obj = new ARInfo();
                    obj.ItemId = StringConvert.ToString(dr["Ca_Doc"].ToString());
                    obj.CaId = StringConvert.ToString(dr["Ca_No"].ToString());
                    obj.DtId = "M" + StringConvert.ToString(dr["Main_Transaction"].ToString()) + StringConvert.ToString(dr["Sub_Transaction"].ToString());
                    obj.Description = StringConvert.ToString(dr["Text"].ToString());
                    obj.InvoiceNo = StringConvert.ToString(dr["Ref_Doc"].ToString());
                    obj.InvoiceDt = StringConvert.ToDateTime(dr["Doc_Date"].ToString(), provider);
                    obj.GroupInvoiceNo = StringConvert.ToString(dr["Classification"].ToString());
                    obj.Period = StringConvert.ToString(dr["Allocation_Date"].ToString());
                    obj.MeterId = StringConvert.ToString(dr["Equip_No"].ToString());
                    obj.RateTypeId = StringConvert.ToString(dr["Rate_Type"].ToString());
                    obj.MeterReadDt = StringConvert.ToDateTime(dr["Meter_Date"].ToString(), provider);
                    obj.ReadUnit = StringConvert.ToDecimal(dr["Meter_Unit"].ToString());
                    obj.LastUnit = StringConvert.ToDecimal(dr["Meter_Unit_Last"].ToString());
                    obj.BaseAmount = StringConvert.ToDecimal(dr["Amt_Base"].ToString());
                    obj.FTUnitPrice = StringConvert.ToDecimal(dr["Ft_Unit"].ToString());
                    obj.FTAmount = StringConvert.ToDecimal(dr["Ft_Amt"].ToString());
                    obj.UnitPrice = StringConvert.ToDecimal(dr["Unit_Price"].ToString());
                    obj.Qty = StringConvert.ToDecimal(dr["Quantity"].ToString());
                    obj.Amount = StringConvert.ToDecimal(dr["Amt_Tax_Base"].ToString());
                    obj.UnitTypeId = StringConvert.ToString(dr["Uom"].ToString());
                    obj.TaxCode = StringConvert.ToString(dr["Tax_Code"].ToString());
                    obj.VatAmount = StringConvert.ToDecimal(dr["Amt_Tax"].ToString());
                    obj.GAmount = StringConvert.ToDecimal(dr["Amt"].ToString());
                    obj.DueDt = StringConvert.ToDateTime(dr["Due_Date_Payment"].ToString(), provider);
                    obj.DueDt2 = StringConvert.ToDateTime(dr["Deferral"].ToString(), provider);
                    obj.InterestLockFlag = StringConvert.ToString(dr["Int_Lock_Reason"].ToString());
                    obj.InterestKey = StringConvert.ToString(dr["Interest_Key"].ToString());
                    obj.DisconnectDt = StringConvert.ToDateTime(dr["Date_Issue"].ToString(), provider);
                    obj.DisconnectDoc = StringConvert.ToString(dr["Doc_Disconnect"].ToString());
                    obj.SubstDocNo = StringConvert.ToString(dr["Substitude_No"].ToString());
                    obj.InstallmentPeriod = StringConvert.ToInt32(dr["Repetition_Item"].ToString());
                    obj.InstallmentTotalPeriod = StringConvert.ToInt32(dr["No_Installment"].ToString());
                    obj.SpotBillInvoiceNo = StringConvert.ToString(dr["Device_Value"].ToString());
                    obj.CancelFlag = StringConvert.ToString(dr["Item_Cancled"].ToString());
                    obj.PaymentOrderFlag = StringConvert.ToString(dr["Item_Included"].ToString());
                    obj.PaymentOrderDt = StringConvert.ToDateTime(dr["Date_Payment"].ToString(), provider);
                    obj.ModifiedFlag = StringConvert.ToString(dr["Dunn_Lock_Reason"].ToString());
                    obj.Action = dr["Action"].ToString();
                    obj.SyncFlag = "0";
                    obj.PostDt = DateTime.Now;
                    obj.PostBranchServerId = "";
                    obj.ModifiedDt = DateTime.Now;
                    obj.ModifiedBy = "RFC_CALL";

                    string Id = dr["Id"].ToString();
                    if (Id == "TRR0010A")
                        obj.FileType = "A";
                    else if (Id == "TRR0010B")
                        obj.FileType = "B";
                    else if (Id == "TRR0010C")
                        obj.FileType = "C";
                    else if (Id == "TRR0010D")
                        obj.FileType = "D";
                    else if (Id == "TRR0010E")
                        obj.FileType = "E";
                    else if (Id == "TRR0010F")
                        obj.FileType = "F";

                    list.Add(obj);
                }
            }
            return list;
        }

        public List<PayFromSAPInfo> ToPayFromSAP(DataTable z20)
        {
            IFormatProvider provider = CultureInfo.CreateSpecificCulture("en-EN");
            List<PayFromSAPInfo> list = new List<PayFromSAPInfo>();
            foreach (DataRow dr in z20.Rows)
            {
                PayFromSAPInfo obj = new PayFromSAPInfo();
                obj.PaymentDocNo = StringConvert.ToString(dr["Payment_Doc"].ToString());
                obj.ReceiptNo = StringConvert.ToString(dr["Receipt_No"].ToString());
                obj.InvoiceNo = StringConvert.ToString(dr["Invoice_No"].ToString());
                obj.CaDoc = StringConvert.ToString(dr["Ca_Doc"].ToString());
                obj.DocType = StringConvert.ToString(dr["Doc_Type"].ToString());
                obj.PaymentDt = StringConvert.ToDateTime(dr["Date_Payment"].ToString(), provider);
                obj.PmId = StringConvert.ToString(dr["Payment_Id"].ToString());
                obj.VatAmount = StringConvert.ToDecimal(dr["Tax_Amt"].ToString());
                obj.Amount = StringConvert.ToDecimal(dr["Amt"].ToString());
                obj.CancelFlag = StringConvert.ToString(dr["Flag"].ToString());
                obj.DueDt = StringConvert.ToDateTime(dr["Inv_Duedate"].ToString(), provider);
                obj.SyncFlag = "0";
                obj.ModifiedBy = "RFC_CALL";
                obj.ModifiedDt = DateTime.Now;
                obj.Action = dr["Action"].ToString();
                list.Add(obj);
            }

            if (list.Count > 0)
            {
                list.Sort(new ObjectComparer<PayFromSAPInfo>("PaymentDocNo ASC, CaDoc ASC, InvoiceNo ASC, DueDt ASC", true));

                for (int i = 0; i < list.Count; i++)
                {
                    decimal? totalAmount = 0;
                    List<PayFromSAPInfo> ps = list.FindAll(delegate(PayFromSAPInfo p) { return p.PaymentDocNo == list[i].PaymentDocNo; });

                    if (ps.Count > 0)
                    {
                        foreach (PayFromSAPInfo p in ps)
                        {
                            totalAmount += p.Amount;
                        }
                    }

                    if (i == 0)
                    {
                        list[i].TotalAmount = totalAmount;
                        list[i].ARPmId = string.Format("ZX{0}{1}", list[i].PaymentDocNo, "00000001");
                        list[i].ARPtId = string.Format("ZX{0}{1}", list[i].PaymentDocNo, "00000001");
                        list[i].ReceiptId = string.Format("ZX{0}{1}", list[i].PaymentDocNo, "01");
                    }
                    else
                    {
                        if (list[i].PaymentDocNo == list[i - 1].PaymentDocNo)
                        {
                            list[i].TotalAmount = totalAmount;
                            list[i].ARPmId = string.Format("ZX{0}{1}", list[i].PaymentDocNo, (Convert.ToInt32(list[i - 1].ARPmId.Substring(14, 8)) + 1).ToString().PadLeft(8, '0'));
                            list[i].ARPtId = list[i - 1].ARPtId;
                            list[i].ReceiptId = string.Format("ZX{0}{1}", list[i].PaymentDocNo, (Convert.ToInt32(list[i - 1].ReceiptId.Substring(14, 2)) + 1).ToString().PadLeft(2, '0'));
                        }
                        else
                        {
                            list[i].TotalAmount = totalAmount;
                            list[i].ARPmId = string.Format("ZX{0}{1}", list[i].PaymentDocNo, "00000001");
                            list[i].ARPtId = string.Format("ZX{0}{1}", list[i].PaymentDocNo, "00000001");
                            list[i].ReceiptId = string.Format("ZX{0}{1}", list[i].PaymentDocNo, "01");
                        }
                    }
                }
            }

            return list;
        }

        public List<DisconnectionDoc> ToDisconnectionDoc(DataTable z40)
        {
            IFormatProvider provider = CultureInfo.CreateSpecificCulture("en-EN");
            List<DisconnectionDoc> list = new List<DisconnectionDoc>();
            foreach (DataRow dr in z40.Rows)
            {
                DisconnectionDoc obj = new DisconnectionDoc();
                obj.DiscNo = StringConvert.ToString(dr["Discno"].ToString());
                obj.CreatedDt = StringConvert.ToDateTime(dr["Cre_Date"].ToString(), provider);
                obj.DiscStatusId = StringConvert.ToString(dr["Discno_Status"].ToString());
                obj.ReleaseDt = StringConvert.ToDateTime(dr["Release_Dt"].ToString(), provider);
                obj.WorkOrderNo = StringConvert.ToString(dr["Work_No"].ToString());
                obj.DiscPlanStart = StringConvert.ToDateTime(dr["Disc_Plan_Start"].ToString(), provider);
                obj.DiscPlanEnd = StringConvert.ToDateTime(dr["Disc_Plan_End"].ToString(), provider);
                obj.WorkCenter = StringConvert.ToString(dr["Work_Center"].ToString());
                obj.PostpConfirm = StringConvert.ToDateTime(dr["Postpone_Dt"].ToString(), provider);
                obj.FuseRemConfirm = StringConvert.ToDateTime(dr["Fuseremv_Dt"].ToString(), provider);
                obj.MeterRemConfirm = StringConvert.ToDateTime(dr["Meterremv_Dt"].ToString(), provider);
                obj.ModifiedBy = "RFC_CALL";
                obj.ModifiedDt = DateTime.Now;
                obj.Action = dr["Action"].ToString();
                list.Add(obj);
            }
            return list;
        }


        public List<RTDisconnectionDocCaDoc> ToRTDisconnectionDocCaDoc(DataTable z45)
        {
            IFormatProvider provider = CultureInfo.CreateSpecificCulture("en-EN");
            List<RTDisconnectionDocCaDoc> list = new List<RTDisconnectionDocCaDoc>();
            foreach (DataRow dr in z45.Rows)
            {
                RTDisconnectionDocCaDoc obj = new RTDisconnectionDocCaDoc();
                obj.DiscNo = StringConvert.ToString(dr["Discno"].ToString());
                obj.CaDocNo = StringConvert.ToString(dr["Ca_Doc"].ToString());
                obj.CancelFlag = StringConvert.ToString(dr["Cancel"].ToString());
                obj.ModifiedBy = "RFC_CALL";
                obj.ModifiedDt = DateTime.Now;
                obj.Action = dr["Action"].ToString();
                list.Add(obj);
            }
            return list;
        }

        public bool CheckExistingInvoiceNo(string caId, string period)
        {
            try
            {
                BillingDA da = new BillingDA();
                return da.CheckExistingInvoiceNo(caId, period);
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "CheckExistingInvoiceNo", ex.ToString());
                throw;
            }
        }


        public LastDisconnect GetLastDisconnect(string caId)
        {
            try
            {
                BillingDA da = new BillingDA();
                LastDisconnect aLastDisconnect = da.GetLastDisconnect(caId);

                return aLastDisconnect;
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "GetLastDisconnect", ex.ToString());
                throw;
            }
        }


        public List<PaymentNonReceiptInfo> SearchPaymentNonReceipt(PaymentNonReceiptInfo receiptGen)
        {
            try
            {
                BillingDA da = new BillingDA();
                List<PaymentNonReceiptInfo> paymentList = da.SearchPaymentNonReceipt(receiptGen);
                return paymentList;
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "SearchPaymentNonReceipt", ex.ToString());
                throw;
            }
        }


        public void CreateReceiptService(List<PaymentNonReceiptInfo> paymentGenReList)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    BillingDA da = new BillingDA();
                    foreach (PaymentNonReceiptInfo paymentGenReceipt in paymentGenReList)
                    {
                        paymentGenReceipt.PostBranchId = Session.Branch.PostedServerId;
                        if (paymentGenReceipt.PmId.ToUpper() == "C")
                        {
                            da.CreateReceiptData(trans, paymentGenReceipt);
                        }
                        else
                        {
                            da.CreateGroupInvoiceReceiptData(trans, paymentGenReceipt);
                        }
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Logger.WriteError(Logger.Module.POS, "CreateReceiptService", ex.ToString());
                    throw;
                }
            }
        }

        public List<CaAndBpInfo> GetCaAndBpOtherBranch(string caId)
        {
            try
            {
                BillingDA da = new BillingDA();
                List<CaAndBpInfo> list = new List<CaAndBpInfo>();
                CaAndBpInfo obj = new CaAndBpInfo();
                obj.BpObj = da.GetBpOtherBranch(caId);
                obj.CaObj = da.GetCaOtherBranch(caId);
                list.Add(obj);      

                return list;
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "GetCaAndBpOtherBranch", ex.ToString());
                throw;
            }
        }

        public void SaveCaAndBpOtherBranch(List<CaAndBpInfo> list)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbTransaction trans;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                try
                {
                    BillingDA da = new BillingDA();
                    da.SaveBpAndCaOtherBranch(list, trans);
                    //da.SaveCaOtherBranch(list[0].CaObj, trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Logger.WriteError(Logger.Module.POS, "SaveCaAndBpOtherBranch", ex.ToString());
                    throw;
                }
            }
        }

        public bool GetPaidGAmount(string invoiceNo)
        {
            try
            {
                BillingDA da = new BillingDA();
                return da.GetPaidGAmount(invoiceNo);
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "GetPaidGAmount", ex.ToString());
                throw;
            }
        }

        public bool GetInActiveBillBook(string invoiceNo)
        {
            try
            {
                BillingDA da = new BillingDA();
                return da.GetInActiveBillBook(invoiceNo);
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "CheckInActiveBillBook", ex.ToString());
                throw;
            }
        }

        public bool GetDuplicateExtReceipt(string receiptId, string branchId)
        {
            try
            {
                BillingDA da = new BillingDA();
                return da.GetDuplicateExtReceipt(receiptId, branchId);
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "GetDuplicateExtReceipt", ex.ToString());
                throw;
            }
        }

        public bool ValidatePaymentActive(string receiptId, bool isPayment)
        {
            try
            {
                BillingDA da = new BillingDA();
                return da.ValidatePaymentActive(receiptId, isPayment);
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "ValidatePaymentActive", ex.ToString());
                throw;
            }
        }

        public void OfflineLog(OfflineLogInfo logInfo)
        {
            BillingDA da = new BillingDA();
            da.OfflineLog(logInfo);
        }

        #endregion
        
        #region IBillingService Members


        public string CheckWorkStatus(OpenWorkParam param)
        {
            try
            {
                BillingDA da = new BillingDA();
                param.PostedBranchId = Session.Branch.PostedServerId;
                return da.CheckWorkStatus(param);
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "CheckWorkStatus", ex.ToString());
                throw;
            }
        }

        //// รวมใบเสร็จแผนผ่อน 2021-10-07 Check ใบเสร็จรวม Enable Status from ta.AppSetting
        public string CheckSettingGroupReceiptEnableStatus()
        {
            try
            {
                BillingDA da = new BillingDA();               
                return da.CheckSettingGroupReceiptEnableStatus();
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "CheckSettingGroupReceiptEnableStatus", ex.ToString());
                throw;
            }
        }


        #endregion

        

        #region Check money in tray

        public TrayMoneyInfo GetMoneyInTray(string workId)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    TrayMoneyInfo trayMoneyInfo = new TrayMoneyInfo();
                    BillingDA da = new BillingDA();
                    trayMoneyInfo = da.GetMoneyInTray(trans, workId);

                    trans.Commit();
                    return trayMoneyInfo;
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        #endregion


        #region OneTouch
        public List<Invoice> SearchInvoiceFromOneTouch(OneTouchSearchParam param)
        {
            //ไม่ได้ส่วนนี้  ใช้ SG ดึง Web Service ของ OneTouch
            List<Invoice> invoices = GetDataFromOneTouch(param);
            return invoices;

        }

        private List<Invoice> GetDataFromOneTouch(OneTouchSearchParam param)
        {
            
                List<OneTouchInfo> OneTouchData = new List<OneTouchInfo>();
                List<Invoice> invoices = new List<Invoice>();

                //ไม่ได้ส่วนนี้  ใช้ SG ดึง Web Service ของ OneTouch
                return invoices;


        }
        
        public bool FlagOneTouchPayment(OneTouchLogInfo param)
        {

            //ไม่ได้ส่วนนี้  ใช้ SG ดึง Web Service ของ OneTouch
            return false;
        }

        public void SaveOneTouchLog(OneTouchLogInfo log)
        {
            
                BillingDA da = new BillingDA();
                da.SaveOneTouchLog(log);
            
        }
        
        #endregion OneTouchf
 
        #region Offline Payment
        public List<OfflinePayment> GetOfflinePayment(string branchId, string cashierId, string workId)
        {

            try
            {
                BillingDA da = new BillingDA();
            
                List<OfflinePayment> OfflinePayment = da.GetOfflinePayment(branchId, cashierId, workId);

                return OfflinePayment;
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "OfflinePayment", ex.ToString());
                throw;
            }


        }

        public void UpdateOfflinePayment(string branchId, string cashierId, string posId, string workId)
        {
            try
            {
                BillingDA da = new BillingDA();
                da.UpdateOfflinePayment(branchId, cashierId, posId, workId);
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "UpdateOfflinePayment", ex.ToString());
                throw;
            }
        }

        

        #endregion Offline Payment

        #region IBillingService Members


        public QRPaymentResponse CheckStatusQRPayment(QRPaymentInfo qrpaymentInfo)
        {
            // Implement ตามเงือนไขของ interface , Code ทำงานที่ Billing Service
            return new QRPaymentResponse();
        }

        public QRRefundResponse QRPaymentRefund(QRPaymentInfo qrpaymentInfo)
        {
            // Implement ตามเงือนไขของ interface , Code ทำงานที่ Billing Service
            return new QRRefundResponse();
        }

        /// <summary>
        /// สอบถาม
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public string QRPaymentMethodByBranchId(string branchId)
        {
            try
            {
                BillingDA da = new BillingDA();
                return da.QRPaymentMethodByBranchId(branchId);
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "Get QRPayment status by branchid fail", ex.ToString());
                return "0";
            }
        }


        public QRRefundResponse QRPostOfflinePayment(QRPaymentInfo qrpaymentInfo)
        {
            // Implement ตามเงือนไขของ interface , Code ทำงานที่ Billing Service
            return new QRRefundResponse();
        }

        #endregion
        
    }
}

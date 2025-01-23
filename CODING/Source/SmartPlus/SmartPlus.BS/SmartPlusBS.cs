using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.SmartPlus.Interface.Services;
using PEA.BPM.SmartPlus.Interface.BusinessEntities;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PEA.BPM.SmartPlus.DA;
using System.Web.Security;
using System.Threading;
using System.Linq;
using PEA.BPM.PaymentCollectionModule.DA;
using System.Data;
using PEA.BPM.PaymentCollectionModule.BS;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using System.Globalization;
using AgencyEntities = PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using AgencyDA = PEA.BPM.AgencyManagementModule.DA;
using PEA.BPM.SmartPlus.SG;
using System.Configuration;

namespace PEA.BPM.SmartPlus.BS
{
    public class SmartPlusBS 
    {   
        public SmartPlusBS()
        {
            var svc     = new SmartPlusDA();
        }

        #region ..GET WEB.CONFIG         
        public bool RequestLogAndFilterPerform(string serviceName, string caId)
        {
            //SMARTPLUS_REQUEST_FILTER -> YES = Filter incoming request, NO = Not filter incoming 
            //SMARTPLUS_REQUEST_LOG    -> YES = Keep every request before get AR or Update mark flag, NO = Not keep anything
            //SMARTPLUS_DELAY_TIME     -> 10  (unit in Milisecond)
            bool Result = true;
            try
            {
                string SMARTPLUS_REQUEST_FILTER = ConfigurationManager.AppSettings["SMARTPLUS_REQUEST_FILTER"];                
                int    SMARTPLUS_DELAY_TIME     = Convert.ToInt32(ConfigurationManager.AppSettings["SMARTPLUS_DELAY_TIME"]);

                
                if (SMARTPLUS_REQUEST_FILTER == "YES")
                {                    
                    SmartPlusDA sda = new SmartPlusDA();
                    Result          = sda.SmartplusLogAndFilter(serviceName, caId, SMARTPLUS_DELAY_TIME);
                }
                else
                {
                    Result = true;
                }
            }
            catch (Exception ex)
            {
                Result = true;
            }

            return Result;
        }


        #endregion


        #region ..SMARTPLUS V2 2022-05-23 (SEPERATE SERVICE FROM MAIN BPM)

        public List<ARInformationInfo> SearchContractorServiceV2(string CaId, string BillFlag, DateTime InterestDate)
        {
            List<ARInformationInfo> arInformationInfoList = new List<ARInformationInfo>();
            CustomerSearchParam customerSearchParam = new CustomerSearchParam();
            List<PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Invoice> invoiceList = new List<PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Invoice>();
            SmartPlusBS     smartPlusBs     = new SmartPlusBS();
            BPMBillingSG    bpmBillingSg    = new BPMBillingSG();
            SmartPlusDA     sda             = new SmartPlusDA();
            try
            {
                List<Invoice> invoices          = sda.SearchInvoiceByCustomerId(CaId, true);
                List<Invoice> invWithInterest   = CheckInterestByInvoice(invoices, InterestDate, true);
                arInformationInfoList           = InterestPendingPerform(CaId, invWithInterest, BillFlag);                
            }
            catch (Exception ex)
            {
                arInformationInfoList = null;
            }
            return arInformationInfoList;
        }

        /// 2022-05-30
        /// <summary>
        /// Smarplus from V1 to V2 use this function
        /// </summary>
        /// <param name="CaId"></param>
        /// <param name="BillFlag"></param>
        /// <returns></returns>
        public List<ARInformationInfo> SearchContractorServiceV2(string CaId, string BillFlag)
        {
            List<ARInformationInfo> arInformationInfoList   = new List<ARInformationInfo>();
            CustomerSearchParam customerSearchParam         = new CustomerSearchParam();
            List<PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Invoice> invoiceList = new List<PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Invoice>();
            SmartPlusBS smartPlusBs     = new SmartPlusBS();
            BPMBillingSG bpmBillingSg   = new BPMBillingSG();
            SmartPlusDA sda             = new SmartPlusDA();
            try
            {
                List<Invoice> invoices  = sda.SearchInvoiceByCustomerId(CaId, true);
                arInformationInfoList   = InterestPendingPerform(CaId, invoices, BillFlag);
            }
            catch (Exception ex)
            {
                arInformationInfoList = null;
            }
            return arInformationInfoList;
        }        

        #endregion

        #region ..SMARTPLUS V1

        //(SRS)-0026
        public ContractorAccountDetailModel SearchConAccountService(string CaId)
        {
            var detail = new ContractorAccountDetailModel();
            try
            {
                 SmartPlusDA service = new SmartPlusDA();
                 detail = service.SearchContractorInformation(CaId);
                 return detail;
            }
            catch (Exception)
            {                
                throw;
            }            
        }

        public List<ARInformationInfo> SearchContractorService(string CaId, string BillFlag)
        {
            DateTime? InterestDate = null;
            List<ARInformationInfo> arInformationInfoList = new List<ARInformationInfo>();
            CustomerSearchParam customerSearchParam = new CustomerSearchParam();
            List<PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Invoice> invoiceList = new List<PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Invoice>();
            SmartPlusBS smartPlusBs = new SmartPlusBS();
            BPMBillingSG bpmBillingSg = new BPMBillingSG();
            try
            {
                List<PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Invoice> posInfo = bpmBillingSg.GetPOSInfoWithInterestDate(CaId, "Z00000", InterestDate);
                arInformationInfoList = this.InterestPendingPerform(CaId, posInfo, BillFlag);
                //// ISSUE#INTEREST DATE 2021-06-08
                /*
                DateTime currentDate = DateTime.Now.Date;
                if (InterestDate != null && InterestDate >= currentDate)
                {
                    bool IS_ADDED               = true;
                    List<Invoice> listInvoice   = CheckInterestByInvoice(posInfo, InterestDate, IS_ADDED);
                    arInformationInfoList       = this.InterestPendingPerform(CaId, posInfo, BillFlag);
                }
                else
                {                   
                    arInformationInfoList = this.InterestPendingPerform(CaId, posInfo, BillFlag);
                }
                */
            }
            catch (Exception ex)
            {
                arInformationInfoList = null;
            }
            return arInformationInfoList;
        }

        public List<ARInformationInfo> SearchContractorService(string CaId, string BillFlag,DateTime? InterestDate)
        {
            List<ARInformationInfo> arInformationInfoList   = new List<ARInformationInfo>();
            CustomerSearchParam customerSearchParam         = new CustomerSearchParam();
            List<PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Invoice> invoiceList = new List<PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Invoice>();
            SmartPlusBS smartPlusBs     = new SmartPlusBS();
            BPMBillingSG bpmBillingSg   = new BPMBillingSG();
            try
            {
                List<PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Invoice> posInfo = bpmBillingSg.GetPOSInfoWithInterestDate(CaId, "Z00000", InterestDate);
                arInformationInfoList = this.InterestPendingPerform(CaId, posInfo, BillFlag);
                //// ISSUE#INTEREST DATE 2021-06-08
                /*
                DateTime currentDate = DateTime.Now.Date;
                if (InterestDate != null && InterestDate >= currentDate)
                {
                    bool IS_ADDED               = true;
                    List<Invoice> listInvoice   = CheckInterestByInvoice(posInfo, InterestDate, IS_ADDED);
                    arInformationInfoList       = this.InterestPendingPerform(CaId, posInfo, BillFlag);
                }
                else
                {                   
                    arInformationInfoList = this.InterestPendingPerform(CaId, posInfo, BillFlag);
                }
                */
            }
            catch (Exception ex)
            {
                arInformationInfoList = null;
            }
            return arInformationInfoList;
        }        

        //(SRS)-0026
        public List<ARInformationInfo> SearchContractorService(string CaId)
        {
            var arInfo      = new List<ARInformationInfo>();
            var model       = new CustomerSearchParam();
            var listInvoice = new List<Invoice>();
            var PaymentBS   = new SmartPlusBS();
            var sg          = new BPMBillingSG();
            try
            {
                #region .. 1ST TO GET AR INFORMATION
                listInvoice     = sg.GetPOSInfo(CaId, "Z00000");
                foreach (var r in listInvoice)
                {
                    if (r.Qty == null)
                    {
                        r.Qty = r.ToPayQty;
                    }

                    if (r.OriginalInvoiceNo == null)
                    {
                        r.OriginalInvoiceNo = r.InvoiceNo;
                    }
                }
                arInfo = InterestPendingPerform(CaId, listInvoice);
                #endregion
            }
            catch (Exception ex)
            {
                string errorText = ex.Message;
                string authToken = null;
                if (errorText.Contains("TokenExpired") == true)
                {
                    authToken = sg.GetAuthenToken();
                }

                #region .. 2ND TRY TO GET AR INFORMATION
                try
                {
                    listInvoice     = sg.GetPOSInfo(CaId, "Z00000");
                    foreach (var r in listInvoice)
                    {
                        if (r.Qty == null)
                        {
                            r.Qty = r.ToPayQty;
                        }

                        if (r.OriginalInvoiceNo == null)
                        {
                            r.OriginalInvoiceNo = r.InvoiceNo;
                        }
                    }
                    arInfo = InterestPendingPerform(CaId, listInvoice);
                }
                catch (Exception)
                {                    
                    throw;
                }
                #endregion
            }

            return arInfo;
        }

        //(SRS)-0026
        public string UpdateBillMarkFlagService(string CaId, string InvoiceNo,string wsKey)
        {
            string updateResult = null;
            try
            {
                SmartPlusDA da  = new SmartPlusDA();
                updateResult = da.UpdateBillMarkFlagData(CaId, InvoiceNo, wsKey); 
            }
            catch
            {
                throw;
            }

            return updateResult;
        }

        //(SRS)-0026
        public string CancelPaymentService(string CaId, string InvoiceNo, string wsKey)
        {
            string cancelResult = null;
            try
            {
                SmartPlusDA da  = new SmartPlusDA();
                cancelResult = da.CancelPayment(CaId, InvoiceNo, wsKey);                
            }
            catch
            {
                throw;
            }

            return cancelResult;
        }
        
        public List<ARInformationInfo> SearchARInformationFollowingBPMLogic(string CaId, string BillFlag)
        {
            List<ARInformationInfo> arInfo          = new List<ARInformationInfo>();
            CustomerSearchParam     model           = new CustomerSearchParam();
            List<Invoice>           listInvoice     = new List<Invoice>();
            model.CaId              = CaId;
            model.IsSearByBP        = true;
            
            try
            {
               var PaymentBS    = new SmartPlusBS();
               listInvoice      = PaymentBS.SearchInvoiceByCustomerId(model);
               foreach (var r in listInvoice)
               {
                   if (r.Qty == null)
                   {
                       r.Qty = r.ToPayQty;
                       
                   }
                   //if (r.OriginalInvoiceNo == null)
                   //{
                   //    r.OriginalInvoiceNo = r.InvoiceNo;
                   //}
               }
               arInfo           = InterestPendingPerform(CaId,listInvoice);               
            }
            catch (Exception)
            {
                throw;
            }
            return arInfo;
        }

        private List<ARInformationInfo> InterestPendingPerform(
            string CaId,
            List<PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Invoice> listInvoice,
            string BillFlag,
            DateTime InterestDate)
        {
            List<InvoiceReferenceModel> invoiceReferenceModelList = new List<InvoiceReferenceModel>();
            foreach (PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Invoice invoice in listInvoice)
            {
                InvoiceReferenceModel invoiceReferenceModel = new InvoiceReferenceModel();
                if (invoice.InvoiceNo != null)
                {
                    invoiceReferenceModel.ModelOriginalInvoiceNo = invoice.OriginalInvoiceNo;
                    invoiceReferenceModel.ModelRefInvoiceNo      = invoice.InvoiceNo;
                    invoiceReferenceModelList.Add(invoiceReferenceModel);
                }
            }
            string wsTransactionId  = Guid.NewGuid().ToString();
            SmartPlusDA smartPlusDa = new SmartPlusDA();
            string str = (string)null;
            int num1   = 0;
            foreach (PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Invoice invoice in listInvoice)
            {
                InterestPendingModel model = new InterestPendingModel();
                if (invoice.InvoiceNo != null)
                    str = invoice.InvoiceNo;
                model.Period = invoice.Period;
                if (invoice.Period == null)
                {
                    ++num1;
                    model.Period = "000000";
                    //model.Period = "งวดที่ " + invoice.InstallmentPeriod.ToString() + @"/" + invoice.InstallmentTotalPeriod.ToString();//"งวดที่"+InstallmentPeriod+"/"+Installment
                }
                    
                model.InvoiceNo     = invoice.InvoiceNo;
                model.Description   = "";
                
                model.RefInvoiceNo  = str;
                model.Qty           = new Decimal?(0M);
                model.GAmount       = invoice.GAmount;
                if (invoice.InvoiceNo != null)
                {
                    foreach (Bill bill in invoice.Bills)
                    {
                        if (!bill.FtAmount.HasValue)
                            model.FTAmount = new Decimal?(0M);
                    }
                }
                else
                {
                    foreach (Bill bill in invoice.Bills)
                    {
                        model.Description = bill.Description;
                        model.Period = invoice.Period;
                        if (invoice.Period == null)
                        {
                            model.Period = "000000";
                            //model.Period = "งวดที่ " + invoice.InstallmentPeriod.ToString() + @"/" + invoice.InstallmentTotalPeriod.ToString();//"งวดที่"+InstallmentPeriod+"/"+Installment
                        }
                        model.FTAmount  = new Decimal?(0M);
                        model.Qty       = invoice.ToPayQty;
                        model.GAmount   = invoice.ToPayGAmount;
                    }
                }


                model.CaId          = invoice.CaId;
                model.UnitTypeId    = invoice.DebtType;
                model.Amount        = invoice.AmountExVat;
                model.TaxCode       = "";
                model.VatAmount     = invoice.VatAmount;
                model.POSDebtFlag   = "";
                model.ModifiedBy    = "SMP";
                model.WebServiceCallGroupId     = wsTransactionId;
                model.PostBranchServerId        = invoice.BranchId;
                model.OriginalInvoiceNo         = invoice.OriginalInvoiceNo;
                if (invoice.OriginalInvoiceNo == null)
                    model.OriginalInvoiceNo = "";

                model.InstallmentPeriodText = null;
                if (!String.IsNullOrEmpty(invoice.InstallmentPeriod.ToString()) && !String.IsNullOrEmpty(invoice.InstallmentTotalPeriod.ToString()))
                {                    
                    model.InstallmentPeriodText = "งวดที่ " + invoice.InstallmentPeriod.ToString() + @"/" + invoice.InstallmentTotalPeriod.ToString();//"งวดที่"+InstallmentPeriod+"/"+Installment
                }
                    
                int num2 = 0;
                num2     = smartPlusDa.InsertInterestPending(model);
            }
            List<ARInformationInfo> arInformationInfoList1 = new List<ARInformationInfo>();
            List<ARInformationInfo> arInformationInfoList2;
            try
            {
                arInformationInfoList2 = smartPlusDa.SearchSmartPlusCorpBillInformation(CaId, BillFlag, wsTransactionId);
                foreach (ARInformationInfo arInformationInfo in arInformationInfoList2)
                {
                    if (arInformationInfo.MainSub == "M00400010" || arInformationInfo.MainSub == "00400010")
                        arInformationInfo.CaDoc = "";
                }
            }
            catch (Exception ex)
            {
                arInformationInfoList2 = (List<ARInformationInfo>)null;
            }
            return arInformationInfoList2;
        }


        private List<ARInformationInfo> InterestPendingPerform(
            string CaId,
            List<PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Invoice> listInvoice,
            string BillFlag)
        {
            List<InvoiceReferenceModel> invoiceReferenceModelList = new List<InvoiceReferenceModel>();
            foreach (PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Invoice invoice in listInvoice)
            {
                InvoiceReferenceModel invoiceReferenceModel = new InvoiceReferenceModel();
                if (invoice.InvoiceNo != null)
                {
                    invoiceReferenceModel.ModelOriginalInvoiceNo = invoice.OriginalInvoiceNo;
                    invoiceReferenceModel.ModelRefInvoiceNo      = invoice.InvoiceNo;
                    invoiceReferenceModelList.Add(invoiceReferenceModel);
                }
            }
            string wsTransactionId  = Guid.NewGuid().ToString();
            SmartPlusDA smartPlusDa = new SmartPlusDA();
            string str              = (string)null;
            int num1                = 0;
            foreach (PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Invoice invoice in listInvoice)
            {
                InterestPendingModel model = new InterestPendingModel();
                if (invoice.InvoiceNo != null)
                    str = invoice.InvoiceNo;
                if (invoice.Period == null)
                    ++num1;
                model.InvoiceNo     = invoice.InvoiceNo;
                model.Description   = "";
                model.Period        = invoice.Period;
                if (invoice.Period == null)
                {
                    model.Period = "000000";
                }
                
                model.RefInvoiceNo  = str;
                model.Qty           = 0;
                model.GAmount       = invoice.GAmount;
                if (invoice.InvoiceNo != null)
                {
                    foreach (Bill bill in invoice.Bills)
                    {
                        if (!bill.FtAmount.HasValue)
                            model.FTAmount = 0;
                    }
                }
                else
                {
                    foreach (Bill bill in invoice.Bills)
                    {
                        model.Description = bill.Description;
                        model.Period      = invoice.Period;
                        if (invoice.Period == null)
                        {
                            model.Period = "000000";                            
                        }
                        model.FTAmount  = 0;
                        model.Qty       = invoice.ToPayQty;
                        model.GAmount   = invoice.ToPayGAmount;
                    }
                }

                

                model.CaId          = invoice.CaId;
                model.UnitTypeId    = invoice.DebtType;
                model.Amount        = invoice.AmountExVat;
                model.TaxCode       = "";
                model.VatAmount     = invoice.VatAmount;
                model.POSDebtFlag   = "";
                model.ModifiedBy    = "SMP";
                model.WebServiceCallGroupId     = wsTransactionId;
                model.PostBranchServerId        = invoice.BranchId;
                model.OriginalInvoiceNo         = invoice.OriginalInvoiceNo;
                if (invoice.OriginalInvoiceNo == null)
                    model.OriginalInvoiceNo = "";

                model.InstallmentPeriodText = null;
                if (!String.IsNullOrEmpty(invoice.InstallmentPeriod.ToString()) && !String.IsNullOrEmpty(invoice.InstallmentTotalPeriod.ToString()))
                {
                    model.InstallmentPeriodText = "งวดที่ " + invoice.InstallmentPeriod.ToString() + @"/" + invoice.InstallmentTotalPeriod.ToString();//"งวดที่"+InstallmentPeriod+"/"+Installment
                }
                int num2 = 0;
                num2 = smartPlusDa.InsertInterestPending(model);
            }
            //List<ARInformationInfo> arInformationInfoList1 = new List<ARInformationInfo>();
            List<ARInformationInfo> arInformationInfoList2;
            try
            {
                arInformationInfoList2   = smartPlusDa.SearchSmartPlusCorpBillInformation(CaId, BillFlag, wsTransactionId);
                foreach (ARInformationInfo arInformationInfo in arInformationInfoList2)
                {
                    if (arInformationInfo.MainSub == "M00400010" || arInformationInfo.MainSub == "00400010")
                        arInformationInfo.CaDoc = "";
                }
            }
            catch (Exception ex)
            {
                arInformationInfoList2 = (List<ARInformationInfo>)null;
            }
            return arInformationInfoList2;
        }
       
        private List<ARInformationInfo> InterestPendingPerform(string CaId, List<Invoice> listInvoice)
        {
            //+------------------------------------------------------------------+
            //|Assign Value for Reference InvoiceNo == OrginalInvoiceNo          |
            //+------------------------------------------------------------------+
            var listRef = new List<InvoiceReferenceModel>();
            foreach (var r in listInvoice)
            {
                var model       = new InvoiceReferenceModel();
                if (r.InvoiceNo != null)
                {
                    model.ModelOriginalInvoiceNo = r.OriginalInvoiceNo;
                    model.ModelRefInvoiceNo      = r.InvoiceNo;
                    listRef.Add(model);
                }
            }

            //+------------------------------------------------------------------+
            //| Get dynamic InvoiceNo from Interest pending table                |
            //+------------------------------------------------------------------+
            

            #region ..INSERT INTEREST PENDING

            string WSCallGroupId        = Guid.NewGuid().ToString();
            var    da                   = new SmartPlusDA();
            foreach (var r in listInvoice)
            {
                var m = new InterestPendingModel();

                foreach (var inv in listRef)
                {
                    if (inv.ModelRefInvoiceNo != null)
                    {
                        m.RefInvoiceNo = inv.ModelRefInvoiceNo;
                    }                    
                }

                m.Description = "";
                if(r.InvoiceNo == null)
                {
                    foreach(var b in r.Bills)
                    {
                        m.Description = b.Description;
                    }
                }
                
                m.CaId                  = r.CaId;                
                m.Qty                   = r.Qty;
                m.UnitTypeId            = r.DebtType;
                m.Amount                = r.AmountExVat;
                m.TaxCode               = "";
                m.VatAmount             = r.VatAmount;
                m.GAmount               = r.GAmount;
                m.POSDebtFlag           = "";
                m.ModifiedBy            = "SMP";
                m.InvoiceNo             = r.InvoiceNo;
                //m.RefInvoiceNo          = r.OriginalInvoiceNo;
                m.WebServiceCallGroupId = WSCallGroupId;
                m.PostBranchServerId    = r.BranchId;
                m.OriginalInvoiceNo     = r.OriginalInvoiceNo;

                if (!String.IsNullOrEmpty(r.InstallmentPeriod.ToString()) && !String.IsNullOrEmpty(r.InstallmentTotalPeriod.ToString()))
                {
                    m.InstallmentPeriodText = "งวดที่ " + r.InstallmentPeriod.ToString() + @"/" + r.InstallmentTotalPeriod.ToString();
                }
                
                var intRecords          = 0;
                intRecords              = da.InsertInterestPending(m);
            }
            #endregion

            //+------------------------------------------------------------------+
            //| Result                                                           |
            //+------------------------------------------------------------------+
            var Result      =  new List<ARInformationInfo>();
            string BillFlag = "2";
            Result          = da.SearchSmartPlusCorpBillInformation(CaId, BillFlag, WSCallGroupId);

            foreach (var d in Result)
            {
                if (d.MainSub == "M00400010" || d.MainSub == "00400010")
                {
                    d.CaDoc = "";
                }
            }
            return Result;
        }

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
                    
                    invoices = CheckInterestByInvoice(invoices,param.ToPayDate == null ? Session.BpmDateTime.Now.Date : param.ToPayDate.Value, true);
                    
                }

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
                    || (inv.Bills.Count > 0 && inv.Bills[0].GroupInvoiceId != null && inv.Bills[0].PaymentMethodId == CodeNames.PaymentMethod.GroupInvoicing.Id)
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
                            DateTime? dueDate           = bill.DueDate;
                            DateTime? deferralDate      = bill.DeferralDate;
                            DateTime? originalDueDate   = bill.OriginalDueDate;
                            decimal? amount             = bill.TaxCode != null ? (bill.TaxCode[0].ToString() == "O" ? bill.GAmount.Value : bill.AmountExVat.Value) : (bill.VatAmount == null ? bill.GAmount.Value : bill.GAmount.Value - bill.VatAmount.Value);

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
                        CalendarBS calendarBS   = new CalendarBS("Z00000");
                        Bill bill               = inv.Bills[0];
                        DateTime stDate         = toCalInterestDate;
                        stDate                  = calendarBS.GetFirstWorkingDay(stDate);
                        TimeSpan timespan       = toPayDate - stDate;
                        int totalDays           = (int)timespan.TotalDays;

                        //-----เพิ่มใหม--------------
                        int totalCalculateDays  = 0;
                        int beginYear           = stDate.Year;
                        int endYear             = toPayDate.Year;
                        //-------------------------

                        if (totalDays > 0)
                        {
                            Bill b              = new Bill();
                            b.CustomerId        = bill.CustomerId;
                            b.Name              = bill.Name;
                            b.Address           = bill.Address;
                            b.BranchId          = inv.BranchId;
                            b.ContractTypeId    = bill.ContractTypeId;

                            if (inv.Bills[0].DebtId == CodeNames.DebtType.AgencyGroupInvoicing.Id)
                            {
                                b.DebtId    = CodeNames.DebtType.Interest.GroupInvoice.Id;
                                b.DebtType  = CodeNames.DebtType.Interest.GroupInvoice.Name;
                            }
                            else
                            {
                                b.DebtId    = CodeNames.DebtType.Interest.Id;
                                b.DebtType  = CodeNames.DebtType.Interest.Name;
                            }

                            b.Period        = bill.Period;
                            b.Description   = string.Format("วันที่ {0} ถึง {1} จำนวน {2} วัน",
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
                                    totalAmountGroupByInvoice = Convert.ToDecimal(obb.TotalAmount - obb.Vat);

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




                            b.InterestRate  = bill.InterestRate;
                            b.Qty           = totalCalculateDays;
                            b.FullQty       = totalCalculateDays;
                            //-------------------------


                            b.UnitTypeId    = CodeNames.UnitType.Day.Id;
                            b.UnitTypeName  = CodeNames.UnitType.Day.Name;
                            b.AmountExVat   = interest;
                            b.FullAmount    = interest;
                            b.TaxCode       = CodeNames.TaxCode.NoTaxCharge.TaxCode;
                            b.TaxRate       = StringConvert.ToDecimal(CodeNames.TaxCode.NoTaxCharge.TaxRate);
                            b.GAmount       = b.AmountExVat;
                            b.FullGAmount   = b.AmountExVat;
                            b.ControllerId  = bill.ControllerId;
                            b.DataState     = BillDataStage.NewItem;

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
                            i.BranchId          = b.BranchId;
                            i.TechBranchName    = inv.TechBranchName;
                            i.CommBranchId      = inv.CommBranchId;
                            i.CommBranchName    = inv.CommBranchName;
                            i.CaId              = b.CustomerId;
                            i.Name              = b.Name;
                            i.Address           = b.Address;
                            i.ControllerId      = inv.ControllerId;
                            i.ControllerName    = inv.ControllerName;
                            i.MruId             = inv.MruId;
                            i.GAmount           = b.GAmount;
                            i.PaidVatAmount     = 0;
                            i.PaidGAmount       = 0;
                            i.ToPayQty          = b.Qty;
                            i.ToPayGAmount      = i.ToBePaidGAmount;
                            i.ToPayVatAmount    = i.ToBePaidVatAmount;
                            i.CaDoc             = null;
                            i.DataState         = InvoiceDataStage.NewItem;
                            i.Bills             = new List<Bill>();
                            i.Bills.Add(b);

                            if (Math.Round((decimal)b.GAmount, 2) != StringConvert.ToDecimal("0.00")) //201802271128 Kanokwan.L Defect#58 แก้ไขการคำนวณดอกเบี้ย 0.00 ไม่ให้แสดงบนหน้าจอ
                            paidInvoices.Add(i);

                        }
                    }

                    string IS_NOT_ELSE = "X";
                }

                string IS_NOT_ELSE2 = "X";
                ////แผนผ่อน กรณีไม่ได้ mark X ต้องให้เหลือดอกเบี้ยตามรายการของแผนภายใต้ CA ที่ค้นหา
                //if ()


            }

            string IS_NOT_ELSE3 = "X";

            paidInvoices = paidInvoices.GroupBy(n => new { n.CaId, n.InvoiceNo, n.OriginalInvoiceNo, n.DebtType, n.DueDate, n.GAmount }).Select(n => n.FirstOrDefault()).ToList();
            //201802271128 Kanokwan.L Defect#96 แก้ไขการคำนวณดอกเบี้ยไม่แสดง




            return paidInvoices;
        }
        
        public List<ARInformationInfo> SearchBillInformation(string CaId, string BillFlag)
        {
            List<ARInformationInfo> conList = new List<ARInformationInfo>();
            //try
            //{
            //    SmartPlusDA conData = new SmartPlusDA();
            //    conList = conData.SearchSmartPlusCorpBillInformation(CaId, BillFlag);                
            //}
            //catch (Exception)
            //{                
            //    throw;
            //}
            return conList;           
        }       

        public void InsertTransactionLog(string serviceName, string userId, string token, string caId, string invoiceNo, string agencyId, string serviceResultText)
        {
            try
            {
                SmartPlusDA service = new SmartPlusDA();
                //service.InsertSmartPlusTransactionLog(serviceName, userId, token, caId, invoiceNo, agencyId, serviceResultText);
            }
            catch
            {

            }
        }

        #endregion


        //private List<Invoice> CheckInterestByInvoice(List<Invoice> invoices, DateTime toPayDate, bool isAdded)
        //{
        //    List<Invoice> paidInvoices = new List<Invoice>();

        //    DateTime today = toPayDate; //Session.BpmDateTime.Now.Date;
        //    foreach (Invoice inv in invoices)
        //    {
        //        if (isAdded)
        //        {
        //            paidInvoices.Add(inv);
        //        }

        //        string debitNoteDebt = "M00100010,M00100090";

        //        if (((inv != null && inv.PmId == CodeNames.PaymentMethod.EPayment.Id)
        //            //|| (inv.Bills.Count > 0 && inv.Bills[0].GroupInvoiceId != null && inv.Bills[0].IsElectricDebt()) //2018002081423 Kanokwan.L แก้ไขการคิดดอกเบี้ยไมให้คิดกรณีค้นหาหนี้ด้วย Group
        //            || (inv.Bills.Count > 0 && inv.Bills[0].GroupInvoiceId != null && inv.Bills[0].PaymentMethodId == CodeNames.PaymentMethod.GroupInvoicing.Id)
        //            || (inv.Bills.Count > 0 && debitNoteDebt.Contains(inv.Bills[0].DebtId))) == false)
        //        {
        //            decimal toCalInterestAmount = 0;
        //            bool isInstallment = inv.InstallmentPeriod != null;

        //            DateTime toCalInterestDate = DateTime.Now;

        //            //Check for adding interest
        //            foreach (Bill bill in inv.Bills)
        //            {
        //                Decimal? interestRate = bill.InterestRate;

        //                if (interestRate != null && interestRate > 0)
        //                {
        //                    DateTime? dueDate = bill.DueDate;
        //                    DateTime? deferralDate = bill.DeferralDate;
        //                    DateTime? originalDueDate = bill.OriginalDueDate;
        //                    decimal? amount = bill.TaxCode != null ? (bill.TaxCode[0].ToString() == "O" ? bill.GAmount.Value : bill.AmountExVat.Value) : (bill.VatAmount == null ? bill.GAmount.Value : bill.GAmount.Value - bill.VatAmount.Value);

        //                    string interlockFlag = bill.InterestLockFlag;

        //                    if (interlockFlag == null)
        //                    {
        //                        if (!isInstallment && dueDate < today)
        //                        {

        //                            if (inv.InvoiceNo.Length == 22 && inv.Bills[0].GroupInvoiceId != null)
        //                            {
        //                                toCalInterestDate = dueDate.Value;
        //                                toCalInterestAmount = 0;
        //                            }
        //                            else
        //                            {
        //                                toCalInterestDate = dueDate.Value;
        //                                toCalInterestAmount += amount.Value;
        //                            }

        //                        }
        //                        else if (isInstallment && originalDueDate < today)
        //                        {
        //                            toCalInterestDate = originalDueDate.Value;
        //                            toCalInterestAmount += bill.LeftInstallmentAmount.Value;
        //                        }
        //                    }
        //                    else if (interlockFlag == "X")
        //                    {
        //                        if (!isInstallment && deferralDate < today)
        //                        {
        //                            toCalInterestDate = deferralDate.Value;
        //                            toCalInterestAmount += amount.Value;
        //                        }
        //                        else if (isInstallment && dueDate < today)
        //                        {
        //                            toCalInterestDate = dueDate.Value;
        //                            toCalInterestAmount += amount.Value;
        //                        }
        //                    }
        //                }
        //            }

        //            if (toCalInterestAmount > 0 || (inv.InvoiceNo.Length == 22 && inv.Bills[0].GroupInvoiceId != null)) // เพิ่มเติมกรณี GroupInvoice
        //            {
        //                CalendarBS calendarBS = new CalendarBS(Session.Branch.Id);
        //                Bill bill = inv.Bills[0];
        //                DateTime stDate = toCalInterestDate;
        //                stDate = calendarBS.GetFirstWorkingDay(stDate);
        //                TimeSpan timespan = toPayDate - stDate;
        //                int totalDays = (int)timespan.TotalDays;

        //                //-----เพิ่มใหม--------------
        //                int totalCalculateDays = 0;
        //                int beginYear = stDate.Year;
        //                int endYear = toPayDate.Year;
        //                //-------------------------

        //                if (totalDays > 0)
        //                {
        //                    Bill b = new Bill();
        //                    b.CustomerId = bill.CustomerId;
        //                    b.Name = bill.Name;
        //                    b.Address = bill.Address;
        //                    b.BranchId = inv.BranchId;
        //                    b.ContractTypeId = bill.ContractTypeId;

        //                    if (inv.Bills[0].DebtId == CodeNames.DebtType.AgencyGroupInvoicing.Id)
        //                    {
        //                        b.DebtId = CodeNames.DebtType.Interest.GroupInvoice.Id;
        //                        b.DebtType = CodeNames.DebtType.Interest.GroupInvoice.Name;
        //                    }
        //                    else
        //                    {
        //                        b.DebtId = CodeNames.DebtType.Interest.Id;
        //                        b.DebtType = CodeNames.DebtType.Interest.Name;
        //                    }

        //                    b.Period = bill.Period;
        //                    b.Description = string.Format("วันที่ {0} ถึง {1} จำนวน {2} วัน",
        //                        stDate.AddDays(+1).ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
        //                        toPayDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
        //                        totalDays);

        //                    //decimal interest = toCalInterestAmount
        //                    //    * (Convert.ToDecimal(totalDays) / Convert.ToDecimal(365))
        //                    //    * (bill.InterestRate.Value / Convert.ToDecimal(100));
        //                    //interest = Decimal.Round(interest, 2);
        //                    //b.InterestRate = bill.InterestRate;
        //                    //b.Qty = totalDays;
        //                    //b.FullQty = totalDays;


        //                    //-----เพิ่มใหม--------------
        //                    decimal interest = 0;
        //                    int leapDays = 0;

        //                    if (inv.InvoiceNo.Length == 22 && inv.Bills[0].GroupInvoiceId != null)
        //                    {
        //                        // GroupInvoice
        //                        decimal calInterestByInvoice = 0;
        //                        AgencyEntities.BillBookCheckInInfo _retVal = new AgencyEntities.BillBookCheckInInfo();
        //                        AgencyDA.BillBookDataAccess billDA = new AgencyDA.BillBookDataAccess();
        //                        _retVal = billDA.GetGroupInvoiceCheckInInfomation(inv.Bills[0].GroupInvoiceId, null, false);

        //                        List<AgencyEntities.BillBookCheckinDetailInfo> oBillbookCheckinDetailInfo = new List<AgencyEntities.BillBookCheckinDetailInfo>();
        //                        oBillbookCheckinDetailInfo = _retVal.BillBookCheckInDetail;
        //                        Decimal totalAmountGroupByInvoice = 0;
        //                        foreach (AgencyEntities.BillBookCheckinDetailInfo obb in oBillbookCheckinDetailInfo)
        //                        {
        //                            totalAmountGroupByInvoice = Convert.ToDecimal(obb.TotalAmount - obb.Vat);

        //                            if (toPayDate.Year - stDate.Year == 0)
        //                            {
        //                                leapDays = DateTime.IsLeapYear(toPayDate.Year) == true ? 366 : 365;

        //                                calInterestByInvoice = totalAmountGroupByInvoice
        //                                    * (Convert.ToDecimal(totalDays) / Convert.ToDecimal(leapDays))
        //                                    * (bill.InterestRate.Value / Convert.ToDecimal(100));
        //                                calInterestByInvoice = Decimal.Round(calInterestByInvoice, 2);

        //                                totalCalculateDays = totalDays;
        //                                interest += calInterestByInvoice;
        //                            }
        //                            else
        //                            {
        //                                int tmpYear = beginYear;
        //                                while (tmpYear <= endYear)
        //                                {
        //                                    decimal tmpInterest = 0;
        //                                    leapDays = DateTime.IsLeapYear(tmpYear) == true ? 366 : 365;
        //                                    if (tmpYear < endYear && tmpYear == beginYear)
        //                                    {
        //                                        totalDays = leapDays - stDate.DayOfYear;
        //                                    }
        //                                    else if (tmpYear < endYear && tmpYear > beginYear)
        //                                    {
        //                                        totalDays = leapDays;
        //                                    }
        //                                    else if (tmpYear == endYear)
        //                                    {
        //                                        totalDays = toPayDate.DayOfYear;
        //                                    }


        //                                    tmpInterest = totalAmountGroupByInvoice
        //                                       * (Convert.ToDecimal(totalDays) / Convert.ToDecimal(leapDays))
        //                                       * (bill.InterestRate.Value / Convert.ToDecimal(100));
        //                                    tmpInterest = Decimal.Round(tmpInterest, 2);

        //                                    interest += tmpInterest;

        //                                    if (oBillbookCheckinDetailInfo.First() == obb)
        //                                    {
        //                                        totalCalculateDays += totalDays;
        //                                    }
        //                                    tmpYear++;
        //                                }

        //                            }

        //                        }
        //                    }
        //                    else
        //                    {
        //                        // !=GroupInvoice
        //                        if (toPayDate.Year - stDate.Year == 0)
        //                        {
        //                            leapDays = DateTime.IsLeapYear(toPayDate.Year) == true ? 366 : 365;

        //                            interest = toCalInterestAmount
        //                                * (Convert.ToDecimal(totalDays) / Convert.ToDecimal(leapDays))
        //                                * (bill.InterestRate.Value / Convert.ToDecimal(100));
        //                            interest = Decimal.Round(interest, 2);

        //                            totalCalculateDays = totalDays;
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
        //                    }




        //                    b.InterestRate = bill.InterestRate;
        //                    b.Qty = totalCalculateDays;
        //                    b.FullQty = totalCalculateDays;
        //                    //-------------------------


        //                    b.UnitTypeId = CodeNames.UnitType.Day.Id;
        //                    b.UnitTypeName = CodeNames.UnitType.Day.Name;
        //                    b.AmountExVat = interest;
        //                    b.FullAmount = interest;
        //                    b.TaxCode = CodeNames.TaxCode.NoTaxCharge.TaxCode;
        //                    b.TaxRate = StringConvert.ToDecimal(CodeNames.TaxCode.NoTaxCharge.TaxRate);
        //                    b.GAmount = b.AmountExVat;
        //                    b.FullGAmount = b.AmountExVat;
        //                    b.ControllerId = bill.ControllerId;
        //                    b.DataState = BillDataStage.NewItem;

        //                    Invoice i = new Invoice();
        //                    i.InvoiceNo = null;
        //                    if (inv.InvoiceNo.Length == 22 && inv.Bills[0].GroupInvoiceId != null) // กรณีดอกเบี้ยการชำระแบบกลุ่ม
        //                    {
        //                        i.OriginalInvoiceNo = inv.Bills[0].GroupInvoiceId; // แก้ ดอกเบี้ยกลุ่ม
        //                        //i.OriginalInvoiceNo = inv.InvoiceNo; // แก้ ดอกเบี้ยกลุ่ม
        //                    }
        //                    else if (isInstallment && inv.Bills[0].InterestLockFlag == null) // กรณีดอกเบี้ยแผนผ่อน InterestLockFlag=null DF#109 201807191013 Kanokwan.L คิดดอกเบี้ยที่แม่ ต้องให้แสดงรายการดอกเบี้ยแค่รายการเดียว
        //                    {
        //                        i.OriginalInvoiceNo = inv.OriginalInvoiceNo;
        //                    }
        //                    else
        //                    {
        //                        i.OriginalInvoiceNo = inv.InvoiceNo;
        //                    }
        //                    i.BranchId = b.BranchId;
        //                    i.TechBranchName = inv.TechBranchName;
        //                    i.CommBranchId = inv.CommBranchId;
        //                    i.CommBranchName = inv.CommBranchName;
        //                    i.CaId = b.CustomerId;
        //                    i.Name = b.Name;
        //                    i.Address = b.Address;
        //                    i.ControllerId = inv.ControllerId;
        //                    i.ControllerName = inv.ControllerName;
        //                    i.MruId = inv.MruId;
        //                    i.GAmount = b.GAmount;
        //                    i.PaidVatAmount = 0;
        //                    i.PaidGAmount = 0;
        //                    i.ToPayQty = b.Qty;
        //                    i.ToPayGAmount = i.ToBePaidGAmount;
        //                    i.ToPayVatAmount = i.ToBePaidVatAmount;
        //                    i.CaDoc = null;
        //                    i.DataState = InvoiceDataStage.NewItem;
        //                    i.Bills = new List<Bill>();
        //                    i.Bills.Add(b);

        //                    if (Math.Round((decimal)b.GAmount, 2) != StringConvert.ToDecimal("0.00")) //201802271128 Kanokwan.L Defect#58 แก้ไขการคำนวณดอกเบี้ย 0.00 ไม่ให้แสดงบนหน้าจอ
        //                        paidInvoices.Add(i);

        //                }
        //            }
        //        }


        //        ////แผนผ่อน กรณีไม่ได้ mark X ต้องให้เหลือดอกเบี้ยตามรายการของแผนภายใต้ CA ที่ค้นหา
        //        //if ()


        //    }


        //    paidInvoices = paidInvoices.GroupBy(n => new { n.CaId, n.InvoiceNo, n.OriginalInvoiceNo, n.DebtType, n.DueDate, n.GAmount }).Select(n => n.FirstOrDefault()).ToList();
        //    //201802271128 Kanokwan.L Defect#96 แก้ไขการคำนวณดอกเบี้ยไม่แสดง




        //    return paidInvoices;
        //}

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.ePaymentsModule.Interface.Services;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.ReceiptPrinting;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PEA.BPM.ePaymentsModule.DA;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.ePaymentsModule.Interface.Constants;

namespace PEA.BPM.ePaymentsModule.BS
{
    public class BillingBS : IBillingService
    {

        private string GetReceiptId()
        {
            Random r = new Random();

            return "Z000000" + r.Next(100, 999).ToString() + r.Next(10000000, 9999999).ToString();
        }

        public string UpdateBillMarkFlagService(string caId, string invoiceNo, string mofifedBy, string postServerId)
        {
            string vResult = null;
            try
            {
                CommonDA markFlagData = new CommonDA();
                //update arpayment (Mark flag)
                vResult = markFlagData.InsertARPayment(caId, invoiceNo, mofifedBy, postServerId);
            }
            catch (Exception e)
            {
                throw;
            }
            return vResult;
        }

        public void DelBillMarkFlagService()
        {
            try
            {
                BillingDA conData = new BillingDA();
                conData.DelBillMarkFlagService();

            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<ClearifyInfo> SearchDebtService(SearchDebtParam searchDebtParam)
        {
            try
            {
                CommonDA conData = new CommonDA();
                List<ClearifyInfo> clearifyList = conData.SearchDebtData(searchDebtParam);
                return clearifyList;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<BPMClearifyInfo> SearchBPMDebtClearify(SearchDebtParam searchDebtParam)
        {
            try
            {
                CommonDA conData = new CommonDA();
                List<BPMClearifyInfo> clearifyList = conData.SearchBPMDebtClearify(searchDebtParam);
                return clearifyList;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<EpayUploadItem> GetDebtComparableService(string caInvoceNo)
        {
            try
            {
                BillingDA conData = new BillingDA();
                List<EpayUploadItem> ePayUploadList = conData.GetDebtComparableData(caInvoceNo);
                return ePayUploadList;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<Company> SearchCompany(Company compParm)
        {
            try
            {
                BillingDA conData = new BillingDA();
                List<Company> compList = conData.SearchCompany(compParm);
                return compList;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void InsertEPayUploadService(List<EPaymentUploadFile> ePayFileList)
        {
            string fileId = null;
            string ePayItemId = null;
            string terminalId = null;
            string refDoc = null;
            string fileList = "";
            DateTime currentDate = DateTime.Now;
            int row = 0;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction(System.Data.IsolationLevel.Snapshot);
                try
                {
                    BillingDA conData = new BillingDA();
                    CommonBS common = new CommonBS();
                    refDoc = common.GetRefDocNo();
                    foreach (EPaymentUploadFile ePayUpload in ePayFileList)
                    {
                        terminalId = ePayUpload.PosId;
                        ePayUpload.CurrentDate = currentDate;
                        ePayUpload.EPaymentUpload.PostBranchId = Session.Branch.PostedServerId;
                        ePayUpload.EPaymentUpload.ModifiedDt = currentDate;
                        fileId = conData.InsertEPayUploadData(trans, ePayUpload.EPaymentUpload, terminalId);
                        fileList += fileId.Trim() + ",";
                        row = 0;
                        foreach (EpayUploadItem ePayItem in ePayUpload.EpaymentUploadItem)
                        {
                            ePayItem.EpayUploads.FileId = fileId;
                            ePayItem.PostBranchId = Session.Branch.PostedServerId;
                            ePayItem.ModifiedDt = currentDate;
                            ePayItemId = conData.InsertEPayUploadItemData(trans, ePayItem, terminalId);
                            if (!ePayItem.UploadStatus.Equals(EPaymentUploadStatus.CLEAR))
                            {
                                EPayClearify ePayClearify = ePayUpload.EPaymentClearify[row];
                                ePayClearify.EpayUploadItems.EpayItemId = ePayItemId;
                                ePayClearify.PostBranchId = Session.Branch.PostedServerId;
                                ePayClearify.ModifiedDt = currentDate;
                                ePayClearify.RefDocNo = refDoc;
                                conData.InsertEPayClearifyData(trans, ePayClearify, terminalId);
                            }
                            row++;
                        }
                    }

                    PaymentTransParam payTrans = new PaymentTransParam();
                    payTrans.FileId = fileList;
                    payTrans.Prefix = "MC";
                    payTrans.PosId = terminalId;
                    payTrans.PtId = "4";
                    payTrans.ReceiptType = "5";
                    payTrans.ModifiedBy = Session.User.Id;
                    payTrans.ModifiedDt = currentDate;
                    conData.InsertPaymentTransData(trans, payTrans);

                    trans.Commit();

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Logger.WriteError(Logger.Module.EPAYMENT, "InsertEPayUploadService", ex.ToString());
                    throw;
                }
            }
        }

        public List<AgentPayment> GetAgentPaymentService(AgentPayment agentPayment)
        {
            try
            {
                BillingDA conData = new BillingDA();
                List<AgentPayment> agentPayList = conData.GetAgentPaymentData(agentPayment);
                return agentPayList;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void InsertAgentPaymentService(List<AgentPayment> agentPayList)
        {
            int i = 0;
            DateTime currentDate = DateTime.Now;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction(System.Data.IsolationLevel.Snapshot);
                try
                {
                    BillingDA conData = new BillingDA();

                    foreach (AgentPayment agentPay in agentPayList)
                    {
                        if (!agentPay.IsSysData)
                        {
                            i++;
                            agentPay.PostBranchServerId = Session.Branch.PostedServerId;
                            agentPay.ModifiedDt = currentDate;
                            agentPay.ModifiedBy = Session.User.Id;
                            if (i == 1)
                            {
                                agentPay.ModifiedDt = currentDate;
                                agentPay.ModifiedBy = Session.User.Id;
                                conData.InsertAgentPaymentMaster(trans, agentPay);
                            }
                            conData.InsertAgentPaymentDetail(trans, agentPay);
                        }
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Logger.WriteError(Logger.Module.EPAYMENT, "InsertEPayUploadService", ex.ToString());
                    throw;
                }
            }
        }

        public bool IsUploadFileNameExist(string fileName)
        {
            try
            {
                BillingDA conData = new BillingDA();
                int result = conData.GetUploadFileName(fileName);
                return result > 0;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //List<BPMClearifyInfo> SearchBPMDebtClearify(SearchDebtParam searchDebtParam)
        //{
        //    try
        //    {
        //        CommonDA conData = new CommonDA();
        //        return conData.SearchBPMDebtClearify(searchDebtParam);
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }
        //}

        public bool SaveClearify(DbTransaction trans, SaveClearifyInfo saveClearifyItem)
        {           
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();                
                try
                {                                  
                    BillingDA billingDa = new BillingDA();
                    CommonDA commDa = new CommonDA();
                    int rowCount = 0;
                    string invoiceNo = String.Empty;
                    saveClearifyItem.PostBranchServerId = Session.Branch.PostedServerId;
                    saveClearifyItem.ReceiptPrefix = "D";
                    Agency agent = new Agency();
                    agent.AgencyId = saveClearifyItem.AgentId;
                    agent = commDa.GetCaData(agent);
                    Agency customer = new Agency();
                    customer.AgencyId = saveClearifyItem.SaveCaId == String.Empty ? "0000" : saveClearifyItem.SaveCaId;
                    customer = commDa.GetCaData(customer);

                    // save data นำฝาก
                    if (saveClearifyItem.OverDebtOwner > 0)                    
                    {

                        string paymentId = billingDa.InsertClearifyPayment(trans, saveClearifyItem.PaymentDt, saveClearifyItem.PostId, saveClearifyItem.BranchId, saveClearifyItem.PostBranchServerId, saveClearifyItem.ModifeidBy);
                        if (paymentId == String.Empty)
                        {
                            trans.Rollback();
                            Logger.WriteError(Logger.Module.EPAYMENT, "SaveClearify", "PaymentId is null");
                            return false;
                        }
                        string arPtId = billingDa.InsertClearifyARPaymentType(trans, paymentId, saveClearifyItem.OverDebtOwner, saveClearifyItem.PostId, saveClearifyItem.PostBranchServerId, saveClearifyItem.ModifeidBy);
                        if (arPtId == String.Empty)
                        {
                            trans.Rollback();
                            Logger.WriteError(Logger.Module.EPAYMENT, "SaveClearify", "ARPtId is null");
                            return false;
                        }

                        string caid = saveClearifyItem.DepReceiptType == "2" ? saveClearifyItem.AgentId : saveClearifyItem.SaveCaId;
                        invoiceNo = billingDa.InsertClearifyAR(trans, saveClearifyItem.BranchId, caid, saveClearifyItem.ClearifyDes, saveClearifyItem.OverDebtOwner, saveClearifyItem.PostId, saveClearifyItem.PostBranchServerId, saveClearifyItem.ModifeidBy, saveClearifyItem.PaidAmount != 0?String.Empty:saveClearifyItem.InvoiceNo);
                        if (invoiceNo == String.Empty)
                        {
                            trans.Rollback();
                            Logger.WriteError(Logger.Module.EPAYMENT, "SaveClearify", "InvoiceNo is null");
                            return false;
                        }

                        string arPmId = billingDa.InsertClearifyARPayment(trans, arPtId, invoiceNo, saveClearifyItem.BranchId, saveClearifyItem.OverDebtOwner, saveClearifyItem.PaymentDt, saveClearifyItem.PostId, saveClearifyItem.PostBranchServerId, saveClearifyItem.ModifeidBy);
                        if (arPmId == String.Empty)
                        {
                            trans.Rollback();
                            Logger.WriteError(Logger.Module.EPAYMENT, "SaveClearify", "ARPmId is null");
                            return false;
                        }
                        rowCount = billingDa.InsertClearifyRTARPaymentTypeARPayment(trans, arPtId, arPmId, saveClearifyItem.OverDebtOwner, saveClearifyItem.PostBranchServerId, saveClearifyItem.ModifeidBy);
                        if (rowCount <= 0)
                        {
                            trans.Rollback();
                            Logger.WriteError(Logger.Module.EPAYMENT, "SaveClearify", "Can not save in RTARPaymentTypeARPayment");
                            return false;
                        }

                   
                        Receipt depositReceipt = new Receipt();
                        depositReceipt.ReceiptType = "6";
                        depositReceipt.Active = "1";

                        if (saveClearifyItem.DepReceiptType == "2")
                        {
                            depositReceipt.CaId = agent.AgencyId;                            
                            depositReceipt.Name = agent.AgencyName;
                            depositReceipt.Address = agent.Address;
                        }
                        else 
                        {
                            depositReceipt.CaId = customer.AgencyId;                           
                            depositReceipt.Name = customer.AgencyName;
                            depositReceipt.Address = customer.Address;
                        }

                        depositReceipt.ARPtId = arPtId;
                        depositReceipt.ARPmId = arPmId;                        
                        depositReceipt.ExtReceiptDt = null;
                        depositReceipt.ExtReceiptId = null;
                        depositReceipt.IsNameAddrModified = "0";
                        depositReceipt.ModifiedBy = saveClearifyItem.ModifeidBy;
                        depositReceipt.ModifiedDt = DateTime.Now;                       
                        depositReceipt.PaymentId = paymentId;
                        depositReceipt.PostBranchServerId = saveClearifyItem.PostBranchServerId;
                        depositReceipt.PostDt = DateTime.Now;

                        // prefix Deposit receipt => D
                        // หนี้ ผู้ใช้ไฟใช้ M=> clear ที่บ้าน, MC => ที่ส่วนกลาง
                        rowCount = billingDa.InsertClearifyReceipt(trans, depositReceipt, saveClearifyItem.PostId, saveClearifyItem.ReceiptPrefix);
                        if (rowCount == 0)
                        {
                            trans.Rollback();
                            Logger.WriteError(Logger.Module.EPAYMENT, "SaveClearify", "Receipt Id is null.");
                            return false;
                        }
                       
                    }
                    
                    if (saveClearifyItem.PaidAmount != 0)
                    {
                        rowCount = billingDa.ClearCaPayment(trans, saveClearifyItem.PostId, saveClearifyItem.InvoiceNo, saveClearifyItem.NewInvoiceNo, saveClearifyItem.PostBranchServerId,
                                            saveClearifyItem.ModifeidBy, saveClearifyItem.BranchId, customer.AgencyId, customer.AgencyName, customer.Address, saveClearifyItem.PaidAmount);

                        if (rowCount == 0)
                        {
                            trans.Rollback();
                            Logger.WriteError(Logger.Module.EPAYMENT, "SaveClearify", "Can not clear customer debt.");
                            return false;
                        }
                    }

                    //update clearify status
                    rowCount = billingDa.UpldateClearify(trans, saveClearifyItem, invoiceNo);
                    if (rowCount == 0)
                    {
                        trans.Rollback();
                        Logger.WriteError(Logger.Module.EPAYMENT, "SaveClearify", "Can not update Clearify.");
                        return false;
                    }

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Logger.WriteError(Logger.Module.POS, "SaveClearify", ex.ToString());
                    throw;
                }
            }

        }

        public List<CancelPayment> SearchAgentPayment(CancelPayment payment)
        {
            try
            {
                BillingDA conData = new BillingDA();
                List<CancelPayment> paymentList = conData.SearchAgentPaymentData(payment);
                return paymentList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void InsertCancelPayment(List<CancelPayment> cancelPayment)
        {
            DateTime currentDate = DateTime.Now;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    BillingDA conData = new BillingDA();
                    foreach(CancelPayment cancelPay in cancelPayment)
                    {
                        cancelPay.PostBranchId = Session.Branch.PostedServerId;
                        cancelPay.ModifiedDt = currentDate;
                        conData.InsertCancelPaymentData(trans, cancelPay);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Logger.WriteError(Logger.Module.EPAYMENT, "InsertCancelPaymentService", ex.ToString());
                    throw new Exception(ex.Message);
                }
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Integration.BPMIntegration.Interface.Services;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using PEA.BPM.Integration.BPMIntegration.DA;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Integration.BPMIntegration.Interface.ExportEntities;
using System.IO;
using System.ComponentModel;
using PEA.BPM.Integration.ACABatchService;


namespace PEA.BPM.Integration.BPMIntegration.BS
{
    public class BPMSAPService : IBpmSAPService
    {
        #region Import Data from SAP DL01_ISOLATE
        /// <summary>
        /// Import Data from SAP
        /// </summary>        
        public bool Import_NonWorkingDay(List<NonWorkingDayInfo> impList, ACABatchParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            bool retVal = da.UpdateNonWorkingDay(impList, param);
            return retVal;
        }

        public bool Import_AccountClass(List<AccountClassInfo> impList, ACABatchParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            bool retVal = da.UpdateAccountClass(impList, param);
            return retVal;
        }

        public bool Import_ContractType(List<ContractTypeInfo> impList, ACABatchParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            bool retVal = da.UpdateContractType(impList, param);
            return retVal;
        }

        public bool Import_MeterSizeType(List<MeterSizeInfo> impList, ACABatchParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            bool retVal = da.UpdateMeterSizeType(impList, param);
            return retVal;
        }

        public bool Import_PaymentMethod(List<PaymentMethodInfo> impList, ACABatchParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            bool retVal = da.UpdatePaymentMethod(impList, param);
            return retVal;
        }

        public bool Import_TaxCode(List<TaxCodeInfo> impList, ACABatchParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            bool retVal = da.UpdateTaxCode(impList, param);
            return retVal;
        }

        public bool Import_UnitType(List<UnitTypeInfo> impList, ACABatchParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            bool retVal = da.UpdateUnitType(impList, param);
            return retVal;
        }

        #endregion

        #region Import Data from SAP DL02_MASTER

        public bool Import_BusinessPartner(List<BusinessPartnerInfo> impList, ACABatchParam param, ref bool skipError, string fileType)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            bool retVal = da.UpdateBusinessPartner(impList, param, ref skipError, fileType);
            return retVal;
        }

        public bool Import_Branch(List<BranchInfo> impList, ACABatchParam param, ref bool skipError)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            param.Overwrite = true;
            bool retVal = da.UpdateBranch(impList, param, ref skipError);
            return retVal;
        }

        public bool Import_MRU(List<MRUInfo> impList, ACABatchParam param, ref bool skipError)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            bool retVal = da.UpdateMRU(impList, param, ref skipError);
            return retVal;
        }

        public bool Import_MRUPlan(List<MRUPlanInfo> impList, ACABatchParam param, ref bool skipError)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            bool retVal = da.UpdateMRUPlan(impList, param, ref skipError);
            return retVal;
        }

        public bool Import_CABulk(ACABatchParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            return da.UpdateCAByBulk(param);
        }

        public bool Import_ControllerBulk(ACABatchParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            return da.UpdateControllerByBulk(param);
        }

        public bool Import_MRUPlanByBulk(ACABatchParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            return da.UpdateMRUPlanByBulk(param);
        }

        public bool Import_ContractAccount(List<ContractAccountInfo> impList, ACABatchParam param, ref bool skipError, string fileType)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            bool retVal = da.UpdateContractAccount(impList, param, ref skipError, fileType);
            return retVal;
        }

        public bool Import_Employee(List<EmployeeInfo> impList, ACABatchParam param, ref bool skipError)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            param.Overwrite = true;
            bool retVal = da.UpdateEmployee(impList, param, ref skipError);
            return retVal;
        }

        public bool Import_AgencyAsset(List<AgencyAssetInfo> impList, ACABatchParam param, ref bool skipError)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            bool retVal = da.UpdateAgencyAsset(impList, param, ref skipError);
            return retVal;
        }

        public bool Import_Bank(List<BankInfo> impList, ACABatchParam param, ref bool skipError)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            param.Overwrite = true;
            bool retVal = da.UpdateBank(impList, param, ref skipError);
            return retVal;
        }

        public bool Import_BankAccount(List<BankAccountInfo> impList, ACABatchParam param, ref bool skipError)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            param.Overwrite = true;
            bool retVal = da.UpdateBankAccount(impList, param, ref skipError);
            return retVal;
        }

        public bool Import_MainSub(List<MainSubInfo> impList, ACABatchParam param, ref bool skipError)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            param.Overwrite = true;
            bool retVal = da.UpdateMainSub(impList, param, ref skipError);
            return retVal;
        }

        public bool Import_Company(List<CompanyInfo> impList, ACABatchParam param, ref bool skipError)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            param.Overwrite = true;
            bool retVal = da.UpdateCompany(impList, param, ref skipError);
            return retVal;
        }

        #endregion

        #region Import Data from SAP DL03_BILLING

        public int Import_BillingData(ACABatchParam param)
        {
            int ret = 0;
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    BPMSAPDataAccess da = new BPMSAPDataAccess();
                    ret = da.UpdateBillingData(trans, param);
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }

            return ret;
        }

        #endregion

        #region Import Data from SAP DL04_TRANSACTION

        public bool Import_AR(ACABatchParam param, string fileType)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            return da.UpdateAR(param, fileType);
        }

        public bool Import_ARElectric(ACABatchParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            return da.UpdateARElectric(param);
        }

        public bool Import_DisconnectionDoc(List<DisconnectionDocInfo> impList, ACABatchParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            bool retVal = da.UpdateDisconnectionDoc(impList, param);
            return retVal;
        }

        public bool Import_RTDisconnectionDocCaDoc(List<RTDisconnectionDocCaDocInfo> impList, ACABatchParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            bool retVal = da.UpdateRTDisconnectionDocCaDoc(impList, param);
            return retVal;
        }
        public bool Import_DisconnectionStatus(List<DisconnectionStatus> impList, ACABatchParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            bool retVal = da.UpdateDisconnectionStatus(impList, param);
            return retVal;
        }

        public bool Import_CancelBPMPayment(ACABatchParam param)
        {
            return new BPMSAPDataAccess().UpdateCancelBPMPayment(param);
        }

        public bool Import_CancelBPMPayment_50A(ACABatchParam param)
        {
            return new BPMSAPDataAccess().UpdateCancelBPMPayment_50A(param);
        }

        public bool Import_ARSubGroupInvoice(ACABatchParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            return da.UpdateARSubGroupInvoice(param);
        }

        public bool Import_ARDisconnect(ACABatchParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            return da.UpdateARDisconnect(param);
        }

        #endregion

        #region Import Data from SAP DL05_PAYFROMSAP
        public bool Import_PayFromSAP(ACABatchParam param, string fileType)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            return da.UpdatePayFromSAP(param, fileType);
        }

        #endregion

        #region Import Data from SAP DL06_DIRECTDEBIT

        public bool Import_BillingDetailStatus(List<BillingDetailStatusInfo> impList, ACABatchParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            bool retVal = da.UpdateBillingDetailStatus(impList, param);
            return retVal;
        }

        public bool Import_BillingReverse(List<BillingReverseInfo> impList, ACABatchParam param)
        {
            bool ret = false;
            try
            {
                BPMSAPDataAccess da = new BPMSAPDataAccess();
                ret = da.UpdateBillingReverse(impList, param);
            }
            catch (Exception)
            {
                throw;
            }

            return ret;
        }

        public bool Import_DirectDebit(ACABatchParam param)
        {
            bool ret = false;
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    BPMSAPDataAccess da = new BPMSAPDataAccess();
                    ret = da.UpdateDirectDebit(trans, param);
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }

            return ret;
        }

        public bool Import_Receiptx(ACABatchParam param)
        {
            bool ret = false;
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    BPMSAPDataAccess da = new BPMSAPDataAccess();
                    ret = da.UpdateReceiptx(trans, param);
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }

            return ret;
        }

        public bool Import_PWBBillStatus(ACABatchParam param)
        {
            bool ret = false;
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    BPMSAPDataAccess da = new BPMSAPDataAccess();
                    ret = da.UpdatePWBBillStatus(trans, param);
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }

            return ret;
        }

        #endregion

        #region Export to SAP

        /// <summary>
        /// Export Agency Commission to SAP
        /// </summary>        
        /// 
        public List<AG_Compensation> Export_AgencyCompensation(string branchId, ACABatchParam param)
        {
            List<AG_Compensation> comList = new List<AG_Compensation>();

            BPMSAPDataAccess da = new BPMSAPDataAccess();
            comList.AddRange(da.GetAgencyCompensation(branchId, param));

            return comList;
        }

        /// <summary>
        /// Get BillBook For Export
        /// </summary>
        /// <returns></returns>
        public List<AG_BillBook> GetBillBookForExport()
        {
            List<AG_BillBook> bbiList = new List<AG_BillBook>();

            BPMSAPDataAccess da = new BPMSAPDataAccess();
            bbiList.AddRange(da.GetBillBookForExport());

            return bbiList;
        }

        /// <summary>
        /// Export BillBook to SAP
        /// </summary>
        /// <param name="billBookId"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<AG_BillBookInvoice> Export_BillBookInvoice(string billBookId, ACABatchParam param)
        {
            List<AG_BillBookInvoice> bbiList = new List<AG_BillBookInvoice>();

            BPMSAPDataAccess da = new BPMSAPDataAccess();
            bbiList.AddRange(da.GetBillBookInvoice(billBookId, param));

            return bbiList;
        }

        public List<AR_Normal> Export_ARNormal(string branchId)
        {
            List<AR_Normal> arList = new List<AR_Normal>();

            BPMSAPDataAccess da = new BPMSAPDataAccess();
            arList = da.GetARNormal(branchId);

            return arList;
        }

        public List<AR_SpotBill> Export_ARSpotBill(string branchId)
        {
            List<AR_SpotBill> arList = new List<AR_SpotBill>();

            BPMSAPDataAccess da = new BPMSAPDataAccess();
            arList = da.GetARSpotBill(branchId);

            return arList;
        }

        public List<AR_GrpInv> Export_ARGroupInvoice(string branchId)
        {
            List<AR_GrpInv> arList = new List<AR_GrpInv>();

            BPMSAPDataAccess da = new BPMSAPDataAccess();
            arList = da.GetARGroupInvoice(branchId);

            return arList;
        }

        public List<AR_Agency> Export_ARPaidByAgency(string branchId)
        {
            List<AR_Agency> arList = new List<AR_Agency>();

            BPMSAPDataAccess da = new BPMSAPDataAccess();
            arList = da.GetARPaidByAgency(branchId);

            return arList;
        }

        public List<AR_PartialPay> Export_ARPartialPayment(string branchId)
        {
            List<AR_PartialPay> arList = new List<AR_PartialPay>();

            BPMSAPDataAccess da = new BPMSAPDataAccess();
            arList = da.GetARPartialPayment(branchId);

            return arList;
        }

        public List<ARn_Normal> Export_ARNonInvNormal(string branchId)
        {
            List<ARn_Normal> arList = new List<ARn_Normal>();

            BPMSAPDataAccess da = new BPMSAPDataAccess();
            arList = da.GetARNonInvNormal(branchId);

            return arList;
        }

        public List<ARn_CashSale> Export_ARNonInvCashSale(string branchId)
        {
            List<ARn_CashSale> arList = new List<ARn_CashSale>();

            BPMSAPDataAccess da = new BPMSAPDataAccess();
            arList = da.GetARNonInvCashSale(branchId);

            return arList;
        }

        public List<ARn_AdvPayment> Export_ARNonInvAdvancePayment(string branchId)
        {
            List<ARn_AdvPayment> arList = new List<ARn_AdvPayment>();

            BPMSAPDataAccess da = new BPMSAPDataAccess();
            arList = da.GetARNonInvAdvancePayment(branchId);

            return arList;
        }

        public List<ARn_APPayment> Export_APPayment(string branchId)
        {
            List<ARn_APPayment> arList = new List<ARn_APPayment>();

            BPMSAPDataAccess da = new BPMSAPDataAccess();
            arList = da.GetAPPayment(branchId);

            return arList;
        }

        public List<AR_DepositReceipt> Export_ARDepositReceipt(string branchId)
        {
            List<AR_DepositReceipt> arList = new List<AR_DepositReceipt>();

            BPMSAPDataAccess da = new BPMSAPDataAccess();
            arList = da.GetARDepositReceipt(branchId);

            return arList;
        }

        public List<AP_Payment> Export_APVoucher(string branchId)
        {
            List<AP_Payment> apList = new List<AP_Payment>();

            BPMSAPDataAccess da = new BPMSAPDataAccess();
            apList = da.GetAP_PaymentVoucher(branchId);

            return apList;
        }

        public List<AP_BankPayIn> Export_APBankPayIn(string branchId)
        {
            List<AP_BankPayIn> apList = new List<AP_BankPayIn>();

            BPMSAPDataAccess da = new BPMSAPDataAccess();
            apList = da.GetAP_BankPayIn(branchId);

            return apList;
        }

        public List<AR_Normal> Export_MPayARNormal(string branchId)
        {
            List<AR_Normal> arList = new List<AR_Normal>();

            BPMSAPDataAccess da = new BPMSAPDataAccess();
            arList = da.GetMPayARNormal(branchId);

            return arList;
        }

        public List<ARn_AdvPayment> Export_MPayARNonInvAdvancePayment(string branchId)
        {
            List<ARn_AdvPayment> arList = new List<ARn_AdvPayment>();

            BPMSAPDataAccess da = new BPMSAPDataAccess();
            arList = da.GetMPayARNonInvAdvancePayment(branchId);

            return arList;
        }

        public List<AR_DepositReceipt> Export_MPayARDepositReceipt(string branchId)
        {
            List<AR_DepositReceipt> arList = new List<AR_DepositReceipt>();

            BPMSAPDataAccess da = new BPMSAPDataAccess();
            arList = da.GetMPayARDepositReceipt(branchId);

            return arList;
        }

        public List<ARn_APPayment> Export_APChequePayment(string branchId)
        {
            List<ARn_APPayment> arList = new List<ARn_APPayment>();

            BPMSAPDataAccess da = new BPMSAPDataAccess();
            arList = da.GetAPChequePayment(branchId);

            return arList;
        }

        public List<AR_Reconnect> Export_ARReconnect(string branchId)
        {
            List<AR_Reconnect> arList = new List<AR_Reconnect>();

            BPMSAPDataAccess da = new BPMSAPDataAccess();
            arList = da.GetARReconnect(branchId);

            return arList;
        }

        #endregion

        #region AutoExport

        public void MarkBranchPrefix()
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            da.MarkBranchPrefix();
        }

        public void ProcessExportDone()
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            da.ProcessExportDone();
        }

        public bool CheckAnotherRegionalToExport()
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            return da.CheckAnotherRegionalToExport();
        }

        #endregion

        #region Set Sync Flag

        /// <summary>
        /// After exporting data to file then Set Sync_Flag = 0.
        /// </summary>


        public void SetSync_AgencyCompensation(SetSyncParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            da.SetSyncAgencyCompensation(param);
        }


        public void SetSync_ARNormal(SetSyncParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            da.SetSyncARNormal(param);
        }


        public void SetSync_ARSpotBill(SetSyncParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            da.SetSyncARSpotBill(param);
        }


        public void SetSync_ARGroupInvoice(SetSyncParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            da.SetSyncARGroupInvoice(param);
        }


        public void SetSync_ARPaidByAgency(SetSyncParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            da.SetSyncARPaidByAgency(param);
        }


        public void SetSync_ARPartialPayment(SetSyncParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            da.SetSyncARPartialPayment(param);
        }


        public void SetSync_ARNonInvNormal(SetSyncParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            da.SetSyncARNonInvNormal(param);
        }


        public void SetSync_ARNonInvCashSale(SetSyncParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            da.SetSyncARNonInvCashSale(param);
        }


        public void SetSync_ARNonInvAdvancePayment(SetSyncParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            da.SetSyncARNonInvAdvancePayment(param);
        }


        public void SetSync_APPayment(SetSyncParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            da.SetSyncAPPayment(param);
        }


        public void SetSync_ARDepositReceipt(SetSyncParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            da.SetSyncARDepositReceipt(param);
        }


        public void SetSync_APVoucher(SetSyncParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            da.SetSyncAPVoucher(param);
        }


        public void SetSync_APBankPayIn(SetSyncParam param)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            da.SetSyncAPBankPayIn(param);
        }

        public string GetExportFileName(string interfaceName)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            return da.GetExportFileName(interfaceName);
        }

        public string GetExportFileNameWithoutExtension(string interfaceName)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            return da.GetExportFileNameWithoutExtension(interfaceName);
        }

        public string GetExportFileNameTimestamp(string interfaceName, string billBookId)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            return da.GetExportFileNameTimestamp(interfaceName, billBookId);
        }

        #endregion

        #region ClearSpotBillCase

        public void ClearSpotBillCase()
        {
            try
            {
                BPMSAPDataAccess da = new BPMSAPDataAccess();
                da.ClearSpotBillCase();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Decrease Interface Running Number

        public void DecreaseInterfaceRunningNumber(string interfaceNumber)
        {
            try
            {
                BPMSAPDataAccess da = new BPMSAPDataAccess();
                da.DecreaseInterfaceRunningNumber(interfaceNumber);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GetListOfBranchIdForExportingData

        public List<String> GetListOfBranchIdForExportingData()
        {
            try
            {
                BPMSAPDataAccess da = new BPMSAPDataAccess();
                List<String> list = new List<String>();
                list = da.GetListOfBranchIdForExportingData();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ClearListOfBranchIdForExportingData(List<string> branchIdList)
        {
            try
            {
                BPMSAPDataAccess da = new BPMSAPDataAccess();
                da.ClearListOfBranchIdForExportingData(branchIdList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Reconnection

        public List<BillInfo> GetReconnection(string BranchId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                BPMSAPDataAccess da = new BPMSAPDataAccess();
                return da.GetReconnection(BranchId, fromDate, toDate);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Other

        public DateTime GetDBDateTime()
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            return da.GetDBDateTime();
        }

        public string GetDBDateTime(string format)
        {
            BPMSAPDataAccess da = new BPMSAPDataAccess();
            return da.GetDBDateTime(format);
        }

        #endregion
    }
}
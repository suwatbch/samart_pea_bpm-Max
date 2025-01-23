using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Integration.BPMIntegration.Interface;
using PEA.BPM.Integration.BPMIntegration.DA;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using PEA.BPM.Integration.BPMIntegration.Interface.Services;
using PEA.BPM.Architecture.CommonUtilities;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Configuration;

namespace PEA.BPM.Integration.BPMIntegration.BS
{
    public class BPMServerService : IBpmServerService
    {
        #region DL10 ISOLATE

        public List<NonWorkingDayInfo> GetUpdateNonWorkingDay(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<NonWorkingDayInfo> retVals = new List<NonWorkingDayInfo>();
                BPMServerDataAccess da = new BPMServerDataAccess();
                retVals = da.GetUpdateNonworkingDay(branchId, lastModifiedDate, serverDate);
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
       
        public List<AccountClassInfo> GetUpdateAccountClass(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<AccountClassInfo> retVals = new List<AccountClassInfo>();
                BPMServerDataAccess da = new BPMServerDataAccess();
                retVals = da.GetUpdateAccountClass(branchId, lastModifiedDate, serverDate);
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ContractTypeInfo> GetUpdateContractType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<ContractTypeInfo> retVals = new List<ContractTypeInfo>();
                BPMServerDataAccess da = new BPMServerDataAccess();
                retVals = da.GetUpdateContractType(branchId, lastModifiedDate, serverDate);
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<MeterSizeInfo> GetUpdateMeterSize(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<MeterSizeInfo> retVals = new List<MeterSizeInfo>();
                BPMServerDataAccess da = new BPMServerDataAccess();
                retVals = da.GetUpdateMeterSize(branchId, lastModifiedDate, serverDate);
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<PaymentMethodInfo> GetUpdatePaymentMethod(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<PaymentMethodInfo> retVals = new List<PaymentMethodInfo>();
                BPMServerDataAccess da = new BPMServerDataAccess();
                retVals = da.GetUpdatePaymentMethod(branchId, lastModifiedDate, serverDate);
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<TaxCodeInfo> GetUpdateTaxCode(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<TaxCodeInfo> retVals = new List<TaxCodeInfo>();
                BPMServerDataAccess da = new BPMServerDataAccess();
                retVals = da.GetUpdateTaxCode(branchId, lastModifiedDate, serverDate);
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<UnitTypeInfo> GetUpdateUnitType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<UnitTypeInfo> retVals = new List<UnitTypeInfo>();
                BPMServerDataAccess da = new BPMServerDataAccess();
                retVals = da.GetUpdateUnitType(branchId, lastModifiedDate, serverDate);
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<BusinessPartnerTypeInfo> GetUpdateBusinessPartnerType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateBusinessPartnerType(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CalendarInfo> GetUpdateCalendar(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateCalendar(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<PaymentTypeInfo> GetUpdatePaymentType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdatePaymentType(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<InterestKeyInfo> GetUpdateInterestKey(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                return new BPMServerDataAccess().GetUpdateInterestKey(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AgencyBillCollectionStatusInfo> GetUpdateAgencyBillCollectionStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                return new BPMServerDataAccess().GetUpdateAgencyBillCollectionStatus(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AgencyBillOutStatusInfo> GetUpdateAgencyBillOutStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                return new BPMServerDataAccess().GetUpdateAgencyBillOutStatus(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AgencyCommissionRateInfo> GetUpdateAgencyCommissionRate(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                return new BPMServerDataAccess().GetUpdateAgencyCommissionRate(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<BillBookStatusInfo> GetUpdateBillBookStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                return new BPMServerDataAccess().GetUpdateBillBookStatus(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AgencyCollectAreaInfo> GetUpdateAgencyCollectArea(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                return new BPMServerDataAccess().GetUpdateAgencyCollectArea(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<DisconnectionStatusInfo> GetUpdateDisconnectionStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                return new BPMServerDataAccess().GetUpdateDisconnectionStatus(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CashierMoneyFlowTypeInfo> GetUpdateCashierMoneyFlowType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                return new BPMServerDataAccess().GetUpdateCashierMoneyFlowType(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<OwnBankInfo> GetUpdateOwnBank(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                return new BPMServerDataAccess().GetUpdateOwnBank(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region DL20 MASTER

        public List<BusinessPartnerInfo> GetUpdateBusinessPartner(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<BusinessPartnerInfo> retVals = new List<BusinessPartnerInfo>();
                BPMServerDataAccess da = new BPMServerDataAccess();
                retVals = da.GetUpdateBusinessPartner(branchId, lastModifiedDate, serverDate);
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }            
        }

        public List<BranchInfo> GetUpdateBranch(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<BranchInfo> retVals = new List<BranchInfo>();
                BPMServerDataAccess da = new BPMServerDataAccess();
                retVals = da.GetUpdateBranch(branchId, lastModifiedDate, serverDate);
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<MRUInfo> GetUpdateMRU(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<MRUInfo> retVals = new List<MRUInfo>();
                BPMServerDataAccess da = new BPMServerDataAccess();
                retVals = da.GetUpdateMru(branchId, lastModifiedDate, serverDate);
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<MRUPlanInfo> GetUpdateMRUPlan(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<MRUPlanInfo> retVals = new List<MRUPlanInfo>();
                BPMServerDataAccess da = new BPMServerDataAccess();
                retVals = da.GetUpdateMRUPlan(branchId, lastModifiedDate, serverDate);
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        public List<ContractAccountInfo> GetUpdateContractAccount(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<ContractAccountInfo> retVals = new List<ContractAccountInfo>();
                BPMServerDataAccess da = new BPMServerDataAccess();
                retVals = da.GetUpdateContractAccount(branchId, lastModifiedDate, serverDate);
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<EmployeeInfo> GetUpdateEmployee(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<EmployeeInfo> retVals = new List<EmployeeInfo>();
                BPMServerDataAccess da = new BPMServerDataAccess();
                retVals = da.GetUpdateEmployee(branchId, lastModifiedDate, serverDate);
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AgencyAssetInfo> GetUpdateAgencyAsset(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<AgencyAssetInfo> retVals = new List<AgencyAssetInfo>();
                BPMServerDataAccess da = new BPMServerDataAccess();
                retVals = da.GetUpdateAgencyAsset(branchId, lastModifiedDate, serverDate);
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        public List<BankInfo> GetUpdateBank(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<BankInfo> retVals = new List<BankInfo>();
                BPMServerDataAccess da = new BPMServerDataAccess();
                retVals = da.GetUpdateBank(branchId, lastModifiedDate, serverDate);
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
   
        public List<BankAccountInfo> GetUpdateBankAccount(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<BankAccountInfo> retVals = new List<BankAccountInfo>();
                BPMServerDataAccess da = new BPMServerDataAccess();
                retVals = da.GetUpdateBankAccount(branchId, lastModifiedDate, serverDate);
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<MainSubInfo> GetUpdateDebtType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<MainSubInfo> retVals = new List<MainSubInfo>();
                BPMServerDataAccess da = new BPMServerDataAccess();
                retVals = da.GetUpdateDebtType(branchId, lastModifiedDate, serverDate);
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<PaymentSequenceInfo> GetUpdatePaymentSequence(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<PaymentSequenceInfo> retVals = new List<PaymentSequenceInfo>();
                BPMServerDataAccess da = new BPMServerDataAccess();
                retVals = da.GetUpdatePaymentSequence(branchId, lastModifiedDate, serverDate);
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<OldCaMappingInfo> GetUpdateOldCaMapping(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<OldCaMappingInfo> retVals = new List<OldCaMappingInfo>();
                BPMServerDataAccess da = new BPMServerDataAccess();
                retVals = da.GetUpdateOldCaMapping(branchId, lastModifiedDate, serverDate);
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region DL30 BILLING

        public List<PrintPoolInfo> GetUpdatePrintPool(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdatePrintPool(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<GrpPrintPoolInfo> GetUpdateGrpPrintPool(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateGrpPrintPool(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<GreenReceiptDetailInfo> GetUpdateGreenReceiptDetail(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateGreenReceiptDetail(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //public List<GreenReceiptPrintHistoryInfo> GetUpdateGreenReceiptPrintHistory(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        //{
        //    try
        //    {
        //        BPMServerDataAccess da = new BPMServerDataAccess();
        //        return da.GetUpdateGreenReceiptPrintHistory(branchId, lastModifiedDate, serverDate);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        public List<MaxBillSeqNoInfo> GetUpdateMaxBillSeqNo(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateMaxBillSeqNo(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<BillingDetailInfo> GetUpdateBillingDetail(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateBillingDetail(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<PrintHistoryInfo> GetUpdatePrintHistory(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdatePrintHistory(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<DeliveryHistoryInfo> GetUpdateDeliveryHistory(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateDeliveryHistory(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ApproverInfo> GetUpdateApprover(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateApprover(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<DeliveryPlaceInfo> GetUpdateDeliveryPlace(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateDeliveryPlace(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<PWBBillStatusInfo> GetUpdatePWBBillStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdatePWBBillStatus(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<BillUpdateInfo> GetUpdateBillUpdate(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateBillUpdate(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<BarcodeMRUInfo> GetUpdateBarcodeMRU(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateBarcodeMRU(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<BillStatusInfo> GetUpdateBillStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateBillStatus(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        #endregion
        
        #region DL40 AR

        public List<AR> GetUpdateAR(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateAR(branchId, lastModifiedDt, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }            
        }

        public List<DisconnectionDocInfo> GetUpdateDisconnectionDoc(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateDisconnectionDoc(branchId, lastModifiedDt, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<RTDisconnectionDocCaDocInfo> GetUpdateRTDisconnectionDocCaDoc(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateRTDisconnectionDocCaDoc(branchId, lastModifiedDt, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<APInfo> GetUpdateAP(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateAP(branchId, lastModifiedDt, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<APChequePaymentInfo> GetUpdateAPChequePayment(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateAPChequePayment(branchId, lastModifiedDt, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region DL50 PAIDFROMSAP

        public List<ARPayment> GetUpdateARPayment(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            try
            {
                return new BPMServerDataAccess().GetUpdateARPayment(branchId, lastModifiedDt, serverDate);
            }
            catch (Exception e)
            {
                throw;
            } 
        }

        public List<Payment> GetUpdatePayment(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            try
            {
                return new BPMServerDataAccess().GetUpdatePayment(branchId, lastModifiedDt, serverDate);
            }
            catch (Exception e)
            {
                throw;
            } 
        }

        public List<ARPaymentType> GetUpdateARPaymentType(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            try
            {
                return new BPMServerDataAccess().GetUpdateARPaymentType(branchId, lastModifiedDt, serverDate);
            }
            catch (Exception e)
            {
                throw;
            } 
        }

        public List<RTARPaymentTypeARPayment> GetUpdateRTARPaymentTypeARPayment(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            try
            {
                return new BPMServerDataAccess().GetUpdateRTARPaymentTypeARPayment(branchId, lastModifiedDt, serverDate);
            }
            catch (Exception e)
            {
                throw;
            } 
        }

        public List<Receipt> GetUpdateReceipt(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            try
            {
                return new BPMServerDataAccess().GetUpdateReceipt(branchId, lastModifiedDt, serverDate);
            }
            catch (Exception e)
            {
                throw;
            } 
        }

        public List<ReceiptItem> GetUpdateReceiptItem(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            try
            {
                return new BPMServerDataAccess().GetUpdateReceiptItem(branchId, lastModifiedDt, serverDate);
            }
            catch (Exception e)
            {
                throw;
            } 
        }

        #endregion

        #region DL51 CASH MANAGEMENT


        public List<CashierWorkStatusInfo> GetUpdateCashierWorkStatus(string _branchId, DateTime _lastModifiedDate, DateTime _serverDate)
        {
            try
            {
                List<CashierWorkStatusInfo> retVals = new List<CashierWorkStatusInfo>();
                BPMServerDataAccess da = new BPMServerDataAccess();
                retVals = da.GetUpdateCashierWorkStatus(_branchId, _lastModifiedDate, _serverDate);
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CashierMoneyTransferInfo> GetUpdateCashierMoneyTransfer(string _branchId, DateTime _lastModifiedDate, DateTime _serverDate)
        {
            try
            {
                List<CashierMoneyTransferInfo> retVals = new List<CashierMoneyTransferInfo>();
                BPMServerDataAccess da = new BPMServerDataAccess();
                retVals = da.GetUpdateCashierMoneyTransfer(_branchId, _lastModifiedDate, _serverDate);
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CashierMoneyFlowInfo> GetUpdateCashierMoneyFlow(string _branchId, DateTime _lastModifiedDate, DateTime _serverDate)
        {
            try
            {
                List<CashierMoneyFlowInfo> retVals = new List<CashierMoneyFlowInfo>();
                BPMServerDataAccess da = new BPMServerDataAccess();
                retVals = da.GetUpdateCashierMoneyFlow(_branchId, _lastModifiedDate, _serverDate);
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CashierMoneyFlowItemInfo> GetUpdateCashierMoneyFlowItem(string _branchId, DateTime _lastModifiedDate, DateTime _serverDate)
        {
            try
            {
                List<CashierMoneyFlowItemInfo> retVals = new List<CashierMoneyFlowItemInfo>();
                BPMServerDataAccess da = new BPMServerDataAccess();
                retVals = da.GetUpdateCashierMoneyFlowItem(_branchId, _lastModifiedDate, _serverDate);
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region DL60 AGENCY

        public List<BillBookInfo> GetUpdateBillBook(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateBillBook(branchId, lastModifiedDt, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<BillStatusInfoInfo> GetUpdateBillStatusInfo(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateBillStatusInfo(branchId, lastModifiedDt, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<BillBookDetailInfo> GetUpdateBillBookDetail(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateBillBookDetail(branchId, lastModifiedDt, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        
        public List<BillBookInputItemInfo> GetUpdateBillBookInputItem(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateBillBookInputItem(branchId, lastModifiedDt, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<BillBookInputSetInfo> GetUpdateBillBookInputSet(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateBillBookInputSet(branchId, lastModifiedDt, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<AgencyCommissionInfo> GetUpdateAgencyCommission(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateAgencyCommission(branchId, lastModifiedDt, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<RTAgencyContractMruInfo> GetUpdateRTAgencyContractMru(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateRTAgencyContractMru(branchId, lastModifiedDt, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<RTAgencyCommissionBillBookInfo> GetUpdateRTAgencyCommissionBillBook(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateRTAgencyCommissionBillBook(branchId, lastModifiedDt, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //public List<AgencyModuleConfigInfo> GetUpdateAgencyModuleConfig(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        //{
        //    try
        //    {
        //        BPMServerDataAccess da = new BPMServerDataAccess();
        //        return da.GetUpdateAgencyModuleConfig(branchId, lastModifiedDt, serverDate);
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }
        //}

        #endregion

        #region DL61 DOWNLOAD EPAYMENT

        public List<EPayClarifyInfo> GetUpdateEPayClarify(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateEPayClearify(branchId, lastModifiedDt, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<EPayUploadInfo> GetUpdateEPayUpload(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateEPayUpload(branchId, lastModifiedDt, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<EPayUploadItemInfo> GetUpdateEPayUploadItem(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateEPayUploadItem(branchId, lastModifiedDt, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<RTEPayUploadPaymentInfo> GetUpdateRTEPayUploadPayment(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetUpdateRTEPayUploadPayment(branchId, lastModifiedDt, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region DL70 TECHNICAL

        public List<AppSettingInfo> DownloadAppSetting(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.DownloadAppSetting(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<Terminal> DownloadTerminal(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.DownloadTerminal(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<ServiceInfo> DownloadService(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.DownloadService(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<FunctionInfo> DownloadFunction(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.DownloadFunction(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<RoleInfo> DownloadRole(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.DownloadRole(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<UserInfo> DownloadUser(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.DownloadUser(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<FunctionServiceInfo> DownloadFunctionService(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.DownloadFunctionService(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<RoleFunctionInfo> DownloadRoleFunction(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.DownloadRoleFunction(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<RoleUserInfo> DownloadRoleUser(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.DownloadRoleUser(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<UnlockLogInfo> DownloadUnlockLog(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.DownloadUnlockLog(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<UnlockRemarkInfo> DownloadUnlockRemark(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.DownloadUnlockRemark(branchId, lastModifiedDate, serverDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        

        #endregion         

        #region DL71 UPLOAD MASTER

        public int UploadMRUPlan(List<MRUPlanInfo> ars, string branchId)
        {
            try
            {
                BPMServerDataAccess _bpmServerDataAccess = new BPMServerDataAccess();
                return _bpmServerDataAccess.UploadMRUPlan(ars, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region DL80 UPLOAD AR

        public int UploadAR(List<AR> ars, string branchId)
        {
            try
            {
                BPMServerDataAccess _bpmServerDataAccess = new BPMServerDataAccess();
                return _bpmServerDataAccess.UploadAR(ars, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UploadAP(List<APInfo> ars, string branchId)
        {
            try
            {
                BPMServerDataAccess _bpmServerDataAccess = new BPMServerDataAccess();
                return _bpmServerDataAccess.UploadAP(ars, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UploadAPChequePayment(List<APChequePaymentInfo> ars, string branchId)
        {
            try
            {
                BPMServerDataAccess _bpmServerDataAccess = new BPMServerDataAccess();
                return _bpmServerDataAccess.UploadAPChequePayment(ars, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region DL90 UPLOAD PAYMENT
       
        public int UploadARPayment(List<ARPayment> arPayments, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadARPayment(arPayments, branchId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int UploadPayment(List<Payment> payments, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadPayment(payments, branchId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int UploadARPaymentType(List<ARPaymentType> arPaymentTypes, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadARPaymentType(arPaymentTypes, branchId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int UploadRTARPaymentTypeARPayment(List<RTARPaymentTypeARPayment> rts, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadRTARPaymentTypeARPayment(rts, branchId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int UploadReceipt(List<Receipt> receipts, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadReceipt(receipts, branchId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int UploadReceiptItem(List<ReceiptItem> receiptItems, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadReceiptItem(receiptItems, branchId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int UploadPaymentLog(List<PaymentLogInfo> receiptItems, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadPaymentLog(receiptItems, branchId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region DL91 UPLOAD CASH MANAGEMENT


        public int UploadCashierWorkStatus(List<CashierWorkStatusInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadCashierWorkStatus(list, branchId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int UploadCashierMoneyTransfer(List<CashierMoneyTransferInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadCashierMoneyTransfer(list, branchId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int UploadCashierMoneyFlow(List<CashierMoneyFlowInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadCashierMoneyFlow(list, branchId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int UploadCashierMoneyFlowItem(List<CashierMoneyFlowItemInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadCashierMoneyFlowItem(list, branchId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region DL100 UPLOAD AGENCY

        public int UploadBillBook(List<BillBookInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadBillBook(list, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UploadBillStatusInfo(List<BillStatusInfoInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadBillStatusInfo(list, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UploadBillBookDetail(List<BillBookDetailInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadBillBookDetail(list, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UploadBillBookInputItem(List<BillBookInputItemInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadBillBookInputItem(list, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UploadBillBookInputSet(List<BillBookInputSetInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadBillBookInputSet(list, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UploadAgencyCommission(List<AgencyCommissionInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadAgencyCommission(list, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UploadRTAgencyContractMru(List<RTAgencyContractMruInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadRTAgencyContractMru(list, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UploadRTAgencyCommissionBillBook(List<RTAgencyCommissionBillBookInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadRTAgencyCommissionBillBook(list, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion        

        #region DL101 UPLOAD EPAYMENT

        public int UploadEPayClarify(List<EPayClarifyInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadEPayClearify(list, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UploadEPayUpload(List<EPayUploadInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadEPayUpload(list, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UploadEPayUploadItem(List<EPayUploadItemInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadEPayUploadItem(list, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UploadRTEPayUploadPayment(List<RTEPayUploadPaymentInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadRTEPayUploadPayment(list, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region DL110 UPLOAD TECHNICAL

        public int UploadUnlockLog(List<UnlockLogInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadUnlockLog(list, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UploadRoleUser(List<RoleUserInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadRoleUser(list, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UploadUser(List<UserInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadUser(list, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion        
        
        #region DL120 UPLOAD BILLING

        public int UploadPrintPool(List<PrintPoolInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadPrintPool(list, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UploadGrpPrintPool(List<GrpPrintPoolInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadGrpPrintPool(list, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UploadGreenReceiptDetail(List<GreenReceiptDetailInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadGreenReceiptDetail(list, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //public int UploadGreenReceiptPrintHistory(List<GreenReceiptPrintHistoryInfo> list, string branchId)
        //{
        //    try
        //    {
        //        return new BPMServerDataAccess().UploadGreenReceiptPrintHistory(list, branchId);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        public int UploadMaxBillSeqNo(List<MaxBillSeqNoInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadMaxBillSeqNo(list, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UploadPrintHistory(List<PrintHistoryInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadPrintHistory(list, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UploadDeliveryHistory(List<DeliveryHistoryInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadDeliveryHistory(list, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UploadApprover(List<ApproverInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadApprover(list, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UploadDeliveryPlace(List<DeliveryPlaceInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadDeliveryPlace(list, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UploadBarcodeMRU(List<BarcodeMRUInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadBarcodeMRU(list, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UploadBillStatus(List<BillStatusInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadBillStatus(list, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region DL200 UPLOAD EXPORT TABLE

        public int UploadExportTransactionLog(List<ExportTransactionLogInfo> list, string branchId)
        {
            try
            {
                return new BPMServerDataAccess().UploadExportTransactionLog(list, branchId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region GetUpdateServerTime

        public DateTime GetServerTime()
        {
            try
            {
                BPMServerDataAccess da = new BPMServerDataAccess();
                return da.GetServerTime();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region SignalExport

        public void SignalExport(string batchName, string branchId, string modifiedBy)
        {
            try
            {
                //InsertBranchIdForExportingToSAP(branchId, modifiedBy);
                //string batchName = ConfigurationManager.AppSettings["ExportBatchName"];
                string destination = ConfigurationManager.AppSettings["Destination"];
                BPMServerDataAccess da = new BPMServerDataAccess();
                da.EnqueueBatchJob(batchName, destination, new string[] { branchId });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

    }
}

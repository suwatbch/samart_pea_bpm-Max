using System;

using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Integration.BPMIntegration.DA;
using PEA.BPM.Integration.BPMIntegration.Interface;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using System.Data.Common;
using PEA.BPM.Integration.BPMIntegration.Interface.Services;



namespace PEA.BPM.Integration.BPMIntegration.BS
{
    public class BPMBranchService : IBpmBranchService
    {
        #region  Update DL10 Data to branch

        public bool UpdateAccountClass(List<AccountClassInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateAccountClass(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateContractType(List<ContractTypeInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateContractType(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateMeterSize(List<MeterSizeInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateMeterSize(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdatePaymentMethod(List<PaymentMethodInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdatePaymentMethod(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateTaxCode(List<TaxCodeInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateTaxCode(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateUnitType(List<UnitTypeInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateUnitType(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateBusinessPartnerType(List<BusinessPartnerTypeInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateBusinessPartnerType(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }        

        public bool UpdatePaymentType(List<PaymentTypeInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdatePaymentType(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateInterestKey(List<InterestKeyInfo> list, ACABatchParam param)
        {
            try
            {
                return new BPMBranchDataAccess().UpdateInterestKey(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateAgencyBillCollectionStatus(List<AgencyBillCollectionStatusInfo> list, ACABatchParam param)
        {
            try
            {
                return new BPMBranchDataAccess().UpdateAgencyBillCollectionStatus(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateAgencyBillOutStatus(List<AgencyBillOutStatusInfo> list, ACABatchParam param)
        {
            try
            {
                return new BPMBranchDataAccess().UpdateAgencyBillOutStatus(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateAgencyCommissionRate(List<AgencyCommissionRateInfo> list, ACABatchParam param)
        {
            try
            {
                return new BPMBranchDataAccess().UpdateAgencyCommissionRate(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateBillBookStatus(List<BillBookStatusInfo> list, ACABatchParam param)
        {
            try
            {
                return new BPMBranchDataAccess().UpdateBillBookStatus(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateAgencyCollectArea(List<AgencyCollectAreaInfo> list, ACABatchParam param)
        {
            try
            {
                return new BPMBranchDataAccess().UpdateAgencyCollectArea(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateDisconnectionStatus(List<DisconnectionStatusInfo> list, ACABatchParam param)
        {
            try
            {
                return new BPMBranchDataAccess().UpdateDisconnectionStatus(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateCashierMoneyFlowType(List<CashierMoneyFlowTypeInfo> list, ACABatchParam param)
        {
            try
            {
                return new BPMBranchDataAccess().UpdateCashierMoneyFlowType(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateOwnBank(List<OwnBankInfo> list, ACABatchParam param)
        {
            try
            {
                return new BPMBranchDataAccess().UpdateOwnBank(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region Update DL20 Data to branch

        public bool UpdateCalendar(List<CalendarInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateCalendar(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateNonworkingDay(List<NonWorkingDayInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateNonWorkingDay(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateBusinessPartner(List<BusinessPartnerInfo> bizList, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateBusinessPartner(bizList, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateBranch(List<BranchInfo> branchList, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateBranch(branchList, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateMRU(List<MRUInfo> mruList, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateMRU(mruList, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateMRUPlan(List<MRUPlanInfo> mruList, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateMRUPlan(mruList, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateContractAccount(List<ContractAccountInfo> caList, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateContractAccount(caList, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateEmployee(List<EmployeeInfo> empList, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateEmployee(empList, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateAgencyAsset(List<AgencyAssetInfo> agList, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateAgencyAsset(agList, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        public bool UpdateBank(List<BankInfo> bankList, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateBank(bankList, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateBankAccount(List<BankAccountInfo> bankAccountList, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateBankAccount(bankAccountList, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }        

        public bool UpdateDebtType(List<MainSubInfo> dtList, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateDebtType(dtList, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdatePaymentSequence(List<PaymentSequenceInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdatePaymentSequence(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateOldCaMapping(List<OldCaMappingInfo> bizList, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateOldCaMapping(bizList, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region Update DL30 Data to branch

        public bool UpdatePrintPool(List<PrintPoolInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdatePrintPool(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateGrpPrintPool(List<GrpPrintPoolInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateGrpPrintPool(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateGreenReceiptDetail(List<GreenReceiptDetailInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateGreenReceiptDetail(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //public bool UpdateGreenReceiptPrintHistory(List<GreenReceiptPrintHistoryInfo> list, ACABatchParam param)
        //{
        //    try
        //    {
        //        BPMBranchDataAccess da = new BPMBranchDataAccess();
        //        return da.UpdateGreenReceiptPrintHistory(list, param);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        public bool UpdateMaxBillSeqNo(List<MaxBillSeqNoInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateMaxBillSeqNo(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateBillingDetail(List<BillingDetailInfo> blList, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateBillingDetail(blList, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdatePrintHistory(List<PrintHistoryInfo> blList, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdatePrintHistory(blList, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateDeliveryHistory(List<DeliveryHistoryInfo> blList, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateDeliveryHistory(blList, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateApprover(List<ApproverInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateApprover(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateDeliveryPlace(List<DeliveryPlaceInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateDeliveryPlace(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdatePWBBillStatus(List<PWBBillStatusInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdatePWBBillStatus(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateBillUpdate(List<BillUpdateInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateBillUpdate(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateBarcodeMRU(List<BarcodeMRUInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateBarcodeMRU(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateBillStatus(List<BillStatusInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateBillStatus(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region Update DL40 Data to branch

        public bool UpdateAR(List<AR> ars, ACABatchParam param)
        {
            try
            {
                return new BPMBranchDataAccess().UpdateAR(ars, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateDisconnectionDoc(List<DisconnectionDocInfo> list, ACABatchParam param)
        {
            try
            {
                return new BPMBranchDataAccess().UpdateDisconnectionDoc(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateRTDisconnectionDocCaDoc(List<RTDisconnectionDocCaDocInfo> list, ACABatchParam param)
        {
            try
            {
                return new BPMBranchDataAccess().UpdateRTDisconnectionDocCaDoc(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        public bool UpdateAP(List<APInfo> ars, ACABatchParam param)
        {
            try
            {
                return new BPMBranchDataAccess().UpdateAP(ars, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateAPChequePayment(List<APChequePaymentInfo> ars, ACABatchParam param)
        {
            try
            {
                return new BPMBranchDataAccess().UpdateAPChequePayment(ars, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region Update DL50 Data to branch

        public bool UpdateARPayment(List<ARPayment> arPayments, ACABatchParam param)
        {
            try
            {
                return new BPMBranchDataAccess().UpdateARPayment(arPayments, param);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool UpdatePayment(List<Payment> payments,ACABatchParam param)
        {
            try
            {
                return new BPMBranchDataAccess().UpdatePayment(payments, param);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool UpdateARPaymentType(List<ARPaymentType> arPaymentTypes, ACABatchParam param)
        {
            try
            {
                return new BPMBranchDataAccess().UpdateARPaymentType(arPaymentTypes, param);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool UpdateRTARPaymentTypeARPayment(List<RTARPaymentTypeARPayment> rts, ACABatchParam param)
        {
            try
            {
                return new BPMBranchDataAccess().UpdateRTARPaymentTypeARPayment(rts, param);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool UpdateReceipt(List<Receipt> receipts, ACABatchParam param)
        {
            try
            {
                return new BPMBranchDataAccess().UpdateReceipt(receipts, param);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool UpdateReceiptItem(List<ReceiptItem> receiptItems, ACABatchParam param)
        {
            try
            {
                return new BPMBranchDataAccess().UpdateReceiptItem(receiptItems, param);
            }
            catch (Exception e)
            {
                throw;
            }
        }

      

        #endregion

        #region Update DL51 to branch


        public bool UpdateCashierWorkStatus(List<CashierWorkStatusInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateCashierWorkStatus(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateCashierMoneyTransfer(List<CashierMoneyTransferInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateCashierMoneyTransfer(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateCashierMoneyFlow(List<CashierMoneyFlowInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateCashierMoneyFlow(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateCashierMoneyFlowItem(List<CashierMoneyFlowItemInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateCashierMoneyFlowItem(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region Update DL60 Data to branch

        public bool UpdateBillBook(List<BillBookInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateBillBook(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateBillStatusInfo(List<BillStatusInfoInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateBillStatus(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateBillBookDetail(List<BillBookDetailInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateBillBookDetail(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateBillBookInputItem(List<BillBookInputItemInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateBillBookInputItem(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateBillBookInputSet(List<BillBookInputSetInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateBillBookInputSet(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateAgencyCommission(List<AgencyCommissionInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateAgencyCommission(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateRTAgencyContractMru(List<RTAgencyContractMruInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateRTAgencyContractMru(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateRTAgencyCommissionBillBook(List<RTAgencyCommissionBillBookInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateRTAgencyCommissionBillBook(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //public bool UpdateAgencyModuleConfig(List<AgencyModuleConfigInfo> list, ACABatchParam param)
        //{
        //    try
        //    {
        //        BPMBranchDataAccess da = new BPMBranchDataAccess();
        //        return da.UpdateAgencyModuleConfig(list, param);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        #endregion

        #region Update DL61 Data to branch

        public bool UpdateEPayClarify(List<EPayClarifyInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateEPayClearify(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateEPayUpload(List<EPayUploadInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateEPayUpload(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateEPayUploadItem(List<EPayUploadItemInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateEPayUploadItem(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateRTEPayUploadPayment(List<RTEPayUploadPaymentInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateRTEPayUploadPayment(list, param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion        

        #region Update DL70 Data to branch

        public bool UpdateService(List<ServiceInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateService(list, param);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool UpdateFunction(List<FunctionInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateFunction(list, param);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool UpdateRole(List<RoleInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateRole(list,param);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool UpdateUser(List<UserInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateUser(list,param);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool UpdateFunctionService(List<FunctionServiceInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateFunctionService(list,param);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool UpdateRoleFunction(List<RoleFunctionInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateRoleFunction(list,param);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool UpdateRoleUser(List<RoleUserInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateRoleUser(list,param);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool UpdateUnlockLog(List<UnlockLogInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateUnlockLog(list,param);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool UpdateTerminal(List<Terminal> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateTerminal(list,param);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool UpdateAppSetting(List<AppSettingInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateAppSetting(list,param);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool UpdateUnlockRemark(List<UnlockRemarkInfo> list, ACABatchParam param)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.UpdateUnlockRemark(list, param);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion            

        #region Get Data to Upload - Master
        
        public List<MRUPlanInfo> GetToUploadMRUPlan(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadMRUPlan(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region Get Data to Upload - Billing

        public List<PrintPoolInfo> GetToUploadPrintPool(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadPrintPool(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<GrpPrintPoolInfo> GetToUploadGrpPrintPool(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadGrpPrintPool(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<GreenReceiptDetailInfo> GetToUploadGreenReceiptDetail(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadGreenReceiptDetail(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //public List<GreenReceiptPrintHistoryInfo> GetToUploadGreenReceiptPrintHistory(DateTime lastModifiedDate)
        //{
        //    try
        //    {
        //        return new BPMBranchDataAccess().GetToUploadGreenReceiptPrintHistory(lastModifiedDate);
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }
        //}

        public List<MaxBillSeqNoInfo> GetToUploadMaxBillSeqNo(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadMaxBillSeqNo(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<PrintHistoryInfo> GetToUploadPrintHistory(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadPrintHistory(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<DeliveryHistoryInfo> GetToUploadDeliveryHistory(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadDeliveryHistory(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<ApproverInfo> GetToUploadApprover(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadApprover(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<DeliveryPlaceInfo> GetToUploadDeliveryPlace(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadDeliveryPlace(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }


        public List<BarcodeMRUInfo> GetToUploadBarcodeMRU(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadBarcodeMRU(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<BillStatusInfo> GetToUploadBillStatus(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadBillStatus(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region Get Data to Upload - AR

        public List<AR> GetToUploadAR(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadAR(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<APInfo> GetToUploadAP(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadAP(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<APChequePaymentInfo> GetToUploadAPChequePayment(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadAPChequePayment(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region Get Data to Upload - PayFromSAP

        public List<ARPayment> GetToUploadARPayment(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadARPayment(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<Payment> GetToUploadPayment(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadPayment(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<ARPaymentType> GetToUploadARPaymentType(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadARPaymentType(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<RTARPaymentTypeARPayment> GetToUploadRTARPaymentTypeARPayment(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadRTARPaymentTypeARPayment(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<Receipt> GetToUploadReceipt(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadReceipt(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<ReceiptItem> GetToUploadReceiptItem(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadReceiptItem(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<PaymentLogInfo> GetToUploadPaymentLog(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadPaymentLog(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region Get Data to Upload - Cash Management


        public List<CashierWorkStatusInfo> GetToUploadCashierWorkStatus(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadCashierWorkStatus(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<CashierMoneyTransferInfo> GetToUploadCashierMoneyTransfer(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadCashierMoneyTransfer(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<CashierMoneyFlowInfo> GetToUploadCashierMoneyFlow(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadCashierMoneyFlow(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<CashierMoneyFlowItemInfo> GetToUploadCashierMoneyFlowItem(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadCashierMoneyFlowItem(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region Get Data to Upload - Agency

        public List<BillBookInfo> GetToUploadBillBook(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadBillBook(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<BillStatusInfoInfo> GetToUploadBillStatusInfo(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadBillStatusInfo(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<BillBookDetailInfo> GetToUploadBillBookDetail(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadBillBookDetail(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<BillBookInputItemInfo> GetToUploadBillBookInputItem(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadBillBookInputItem(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<BillBookInputSetInfo> GetToUploadBillBookInputSet(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadBillBookInputSet(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<AgencyCommissionInfo> GetToUploadAgencyCommission(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadAgencyCommission(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<RTAgencyContractMruInfo> GetToUploadRTAgencyContractMru(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadRTAgencyContractMru(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<RTAgencyCommissionBillBookInfo> GetToUploadRTAgencyCommissionBillBook(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadRTAgencyCommissionBillBook(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region Get Data to Upload - Technical

        public List<UserInfo> GetToUploadUser(DateTime lastModifiedDate)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.GetToUploadUser(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<RoleUserInfo> GetToUploadRoleUser(DateTime lastModifiedDate)
        {
            try
            {
                BPMBranchDataAccess da = new BPMBranchDataAccess();
                return da.GetToUploadRoleUser(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<UnlockLogInfo> GetToUploadUnlockLog(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadUnlockLog(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region Get Data to Upload - EPayment

        public List<EPayClarifyInfo> GetToUploadEPayClarify(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadEPayClearify(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region  Get Data to Upload - Export Table

        public List<ExportTransactionLogInfo> GetToUploadExportTransactionLog(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().GetToUploadExportTransactionLog(lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region Update SyncFlag - AR

        public int SetSyncAR(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("AR", lastModifiedDate);                 
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncAP(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("AP", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncAPChequePayment(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("APChequePayment", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncARPayment(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("ARPayment", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncPayment(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("Payment", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncARPaymentType(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("ARPaymentType", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncRTARPaymentTypeARPayment(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("RTARPaymentTypeARPayment", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncReceipt(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("Receipt", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncReceiptItem(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("ReceiptItem", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncPaymentLog(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("PaymentLog", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion               

        #region Update SyncFlag - Technical

        public int SetSyncRoleUser(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("RTRoleUser", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncUser(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("User", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncUnlockLog(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("UnlockLog", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }      


        #endregion       
    
        #region Update SyncFlag - Agency


        public int SetSyncBillBook(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("BillBook", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncBillStatusInfo(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("BillStatusInfo", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncBillBookDetail(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("BillBookDetail", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncBillBookInputItem(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("BillBookInputItem", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncBillBookInputSet(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("BillBookInputSet", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncAgencyCommission(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("AgencyCommission", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncRTAgencyContractMru(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("RTAgencyContractMru", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncRTAgencyCommissionBillBook(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("RTAgencyCommissionBillBook", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region Update SyncFlag - Billing

        public int SetSyncBillingDetail(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("BillingDetail", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncPrintHistory(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("PrintHistory", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncDeliveryHistory(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("DeliveryHistory", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncApprover(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("Approver", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncDeliveryPlace(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("DeliveryPlace", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncPrintPool(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("PrintPool", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncGrpPrintPool(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("GrpPrintPool", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncMaxBillSeqNo(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("MaxBillSeqNo", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncGreenReceiptDetail(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("GreenReceiptDetail", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //public int SetSyncGreenReceiptPrintHistory(DateTime lastModifiedDate)
        //{
        //    try
        //    {
        //        return new BPMBranchDataAccess().SetSync("GreenReceiptPrintHistory", lastModifiedDate);
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }
        //}

        public int SetSyncBarcodeMRU(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("BarcodeMRU", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncBillStatus(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("BillStatus", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region Update SyncFlag - Cash Management


        public int SetSyncCashierWorkStatus(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("CashierWorkStatus", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncCashierMoneyTransfer(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("CashierMoneyTransfer", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncCashierMoneyFlow(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("CashierMoneyFlow", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SetSyncCashierMoneyFlowItem(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("CashierMoneyFlowItem", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion        

        #region Update SyncFlag - EPayment

        public int SetSyncEPayClarify(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("EPayClearify", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region Update SyncFlag - Master

        public int SetSyncMRUPlan(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("MRUPlan", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region Update SyncFlag - Export Table

        public int SetSyncExportTransactionLog(DateTime lastModifiedDate)
        {
            try
            {
                return new BPMBranchDataAccess().SetSync("ExportTransactionLog", lastModifiedDate);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region IBpmBranchService Members


        //public bool UpdateBillStatus(List<BillStatusInfo> list, ACABatchParam param)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<BillStatusInfo> GetToUploadBillStatus(DateTime lastModifiedDate)
        //{
        //    throw new NotImplementedException();
        //}

        //public int SetSyncBillStatus(DateTime lastModifiedDate)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading;
using System.Data.Common;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using PEA.BPM.Integration.ACABatchService;
using System.IO;

namespace PEA.BPM.Integration.BPMIntegration.DA
{
    public class BPMBranchDataAccess
    {
        #region Update DL010 Data to Branch

        public bool UpdateNonWorkingDay(List<NonWorkingDayInfo> nwList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (NonWorkingDayInfo obj in nwList)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_NonWorkingDay");
                        db.AddInParameter(cmd, "pNwId", DbType.String, obj.NwId);
                        db.AddInParameter(cmd, "pCdType", DbType.String, obj.CdType);
                        db.AddInParameter(cmd, "pNwDay", DbType.DateTime, obj.NwDay);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        db.ExecuteNonQuery(cmd, trans);
                        lineSuccess++;
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateAccountClass(List<AccountClassInfo> accountList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (AccountClassInfo obj in accountList)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_AccountClass");
                        db.AddInParameter(cmd, "pAccountClassId", DbType.String, obj.AccountClassId);
                        db.AddInParameter(cmd, "pAccountClassName", DbType.String, obj.AccountClassName);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateContractType(List<ContractTypeInfo> contractList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (ContractTypeInfo obj in contractList)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_ContractType");
                        db.AddInParameter(cmd, "pCtId", DbType.String, obj.CtId);
                        db.AddInParameter(cmd, "pCtName", DbType.String, obj.CtName);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateMeterSize(List<MeterSizeInfo> meterList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (MeterSizeInfo obj in meterList)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_MeterSize");
                        db.AddInParameter(cmd, "pMeterSizeId", DbType.String, obj.MeterSizeId);
                        db.AddInParameter(cmd, "pMeterSizeName", DbType.String, obj.MeterSizeName);
                        db.AddInParameter(cmd, "pReConnectMeterRate", DbType.Decimal, obj.ReConnectMeterRate);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdatePaymentMethod(List<PaymentMethodInfo> pmList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (PaymentMethodInfo obj in pmList)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_PaymentMethod");
                        db.AddInParameter(cmd, "pPmId", DbType.String, obj.PmId);
                        db.AddInParameter(cmd, "pPmName", DbType.String, obj.PmName);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateTaxCode(List<TaxCodeInfo> tcList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (TaxCodeInfo obj in tcList)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_TaxCode");
                        db.AddInParameter(cmd, "pTaxCode", DbType.String, obj.TaxCode);
                        db.AddInParameter(cmd, "pTaxName", DbType.String, obj.TaxName);
                        db.AddInParameter(cmd, "pTaxRate", DbType.Decimal, obj.TaxRate);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateUnitType(List<UnitTypeInfo> utList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (UnitTypeInfo obj in utList)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_UnitType");
                        db.AddInParameter(cmd, "pUnitTypeId", DbType.String, obj.UnitTypeId);
                        db.AddInParameter(cmd, "pUnitTypeName", DbType.String, obj.UnitTypeName);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateBusinessPartnerType(List<BusinessPartnerTypeInfo> utList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (BusinessPartnerTypeInfo obj in utList)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_BusinessPartnerType");
                        db.AddInParameter(cmd, "pBpTypeId", DbType.String, obj.BpTypeId);
                        db.AddInParameter(cmd, "pBpTypeDesc", DbType.String, obj.BpTypeDesc);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateCalendar(List<CalendarInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (CalendarInfo obj in list)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_Calendar");
                        db.AddInParameter(cmd, "pCdType", DbType.String, obj.CdType);
                        db.AddInParameter(cmd, "pCdName", DbType.String, obj.CdName);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdatePaymentType(List<PaymentTypeInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (PaymentTypeInfo obj in list)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_PaymentType");
                        db.AddInParameter(cmd, "pPtId", DbType.String, obj.PtId);
                        db.AddInParameter(cmd, "pPtName", DbType.String, obj.PtName);
                        db.AddInParameter(cmd, "pPaymentSeq", DbType.String, obj.PaymentSeq);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateInterestKey(List<InterestKeyInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (InterestKeyInfo obj in list)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_InterestKey");
                        db.AddInParameter(cmd, "pInterestKey", DbType.String, obj.InterestKey);
                        db.AddInParameter(cmd, "pInterestName", DbType.String, obj.InterestName);
                        db.AddInParameter(cmd, "pInterestRate", DbType.Decimal, obj.InterestRate);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateAgencyBillCollectionStatus(List<AgencyBillCollectionStatusInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (AgencyBillCollectionStatusInfo obj in list)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_AgencyBillCollectionStatus");
                        db.AddInParameter(cmd, "pAbsId", DbType.String, obj.AbsId);
                        db.AddInParameter(cmd, "pAbsName", DbType.String, obj.AbsName);
                        db.AddInParameter(cmd, "pDescription", DbType.String, obj.Description);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateAgencyBillOutStatus(List<AgencyBillOutStatusInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (AgencyBillOutStatusInfo obj in list)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_AgencyBillOutStatus");
                        db.AddInParameter(cmd, "pAboId", DbType.String, obj.AboId);
                        db.AddInParameter(cmd, "pAboName", DbType.String, obj.AboName);
                        db.AddInParameter(cmd, "pDescription", DbType.String, obj.Description);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateAgencyCommissionRate(List<AgencyCommissionRateInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (AgencyCommissionRateInfo obj in list)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_AgencyCommissionRate");
                        db.AddInParameter(cmd, "pRtId", DbType.Int32, obj.RtId);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pHouseRegRate", DbType.Decimal, obj.HouseRegRate);
                        db.AddInParameter(cmd, "pHouseGrpRate", DbType.Decimal, obj.HouseGrpRate);
                        db.AddInParameter(cmd, "pCorpRegRate", DbType.Decimal, obj.CorpRegRate);
                        db.AddInParameter(cmd, "pCorpGrpRate", DbType.Decimal, obj.CorpGrpRate);
                        db.AddInParameter(cmd, "pGovRegRate", DbType.Decimal, obj.GovRegRate);
                        db.AddInParameter(cmd, "pGovGrpRate", DbType.Decimal, obj.GovGrpRate);
                        db.AddInParameter(cmd, "pBillingNinetyPercent", DbType.Decimal, obj.BillingNinetyPercent);
                        db.AddInParameter(cmd, "pBillingNinetyNinePercent", DbType.Decimal, obj.BillingNinetyNinePercent);
                        db.AddInParameter(cmd, "pBillingHundredPercent", DbType.Decimal, obj.BillingHundredPercent);
                        db.AddInParameter(cmd, "pMaxInvPercent", DbType.Decimal, obj.MaxInvPercent);
                        db.AddInParameter(cmd, "pInvRate", DbType.Decimal, obj.InvRate);
                        db.AddInParameter(cmd, "pInvPastThreeMonthRate", DbType.Decimal, obj.InvPastThreeMonthRate);
                        db.AddInParameter(cmd, "pFineRatePerBill", DbType.Decimal, obj.FineRatePerBill);
                        db.AddInParameter(cmd, "pCmCountBlock", DbType.Int32, obj.CmCountBlock);
                        db.AddInParameter(cmd, "pCmCountLimit", DbType.Int32, obj.CmCountLimit);
                        db.AddInParameter(cmd, "pVatRate", DbType.Decimal, obj.VatRate);
                        db.AddInParameter(cmd, "pCollectedPercent", DbType.Decimal, obj.CollectedPercent);
                        db.AddInParameter(cmd, "pPenaltyWaive", DbType.String, obj.PenaltyWaive);
                        db.AddInParameter(cmd, "pCaCount", DbType.Int32, obj.CaCount);
                        db.AddInParameter(cmd, "pUpperRate", DbType.Decimal, obj.UpperRate);
                        db.AddInParameter(cmd, "pLowerRate", DbType.Decimal, obj.LowerRate);
                        db.AddInParameter(cmd, "pTaxRate", DbType.Decimal, obj.TaxRate);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateBillBookStatus(List<BillBookStatusInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (BillBookStatusInfo obj in list)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_BillBookStatus");
                        db.AddInParameter(cmd, "pBsId", DbType.String, obj.BsId);
                        db.AddInParameter(cmd, "pBsName", DbType.String, obj.BsName);
                        db.AddInParameter(cmd, "pBsDesc", DbType.String, obj.BsDesc);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateAgencyCollectArea(List<AgencyCollectAreaInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (AgencyCollectAreaInfo obj in list)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_AgencyCollectArea");
                        db.AddInParameter(cmd, "pCaId", DbType.String, obj.CaId);
                        db.AddInParameter(cmd, "pCollectArea", DbType.String, obj.CollectArea);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateDisconnectionStatus(List<DisconnectionStatusInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (DisconnectionStatusInfo obj in list)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_DisconnectionStatus");
                        db.AddInParameter(cmd, "pDiscStatusId", DbType.String, obj.DiscStatusId);
                        db.AddInParameter(cmd, "pDescription", DbType.String, obj.Description);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateCashierMoneyFlowType(List<CashierMoneyFlowTypeInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (CashierMoneyFlowTypeInfo obj in list)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_CashierMoneyFlowType");
                        db.AddInParameter(cmd, "pFlowType", DbType.String, obj.FlowType);
                        db.AddInParameter(cmd, "pFlowDesc", DbType.String, obj.FlowDesc);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateOwnBank(List<OwnBankInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (OwnBankInfo obj in list)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_OwnBank");
                        db.AddInParameter(cmd, "pHouseBank", DbType.String, obj.HouseBank);
                        db.AddInParameter(cmd, "pBankName", DbType.String, obj.BankName);
                        db.AddInParameter(cmd, "pFilter", DbType.String, obj.Filter);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");

                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        #endregion

        #region Update DL020 Data to Branch

        public bool UpdateBusinessPartner(List<BusinessPartnerInfo> bizList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (BusinessPartnerInfo obj in bizList)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_BusinessPartner");
                        db.AddInParameter(cmd, "pBpId", DbType.String, obj.BpId);
                        db.AddInParameter(cmd, "pBpTypeId", DbType.String, obj.BpTypeId);
                        db.AddInParameter(cmd, "pTaxCode", DbType.String, obj.TaxCode);
                        db.AddInParameter(cmd, "pCitizenId", DbType.String, obj.CitizenId);
                        db.AddInParameter(cmd, "pPassportNo", DbType.String, obj.PassportNo);
                        db.AddInParameter(cmd, "pRegisterId", DbType.String, obj.RegisterId);
                        db.AddInParameter(cmd, "pVatCode", DbType.String, obj.VatCode);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateBranch(List<BranchInfo> branchList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (BranchInfo obj in branchList)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_Branch");
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pBranchName", DbType.String, obj.BranchName);
                        db.AddInParameter(cmd, "pBranchName2", DbType.String, obj.BranchName2);
                        db.AddInParameter(cmd, "pBranchNo", DbType.String, obj.BranchNo);
                        db.AddInParameter(cmd, "pAddress", DbType.String, obj.Address);
                        db.AddInParameter(cmd, "pBranchLevel", DbType.String, obj.BranchLevel);
                        db.AddInParameter(cmd, "pBusinessArea", DbType.String, obj.BusinessArea);
                        db.AddInParameter(cmd, "pBusinessPlace", DbType.String, obj.BusinessPlace);
                        db.AddInParameter(cmd, "pCdType", DbType.String, obj.CdType);
                        db.AddInParameter(cmd, "pParentBranchId", DbType.String, obj.ParentBranchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateMRU(List<MRUInfo> mruList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (MRUInfo obj in mruList)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_MRU");
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pMruId", DbType.String, obj.MruId);
                        db.AddInParameter(cmd, "pMruName", DbType.String, obj.MruName);
                        db.AddInParameter(cmd, "pAdvPay", DbType.String, obj.AdvPay);
                        db.AddInParameter(cmd, "pPortionId", DbType.String, obj.PortionId);
                        db.AddInParameter(cmd, "pPortionDesc", DbType.String, obj.PortionDesc);
                        db.AddInParameter(cmd, "pPtcNo", DbType.String, obj.PtcNo);
                        db.AddInParameter(cmd, "pIntownFlag", DbType.String, obj.IntownFlag);
                        db.AddInParameter(cmd, "pReaderType", DbType.String, obj.ReaderType);
                        db.AddInParameter(cmd, "pTravelHelp", DbType.String, obj.TravelHelp);
                        db.AddInParameter(cmd, "pHelpValidFrom", DbType.DateTime, obj.HelpValidFrom);
                        db.AddInParameter(cmd, "pHelpValidTo", DbType.DateTime, obj.HelpValidTo);
                        db.AddInParameter(cmd, "pMeterReaderId", DbType.String, obj.MeterReaderId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateMRUPlan(List<MRUPlanInfo> mruList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                foreach (MRUPlanInfo obj in mruList)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_MruPlan");
                        db.AddInParameter(cmd, "pMruPlanId", DbType.String, obj.MruPlanId);
                        db.AddInParameter(cmd, "pPortionId", DbType.String, obj.PortionId);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pMruId", DbType.String, obj.MruId);
                        db.AddInParameter(cmd, "pPeriod", DbType.String, obj.Period);
                        db.AddInParameter(cmd, "pMeterReadDt", DbType.DateTime, obj.MeterReadDt);
                        db.AddInParameter(cmd, "pMeterReadActDt", DbType.DateTime, obj.MeterReadActDt);
                        db.AddInParameter(cmd, "pTransferDt", DbType.DateTime, obj.TransferDt);
                        db.AddInParameter(cmd, "pTransferActDt", DbType.DateTime, obj.TransferActDt);
                        db.AddInParameter(cmd, "pBillPrintDt", DbType.DateTime, obj.BillPrintDt);
                        db.AddInParameter(cmd, "pBillPrintActDt", DbType.DateTime, obj.BillPrintActDt);
                        db.AddInParameter(cmd, "pBookCreateDt", DbType.DateTime, obj.BookCreateDt);
                        db.AddInParameter(cmd, "pBookCreateActDt", DbType.DateTime, obj.BookCreateActDt);
                        db.AddInParameter(cmd, "pBookCheckInDt", DbType.DateTime, obj.BookCheckInDt);
                        db.AddInParameter(cmd, "pBookCheckinActDt", DbType.DateTime, obj.BookCheckInActDt);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateContractAccount(List<ContractAccountInfo> caList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (ContractAccountInfo obj in caList)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_ContractAccount");
                        db.AddInParameter(cmd, "pCaId", DbType.String, obj.CaId);
                        db.AddInParameter(cmd, "pTechBranchId", DbType.String, obj.TechBranchId);
                        db.AddInParameter(cmd, "pCommBranchId", DbType.String, obj.CommBranchId);
                        db.AddInParameter(cmd, "pMruId", DbType.String, obj.MruId);
                        db.AddInParameter(cmd, "pBpId", DbType.String, obj.BpId);
                        db.AddInParameter(cmd, "pCaName", DbType.String, obj.CaName);
                        db.AddInParameter(cmd, "pReceiptPrintName", DbType.String, obj.ReceiptPrintName);
                        db.AddInParameter(cmd, "pCaAddress", DbType.String, obj.CaAddress);
                        db.AddInParameter(cmd, "pCtId", DbType.String, obj.CtId);
                        db.AddInParameter(cmd, "pPmId", DbType.String, obj.PmId);
                        db.AddInParameter(cmd, "pAccountClassId", DbType.String, obj.AccountClassId);
                        db.AddInParameter(cmd, "pSecurityDeposit", DbType.Decimal, obj.SecurityDeposit);
                        db.AddInParameter(cmd, "pInterestKey", DbType.String, obj.InterestKey);
                        db.AddInParameter(cmd, "pPaidBy", DbType.String, obj.PaidBy);
                        db.AddInParameter(cmd, "pContractValidFrom", DbType.DateTime, obj.ContractValidFrom);
                        db.AddInParameter(cmd, "pContractValidTo", DbType.DateTime, obj.ContractValidTo);
                        db.AddInParameter(cmd, "pMeterSizeId", DbType.String, obj.MeterSizeId);
                        db.AddInParameter(cmd, "pMeterInstallDt", DbType.DateTime, obj.MeterInstallDt);
                        db.AddInParameter(cmd, "pControllerId", DbType.String, obj.ControllerId);
                        db.AddInParameter(cmd, "pGroupIvReceiptType", DbType.String, obj.GroupIvReceiptType);
                        db.AddInParameter(cmd, "pTransportHelp", DbType.Decimal, obj.TransportHelp);
                        db.AddInParameter(cmd, "pPenaltyWaiveFlag", DbType.String, obj.PenaltyWaiveFlag);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;

        }

        public bool UpdateEmployee(List<EmployeeInfo> empList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (EmployeeInfo obj in empList)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_Employee");
                        db.AddInParameter(cmd, "pEmployeeId", DbType.String, obj.EmployeeId);
                        db.AddInParameter(cmd, "pFirstName", DbType.String, obj.FirstName);
                        db.AddInParameter(cmd, "pLastName", DbType.String, obj.LastName);
                        db.AddInParameter(cmd, "pDepartment", DbType.String, obj.Department);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;

        }

        public bool UpdateAgencyAsset(List<AgencyAssetInfo> agencyList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (AgencyAssetInfo obj in agencyList)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_AgencyAsset");
                        db.AddInParameter(cmd, "pAssetId", DbType.String, obj.AssetId);
                        db.AddInParameter(cmd, "pCaId", DbType.String, obj.CaId);
                        db.AddInParameter(cmd, "pAssetType", DbType.String, obj.AssetType);
                        db.AddInParameter(cmd, "pAssetTypeDesc", DbType.String, obj.AssetTypeDesc);
                        db.AddInParameter(cmd, "pAssetValue", DbType.Decimal, obj.AssetValue);
                        db.AddInParameter(cmd, "pStatus", DbType.String, obj.Status);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateBank(List<BankInfo> bankList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (BankInfo obj in bankList)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_Bank");
                        db.AddInParameter(cmd, "pBankKey", DbType.String, obj.BankKey);
                        db.AddInParameter(cmd, "pBankName", DbType.String, obj.BankName);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateBankAccount(List<BankAccountInfo> bankAccountList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();


                foreach (BankAccountInfo obj in bankAccountList)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_BankAccount");
                        db.AddInParameter(cmd, "pBankkey", DbType.String, obj.BankKey);
                        db.AddInParameter(cmd, "pAccountNo", DbType.String, obj.AccountNo);
                        db.AddInParameter(cmd, "pBusinessPlace", DbType.String, obj.BusinessPlace);
                        db.AddInParameter(cmd, "pClearingAccNo", DbType.String, obj.ClearingAccNo);
                        db.AddInParameter(cmd, "pHouseBank", DbType.String, obj.HouseBank);
                        db.AddInParameter(cmd, "pAccountType", DbType.String, obj.AccountType);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateDebtType(List<MainSubInfo> dtList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (MainSubInfo obj in dtList)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_DebtType");
                        db.AddInParameter(cmd, "pDtId", DbType.String, obj.DtId);
                        db.AddInParameter(cmd, "pDtName", DbType.String, obj.DtName);
                        db.AddInParameter(cmd, "pDefaultPaperSize", DbType.String, obj.DefaultPaperSize);
                        db.AddInParameter(cmd, "pDefaultTaxCode", DbType.String, obj.DefaultTaxCode);
                        db.AddInParameter(cmd, "pDefaultInterestKey", DbType.String, obj.DefaultInterestKey);
                        db.AddInParameter(cmd, "pNonInvoicePaymentFlag", DbType.String, obj.NonInvoicePaymentFlag);
                        db.AddInParameter(cmd, "pCategoryPaymentCode", DbType.String, obj.CategoryPaymentCode);
                        db.AddInParameter(cmd, "pModReceiptHeaderFlag", DbType.String, obj.ModReceiptHeaderFlag);
                        db.AddInParameter(cmd, "pIndividualReceiptFlag", DbType.String, obj.IndividualReceiptFlag);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;

        }

        public bool UpdatePaymentSequence(List<PaymentSequenceInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (PaymentSequenceInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_PaymentSequence");
                        db.AddInParameter(cmd, "pPaymentSequence", DbType.String, obj.PaymentSequence);
                        db.AddInParameter(cmd, "pDtId", DbType.String, obj.DtId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateOldCaMapping(List<OldCaMappingInfo> bizList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (OldCaMappingInfo obj in bizList)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_OldCaMapping");
                        db.AddInParameter(cmd, "pOldCaID", DbType.String, obj.OldCaID);
                        db.AddInParameter(cmd, "pNewCaID", DbType.String, obj.NewCaID);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.String, obj.ModifiedDt);

                        //db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }
        #endregion

        #region Update DL030 Data to Branch

        public bool UpdatePrintPool(List<PrintPoolInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (PrintPoolInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_PrintPool");
                        db.AddInParameter(cmd, "pPrintDoc", DbType.String, obj.PrintDoc);
                        db.AddInParameter(cmd, "pPrintType", DbType.Byte, obj.PrintType);
                        db.AddInParameter(cmd, "pPrintBranch", DbType.String, obj.PrintBranch);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pBranchName", DbType.String, obj.BranchName);
                        db.AddInParameter(cmd, "pMruId", DbType.String, obj.MruId);
                        db.AddInParameter(cmd, "pCaId", DbType.String, obj.CaId);
                        db.AddInParameter(cmd, "pCaName", DbType.String, obj.CaName);
                        db.AddInParameter(cmd, "pBillPred", DbType.String, obj.BillPred);
                        db.AddInParameter(cmd, "pAccountClass", DbType.String, obj.AccountClass);
                        db.AddInParameter(cmd, "pSpotBillFlag", DbType.String, obj.SpotBillFlag);
                        db.AddInParameter(cmd, "pA4Addition", DbType.String, obj.A4Addition);
                        db.AddInParameter(cmd, "pReverse", DbType.Byte, obj.Reverse);
                        db.AddInParameter(cmd, "pOrgDoc", DbType.String, obj.OrgDoc);
                        db.AddInParameter(cmd, "pPrintStatus", DbType.Byte, obj.PrintStatus);
                        db.AddInParameter(cmd, "pAgencyFlag", DbType.Byte, obj.AgencyFlag);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateGrpPrintPool(List<GrpPrintPoolInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (GrpPrintPoolInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GrpPrintPool");
                        db.AddInParameter(cmd, "pPrintDoc", DbType.String, obj.PrintDoc);
                        db.AddInParameter(cmd, "pPrintType", DbType.Byte, obj.PrintType);
                        db.AddInParameter(cmd, "pPrintBranch", DbType.String, obj.PrintBranch);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pBranchName", DbType.String, obj.BranchName);
                        db.AddInParameter(cmd, "pMruId", DbType.String, obj.MruId);
                        db.AddInParameter(cmd, "pCaId", DbType.String, obj.CaId);
                        db.AddInParameter(cmd, "pCaName", DbType.String, obj.CaName);
                        db.AddInParameter(cmd, "pBillPred", DbType.String, obj.BillPred);
                        db.AddInParameter(cmd, "pReceiptNo", DbType.String, obj.ReceiptNo);
                        db.AddInParameter(cmd, "pBankKey", DbType.String, obj.BankKey);
                        db.AddInParameter(cmd, "pHouseBank", DbType.String, obj.HouseBank);
                        db.AddInParameter(cmd, "pBankName", DbType.String, obj.BankName);
                        db.AddInParameter(cmd, "pBankDueDate", DbType.String, obj.BankDueDate);
                        db.AddInParameter(cmd, "pMtNo", DbType.String, obj.MtNo);
                        db.AddInParameter(cmd, "pPayer", DbType.String, obj.Payer);
                        db.AddInParameter(cmd, "pPayerName", DbType.String, obj.PayerName);
                        db.AddInParameter(cmd, "pGroupingDate", DbType.String, obj.GroupingDate);
                        db.AddInParameter(cmd, "pGrpInvPaymentDueDate", DbType.String, obj.GrpInvPaymentDueDate);
                        db.AddInParameter(cmd, "pA4Addition", DbType.String, obj.A4Addition);
                        db.AddInParameter(cmd, "pReverse", DbType.Byte, obj.Reverse);
                        db.AddInParameter(cmd, "pOrgDoc", DbType.String, obj.OrgDoc);
                        db.AddInParameter(cmd, "pPrintStatus", DbType.Byte, obj.PrintStatus);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateGreenReceiptDetail(List<GreenReceiptDetailInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (GreenReceiptDetailInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GreenReceiptDetail");
                        db.AddInParameter(cmd, "pReceiptNo", DbType.String, obj.ReceiptNo);
                        db.AddInParameter(cmd, "pReceiptPrintDoc", DbType.String, obj.ReceiptPrintDoc);
                        db.AddInParameter(cmd, "pPrintBranch", DbType.String, obj.PrintBranch);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pBranchName", DbType.String, obj.BranchName);
                        db.AddInParameter(cmd, "pMruId", DbType.String, obj.MruId);
                        db.AddInParameter(cmd, "pCaId", DbType.String, obj.CaId);
                        db.AddInParameter(cmd, "pCaName", DbType.String, obj.CaName);
                        db.AddInParameter(cmd, "pReceiptPeriod", DbType.String, obj.ReceiptPeriod);
                        db.AddInParameter(cmd, "pw_100_sender", DbType.String, obj.W_100_sender);
                        db.AddInParameter(cmd, "pw_110_post_code", DbType.String, obj.W_110_post_code);
                        db.AddInParameter(cmd, "pw_121_mailing_person", DbType.String, obj.W_121_mailing_person);
                        db.AddInParameter(cmd, "pw_122_mailing_person", DbType.String, obj.W_122_mailing_person);
                        db.AddInParameter(cmd, "pw_211_address", DbType.String, obj.W_211_address);
                        db.AddInParameter(cmd, "pw_212_address", DbType.String, obj.W_212_address);
                        db.AddInParameter(cmd, "pw_213_address", DbType.String, obj.W_213_address);
                        db.AddInParameter(cmd, "pw_241_address", DbType.String, obj.W_241_address);
                        db.AddInParameter(cmd, "pw_242_address", DbType.String, obj.W_242_address);
                        db.AddInParameter(cmd, "pw_243_address", DbType.String, obj.W_243_address);
                        db.AddInParameter(cmd, "pw_250_post_code", DbType.String, obj.W_250_post_code);
                        db.AddInParameter(cmd, "pw_1610_invoice_no", DbType.String, obj.W_1610_invoice_no);
                        db.AddInParameter(cmd, "pw_1620_buss_name", DbType.String, obj.W_1620_buss_name);
                        db.AddInParameter(cmd, "pw_1631_name", DbType.String, obj.W_1631_name);
                        db.AddInParameter(cmd, "pw_1632_name", DbType.String, obj.W_1632_name);
                        db.AddInParameter(cmd, "pw_1633_name", DbType.String, obj.W_1633_name);
                        db.AddInParameter(cmd, "pw_1640_device_13_l1", DbType.String, obj.W_1640_device_13_l1);
                        db.AddInParameter(cmd, "pw_1650_rate_cat_13_l2", DbType.String, obj.W_1650_rate_cat_13_l2);
                        db.AddInParameter(cmd, "pw_1660_contract_ac_14_l1", DbType.String, obj.W_1660_contract_ac_14_l1);
                        db.AddInParameter(cmd, "pw_1670_period_15_l1", DbType.String, obj.W_1670_period_15_l1);
                        db.AddInParameter(cmd, "pw_1680_mr_date_15_l2", DbType.String, obj.W_1680_mr_date_15_l2);
                        db.AddInParameter(cmd, "pw_1690_unit_after_16_l1", DbType.String, obj.W_1690_unit_after_16_l1);
                        db.AddInParameter(cmd, "pw_1700_unit_previous_16_l2", DbType.String, obj.W_1700_unit_previous_16_l2);
                        db.AddInParameter(cmd, "pw_1710_consumption_17_l1", DbType.String, obj.W_1710_consumption_17_l1);
                        db.AddInParameter(cmd, "pw_1720_based_amount_18_l1", DbType.String, obj.W_1720_based_amount_18_l1);
                        db.AddInParameter(cmd, "pw_1730_discount_amount_19_l1", DbType.String, obj.W_1730_discount_amount_19_l1);
                        db.AddInParameter(cmd, "pw_1740_disct_descript", DbType.String, obj.W_1740_disct_descript);
                        db.AddInParameter(cmd, "pw_1750_baht", DbType.String, obj.W_1750_baht);
                        db.AddInParameter(cmd, "pw_1760_ft_amount_20_l1", DbType.String, obj.W_1760_ft_amount_20_l1);
                        db.AddInParameter(cmd, "pw_1770_ft_price_20_l2", DbType.String, obj.W_1770_ft_price_20_l2);
                        db.AddInParameter(cmd, "pw_1780_net_before_tax_21_l1", DbType.String, obj.W_1780_net_before_tax_21_l1);
                        db.AddInParameter(cmd, "pw_1790_tax_amount_22_l1", DbType.String, obj.W_1790_tax_amount_22_l1);
                        db.AddInParameter(cmd, "pw_1800_tax_rate_22_l2", DbType.String, obj.W_1800_tax_rate_22_l2);
                        db.AddInParameter(cmd, "pw_1810_total_value_23_l1", DbType.String, obj.W_1810_total_value_23_l1);
                        db.AddInParameter(cmd, "pw_1820_payment_date_24_l1", DbType.String, obj.W_1820_payment_date_24_l1);
                        db.AddInParameter(cmd, "pReceiptPrintStatus", DbType.Byte, obj.ReceiptPrintStatus);
                        db.AddInParameter(cmd, "pHasGrouped", DbType.String, obj.HasGrouped);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pCreatedDt", DbType.DateTime, obj.CreatedDt);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);
                        db.AddInParameter(cmd, "pFileName", DbType.String, obj.FileName);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        //public bool UpdateGreenReceiptPrintHistory(List<GreenReceiptPrintHistoryInfo> list, ACABatchParam param)
        //{
        //    Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
        //    ACABatchLogger logger = new ACABatchLogger();
        //    DbTransaction trans;
        //    int line = 0;
        //    int lineSuccess = 0;

        //    using (DbConnection conn = db.CreateConnection())
        //    {
        //        conn.Open();
        //        trans = conn.BeginTransaction();

        //        foreach (GreenReceiptPrintHistoryInfo obj in list)
        //        {
        //            line++;

        //            try
        //            {
        //                DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GreenReceiptPrintHistory");
        //                db.AddInParameter(cmd, "pReceiptNo", DbType.String, obj.ReceiptNo);
        //                db.AddInParameter(cmd, "pPrintBranch", DbType.String, obj.PrintBranch);
        //                db.AddInParameter(cmd, "pPrintLog", DbType.Int32, obj.PrintLog);
        //                db.AddInParameter(cmd, "pPrintDate", DbType.DateTime, obj.PrintDate);
        //                db.AddInParameter(cmd, "pBillSeqNo", DbType.Int32, obj.BillSeqNo);
        //                db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
        //                db.AddInParameter(cmd, "pMruId", DbType.String, obj.MruId);
        //                db.AddInParameter(cmd, "pReceiptPeriod", DbType.String, obj.ReceiptPeriod);
        //                db.AddInParameter(cmd, "pCaId", DbType.String, obj.CaId);
        //                db.AddInParameter(cmd, "pBankId", DbType.String, obj.BankId);
        //                db.AddInParameter(cmd, "pOrgDoc", DbType.String, obj.OrgDoc);
        //                db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
        //                db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
        //                db.AddInParameter(cmd, "pSyncFlag", DbType.String, obj.SyncFlag);
        //                db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
        //                db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);
        //                db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
        //                lineSuccess += db.ExecuteNonQuery(cmd, trans);
        //            }
        //            catch (Exception e)
        //            {
        //                //aca log 
        //                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
        //                trans.Rollback();
        //                return false;
        //            }
        //        }
        //        trans.Commit();
        //        //log number of line imported
        //        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
        //    }
        //    return true;
        //}

        public bool UpdateBillingDetail(List<BillingDetailInfo> bdList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (BillingDetailInfo obj in bdList)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_BillingDetail");
                        db.AddInParameter(cmd, "pw_01_print_doc", DbType.String, obj.W_01_print_doc);
                        db.AddInParameter(cmd, "pw_10_doc_date", DbType.String, obj.W_10_doc_date);
                        db.AddInParameter(cmd, "pw_20_buss_place", DbType.String, obj.W_20_buss_place);
                        db.AddInParameter(cmd, "pw_30_office_code", DbType.String, obj.W_30_office_code);
                        db.AddInParameter(cmd, "pw_40_sname", DbType.String, obj.W_40_sname);
                        db.AddInParameter(cmd, "pw_80_cust_name1", DbType.String, obj.W_80_cust_name1);
                        db.AddInParameter(cmd, "pw_80_cust_name2", DbType.String, obj.W_80_cust_name2);
                        db.AddInParameter(cmd, "pw_90_cust_name1", DbType.String, obj.W_90_cust_name1);
                        db.AddInParameter(cmd, "pw_90_cust_name2", DbType.String, obj.W_90_cust_name2);
                        db.AddInParameter(cmd, "pw_100_sender", DbType.String, obj.W_100_sender);
                        db.AddInParameter(cmd, "pw_110_post_code", DbType.String, obj.W_110_post_code);
                        db.AddInParameter(cmd, "pw_121_mailing_person", DbType.String, obj.W_121_mailing_person);
                        db.AddInParameter(cmd, "pw_122_mailing_person", DbType.String, obj.W_122_mailing_person);
                        db.AddInParameter(cmd, "pw_130_period", DbType.String, obj.W_130_period);
                        db.AddInParameter(cmd, "pw_140_reg", DbType.String, obj.W_140_reg);
                        db.AddInParameter(cmd, "pw_150_cont_acct", DbType.String, obj.W_150_cont_acct);
                        db.AddInParameter(cmd, "pw_160_device", DbType.String, obj.W_160_device);
                        db.AddInParameter(cmd, "pw_170_rate_cat", DbType.String, obj.W_170_rate_cat);
                        db.AddInParameter(cmd, "pw_171_ettat_code", DbType.String, obj.W_171_ettat_code);
                        db.AddInParameter(cmd, "pw_180_voltage", DbType.String, obj.W_180_voltage);
                        db.AddInParameter(cmd, "pw_190_multi_n", DbType.String, obj.W_190_multi_n);
                        db.AddInParameter(cmd, "pw_191_multi_o", DbType.String, obj.W_191_multi_o);
                        db.AddInParameter(cmd, "pw_200_mr_date", DbType.String, obj.W_200_mr_date);
                        db.AddInParameter(cmd, "pw_216_address", DbType.String, obj.W_216_address);
                        db.AddInParameter(cmd, "pw_217_address", DbType.String, obj.W_217_address);
                        db.AddInParameter(cmd, "pw_218_address", DbType.String, obj.W_218_address);
                        db.AddInParameter(cmd, "pw_221_address", DbType.String, obj.W_221_address);
                        db.AddInParameter(cmd, "pw_222_address", DbType.String, obj.W_222_address);
                        db.AddInParameter(cmd, "pw_223_address", DbType.String, obj.W_223_address);
                        db.AddInParameter(cmd, "pw_230_post_code", DbType.String, obj.W_230_post_code);
                        db.AddInParameter(cmd, "pw_241_address", DbType.String, obj.W_241_address);
                        db.AddInParameter(cmd, "pw_242_address", DbType.String, obj.W_242_address);
                        db.AddInParameter(cmd, "pw_243_address", DbType.String, obj.W_243_address);
                        db.AddInParameter(cmd, "pw_250_post_code", DbType.String, obj.W_250_post_code);
                        db.AddInParameter(cmd, "pw_255_device_1", DbType.String, obj.W_255_device_1);
                        db.AddInParameter(cmd, "pw_260_zwstand_1_txt", DbType.String, obj.W_260_zwstand_1_txt);
                        db.AddInParameter(cmd, "pw_270_zwstvor_1_txt", DbType.String, obj.W_270_zwstvor_1_txt);
                        db.AddInParameter(cmd, "pw_280_umwfakt_1_txt", DbType.String, obj.W_280_umwfakt_1_txt);
                        db.AddInParameter(cmd, "pw_290_abrmenge_1_txt", DbType.String, obj.W_290_abrmenge_1_txt);
                        db.AddInParameter(cmd, "pw_295_device_2", DbType.String, obj.W_295_device_2);
                        db.AddInParameter(cmd, "pw_300_zwstand_2_txt", DbType.String, obj.W_300_zwstand_2_txt);
                        db.AddInParameter(cmd, "pw_310_zwstvor_2_txt", DbType.String, obj.W_310_zwstvor_2_txt);
                        db.AddInParameter(cmd, "pw_320_umwfakt_2_txt", DbType.String, obj.W_320_umwfakt_2_txt);
                        db.AddInParameter(cmd, "pw_330_abrmenge_2_txt", DbType.String, obj.W_330_abrmenge_2_txt);
                        db.AddInParameter(cmd, "pw_340_peak_txt", DbType.String, obj.W_340_peak_txt);
                        db.AddInParameter(cmd, "pw_350_dash_txt", DbType.String, obj.W_350_dash_txt);
                        db.AddInParameter(cmd, "pw_355_device_3", DbType.String, obj.W_355_device_3);
                        db.AddInParameter(cmd, "pw_360_zwstand_3_txt", DbType.String, obj.W_360_zwstand_3_txt);
                        db.AddInParameter(cmd, "pw_370_zwstvor_3_txt", DbType.String, obj.W_370_zwstvor_3_txt);
                        db.AddInParameter(cmd, "pw_380_umwfakt_3_txt", DbType.String, obj.W_380_umwfakt_3_txt);
                        db.AddInParameter(cmd, "pw_390_abrmenge_3_txt", DbType.String, obj.W_390_abrmenge_3_txt);
                        db.AddInParameter(cmd, "pw_400_off_peak_txt", DbType.String, obj.W_400_off_peak_txt);
                        db.AddInParameter(cmd, "pw_410_ene_txt", DbType.String, obj.W_410_ene_txt);
                        db.AddInParameter(cmd, "pw_415_device_4", DbType.String, obj.W_415_device_4);
                        db.AddInParameter(cmd, "pw_420_zwstand_4_txt", DbType.String, obj.W_420_zwstand_4_txt);
                        db.AddInParameter(cmd, "pw_430_zwstvor_4_txt", DbType.String, obj.W_430_zwstvor_4_txt);
                        db.AddInParameter(cmd, "pw_440_umwfakt_4_txt", DbType.String, obj.W_440_umwfakt_4_txt);
                        db.AddInParameter(cmd, "pw_450_abrmenge_4_txt", DbType.String, obj.W_450_abrmenge_4_txt);
                        db.AddInParameter(cmd, "pw_460_hol_txt", DbType.String, obj.W_460_hol_txt);
                        db.AddInParameter(cmd, "pw_470_zwstand_1_txt", DbType.String, obj.W_470_zwstand_1_txt);
                        db.AddInParameter(cmd, "pw_480_zwstvor_1_txt", DbType.String, obj.W_480_zwstvor_1_txt);
                        db.AddInParameter(cmd, "pw_490_consumption_txt", DbType.String, obj.W_490_consumption_txt);
                        db.AddInParameter(cmd, "pw_500_txt6", DbType.String, obj.W_500_txt6);
                        db.AddInParameter(cmd, "pw_510_mr_kw_6_1_txt", DbType.String, obj.W_510_mr_kw_6_1_txt);
                        db.AddInParameter(cmd, "pw_520_mr_kw_6_2_txt", DbType.String, obj.W_520_mr_kw_6_2_txt);
                        db.AddInParameter(cmd, "pw_530_mr_kw_6_3_txt", DbType.String, obj.W_530_mr_kw_6_3_txt);
                        db.AddInParameter(cmd, "pw_540_mr_kw_6_4_txt", DbType.String, obj.W_540_mr_kw_6_4_txt);
                        db.AddInParameter(cmd, "pw_550_mr_kw_6_5", DbType.String, obj.W_550_mr_kw_6_5);
                        db.AddInParameter(cmd, "pw_555_device_6_7", DbType.String, obj.W_555_device_6_7);
                        db.AddInParameter(cmd, "pw_560_mr_kw_7_1_txt", DbType.String, obj.W_560_mr_kw_7_1_txt);
                        db.AddInParameter(cmd, "pw_570_mr_kw_7_2_txt", DbType.String, obj.W_570_mr_kw_7_2_txt);
                        db.AddInParameter(cmd, "pw_580_mr_kw_7_3_txt", DbType.String, obj.W_580_mr_kw_7_3_txt);
                        db.AddInParameter(cmd, "pw_590_mr_kw_7_4_txt", DbType.String, obj.W_590_mr_kw_7_4_txt);
                        db.AddInParameter(cmd, "pw_600_mr_kw_7_5", DbType.String, obj.W_600_mr_kw_7_5);
                        db.AddInParameter(cmd, "pw_610_mr_kw_8_1_txt", DbType.String, obj.W_610_mr_kw_8_1_txt);
                        db.AddInParameter(cmd, "pw_620_mr_kw_8_2_txt", DbType.String, obj.W_620_mr_kw_8_2_txt);
                        db.AddInParameter(cmd, "pw_630_mr_kw_8_3_txt", DbType.String, obj.W_630_mr_kw_8_3_txt);
                        db.AddInParameter(cmd, "pw_635_mr_kw_8_4_txt", DbType.String, obj.W_635_mr_kw_8_4_txt);
                        db.AddInParameter(cmd, "pw_640_mr_kw_8_5", DbType.String, obj.W_640_mr_kw_8_5);
                        db.AddInParameter(cmd, "pw_650_mr_kw_9_1_txt", DbType.String, obj.W_650_mr_kw_9_1_txt);
                        db.AddInParameter(cmd, "pw_660_mr_kw_9_2_txt", DbType.String, obj.W_660_mr_kw_9_2_txt);
                        db.AddInParameter(cmd, "pw_670_mr_kw_9_3_txt", DbType.String, obj.W_670_mr_kw_9_3_txt);
                        db.AddInParameter(cmd, "pw_680_mr_kw_9_4_txt", DbType.String, obj.W_680_mr_kw_9_4_txt);
                        db.AddInParameter(cmd, "pw_690_mr_kw_9_5", DbType.String, obj.W_690_mr_kw_9_5);
                        db.AddInParameter(cmd, "pw_695_device_9_7", DbType.String, obj.W_695_device_9_7);
                        db.AddInParameter(cmd, "pw_700_mr_kw_10_1_txt", DbType.String, obj.W_700_mr_kw_10_1_txt);
                        db.AddInParameter(cmd, "pw_710_mr_kw_10_2_txt", DbType.String, obj.W_710_mr_kw_10_2_txt);
                        db.AddInParameter(cmd, "pw_720_mr_kw_10_3_txt", DbType.String, obj.W_720_mr_kw_10_3_txt);
                        db.AddInParameter(cmd, "pw_730_mr_kw_10_4_txt", DbType.String, obj.W_730_mr_kw_10_4_txt);
                        db.AddInParameter(cmd, "pw_740_mr_kw_10_5", DbType.String, obj.W_740_mr_kw_10_5);
                        db.AddInParameter(cmd, "pw_750_mr_kw_11_1_txt", DbType.String, obj.W_750_mr_kw_11_1_txt);
                        db.AddInParameter(cmd, "pw_760_mr_kw_11_2_txt", DbType.String, obj.W_760_mr_kw_11_2_txt);
                        db.AddInParameter(cmd, "pw_770_mr_kw_11_3_txt", DbType.String, obj.W_770_mr_kw_11_3_txt);
                        db.AddInParameter(cmd, "pw_775_mr_kw_11_4_txt", DbType.String, obj.W_775_mr_kw_11_4_txt);
                        db.AddInParameter(cmd, "pw_780_mr_kw_11_5", DbType.String, obj.W_780_mr_kw_11_5);
                        db.AddInParameter(cmd, "pw_790_mr_kw_12_1_txt", DbType.String, obj.W_790_mr_kw_12_1_txt);
                        db.AddInParameter(cmd, "pw_800_mr_kw_12_2_txt", DbType.String, obj.W_800_mr_kw_12_2_txt);
                        db.AddInParameter(cmd, "pw_810_mr_kw_12_3_txt", DbType.String, obj.W_810_mr_kw_12_3_txt);
                        db.AddInParameter(cmd, "pw_820_mr_kw_12_4_txt", DbType.String, obj.W_820_mr_kw_12_4_txt);
                        db.AddInParameter(cmd, "pw_830_mr_kw_12_5", DbType.String, obj.W_830_mr_kw_12_5);
                        db.AddInParameter(cmd, "pw_835_device_12_7", DbType.String, obj.W_835_device_12_7);
                        db.AddInParameter(cmd, "pw_840_mr_kw_13_1_txt", DbType.String, obj.W_840_mr_kw_13_1_txt);
                        db.AddInParameter(cmd, "pw_850_mr_kw_13_2_txt", DbType.String, obj.W_850_mr_kw_13_2_txt);
                        db.AddInParameter(cmd, "pw_860_mr_kw_13_3_txt", DbType.String, obj.W_860_mr_kw_13_3_txt);
                        db.AddInParameter(cmd, "pw_870_mr_kw_13_4_txt", DbType.String, obj.W_870_mr_kw_13_4_txt);
                        db.AddInParameter(cmd, "pw_890_mr_kw_13_5", DbType.String, obj.W_890_mr_kw_13_5);
                        db.AddInParameter(cmd, "pw_900_mr_kw_14_1_txt", DbType.String, obj.W_900_mr_kw_14_1_txt);
                        db.AddInParameter(cmd, "pw_910_mr_kw_14_2_txt", DbType.String, obj.W_910_mr_kw_14_2_txt);
                        db.AddInParameter(cmd, "pw_920_mr_kw_14_3_txt", DbType.String, obj.W_920_mr_kw_14_3_txt);
                        db.AddInParameter(cmd, "pw_925_mr_kw_14_4_txt", DbType.String, obj.W_925_mr_kw_14_4_txt);
                        db.AddInParameter(cmd, "pw_930_mr_kw_14_5", DbType.String, obj.W_930_mr_kw_14_5);
                        db.AddInParameter(cmd, "pw_940_mr_kw_15_1_txt", DbType.String, obj.W_940_mr_kw_15_1_txt);
                        db.AddInParameter(cmd, "pw_950_mr_kw_15_2_txt", DbType.String, obj.W_950_mr_kw_15_2_txt);
                        db.AddInParameter(cmd, "pw_960_mr_kw_15_3_txt", DbType.String, obj.W_960_mr_kw_15_3_txt);
                        db.AddInParameter(cmd, "pw_970_mr_kw_15_4_txt", DbType.String, obj.W_970_mr_kw_15_4_txt);
                        db.AddInParameter(cmd, "pw_980_mr_kw_15_5", DbType.String, obj.W_980_mr_kw_15_5);
                        db.AddInParameter(cmd, "pw_985_device_15_7", DbType.String, obj.W_985_device_15_7);
                        db.AddInParameter(cmd, "pw_990_mr_kw_16_1_txt", DbType.String, obj.W_990_mr_kw_16_1_txt);
                        db.AddInParameter(cmd, "pw_1000_mr_kw_16_2_txt", DbType.String, obj.W_1000_mr_kw_16_2_txt);
                        db.AddInParameter(cmd, "pw_1010_mr_kw_16_3_txt", DbType.String, obj.W_1010_mr_kw_16_3_txt);
                        db.AddInParameter(cmd, "pw_1020_mr_kw_16_4_txt", DbType.String, obj.W_1020_mr_kw_16_4_txt);
                        db.AddInParameter(cmd, "pw_1030_mr_kw_16_5", DbType.String, obj.W_1030_mr_kw_16_5);
                        db.AddInParameter(cmd, "pw_1040_mr_kw_17_1_txt", DbType.String, obj.W_1040_mr_kw_17_1_txt);
                        db.AddInParameter(cmd, "pw_1050_mr_kw_17_2_txt", DbType.String, obj.W_1050_mr_kw_17_2_txt);
                        db.AddInParameter(cmd, "pw_1060_mr_kw_17_3_txt", DbType.String, obj.W_1060_mr_kw_17_3_txt);
                        db.AddInParameter(cmd, "pw_1065_mr_kw_17_4_txt", DbType.String, obj.W_1065_mr_kw_17_4_txt);
                        db.AddInParameter(cmd, "pw_1070_mr_kw_17_5", DbType.String, obj.W_1070_mr_kw_17_5);
                        db.AddInParameter(cmd, "pw_1080_service_txt", DbType.String, obj.W_1080_service_txt);
                        db.AddInParameter(cmd, "pw_1090_service_support_txt", DbType.String, obj.W_1090_service_support_txt);
                        db.AddInParameter(cmd, "pw_1100_sum_service_support_txt", DbType.String, obj.W_1100_sum_service_support_txt);
                        db.AddInParameter(cmd, "pw_1110_basic_19_1_txt", DbType.String, obj.W_1110_basic_19_1_txt);
                        db.AddInParameter(cmd, "pw_1120_description", DbType.String, obj.W_1120_description);
                        db.AddInParameter(cmd, "pw_1130_kvar_20_1_txt", DbType.String, obj.W_1130_kvar_20_1_txt);
                        db.AddInParameter(cmd, "pw_1140_kvar_20_2_txt", DbType.String, obj.W_1140_kvar_20_2_txt);
                        db.AddInParameter(cmd, "pw_1150_kvar_20_3_txt", DbType.String, obj.W_1150_kvar_20_3_txt);
                        db.AddInParameter(cmd, "pw_1160_kvar_20_4_txt", DbType.String, obj.W_1160_kvar_20_4_txt);
                        db.AddInParameter(cmd, "pw_1170_kvar_21_1_txt", DbType.String, obj.W_1170_kvar_21_1_txt);
                        db.AddInParameter(cmd, "pw_1180_kvar_21_2_txt", DbType.String, obj.W_1180_kvar_21_2_txt);
                        db.AddInParameter(cmd, "pw_1190_kvar_21_3_txt", DbType.String, obj.W_1190_kvar_21_3_txt);
                        db.AddInParameter(cmd, "pw_1200_kvar_21_4_txt", DbType.String, obj.W_1200_kvar_21_4_txt);
                        db.AddInParameter(cmd, "pw_1210_gen_kw_amt_txt", DbType.String, obj.W_1210_gen_kw_amt_txt);
                        db.AddInParameter(cmd, "pw_1220_trans_kw_amt_txt", DbType.String, obj.W_1220_trans_kw_amt_txt);
                        db.AddInParameter(cmd, "pw_1230_dist_kw_amt_txt", DbType.String, obj.W_1230_dist_kw_amt_txt);
                        db.AddInParameter(cmd, "pw_1240_gen_kwh_amt_txt", DbType.String, obj.W_1240_gen_kwh_amt_txt);
                        db.AddInParameter(cmd, "pw_1250_trans_kwh_amt_txt", DbType.String, obj.W_1250_trans_kwh_amt_txt);
                        db.AddInParameter(cmd, "pw_1260_dist_kwh_amt_txt", DbType.String, obj.W_1260_dist_kwh_amt_txt);
                        db.AddInParameter(cmd, "pw_1270_dis_supp_amt_txt", DbType.String, obj.W_1270_dis_supp_amt_txt);
                        db.AddInParameter(cmd, "pw_1280_gen_ft_amt_txt", DbType.String, obj.W_1280_gen_ft_amt_txt);
                        db.AddInParameter(cmd, "pw_1290_trans_ft_amt_txt", DbType.String, obj.W_1290_trans_ft_amt_txt);
                        db.AddInParameter(cmd, "pw_1300_dist_ft_amt_txt", DbType.String, obj.W_1300_dist_ft_amt_txt);
                        db.AddInParameter(cmd, "pw_1310_amount_txt", DbType.String, obj.W_1310_amount_txt);
                        db.AddInParameter(cmd, "pw_1320_foreign_amt", DbType.String, obj.W_1320_foreign_amt);
                        db.AddInParameter(cmd, "pw_1330_foreign_txt", DbType.String, obj.W_1330_foreign_txt);
                        db.AddInParameter(cmd, "pw_1340_foreign_uit", DbType.String, obj.W_1340_foreign_uit);
                        db.AddInParameter(cmd, "pw_1345_blue_txt1", DbType.String, obj.W_1345_blue_txt1);
                        db.AddInParameter(cmd, "pw_1350_ftgen_txt", DbType.String, obj.W_1350_ftgen_txt);
                        db.AddInParameter(cmd, "pw_1360_fttrn_txt", DbType.String, obj.W_1360_fttrn_txt);
                        db.AddInParameter(cmd, "pw_1370_ftdis_txt", DbType.String, obj.W_1370_ftdis_txt);
                        db.AddInParameter(cmd, "pw_1380_fttot_txt", DbType.String, obj.W_1380_fttot_txt);
                        db.AddInParameter(cmd, "pw_1381_ft_peiod_txt", DbType.String, obj.W_1381_ft_peiod_txt);
                        db.AddInParameter(cmd, "pw_1390_ftunit_txt", DbType.String, obj.W_1390_ftunit_txt);
                        db.AddInParameter(cmd, "pw_1400_ftchg_txt", DbType.String, obj.W_1400_ftchg_txt);
                        db.AddInParameter(cmd, "pw_1410_basic_14_6_txt", DbType.String, obj.W_1410_basic_14_6_txt);
                        db.AddInParameter(cmd, "pw_1420_amin_14_7", DbType.String, obj.W_1420_amin_14_7);
                        db.AddInParameter(cmd, "pw_1430_ft_basic_txt", DbType.String, obj.W_1430_ft_basic_txt);
                        db.AddInParameter(cmd, "pw_1440_power_txt", DbType.String, obj.W_1440_power_txt);
                        db.AddInParameter(cmd, "pw_1450_mr_kw_17_6_txt", DbType.String, obj.W_1450_mr_kw_17_6_txt);
                        db.AddInParameter(cmd, "pw_1460_mr_kw_17_7", DbType.String, obj.W_1460_mr_kw_17_7);
                        db.AddInParameter(cmd, "pw_1470_baht", DbType.String, obj.W_1470_baht);
                        db.AddInParameter(cmd, "pw_1480_net_before_vat_txt", DbType.String, obj.W_1480_net_before_vat_txt);
                        db.AddInParameter(cmd, "pw_1485_net_before_vat_left", DbType.String, obj.W_1485_net_before_vat_left);
                        db.AddInParameter(cmd, "pw_1490_tax_rate_txt", DbType.String, obj.W_1490_tax_rate_txt);
                        db.AddInParameter(cmd, "pw_1500_tax_amount_txt", DbType.String, obj.W_1500_tax_amount_txt);
                        db.AddInParameter(cmd, "pw_1505_tax_amount_left", DbType.String, obj.W_1505_tax_amount_left);
                        db.AddInParameter(cmd, "pw_1510_total_amnt_txt", DbType.String, obj.W_1510_total_amnt_txt);
                        db.AddInParameter(cmd, "pw_1550_case_text1", DbType.String, obj.W_1550_case_text1);
                        db.AddInParameter(cmd, "pw_1550_case_text2", DbType.String, obj.W_1550_case_text2);
                        db.AddInParameter(cmd, "pw_1550_case_text3", DbType.String, obj.W_1550_case_text3);
                        db.AddInParameter(cmd, "pw_1550_case_text4", DbType.String, obj.W_1550_case_text4);
                        db.AddInParameter(cmd, "pw_1550_case_text5", DbType.String, obj.W_1550_case_text5);
                        db.AddInParameter(cmd, "pw_1550_case_text6", DbType.String, obj.W_1550_case_text6);
                        db.AddInParameter(cmd, "pw_1550_case_text7", DbType.String, obj.W_1550_case_text7);
                        db.AddInParameter(cmd, "pw_1550_case_text8", DbType.String, obj.W_1550_case_text8);
                        db.AddInParameter(cmd, "pw_1560_spell_amount", DbType.String, obj.W_1560_spell_amount);
                        db.AddInParameter(cmd, "pw_1570_account_number", DbType.String, obj.W_1570_account_number);
                        db.AddInParameter(cmd, "pw_1580_payment_due_date", DbType.String, obj.W_1580_payment_due_date);
                        db.AddInParameter(cmd, "pw_1581_bank_due_date", DbType.String, obj.W_1581_bank_due_date);
                        db.AddInParameter(cmd, "pw_1590_barcode1", DbType.String, obj.W_1590_barcode1);
                        db.AddInParameter(cmd, "pw_1600_barcode2", DbType.String, obj.W_1600_barcode2);
                        db.AddInParameter(cmd, "pw_1830_payment_method", DbType.String, obj.W_1830_payment_method);
                        db.AddInParameter(cmd, "pw_1840_mru", DbType.String, obj.W_1840_mru);
                        db.AddInParameter(cmd, "pw_1841_mru_full", DbType.String, obj.W_1841_mru_full);
                        db.AddInParameter(cmd, "pw_1850_adjust_amt", DbType.String, obj.W_1850_adjust_amt);
                        db.AddInParameter(cmd, "pw_1851_adjust_amt", DbType.String, obj.W_1851_adjust_amt);
                        db.AddInParameter(cmd, "pw_1860_trsg", DbType.String, obj.W_1860_trsg);
                        db.AddInParameter(cmd, "pw_1861_crsg", DbType.String, obj.W_1861_crsg);
                        db.AddInParameter(cmd, "pw_1880_payment_lot", DbType.String, obj.W_1880_payment_lot);
                        db.AddInParameter(cmd, "pw_1890_payer", DbType.String, obj.W_1890_payer);
                        db.AddInParameter(cmd, "pw_1900_absorb_amt_mod", DbType.String, obj.W_1900_absorb_amt_mod);
                        db.AddInParameter(cmd, "pw_1901_discount_amt_pea", DbType.String, obj.W_1901_discount_amt_pea);
                        db.AddInParameter(cmd, "pw_1902_absorb_qty", DbType.String, obj.W_1902_absorb_qty);
                        db.AddInParameter(cmd, "pw_1910_org_doc", DbType.String, obj.W_1910_org_doc);
                        db.AddInParameter(cmd, "pw_1911_reverse", DbType.String, obj.W_1911_reverse);
                        db.AddInParameter(cmd, "pw_1950_collector_id", DbType.String, obj.W_1950_collector_id);
                        db.AddInParameter(cmd, "pw_1960_acct_class", DbType.String, obj.W_1960_acct_class);
                        db.AddInParameter(cmd, "pw_1970_print_dt", DbType.String, obj.W_1970_print_dt);
                        db.AddInParameter(cmd, "pw_1971_print_time", DbType.DateTime, obj.W_1971_print_time);
                        db.AddInParameter(cmd, "pw_1980_spotbill", DbType.String, obj.W_1980_spotbill);
                        db.AddInParameter(cmd, "pw_1990_addition", DbType.String, obj.W_1990_addition);
                        db.AddInParameter(cmd, "pw_2000_dispctrl", DbType.String, obj.W_2000_dispctrl);
                        db.AddInParameter(cmd, "pw_2010_noprnt", DbType.String, obj.W_2010_noprnt);
                        db.AddInParameter(cmd, "pw_2020_noprnt_txt", DbType.String, obj.W_2020_noprnt_txt);
                        db.AddInParameter(cmd, "pw_2030_barcode_a4", DbType.String, obj.W_2030_barcode_a4);
                        db.AddInParameter(cmd, "pw_2040_portion", DbType.String, obj.W_2040_portion);
                        db.AddInParameter(cmd, "pw_2050_mdenr", DbType.String, obj.W_2050_mdenr);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pCreatedDt", DbType.DateTime, obj.CreatedDt);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);
                        db.AddInParameter(cmd, "pFileName", DbType.String, obj.FileName);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);                       
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdatePrintHistory(List<PrintHistoryInfo> bmList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (PrintHistoryInfo obj in bmList)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_PrintHistory");
                        db.AddInParameter(cmd, "pPrintDoc", DbType.String, obj.PrintDoc);
                        db.AddInParameter(cmd, "pPrintType", DbType.Byte, obj.PrintType);
                        db.AddInParameter(cmd, "pPrintBranch", DbType.String, obj.PrintBranch);
                        db.AddInParameter(cmd, "pPrintLog", DbType.Int32, obj.PrintLog);
                        db.AddInParameter(cmd, "pPrintBranchName", DbType.String, obj.PrintBranchName);
                        db.AddInParameter(cmd, "pPrintDate", DbType.DateTime, obj.PrintDate);
                        db.AddInParameter(cmd, "pBillSeqNo", DbType.Int32, obj.BillSeqNo);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pBranchName", DbType.String, obj.BranchName);
                        db.AddInParameter(cmd, "pMruId", DbType.String, obj.MruId);
                        db.AddInParameter(cmd, "pCaId", DbType.String, obj.CaId);
                        db.AddInParameter(cmd, "pCaName", DbType.String, obj.CaName);
                        db.AddInParameter(cmd, "pBillPred", DbType.String, obj.BillPred);
                        db.AddInParameter(cmd, "pMtNo", DbType.String, obj.MtNo);
                        db.AddInParameter(cmd, "pPayer", DbType.String, obj.Payer);
                        db.AddInParameter(cmd, "pPayerName", DbType.String, obj.PayerName);
                        db.AddInParameter(cmd, "pGrpInvPaymentDueDate", DbType.String, obj.GrpInvPaymentDueDate);
                        db.AddInParameter(cmd, "pBankKey", DbType.String, obj.BankKey);
                        db.AddInParameter(cmd, "pHouseBank", DbType.String, obj.HouseBank);
                        db.AddInParameter(cmd, "pBankName", DbType.String, obj.BankName);
                        db.AddInParameter(cmd, "pBankDueDate", DbType.String, obj.BankDueDate);
                        db.AddInParameter(cmd, "pReverse", DbType.Byte, obj.Reverse);
                        db.AddInParameter(cmd, "pOrgDoc", DbType.String, obj.OrgDoc);
                        db.AddInParameter(cmd, "pReceiptNo", DbType.String, obj.ReceiptNo);
                        db.AddInParameter(cmd, "pApproverId", DbType.String, obj.ApproverId);
                        db.AddInParameter(cmd, "pApproverName", DbType.String, obj.ApproverName);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateDeliveryHistory(List<DeliveryHistoryInfo> bmList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (DeliveryHistoryInfo obj in bmList)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_DeliveryHistory");
                        db.AddInParameter(cmd, "pPrintDoc", DbType.String, obj.PrintDoc);
                        db.AddInParameter(cmd, "pPrintType", DbType.Byte, obj.PrintType);
                        db.AddInParameter(cmd, "pPrintBranch", DbType.String, obj.PrintBranch);
                        db.AddInParameter(cmd, "pSentLog", DbType.Int32, obj.SentLog);
                        db.AddInParameter(cmd, "pBillSeqNo", DbType.Int32, obj.BillSeqNo);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pBranchName", DbType.String, obj.BranchName);
                        db.AddInParameter(cmd, "pMruId", DbType.String, obj.MruId);
                        db.AddInParameter(cmd, "pBillPred", DbType.String, obj.BillPred);
                        db.AddInParameter(cmd, "pSentDt", DbType.DateTime, obj.SentDt);
                        db.AddInParameter(cmd, "pDeliveryPred", DbType.String, obj.DeliveryPred);
                        db.AddInParameter(cmd, "pDeliveryPlaceId", DbType.String, obj.DeliveryPlaceId);
                        db.AddInParameter(cmd, "pDeliveryPlaceName", DbType.String, obj.DeliveryPlaceName);
                        db.AddInParameter(cmd, "pOrgDoc", DbType.String, obj.OrgDoc);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);
 
                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateMaxBillSeqNo(List<MaxBillSeqNoInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (MaxBillSeqNoInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_MaxBillSeqNo");
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pPrintType", DbType.Byte, obj.PrintType);
                        db.AddInParameter(cmd, "pMaxNo", DbType.Int32, obj.MaxNo);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }
        
        public bool UpdateApprover(List<ApproverInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (ApproverInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_Approver");
                        db.AddInParameter(cmd, "pApproverId", DbType.String, obj.ApproverId);
                        db.AddInParameter(cmd, "pApproverName", DbType.String, obj.ApproverName);
                        db.AddInParameter(cmd, "pPosition", DbType.String, obj.Position);
                        db.AddInParameter(cmd, "pCreateBranchId", DbType.String, obj.CreateBranchId);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateDeliveryPlace(List<DeliveryPlaceInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (DeliveryPlaceInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_DeliveryPlace");
                        db.AddInParameter(cmd, "pDeliveryPlaceId", DbType.String, obj.DeliveryPlaceId);
                        db.AddInParameter(cmd, "pDeliveryBranchId", DbType.String, obj.DeliveryBranchId);
                        db.AddInParameter(cmd, "pDeliveryPlaceName", DbType.String, obj.DeliveryPlaceName);
                        db.AddInParameter(cmd, "pCreateBranchId", DbType.String, obj.CreateBranchId);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdatePWBBillStatus(List<PWBBillStatusInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (PWBBillStatusInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_PWBBillStatus");
                        db.AddInParameter(cmd, "pBillPred", DbType.String, obj.BillPred);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pMruId", DbType.String, obj.MruId);
                        db.AddInParameter(cmd, "pPortion", DbType.String, obj.Portion);
                        db.AddInParameter(cmd, "pReaderNo", DbType.String, obj.ReaderNo);
                        db.AddInParameter(cmd, "pBranchName", DbType.String, obj.BranchName);
                        db.AddInParameter(cmd, "pCaBlue", DbType.Int32, obj.CaBlue);
                        db.AddInParameter(cmd, "pCaGreen", DbType.Int32, obj.CaGreen);
                        db.AddInParameter(cmd, "pCaA4", DbType.Int32, obj.CaA4);
                        db.AddInParameter(cmd, "pCaSpotBill", DbType.Int32, obj.CaSpotBill);
                        db.AddInParameter(cmd, "pCaGrpInv", DbType.Int32, obj.CaGrpInv);
                        db.AddInParameter(cmd, "pCaTypeF", DbType.Int32, obj.CaTypeF);
                        db.AddInParameter(cmd, "pCaOther", DbType.Int32, obj.CaOther);
                        db.AddInParameter(cmd, "pCaTotal", DbType.Int32, obj.CaTotal);
                        db.AddInParameter(cmd, "pTotPrintBlue", DbType.Int32, obj.TotPrintBlue);
                        db.AddInParameter(cmd, "pNoPrintBlue", DbType.Int32, obj.NoPrintBlue);
                        db.AddInParameter(cmd, "pTotPrintGreen", DbType.Int32, obj.TotPrintGreen);
                        db.AddInParameter(cmd, "pNoPrintGreen", DbType.Int32, obj.NoPrintGreen);
                        db.AddInParameter(cmd, "pTotPrintA4", DbType.Int32, obj.TotPrintA4);
                        db.AddInParameter(cmd, "pNoPrintA4", DbType.Int32, obj.NoPrintA4);
                        db.AddInParameter(cmd, "pTotPrintSpotBill", DbType.Int32, obj.TotPrintSpotBill);
                        db.AddInParameter(cmd, "pNoPrintSpotBill", DbType.Int32, obj.NoPrintSpotBill);
                        db.AddInParameter(cmd, "pTotPrintGrpInv", DbType.Int32, obj.TotPrintGrpInv);
                        db.AddInParameter(cmd, "pNoPrintGrpInv", DbType.Int32, obj.NoPrintGrpInv);
                        db.AddInParameter(cmd, "pTotPrintTypeF", DbType.Int32, obj.TotPrintTypeF);
                        db.AddInParameter(cmd, "pNoPrintTypeF", DbType.Int32, obj.NoPrintTypeF);
                        db.AddInParameter(cmd, "pAnyOther", DbType.Int32, obj.AnyOther);
                        db.AddInParameter(cmd, "pTotalPrint", DbType.Int32, obj.TotalPrint);
                        db.AddInParameter(cmd, "pTotalNoPrint", DbType.Int32, obj.TotalNoPrint);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);

                        db.AddInParameter(cmd, "pAction", DbType.String, obj.Action);
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateBillUpdate(List<BillUpdateInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (BillUpdateInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_BillUpdate");
                        db.AddInParameter(cmd, "pPrintDoc", DbType.String, obj.PrintDoc);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pCrsg", DbType.String, obj.Crsg);
                        db.AddInParameter(cmd, "pTrsg", DbType.String, obj.Trsg);
                        db.AddInParameter(cmd, "pMruId", DbType.String, obj.MruId);
                        db.AddInParameter(cmd, "pCaId", DbType.String, obj.CaId);
                        db.AddInParameter(cmd, "pPmId", DbType.String, obj.PmId);
                        db.AddInParameter(cmd, "pReceiptNo", DbType.String, obj.ReceiptNo);
                        db.AddInParameter(cmd, "pBankId", DbType.String, obj.BankId);
                        db.AddInParameter(cmd, "pBankDueDate", DbType.String, obj.BankDueDate);
                        db.AddInParameter(cmd, "pMtNo", DbType.String, obj.MtNo);
                        db.AddInParameter(cmd, "pGrpInvPaymentDueDate", DbType.String, obj.GrpInvPaymentDueDate);
                        db.AddInParameter(cmd, "pGroupingDate", DbType.String, obj.GroupingDate);
                        db.AddInParameter(cmd, "pFileName", DbType.String, obj.FileName);
                        db.AddInParameter(cmd, "pAction", DbType.String, obj.Action);
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateBarcodeMRU(List<BarcodeMRUInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (BarcodeMRUInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_BarcodeMRU");
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pMruId", DbType.String, obj.MruId);
                        db.AddInParameter(cmd, "pIsPrinted", DbType.String, obj.IsPrinted);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateBillStatus(List<BillStatusInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (BillStatusInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_BillStatus");
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pMruId", DbType.String, obj.MruId);
                        db.AddInParameter(cmd, "pBillPred", DbType.String, obj.BillPred);
                        db.AddInParameter(cmd, "pBranchName", DbType.String, obj.BranchName);
                        db.AddInParameter(cmd, "pPortionId", DbType.String, obj.PortionId);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }
        #endregion

        #region Update DL040 Data to Branch

        public bool UpdateAR(List<AR> arList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (AR obj in arList)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_AR");
                        db.AddInParameter(cmd, "pItemId", DbType.String, obj.ItemId);
                        //db.AddInParameter(cmd, "pCaDoc", DbType.String, obj.CaDoc);
                        db.AddInParameter(cmd, "pCaId", DbType.String, obj.CaId);
                        db.AddInParameter(cmd, "pDtId", DbType.String, obj.DtId);
                        db.AddInParameter(cmd, "pDescription", DbType.String, obj.Description);
                        db.AddInParameter(cmd, "pInvoiceNo", DbType.String, obj.InvoiceNo);
                        db.AddInParameter(cmd, "pInvoiceDt", DbType.DateTime, obj.InvoiceDt);
                        db.AddInParameter(cmd, "pGroupInvoiceNo", DbType.String, obj.GroupInvoiceNo);
                        db.AddInParameter(cmd, "pBillBookId", DbType.String, obj.BillBookId);
                        db.AddInParameter(cmd, "pPeriod", DbType.String, obj.Period);
                        db.AddInParameter(cmd, "pMeterId", DbType.String, obj.MeterId);
                        db.AddInParameter(cmd, "pRateTypeId", DbType.String, obj.RateTypeId);
                        db.AddInParameter(cmd, "pMeterReadDt", DbType.DateTime, obj.MeterReadDt);
                        db.AddInParameter(cmd, "pReadUnit", DbType.Decimal, obj.ReadUnit);
                        db.AddInParameter(cmd, "pLastUnit", DbType.Decimal, obj.LastUnit);
                        db.AddInParameter(cmd, "pBaseAmount", DbType.Decimal, obj.BaseAmount);
                        db.AddInParameter(cmd, "pFTUnitPrice", DbType.Decimal, obj.FTUnitPrice);
                        db.AddInParameter(cmd, "pFTAmount", DbType.Decimal, obj.FTAmount);
                        db.AddInParameter(cmd, "pUnitPrice", DbType.Decimal, obj.UnitPrice);
                        db.AddInParameter(cmd, "pQty", DbType.Decimal, obj.Qty);
                        db.AddInParameter(cmd, "pUnitTypeId", DbType.String, obj.UnitTypeId);
                        db.AddInParameter(cmd, "pAmount", DbType.Decimal, obj.Amount);
                        db.AddInParameter(cmd, "pTaxCode", DbType.String, obj.TaxCode);
                        db.AddInParameter(cmd, "pVatAmount", DbType.Decimal, obj.VatAmount);
                        db.AddInParameter(cmd, "pGAmount", DbType.Decimal, obj.GAmount);
                        db.AddInParameter(cmd, "pDueDt", DbType.DateTime, obj.DueDt);
                        db.AddInParameter(cmd, "pDueDt2", DbType.DateTime, obj.DueDt2);
                        db.AddInParameter(cmd, "pDisconnectDt", DbType.DateTime, obj.DisconnectDt);
                        db.AddInParameter(cmd, "pDisconnectDoc", DbType.String, obj.DisconnectDoc);
                        db.AddInParameter(cmd, "pSubstDocNo", DbType.String, obj.SubstDocNo);
                        db.AddInParameter(cmd, "pOriginalInvoiceNo", DbType.String, obj.OriginalInvoiceNo);
                        db.AddInParameter(cmd, "pSpotBillInvoiceNo", DbType.String, obj.SpotBillInvoiceNo);
                        db.AddInParameter(cmd, "pInterestLockFlag", DbType.String, obj.InterestLockFlag);
                        db.AddInParameter(cmd, "pInterestKey", DbType.String, obj.InterestKey);
                        db.AddInParameter(cmd, "pInstallmentFlag", DbType.String, obj.InstallmentFlag);
                        db.AddInParameter(cmd, "pInstallmentPeriod", DbType.Int32, obj.InstallmentPeriod);
                        db.AddInParameter(cmd, "pInstallmentTotalPeriod", DbType.Int32, obj.InstallmentTotalPeriod);
                        db.AddInParameter(cmd, "pPaymentOrderFlag", DbType.String, obj.PaymentOrderFlag);
                        db.AddInParameter(cmd, "pPaymentOrderDt", DbType.DateTime, obj.PaymentOrderDt);
                        db.AddInParameter(cmd, "pCheckinRefNo", DbType.String, obj.CheckInRefNo);
                        db.AddInParameter(cmd, "pCancelFlag", DbType.String, obj.CancelFlag);
                        db.AddInParameter(cmd, "pModifiedFlag", DbType.String, obj.ModifiedFlag);
                        db.AddInParameter(cmd, "pPOSDebtFlag", DbType.String, obj.POSDebtFlag);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pTechBranchId", DbType.String, obj.TechBranchId);
                        db.AddInParameter(cmd, "pCommBranchId", DbType.String, obj.CommBranchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pCreatedDt", DbType.DateTime, obj.CreatedDt);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);
                        db.AddInParameter(cmd, "pFileName", DbType.String, obj.FileName);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");

                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateDisconnectionDoc(List<DisconnectionDocInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (DisconnectionDocInfo obj in list)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_DisconnectionDoc");
                        db.AddInParameter(cmd, "pDiscNo", DbType.String, obj.DiscNo);
                        db.AddInParameter(cmd, "pCreatedDt", DbType.DateTime, obj.CreatedDt);
                        db.AddInParameter(cmd, "pDiscStatusId", DbType.String, obj.DiscStatusId);
                        db.AddInParameter(cmd, "pReleaseDt", DbType.DateTime, obj.ReleaseDt);
                        db.AddInParameter(cmd, "pWorkOrderNo", DbType.String, obj.WorkOrderNo);
                        db.AddInParameter(cmd, "pDiscPlanStart", DbType.DateTime, obj.DiscPlanStart);
                        db.AddInParameter(cmd, "pDiscPlanEnd", DbType.DateTime, obj.DiscPlanEnd);
                        db.AddInParameter(cmd, "pWorkCenter", DbType.String, obj.WorkCenter);
                        db.AddInParameter(cmd, "pPostpConfirm", DbType.DateTime, obj.PostpConfirm);
                        db.AddInParameter(cmd, "pFuseRemConfirm", DbType.DateTime, obj.FuseRemConfirm);
                        db.AddInParameter(cmd, "pMeterRemConfirm", DbType.DateTime, obj.MeterRemConfirm);
                        db.AddInParameter(cmd, "pTechBranchId", DbType.String, obj.TechBranchId);
                        db.AddInParameter(cmd, "pCommBranchId", DbType.String, obj.CommBranchId);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateRTDisconnectionDocCaDoc(List<RTDisconnectionDocCaDocInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (RTDisconnectionDocCaDocInfo obj in list)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_RTDisconnectionDocCaDoc");
                        db.AddInParameter(cmd, "pDiscNo", DbType.String, obj.DiscNo);
                        db.AddInParameter(cmd, "pCaDocNo", DbType.String, obj.CaDocNo);
                        db.AddInParameter(cmd, "pCancelFlag", DbType.String, obj.CancelFlag);
                        db.AddInParameter(cmd, "pTechBranchId", DbType.String, obj.CaDocNo);
                        db.AddInParameter(cmd, "pCommBranchId", DbType.String, obj.CaDocNo);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateAP(List<APInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (APInfo obj in list)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_AP");
                        db.AddInParameter(cmd, "pAPId", DbType.String, obj.APId);
                        db.AddInParameter(cmd, "pAPPmId", DbType.String, obj.APPmId);
                        db.AddInParameter(cmd, "pAPDtId", DbType.String, obj.APDtId);
                        db.AddInParameter(cmd, "pGLBankKey", DbType.String, obj.GLBankKey);
                        db.AddInParameter(cmd, "pBankName", DbType.String, obj.BankName);
                        db.AddInParameter(cmd, "pClearingAccNo", DbType.String, obj.ClearingAccNo);
                        db.AddInParameter(cmd, "pRefNo", DbType.String, obj.RefNo);
                        db.AddInParameter(cmd, "pCaId", DbType.String, obj.CaId);
                        db.AddInParameter(cmd, "pCaName", DbType.String, obj.CaName);
                        db.AddInParameter(cmd, "pChequeAmount", DbType.Decimal, obj.ChequeAmount);
                        db.AddInParameter(cmd, "pCashAmount", DbType.Decimal, obj.CashAmount);
                        db.AddInParameter(cmd, "pAdjAmount", DbType.Decimal, obj.AdjAmount);
                        db.AddInParameter(cmd, "pPaymentDt", DbType.DateTime, obj.PaymentDt);
                        db.AddInParameter(cmd, "pCancelDt", DbType.DateTime, obj.CancelDt);
                        db.AddInParameter(cmd, "pCancelReason", DbType.String, obj.CancelReason);
                        db.AddInParameter(cmd, "pCancelCashierId", DbType.String, obj.CancelCashierId);
                        db.AddInParameter(cmd, "pCancelCashierName", DbType.String, obj.CancelCashierName);
                        db.AddInParameter(cmd, "pCashierId", DbType.String, obj.CashierId);
                        db.AddInParameter(cmd, "pCashierName", DbType.String, obj.CashierName);
                        db.AddInParameter(cmd, "pPosId", DbType.String, obj.PosId);
                        db.AddInParameter(cmd, "pTerminalCode", DbType.String, obj.TerminalCode);
                        db.AddInParameter(cmd, "pAPQty", DbType.Int32, obj.APQty);
                        db.AddInParameter(cmd, "pSepCheque", DbType.String, obj.SepCheque);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pBranchName", DbType.String, obj.BranchName);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        //db.AddInParameter(cmd, "pExportedOnce", DbType.String, obj.ExportedOnce);
                        db.AddInParameter(cmd, "pPaidFlag", DbType.String, obj.PaidFlag);
                        db.AddInParameter(cmd, "pCanceledFlag", DbType.String, obj.CanceledFlag);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateAPChequePayment(List<APChequePaymentInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (APChequePaymentInfo obj in list)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_APChequePayment");
                        db.AddInParameter(cmd, "pAPId", DbType.String, obj.APId);
                        db.AddInParameter(cmd, "pChqItemId", DbType.String, obj.ChqItemId);
                        db.AddInParameter(cmd, "pBankKey", DbType.String, obj.BankKey);
                        db.AddInParameter(cmd, "pBankName", DbType.String, obj.BankName);
                        db.AddInParameter(cmd, "pChqAccNo", DbType.String, obj.ChqAccNo);
                        db.AddInParameter(cmd, "pChqNo", DbType.String, obj.ChqNo);
                        db.AddInParameter(cmd, "pChqDt", DbType.DateTime, obj.ChqDt);
                        db.AddInParameter(cmd, "pChqAmt", DbType.Decimal, obj.ChqAmt);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }          

        #endregion

        #region Update DL050 Data to Branch

        public bool UpdatePayment(List<Payment> pList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (Payment obj in pList)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_Payment");
                        db.AddInParameter(cmd, "pPaymentId", DbType.String, obj.PaymentId);
                        db.AddInParameter(cmd, "pPaymentDt", DbType.DateTime, obj.PaymentDt);
                        db.AddInParameter(cmd, "pPosId", DbType.String, obj.PosId);
                        db.AddInParameter(cmd, "pTerminalCode", DbType.String, obj.TerminalCode);
                        db.AddInParameter(cmd, "pCashierId", DbType.String, obj.CashierId);
                        db.AddInParameter(cmd, "pCashierName", DbType.String, obj.CashierName);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pBranchName", DbType.String, obj.BranchName);
                        db.AddInParameter(cmd, "pOriginalPaymentId", DbType.String, obj.OriginalPaymentId);
                        db.AddInParameter(cmd, "pPaidChannel", DbType.Byte, obj.PaidChannel);
                        db.AddInParameter(cmd, "pCmScope", DbType.Byte, obj.CmScope);
                        db.AddInParameter(cmd, "pWorkId", DbType.String, obj.WorkId);
                        db.AddInParameter(cmd, "pWorkFlag", DbType.Byte, obj.WorkFlag);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateARPaymentType(List<ARPaymentType> pList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (ARPaymentType obj in pList)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_ARPaymentType");
                        db.AddInParameter(cmd, "pARPtId", DbType.String, obj.ARPtId);
                        db.AddInParameter(cmd, "pPaymentId", DbType.String, obj.PaymentId);
                        db.AddInParameter(cmd, "pAmount", DbType.Decimal, obj.Amount);
                        db.AddInParameter(cmd, "pPtId", DbType.String, obj.PtId);
                        db.AddInParameter(cmd, "pChangeAmount", DbType.Decimal, obj.ChangeAmount);
                        db.AddInParameter(cmd, "pFeeAmount", DbType.Decimal, obj.FeeAmount);
                        db.AddInParameter(cmd, "pBankKey", DbType.String, obj.BankKey);
                        db.AddInParameter(cmd, "pBankName", DbType.String, obj.BankName);
                        db.AddInParameter(cmd, "pGroupBankName", DbType.String, obj.GroupBankName);
                        db.AddInParameter(cmd, "pChqNo", DbType.String, obj.ChqNo);
                        db.AddInParameter(cmd, "pChqAccNo", DbType.String, obj.ChqAccNo);
                        db.AddInParameter(cmd, "pChqDt", DbType.DateTime, obj.ChqDt);
                        db.AddInParameter(cmd, "pDraftFlag", DbType.String, obj.DraftFlag);
                        db.AddInParameter(cmd, "pCashierChequeFlag", DbType.String, obj.CashierChequeFlag);
                        db.AddInParameter(cmd, "pTranfAccNo", DbType.String, obj.TranfAccNo);
                        db.AddInParameter(cmd, "pTranfDt", DbType.DateTime, obj.TranfDt);
                        db.AddInParameter(cmd, "pCancelARPtId", DbType.String, obj.CancelARPtId);
                        db.AddInParameter(cmd, "pClearingAccNo", DbType.String, obj.ClearingAccNo);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateARPayment(List<ARPayment> pList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (ARPayment obj in pList)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_ARPayment");
                        db.AddInParameter(cmd, "pARPmId", DbType.String, obj.ARPmId);
                        db.AddInParameter(cmd, "pInvoiceNo", DbType.String, obj.InvoiceNo);
                        db.AddInParameter(cmd, "pPmId", DbType.String, obj.PmId);
                        db.AddInParameter(cmd, "pDocType", DbType.String, obj.DocType);
                        db.AddInParameter(cmd, "pQty", DbType.Decimal, obj.Qty);
                        db.AddInParameter(cmd, "pVatAmount", DbType.Decimal, obj.VatAmount);
                        db.AddInParameter(cmd, "pGAmount", DbType.Decimal, obj.GAmount);
                        db.AddInParameter(cmd, "pAdjAmount", DbType.Decimal, obj.AdjAmount);
                        db.AddInParameter(cmd, "pCancelARPmId", DbType.String, obj.CancelARPmId);
                        db.AddInParameter(cmd, "pPaymentDt", DbType.DateTime, obj.PaymentDt);
                        db.AddInParameter(cmd, "pPending", DbType.String, obj.Pending);
                        db.AddInParameter(cmd, "pPaidChannel", DbType.Int32, obj.PaidChannel);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pTechBranchId", DbType.String, obj.TechBranchId);
                        db.AddInParameter(cmd, "pCommBranchId", DbType.String, obj.CommBranchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateRTARPaymentTypeARPayment(List<RTARPaymentTypeARPayment> pList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (RTARPaymentTypeARPayment obj in pList)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_RTARPaymentTypeARPayment");
                        db.AddInParameter(cmd, "pARPtId", DbType.String, obj.ARPtId);
                        db.AddInParameter(cmd, "pARPmId", DbType.String, obj.ARPmId);
                        db.AddInParameter(cmd, "pAmount", DbType.Decimal, obj.Amount);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pTechBranchId", DbType.String, obj.TechBranchId);
                        db.AddInParameter(cmd, "pCommBranchId", DbType.String, obj.CommBranchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateReceipt(List<Receipt> pList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (Receipt obj in pList)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_Receipt");
                        db.AddInParameter(cmd, "pReceiptId", DbType.String, obj.ReceiptId);
                        db.AddInParameter(cmd, "pPaymentId", DbType.String, obj.PaymentId);
                        db.AddInParameter(cmd, "pPrintingSequence", DbType.Int16, obj.PrintingSequence);
                        db.AddInParameter(cmd, "pTotalReceipt", DbType.Int16, obj.TotalReceipt);
                        db.AddInParameter(cmd, "pCaId", DbType.String, obj.CaId);
                        db.AddInParameter(cmd, "pName", DbType.String, obj.Name);
                        db.AddInParameter(cmd, "pAddress", DbType.String, obj.Address);
                        db.AddInParameter(cmd, "pIsNameAddrModified", DbType.String, obj.IsNameAddrModified);
                        db.AddInParameter(cmd, "pNoOfPrinting", DbType.Int32, obj.NoOfPrinting);
                        db.AddInParameter(cmd, "pCancelDt", DbType.DateTime, obj.CancelDt);
                        db.AddInParameter(cmd, "pCancelReason", DbType.String, obj.CancelReason);
                        db.AddInParameter(cmd, "pReceiptType", DbType.String, obj.ReceiptType);
                        db.AddInParameter(cmd, "pExtReceiptId", DbType.String, obj.ExtReceiptId);
                        db.AddInParameter(cmd, "pExtReceiptDt", DbType.DateTime, obj.ExtReceiptDt);
                        db.AddInParameter(cmd, "pCaDoc", DbType.String, obj.CaDoc);
                        db.AddInParameter(cmd, "pDescription", DbType.String, obj.Description);
                        db.AddInParameter(cmd, "pInvoiceNo", DbType.String, obj.InvoiceNo);
                        db.AddInParameter(cmd, "pInvoiceDt", DbType.DateTime, obj.InvoiceDt);
                        db.AddInParameter(cmd, "pOriginalInvoiceNo", DbType.String, obj.OriginalInvoiceNo);
                        db.AddInParameter(cmd, "pOriginalInvoiceDt", DbType.DateTime, obj.OriginalInvoiceDt);
                        db.AddInParameter(cmd, "pInstallmentPeriod", DbType.Int32, obj.InstallmentPeriod);
                        db.AddInParameter(cmd, "pInstallmentTotalPeriod", DbType.Int32, obj.InstallmentTotalPeriod);
                        db.AddInParameter(cmd, "pMruId", DbType.String, obj.MruId);
                        db.AddInParameter(cmd, "pMeterId", DbType.String, obj.MeterId);
                        db.AddInParameter(cmd, "pRateTypeId", DbType.String, obj.RateTypeId);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pTechBranchName", DbType.String, obj.TechBranchName);
                        db.AddInParameter(cmd, "pCommBranchId", DbType.String, obj.CommBranchId);
                        db.AddInParameter(cmd, "pCommBranchName", DbType.String, obj.CommBranchName);
                        db.AddInParameter(cmd, "pPeriod", DbType.String, obj.Period);
                        db.AddInParameter(cmd, "pGroupInvoiceNo", DbType.String, obj.GroupInvoiceNo);
                        db.AddInParameter(cmd, "pBillBookId", DbType.String, obj.BillBookId);
                        db.AddInParameter(cmd, "pSpotBillInvoiceNo", DbType.String, obj.SpotBillInvoiceNo);
                        db.AddInParameter(cmd, "pMeterReadDt", DbType.DateTime, obj.MeterReadDt);
                        db.AddInParameter(cmd, "pReadUnit", DbType.Decimal, obj.ReadUnit);
                        db.AddInParameter(cmd, "pLastUnit", DbType.Decimal, obj.LastUnit);
                        db.AddInParameter(cmd, "pFullBaseAmount", DbType.Decimal, obj.FullBaseAmount);
                        db.AddInParameter(cmd, "pFullFTAmount", DbType.Decimal, obj.FullFTAmount);
                        db.AddInParameter(cmd, "pFullQty", DbType.Decimal, obj.FullQty);
                        db.AddInParameter(cmd, "pFullAmount", DbType.Decimal, obj.FullAmount);
                        db.AddInParameter(cmd, "pFullVatAmount", DbType.Decimal, obj.FullVatAmount);
                        db.AddInParameter(cmd, "pFullGAmount", DbType.Decimal, obj.FullGAmount);
                        db.AddInParameter(cmd, "pBaseAmount", DbType.Decimal, obj.BaseAmount);
                        db.AddInParameter(cmd, "pFTUnitPrice", DbType.Decimal, obj.FTUnitPrice);
                        db.AddInParameter(cmd, "pFTAmount", DbType.Decimal, obj.FTAmount);
                        db.AddInParameter(cmd, "pUnitPrice", DbType.Decimal, obj.UnitPrice);
                        db.AddInParameter(cmd, "pQty", DbType.Decimal, obj.Qty);
                        db.AddInParameter(cmd, "pAmount", DbType.Decimal, obj.Amount);
                        db.AddInParameter(cmd, "pVatAmount", DbType.Decimal, obj.VatAmount);
                        db.AddInParameter(cmd, "pGAmount", DbType.Decimal, obj.GAmount);
                        db.AddInParameter(cmd, "pQtyInstallment", DbType.Decimal, obj.QtyInstallment);
                        db.AddInParameter(cmd, "pAmountInstallment", DbType.Decimal, obj.AmountInstallment);
                        db.AddInParameter(cmd, "pVatAmountInstallment", DbType.Decimal, obj.VatAmountInstallment);
                        db.AddInParameter(cmd, "pGAmountInstallment", DbType.Decimal, obj.GAmountInstallment);
                        db.AddInParameter(cmd, "pDtId", DbType.String, obj.DtId);
                        db.AddInParameter(cmd, "pDtName", DbType.String, obj.DtName);
                        db.AddInParameter(cmd, "pControllerId", DbType.String, obj.ControllerId);
                        db.AddInParameter(cmd, "pControllerName", DbType.String, obj.ControllerName);
                        db.AddInParameter(cmd, "pTaxCode", DbType.String, obj.TaxCode);
                        db.AddInParameter(cmd, "pTaxRate", DbType.Decimal, obj.TaxRate);
                        db.AddInParameter(cmd, "pPartialPayment", DbType.Byte, obj.PartialPayment);
                        db.AddInParameter(cmd, "pGroupIvReceiptType", DbType.String, obj.GroupIvReceiptType);
                        db.AddInParameter(cmd, "pPaymentBranchId", DbType.String, obj.PaymentBranchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, param.BranchId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        trans.Rollback();
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateReceiptItem(List<ReceiptItem> pList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (ReceiptItem obj in pList)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_ReceiptItem");
                        db.AddInParameter(cmd, "pReceiptId", DbType.String, obj.ReceiptId);
                        db.AddInParameter(cmd, "pARPmId", DbType.String, obj.ARPmId);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pTechBranchId", DbType.String, obj.TechBranchId);
                        db.AddInParameter(cmd, "pCommBranchId", DbType.String, obj.CommBranchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        #endregion               

        #region Update DL051 Data to Branch

        public bool UpdateCashierWorkStatus(List<CashierWorkStatusInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (CashierWorkStatusInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_CashierWorkStatus");
                        db.AddInParameter(cmd, "pWorkId", DbType.String, obj.WorkId);
                        db.AddInParameter(cmd, "pCashierId", DbType.String, obj.CashierId);
                        db.AddInParameter(cmd, "pCashierName", DbType.String, obj.CashierName);
                        db.AddInParameter(cmd, "pPosId", DbType.String, obj.PosId);
                        db.AddInParameter(cmd, "pTerminalCode", DbType.String, obj.TerminalCode);
                        db.AddInParameter(cmd, "pStatus", DbType.String, obj.Status);
                        db.AddInParameter(cmd, "pOpenWorkDt", DbType.DateTime, obj.OpenWorkDt);
                        db.AddInParameter(cmd, "pCloseWorkDt", DbType.DateTime, obj.CloseWorkDt);
                        db.AddInParameter(cmd, "pCloseWorkBy", DbType.String, obj.CloseWorkBy);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pBaseLine", DbType.Int32, obj.BaseLine);
                        db.AddInParameter(cmd, "pMarkedBL", DbType.String, obj.MarkedBL);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateCashierMoneyTransfer(List<CashierMoneyTransferInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (CashierMoneyTransferInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_CashierMoneyTransfer");
                        db.AddInParameter(cmd, "pTransferId", DbType.String, obj.TransferId);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pSender", DbType.String, obj.Sender);
                        db.AddInParameter(cmd, "pSenderName", DbType.String, obj.SenderName);
                        db.AddInParameter(cmd, "pReceiver", DbType.String, obj.Receiver);
                        db.AddInParameter(cmd, "pReceiverName", DbType.String, obj.ReceiverName);
                        db.AddInParameter(cmd, "pCommander", DbType.String, obj.Commander);
                        db.AddInParameter(cmd, "pStatus", DbType.String, obj.Status);
                        db.AddInParameter(cmd, "pRequestDt", DbType.DateTime, obj.RequestDt);
                        db.AddInParameter(cmd, "pRequestPosId", DbType.String, obj.RequestPosId);
                        db.AddInParameter(cmd, "pResponseDt", DbType.DateTime, obj.ResponseDt);
                        db.AddInParameter(cmd, "pResponsePosId", DbType.String, obj.ResponsePosId);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateCashierMoneyFlow(List<CashierMoneyFlowInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (CashierMoneyFlowInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_CashierMoneyFlow");
                        db.AddInParameter(cmd, "pFlowId", DbType.String, obj.FlowId);
                        db.AddInParameter(cmd, "pFlowType", DbType.String, obj.FlowType);
                        db.AddInParameter(cmd, "pFlowDesc", DbType.String, obj.FlowDesc);
                        db.AddInParameter(cmd, "pFlowCat", DbType.String, obj.FlowCat);
                        db.AddInParameter(cmd, "pGLBankKey", DbType.String, obj.GLBankKey);
                        db.AddInParameter(cmd, "pBankName", DbType.String, obj.BankName);
                        db.AddInParameter(cmd, "pGLAccountNo", DbType.String, obj.GLAccountNo);
                        db.AddInParameter(cmd, "pCashAmt", DbType.Decimal, obj.CashAmt);
                        db.AddInParameter(cmd, "pChequeAmt", DbType.Decimal, obj.ChequeAmt);
                        db.AddInParameter(cmd, "pWorkId", DbType.String, obj.WorkId);
                        db.AddInParameter(cmd, "pCashierId", DbType.String, obj.CashierId);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pTransferId", DbType.String, obj.TransferId);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateCashierMoneyFlowItem(List<CashierMoneyFlowItemInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (CashierMoneyFlowItemInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_CashierMoneyFlowItem");
                        db.AddInParameter(cmd, "pFlowId", DbType.String, obj.FlowId);
                        db.AddInParameter(cmd, "pChqItemId", DbType.String, obj.ChqItemId);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pBankKey", DbType.String, obj.BankKey);
                        db.AddInParameter(cmd, "pChqNo", DbType.String, obj.ChqNo);
                        db.AddInParameter(cmd, "pChqAccNo", DbType.String, obj.ChqAccNo);
                        db.AddInParameter(cmd, "pChqDt", DbType.DateTime, obj.ChqDt);
                        db.AddInParameter(cmd, "pAmount", DbType.Decimal, obj.Amount);
                        db.AddInParameter(cmd, "pValidFlag", DbType.String, obj.ValidFlag);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        #endregion

        #region Update DL060 Data to Branch

        public bool UpdateBillBook(List<BillBookInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (BillBookInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_BillBook");
                        db.AddInParameter(cmd, "pBillBookId", DbType.String, obj.BillBookId);
                        db.AddInParameter(cmd, "pBookHolderId", DbType.String, obj.BookHolderId);
                        db.AddInParameter(cmd, "pBookHolderName", DbType.String, obj.BookHolderName);
                        db.AddInParameter(cmd, "pBookLot", DbType.Int32, obj.BookLot);
                        db.AddInParameter(cmd, "pCreateDate", DbType.DateTime, obj.CreateDate);
                        db.AddInParameter(cmd, "pAdvPayAmount", DbType.Decimal, obj.AdvPayAmount);
                        db.AddInParameter(cmd, "pAdvPayDueDate", DbType.DateTime, obj.AdvPayDueDate);
                        db.AddInParameter(cmd, "pReturnDueDate", DbType.DateTime, obj.ReturnDueDate);
                        db.AddInParameter(cmd, "pCheckInDate", DbType.DateTime, obj.CheckInDate);
                        db.AddInParameter(cmd, "pBookPeriod", DbType.String, obj.BookPeriod);
                        db.AddInParameter(cmd, "pReceiveCount", DbType.Byte, obj.ReceiveCount);
                        db.AddInParameter(cmd, "pBkId", DbType.String, obj.BkId);
                        db.AddInParameter(cmd, "pCreatedBy", DbType.String, obj.CreatedBy);
                        db.AddInParameter(cmd, "pNote", DbType.String, obj.Note);
                        db.AddInParameter(cmd, "pBsId", DbType.String, obj.BsId);
                        db.AddInParameter(cmd, "pAboId", DbType.String, obj.AboId);
                        db.AddInParameter(cmd, "pTotalBillCollected", DbType.Int32, obj.TotalBillCollected);
                        db.AddInParameter(cmd, "pTotalBill", DbType.Int32, obj.TotalBill);
                        db.AddInParameter(cmd, "pBookTotalAmount", DbType.Decimal, obj.BookTotalAmount);
                        db.AddInParameter(cmd, "pBookPaidAmount", DbType.Decimal, obj.BookPaidAmount);
                        db.AddInParameter(cmd, "pBaseAmount", DbType.Decimal, obj.BaseAmount);
                        db.AddInParameter(cmd, "pFTAmount", DbType.Decimal, obj.FTAmount);
                        db.AddInParameter(cmd, "pVatAmount", DbType.Decimal, obj.VatAmount);
                        db.AddInParameter(cmd, "pCreatedBranchId", DbType.String, obj.CreatedBranchId);
                        db.AddInParameter(cmd, "pCreatedBranchName", DbType.String, obj.CreatedBranchName);
                        db.AddInParameter(cmd, "pCollectArea", DbType.String, obj.CollectArea);
                        db.AddInParameter(cmd, "pContractValidFrom", DbType.DateTime, obj.ContractValidFrom);
                        db.AddInParameter(cmd, "pSecurityDeposit", DbType.Decimal, obj.SecurityDeposit);
                        db.AddInParameter(cmd, "pAccountClassId", DbType.String, obj.AccountClassId);
                        db.AddInParameter(cmd, "pBpTypeId", DbType.String, obj.BpTypeId);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateBillStatus(List<BillStatusInfoInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (BillStatusInfoInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_BillStatusInfo");
                        db.AddInParameter(cmd, "pInvoiceNo", DbType.String, obj.InvoiceNo);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pMruId", DbType.String, obj.MruId);
                        db.AddInParameter(cmd, "pCaId", DbType.String, obj.CaId);
                        db.AddInParameter(cmd, "pPeriod", DbType.String, obj.Period);
                        db.AddInParameter(cmd, "pAbsId", DbType.String, obj.AbsId);
                        db.AddInParameter(cmd, "pAboId", DbType.String, obj.AboId);
                        db.AddInParameter(cmd, "pPmId", DbType.String, obj.PmId);
                        db.AddInParameter(cmd, "pAllowRepeated", DbType.String, obj.AllowRepeated);
                        db.AddInParameter(cmd, "pInBook", DbType.String, obj.InBook);
                        db.AddInParameter(cmd, "pRateCatId", DbType.String, obj.RateCatId);
                        db.AddInParameter(cmd, "pPaidBy", DbType.String, obj.PaidBy);
                        db.AddInParameter(cmd, "pFt", DbType.Decimal, obj.Ft);
                        db.AddInParameter(cmd, "pVat", DbType.Decimal, obj.Vat);
                        db.AddInParameter(cmd, "pBaseAmount", DbType.Decimal, obj.BaseAmount);
                        db.AddInParameter(cmd, "pTotalAmount", DbType.Decimal, obj.TotalAmount);
                        db.AddInParameter(cmd, "pPaidType", DbType.String, obj.PaidType);
                        db.AddInParameter(cmd, "pPrintBranch", DbType.String, obj.PrintBranch);
                        db.AddInParameter(cmd, "pOrgDoc", DbType.String, obj.OrgDoc);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateBillBookDetail(List<BillBookDetailInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (BillBookDetailInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_BillBookDetail");
                        db.AddInParameter(cmd, "pInvoiceNo", DbType.String, obj.InvoiceNo);
                        db.AddInParameter(cmd, "pBillBookId", DbType.String, obj.BillBookId);
                        db.AddInParameter(cmd, "pPeriod", DbType.String, obj.Period);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pBranchName", DbType.String, obj.BranchName);
                        db.AddInParameter(cmd, "pMruId", DbType.String, obj.MruId);
                        db.AddInParameter(cmd, "pCaId", DbType.String, obj.CaId);
                        db.AddInParameter(cmd, "pCaName", DbType.String, obj.CaName);
                        db.AddInParameter(cmd, "pCaAddress", DbType.String, obj.CaAddress);
                        db.AddInParameter(cmd, "pAbsId", DbType.String, obj.AbsId);
                        db.AddInParameter(cmd, "pPmId", DbType.String, obj.PmId);
                        db.AddInParameter(cmd, "pInvSelected", DbType.String, obj.InvSelected);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateBillBookInputItem(List<BillBookInputItemInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (BillBookInputItemInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_BillBookInputItem");
                        db.AddInParameter(cmd, "pBIpItemId", DbType.String, obj.BIpItemId);
                        db.AddInParameter(cmd, "pFilterType", DbType.String, obj.FilterType);
                        db.AddInParameter(cmd, "pBillBookId", DbType.String, obj.BillBookId);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pMruId", DbType.String, obj.MruId);
                        db.AddInParameter(cmd, "pCaId", DbType.String, obj.CaId);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateBillBookInputSet(List<BillBookInputSetInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (BillBookInputSetInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_BillBookInputSet");
                        db.AddInParameter(cmd, "pBIpSetId", DbType.String, obj.BIpSetId);
                        db.AddInParameter(cmd, "pBillBookId", DbType.String, obj.BillBookId);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pMruId", DbType.String, obj.MruId);
                        db.AddInParameter(cmd, "pBillPeriodType", DbType.String, obj.BillPeriodType);
                        db.AddInParameter(cmd, "pBillOutType", DbType.String, obj.BillOutType);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateAgencyCommission(List<AgencyCommissionInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (AgencyCommissionInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_AgencyCommission");
                        db.AddInParameter(cmd, "pCmId", DbType.String, obj.CmId);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pCalDt", DbType.DateTime, obj.CalDt);
                        db.AddInParameter(cmd, "pCalCount", DbType.Int32, obj.CalCount);
                        db.AddInParameter(cmd, "pCmAmount", DbType.Decimal, obj.CmAmount);
                        db.AddInParameter(cmd, "pBaseCmAmount", DbType.Decimal, obj.BaseCmAmount);
                        db.AddInParameter(cmd, "pSpecialCmAmount", DbType.Decimal, obj.SpecialCmAmount);
                        db.AddInParameter(cmd, "pInvCmAmount", DbType.Decimal, obj.InvCmAmount);
                        db.AddInParameter(cmd, "pOverNinety", DbType.String, obj.OverNinety);
                        db.AddInParameter(cmd, "pFineAmount", DbType.Decimal, obj.FineAmount);
                        db.AddInParameter(cmd, "pTaxAmount", DbType.Decimal, obj.TaxAmount);
                        db.AddInParameter(cmd, "pVatAmount", DbType.Decimal, obj.VatAmount);
                        db.AddInParameter(cmd, "pTransCost", DbType.Decimal, obj.TransCost);
                        db.AddInParameter(cmd, "pFarLandHelp", DbType.Decimal, obj.FarLandHelp);
                        db.AddInParameter(cmd, "pSpecialMoney", DbType.Decimal, obj.SpecialMoney);
                        db.AddInParameter(cmd, "pRtId", DbType.Int32, obj.RtId);
                        db.AddInParameter(cmd, "pFineEnabled", DbType.String, obj.FineEnabled);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateRTAgencyContractMru(List<RTAgencyContractMruInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (RTAgencyContractMruInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_RTAgencyContractMru");
                        db.AddInParameter(cmd, "pAgentMruId", DbType.String, obj.AgentMruId);
                        db.AddInParameter(cmd, "pCaId", DbType.String, obj.CaId);
                        db.AddInParameter(cmd, "pCaName", DbType.String, obj.CaName);
                        db.AddInParameter(cmd, "pCaBranchId", DbType.String, obj.CaBranchId);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pBranchName", DbType.String, obj.BranchName);
                        db.AddInParameter(cmd, "pMruId", DbType.String, obj.MruId);
                        db.AddInParameter(cmd, "pSecurityDeposit", DbType.Decimal, obj.SecurityDeposit);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateRTAgencyCommissionBillBook(List<RTAgencyCommissionBillBookInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (RTAgencyCommissionBillBookInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_RTAgencyCommissionBillBook");
                        db.AddInParameter(cmd, "pCmId", DbType.String, obj.CmId);
                        db.AddInParameter(cmd, "pBillBookId", DbType.String, obj.BillBookId);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        //discharge
        //public bool UpdateAgencyModuleConfig(List<AgencyModuleConfigInfo> list, ACABatchParam param)
        //{
        //    Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
        //    ACABatchLogger logger = new ACABatchLogger();
        //    DbTransaction trans;
        //    int line = 0;
        //    int lineSuccess = 0;

        //    using (DbConnection conn = db.CreateConnection())
        //    {
        //        conn.Open();
        //        trans = conn.BeginTransaction();

        //        foreach (AgencyModuleConfigInfo obj in list)
        //        {
        //            line++;

        //            try
        //            {
        //                DbCommand cmd = db.GetStoredProcCommand("ta_syncb_AgencyModuleConfig");
        //                db.AddInParameter(cmd, "pDebitAcc", DbType.String, obj.DebitAcc);
        //                db.AddInParameter(cmd, "pCmCountBlock", DbType.String, obj.CmCountBlock);
        //                db.AddInParameter(cmd, "pCmCountLimit", DbType.Int32, obj.CmCountLimit);
        //                db.AddInParameter(cmd, "pVatRate", DbType.Decimal, obj.VatRate);
        //                db.AddInParameter(cmd, "pTaxRate", DbType.Decimal, obj.TaxRate);
        //                db.AddInParameter(cmd, "pCollectedPercent", DbType.Decimal, obj.CollectedPercent);
        //                db.AddInParameter(cmd, "pCaCount", DbType.Int32, obj.CaCount);
        //                db.AddInParameter(cmd, "pUpperRate", DbType.Decimal, obj.UpperRate);
        //                db.AddInParameter(cmd, "pLowerRate", DbType.Decimal, obj.LowerRate);
        //                db.AddInParameter(cmd, "pPenaltyWaiveFlag", DbType.String, obj.PenaltyWaiveFlag);
        //                db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
        //                db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
        //                db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);
        //                db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
        //                lineSuccess += db.ExecuteNonQuery(cmd, trans);
        //            }
        //            catch (Exception e)
        //            {
        //                //aca log 
        //                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
        //                trans.Rollback();
        //                return false;
        //            }
        //        }
        //        trans.Commit();
        //        //log number of line imported
        //        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
        //    }
        //    return true;
        //}

        #endregion

        #region Update DL061 Data to Branch

        public bool UpdateEPayClearify(List<EPayClarifyInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (EPayClarifyInfo obj in list)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_EPayClarify");
                        db.AddInParameter(cmd, "pIssueId", DbType.String, obj.IssueId);
                        db.AddInParameter(cmd, "pEPayItemId", DbType.String, obj.EPayItemId);
                        db.AddInParameter(cmd, "pInvoiceNo", DbType.String, obj.InvoiceNo);
                        db.AddInParameter(cmd, "pNewInvoiceNo", DbType.String, obj.NewInvoiceNo);
                        db.AddInParameter(cmd, "pReceiptInvoiceNo", DbType.String, obj.ReceiptInvoiceNo);
                        db.AddInParameter(cmd, "pFixedType", DbType.String, obj.FixedType);
                        db.AddInParameter(cmd, "pFixedDt", DbType.DateTime, obj.FixedDt);
                        db.AddInParameter(cmd, "pFixedBy", DbType.String, obj.FixedBy);
                        db.AddInParameter(cmd, "pRefDocNo", DbType.String, obj.RefDocNo);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateEPayUpload(List<EPayUploadInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (EPayUploadInfo obj in list)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_EPayUpload");
                        db.AddInParameter(cmd, "pFileId", DbType.String, obj.FileId);
                        db.AddInParameter(cmd, "pCompanyId", DbType.String, obj.CompanyId);
                        db.AddInParameter(cmd, "pAgentId", DbType.String, obj.AgentId);
                        db.AddInParameter(cmd, "pAgentName", DbType.String, obj.AgentName);
                        db.AddInParameter(cmd, "pAccountClassId", DbType.String, obj.AccountClassId);
                        db.AddInParameter(cmd, "pAccountClassName", DbType.String, obj.AccountClassName);
                        db.AddInParameter(cmd, "pFileName", DbType.String, obj.FileName);
                        db.AddInParameter(cmd, "pUploadDt", DbType.DateTime, obj.UploadDt);
                        db.AddInParameter(cmd, "pBillCount", DbType.Int32, obj.BillCount);
                        db.AddInParameter(cmd, "pBillAmount", DbType.Decimal, obj.BillAmount);
                        db.AddInParameter(cmd, "pTotalBillCount", DbType.Int32, obj.TotalBillCount);
                        db.AddInParameter(cmd, "pTotalBillAmount", DbType.Decimal, obj.TotalBillAmount);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateEPayUploadItem(List<EPayUploadItemInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (EPayUploadItemInfo obj in list)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_EPayUploadItem");
                        db.AddInParameter(cmd, "pEPayItemId", DbType.String, obj.EPayItemId);
                        db.AddInParameter(cmd, "pFileId", DbType.String, obj.FileId);
                        db.AddInParameter(cmd, "pRegional", DbType.String, obj.Regional);
                        db.AddInParameter(cmd, "pRegionalName", DbType.String, obj.RegionalName);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pBranchName", DbType.String, obj.BranchName);
                        db.AddInParameter(cmd, "pMruId", DbType.String, obj.MruId);
                        db.AddInParameter(cmd, "pCaId", DbType.String, obj.CaId);
                        db.AddInParameter(cmd, "pCaName", DbType.String, obj.CaName);
                        db.AddInParameter(cmd, "pPayDt", DbType.DateTime, obj.PayDt);
                        db.AddInParameter(cmd, "pDueDt", DbType.DateTime, obj.DueDt);
                        db.AddInParameter(cmd, "pOutSourceAmount", DbType.Decimal, obj.OutSourceAmount);
                        db.AddInParameter(cmd, "pVatAmount", DbType.Decimal, obj.VatAmount);
                        db.AddInParameter(cmd, "pRecNo", DbType.String, obj.RecNo);
                        db.AddInParameter(cmd, "pPeriod", DbType.String, obj.Period);
                        db.AddInParameter(cmd, "pCompanyId", DbType.String, obj.CompanyId);
                        db.AddInParameter(cmd, "pActCode", DbType.String, obj.ActCode);
                        db.AddInParameter(cmd, "pPosNo", DbType.String, obj.PosNo);
                        db.AddInParameter(cmd, "pInvoiceNo", DbType.String, obj.InvoiceNo);
                        db.AddInParameter(cmd, "pUploadStatus", DbType.String, obj.UploadStatus);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateRTEPayUploadPayment(List<RTEPayUploadPaymentInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (RTEPayUploadPaymentInfo obj in list)
                {
                    line++;
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_RTEPayUploadPayment");
                        db.AddInParameter(cmd, "pUploadDt", DbType.DateTime, obj.UploadDt);
                        db.AddInParameter(cmd, "pCompanyId", DbType.String, obj.CompanyId);
                        db.AddInParameter(cmd, "pPaymentId", DbType.String, obj.PaymentId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        #endregion

        #region Update DL070 Data to Branch

        public bool UpdateService(List<ServiceInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (ServiceInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_Service");
                        db.AddInParameter(cmd, "pSvcId", DbType.String, obj.SvcId);
                        db.AddInParameter(cmd, "pModule", DbType.String, obj.Module);
                        db.AddInParameter(cmd, "pSvcName", DbType.String, obj.SvcName);
                        db.AddInParameter(cmd, "pMethodName", DbType.String, obj.MethodName);
                        db.AddInParameter(cmd, "pDescription", DbType.String, obj.Description);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateFunction(List<FunctionInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (FunctionInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_Function");
                        db.AddInParameter(cmd, "pFncId", DbType.String, obj.FncId);
                        db.AddInParameter(cmd, "pModule", DbType.String, obj.Module);
                        db.AddInParameter(cmd, "pFncName", DbType.String, obj.FncName);
                        db.AddInParameter(cmd, "pSubFncName", DbType.String, obj.SubFncName);
                        db.AddInParameter(cmd, "pDescription", DbType.String, obj.Description);
                        db.AddInParameter(cmd, "pUnlockableFlag", DbType.String, obj.UnlockableFlag);
                        db.AddInParameter(cmd, "pUnlockRemarkFlag", DbType.String, obj.UnlockRemarkFlag);
                        db.AddInParameter(cmd, "pInternal", DbType.Int16, obj.Internal);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateRole(List<RoleInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (RoleInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_Role");
                        db.AddInParameter(cmd, "pRoleId", DbType.String, obj.RoleId);
                        db.AddInParameter(cmd, "pRoleName", DbType.String, obj.RoleName);
                        db.AddInParameter(cmd, "pDescription", DbType.String, obj.Description);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateUser(List<UserInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (UserInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_User");
                        db.AddInParameter(cmd, "pUserId", DbType.String, obj.UserId);
                        db.AddInParameter(cmd, "pPassword", DbType.String, obj.Password);
                        db.AddInParameter(cmd, "pFullName", DbType.String, obj.FullName);
                        db.AddInParameter(cmd, "pDepartment", DbType.String, obj.Department);
                        //db.AddInParameter(cmd, "pToken", DbType.String, obj.Token);
                        //db.AddInParameter(cmd, "pLastRequest", DbType.DateTime, obj.LastRequest);
                        db.AddInParameter(cmd, "pLastLogin", DbType.DateTime, obj.LastLogin);
                        db.AddInParameter(cmd, "pPwdExpiredDt", DbType.DateTime, obj.PwdExpiredDt);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pPermission", DbType.String, obj.Permission);
                        db.AddInParameter(cmd, "pCurIP", DbType.String, obj.CurIP);
                        db.AddInParameter(cmd, "pReqIP", DbType.String, obj.ReqIP);
                        db.AddInParameter(cmd, "pReqFlag", DbType.String, obj.ReqFlag);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateFunctionService(List<FunctionServiceInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (FunctionServiceInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_FunctionService");
                        db.AddInParameter(cmd, "pRTId", DbType.String, obj.RTId);
                        db.AddInParameter(cmd, "pFncId", DbType.String, obj.FncId);
                        db.AddInParameter(cmd, "pSvcId", DbType.String, obj.SvcId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateRoleFunction(List<RoleFunctionInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (RoleFunctionInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_RoleFunction");
                        db.AddInParameter(cmd, "pRTId", DbType.String, obj.RTId);
                        db.AddInParameter(cmd, "pRoleId", DbType.String, obj.RoleId);
                        db.AddInParameter(cmd, "pFncId", DbType.String, obj.FncId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateRoleUser(List<RoleUserInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (RoleUserInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_RoleUser");
                        db.AddInParameter(cmd, "pRTId", DbType.String, obj.RTId);
                        db.AddInParameter(cmd, "pRoleId", DbType.String, obj.RoleId);
                        db.AddInParameter(cmd, "pUserId", DbType.String, obj.UserId);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateUnlockLog(List<UnlockLogInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (UnlockLogInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_UnlockLog");
                        db.AddInParameter(cmd, "pUnlockId", DbType.String, obj.UnlockId);
                        db.AddInParameter(cmd, "pFncId", DbType.String, obj.FncId);
                        db.AddInParameter(cmd, "pUnlockDt", DbType.DateTime, obj.UnlockDt);
                        db.AddInParameter(cmd, "pCurrentUserId", DbType.String, obj.CurrentUserId);
                        db.AddInParameter(cmd, "pUnlockUserId", DbType.String, obj.UnlockUserId);
                        db.AddInParameter(cmd, "pDescription", DbType.String, obj.Description);
                        db.AddInParameter(cmd, "pRemark", DbType.String, obj.Remark);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pBranchName", DbType.String, obj.BranchName);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, obj.PostBranchServerId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);
                        
                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateTerminal(List<Terminal> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (Terminal obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_Terminal");
                        db.AddInParameter(cmd, "pTerminalId", DbType.String, obj.TerminalId);
                        db.AddInParameter(cmd, "pTerminalCode", DbType.String, obj.TerminalCode);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pTaxCode", DbType.String, obj.TaxCode);
                        db.AddInParameter(cmd, "pIP", DbType.String, obj.IP);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateAppSetting(List<AppSettingInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (AppSettingInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_AppSetting");
                        db.AddInParameter(cmd, "pSettingCode", DbType.String, obj.SettingCode);
                        db.AddInParameter(cmd, "pSettingValue", DbType.String, obj.SettingValue);
                        db.AddInParameter(cmd, "pModule", DbType.String, obj.Module);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        public bool UpdateUnlockRemark(List<UnlockRemarkInfo> list, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = new ACABatchLogger();
            DbTransaction trans;
            int line = 0;
            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (UnlockRemarkInfo obj in list)
                {
                    line++;

                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncb_UnlockRemark");
                        db.AddInParameter(cmd, "pFncId", DbType.String, obj.FncId);
                        db.AddInParameter(cmd, "pRemarkId", DbType.String, obj.RemarkId);
                        db.AddInParameter(cmd, "pDescription", DbType.String, obj.Description);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        //aca log 
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                //log number of line imported
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.FileName, lineSuccess);
            }
            return true;
        }

        #endregion

        #region Get to upload data - Master

        public List<MRUPlanInfo> GetToUploadMRUPlan(DateTime lastModifiedDt)
        {
            List<MRUPlanInfo> ret = new List<MRUPlanInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadMRUPlan");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                MRUPlanInfo obj = new MRUPlanInfo();
                obj.MruPlanId = DaHelper.GetString(dr, "MruPlanId");
                obj.PortionId = DaHelper.GetString(dr, "PortionId");
                obj.BranchId = DaHelper.GetString(dr, "BranchId");
                obj.MruId = DaHelper.GetString(dr, "MruId");
                obj.Period = DaHelper.GetString(dr, "Period");
                obj.MeterReadDt = DaHelper.GetDate(dr, "MeterReadDt");
                obj.MeterReadActDt = DaHelper.GetDate(dr, "MeterReadActDt");
                obj.TransferDt = DaHelper.GetDate(dr, "TransferDt");
                obj.TransferActDt = DaHelper.GetDate(dr, "TransferActDt");
                obj.BillPrintDt = DaHelper.GetDate(dr, "BillPrintDt");
                obj.BillPrintActDt = DaHelper.GetDate(dr, "BillPrintActDt");
                obj.BookCreateDt = DaHelper.GetDate(dr, "BookCreateDt");
                obj.BookCreateActDt = DaHelper.GetDate(dr, "BookCreateActDt");
                obj.BookCheckInDt = DaHelper.GetDate(dr, "BookCheckInDt");
                obj.BookCheckInActDt = DaHelper.GetDate(dr, "BookCheckinActDt");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        #endregion

        #region Get to upload data - Billing

        public List<PrintPoolInfo> GetToUploadPrintPool(DateTime lastModifiedDt)
        {
            List<PrintPoolInfo> ret = new List<PrintPoolInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadPrintPool");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                PrintPoolInfo obj = new PrintPoolInfo();
                obj.PrintDoc = DaHelper.GetString(dr, "PrintDoc");
                obj.PrintType = DaHelper.GetByte(dr, "PrintType");
                obj.PrintBranch = DaHelper.GetString(dr, "PrintBranch");
                obj.BranchId = DaHelper.GetString(dr, "BranchId");
                obj.BranchName = DaHelper.GetString(dr, "BranchName");
                obj.MruId = DaHelper.GetString(dr, "MruId");
                obj.CaId = DaHelper.GetString(dr, "CaId");
                obj.CaName = DaHelper.GetString(dr, "CaName");
                obj.BillPred = DaHelper.GetString(dr, "BillPred");
                obj.AccountClass = DaHelper.GetString(dr, "AccountClass");
                obj.SpotBillFlag = DaHelper.GetString(dr, "SpotBillFlag");
                obj.A4Addition = DaHelper.GetString(dr, "A4Addition");
                obj.Reverse = DaHelper.GetByte(dr, "Reverse");
                obj.OrgDoc = DaHelper.GetString(dr, "OrgDoc");
                obj.PrintStatus = DaHelper.GetByte(dr, "PrintStatus");
                obj.AgencyFlag = DaHelper.GetByte(dr, "AgencyFlag");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        public List<GrpPrintPoolInfo> GetToUploadGrpPrintPool(DateTime lastModifiedDt)
        {
            List<GrpPrintPoolInfo> ret = new List<GrpPrintPoolInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadGrpPrintPool");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                GrpPrintPoolInfo obj = new GrpPrintPoolInfo();
                obj.PrintDoc = DaHelper.GetString(dr, "PrintDoc");
                obj.PrintType = DaHelper.GetByte(dr, "PrintType");
                obj.PrintBranch = DaHelper.GetString(dr, "PrintBranch");
                obj.BranchId = DaHelper.GetString(dr, "BranchId");
                obj.BranchName = DaHelper.GetString(dr, "BranchName");
                obj.MruId = DaHelper.GetString(dr, "MruId");
                obj.CaId = DaHelper.GetString(dr, "CaId");
                obj.CaName = DaHelper.GetString(dr, "CaName");
                obj.BillPred = DaHelper.GetString(dr, "BillPred");
                obj.ReceiptNo = DaHelper.GetString(dr, "ReceiptNo");
                obj.BankKey = DaHelper.GetString(dr, "BankKey");
                obj.HouseBank = DaHelper.GetString(dr, "HouseBank");
                obj.BankName = DaHelper.GetString(dr, "BankName");
                obj.BankDueDate = DaHelper.GetString(dr, "BankDueDate");
                obj.MtNo = DaHelper.GetString(dr, "MtNo");
                obj.Payer = DaHelper.GetString(dr, "Payer");
                obj.PayerName = DaHelper.GetString(dr, "PayerName");
                obj.GroupingDate = DaHelper.GetString(dr, "GroupingDate");
                obj.GrpInvPaymentDueDate = DaHelper.GetString(dr, "GrpInvPaymentDueDate");
                obj.A4Addition = DaHelper.GetString(dr, "A4Addition");
                obj.Reverse = DaHelper.GetByte(dr, "Reverse");
                obj.OrgDoc = DaHelper.GetString(dr, "OrgDoc");
                obj.PrintStatus = DaHelper.GetByte(dr, "PrintStatus");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        public List<GreenReceiptDetailInfo> GetToUploadGreenReceiptDetail(DateTime lastModifiedDt)
        {
            List<GreenReceiptDetailInfo> ret = new List<GreenReceiptDetailInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadGreenReceiptDetail");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                GreenReceiptDetailInfo obj = new GreenReceiptDetailInfo();
                obj.ReceiptNo = DaHelper.GetString(dr, "ReceiptNo");
                obj.ReceiptPrintDoc = DaHelper.GetString(dr, "ReceiptPrintDoc");
                obj.PrintBranch = DaHelper.GetString(dr, "PrintBranch");
                obj.BranchId = DaHelper.GetString(dr, "BranchId");
                obj.BranchName = DaHelper.GetString(dr, "BranchName");
                obj.MruId = DaHelper.GetString(dr, "MruId");
                obj.CaId = DaHelper.GetString(dr, "CaId");
                obj.CaName = DaHelper.GetString(dr, "CaName");
                obj.ReceiptPeriod = DaHelper.GetString(dr, "ReceiptPeriod");
                obj.W_100_sender = DaHelper.GetString(dr, "w_100_sender");
                obj.W_110_post_code = DaHelper.GetString(dr, "w_110_post_code");
                obj.W_121_mailing_person = DaHelper.GetString(dr, "w_121_mailing_person");
                obj.W_122_mailing_person = DaHelper.GetString(dr, "w_122_mailing_person");
                obj.W_211_address = DaHelper.GetString(dr, "w_211_address");
                obj.W_212_address = DaHelper.GetString(dr, "w_212_address");
                obj.W_213_address = DaHelper.GetString(dr, "w_213_address");
                obj.W_241_address = DaHelper.GetString(dr, "w_241_address");
                obj.W_242_address = DaHelper.GetString(dr, "w_242_address");
                obj.W_243_address = DaHelper.GetString(dr, "w_243_address");
                obj.W_250_post_code = DaHelper.GetString(dr, "w_250_post_code");
                obj.W_1610_invoice_no = DaHelper.GetString(dr, "w_1610_invoice_no");
                obj.W_1620_buss_name = DaHelper.GetString(dr, "w_1620_buss_name");
                obj.W_1631_name = DaHelper.GetString(dr, "w_1631_name");
                obj.W_1632_name = DaHelper.GetString(dr, "w_1632_name");
                obj.W_1633_name = DaHelper.GetString(dr, "w_1633_name");
                obj.W_1640_device_13_l1 = DaHelper.GetString(dr, "w_1640_device_13_l1");
                obj.W_1650_rate_cat_13_l2 = DaHelper.GetString(dr, "w_1650_rate_cat_13_l2");
                obj.W_1660_contract_ac_14_l1 = DaHelper.GetString(dr, "w_1660_contract_ac_14_l1");
                obj.W_1670_period_15_l1 = DaHelper.GetString(dr, "w_1670_period_15_l1");
                obj.W_1680_mr_date_15_l2 = DaHelper.GetString(dr, "w_1680_mr_date_15_l2");
                obj.W_1690_unit_after_16_l1 = DaHelper.GetString(dr, "w_1690_unit_after_16_l1");
                obj.W_1700_unit_previous_16_l2 = DaHelper.GetString(dr, "w_1700_unit_previous_16_l2");
                obj.W_1710_consumption_17_l1 = DaHelper.GetString(dr, "w_1710_consumption_17_l1");
                obj.W_1720_based_amount_18_l1 = DaHelper.GetString(dr, "w_1720_based_amount_18_l1");
                obj.W_1730_discount_amount_19_l1 = DaHelper.GetString(dr, "w_1730_discount_amount_19_l1");
                obj.W_1740_disct_descript = DaHelper.GetString(dr, "w_1740_disct_descript");
                obj.W_1750_baht = DaHelper.GetString(dr, "w_1750_baht");
                obj.W_1760_ft_amount_20_l1 = DaHelper.GetString(dr, "w_1760_ft_amount_20_l1");
                obj.W_1770_ft_price_20_l2 = DaHelper.GetString(dr, "w_1770_ft_price_20_l2");
                obj.W_1780_net_before_tax_21_l1 = DaHelper.GetString(dr, "w_1780_net_before_tax_21_l1");
                obj.W_1790_tax_amount_22_l1 = DaHelper.GetString(dr, "w_1790_tax_amount_22_l1");
                obj.W_1800_tax_rate_22_l2 = DaHelper.GetString(dr, "w_1800_tax_rate_22_l2");
                obj.W_1810_total_value_23_l1 = DaHelper.GetString(dr, "w_1810_total_value_23_l1");
                obj.W_1820_payment_date_24_l1 = DaHelper.GetString(dr, "w_1820_payment_date_24_l1");
                obj.ReceiptPrintStatus = DaHelper.GetByte(dr, "ReceiptPrintStatus");
                obj.HasGrouped = DaHelper.GetString(dr, "HasGrouped");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.CreatedDt = DaHelper.GetDate(dr, "CreatedDt");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                obj.FileName = DaHelper.GetString(dr, "FileName");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        //public List<GreenReceiptPrintHistoryInfo> GetToUploadGreenReceiptPrintHistory(DateTime lastModifiedDt)
        //{
        //    List<GreenReceiptPrintHistoryInfo> ret = new List<GreenReceiptPrintHistoryInfo>();
        //    Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
        //    DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadGreenReceiptPrintHistory");
        //    db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
        //    DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        GreenReceiptPrintHistoryInfo obj = new GreenReceiptPrintHistoryInfo();
        //        obj.ReceiptNo = DaHelper.GetString(dr, "ReceiptNo");
        //        obj.PrintBranch = DaHelper.GetString(dr, "PrintBranch");
        //        obj.PrintLog = DaHelper.GetInt(dr, "PrintLog");
        //        obj.PrintDate = DaHelper.GetDate(dr, "PrintDate");
        //        obj.BillSeqNo = DaHelper.GetInt(dr, "BillSeqNo");
        //        obj.BranchId = DaHelper.GetString(dr, "BranchId");
        //        obj.MruId = DaHelper.GetString(dr, "MruId");
        //        obj.ReceiptPeriod = DaHelper.GetString(dr, "ReceiptPeriod");
        //        obj.CaId = DaHelper.GetString(dr, "CaId");
        //        obj.BankId = DaHelper.GetString(dr, "BankId");
        //        obj.OrgDoc = DaHelper.GetString(dr, "OrgDoc");
        //        obj.PostDt = DaHelper.GetDate(dr, "PostDt");
        //        obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
        //        obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
        //        obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
        //        obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
        //        obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
        //        ret.Add(obj);
        //    }

        //    return ret;
        //}

        public List<PrintHistoryInfo> GetToUploadPrintHistory(DateTime lastModifiedDt)
        {
            List<PrintHistoryInfo> ret = new List<PrintHistoryInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadPrintHistory");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                PrintHistoryInfo obj = new PrintHistoryInfo();
                obj.PrintDoc = DaHelper.GetString(dr, "PrintDoc");
                obj.PrintType = DaHelper.GetByte(dr, "PrintType");
                obj.PrintBranch = DaHelper.GetString(dr, "PrintBranch");
                obj.PrintLog = DaHelper.GetInt(dr, "PrintLog");
                obj.PrintBranchName = DaHelper.GetString(dr, "PrintBranchName");
                obj.PrintDate = DaHelper.GetDate(dr, "PrintDate");
                obj.BillSeqNo = DaHelper.GetInt(dr, "BillSeqNo");
                obj.BranchId = DaHelper.GetString(dr, "BranchId");
                obj.BranchName = DaHelper.GetString(dr, "BranchName");
                obj.MruId = DaHelper.GetString(dr, "MruId");
                obj.CaId = DaHelper.GetString(dr, "CaId");
                obj.CaName = DaHelper.GetString(dr, "CaName");
                obj.BillPred = DaHelper.GetString(dr, "BillPred");
                obj.MtNo = DaHelper.GetString(dr, "MtNo");
                obj.Payer = DaHelper.GetString(dr, "Payer");
                obj.PayerName = DaHelper.GetString(dr, "PayerName");
                obj.GrpInvPaymentDueDate = DaHelper.GetString(dr, "GrpInvPaymentDueDate");
                obj.BankKey = DaHelper.GetString(dr, "BankKey");
                obj.HouseBank = DaHelper.GetString(dr, "HouseBank");
                obj.BankName = DaHelper.GetString(dr, "BankName");
                obj.BankDueDate = DaHelper.GetString(dr, "BankDueDate");
                obj.Reverse = DaHelper.GetByte(dr, "Reverse");
                obj.OrgDoc = DaHelper.GetString(dr, "OrgDoc");
                obj.ReceiptNo = DaHelper.GetString(dr, "ReceiptNo");
                obj.ApproverId = DaHelper.GetString(dr, "ApproverId");
                obj.ApproverName = DaHelper.GetString(dr, "ApproverName");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        public List<DeliveryHistoryInfo> GetToUploadDeliveryHistory(DateTime lastModifiedDt)
        {
            List<DeliveryHistoryInfo> ret = new List<DeliveryHistoryInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadDeliveryHistory");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                DeliveryHistoryInfo obj = new DeliveryHistoryInfo();
                obj.PrintDoc = DaHelper.GetString(dr, "PrintDoc");
                obj.PrintType = DaHelper.GetByte(dr, "PrintType");
                obj.PrintBranch = DaHelper.GetString(dr, "PrintBranch");
                obj.SentLog = DaHelper.GetInt(dr, "SentLog");
                obj.BillSeqNo = DaHelper.GetInt(dr, "BillSeqNo");
                obj.BranchId = DaHelper.GetString(dr, "BranchId");
                obj.BranchName = DaHelper.GetString(dr, "BranchName");
                obj.MruId = DaHelper.GetString(dr, "MruId");
                obj.BillPred = DaHelper.GetString(dr, "BillPred");
                obj.SentDt = DaHelper.GetDate(dr, "SentDt");
                obj.DeliveryPred = DaHelper.GetString(dr, "DeliveryPred");
                obj.DeliveryPlaceId = DaHelper.GetString(dr, "DeliveryPlaceId");
                obj.DeliveryPlaceName = DaHelper.GetString(dr, "DeliveryPlaceName");
                obj.OrgDoc = DaHelper.GetString(dr, "OrgDoc");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        public List<MaxBillSeqNoInfo> GetToUploadMaxBillSeqNo(DateTime lastModifiedDt)
        {
            List<MaxBillSeqNoInfo> ret = new List<MaxBillSeqNoInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadMaxBillSeqNo");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                MaxBillSeqNoInfo obj = new MaxBillSeqNoInfo();
                obj.BranchId = DaHelper.GetString(dr, "BranchId");
                obj.PrintType = DaHelper.GetByte(dr, "PrintType");
                obj.MaxNo = DaHelper.GetInt(dr, "MaxNo");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }
        
        public List<ApproverInfo> GetToUploadApprover(DateTime lastModifiedDt)
        {
            List<ApproverInfo> ret = new List<ApproverInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadApprover");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                ApproverInfo obj = new ApproverInfo();
                obj.ApproverId = DaHelper.GetString(dr, "ApproverId");
                obj.ApproverName = DaHelper.GetString(dr, "ApproverName");
                obj.Position = DaHelper.GetString(dr, "Position");
                obj.CreateBranchId = DaHelper.GetString(dr, "CreateBranchId");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        public List<DeliveryPlaceInfo> GetToUploadDeliveryPlace(DateTime lastModifiedDt)
        {
            List<DeliveryPlaceInfo> ret = new List<DeliveryPlaceInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadDeliveryPlace");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                DeliveryPlaceInfo obj = new DeliveryPlaceInfo();
                obj.DeliveryPlaceId = DaHelper.GetString(dr, "DeliveryPlaceId");
                obj.DeliveryBranchId = DaHelper.GetString(dr, "DeliveryBranchId");
                obj.DeliveryPlaceName = DaHelper.GetString(dr, "DeliveryPlaceName");
                obj.CreateBranchId = DaHelper.GetString(dr, "CreateBranchId");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        public List<BarcodeMRUInfo> GetToUploadBarcodeMRU(DateTime lastModifiedDt)
        {
            List<BarcodeMRUInfo> ret = new List<BarcodeMRUInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadBarcodeMRU");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                BarcodeMRUInfo obj = new BarcodeMRUInfo();
                obj.BranchId = DaHelper.GetString(dr, "BranchId");
                obj.MruId = DaHelper.GetString(dr, "MruId");
                obj.IsPrinted = DaHelper.GetString(dr, "IsPrinted");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        public List<BillStatusInfo> GetToUploadBillStatus(DateTime lastModifiedDt)
        {
            List<BillStatusInfo> ret = new List<BillStatusInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadBillStatus");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                BillStatusInfo obj = new BillStatusInfo();
                obj.BranchId = DaHelper.GetString(dr, "BranchId");
                obj.MruId = DaHelper.GetString(dr, "MruId");
                obj.BillPred = DaHelper.GetString(dr, "BillPred");
                obj.BranchName = DaHelper.GetString(dr, "BranchName");
                obj.PortionId = DaHelper.GetString(dr, "PortionId");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        #endregion

        #region Get to upload data - AR

        public List<AR> GetToUploadAR(DateTime lastModifiedDt)
        {
            List<AR> ret = new List<AR>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadAR");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                AR obj = new AR();
                obj.ItemId = DaHelper.GetString(dr, "ItemId");
                obj.CaDoc = DaHelper.GetString(dr, "CaDoc");
                obj.CaId = DaHelper.GetString(dr, "CaId");
                obj.DtId = DaHelper.GetString(dr, "DtId");
                obj.Description = DaHelper.GetString(dr, "Description");
                obj.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                obj.InvoiceDt = DaHelper.GetDate(dr, "InvoiceDt");
                obj.GroupInvoiceNo = DaHelper.GetString(dr, "GroupInvoiceNo");
                obj.BillBookId = DaHelper.GetString(dr, "BillBookId");
                obj.Period = DaHelper.GetString(dr, "Period");
                obj.MeterId = DaHelper.GetString(dr, "MeterId");
                obj.RateTypeId = DaHelper.GetString(dr, "RateTypeId");
                obj.MeterReadDt = DaHelper.GetDate(dr, "MeterReadDt");
                obj.ReadUnit = DaHelper.GetDecimal(dr, "ReadUnit");
                obj.LastUnit = DaHelper.GetDecimal(dr, "LastUnit");
                obj.BaseAmount = DaHelper.GetDecimal(dr, "BaseAmount");
                obj.FTUnitPrice = DaHelper.GetDecimal(dr, "FTUnitPrice");
                obj.FTAmount = DaHelper.GetDecimal(dr, "FTAmount");
                obj.UnitPrice = DaHelper.GetDecimal(dr, "UnitPrice");
                obj.Qty = DaHelper.GetDecimal(dr, "Qty");
                obj.UnitTypeId = DaHelper.GetString(dr, "UnitTypeId");
                obj.Amount = DaHelper.GetDecimal(dr, "Amount");
                obj.TaxCode = DaHelper.GetString(dr, "TaxCode");
                obj.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                obj.GAmount = DaHelper.GetDecimal(dr, "GAmount");
                obj.DueDt = DaHelper.GetDate(dr, "DueDt");
                obj.DueDt2 = DaHelper.GetDate(dr, "DueDt2");
                obj.DisconnectDt = DaHelper.GetDate(dr, "DisconnectDt");
                obj.DisconnectDoc = DaHelper.GetString(dr, "DisconnectDoc");
                obj.SubstDocNo = DaHelper.GetString(dr, "SubstDocNo");
                obj.OriginalInvoiceNo = DaHelper.GetString(dr, "OriginalInvoiceNo");
                obj.SpotBillInvoiceNo = DaHelper.GetString(dr, "SpotBillInvoiceNo");
                obj.InterestLockFlag = DaHelper.GetString(dr, "InterestLockFlag");
                obj.InterestKey = DaHelper.GetString(dr, "InterestKey");
                obj.InstallmentFlag = DaHelper.GetString(dr, "InstallmentFlag");
                obj.InstallmentPeriod = DaHelper.GetInt(dr, "InstallmentPeriod");
                obj.InstallmentTotalPeriod = DaHelper.GetInt(dr, "InstallmentTotalPeriod");
                obj.PaymentOrderFlag = DaHelper.GetString(dr, "PaymentOrderFlag");
                obj.PaymentOrderDt = DaHelper.GetDate(dr, "PaymentOrderDt");
                obj.CheckInRefNo = DaHelper.GetString(dr, "CheckinRefNo");
                obj.CancelFlag = DaHelper.GetString(dr, "CancelFlag");
                obj.ModifiedFlag = DaHelper.GetString(dr, "ModifiedFlag");
                obj.POSDebtFlag = DaHelper.GetString(dr, "POSDebtFlag");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.CreatedDt = DaHelper.GetDate(dr, "CreatedDt");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                obj.FileName = DaHelper.GetString(dr, "FileName");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        public List<APInfo> GetToUploadAP(DateTime lastModifiedDt)
        {
            List<APInfo> ret = new List<APInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadAP");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                APInfo obj = new APInfo();
                obj.APId = DaHelper.GetString(dr, "APId");
                obj.APPmId = DaHelper.GetString(dr, "APPmId");
                obj.APDtId = DaHelper.GetString(dr, "APDtId");
                obj.GLBankKey = DaHelper.GetString(dr, "GLBankKey");
                obj.BankName = DaHelper.GetString(dr, "BankName");
                obj.ClearingAccNo = DaHelper.GetString(dr, "ClearingAccNo");
                obj.RefNo = DaHelper.GetString(dr, "RefNo");
                obj.CaId = DaHelper.GetString(dr, "CaId");
                obj.CaName = DaHelper.GetString(dr, "CaName");
                obj.ChequeAmount = DaHelper.GetDecimal(dr, "ChequeAmount");
                obj.CashAmount = DaHelper.GetDecimal(dr, "CashAmount");
                obj.AdjAmount = DaHelper.GetDecimal(dr, "AdjAmount");
                obj.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                obj.CancelDt = DaHelper.GetDate(dr, "CancelDt");
                obj.CancelReason = DaHelper.GetString(dr, "CancelReason");
                obj.CancelCashierId = DaHelper.GetString(dr, "CancelCashierId");
                obj.CancelCashierName = DaHelper.GetString(dr, "CancelCashierName");
                obj.CashierId = DaHelper.GetString(dr, "CashierId");
                obj.CashierName = DaHelper.GetString(dr, "CashierName");
                obj.PosId = DaHelper.GetString(dr, "PosId");
                obj.TerminalCode = DaHelper.GetString(dr, "TerminalCode");
                obj.APQty = DaHelper.GetInt(dr, "APQty");
                obj.SepCheque = DaHelper.GetString(dr, "SepCheque");
                obj.BranchId = DaHelper.GetString(dr, "BranchId");
                obj.BranchName = DaHelper.GetString(dr, "BranchName");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                //obj.ExportedOnce = DaHelper.GetString(dr, "ExportedOnce");
                obj.PaidFlag = DaHelper.GetString(dr, "PaidFlag");
                obj.CanceledFlag = DaHelper.GetString(dr, "CanceledFlag");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;

                ret.Add(obj);
            }

            return ret;
        }

        public List<APChequePaymentInfo> GetToUploadAPChequePayment(DateTime lastModifiedDt)
        {
            List<APChequePaymentInfo> ret = new List<APChequePaymentInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadAPChequePayment");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                APChequePaymentInfo obj = new APChequePaymentInfo();
                obj.APId = DaHelper.GetString(dr, "APId");
                obj.ChqItemId = DaHelper.GetString(dr, "ChqItemId");
                obj.BankKey = DaHelper.GetString(dr, "BankKey");
                obj.BankName = DaHelper.GetString(dr, "BankName");
                obj.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                obj.ChqNo = DaHelper.GetString(dr, "ChqNo");
                obj.ChqDt = DaHelper.GetDate(dr, "ChqDt");
                obj.ChqAmt = DaHelper.GetDecimal(dr, "ChqAmt");
                db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;

                ret.Add(obj);
            }

            return ret;
        }

        #endregion

        #region Get to upload data - Payment

        public List<Payment> GetToUploadPayment(DateTime lastModifiedDt)
        {
            List<Payment> ret = new List<Payment>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadPayment");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Payment obj = new Payment();
                obj.PaymentId = DaHelper.GetString(dr, "PaymentId");
                obj.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                obj.PosId = DaHelper.GetString(dr, "PosId");
                obj.TerminalCode = DaHelper.GetString(dr, "TerminalCode");
                obj.CashierId = DaHelper.GetString(dr, "CashierId");
                obj.CashierName = DaHelper.GetString(dr, "CashierName");
                obj.BranchId = DaHelper.GetString(dr, "BranchId");
                obj.BranchName = DaHelper.GetString(dr, "BranchName");
                obj.OriginalPaymentId = DaHelper.GetString(dr, "OriginalPaymentId");
                obj.PaidChannel = DaHelper.GetByte(dr, "PaidChannel");
                obj.CmScope = DaHelper.GetByte(dr, "CmScope");
                obj.WorkId = DaHelper.GetString(dr, "WorkId");
                obj.WorkFlag = DaHelper.GetByte(dr, "WorkFlag");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;

                ret.Add(obj);
            }

            return ret;
        }

        public List<ARPaymentType> GetToUploadARPaymentType(DateTime lastModifiedDt)
        {
            List<ARPaymentType> ret = new List<ARPaymentType>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadARPaymentType");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                ARPaymentType obj = new ARPaymentType();
                obj.ARPtId = DaHelper.GetString(dr, "ARPtId");
                obj.PaymentId = DaHelper.GetString(dr, "PaymentId");
                obj.Amount = DaHelper.GetDecimal(dr, "Amount");
                obj.PtId = DaHelper.GetString(dr, "PtId");
                obj.ChangeAmount = DaHelper.GetDecimal(dr, "ChangeAmount");
                obj.FeeAmount = DaHelper.GetDecimal(dr, "FeeAmount");
                obj.BankKey = DaHelper.GetString(dr, "BankKey");
                obj.BankName = DaHelper.GetString(dr, "BankName");
                obj.GroupBankName = DaHelper.GetString(dr, "GroupBankName");
                obj.ChqNo = DaHelper.GetString(dr, "ChqNo");
                obj.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                obj.ChqDt = DaHelper.GetDate(dr, "ChqDt");
                obj.DraftFlag = DaHelper.GetString(dr, "DraftFlag");
                obj.CashierChequeFlag = DaHelper.GetString(dr, "CashierChequeFlag");
                obj.TranfAccNo = DaHelper.GetString(dr, "TranfAccNo");
                obj.TranfDt = DaHelper.GetDate(dr, "TranfDt");
                obj.CancelARPtId = DaHelper.GetString(dr, "CancelARPtId");
                obj.ClearingAccNo = DaHelper.GetString(dr, "ClearingAccNo");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        public List<ARPayment> GetToUploadARPayment(DateTime lastModifiedDt)
        {
            List<ARPayment> ret = new List<ARPayment>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadARPayment");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                ARPayment obj = new ARPayment();
                obj.ARPmId = DaHelper.GetString(dr, "ARPmId");
                obj.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                obj.PmId = DaHelper.GetString(dr, "PmId");
                obj.DocType = DaHelper.GetString(dr, "DocType");
                obj.Qty = DaHelper.GetDecimal(dr, "Qty");
                obj.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                obj.GAmount = DaHelper.GetDecimal(dr, "GAmount");
                obj.AdjAmount = DaHelper.GetDecimal(dr, "AdjAmount");
                obj.CancelARPmId = DaHelper.GetString(dr, "CancelARPmId");
                obj.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                obj.Pending = DaHelper.GetString(dr, "Pending");
                obj.PaidChannel = DaHelper.GetByte(dr, "PaidChannel");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        public List<RTARPaymentTypeARPayment> GetToUploadRTARPaymentTypeARPayment(DateTime lastModifiedDt)
        {
            List<RTARPaymentTypeARPayment> ret = new List<RTARPaymentTypeARPayment>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadRTARPaymentTypeARPayment");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                RTARPaymentTypeARPayment obj = new RTARPaymentTypeARPayment();
                obj.ARPtId = DaHelper.GetString(dr, "ARPtId");
                obj.ARPmId = DaHelper.GetString(dr, "ARPmId");
                obj.Amount = DaHelper.GetDecimal(dr, "Amount");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        public List<Receipt> GetToUploadReceipt(DateTime lastModifiedDt)
        {
            List<Receipt> ret = new List<Receipt>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadReceipt");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Receipt obj = new Receipt();
                obj.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                obj.PaymentId = DaHelper.GetString(dr, "PaymentId");
                obj.PrintingSequence = DaHelper.GetShort(dr, "PrintingSequence");
                obj.TotalReceipt = DaHelper.GetShort(dr, "TotalReceipt");
                obj.CaId = DaHelper.GetString(dr, "CaId");
                obj.Name = DaHelper.GetString(dr, "Name");
                obj.Address = DaHelper.GetString(dr, "Address");
                obj.IsNameAddrModified = DaHelper.GetString(dr, "IsNameAddrModified");
                obj.NoOfPrinting = DaHelper.GetInt(dr, "NoOfPrinting");
                obj.CancelDt = DaHelper.GetDate(dr, "CancelDt");
                obj.CancelReason = DaHelper.GetString(dr, "CancelReason");
                obj.ReceiptType = DaHelper.GetString(dr, "ReceiptType");
                obj.ExtReceiptId = DaHelper.GetString(dr, "ExtReceiptId");
                obj.ExtReceiptDt = DaHelper.GetDate(dr, "ExtReceiptDt");
                obj.CaDoc = DaHelper.GetString(dr, "CaDoc");
                obj.Description = DaHelper.GetString(dr, "Description");
                obj.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                obj.InvoiceDt = DaHelper.GetDate(dr, "InvoiceDt");
                obj.OriginalInvoiceNo = DaHelper.GetString(dr, "OriginalInvoiceNo");
                obj.OriginalInvoiceDt = DaHelper.GetDate(dr, "OriginalInvoiceDt");
                obj.InstallmentPeriod = DaHelper.GetInt(dr, "InstallmentPeriod");
                obj.InstallmentTotalPeriod = DaHelper.GetInt(dr, "InstallmentTotalPeriod");
                obj.MruId = DaHelper.GetString(dr, "MruId");
                obj.MeterId = DaHelper.GetString(dr, "MeterId");
                obj.RateTypeId = DaHelper.GetString(dr, "RateTypeId");
                obj.BranchId = DaHelper.GetString(dr, "BranchId");
                obj.TechBranchName = DaHelper.GetString(dr, "TechBranchName");
                obj.CommBranchId = DaHelper.GetString(dr, "CommBranchId");
                obj.CommBranchName = DaHelper.GetString(dr, "CommBranchName");
                obj.Period = DaHelper.GetString(dr, "Period");
                obj.GroupInvoiceNo = DaHelper.GetString(dr, "GroupInvoiceNo");
                obj.BillBookId = DaHelper.GetString(dr, "BillBookId");
                obj.SpotBillInvoiceNo = DaHelper.GetString(dr, "SpotBillInvoiceNo");
                obj.MeterReadDt = DaHelper.GetDate(dr, "MeterReadDt");
                obj.ReadUnit = DaHelper.GetDecimal(dr, "ReadUnit");
                obj.LastUnit = DaHelper.GetDecimal(dr, "LastUnit");
                obj.FullBaseAmount = DaHelper.GetDecimal(dr, "FullBaseAmount");
                obj.FullFTAmount = DaHelper.GetDecimal(dr, "FullFTAmount");
                obj.FullQty = DaHelper.GetDecimal(dr, "FullQty");
                obj.FullAmount = DaHelper.GetDecimal(dr, "FullAmount");
                obj.FullVatAmount = DaHelper.GetDecimal(dr, "FullVatAmount");
                obj.FullGAmount = DaHelper.GetDecimal(dr, "FullGAmount");
                obj.BaseAmount = DaHelper.GetDecimal(dr, "BaseAmount");
                obj.FTUnitPrice = DaHelper.GetDecimal(dr, "FTUnitPrice");
                obj.FTAmount = DaHelper.GetDecimal(dr, "FTAmount");
                obj.UnitPrice = DaHelper.GetDecimal(dr, "UnitPrice");
                obj.Qty = DaHelper.GetDecimal(dr, "Qty");
                obj.Amount = DaHelper.GetDecimal(dr, "Amount");
                obj.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                obj.GAmount = DaHelper.GetDecimal(dr, "GAmount");
                obj.QtyInstallment = DaHelper.GetDecimal(dr, "QtyInstallment");
                obj.AmountInstallment = DaHelper.GetDecimal(dr, "AmountInstallment");
                obj.VatAmountInstallment = DaHelper.GetDecimal(dr, "VatAmountInstallment");
                obj.GAmountInstallment = DaHelper.GetDecimal(dr, "GAmountInstallment");
                obj.DtId = DaHelper.GetString(dr, "DtId");
                obj.DtName = DaHelper.GetString(dr, "DtName");
                obj.ControllerId = DaHelper.GetString(dr, "ControllerId");
                obj.ControllerName = DaHelper.GetString(dr, "ControllerName");
                obj.TaxCode = DaHelper.GetString(dr, "TaxCode");
                obj.TaxRate = DaHelper.GetDecimal(dr, "TaxRate");
                obj.PartialPayment = DaHelper.GetByte(dr, "PartialPayment");
                obj.GroupIvReceiptType = DaHelper.GetString(dr, "GroupIvReceiptType");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        public List<ReceiptItem> GetToUploadReceiptItem(DateTime lastModifiedDt)
        {
            List<ReceiptItem> ret = new List<ReceiptItem>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadReceiptItem");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                ReceiptItem obj = new ReceiptItem();
                obj.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                obj.ARPmId = DaHelper.GetString(dr, "ARPmId");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        public List<PaymentLogInfo> GetToUploadPaymentLog(DateTime lastModifiedDt)
        {
            List<PaymentLogInfo> ret = new List<PaymentLogInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadPaymentLog");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                PaymentLogInfo obj = new PaymentLogInfo();
                obj.CorrectedId = DaHelper.GetString(dr, "CorrectedId");
                obj.CorrectedPairId = DaHelper.GetString(dr, "CorrectedPairId");
                obj.CorrectedCaseCode = DaHelper.GetString(dr, "CorrectedCaseCode");
                obj.CorrectedStage = DaHelper.GetByte(dr, "CorrectedStage");
                obj.CorrectedBy = DaHelper.GetString(dr, "CorrectedBy");
                obj.CorrectedDt = DaHelper.GetDate(dr, "CorrectedDt");
                obj.PaymentId = DaHelper.GetString(dr, "PaymentId");
                obj.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                obj.PosId = DaHelper.GetString(dr, "PosId");
                obj.TerminalCode = DaHelper.GetString(dr, "TerminalCode");
                obj.CashierId = DaHelper.GetString(dr, "CashierId");
                obj.CashierName = DaHelper.GetString(dr, "CashierName");
                obj.BranchId = DaHelper.GetString(dr, "BranchId");
                obj.BranchName = DaHelper.GetString(dr, "BranchName");
                obj.OriginalPaymentId = DaHelper.GetString(dr, "OriginalPaymentId");
                obj.PaidChannel = DaHelper.GetByte(dr, "PaidChannel");
                obj.CmScope = DaHelper.GetByte(dr, "CmScope");
                obj.WorkId = DaHelper.GetString(dr, "WorkId");
                obj.WorkFlag = DaHelper.GetByte(dr, "WorkFlag");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        #endregion

        #region Get to upload data - Cash Management

        public List<CashierWorkStatusInfo> GetToUploadCashierWorkStatus(DateTime lastModifiedDt)
        {
            List<CashierWorkStatusInfo> ret = new List<CashierWorkStatusInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadCashierWorkStatus");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                CashierWorkStatusInfo obj = new CashierWorkStatusInfo();
                obj.WorkId = DaHelper.GetString(dr, "WorkId");
                obj.CashierId = DaHelper.GetString(dr, "CashierId");
                obj.CashierName = DaHelper.GetString(dr, "CashierName");
                obj.PosId = DaHelper.GetString(dr, "PosId");
                obj.TerminalCode = DaHelper.GetString(dr, "TerminalCode");
                obj.Status = DaHelper.GetString(dr, "Status");
                obj.OpenWorkDt = DaHelper.GetDate(dr, "OpenWorkDt");
                obj.CloseWorkDt = DaHelper.GetDate(dr, "CloseWorkDt");
                obj.CloseWorkBy = DaHelper.GetString(dr, "CloseWorkBy");
                obj.BranchId = DaHelper.GetString(dr, "BranchId");
                obj.BaseLine = DaHelper.GetInt(dr, "BaseLine");
                obj.MarkedBL = DaHelper.GetString(dr, "MarkedBL");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        public List<CashierMoneyTransferInfo> GetToUploadCashierMoneyTransfer(DateTime lastModifiedDt)
        {
            List<CashierMoneyTransferInfo> ret = new List<CashierMoneyTransferInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadCashierMoneyTransfer");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                CashierMoneyTransferInfo obj = new CashierMoneyTransferInfo();
                obj.TransferId = DaHelper.GetString(dr, "TransferId");
                obj.Sender = DaHelper.GetString(dr, "Sender");
                obj.SenderName = DaHelper.GetString(dr, "SenderName");
                obj.Receiver = DaHelper.GetString(dr, "Receiver");
                obj.ReceiverName = DaHelper.GetString(dr, "ReceiverName");
                obj.Commander = DaHelper.GetString(dr, "Commander");
                obj.Status = DaHelper.GetString(dr, "Status");
                obj.RequestDt = DaHelper.GetDate(dr, "RequestDt");
                obj.RequestPosId = DaHelper.GetString(dr, "RequestPosId");
                obj.ResponseDt = DaHelper.GetDate(dr, "ResponseDt");
                obj.ResponsePosId = DaHelper.GetString(dr, "ResponsePosId");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        public List<CashierMoneyFlowInfo> GetToUploadCashierMoneyFlow(DateTime lastModifiedDt)
        {
            List<CashierMoneyFlowInfo> ret = new List<CashierMoneyFlowInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadCashierMoneyFlow");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                CashierMoneyFlowInfo obj = new CashierMoneyFlowInfo();
                obj.FlowId = DaHelper.GetString(dr, "FlowId");
                obj.FlowType = DaHelper.GetString(dr, "FlowType");
                obj.FlowDesc = DaHelper.GetString(dr, "FlowDesc");
                obj.FlowCat = DaHelper.GetString(dr, "FlowCat");
                obj.GLBankKey = DaHelper.GetString(dr, "GLBankKey");
                obj.BankName = DaHelper.GetString(dr, "BankName");
                obj.GLAccountNo = DaHelper.GetString(dr, "GLAccountNo");
                obj.CashAmt = DaHelper.GetDecimal(dr, "CashAmt");
                obj.ChequeAmt = DaHelper.GetDecimal(dr, "ChequeAmt");
                obj.WorkId = DaHelper.GetString(dr, "WorkId");
                obj.CashierId = DaHelper.GetString(dr, "CashierId");
                obj.TransferId = DaHelper.GetString(dr, "TransferId");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        public List<CashierMoneyFlowItemInfo> GetToUploadCashierMoneyFlowItem(DateTime lastModifiedDt)
        {
            List<CashierMoneyFlowItemInfo> ret = new List<CashierMoneyFlowItemInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadCashierMoneyFlowItem");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                CashierMoneyFlowItemInfo obj = new CashierMoneyFlowItemInfo();
                obj.FlowId = DaHelper.GetString(dr, "FlowId");
                obj.ChqItemId = DaHelper.GetString(dr, "ChqItemId");
                obj.BankKey = DaHelper.GetString(dr, "BankKey");
                obj.ChqNo = DaHelper.GetString(dr, "ChqNo");
                obj.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                obj.ChqDt = DaHelper.GetDate(dr, "ChqDt");
                obj.Amount = DaHelper.GetDecimal(dr, "Amount");
                obj.ValidFlag = DaHelper.GetString(dr, "ValidFlag");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        #endregion

        #region Get to upload data - Agency

        public List<BillBookInfo> GetToUploadBillBook(DateTime lastModifiedDt)
        {
            List<BillBookInfo> ret = new List<BillBookInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadBillBook");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                BillBookInfo obj = new BillBookInfo();
                obj.BillBookId = DaHelper.GetString(dr, "BillBookId");
                obj.BookHolderId = DaHelper.GetString(dr, "BookHolderId");
                obj.BookHolderName = DaHelper.GetString(dr, "BookHolderName");
                obj.BookLot = DaHelper.GetInt(dr, "BookLot");
                obj.CreateDate = DaHelper.GetDate(dr, "CreateDate");
                obj.AdvPayAmount = DaHelper.GetDecimal(dr, "AdvPayAmount");
                obj.AdvPayDueDate = DaHelper.GetDate(dr, "AdvPayDueDate");
                obj.ReturnDueDate = DaHelper.GetDate(dr, "ReturnDueDate");
                obj.CheckInDate = DaHelper.GetDate(dr, "CheckInDate");
                obj.BookPeriod = DaHelper.GetString(dr, "BookPeriod");
                obj.ReceiveCount = DaHelper.GetByte(dr, "ReceiveCount");
                obj.BkId = DaHelper.GetString(dr, "BkId");
                obj.CreatedBy = DaHelper.GetString(dr, "CreatedBy");
                obj.Note = DaHelper.GetString(dr, "Note");
                obj.BsId = DaHelper.GetString(dr, "BsId");
                obj.AboId = DaHelper.GetString(dr, "AboId");
                obj.TotalBillCollected = DaHelper.GetInt(dr, "TotalBillCollected");
                obj.TotalBill = DaHelper.GetInt(dr, "TotalBill");
                obj.BookTotalAmount = DaHelper.GetDecimal(dr, "BookTotalAmount");
                obj.BookPaidAmount = DaHelper.GetDecimal(dr, "BookPaidAmount");
                obj.BaseAmount = DaHelper.GetDecimal(dr, "BaseAmount");
                obj.FTAmount = DaHelper.GetDecimal(dr, "FTAmount");
                obj.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                obj.CreatedBranchId = DaHelper.GetString(dr, "CreatedBranchId");
                obj.CreatedBranchName = DaHelper.GetString(dr, "CreatedBranchName");
                obj.CollectArea = DaHelper.GetString(dr, "CollectArea");
                obj.ContractValidFrom = DaHelper.GetDate(dr, "ContractValidFrom");
                obj.SecurityDeposit = DaHelper.GetDecimal(dr, "SecurityDeposit");
                obj.AccountClassId = DaHelper.GetString(dr, "AccountClassId");
                obj.BpTypeId = DaHelper.GetString(dr, "BpTypeId");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        public List<BillStatusInfoInfo> GetToUploadBillStatusInfo(DateTime lastModifiedDt)
        {
            List<BillStatusInfoInfo> ret = new List<BillStatusInfoInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadBillStatusInfo");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                BillStatusInfoInfo obj = new BillStatusInfoInfo();
                obj.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                obj.BranchId = DaHelper.GetString(dr, "BranchId");
                obj.MruId = DaHelper.GetString(dr, "MruId");
                obj.CaId = DaHelper.GetString(dr, "CaId");
                obj.Period = DaHelper.GetString(dr, "Period");
                obj.AbsId = DaHelper.GetString(dr, "AbsId");
                obj.AboId = DaHelper.GetString(dr, "AboId");
                obj.PmId = DaHelper.GetString(dr, "PmId");
                obj.AllowRepeated = DaHelper.GetString(dr, "AllowRepeated");
                obj.InBook = DaHelper.GetString(dr, "InBook");
                obj.RateCatId = DaHelper.GetString(dr, "RateCatId");
                obj.PaidBy = DaHelper.GetString(dr, "PaidBy");
                obj.Ft = DaHelper.GetDecimal(dr, "Ft");
                obj.Vat = DaHelper.GetDecimal(dr, "Vat");
                obj.BaseAmount = DaHelper.GetDecimal(dr, "BaseAmount");
                obj.TotalAmount = DaHelper.GetDecimal(dr, "TotalAmount");
                obj.PaidType = DaHelper.GetString(dr, "PaidType");
                obj.PrintBranch = DaHelper.GetString(dr, "PrintBranch");
                obj.OrgDoc = DaHelper.GetString(dr, "OrgDoc");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        public List<BillBookDetailInfo> GetToUploadBillBookDetail(DateTime lastModifiedDt)
        {
            List<BillBookDetailInfo> ret = new List<BillBookDetailInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadBillBookDetail");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                BillBookDetailInfo obj = new BillBookDetailInfo();
                obj.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                obj.BillBookId = DaHelper.GetString(dr, "BillBookId");
                obj.Period = DaHelper.GetString(dr, "Period");
                obj.BranchId = DaHelper.GetString(dr, "BranchId");
                obj.BranchName = DaHelper.GetString(dr, "BranchName");
                obj.MruId = DaHelper.GetString(dr, "MruId");
                obj.CaId = DaHelper.GetString(dr, "CaId");
                obj.CaName = DaHelper.GetString(dr, "CaName");
                obj.CaAddress = DaHelper.GetString(dr, "CaAddress");
                obj.AbsId = DaHelper.GetString(dr, "AbsId");
                obj.PmId = DaHelper.GetString(dr, "PmId");
                obj.InvSelected = DaHelper.GetString(dr, "InvSelected");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        public List<BillBookInputItemInfo> GetToUploadBillBookInputItem(DateTime lastModifiedDt)
        {
            List<BillBookInputItemInfo> ret = new List<BillBookInputItemInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadBillBookInputItem");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                BillBookInputItemInfo obj = new BillBookInputItemInfo();
                obj.BIpItemId = DaHelper.GetString(dr, "BIpItemId");
                obj.FilterType = DaHelper.GetString(dr, "FilterType");
                obj.BillBookId = DaHelper.GetString(dr, "BillBookId");
                obj.BranchId = DaHelper.GetString(dr, "BranchId");
                obj.MruId = DaHelper.GetString(dr, "MruId");
                obj.CaId = DaHelper.GetString(dr, "CaId");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        public List<BillBookInputSetInfo> GetToUploadBillBookInputSet(DateTime lastModifiedDt)
        {
            List<BillBookInputSetInfo> ret = new List<BillBookInputSetInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadBillBookInputSet");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                BillBookInputSetInfo obj = new BillBookInputSetInfo();
                obj.BIpSetId = DaHelper.GetString(dr, "BIpSetId");
                obj.BillBookId = DaHelper.GetString(dr, "BillBookId");
                obj.BranchId = DaHelper.GetString(dr, "BranchId");
                obj.MruId = DaHelper.GetString(dr, "MruId");
                obj.BillPeriodType = DaHelper.GetString(dr, "BillPeriodType");
                obj.BillOutType = DaHelper.GetString(dr, "BillOutType");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        public List<AgencyCommissionInfo> GetToUploadAgencyCommission(DateTime lastModifiedDt)
        {
            List<AgencyCommissionInfo> ret = new List<AgencyCommissionInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadAgencyCommission");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                AgencyCommissionInfo obj = new AgencyCommissionInfo();
                obj.CmId = DaHelper.GetString(dr, "CmId");
                obj.CalDt = DaHelper.GetDate(dr, "CalDt");
                obj.CalCount = DaHelper.GetInt(dr, "CalCount");
                obj.CmAmount = DaHelper.GetDecimal(dr, "CmAmount");
                obj.BaseCmAmount = DaHelper.GetDecimal(dr, "BaseCmAmount");
                obj.SpecialCmAmount = DaHelper.GetDecimal(dr, "SpecialCmAmount");
                obj.InvCmAmount = DaHelper.GetDecimal(dr, "InvCmAmount");
                obj.OverNinety = DaHelper.GetString(dr, "OverNinety");
                obj.FineAmount = DaHelper.GetDecimal(dr, "FineAmount");
                obj.TaxAmount = DaHelper.GetDecimal(dr, "TaxAmount");
                obj.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                obj.TransCost = DaHelper.GetDecimal(dr, "TransCost");
                obj.FarLandHelp = DaHelper.GetDecimal(dr, "FarLandHelp");
                obj.SpecialMoney = DaHelper.GetDecimal(dr, "SpecialMoney");
                obj.RtId = DaHelper.GetInt(dr, "RtId");
                obj.FineEnabled = DaHelper.GetString(dr, "FineEnabled");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        public List<RTAgencyContractMruInfo> GetToUploadRTAgencyContractMru(DateTime lastModifiedDt)
        {
            List<RTAgencyContractMruInfo> ret = new List<RTAgencyContractMruInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadRTAgencyContractMru");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                RTAgencyContractMruInfo obj = new RTAgencyContractMruInfo();
                obj.AgentMruId = DaHelper.GetString(dr, "AgentMruId");
                obj.CaId = DaHelper.GetString(dr, "CaId");
                obj.CaName = DaHelper.GetString(dr, "CaName");
                obj.BranchId = DaHelper.GetString(dr, "BranchId");
                obj.BranchName = DaHelper.GetString(dr, "BranchName");
                obj.MruId = DaHelper.GetString(dr, "MruId");
                obj.SecurityDeposit = DaHelper.GetDecimal(dr, "SecurityDeposit");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        public List<RTAgencyCommissionBillBookInfo> GetToUploadRTAgencyCommissionBillBook(DateTime lastModifiedDt)
        {
            List<RTAgencyCommissionBillBookInfo> ret = new List<RTAgencyCommissionBillBookInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadRTAgencyCommissionBillBook");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                RTAgencyCommissionBillBookInfo obj = new RTAgencyCommissionBillBookInfo();
                obj.CmId = DaHelper.GetString(dr, "CmId");
                obj.BillBookId = DaHelper.GetString(dr, "BillBookId");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        #endregion

        #region Get to upload data - ePayment

        public List<EPayClarifyInfo> GetToUploadEPayClearify(DateTime lastModifiedDt)
        {
            List<EPayClarifyInfo> ret = new List<EPayClarifyInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadEPayClarify");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                EPayClarifyInfo obj = new EPayClarifyInfo();
                obj.IssueId = DaHelper.GetString(dr, "IssueId");
                obj.EPayItemId = DaHelper.GetString(dr, "EPayItemId");
                obj.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                obj.NewInvoiceNo = DaHelper.GetString(dr, "NewInvoiceNo");
                obj.ReceiptInvoiceNo = DaHelper.GetString(dr, "ReceiptInvoiceNo");
                obj.FixedType = DaHelper.GetString(dr, "FixedType");
                obj.FixedDt = DaHelper.GetDate(dr, "FixedDt");
                obj.FixedBy = DaHelper.GetString(dr, "FixedBy");
                obj.RefDocNo = DaHelper.GetString(dr, "RefDocNo");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
          
                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        public List<EPayUploadInfo> GetToUploadEPayUpload(DateTime lastModifiedDt)
        {
            List<EPayUploadInfo> ret = new List<EPayUploadInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadEPayUpload");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                EPayUploadInfo obj = new EPayUploadInfo();
                obj.FileId = DaHelper.GetString(dr, "FileId");
                obj.CompanyId = DaHelper.GetString(dr, "CompanyId");
                obj.AgentId = DaHelper.GetString(dr, "AgentId");
                obj.AgentName = DaHelper.GetString(dr, "AgentName");
                obj.AccountClassId = DaHelper.GetString(dr, "AccountClassId");
                obj.AccountClassName = DaHelper.GetString(dr, "AccountClassName");
                obj.FileName = DaHelper.GetString(dr, "FileName");
                obj.UploadDt = DaHelper.GetDate(dr, "UploadDt");
                obj.BillCount = DaHelper.GetInt(dr, "BillCount");
                obj.BillAmount = DaHelper.GetDecimal(dr, "BillAmount");
                obj.TotalBillCount = DaHelper.GetInt(dr, "TotalBillCount");
                obj.TotalBillAmount = DaHelper.GetDecimal(dr, "TotalBillAmount");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        public List<EPayUploadItemInfo> GetToUploadEPayUploadItem(DateTime lastModifiedDt)
        {
            List<EPayUploadItemInfo> ret = new List<EPayUploadItemInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadEPayUploadItem");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                EPayUploadItemInfo obj = new EPayUploadItemInfo();
                obj.EPayItemId = DaHelper.GetString(dr, "EPayItemId");
                obj.FileId = DaHelper.GetString(dr, "FileId");
                obj.Regional = DaHelper.GetString(dr, "Regional");
                obj.RegionalName = DaHelper.GetString(dr, "RegionalName");
                obj.BranchId = DaHelper.GetString(dr, "BranchId");
                obj.BranchName = DaHelper.GetString(dr, "BranchName");
                obj.MruId = DaHelper.GetString(dr, "MruId");
                obj.CaId = DaHelper.GetString(dr, "CaId");
                obj.CaName = DaHelper.GetString(dr, "CaName");
                obj.PayDt = DaHelper.GetDate(dr, "PayDt");
                obj.DueDt = DaHelper.GetDate(dr, "DueDt");
                obj.OutSourceAmount = DaHelper.GetDecimal(dr, "OutSourceAmount");
                obj.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                obj.RecNo = DaHelper.GetString(dr, "RecNo");
                obj.Period = DaHelper.GetString(dr, "Period");
                obj.CompanyId = DaHelper.GetString(dr, "CompanyId");
                obj.ActCode = DaHelper.GetString(dr, "ActCode");
                obj.PosNo = DaHelper.GetString(dr, "PosNo");
                obj.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                obj.UploadStatus = DaHelper.GetString(dr, "UploadStatus");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        public List<RTEPayUploadPaymentInfo> GetToUploadRTEPayUploadPayment(DateTime lastModifiedDt)
        {
            List<RTEPayUploadPaymentInfo> ret = new List<RTEPayUploadPaymentInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadRTEPayUploadPayment");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                RTEPayUploadPaymentInfo obj = new RTEPayUploadPaymentInfo();
                obj.UploadDt = DaHelper.GetDate(dr, "UploadDt");
                obj.CompanyId = DaHelper.GetString(dr, "CompanyId");
                obj.PaymentId = DaHelper.GetString(dr, "PaymentId");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        #endregion

        #region Get to upload data - Technical

        public List<UserInfo> GetToUploadUser(DateTime lastModifiedDt)
        {
            List<UserInfo> users = new List<UserInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadUser");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                UserInfo obj = new UserInfo();
                obj.UserId = DaHelper.GetString(dr, "UserId");
                obj.Password = DaHelper.GetString(dr, "Password");
                obj.FullName = DaHelper.GetString(dr, "FullName");
                obj.Department = DaHelper.GetString(dr, "Department");
                obj.Token = DaHelper.GetString(dr, "Token");
                obj.LastRequest = DaHelper.GetDate(dr, "LastRequest");
                obj.LastLogin = DaHelper.GetDate(dr, "LastLogin");
                obj.PwdExpiredDt = DaHelper.GetDate(dr, "PwdExpiredDt");
                obj.BranchId = DaHelper.GetString(dr, "BranchId");
                obj.Permission = DaHelper.GetString(dr, "Permission");
                obj.CurIP = DaHelper.GetString(dr, "CurIP");
                obj.ReqIP = DaHelper.GetString(dr, "ReqIP");
                obj.ReqFlag = DaHelper.GetString(dr, "ReqFlag");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                users.Add(obj);
            }

            return users;
        }

        public List<RoleUserInfo> GetToUploadRoleUser(DateTime lastModifiedDt)
        {
            List<RoleUserInfo> rsList = new List<RoleUserInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadRTRoleUser");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                RoleUserInfo obj = new RoleUserInfo();
                obj.RTId = DaHelper.GetString(dr, "RTId");
                obj.RoleId = DaHelper.GetString(dr, "RoleId");
                obj.UserId = DaHelper.GetString(dr, "UserId");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false; ;
                rsList.Add(obj);
            }

            return rsList;
        }

        public List<UnlockLogInfo> GetToUploadUnlockLog(DateTime lastModifiedDt)
        {
            List<UnlockLogInfo> rsList = new List<UnlockLogInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadUnlockLog");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                UnlockLogInfo obj = new UnlockLogInfo();
                obj.UnlockId = DaHelper.GetString(dr, "UnlockId");
                obj.FncId = DaHelper.GetString(dr, "FncId");
                obj.UnlockDt = DaHelper.GetDate(dr, "UnlockDt");
                obj.CurrentUserId = DaHelper.GetString(dr, "CurrentUserId");
                obj.UnlockUserId = DaHelper.GetString(dr, "UnlockUserId");
                obj.Description = DaHelper.GetString(dr, "Description");
                obj.Remark = DaHelper.GetString(dr, "Remark");
                obj.BranchId = DaHelper.GetString(dr, "BranchId");
                obj.BranchName = DaHelper.GetString(dr, "BranchName");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                rsList.Add(obj);
            }

            return rsList;
        }

        #endregion

        #region Get to upload data - ExportTable

        public List<ExportTransactionLogInfo> GetToUploadExportTransactionLog(DateTime lastModifiedDt)
        {
            List<ExportTransactionLogInfo> ret = new List<ExportTransactionLogInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_syncb_GetToUploadExportTransactionLog");
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                ExportTransactionLogInfo obj = new ExportTransactionLogInfo();
                obj.BranchId = DaHelper.GetString(dr, "BranchId");
                obj.CreatedDt = DaHelper.GetDate(dr, "CreatedDt");
                obj.ExportType = DaHelper.GetString(dr, "ExportType");
                obj.ItemId = DaHelper.GetString(dr, "ItemId");
                obj.ARPmId = DaHelper.GetString(dr, "ARPmId");
                obj.ARPtId = DaHelper.GetString(dr, "ARPtId");
                obj.PaymentId = DaHelper.GetString(dr, "PaymentId");
                obj.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                obj.APId = DaHelper.GetString(dr, "APId");
                obj.ChqItemId = DaHelper.GetString(dr, "ChqItemId");
                obj.ExportFlag = DaHelper.GetInt(dr, "ExportFlag");
                obj.ExportDt = DaHelper.GetDate(dr, "ExportDt");
                obj.WorkId = DaHelper.GetString(dr, "WorkId");
                obj.CloseWorkDt = DaHelper.GetDate(dr, "CloseWorkDt");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");

                //obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                ret.Add(obj);
            }

            return ret;
        }

        #endregion

        #region Set syncFlag to "0"

        public int SetSync(string entityName, DateTime lastModifiedDt)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand(String.Format("{0}{1}", "ta_syncb_SetSync", entityName));
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            return db.ExecuteNonQuery(cmd);
        }

        public int SetSync(string entityName, DateTime lastModifiedDt, string firstRec, string lastRec)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand(String.Format("{0}{1}", "ta_syncb_SetSync", entityName));
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
            db.AddInParameter(cmd, "pFirstRec", DbType.String, firstRec);
            db.AddInParameter(cmd, "pLastRec", DbType.String, lastRec);
            return db.ExecuteNonQuery(cmd);
        }


        #endregion
    }
}

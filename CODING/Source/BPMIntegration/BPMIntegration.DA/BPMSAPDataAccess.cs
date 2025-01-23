using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using System.Data.Common;
using PEA.BPM.Integration.ACABatchService;
using PEA.BPM.Integration.BPMIntegration.Interface.ExportEntities;
using System.Data;
using System.Data.SqlClient;
using PEA.BPM.Architecture.CommonUtilities;
using System.IO;

namespace PEA.BPM.Integration.BPMIntegration.DA
{
    public class BPMSAPDataAccess
    {
        private const int CMD_TIMEOUT = 600; //10 minutes

        #region Import Data from SAP DL01_ISOLATE

        public bool UpdateNonWorkingDay(List<NonWorkingDayInfo> impList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int line = 0;
            int lineSuccess = 0;
            bool reset = false;

            if (impList[0].Action == "O")
                reset = true;

            /*
            string testtxt = impList[0].ListNw[0].NwDate.ToString();

            if (string.IsNullOrEmpty(testtxt))
                logger.WriteLog(param.BatchKey, ACABatchLogger.ErrorType.Processing, "Test", param.FileName, 0);
            else
                logger.WriteLog(param.BatchKey, ACABatchLogger.ErrorType.Processing, testtxt, param.FileName, 0);
            */
            foreach (NonWorkingDayInfo a in impList)
            {
                line++;
                try
                {
                    for (int i = 0; i < a.ListNw.Count; i++)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_Imp_NonWorkingDay");
                        db.AddInParameter(cmd, "NwId", DbType.String, a.ListNw[i].NwKey);
                        db.AddInParameter(cmd, "CdType", DbType.String, a.CdType);
                        db.AddInParameter(cmd, "NwDay", DbType.DateTime, a.ListNw[i].NwDate);
                        db.AddInParameter(cmd, "ModifiedBy", DbType.String, a.ModifiedBy);
                        db.AddInParameter(cmd, "Action", DbType.String, a.Action);
                        db.AddInParameter(cmd, "ExitOnDoubleInsert", DbType.String, param.Overwrite ? "0" : "1");
                        db.AddInParameter(cmd, "Reset", DbType.String, reset ? "1" : "0");
                        db.ExecuteNonQuery(cmd);
                        reset = false;
                        lineSuccess++;
                    }
                }
                catch (Exception e)
                {
                    //aca log 
                    logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.ShortFileName, line);
                    return false;
                }
            }

            //log number of line imported
            logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.ShortFileName, lineSuccess);

            return true;
        }

        public bool UpdateAccountClass(List<AccountClassInfo> impList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int line = 0;
            int lineSuccess = 0;
            bool reset = true;

            foreach (AccountClassInfo a in impList)
            {
                line++;
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("ta_Imp_AccountClass");
                    db.AddInParameter(cmd, "AccountClassId", DbType.String, a.AccountClassId);
                    db.AddInParameter(cmd, "AccountClassName", DbType.String, a.AccountClassName);
                    db.AddInParameter(cmd, "ModifiedBy", DbType.String, a.ModifiedBy);
                    db.AddInParameter(cmd, "Action", DbType.String, a.Action);
                    db.AddInParameter(cmd, "ExitOnDoubleInsert", DbType.String, param.Overwrite ? "0" : "1");
                    db.AddInParameter(cmd, "Reset", DbType.String, reset ? "1" : "0");
                    db.ExecuteNonQuery(cmd);
                    reset = false;
                    lineSuccess++;
                }
                catch (Exception e)
                {
                    //aca log 
                    logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.ShortFileName, line);
                    return false;
                }
            }
            //log number of line imported
            logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.ShortFileName, lineSuccess);
            
            return true;
        }

        public bool UpdateContractType(List<ContractTypeInfo> impList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int line = 0;
            int lineSuccess = 0;
            bool reset = true;

            foreach (ContractTypeInfo a in impList)
            {
                line++;
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("ta_Imp_ContractType");
                    db.AddInParameter(cmd, "CtId", DbType.String, a.CtId);
                    db.AddInParameter(cmd, "CtName", DbType.String, a.CtName);
                    db.AddInParameter(cmd, "ModifiedBy", DbType.String, a.ModifiedBy);
                    db.AddInParameter(cmd, "Action", DbType.String, a.Action);
                    db.AddInParameter(cmd, "ExitOnDoubleInsert", DbType.String, param.Overwrite ? "0" : "1");
                    db.AddInParameter(cmd, "Reset", DbType.String, reset ? "1" : "0");
                    db.ExecuteNonQuery(cmd);
                    reset = false;
                    lineSuccess++;
                }
                catch (Exception e)
                {
                    //aca log 
                    logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.ShortFileName, line);
                    return false;
                }
            }
            //log number of line imported
            logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.ShortFileName, lineSuccess);
            
            return true;
        }

        public bool UpdateMeterSizeType(List<MeterSizeInfo> impList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int line = 0;
            int lineSuccess = 0;
            bool reset = false;

            if (impList[0].Action == "O")
                reset = true;

            foreach (MeterSizeInfo a in impList)
            {
                line++;
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("ta_Imp_MeterSize");
                    db.AddInParameter(cmd, "MeterSizeId", DbType.String, a.MeterSizeId);
                    db.AddInParameter(cmd, "MeterSizeName", DbType.String, a.MeterSizeName);
                    db.AddInParameter(cmd, "ReConnectMeterRate", DbType.Decimal, a.ReConnectMeterRate);
                    db.AddInParameter(cmd, "ModifiedBy", DbType.String, a.ModifiedBy);
                    db.AddInParameter(cmd, "Action", DbType.String, a.Action);
                    db.AddInParameter(cmd, "ExitOnDoubleInsert", DbType.String, param.Overwrite ? "0" : "1");
                    db.AddInParameter(cmd, "Reset", DbType.String, reset ? "1" : "0");
                    db.ExecuteNonQuery(cmd);
                    reset = false;
                    lineSuccess++;
                }
                catch (Exception e)
                {
                    //aca log 
                    logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.ShortFileName, line);
                    return false;
                }
            }

            //log number of line imported
            logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.ShortFileName, lineSuccess);
           
            return true;
        }

        public bool UpdatePaymentMethod(List<PaymentMethodInfo> impList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int line = 0;
            int lineSuccess = 0;
            bool reset = true;

            foreach (PaymentMethodInfo a in impList)
            {
                line++;
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("ta_Imp_PaymentMethod");
                    db.AddInParameter(cmd, "PmId", DbType.String, a.PmId);
                    db.AddInParameter(cmd, "PmName", DbType.String, a.PmName);
                    db.AddInParameter(cmd, "ModifiedBy", DbType.String, a.ModifiedBy);
                    db.AddInParameter(cmd, "Action", DbType.String, a.Action);
                    db.AddInParameter(cmd, "ExitOnDoubleInsert", DbType.String, param.Overwrite ? "0" : "1");
                    db.AddInParameter(cmd, "Reset", DbType.String, reset ? "1" : "0");
                    db.ExecuteNonQuery(cmd);
                    reset = false;
                    lineSuccess++;
                }
                catch (Exception e)
                {
                    //aca log 
                    logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.ShortFileName, line);
                    return false;
                }
            }
            //log number of line imported
            logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.ShortFileName, lineSuccess);

            return true;
        }

        public bool UpdateTaxCode(List<TaxCodeInfo> impList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int line = 0;
            int lineSuccess = 0;
            bool reset = false;

            if (impList[0].Action == "O")
                reset = true;

            foreach (TaxCodeInfo a in impList)
            {
                line++;
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("ta_Imp_TaxCode");
                    db.AddInParameter(cmd, "TaxCode", DbType.String, a.TaxCode);
                    db.AddInParameter(cmd, "TaxName", DbType.String, a.TaxName);
                    db.AddInParameter(cmd, "TaxRate", DbType.Decimal, a.TaxRate);
                    db.AddInParameter(cmd, "ModifiedBy", DbType.String, a.ModifiedBy);
                    db.AddInParameter(cmd, "Action", DbType.String, a.Action);
                    db.AddInParameter(cmd, "ExitOnDoubleInsert", DbType.String, param.Overwrite ? "0" : "1");
                    db.AddInParameter(cmd, "Reset", DbType.String, reset ? "1" : "0");
                    db.ExecuteNonQuery(cmd);
                    reset = false;
                    lineSuccess++;
                }
                catch (Exception e)
                {
                    //aca log 
                    logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.ShortFileName, line);
                    return false;
                }
            }
            //log number of line imported
            logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.ShortFileName, lineSuccess);
            return true;
        }

        public bool UpdateUnitType(List<UnitTypeInfo> impList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int line = 0;
            int lineSuccess = 0;
            bool reset = false;

            if (impList[0].Action == "O")
                reset = true;

            foreach (UnitTypeInfo a in impList)
            {
                line++;
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("ta_Imp_UnitType");
                    db.AddInParameter(cmd, "UnitTypeId", DbType.String, a.UnitTypeId);
                    db.AddInParameter(cmd, "UnitTypeName", DbType.String, a.UnitTypeName);
                    db.AddInParameter(cmd, "ModifiedBy", DbType.String, a.ModifiedBy);
                    db.AddInParameter(cmd, "Action", DbType.String, a.Action);
                    db.AddInParameter(cmd, "ExitOnDoubleInsert", DbType.String, param.Overwrite ? "0" : "1");
                    db.AddInParameter(cmd, "Reset", DbType.String, reset ? "1" : "0");
                    db.ExecuteNonQuery(cmd);
                    reset = false;
                    lineSuccess++;
                }
                catch (Exception e)
                {
                    //aca log 
                    logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                    return false;
                }
            }
            //log number of line imported
            logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.ShortFileName, lineSuccess);
            return true;
        }

        #endregion

        #region Import Data from SAP DL02_MASTER

        public bool UpdateBusinessPartner(List<BusinessPartnerInfo> impList, ACABatchParam param, ref bool skipError, string fileType)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int line = 0;
            int lineSuccess = 0;

            foreach (BusinessPartnerInfo a in impList)
            {
                line++;
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("ta_Imp_BusinessPartner");
                    db.AddInParameter(cmd, "BpId", DbType.String, a.BpId);
                    db.AddInParameter(cmd, "BpTypeId", DbType.String, a.BpTypeId);
                    db.AddInParameter(cmd, "TaxCode", DbType.String, a.TaxCode);
                    db.AddInParameter(cmd, "CitizenId", DbType.String, a.CitizenId);
                    db.AddInParameter(cmd, "PassportNo", DbType.String, a.PassportNo);
                    db.AddInParameter(cmd, "RegisterId", DbType.String, a.RegisterId);
                    db.AddInParameter(cmd, "VatCode", DbType.String, a.VatCode);
                    db.AddInParameter(cmd, "ModifiedBy", DbType.String, a.ModifiedBy);
                    db.AddInParameter(cmd, "Action", DbType.String, a.Action);
                    db.AddInParameter(cmd, "ExitOnDoubleInsert", DbType.String, param.Overwrite ? "0" : "1");
                    db.AddInParameter(cmd, "FileType", DbType.String, fileType);
                    int rowUpdated = db.ExecuteNonQuery(cmd);
                    lineSuccess += rowUpdated >= 1 ? 1 : 0;
                }
                catch (SqlException q)
                {
                    if (q.Message == "NUL_PK")
                    {
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, "Could not update or delete the non-existed record.", param.FileName, line);
                        skipError = true;
                        continue;
                    }

                    //Insert fail b/c of foreing constrain - pass
                    if (q.Number == 547)
                    {
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, q.Message, param.FileName, line);
                        skipError = true;
                        continue;
                    }
                    else
                    {
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, q.Message, param.FileName, line);
                        return false;
                    }
                }
                catch (Exception e)
                {
                    //aca log 
                    logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                    return false;
                }
            }

            //log number of line imported
            logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success,
                string.Format("Import Completed (From {0} record(s))", impList.Count),
                param.ShortFileName, lineSuccess);

            return true;
        }

        public bool UpdateBranch(List<BranchInfo> impList, ACABatchParam param, ref bool skipError)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int line = 0;
            int lineSuccess = 0;
            bool reset = false;

            if (impList[0].Action == "O")
                reset = true;

            foreach (BranchInfo a in impList)
            {
                line++;
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("ta_Imp_Branch");
                    db.AddInParameter(cmd, "BranchId", DbType.String, a.BranchId);
                    db.AddInParameter(cmd, "BranchName", DbType.String, a.BranchName);
                    db.AddInParameter(cmd, "BranchName2", DbType.String, a.BranchName2);
                    db.AddInParameter(cmd, "BranchNo", DbType.String, a.BranchNo);
                    db.AddInParameter(cmd, "Address", DbType.String, a.Address);
                    db.AddInParameter(cmd, "BranchLevel", DbType.String, a.BranchLevel);
                    db.AddInParameter(cmd, "BusinessArea", DbType.String, a.BusinessArea);
                    db.AddInParameter(cmd, "BusinessPlace", DbType.String, a.BusinessPlace);
                    db.AddInParameter(cmd, "CdType", DbType.String, a.CdType);
                    db.AddInParameter(cmd, "ParentBranchId", DbType.String, a.ParentBranchId);
                    db.AddInParameter(cmd, "ModifiedBy", DbType.String, a.ModifiedBy);
                    db.AddInParameter(cmd, "Action", DbType.String, a.Action);
                    db.AddInParameter(cmd, "ExitOnDoubleInsert", DbType.String, param.Overwrite ? "0" : "1");
                    db.AddInParameter(cmd, "Reset", DbType.String, reset ? "1" : "0");
                    db.ExecuteNonQuery(cmd);
                    reset = false;
                    lineSuccess++;
                }
                catch (SqlException q)
                {
                    //Insert fail b/c of foreing constrain - pass
                    if (q.Number == 547)
                    {
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, q.Message, param.FileName, line);
                        skipError = true;
                        continue;
                    }
                    else
                    {
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, q.Message, param.FileName, line);
                        return false;
                    }
                }
                catch (Exception e)
                {
                    //aca log 
                    logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                    return false;
                }
            }

            //log number of line imported
            logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.ShortFileName, lineSuccess);

            return true;
        }

        public bool UpdateMRU(List<MRUInfo> impList, ACABatchParam param, ref bool skipError)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int line = 0;
            int lineSuccess = 0;

            foreach (MRUInfo a in impList)
            {
                line++;
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("ta_Imp_MRU");
                    db.AddInParameter(cmd, "BranchId", DbType.String, a.BranchId);
                    db.AddInParameter(cmd, "MruId", DbType.String, a.MruId);
                    db.AddInParameter(cmd, "MruName", DbType.String, a.MruName);
                    db.AddInParameter(cmd, "AdvPay", DbType.String, a.AdvPay);
                    db.AddInParameter(cmd, "PortionId", DbType.String, a.PortionId);
                    db.AddInParameter(cmd, "PortionDesc", DbType.String, a.PortionDesc);
                    db.AddInParameter(cmd, "PtcNo", DbType.String, a.PtcNo);
                    db.AddInParameter(cmd, "ReaderType", DbType.String, a.ReaderType);
                    db.AddInParameter(cmd, "TravelHelp", DbType.String, a.TravelHelp);
                    db.AddInParameter(cmd, "HelpValidFrom", DbType.DateTime, a.HelpValidFrom);
                    db.AddInParameter(cmd, "HelpValidTo", DbType.DateTime, a.HelpValidTo);
                    db.AddInParameter(cmd, "MeterReaderId", DbType.String, a.MeterReaderId);
                    db.AddInParameter(cmd, "ModifiedBy", DbType.String, a.ModifiedBy);
                    db.AddInParameter(cmd, "IntownFlag", DbType.String, a.IntownFlag);
                    db.AddInParameter(cmd, "Action", DbType.String, a.Action);
                    db.AddInParameter(cmd, "ExitOnDoubleInsert", DbType.String, param.Overwrite ? "0" : "1");
                    db.ExecuteNonQuery(cmd);
                    lineSuccess++;
                }
                catch (SqlException q)
                {
                    //Insert fail b/c of foreing constrain - pass
                    if (q.Number == 547)
                    {
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, q.Message, param.FileName, line);
                        skipError = true;
                        continue;
                    }
                    else
                    {
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, q.Message, param.FileName, line);
                        return false;
                    }
                }
                catch (Exception e)
                {
                    //aca log 
                    logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                    return false;
                }
            }

            //log number of line imported
            logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.ShortFileName, lineSuccess);

            return true;
        }

        public bool UpdateMRUPlan(List<MRUPlanInfo> impList, ACABatchParam param, ref bool skipError)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int line = 0;
            int lineSuccess = 0;
            bool success = true;

            foreach (MRUPlanInfo a in impList)
            {
                line++;
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("ta_Imp_MRUPlan");
                    db.AddInParameter(cmd, "PortionId", DbType.String, a.PortionId);
                    db.AddInParameter(cmd, "BranchId", DbType.String, a.BranchId);
                    db.AddInParameter(cmd, "MruId", DbType.String, a.MruId);
                    db.AddInParameter(cmd, "Period", DbType.String, a.Period);
                    db.AddInParameter(cmd, "MeterReadDt", DbType.DateTime, a.MeterReadDt);
                    db.AddInParameter(cmd, "MeterReadActDt", DbType.DateTime, a.MeterReadActDt);
                    //db.AddInParameter(cmd, "TransferDt", DbType.DateTime, a.TransferDt);
                    db.AddInParameter(cmd, "TransferActDt", DbType.DateTime, a.TransferActDt);
                    //db.AddInParameter(cmd, "BillPrintDt", DbType.DateTime, a.BillPrintDt);
                    //db.AddInParameter(cmd, "BillPrintActDt", DbType.DateTime, a.BillPrintActDt);
                    //db.AddInParameter(cmd, "BookCreateDt", DbType.DateTime, a.BookCreateDt);
                    //db.AddInParameter(cmd, "BookCreateActDt", DbType.DateTime, a.BookCreateActDt);
                    db.AddInParameter(cmd, "ModifiedBy", DbType.String, a.ModifiedBy);
                    db.AddInParameter(cmd, "Action", DbType.String, a.Action);
                    db.AddInParameter(cmd, "ExitOnDoubleInsert", DbType.String, param.Overwrite ? "0" : "1");
                    lineSuccess += db.ExecuteNonQuery(cmd);                        
                }
                catch (SqlException q)
                {                        
                    if (q.Message == "NUL_PK")
                    {
                        string msg = "Could not update or delete the non-existed record.";
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, msg, param.FileName, line);
                        success = false;
                        continue;
                    }

                    //Insert fail b/c of foreing constrain - pass
                    else if (q.Number == 547)
                    {
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, q.Message, param.FileName, line);
                        skipError = true;
                        success = false;
                        continue;
                    }
                    else
                    {
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, q.Message, param.FileName, line);
                        return false;
                    }
                }
                catch (Exception e)
                {
                    //aca log 
                    logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                    return false;
                }
            }

            //log number of line imported
            logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.ShortFileName, lineSuccess);

            return success;
        }

        public bool UpdateCAByBulk(ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();

            try
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_imp_CAByBulk");
                db.AddInParameter(cmd, "FullPathName", DbType.String, param.FileName);
                db.AddInParameter(cmd, "BulkFotmatPath", DbType.String, param.BulkFormatPath);
                cmd.CommandTimeout = CMD_TIMEOUT;
                DataSet ds = db.ExecuteDataSet(cmd);
                DataRow dr = ds.Tables[ds.Tables.Count - 1].Rows[0];
                int totalRecord = Convert.ToInt32(dr[0]);
                int deleteNoRecord = Convert.ToInt32(dr[1]);
                int insertExistingRecord = Convert.ToInt32(dr[2]);
                int updateNoRecord = Convert.ToInt32(dr[3]);

                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success,
                    string.Format("Import Completed: DeleteNoRecord({0}), InsertExistRecord({1}), UpdateNoRecord({2})",
                    deleteNoRecord, insertExistingRecord, updateNoRecord),
                    param.ShortFileName, totalRecord);
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        public bool UpdateControllerByBulk(ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();

            try
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_imp_ControllerBulk");
                db.AddInParameter(cmd, "FullPathName", DbType.String, param.FileName);
                db.AddInParameter(cmd, "BulkFotmatPath", DbType.String, param.BulkFormatPath);
                cmd.CommandTimeout = CMD_TIMEOUT;
                DataSet ds = db.ExecuteDataSet(cmd);
                DataRow dr = ds.Tables[ds.Tables.Count - 1].Rows[0];
                int totalRecord = Convert.ToInt32(dr[0]);
                int deleteNoRecord = Convert.ToInt32(dr[1]);
                int insertExistingRecord = Convert.ToInt32(dr[2]);
                int updateNoRecord = Convert.ToInt32(dr[3]);

                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success,
                    string.Format("Import Completed: DeleteNoRecord({0}), InsertExistRecord({1}), UpdateNoRecord({2})",
                    deleteNoRecord, insertExistingRecord, updateNoRecord),
                    param.ShortFileName, totalRecord);
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        public bool UpdateMRUPlanByBulk(ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int CMD_TIMEOUT = 10000;

            try
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_imp_MRUPlanByBulk");
                db.AddInParameter(cmd, "FullPathName", DbType.String, param.FileName);
                db.AddInParameter(cmd, "BulkFotmatPath", DbType.String, param.BulkFormatPath);
                cmd.CommandTimeout = CMD_TIMEOUT;
                DataSet ds = db.ExecuteDataSet(cmd);
                DataRow dr = ds.Tables[ds.Tables.Count - 1].Rows[0];
                int totalRecord = Convert.ToInt32(dr[0]);
                int deleteNoRecord = Convert.ToInt32(dr[1]);
                int insertExistingRecord = Convert.ToInt32(dr[2]);
                int updateNoRecord = Convert.ToInt32(dr[3]);

                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success,
                    string.Format("Import Completed: DeleteNoRecord({0}), InsertExistRecord({1}), UpdateNoRecord({2})",
                    deleteNoRecord, insertExistingRecord, updateNoRecord),
                    param.ShortFileName, totalRecord);
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        public bool UpdateContractAccount(List<ContractAccountInfo> impList, ACABatchParam param, ref bool skipError, string fileType)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int line = 0;
            int lineSuccess = 0;
            bool success = true;

            foreach (ContractAccountInfo a in impList)
            {
                line++;
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("ta_Imp_ContractAccount");
                    db.AddInParameter(cmd, "CaId", DbType.String, a.CaId);
                    db.AddInParameter(cmd, "BpId", DbType.String, a.BpId);
                    db.AddInParameter(cmd, "TechBranchId", DbType.String, a.TechBranchId);
                    db.AddInParameter(cmd, "CommBranchId", DbType.String, a.CommBranchId);
                    db.AddInParameter(cmd, "MruId", DbType.String, a.MruId);
                    db.AddInParameter(cmd, "CaName", DbType.String, a.CaName);
                    db.AddInParameter(cmd, "CaAddress", DbType.String, a.CaAddress);
                    db.AddInParameter(cmd, "CtId", DbType.String, a.CtId);
                    db.AddInParameter(cmd, "PmId", DbType.String, a.PmId);
                    db.AddInParameter(cmd, "AccountClassId", DbType.String, a.AccountClassId);
                    db.AddInParameter(cmd, "SecurityDeposit", DbType.Decimal, a.SecurityDeposit);
                    db.AddInParameter(cmd, "MeterInstallDt", DbType.DateTime, a.MeterInstallDt);
                    db.AddInParameter(cmd, "MeterSizeId", DbType.String, a.MeterSizeId);
                    db.AddInParameter(cmd, "CollectArea", DbType.String, a.CollectArea);
                    db.AddInParameter(cmd, "PaidBy", DbType.String, a.PaidBy);
                    db.AddInParameter(cmd, "TransportHelp", DbType.Decimal, a.TransportHelp);
                    db.AddInParameter(cmd, "PenaltyWaiveFlag", DbType.String, a.PenaltyWaiveFlag);
                    db.AddInParameter(cmd, "ContractValidFrom", DbType.DateTime, a.ContractValidFrom);
                    db.AddInParameter(cmd, "ContractValidTo", DbType.DateTime, a.ContractValidTo);
                    db.AddInParameter(cmd, "GroupIvReceiptType", DbType.String, a.ReceiptType);
                    db.AddInParameter(cmd, "ReceiptPrintName", DbType.String, a.ReceiptPrintName);                        
                    db.AddInParameter(cmd, "InterestKey", DbType.String, a.InterestKey);                  
                    db.AddInParameter(cmd, "ControllerId", DbType.String, a.ControllerId);                        
                    db.AddInParameter(cmd, "ModifiedBy", DbType.String, a.ModifiedBy);
                    db.AddInParameter(cmd, "Action", DbType.String, a.Action);
                    db.AddInParameter(cmd, "ExitOnDoubleInsert", DbType.String, param.Overwrite ? "0" : "1");
                    db.AddInParameter(cmd, "FileType", DbType.String, fileType);

                    //Tax13
                    db.AddInParameter(cmd, "CaTaxId", DbType.String, a.CaTaxId);
                    db.AddInParameter(cmd, "CaTaxBranch", DbType.String, a.CaTaxBranch);

                    int rowUpdated = db.ExecuteNonQuery(cmd);
                    lineSuccess += rowUpdated >= 1 ? 1 : 0;      
                }
                catch (SqlException q)
                {
                    if (q.Message.Contains("NUL_PK"))
                    {
                        string[] tmp = q.Message.Split('|');
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, tmp[1].ToString() + " is not exist. Updating Failed", param.ShortFileName, line);
                        skipError = true;
                        success = false;
                        continue;
                    }
                    else if (q.Message.Contains("DEL_PK"))
                    {
                        string[] tmp = q.Message.Split('|');
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, tmp[1].ToString() + " is not exist. Deleting Failed", param.ShortFileName, line);
                        skipError = true;
                        success = false;
                        continue;
                    }

                    //Insert fail b/c of foreing constrain - pass
                    else if (q.Number == 547)
                    {
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, q.Message, param.ShortFileName, line);
                        skipError = true;
                        success = false;
                        continue;
                    }
                    else
                    {
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, q.Message, param.ShortFileName, line);
                        return false;
                    }
                }
                catch (Exception e)
                {
                    //aca log 
                    logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.ShortFileName, line);
                    return false;
                }
            }

            //log number of succeed-line imported
            logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success,
                string.Format("Import Completed (From {0} record(s))", impList.Count),
                param.ShortFileName, lineSuccess);

            return success;
        }

        public bool UpdateEmployee(List<EmployeeInfo> impList, ACABatchParam param, ref bool skipError)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int line = 0;
            int lineSuccess = 0;
            bool reset = true;

            foreach (EmployeeInfo a in impList)
            {
                line++;
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("ta_Imp_Employee");
                    db.AddInParameter(cmd, "EmployeeId", DbType.String, a.EmployeeId);
                    db.AddInParameter(cmd, "FirstName", DbType.String, a.FirstName);
                    db.AddInParameter(cmd, "LastName", DbType.String, a.LastName);
                    db.AddInParameter(cmd, "Department", DbType.String, a.Department);
                    db.AddInParameter(cmd, "BranchId", DbType.String, a.BranchId);
                    db.AddInParameter(cmd, "ModifiedBy", DbType.String, a.ModifiedBy);
                    db.AddInParameter(cmd, "Action", DbType.String, a.Action);
                    db.AddInParameter(cmd, "ExitOnDoubleInsert", DbType.String, param.Overwrite ? "0" : "1");
                    db.AddInParameter(cmd, "Reset", DbType.String, reset ? "1" : "0");
                    db.ExecuteNonQuery(cmd);
                    reset = false;
                    lineSuccess++;
                }
                catch (Exception e)
                {
                    //aca log 
                    logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.ShortFileName, line);
                    return false;
                }
            }

            //log number of line imported
            logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.ShortFileName, lineSuccess);

            return true;
        }

        public bool UpdateAgencyAsset(List<AgencyAssetInfo> impList, ACABatchParam param, ref bool skipError)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int line = 0;
            int lineSuccess = 0;
            bool success = false;

            foreach (AgencyAssetInfo a in impList)
            {
                line++;
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("ta_Imp_AgencyAsset");
                    db.AddInParameter(cmd, "AssetId", DbType.String, a.AssetId);
                    db.AddInParameter(cmd, "CaId", DbType.String, a.CaId);
                    db.AddInParameter(cmd, "AssetType", DbType.String, a.AssetType);
                    db.AddInParameter(cmd, "AssetTypeDesc", DbType.String, a.AssetTypeDesc);
                    db.AddInParameter(cmd, "AssetValue", DbType.Decimal, a.AssetValue);
                    db.AddInParameter(cmd, "ModifiedBy", DbType.String, a.ModifiedBy);
                    db.AddInParameter(cmd, "Action", DbType.String, a.Action);
                    db.AddInParameter(cmd, "ExitOnDoubleInsert", DbType.String, param.Overwrite ? "0" : "1");
                    db.ExecuteNonQuery(cmd);
                    lineSuccess++;
                }
                catch (SqlException q)
                {                        
                    if (q.Message == "NUL_PK")
                    {
                        string msg = "Could not update or delete the non-existed record.";
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, msg, param.ShortFileName, line);
                        success = false;
                        continue;
                    }

                    //Insert fail b/c of foreing constrain - pass
                    else if (q.Number == 547)
                    {
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, q.Message, param.ShortFileName, line);
                        skipError = true;
                        success = false;
                        continue;
                    }
                    else
                    {
                        logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, q.Message, param.ShortFileName, line);
                        return false;
                    }
                }
                catch (Exception e)
                {
                    //aca log 
                    logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.ShortFileName, line);
                    return false;
                }
            }

            //log number of line imported
            logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.ShortFileName, lineSuccess);

            return success;
        }

        public bool UpdateBank(List<BankInfo> impList, ACABatchParam param, ref bool skipError)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int line = 0;
            int lineSuccess = 0;
            bool reset = false;

            if(impList[0].Action == "O")
                reset = true;

            foreach (BankInfo a in impList)
            {
                line++;
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("ta_Imp_Bank");
                    db.AddInParameter(cmd, "BankKey", DbType.String, a.BankKey);
                    db.AddInParameter(cmd, "BankName", DbType.String, a.BankName);
                    db.AddInParameter(cmd, "ModifiedBy", DbType.String, a.ModifiedBy);
                    db.AddInParameter(cmd, "Action", DbType.String, a.Action);
                    db.AddInParameter(cmd, "ExitOnDoubleInsert", DbType.String, param.Overwrite ? "0" : "1");
                    db.AddInParameter(cmd, "Reset", DbType.String, reset ? "1" : "0");
                    db.ExecuteNonQuery(cmd);
                    reset = false;
                    lineSuccess++;
                }
                catch (Exception e)
                {
                    //aca log 
                    logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.ShortFileName, line);
                    return false;
                }
            }

            //log number of line imported
            logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.ShortFileName, lineSuccess);

            return true;
        }

        public bool UpdateBankAccount(List<BankAccountInfo> impList, ACABatchParam param, ref bool skipError)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int line = 0;
            int lineSuccess = 0;
            bool reset = false;

            if (impList[0].Action == "O")
                reset = true;

            foreach (BankAccountInfo a in impList)
            {
                line++;
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("ta_Imp_BankAccount");
                    db.AddInParameter(cmd, "Bankkey", DbType.String, a.BankKey);
                    db.AddInParameter(cmd, "AccountNo", DbType.String, a.AccountNo);
                    db.AddInParameter(cmd, "BusinessPlace", DbType.String, a.BusinessPlace);
                    db.AddInParameter(cmd, "ClearingAccNo", DbType.String, a.ClearingAccNo);
                    db.AddInParameter(cmd, "HouseBank", DbType.String, a.HouseBank);
                    db.AddInParameter(cmd, "AccountType", DbType.String, a.AccountType);
                    db.AddInParameter(cmd, "ModifiedBy", DbType.String, a.ModifiedBy);
                    db.AddInParameter(cmd, "Action", DbType.String, a.Action);
                    db.AddInParameter(cmd, "ExitOnDoubleInsert", DbType.String, param.Overwrite ? "0" : "1");
                    db.AddInParameter(cmd, "Reset", DbType.String, reset ? "1" : "0");
                    db.ExecuteNonQuery(cmd); 
                    //first row affect will return no. of rows effected by set active=0
                    //that's why we couldn't use lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    reset = false;
                    lineSuccess++;
                }
                catch (SqlException q)
                {
                    logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, q.Message, param.ShortFileName, line);

                    //Insert fail b/c of foreing constrain - pass
                    if (q.Number == 547)
                    {
                        skipError = true;
                        continue;
                    }
                    //else if (q.Number == 90100)
                    //{
                    //    skipError = true;
                    //    continue;
                    //}
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    //aca log 
                    logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.ShortFileName, line);
                    return false;
                }
            }

            //log number of line imported
            logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.ShortFileName, lineSuccess);

            return true;
        }

        public bool UpdateMainSub(List<MainSubInfo> impList, ACABatchParam param, ref bool skipError)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int line = 0;
            int lineSuccess = 0;
            bool reset = false;

            if (impList[0].Action == "O")
                reset = true;

            foreach (MainSubInfo a in impList)
            {
                line++;
                try
                {
                    //insert to debtType just id, name
                    DbCommand cmd = db.GetStoredProcCommand("ta_Imp_DebtType");
                    db.AddInParameter(cmd, "DtId", DbType.String, "M" + a.DtId);
                    db.AddInParameter(cmd, "DtName", DbType.String, a.DtName);
                    db.AddInParameter(cmd, "TaxCode", DbType.String, a.TaxCode);
                    db.AddInParameter(cmd, "AccountCat", DbType.String, a.AccountCat);
                    db.AddInParameter(cmd, "NonInvoicePaymentFlag", DbType.String, a.NonInvoicePaymentFlag);
                    db.AddInParameter(cmd, "ModifiedBy", DbType.String, a.ModifiedBy);
                    db.AddInParameter(cmd, "Action", DbType.String, a.Action);
                    db.AddInParameter(cmd, "ExitOnDoubleInsert", DbType.String, param.Overwrite ? "0" : "1");
                    db.AddInParameter(cmd, "Reset", DbType.String, reset ? "1" : "0");
                    db.ExecuteNonQuery(cmd);
                    reset = false;
                    lineSuccess++;
                }
                catch (Exception e)
                {
                    //aca log 
                    logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.ShortFileName, line);
                    return false;
                }
            }

            //log number of line imported
            logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.ShortFileName, lineSuccess);

            return true;
        }

        public bool UpdateCompany(List<CompanyInfo> impList, ACABatchParam param, ref bool skipError)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int line = 0;
            int lineSuccess = 0;
            bool reset = true;

            foreach (CompanyInfo a in impList)
            {
                line++;
                try
                {
                    //insert to debtType just id, name
                    DbCommand cmd = db.GetStoredProcCommand("ta_Imp_Company");
                    lineSuccess += db.ExecuteNonQuery(cmd);
                    reset = false;
                }
                catch (Exception e)
                {
                    //aca log 
                    logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.ShortFileName, line);
                    return false;
                }
            }

            //log number of line imported
            logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.ShortFileName, lineSuccess);

            return true;
        }

        #endregion

        #region Import Data from SAP DL03_BILLING

        public int UpdateBillingData(DbTransaction trans, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            int CMD_TIMEOUT = 100000;
            DbCommand cmd = db.GetStoredProcCommand("ta_imp_BillDetail");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "FullPathName", DbType.String, param.FileName);
            db.AddInParameter(cmd, "FileName", DbType.String, param.ShortFileName);
            db.AddInParameter(cmd, "BulkFotmatPath", DbType.String, param.BulkFormatPath);
            return (int)db.ExecuteScalar(cmd, trans);
        }

        #endregion

        #region Import Data from SAP DL04_TRANSACTION

        public string GetARInvoiceNo(string prefix, ACABatchParam param)
        {
            ACABatchLogger logger = ACABatchLogger.GetInstance();

            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_get_AR_InvoiceNo");
                db.AddInParameter(cmd, "Prefix", DbType.String, prefix);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                string tmp = DaHelper.GetString(dt.Rows[0], "MaxInvNo");

                cmd = db.GetStoredProcCommand("ta_fix_SpotBillNonReceipt");
                db.ExecuteNonQuery(cmd);

                cmd = db.GetStoredProcCommand("ta_Fix_Clarify_SpotBill");
                db.ExecuteNonQuery(cmd);

                //if (dt.Rows.Count == 0) 
                //    return "1";
                //else
                //    return  DaHelper.GetString(dt.Rows[0], "MaxInvNo").Trim();

                if (String.IsNullOrEmpty(tmp))
                    return "1";
                else
                    return tmp.Trim();
                
            }
            catch (Exception e)
            {
                //aca log 
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, "Function_GetARInvoiceNo:" + e.Message,param.ShortFileName);
                return null;
            }
        }

        public void CheckMissingInvoiceNo(ACABatchParam param)
        {
            ACABatchLogger logger = ACABatchLogger.GetInstance();

            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_imp_check_AR_MissingInvoiceNo");
                db.AddInParameter(cmd, "pFileName", DbType.String, param.ShortFileName);
                //DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                db.ExecuteNonQuery(cmd);
                
            }
            catch (Exception e)
            {
                //aca log 
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, "DA:Function_CheckMissingInvoiceNo:" + e.Message, param.ShortFileName);
            }
        }

        public bool UpdateAR(ACABatchParam param, string fileType)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int CMD_TIMEOUT = 10000;
            try
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_imp_ARBulk");
                db.AddInParameter(cmd, "FileType", DbType.String, fileType);
                db.AddInParameter(cmd, "FullPathName", DbType.String, param.FileName);
                db.AddInParameter(cmd, "FileName", DbType.String, param.ShortFileName);
                db.AddInParameter(cmd, "BulkFotmatPath", DbType.String, param.BulkFormatPath);
                cmd.CommandTimeout = CMD_TIMEOUT;
                DataSet ds = db.ExecuteDataSet(cmd);
                DataRow dr = ds.Tables[ds.Tables.Count - 1].Rows[0];
                int totalRecord = Convert.ToInt32(dr[0]);
                int deleteNoRecord = Convert.ToInt32(dr[1]);
                int insertExistingRecord = Convert.ToInt32(dr[2]);
                int updateNoRecord = Convert.ToInt32(dr[3]);

                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success,
                    string.Format("Import Completed: DeleteNoRecord({0}), InsertExistRecord({1}), UpdateNoRecord({2})",
                    deleteNoRecord, insertExistingRecord, updateNoRecord),
                    param.ShortFileName, totalRecord);

            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        public bool UpdateARElectric(ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int CMD_TIMEOUT = 10000;
            try
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_imp_ARElectricBulk");
                db.AddInParameter(cmd, "FullPathName", DbType.String, param.FileName);
                db.AddInParameter(cmd, "FileName", DbType.String, param.ShortFileName);
                db.AddInParameter(cmd, "BulkFotmatPath", DbType.String, param.BulkFormatPath);
                cmd.CommandTimeout = CMD_TIMEOUT;
                DataSet ds = db.ExecuteDataSet(cmd);
                DataRow dr = ds.Tables[ds.Tables.Count - 1].Rows[0];
                int totalRecord = Convert.ToInt32(dr[0]);
                int deleteNoRecord = Convert.ToInt32(dr[1]);
                int insertExistingRecord = Convert.ToInt32(dr[2]);
                int updateNoRecord = Convert.ToInt32(dr[3]);

                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success,
                    string.Format("Import Completed: DeleteNoRecord({0}), InsertExistRecord({1}), UpdateNoRecord({2})",
                    deleteNoRecord, insertExistingRecord, updateNoRecord),
                    param.ShortFileName, totalRecord);
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        public bool UpdateDisconnectionDoc(List<DisconnectionDocInfo> impList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int line = 0;
            int lineSuccess = 0;
            bool reset = true;

            foreach (DisconnectionDocInfo obj in impList)
            {
                line++;
                try
                {
                    //insert to debtType just id, name
                    DbCommand cmd = db.GetStoredProcCommand("ta_Imp_DisconnectionDoc");
                    db.AddInParameter(cmd, "pDiscNo", DbType.String, obj.DiscNo);
                    db.AddInParameter(cmd, "pCreatedDt", DbType.DateTime, obj.CreatedDt);
                    db.AddInParameter(cmd, "pDiscStatusId", DbType.String, obj.DiscStatusId);
                    db.AddInParameter(cmd, "pReleaseDt", DbType.DateTime, obj.ReleaseDt);
                    db.AddInParameter(cmd, "pWorkOrderNo", DbType.String, obj.WorkOrderNo);
                    db.AddInParameter(cmd, "pWorkCenter", DbType.String, obj.WorkCenter);
                    db.AddInParameter(cmd, "pDiscPlanStart", DbType.DateTime, obj.DiscPlanStart);
                    db.AddInParameter(cmd, "pDiscPlanEnd", DbType.DateTime, obj.DiscPlanEnd);                        
                    db.AddInParameter(cmd, "pPostpConfirm", DbType.DateTime, obj.PostpConfirm);
                    db.AddInParameter(cmd, "pFuseRemConfirm", DbType.DateTime, obj.FuseRemConfirm);
                    db.AddInParameter(cmd, "pMeterRemConfirm", DbType.DateTime, obj.MeterRemConfirm);
                    db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);
                    db.AddInParameter(cmd, "pAction", DbType.String, obj.Action);
                    db.AddInParameter(cmd, "pExitOnDoubleInsert", DbType.String, param.Overwrite ? "0" : "1");
                    //db.AddInParameter(cmd, "pReset", DbType.String, reset ? "1" : "0");
                    lineSuccess += db.ExecuteNonQuery(cmd);
                    reset = false;
                }
                catch (Exception e)
                {
                    //aca log 
                    logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.FileName, line);
                    return false;
                }
            }

            //log number of line imported
            logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.ShortFileName, lineSuccess);

            return true;
        }

        public bool UpdateRTDisconnectionDocCaDoc(List<RTDisconnectionDocCaDocInfo> impList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int line = 0;
            int lineSuccess = 0;
            bool reset = true;

            foreach (RTDisconnectionDocCaDocInfo obj in impList)
            {
                line++;
                try
                {
                    //insert to debtType just id, name
                    DbCommand cmd = db.GetStoredProcCommand("ta_Imp_RTDisconnectionDocCaDoc");
                    db.AddInParameter(cmd, "pDiscNo", DbType.String, obj.DiscNo);
                    db.AddInParameter(cmd, "pCaDocNo", DbType.String, obj.CaDocNo);
                    db.AddInParameter(cmd, "pCancelFlag", DbType.String, obj.CancelFlag);                       
                    db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);
                    db.AddInParameter(cmd, "pAction", DbType.String, obj.Action);
                    db.AddInParameter(cmd, "pExitOnDoubleInsert", DbType.String, param.Overwrite ? "0" : "1");
                    //db.AddInParameter(cmd, "Reset", DbType.String, reset ? "1" : "0");
                    lineSuccess += db.ExecuteNonQuery(cmd);
                    reset = false;
                }
                catch (Exception e)
                {
                    //aca log 
                    logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.ShortFileName, line);
                    return false;
                }
            }

            //log number of line imported
            logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.ShortFileName, lineSuccess);

            return true;
        }

        public bool UpdateDisconnectionStatus(List<DisconnectionStatus> impList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int line = 0;
            int lineSuccess = 0;
            bool reset = true;

            foreach (DisconnectionStatus obj in impList)
            {
                line++;
                try
                {
                    //insert to debtType just id, name
                    DbCommand cmd = db.GetStoredProcCommand("ta_Imp_DisconnectionStatus");
                    db.AddInParameter(cmd, "Caid", DbType.String, obj.CaId);
                    db.AddInParameter(cmd, "DiscNo", DbType.String, obj.DiscNo);
                    db.AddInParameter(cmd, "DiscStatusId", DbType.String, obj.DiscStatusId);
                    db.AddInParameter(cmd, "FixValue", DbType.String, obj.FixValue);
                    db.AddInParameter(cmd, "FileName", DbType.String, param.ShortFileName);
                    lineSuccess += db.ExecuteNonQuery(cmd);
                    
                    reset = false;
                }
                catch (Exception e)
                {
                    //aca log 
                    logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.ShortFileName, line);
                    return false;
                }
            }

            //log number of line imported
            logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.ShortFileName, lineSuccess);

            return true;
        }

        public bool UpdateCancelBPMPayment(ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int CMD_TIMEOUT = 10000;
            try
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_imp_CancelPaidFromSAPBulk");
                db.AddInParameter(cmd, "FullPathName", DbType.String, param.FileName);
                db.AddInParameter(cmd, "FileName", DbType.String, param.ShortFileName);
                db.AddInParameter(cmd, "BulkFotmatPath", DbType.String, param.BulkFormatPath);
                cmd.CommandTimeout = CMD_TIMEOUT;
                DataSet ds = db.ExecuteDataSet(cmd);
                DataRow dr = ds.Tables[ds.Tables.Count - 1].Rows[0];
                int totalRecord = Convert.ToInt32(dr[0]);
                int deleteNoRecord = Convert.ToInt32(dr[1]);
                int insertExistingRecord = Convert.ToInt32(dr[2]);
                int updateNoRecord = Convert.ToInt32(dr[3]);

                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success,
                    string.Format("Import Completed: DeleteNoRecord({0}), InsertExistRecord({1}), UpdateNoRecord({2})",
                    deleteNoRecord, insertExistingRecord, updateNoRecord),
                    param.ShortFileName, totalRecord);

            }
            catch (Exception)
            {
                throw;
            }


            return true;
        }

        public bool UpdateCancelBPMPayment_50A(ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int CMD_TIMEOUT = 10000;
            try
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_imp_CancelPaidFromSAPBulk_50A");
                db.AddInParameter(cmd, "FullPathName", DbType.String, param.FileName);
                db.AddInParameter(cmd, "FileName", DbType.String, param.ShortFileName);
                db.AddInParameter(cmd, "BulkFotmatPath", DbType.String, param.BulkFormatPath);
                cmd.CommandTimeout = CMD_TIMEOUT;
                DataSet ds = db.ExecuteDataSet(cmd);
                DataRow dr = ds.Tables[ds.Tables.Count - 1].Rows[0];
                int totalRecord = Convert.ToInt32(dr[0]);
                int deleteNoRecord = Convert.ToInt32(dr[1]);
                int insertExistingRecord = Convert.ToInt32(dr[2]);
                int updateNoRecord = Convert.ToInt32(dr[3]);

                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success,
                    string.Format("Import Completed: DeleteNoRecord({0}), InsertExistRecord({1}), UpdateNoRecord({2})",
                    deleteNoRecord, insertExistingRecord, updateNoRecord),
                    param.ShortFileName, totalRecord);

            }
            catch (Exception)
            {
                throw;
            }


            return true;
        }

        public bool UpdateARSubGroupInvoice(ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int CMD_TIMEOUT = 10000;
            try
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_imp_ARSubGroupInvoiceBulk");
                //db.AddInParameter(cmd, "FileType", DbType.String, fileType);
                db.AddInParameter(cmd, "FullPathName", DbType.String, param.FileName);
                db.AddInParameter(cmd, "FileName", DbType.String, param.ShortFileName);
                db.AddInParameter(cmd, "BulkFotmatPath", DbType.String, param.BulkFormatPath);
                cmd.CommandTimeout = CMD_TIMEOUT;
                DataSet ds = db.ExecuteDataSet(cmd);
                DataRow dr = ds.Tables[ds.Tables.Count - 1].Rows[0];
                int totalRecord = Convert.ToInt32(dr[0]);
                int deleteNoRecord = Convert.ToInt32(dr[1]);
                int insertExistingRecord = Convert.ToInt32(dr[2]);
                int updateNoRecord = Convert.ToInt32(dr[3]);

                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success,
                    string.Format("Import Completed: DeleteNoRecord({0}), InsertExistRecord({1}), UpdateNoRecord({2})",
                    deleteNoRecord, insertExistingRecord, updateNoRecord),
                    param.ShortFileName, totalRecord);

            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        public bool UpdateARDisconnect(ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int CMD_TIMEOUT = 10000;
            try
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_imp_ARDisconnectBulk");
                //db.AddInParameter(cmd, "FileType", DbType.String, fileType);
                db.AddInParameter(cmd, "FullPathName", DbType.String, param.FileName);
                db.AddInParameter(cmd, "FileName", DbType.String, param.ShortFileName);
                db.AddInParameter(cmd, "BulkFotmatPath", DbType.String, param.BulkFormatPath);
                cmd.CommandTimeout = CMD_TIMEOUT;
                DataSet ds = db.ExecuteDataSet(cmd);
                DataRow dr = ds.Tables[ds.Tables.Count - 1].Rows[0];
                int totalRecord = Convert.ToInt32(dr[0]);
                int deleteNoRecord = Convert.ToInt32(dr[1]);
                int insertExistingRecord = Convert.ToInt32(dr[2]);
                int updateNoRecord = Convert.ToInt32(dr[3]);

                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success,
                    string.Format("Import Completed: DeleteNoRecord({0}), InsertExistRecord({1}), UpdateNoRecord({2})",
                    deleteNoRecord, insertExistingRecord, updateNoRecord),
                    param.ShortFileName, totalRecord);

            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        #endregion

        #region Import Data from SAP DL05_PAYFROMSAP
        public bool UpdatePayFromSAP(ACABatchParam param, string fileType)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int CMD_TIMEOUT = 10000;

            try
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_imp_PayFromSAPBulk_20A");
                if (fileType == "TRR0020A")
                {
                    cmd = db.GetStoredProcCommand("ta_imp_PayFromSAPBulk_20A");
                }
                else if (fileType == "TRR0020B")
                {
                    cmd = db.GetStoredProcCommand("ta_imp_PayFromSAPBulk_20B");
                }
                else if (fileType == "TRR0020C")
                {
                    cmd = db.GetStoredProcCommand("ta_imp_PayFromSAPBulk_20C");
                }
                else if (fileType == "TRR00200")
                {
                    cmd = db.GetStoredProcCommand("ta_imp_PayFromSAPBulk");
                }
                db.AddInParameter(cmd, "FullPathName", DbType.String, param.FileName);
                db.AddInParameter(cmd, "BulkFotmatPath", DbType.String, param.BulkFormatPath);
                cmd.CommandTimeout = CMD_TIMEOUT;
                DataSet ds = db.ExecuteDataSet(cmd);

                DataRow dr = ds.Tables[ds.Tables.Count - 1].Rows[0];
                int totalRecord = Convert.ToInt32(dr[0]);
                int payNoRecord = Convert.ToInt32(dr[1]);
                int insertExistRecord = Convert.ToInt32(dr[2]);
                int updateNoRecord = Convert.ToInt32(dr[3]);
                int deleteNoRecord = Convert.ToInt32(dr[4]);                    
                
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success,
                    string.Format("Import Completed: PayNoRecord({0}), InsertExistRecord({1}), UpdateNoRecord({2}), DeleteNoRecord({3})",
                    new object[] { payNoRecord, insertExistRecord, updateNoRecord, deleteNoRecord}),
                    param.ShortFileName, totalRecord);
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        #endregion

        #region Import Data from SAP DL06_DIRECTDEBIT        

        public bool UpdateBillingDetailStatus(List<BillingDetailStatusInfo> impList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int line = 0;
            int lineSuccess = 0;

            foreach (BillingDetailStatusInfo obj in impList)
            {
                line++;
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("ta_imp_BillingStatus");
                    db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                    db.AddInParameter(cmd, "pPortionId", DbType.String, obj.PortionId);
                    db.AddInParameter(cmd, "pMruId", DbType.String, obj.MruId);
                    db.AddInParameter(cmd, "pBillPred", DbType.String, obj.BillPred);
                    db.AddInParameter(cmd, "pBaseCount", DbType.Int32, obj.BaseCount);
                    db.AddInParameter(cmd, "pProcCount", DbType.Int32, obj.ProcCount);
                    db.AddInParameter(cmd, "pFixCount", DbType.Int32, obj.FixCount);
                    db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);
                    db.ExecuteNonQuery(cmd);
                    lineSuccess++;
                }
                catch (Exception e)
                {
                    //aca log 
                    logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.ShortFileName, line);
                    return false;
                }
            }
            //log number of line imported
            logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.ShortFileName, lineSuccess);

            return true;
        }

        public bool UpdateBillingReverse(List<BillingReverseInfo> impList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int line = 0;
            int lineSuccess = 0;

            foreach (BillingReverseInfo obj in impList)
            {
                line++;
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("ta_imp_UpdReverseBill");
                    db.AddInParameter(cmd, "pInoviceNo", DbType.String, obj.W_01_invoiceNo);
                    db.AddInParameter(cmd, "pSapPrintDate", DbType.String, obj.W_1970_print_dt);
                    db.AddInParameter(cmd, "pOrgDoc", DbType.String, obj.W_1910_org_doc);
                    db.AddInParameter(cmd, "pStoBudat", DbType.String, obj.Sto_budat);
                    db.AddInParameter(cmd, "pIntoOpbel", DbType.String, obj.Intopbel);
                    db.AddInParameter(cmd, "pIcReason", DbType.String, obj.Iceason);
                    db.AddInParameter(cmd, "pStokz", DbType.String, obj.Stokz);                        
                    db.ExecuteNonQuery(cmd);
                    lineSuccess++;
                }
                catch (Exception e)
                {
                    //aca log 
                    logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.ShortFileName, line);
                    return false;
                }
            }
            //log number of line imported
            logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.ShortFileName, lineSuccess);

            return true;
        }

        public bool UpdateBillStatusInfoReverse(List<BillingReverseInfo> impList, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int line = 0;
            int lineSuccess = 0;

            foreach (BillingReverseInfo obj in impList)
            {
                line++;
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("ta_imp_UpdReverseBill");
                    db.AddInParameter(cmd, "pInoviceNo", DbType.String, obj.W_01_invoiceNo);
                    db.AddInParameter(cmd, "pSapPrintDate", DbType.String, obj.W_1970_print_dt);
                    db.AddInParameter(cmd, "pOrgDoc", DbType.String, obj.W_1910_org_doc);
                    db.AddInParameter(cmd, "pStoBudat", DbType.String, obj.Sto_budat);
                    db.AddInParameter(cmd, "pIntoOpbel", DbType.String, obj.Intopbel);
                    db.AddInParameter(cmd, "pIcReason", DbType.String, obj.Iceason);
                    db.AddInParameter(cmd, "pStokz", DbType.String, obj.Stokz);
                    db.ExecuteNonQuery(cmd);
                    lineSuccess++;
                }
                catch (Exception e)
                {
                    //aca log 
                    logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessingError, e.Message, param.ShortFileName, line);
                    return false;
                }
            }
            //log number of line imported
            logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Import Completed", param.ShortFileName, lineSuccess);

            return true;
        }

        public bool UpdateDirectDebit(DbTransaction trans, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int CMD_TIMEOUT = 10000;

            try
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_imp_BillUpdxBulk");
                db.AddInParameter(cmd, "FullPathName", DbType.String, param.FileName);
                db.AddInParameter(cmd, "FileName", DbType.String, param.ShortFileName);
                db.AddInParameter(cmd, "BulkFotmatPath", DbType.String, param.BulkFormatPath);
                cmd.CommandTimeout = CMD_TIMEOUT;
                int totalRecord = (int)db.ExecuteScalar(cmd, trans);

                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, string.Format("Import BillUpdateX successed.", param.FileName, totalRecord), param.ShortFileName, totalRecord);
            }
            catch (Exception)
            {
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Update to database failed! : " + param.ShortFileName);
                throw;
            }

            return true;
        }

        public bool UpdateReceiptx(DbTransaction trans, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int CMD_TIMEOUT = 10000;

            try
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_imp_ReceiptxBulk");
                db.AddInParameter(cmd, "FullPathName", DbType.String, param.FileName);
                db.AddInParameter(cmd, "FileName", DbType.String, param.ShortFileName);
                db.AddInParameter(cmd, "BulkFotmatPath", DbType.String, param.BulkFormatPath);
                cmd.CommandTimeout = CMD_TIMEOUT;
                int totalRecord = (int)db.ExecuteScalar(cmd);

                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, string.Format("Import ReceiptX successed.", param.FileName, totalRecord), param.ShortFileName, totalRecord);
            }
            catch (Exception)
            {
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Update to database failed! : " + param.ShortFileName);
                throw;
            }

            return true;
        }
        
        public bool UpdatePWBBillStatus(DbTransaction trans, ACABatchParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            int CMD_TIMEOUT = 10000;

            try
            {
                DbCommand cmd = db.GetStoredProcCommand("ta_imp_PWBStatusBulk");
                db.AddInParameter(cmd, "FullPathName", DbType.String, param.FileName);
                db.AddInParameter(cmd, "BulkFotmatPath", DbType.String, param.BulkFormatPath);
                cmd.CommandTimeout = CMD_TIMEOUT;
                int totalRecord = (int)db.ExecuteScalar(cmd, trans);
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, string.Format("Import PWBBillStatus successed.", param.FileName, totalRecord), param.ShortFileName, totalRecord);
            }
            catch (Exception)
            {
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Update to database failed! : " + param.ShortFileName);
                throw;
            }
            return true;
        }

        #endregion

        #region Export Data to SAP

        public List<AG_Compensation> GetAgencyCompensation(string branchId, ACABatchParam param)
        {
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            List<AG_Compensation> expList = new List<AG_Compensation>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_AgencyCompensation");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);  //TODO: Add branch id when integration complete.
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

                string tmp1 = string.Format("Prepare data to export for {0}", branchId);
                logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessData, tmp1,  dt.Rows.Count);

                AG_Compensation ac = new AG_Compensation();
                string cmId = String.Empty;

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        if (cmId == String.Empty)
                        {
                            ac = new AG_Compensation();
                            ac.CmId = DaHelper.GetString(r, "CmId");
                            ac.CaId = DaHelper.GetString(r, "BookHolderId");
                            ac.BillBookRefId.Add(DaHelper.GetString(r, "BillBookId"));
                            ac.BookCreateDt = DaHelper.GetDate(r, "CreateDate");
                            ac.Period = DaHelper.GetString(r, "BookPeriod");
                            ac.CalculateDt = DaHelper.GetDate(r, "CalDt");
                            ac.BaseAmount = DaHelper.GetDecimal(r, "BaseCMAmount");
                            ac.VatAmount = DaHelper.GetDecimal(r, "VatAmount");
                            ac.GAmount = DaHelper.GetDecimal(r, "CmAmount");
                            ac.FineAmount = DaHelper.GetDecimal(r, "FineAmount");
                            ac.TaxAmount = DaHelper.GetDecimal(r, "TaxAmount");
                            ac.SyncDt = DaHelper.GetDate(r, "SyncDt");

                        }
                        else if (cmId == DaHelper.GetString(r, "cmId"))
                        {
                            ac.BillBookRefId.Add(DaHelper.GetString(r, "BillBookId"));
                        }
                        else
                        {
                            expList.Add(ac);
                            ac = new AG_Compensation();
                            ac.CmId = DaHelper.GetString(r, "CmId");
                            ac.CaId = DaHelper.GetString(r, "BookHolderId");
                            ac.BillBookRefId.Add(DaHelper.GetString(r, "BillBookId"));
                            ac.BookCreateDt = DaHelper.GetDate(r, "CreateDate");
                            ac.Period = DaHelper.GetString(r, "BookPeriod");
                            ac.CalculateDt = DaHelper.GetDate(r, "CalDt");
                            ac.BaseAmount = DaHelper.GetDecimal(r, "BaseCMAmount");
                            ac.VatAmount = DaHelper.GetDecimal(r, "VatAmount");
                            ac.GAmount = DaHelper.GetDecimal(r, "CmAmount");
                            ac.FineAmount = DaHelper.GetDecimal(r, "FineAmount");
                            ac.TaxAmount = DaHelper.GetDecimal(r, "TaxAmount");
                        }
                        cmId = DaHelper.GetString(r, "cmId");
                    }
                    if (expList.Count == 0 && ac.CmId != null)
                    {
                        expList.Add(ac);
                    }
                    else if (expList[expList.Count - 1].CmId != cmId)
                    {
                        expList.Add(ac);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            string tmp2 = string.Format("expList prepare to return {0} rows", expList.Count.ToString());
            logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessData, tmp2, 0);

            return expList;
        }

        public List<AG_BillBook> GetBillBookForExport()
        {
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            List<AG_BillBook> expList = new List<AG_BillBook>();

            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_exp_GetBillBookExport");
            cmd.CommandTimeout = 0;
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    AG_BillBook bb = new AG_BillBook();
                    bb.BillBookId = DaHelper.GetString(r, "BillBookId");
                  
                    expList.Add(bb);
                }

            }

            return expList;
        }

        public List<AG_BillBookInvoice> GetBillBookInvoice(string billBookId, ACABatchParam param)
        {
            ACABatchLogger logger = ACABatchLogger.GetInstance();
            List<AG_BillBookInvoice> expList = new List<AG_BillBookInvoice>();

            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_exp_BillBookInvoice");
            cmd.CommandTimeout = 0;
            db.AddInParameter(cmd, "BillBookId", DbType.String, billBookId);  //TODO: Add branch id when integration complete.
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            //string tmp1 = string.Format("ta_exp_BillBookInvoice for {0} returned {1} rows", billBookId, dt.Rows.Count.ToString());
            //logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessData, tmp1, 0);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    AG_BillBookInvoice bbi = new AG_BillBookInvoice();
                    bbi.Action = DaHelper.GetString(r, "Action");
                    bbi.Crsg = DaHelper.GetString(r, "Crsg");
                    bbi.CaId = DaHelper.GetString(r, "CaId");
                    bbi.InvoiceNo = DaHelper.GetString(r, "InvoiceNo");
                    bbi.BillBookId = DaHelper.GetString(r, "BillBookId");
                    bbi.ModifiedDate = DaHelper.GetString(r, "ModifiedDate");
                    bbi.ModifiedTime = DaHelper.GetString(r, "ModifiedTime");
                    bbi.SyncDt = DaHelper.GetDate(r, "SyncDt");
                    expList.Add(bbi);
                }

            }

            //string tmp2 = string.Format("expList prepare to return {0} rows", expList.Count.ToString());
            //logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessData, tmp2, 0);

            return expList;
        }


        public List<AR_Normal> GetARNormal(string branchId)
        {
            List<AR_Normal> _expList = new List<AR_Normal>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_ARNormal");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);  //TODO: Add branch id when integration complete.
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                decimal? TotalActualAmount = 0;
                decimal? TotalAmount = 0;
                decimal? TotalOverUnder = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    AR_Normal ar = new AR_Normal();
                    ar.Action = DaHelper.GetString(dr, "Action");
                    ar.PosRSG = DaHelper.GetString(dr, "PosRSG");
                    ar.PtId = DaHelper.GetString(dr, "PtId");
                    ar.ClearingAccNo = DaHelper.GetString(dr, "ClearingAccNo");
                    ar.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                    ar.PosBA = DaHelper.GetString(dr, "PosBA");
                    ar.PosBP = DaHelper.GetString(dr, "PosBP");
                    ar.CaId = DaHelper.GetString(dr, "CaId");
                    ar.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                    ar.CaDoc = DaHelper.GetString(dr, "CaDoc");
                    ar.ReceiptType = DaHelper.GetString(dr, "ReceiptType");
                    ar.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                    ar.PosId = DaHelper.GetString(dr, "PosId");
                    ar.CashierId = DaHelper.GetString(dr, "CashierId");
                    ar.ActualAmount = DaHelper.GetDecimal(dr, "ActualAmount");
                    ar.Amount = DaHelper.GetDecimal(dr, "Amount");
                    ar.OverUnder = DaHelper.GetDecimal(dr, "OverUnder");
                    TotalActualAmount = TotalActualAmount + ((ar.ActualAmount == null) ? 0 : ar.ActualAmount);
                    TotalAmount = TotalAmount + ((ar.Amount == null) ? 0 : ar.Amount);
                    TotalOverUnder = TotalOverUnder + ((ar.OverUnder == null) ? 0 : ar.OverUnder);
                    ar.TotalActualAmount = TotalActualAmount;
                    ar.TotalAmount = TotalAmount;
                    ar.TotalOverUnder = TotalOverUnder;
                    ar.PartialPayment = DaHelper.GetString(dr, "PartialPayment");
                    ar.Fee = DaHelper.GetDecimal(dr, "Fee");
                    ar.BankKey = DaHelper.GetString(dr, "BankKey");
                    ar.ChqNo = DaHelper.GetString(dr, "ChqNo");
                    ar.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                    ar.TransfDt = DaHelper.GetDate(dr, "TranfDt");
                    ar.SpotBillInvoiceNo = DaHelper.GetString(dr, "SpotBillInvoiceNo");
                    ar.Period = DaHelper.GetString(dr, "Period");
                    ar.GroupInvoiceNo = DaHelper.GetString(dr, "GroupInvoiceNo");
                    ar.BillBookId = DaHelper.GetString(dr, "BillBookId");
                    ar.ReceiptCondition = DaHelper.GetString(dr, "ReceiptCondition");
                    ar.DueDt = DaHelper.GetDate(dr, "DueDt");
                    ar.Name = DaHelper.GetString(dr, "Name");
                    ar.Address = DaHelper.GetString(dr, "Address");
                    ar.SyncDt = DaHelper.GetDate(dr, "SyncDt");
                    ar.DisconnectDoc = DaHelper.GetString(dr, "DisconnectDoc");
                    _expList.Add(ar);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return _expList;
        }

        public List<AR_SpotBill> GetARSpotBill(string branchId)
        {
            List<AR_SpotBill> _expList = new List<AR_SpotBill>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_ARSpotBill");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);  //TODO: Add branch id when integration complete.
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                decimal? TotalActualAmount = 0;
                decimal? TotalAmount = 0;
                decimal? TotalOverUnder = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    AR_SpotBill ar = new AR_SpotBill();
                    ar.Action = DaHelper.GetString(dr, "Action");
                    ar.PosRSG = DaHelper.GetString(dr, "PosRSG");
                    ar.PtId = DaHelper.GetString(dr, "PtId");
                    ar.ClearingAccNo = DaHelper.GetString(dr, "ClearingAccNo");
                    ar.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                    ar.PosBA = DaHelper.GetString(dr, "PosBA");
                    ar.PosBP = DaHelper.GetString(dr, "PosBP");
                    ar.CaId = DaHelper.GetString(dr, "CaId");
                    ar.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                    ar.CaDoc = DaHelper.GetString(dr, "CaDoc");
                    ar.ReceiptType = DaHelper.GetString(dr, "ReceiptType");
                    ar.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                    ar.PosId = DaHelper.GetString(dr, "PosId");
                    ar.CashierId = DaHelper.GetString(dr, "CashierId");
                    ar.ActualAmount = DaHelper.GetDecimal(dr, "ActualAmount");
                    ar.Amount = DaHelper.GetDecimal(dr, "Amount");
                    ar.OverUnder = DaHelper.GetDecimal(dr, "OverUnder");
                    TotalActualAmount = TotalActualAmount + ((ar.ActualAmount == null) ? 0 : ar.ActualAmount);
                    TotalAmount = TotalAmount + ((ar.Amount == null) ? 0 : ar.Amount);
                    TotalOverUnder = TotalOverUnder + ((ar.OverUnder == null) ? 0 : ar.OverUnder);
                    ar.TotalActualAmount = TotalActualAmount;
                    ar.TotalAmount = TotalAmount;
                    ar.TotalOverUnder = TotalOverUnder;
                    ar.PartialPayment = DaHelper.GetString(dr, "PartialPayment");
                    ar.Fee = DaHelper.GetDecimal(dr, "Fee");
                    ar.BankKey = DaHelper.GetString(dr, "BankKey");
                    ar.ChqNo = DaHelper.GetString(dr, "ChqNo");
                    ar.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                    ar.TransfDt = DaHelper.GetDate(dr, "TranfDt");
                    ar.SpotBillInvoiceNo = DaHelper.GetString(dr, "SpotBillInvoiceNo");
                    ar.Period = DaHelper.GetString(dr, "Period");
                    ar.GroupInvoiceNo = DaHelper.GetString(dr, "GroupInvoiceNo");
                    ar.BillBookId = DaHelper.GetString(dr, "BillBookId");
                    ar.ReceiptCondition = DaHelper.GetString(dr, "ReceiptCondition");
                    ar.DueDt = DaHelper.GetDate(dr, "DueDt");
                    ar.Name = DaHelper.GetString(dr, "Name");
                    ar.Address = DaHelper.GetString(dr, "Address");
                    ar.SyncDt = DaHelper.GetDate(dr, "SyncDt");

                    ar.NotificationNo = DaHelper.GetString(dr, "NotificationNo");   //OneTouch

                    _expList.Add(ar);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return _expList;
        }

        public List<AR_GrpInv> GetARGroupInvoice(string branchId)
        {
            List<AR_GrpInv> _expList = new List<AR_GrpInv>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_ARGroupInvoice");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);  //TODO: Add branch id when integration complete.
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                decimal? TotalActualAmount = 0;
                decimal? TotalAmount = 0;
                decimal? TotalOverUnder = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    AR_GrpInv ar = new AR_GrpInv();
                    ar.Action = DaHelper.GetString(dr, "Action");
                    ar.PosRSG = DaHelper.GetString(dr, "PosRSG");
                    ar.PtId = DaHelper.GetString(dr, "PtId");
                    ar.ClearingAccNo = DaHelper.GetString(dr, "ClearingAccNo");
                    ar.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                    ar.PosBA = DaHelper.GetString(dr, "PosBA");
                    ar.PosBP = DaHelper.GetString(dr, "PosBP");
                    ar.CaId = DaHelper.GetString(dr, "CaId");
                    ar.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                    ar.CaDoc = DaHelper.GetString(dr, "CaDoc");
                    ar.ReceiptType = DaHelper.GetString(dr, "ReceiptType");
                    ar.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                    ar.PosId = DaHelper.GetString(dr, "PosId");
                    ar.CashierId = DaHelper.GetString(dr, "CashierId");
                    ar.ActualAmount = DaHelper.GetDecimal(dr, "ActualAmount");
                    ar.Amount = DaHelper.GetDecimal(dr, "Amount");
                    ar.OverUnder = DaHelper.GetDecimal(dr, "OverUnder");
                    TotalActualAmount = TotalActualAmount + ((ar.ActualAmount == null) ? 0 : ar.ActualAmount);
                    TotalAmount = TotalAmount + ((ar.Amount == null) ? 0 : ar.Amount);
                    TotalOverUnder = TotalOverUnder + ((ar.OverUnder == null) ? 0 : ar.OverUnder);
                    ar.TotalActualAmount = TotalActualAmount;
                    ar.TotalAmount = TotalAmount;
                    ar.TotalOverUnder = TotalOverUnder;
                    ar.PartialPayment = DaHelper.GetString(dr, "PartialPayment");
                    ar.Fee = DaHelper.GetString(dr, "Fee");
                    ar.BankKey = DaHelper.GetString(dr, "BankKey");
                    ar.ChqNo = DaHelper.GetString(dr, "ChqNo");
                    ar.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                    ar.TransfDt = DaHelper.GetDate(dr, "TranfDt");
                    ar.SpotBillInvoiceNo = DaHelper.GetString(dr, "SpotBillInvoiceNo");
                    ar.Period = DaHelper.GetString(dr, "Period");
                    ar.GroupInvoiceNo = DaHelper.GetString(dr, "GroupInvoiceNo");
                    ar.BillBookId = DaHelper.GetString(dr, "BillBookId");
                    ar.ReceiptCondition = DaHelper.GetString(dr, "ReceiptCondition");
                    ar.DueDt = DaHelper.GetDate(dr, "DueDt");
                    ar.Name = DaHelper.GetString(dr, "Name");
                    ar.Address = DaHelper.GetString(dr, "Address");
                    ar.SyncDt = DaHelper.GetDate(dr, "SyncDt");
                    //// CBS2 Defect Log #103 
                    //// "แก้ไข ให้ ทำการ export DL007_EXPORT_TO_SAP_JOB
                    //// โดย โฟกัส ที่ TRP0020C
                    //// จะทำการแยกไฟล์ การ Export โดยเงื่อนไข คือ 
                    //// 1) CaId 092 หรือ 091
                    //// 2) DisconnectionDoc = ""CS"""
                    //// 2021-Nov-11 Uthen.P
                    ar.DisconnectDoc = DaHelper.GetString(dr, "DisconnectDoc");
                    _expList.Add(ar);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return _expList;
        }

        public List<AR_Agency> GetARPaidByAgency(string branchId)
        {
            List<AR_Agency> _expList = new List<AR_Agency>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_ARPaidByAgency");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);  //TODO: Add branch id when integration complete.
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                decimal? TotalActualAmount = 0;
                decimal? TotalAmount = 0;
                decimal? TotalOverUnder = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    AR_Agency ar = new AR_Agency();
                    ar.Action = DaHelper.GetString(dr, "Action");
                    ar.PosRSG = DaHelper.GetString(dr, "PosRSG");
                    ar.PtId = DaHelper.GetString(dr, "PtId");
                    ar.ClearingAccNo = DaHelper.GetString(dr, "ClearingAccNo");
                    ar.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                    ar.PosBA = DaHelper.GetString(dr, "PosBA");
                    ar.PosBP = DaHelper.GetString(dr, "PosBP");
                    ar.CaId = DaHelper.GetString(dr, "CaId");
                    ar.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                    ar.CaDoc = DaHelper.GetString(dr, "CaDoc");
                    ar.ReceiptType = DaHelper.GetString(dr, "ReceiptType");
                    ar.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                    ar.PosId = DaHelper.GetString(dr, "PosId");
                    ar.CashierId = DaHelper.GetString(dr, "CashierId");
                    ar.ActualAmount = DaHelper.GetDecimal(dr, "ActualAmount");
                    ar.Amount = DaHelper.GetDecimal(dr, "Amount");
                    ar.OverUnder = DaHelper.GetDecimal(dr, "OverUnder");
                    TotalActualAmount = TotalActualAmount + ((ar.ActualAmount == null) ? 0 : ar.ActualAmount);
                    TotalAmount = TotalAmount + ((ar.Amount == null) ? 0 : ar.Amount);
                    TotalOverUnder = TotalOverUnder + ((ar.OverUnder == null) ? 0 : ar.OverUnder);
                    ar.TotalActualAmount = TotalActualAmount;
                    ar.TotalAmount = TotalAmount;
                    ar.TotalOverUnder = TotalOverUnder;
                    ar.PartialPayment = DaHelper.GetString(dr, "PartialPayment");
                    ar.Fee = DaHelper.GetDecimal(dr, "Fee");
                    ar.BankKey = DaHelper.GetString(dr, "BankKey");
                    ar.ChqNo = DaHelper.GetString(dr, "ChqNo");
                    ar.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                    ar.TransfDt = DaHelper.GetDate(dr, "TranfDt");
                    ar.SpotBillInvoiceNo = DaHelper.GetString(dr, "SpotBillInvoiceNo");
                    ar.Period = DaHelper.GetString(dr, "Period");
                    ar.GroupInvoiceNo = DaHelper.GetString(dr, "GroupInvoiceNo");
                    ar.BillBookId = DaHelper.GetString(dr, "BillBookId");
                    ar.ReceiptCondition = DaHelper.GetString(dr, "ReceiptCondition");
                    ar.DueDt = DaHelper.GetDate(dr, "DueDt");
                    ar.Name = DaHelper.GetString(dr, "Name");
                    ar.Address = DaHelper.GetString(dr, "Address");
                    ar.SyncDt = DaHelper.GetDate(dr, "SyncDt");
                    _expList.Add(ar);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return _expList;
        }

        public List<AR_PartialPay> GetARPartialPayment(string branchId)
        {
            List<AR_PartialPay> _expList = new List<AR_PartialPay>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_ARPartialPayment");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);  //TODO: Add branch id when integration complete.
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                decimal? TotalActualAmount = 0;
                decimal? TotalAmount = 0;
                decimal? TotalOverUnder = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    AR_PartialPay ar = new AR_PartialPay();
                    ar.Action = DaHelper.GetString(dr, "Action");
                    ar.PosRSG = DaHelper.GetString(dr, "PosRSG");
                    ar.PtId = DaHelper.GetString(dr, "PtId");
                    ar.ClearingAccNo = DaHelper.GetString(dr, "ClearingAccNo");
                    ar.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                    ar.PosBA = DaHelper.GetString(dr, "PosBA");
                    ar.PosBP = DaHelper.GetString(dr, "PosBP");
                    ar.CaId = DaHelper.GetString(dr, "CaId");
                    ar.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                    ar.CaDoc = DaHelper.GetString(dr, "CaDoc");
                    ar.ReceiptType = DaHelper.GetString(dr, "ReceiptType");
                    ar.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                    ar.PosId = DaHelper.GetString(dr, "PosId");
                    ar.CashierId = DaHelper.GetString(dr, "CashierId");
                    ar.ActualAmount = DaHelper.GetDecimal(dr, "ActualAmount");
                    ar.Amount = DaHelper.GetDecimal(dr, "Amount");
                    ar.OverUnder = DaHelper.GetDecimal(dr, "OverUnder");
                    TotalActualAmount = TotalActualAmount + ((ar.ActualAmount == null) ? 0 : ar.ActualAmount);
                    TotalAmount = TotalAmount + ((ar.Amount == null) ? 0 : ar.Amount);
                    TotalOverUnder = TotalOverUnder + ((ar.OverUnder == null) ? 0 : ar.OverUnder);
                    ar.TotalActualAmount = TotalActualAmount;
                    ar.TotalAmount = TotalAmount;
                    ar.TotalOverUnder = TotalOverUnder;
                    ar.PartialPayment = DaHelper.GetString(dr, "PartialPayment");
                    ar.Fee = DaHelper.GetDecimal(dr, "Fee");
                    ar.BankKey = DaHelper.GetString(dr, "BankKey");
                    ar.ChqNo = DaHelper.GetString(dr, "ChqNo");
                    ar.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                    ar.TransfDt = DaHelper.GetDate(dr, "TranfDt");
                    ar.SpotBillInvoiceNo = DaHelper.GetString(dr, "SpotBillInvoiceNo");
                    ar.Period = DaHelper.GetString(dr, "Period");
                    ar.GroupInvoiceNo = DaHelper.GetString(dr, "GroupInvoiceNo");
                    ar.BillBookId = DaHelper.GetString(dr, "BillBookId");
                    ar.ReceiptCondition = DaHelper.GetString(dr, "ReceiptCondition");
                    ar.DueDt = DaHelper.GetDate(dr, "DueDt");
                    ar.Name = DaHelper.GetString(dr, "Name");
                    ar.Address = DaHelper.GetString(dr, "Address");
                    ar.SyncDt = DaHelper.GetDate(dr, "SyncDt");
                    _expList.Add(ar);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return _expList;
        }

        public List<ARn_Normal> GetARNonInvNormal(string branchId)
        {
            List<ARn_Normal> _expList = new List<ARn_Normal>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_ARNonInvoiceNormal");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);  //TODO: Add branch id when integration complete.
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                decimal? TotalActualAmount = 0;
                decimal? TotalAmount = 0;
                decimal? TotalOverUnder = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    ARn_Normal ar = new ARn_Normal();
                    ar.Action = DaHelper.GetString(dr, "Action");
                    ar.PosRSG = DaHelper.GetString(dr, "PosRSG");
                    ar.PtId = DaHelper.GetString(dr, "PtId");
                    ar.ClearingAccNo = DaHelper.GetString(dr, "ClearingAccNo");
                    ar.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                    ar.PosBA = DaHelper.GetString(dr, "PosBA");
                    ar.PosBP = DaHelper.GetString(dr, "PosBP");
                    ar.CaId = DaHelper.GetString(dr, "CaId");
                    ar.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                    ar.CaDoc = DaHelper.GetString(dr, "CaDoc");
                    ar.ReceiptType = DaHelper.GetString(dr, "ReceiptType");
                    ar.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                    ar.PosId = DaHelper.GetString(dr, "PosId");
                    ar.CashierId = DaHelper.GetString(dr, "CashierId");
                    ar.ActualAmount = DaHelper.GetDecimal(dr, "ActualAmount");
                    ar.Amount = DaHelper.GetDecimal(dr, "Amount");
                    ar.OverUnder = DaHelper.GetDecimal(dr, "OverUnder");
                    TotalActualAmount = TotalActualAmount + ((ar.ActualAmount == null) ? 0 : ar.ActualAmount);
                    TotalAmount = TotalAmount + ((ar.Amount == null) ? 0 : ar.Amount);
                    TotalOverUnder = TotalOverUnder + ((ar.OverUnder == null) ? 0 : ar.OverUnder);
                    ar.TotalActualAmount = TotalActualAmount;
                    ar.TotalAmount = TotalAmount;
                    ar.TotalOverUnder = TotalOverUnder;
                    ar.PartialPayment = DaHelper.GetString(dr, "PartialPayment");
                    ar.Fee = DaHelper.GetDecimal(dr, "Fee");
                    ar.BankKey = DaHelper.GetString(dr, "BankKey");
                    ar.ChqNo = DaHelper.GetString(dr, "ChqNo");
                    ar.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                    ar.TransfDt = DaHelper.GetDate(dr, "TranfDt");
                    ar.SpotBillInvoiceNo = DaHelper.GetString(dr, "SpotBillInvoiceNo");
                    ar.Period = DaHelper.GetString(dr, "Period");
                    ar.GroupInvoiceNo = DaHelper.GetString(dr, "GroupInvoiceNo");
                    ar.BillBookId = DaHelper.GetString(dr, "BillBookId");
                    ar.ReceiptCondition = DaHelper.GetString(dr, "ReceiptCondition");
                    ar.DueDt = DaHelper.GetDate(dr, "DueDt");
                    ar.MainSub = DaHelper.GetString(dr, "MainSub");
                    ar.Name = DaHelper.GetString(dr, "Name");
                    ar.Address = DaHelper.GetString(dr, "Address");
                    ar.Description = DaHelper.GetString(dr, "Description");
                    ar.Qty = DaHelper.GetInt(dr, "Qty");
                    ar.PriceUnit = DaHelper.GetDecimal(dr, "PriceUnit");
                    ar.BaseAmount = DaHelper.GetDecimal(dr, "BaseAmount");
                    ar.VatCode = DaHelper.GetString(dr, "VatCode");
                    ar.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                    ar.DisconnectDoc = DaHelper.GetString(dr, "DisconnectDoc");
                    ar.OriginalInvoiceNo = DaHelper.GetString(dr, "OriginalInvoiceNo");
                    ar.SyncDt = DaHelper.GetDate(dr, "SyncDt");
                    _expList.Add(ar);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return _expList;
        }

        public List<ARn_CashSale> GetARNonInvCashSale(string branchId)
        {
            List<ARn_CashSale> _expList = new List<ARn_CashSale>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_ARNonInvoiceCashSale");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);  //TODO: Add branch id when integration complete.
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                decimal? TotalActualAmount = 0;
                decimal? TotalAmount = 0;
                decimal? TotalOverUnder = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    ARn_CashSale ar = new ARn_CashSale();
                    ar.Action = DaHelper.GetString(dr, "Action");
                    ar.PosRSG = DaHelper.GetString(dr, "PosRSG");
                    ar.PtId = DaHelper.GetString(dr, "PtId");
                    ar.ClearingAccNo = DaHelper.GetString(dr, "ClearingAccNo");
                    ar.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                    ar.PosBA = DaHelper.GetString(dr, "PosBA");
                    ar.PosBP = DaHelper.GetString(dr, "PosBP");
                    ar.CaId = DaHelper.GetString(dr, "CaId");
                    ar.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                    ar.CaDoc = DaHelper.GetString(dr, "CaDoc");
                    ar.ReceiptType = DaHelper.GetString(dr, "ReceiptType");
                    ar.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                    ar.PosId = DaHelper.GetString(dr, "PosId");
                    ar.CashierId = DaHelper.GetString(dr, "CashierId");
                    ar.ActualAmount = DaHelper.GetDecimal(dr, "ActualAmount");
                    ar.Amount = DaHelper.GetDecimal(dr, "Amount");
                    ar.OverUnder = DaHelper.GetDecimal(dr, "OverUnder");
                    TotalActualAmount = TotalActualAmount + ((ar.ActualAmount == null) ? 0 : ar.ActualAmount);
                    TotalAmount = TotalAmount + ((ar.Amount == null) ? 0 : ar.Amount);
                    TotalOverUnder = TotalOverUnder + ((ar.OverUnder == null) ? 0 : ar.OverUnder);
                    ar.TotalActualAmount = TotalActualAmount;
                    ar.TotalAmount = TotalAmount;
                    ar.TotalOverUnder = TotalOverUnder;
                    ar.PartialPayment = DaHelper.GetString(dr, "PartialPayment");
                    ar.Fee = DaHelper.GetDecimal(dr, "Fee");
                    ar.BankKey = DaHelper.GetString(dr, "BankKey");
                    ar.ChqNo = DaHelper.GetString(dr, "ChqNo");
                    ar.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                    ar.TransfDt = DaHelper.GetDate(dr, "TranfDt");
                    ar.SpotBillInvoiceNo = DaHelper.GetString(dr, "SpotBillInvoiceNo");
                    ar.Period = DaHelper.GetString(dr, "Period");
                    ar.GroupInvoiceNo = DaHelper.GetString(dr, "GroupInvoiceNo");
                    ar.BillBookId = DaHelper.GetString(dr, "BillBookId");
                    ar.ReceiptCondition = DaHelper.GetString(dr, "ReceiptCondition");
                    ar.DueDt = DaHelper.GetDate(dr, "DueDt");
                    ar.MainSub = DaHelper.GetString(dr, "MainSub");
                    ar.Name = DaHelper.GetString(dr, "Name");
                    ar.Address = DaHelper.GetString(dr, "Address");
                    ar.Description = DaHelper.GetString(dr, "Description");
                    ar.Qty = DaHelper.GetDecimal(dr, "Qty");
                    ar.PriceUnit = DaHelper.GetDecimal(dr, "PriceUnit");
                    ar.BaseAmount = DaHelper.GetDecimal(dr, "BaseAmount");
                    ar.VatCode = DaHelper.GetString(dr, "VatCode");
                    ar.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                    ar.DisconnectDoc = DaHelper.GetString(dr, "DisconnectDoc");
                    ar.OriginalInvoiceNo = DaHelper.GetString(dr, "OriginalInvoiceNo");
                    ar.SyncDt = DaHelper.GetDate(dr, "SyncDt");
                    _expList.Add(ar);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return _expList;
        }

        public List<ARn_AdvPayment> GetARNonInvAdvancePayment(string branchId)
        {
            List<ARn_AdvPayment> _expList = new List<ARn_AdvPayment>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_ARNonInvoiceAdvancePayment");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);  //TODO: Add branch id when integration complete.
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                decimal? TotalActualAmount = 0;
                decimal? TotalAmount = 0;
                decimal? TotalOverUnder = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    ARn_AdvPayment ar = new ARn_AdvPayment();
                    ar.Action = DaHelper.GetString(dr, "Action");
                    ar.PosRSG = DaHelper.GetString(dr, "PosRSG");
                    ar.PtId = DaHelper.GetString(dr, "PtId");
                    ar.ClearingAccNo = DaHelper.GetString(dr, "ClearingAccNo");
                    ar.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                    ar.PosBA = DaHelper.GetString(dr, "PosBA");
                    ar.PosBP = DaHelper.GetString(dr, "PosBP");
                    ar.CaId = DaHelper.GetString(dr, "CaId");
                    ar.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                    ar.CaDoc = DaHelper.GetString(dr, "CaDoc");
                    ar.ReceiptType = DaHelper.GetString(dr, "ReceiptType");
                    ar.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                    ar.PosId = DaHelper.GetString(dr, "PosId");
                    ar.CashierId = DaHelper.GetString(dr, "CashierId");
                    ar.ActualAmount = DaHelper.GetDecimal(dr, "ActualAmount");
                    ar.Amount = DaHelper.GetDecimal(dr, "Amount");
                    ar.OverUnder = DaHelper.GetDecimal(dr, "OverUnder");
                    TotalActualAmount = TotalActualAmount + ((ar.ActualAmount == null) ? 0 : ar.ActualAmount);
                    TotalAmount = TotalAmount + ((ar.Amount == null) ? 0 : ar.Amount);
                    TotalOverUnder = TotalOverUnder + ((ar.OverUnder == null) ? 0 : ar.OverUnder);
                    ar.TotalActualAmount = TotalActualAmount;
                    ar.TotalAmount = TotalAmount;
                    ar.TotalOverUnder = TotalOverUnder;
                    ar.PartialPayment = DaHelper.GetString(dr, "PartialPayment");
                    ar.Fee = DaHelper.GetDecimal(dr, "Fee");
                    ar.BankKey = DaHelper.GetString(dr, "BankKey");
                    ar.ChqNo = DaHelper.GetString(dr, "ChqNo");
                    ar.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                    ar.TransfDt = DaHelper.GetDate(dr, "TranfDt");
                    ar.SpotBillInvoiceNo = DaHelper.GetString(dr, "SpotBillInvoiceNo");
                    ar.Period = DaHelper.GetString(dr, "Period");
                    ar.GroupInvoiceNo = DaHelper.GetString(dr, "GroupInvoiceNo");
                    ar.BillBookId = DaHelper.GetString(dr, "BillBookId");
                    ar.ReceiptCondition = DaHelper.GetString(dr, "ReceiptCondition");
                    ar.DueDt = DaHelper.GetDate(dr, "DueDt");
                    ar.MainSub = DaHelper.GetString(dr, "MainSub");
                    ar.Name = DaHelper.GetString(dr, "Name");
                    ar.Address = DaHelper.GetString(dr, "Address");
                    ar.Description = DaHelper.GetString(dr, "Description");
                    ar.Qty = DaHelper.GetInt(dr, "Qty");
                    ar.PriceUnit = DaHelper.GetDecimal(dr, "PriceUnit");
                    ar.BaseAmount = DaHelper.GetDecimal(dr, "BaseAmount");
                    ar.VatCode = DaHelper.GetString(dr, "VatCode");
                    ar.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                    ar.DisconnectDoc = DaHelper.GetString(dr, "DisconnectDoc");
                    ar.OriginalInvoiceNo = DaHelper.GetString(dr, "OriginalInvoiceNo");
                    ar.SyncDt = DaHelper.GetDate(dr, "SyncDt");
                    _expList.Add(ar);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return _expList;
        }

        public List<ARn_APPayment> GetAPPayment(string branchId)
        {
            List<ARn_APPayment> _expList = new List<ARn_APPayment>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_APPayment");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);  //TODO: Add branch id when integration complete.
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                decimal? TotalActualAmount = 0;
                decimal? TotalAmount = 0;
                decimal? TotalOverUnder = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    ARn_APPayment ar = new ARn_APPayment();
                    ar.Action = DaHelper.GetString(dr, "Action");
                    ar.PosRSG = DaHelper.GetString(dr, "PosRSG");
                    ar.PtId = DaHelper.GetString(dr, "PtId");
                    ar.ClearingAccNo = DaHelper.GetString(dr, "ClearingAccNo");
                    ar.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                    ar.PosBA = DaHelper.GetString(dr, "PosBA");
                    ar.PosBP = DaHelper.GetString(dr, "PosBP");
                    ar.CaId = DaHelper.GetString(dr, "CaId");
                    ar.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                    ar.CaDoc = DaHelper.GetString(dr, "CaDoc");
                    ar.ReceiptType = DaHelper.GetString(dr, "ReceiptType");
                    ar.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                    ar.PosId = DaHelper.GetString(dr, "PosId");
                    ar.CashierId = DaHelper.GetString(dr, "CashierId");
                    ar.ActualAmount = DaHelper.GetDecimal(dr, "ActualAmount");
                    ar.Amount = DaHelper.GetDecimal(dr, "Amount");
                    ar.OverUnder = DaHelper.GetDecimal(dr, "OverUnder");
                    TotalActualAmount = TotalActualAmount + ((ar.ActualAmount == null) ? 0 : ar.ActualAmount);
                    TotalAmount = TotalAmount + ((ar.Amount == null) ? 0 : ar.Amount);
                    TotalOverUnder = TotalOverUnder + ((ar.OverUnder == null) ? 0 : ar.OverUnder);
                    ar.TotalActualAmount = TotalActualAmount;
                    ar.TotalAmount = TotalAmount;
                    ar.TotalOverUnder = TotalOverUnder;
                    ar.PartialPayment = DaHelper.GetString(dr, "PartialPayment");
                    ar.Fee = DaHelper.GetDecimal(dr, "Fee");
                    ar.BankKey = DaHelper.GetString(dr, "BankKey");
                    ar.ChqNo = DaHelper.GetString(dr, "ChqNo");
                    ar.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                    ar.TransfDt = DaHelper.GetDate(dr, "TranfDt");
                    ar.SpotBillInvoiceNo = DaHelper.GetString(dr, "SpotBillInvoiceNo");
                    ar.Period = DaHelper.GetString(dr, "Period");
                    ar.GroupInvoiceNo = DaHelper.GetString(dr, "GroupInvoiceNo");
                    ar.BillBookId = DaHelper.GetString(dr, "BillBookId");
                    ar.ReceiptCondition = DaHelper.GetString(dr, "ReceiptCondition");
                    ar.DueDt = DaHelper.GetDate(dr, "DueDt");
                    ar.MainSub = DaHelper.GetString(dr, "MainSub");
                    ar.Name = DaHelper.GetString(dr, "Name");
                    ar.Address = DaHelper.GetString(dr, "Address");
                    ar.Description = DaHelper.GetString(dr, "Description");
                    ar.Qty = DaHelper.GetInt(dr, "Qty");
                    ar.PriceUnit = DaHelper.GetDecimal(dr, "PriceUnit");
                    ar.BaseAmount = DaHelper.GetDecimal(dr, "BaseAmount");
                    ar.VatCode = DaHelper.GetString(dr, "VatCode");
                    ar.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                    ar.DisconnectDoc = DaHelper.GetString(dr, "DisconnectDoc");
                    ar.OriginalInvoiceNo = DaHelper.GetString(dr, "OriginalInvoiceNo");
                    ar.SyncDt = DaHelper.GetDate(dr, "SyncDt");
                    _expList.Add(ar);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return _expList;
        }

        public List<AR_DepositReceipt> GetARDepositReceipt(string branchId)
        {
            List<AR_DepositReceipt> _expList = new List<AR_DepositReceipt>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_ARDepositReceipt");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);  //TODO: Add branch id when integration complete.
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                decimal? TotalActualAmount = 0;
                decimal? TotalAmount = 0;
                decimal? TotalOverUnder = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    AR_DepositReceipt ar = new AR_DepositReceipt();
                    ar.Action = DaHelper.GetString(dr, "Action");
                    ar.PosRSG = DaHelper.GetString(dr, "PosRSG");
                    ar.PtId = DaHelper.GetString(dr, "PtId");
                    ar.ClearingAccNo = DaHelper.GetString(dr, "ClearingAccNo");
                    ar.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                    ar.PosBA = DaHelper.GetString(dr, "PosBA");
                    ar.PosBP = DaHelper.GetString(dr, "PosBP");
                    ar.CaId = DaHelper.GetString(dr, "CaId");
                    ar.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                    ar.CaDoc = DaHelper.GetString(dr, "CaDoc");
                    ar.ReceiptType = DaHelper.GetString(dr, "ReceiptType");
                    ar.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                    ar.PosId = DaHelper.GetString(dr, "PosId");
                    ar.CashierId = DaHelper.GetString(dr, "CashierId");
                    ar.ActualAmount = DaHelper.GetDecimal(dr, "ActualAmount");
                    ar.Amount = DaHelper.GetDecimal(dr, "Amount");
                    ar.OverUnder = DaHelper.GetDecimal(dr, "OverUnder");
                    TotalActualAmount = TotalActualAmount + ((ar.ActualAmount == null) ? 0 : ar.ActualAmount);
                    TotalAmount = TotalAmount + ((ar.Amount == null) ? 0 : ar.Amount);
                    TotalOverUnder = TotalOverUnder + ((ar.OverUnder == null) ? 0 : ar.OverUnder);
                    ar.TotalActualAmount = TotalActualAmount;
                    ar.TotalAmount = TotalAmount;
                    ar.TotalOverUnder = TotalOverUnder;
                    ar.PartialPayment = DaHelper.GetString(dr, "PartialPayment");
                    ar.Fee = DaHelper.GetDecimal(dr, "Fee");
                    ar.BankKey = DaHelper.GetString(dr, "BankKey");
                    ar.ChqNo = DaHelper.GetString(dr, "ChqNo");
                    ar.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                    ar.TransfDt = DaHelper.GetDate(dr, "TranfDt");
                    ar.SpotBillInvoiceNo = DaHelper.GetString(dr, "SpotBillInvoiceNo");
                    ar.Period = DaHelper.GetString(dr, "Period");
                    ar.GroupInvoiceNo = DaHelper.GetString(dr, "GroupInvoiceNo");
                    ar.BillBookId = DaHelper.GetString(dr, "BillBookId");
                    ar.ReceiptCondition = DaHelper.GetString(dr, "ReceiptCondition");
                    ar.DueDt = DaHelper.GetDate(dr, "DueDt");
                    ar.MainSub = DaHelper.GetString(dr, "MainSub");
                    ar.Name = DaHelper.GetString(dr, "Name");
                    ar.Address = DaHelper.GetString(dr, "Address");
                    ar.Description = DaHelper.GetString(dr, "Description");
                    ar.Qty = DaHelper.GetDecimal(dr, "Qty");
                    ar.PriceUnit = DaHelper.GetDecimal(dr, "PriceUnit");
                    ar.BaseAmount = DaHelper.GetDecimal(dr, "BaseAmount");
                    ar.VatCode = DaHelper.GetString(dr, "VatCode");
                    ar.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                    ar.DisconnectDoc = DaHelper.GetString(dr, "DisconnectDoc");
                    ar.OriginalInvoiceNo = DaHelper.GetString(dr, "OriginalInvoiceNo");
                    ar.SyncDt = DaHelper.GetDate(dr, "SyncDt");
                    _expList.Add(ar);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return _expList;
        }

        public List<AP_Payment> GetAP_PaymentVoucher(string branchId)
        {
            List<AP_Payment> _expList = new List<AP_Payment>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_APVoucher");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);  //TODO: Add branch id when integration complete.
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                decimal? TotalActualAmount = 0;
                decimal? TotalAmount = 0;
                decimal? TotalOverUnder = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    AP_Payment ap = new AP_Payment();
                    ap.Action = DaHelper.GetString(dr, "Action");
                    ap.PosRSG = DaHelper.GetString(dr, "PosRSG");
                    ap.PtId = DaHelper.GetString(dr, "PtId");
                    ap.ClearingAccNo = DaHelper.GetString(dr, "ClearingAccNo");
                    ap.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                    ap.PosBA = DaHelper.GetString(dr, "PosBA");
                    ap.PosBP = DaHelper.GetString(dr, "PosBP");
                    ap.CaId = DaHelper.GetString(dr, "CaId");
                    ap.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                    ap.PosId = DaHelper.GetString(dr, "PosId");
                    ap.CashierId = DaHelper.GetString(dr, "CashierId");
                    ap.ActualAmount = DaHelper.GetDecimal(dr, "ActualAmount");
                    ap.Amount = DaHelper.GetDecimal(dr, "Amount");
                    ap.OverUnder = DaHelper.GetDecimal(dr, "OverUnder");
                    TotalActualAmount = TotalActualAmount + ((ap.ActualAmount == null) ? 0 : ap.ActualAmount);
                    TotalAmount = TotalAmount + ((ap.Amount == null) ? 0 : ap.Amount);
                    TotalOverUnder = TotalOverUnder + ((ap.OverUnder == null) ? 0 : ap.OverUnder);
                    ap.TotalActualAmount = TotalActualAmount;
                    ap.TotalAmount = TotalAmount;
                    ap.TotalOverUnder = TotalOverUnder;
                    ap.BankKey = DaHelper.GetString(dr, "BankKey");
                    ap.ChqNo = DaHelper.GetString(dr, "ChqNo");
                    ap.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                    ap.TransfDt = DaHelper.GetDate(dr, "TranfDt");
                    ap.MainSub = DaHelper.GetString(dr, "MainSub");
                    ap.SyncDt = DaHelper.GetDate(dr, "SyncDt");
                    _expList.Add(ap);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return _expList;
        }

        public List<AP_BankPayIn> GetAP_BankPayIn(string branchId)
        {
            List<AP_BankPayIn> _expList = new List<AP_BankPayIn>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_APBankPayIn");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);  //TODO: Add branch id when integration complete.
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                decimal? TotalActualAmount = 0;
                decimal? TotalAmount = 0;
                decimal? TotalOverUnder = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    AP_BankPayIn ap = new AP_BankPayIn();
                    ap.Action = DaHelper.GetString(dr, "Action");
                    ap.PosRSG = DaHelper.GetString(dr, "PosRSG");
                    ap.PtId = DaHelper.GetString(dr, "PtId");
                    ap.ClearingAccNo = DaHelper.GetString(dr, "ClearingAccNo");
                    ap.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                    ap.PosBA = DaHelper.GetString(dr, "PosBA");
                    ap.PosBP = DaHelper.GetString(dr, "PosBP");
                    ap.CaId = DaHelper.GetString(dr, "CaId");
                    ap.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                    ap.PosId = DaHelper.GetString(dr, "PosId");
                    ap.CashierId = DaHelper.GetString(dr, "CashierId");
                    ap.ActualAmount = DaHelper.GetDecimal(dr, "ActualAmount");
                    ap.Amount = DaHelper.GetDecimal(dr, "Amount");
                    ap.OverUnder = DaHelper.GetDecimal(dr, "OverUnder");
                    TotalActualAmount = TotalActualAmount + ((ap.ActualAmount == null) ? 0 : ap.ActualAmount);
                    TotalAmount = TotalAmount + ((ap.Amount == null) ? 0 : ap.Amount);
                    TotalOverUnder = TotalOverUnder + ((ap.OverUnder == null) ? 0 : ap.OverUnder);
                    ap.TotalActualAmount = TotalActualAmount;
                    ap.TotalAmount = TotalAmount;
                    ap.TotalOverUnder = TotalOverUnder;
                    ap.BankKey = DaHelper.GetString(dr, "BankKey");
                    ap.ChqNo = DaHelper.GetString(dr, "ChqNo");
                    ap.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                    ap.GLAccountNo = DaHelper.GetString(dr, "GLAccountNo");
                    ap.MainSub = DaHelper.GetString(dr, "MainSub");
                    ap.SyncDt = DaHelper.GetDate(dr, "SyncDt");
                    _expList.Add(ap);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return _expList;
        }

        public List<AR_Normal> GetMPayARNormal(string branchId)
        {
            List<AR_Normal> _expList = new List<AR_Normal>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_MPayARNormal");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);  //TODO: Add branch id when integration complete.
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                decimal? TotalActualAmount = 0;
                decimal? TotalAmount = 0;
                decimal? TotalOverUnder = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    AR_Normal ar = new AR_Normal();
                    ar.Action = DaHelper.GetString(dr, "Action");
                    ar.PosRSG = DaHelper.GetString(dr, "PosRSG");
                    ar.PtId = DaHelper.GetString(dr, "PtId");
                    ar.ClearingAccNo = DaHelper.GetString(dr, "ClearingAccNo");
                    ar.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                    ar.PosBA = DaHelper.GetString(dr, "PosBA");
                    ar.PosBP = DaHelper.GetString(dr, "PosBP");
                    ar.CaId = DaHelper.GetString(dr, "CaId");
                    ar.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                    ar.CaDoc = DaHelper.GetString(dr, "CaDoc");
                    ar.ReceiptType = DaHelper.GetString(dr, "ReceiptType");
                    ar.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                    ar.PosId = DaHelper.GetString(dr, "PosId");
                    ar.CashierId = DaHelper.GetString(dr, "CashierId");
                    ar.ActualAmount = DaHelper.GetDecimal(dr, "ActualAmount");
                    ar.Amount = DaHelper.GetDecimal(dr, "Amount");
                    ar.OverUnder = DaHelper.GetDecimal(dr, "OverUnder");
                    TotalActualAmount = TotalActualAmount + ((ar.ActualAmount == null) ? 0 : ar.ActualAmount);
                    TotalAmount = TotalAmount + ((ar.Amount == null) ? 0 : ar.Amount);
                    TotalOverUnder = TotalOverUnder + ((ar.OverUnder == null) ? 0 : ar.OverUnder);
                    ar.TotalActualAmount = TotalActualAmount;
                    ar.TotalAmount = TotalAmount;
                    ar.TotalOverUnder = TotalOverUnder;
                    ar.PartialPayment = DaHelper.GetString(dr, "PartialPayment");
                    ar.Fee = DaHelper.GetDecimal(dr, "Fee");
                    ar.BankKey = DaHelper.GetString(dr, "BankKey");
                    ar.ChqNo = DaHelper.GetString(dr, "ChqNo");
                    ar.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                    ar.TransfDt = DaHelper.GetDate(dr, "TranfDt");
                    ar.SpotBillInvoiceNo = DaHelper.GetString(dr, "SpotBillInvoiceNo");
                    ar.Period = DaHelper.GetString(dr, "Period");
                    ar.GroupInvoiceNo = DaHelper.GetString(dr, "GroupInvoiceNo");
                    ar.BillBookId = DaHelper.GetString(dr, "BillBookId");
                    ar.ReceiptCondition = DaHelper.GetString(dr, "ReceiptCondition");
                    ar.DueDt = DaHelper.GetDate(dr, "DueDt");
                    ar.Name = DaHelper.GetString(dr, "Name");
                    ar.Address = DaHelper.GetString(dr, "Address");
                    ar.SyncDt = DaHelper.GetDate(dr, "SyncDt");
                    _expList.Add(ar);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return _expList;
        }

        public List<ARn_AdvPayment> GetMPayARNonInvAdvancePayment(string branchId)
        {
            List<ARn_AdvPayment> _expList = new List<ARn_AdvPayment>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_MPayARNonInvoiceAdvancePayment");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);  //TODO: Add branch id when integration complete.
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                decimal? TotalActualAmount = 0;
                decimal? TotalAmount = 0;
                decimal? TotalOverUnder = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    ARn_AdvPayment ar = new ARn_AdvPayment();
                    ar.Action = DaHelper.GetString(dr, "Action");
                    ar.PosRSG = DaHelper.GetString(dr, "PosRSG");
                    ar.PtId = DaHelper.GetString(dr, "PtId");
                    ar.ClearingAccNo = DaHelper.GetString(dr, "ClearingAccNo");
                    ar.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                    ar.PosBA = DaHelper.GetString(dr, "PosBA");
                    ar.PosBP = DaHelper.GetString(dr, "PosBP");
                    ar.CaId = DaHelper.GetString(dr, "CaId");
                    ar.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                    ar.CaDoc = DaHelper.GetString(dr, "CaDoc");
                    ar.ReceiptType = DaHelper.GetString(dr, "ReceiptType");
                    ar.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                    ar.PosId = DaHelper.GetString(dr, "PosId");
                    ar.CashierId = DaHelper.GetString(dr, "CashierId");
                    ar.ActualAmount = DaHelper.GetDecimal(dr, "ActualAmount");
                    ar.Amount = DaHelper.GetDecimal(dr, "Amount");
                    ar.OverUnder = DaHelper.GetDecimal(dr, "OverUnder");
                    TotalActualAmount = TotalActualAmount + ((ar.ActualAmount == null) ? 0 : ar.ActualAmount);
                    TotalAmount = TotalAmount + ((ar.Amount == null) ? 0 : ar.Amount);
                    TotalOverUnder = TotalOverUnder + ((ar.OverUnder == null) ? 0 : ar.OverUnder);
                    ar.TotalActualAmount = TotalActualAmount;
                    ar.TotalAmount = TotalAmount;
                    ar.TotalOverUnder = TotalOverUnder;
                    ar.PartialPayment = DaHelper.GetString(dr, "PartialPayment");
                    ar.Fee = DaHelper.GetDecimal(dr, "Fee");
                    ar.BankKey = DaHelper.GetString(dr, "BankKey");
                    ar.ChqNo = DaHelper.GetString(dr, "ChqNo");
                    ar.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                    ar.TransfDt = DaHelper.GetDate(dr, "TranfDt");
                    ar.SpotBillInvoiceNo = DaHelper.GetString(dr, "SpotBillInvoiceNo");
                    ar.Period = DaHelper.GetString(dr, "Period");
                    ar.GroupInvoiceNo = DaHelper.GetString(dr, "GroupInvoiceNo");
                    ar.BillBookId = DaHelper.GetString(dr, "BillBookId");
                    ar.ReceiptCondition = DaHelper.GetString(dr, "ReceiptCondition");
                    ar.DueDt = DaHelper.GetDate(dr, "DueDt");
                    ar.MainSub = DaHelper.GetString(dr, "MainSub");
                    ar.Name = DaHelper.GetString(dr, "Name");
                    ar.Address = DaHelper.GetString(dr, "Address");
                    ar.Description = DaHelper.GetString(dr, "Description");
                    ar.Qty = DaHelper.GetInt(dr, "Qty");
                    ar.PriceUnit = DaHelper.GetDecimal(dr, "PriceUnit");
                    ar.BaseAmount = DaHelper.GetDecimal(dr, "BaseAmount");
                    ar.VatCode = DaHelper.GetString(dr, "VatCode");
                    ar.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                    ar.DisconnectDoc = DaHelper.GetString(dr, "DisconnectDoc");
                    ar.OriginalInvoiceNo = DaHelper.GetString(dr, "OriginalInvoiceNo");
                    ar.SyncDt = DaHelper.GetDate(dr, "SyncDt");
                    _expList.Add(ar);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return _expList;
        }

        public List<AR_DepositReceipt> GetMPayARDepositReceipt(string branchId)
        {
            List<AR_DepositReceipt> _expList = new List<AR_DepositReceipt>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_MPayARDepositReceipt");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);  //TODO: Add branch id when integration complete.
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                decimal? TotalActualAmount = 0;
                decimal? TotalAmount = 0;
                decimal? TotalOverUnder = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    AR_DepositReceipt ar = new AR_DepositReceipt();
                    ar.Action = DaHelper.GetString(dr, "Action");
                    ar.PosRSG = DaHelper.GetString(dr, "PosRSG");
                    ar.PtId = DaHelper.GetString(dr, "PtId");
                    ar.ClearingAccNo = DaHelper.GetString(dr, "ClearingAccNo");
                    ar.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                    ar.PosBA = DaHelper.GetString(dr, "PosBA");
                    ar.PosBP = DaHelper.GetString(dr, "PosBP");
                    ar.CaId = DaHelper.GetString(dr, "CaId");
                    ar.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                    ar.CaDoc = DaHelper.GetString(dr, "CaDoc");
                    ar.ReceiptType = DaHelper.GetString(dr, "ReceiptType");
                    ar.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                    ar.PosId = DaHelper.GetString(dr, "PosId");
                    ar.CashierId = DaHelper.GetString(dr, "CashierId");
                    ar.ActualAmount = DaHelper.GetDecimal(dr, "ActualAmount");
                    ar.Amount = DaHelper.GetDecimal(dr, "Amount");
                    ar.OverUnder = DaHelper.GetDecimal(dr, "OverUnder");
                    TotalActualAmount = TotalActualAmount + ((ar.ActualAmount == null) ? 0 : ar.ActualAmount);
                    TotalAmount = TotalAmount + ((ar.Amount == null) ? 0 : ar.Amount);
                    TotalOverUnder = TotalOverUnder + ((ar.OverUnder == null) ? 0 : ar.OverUnder);
                    ar.TotalActualAmount = TotalActualAmount;
                    ar.TotalAmount = TotalAmount;
                    ar.TotalOverUnder = TotalOverUnder;
                    ar.PartialPayment = DaHelper.GetString(dr, "PartialPayment");
                    ar.Fee = DaHelper.GetDecimal(dr, "Fee");
                    ar.BankKey = DaHelper.GetString(dr, "BankKey");
                    ar.ChqNo = DaHelper.GetString(dr, "ChqNo");
                    ar.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                    ar.TransfDt = DaHelper.GetDate(dr, "TranfDt");
                    ar.SpotBillInvoiceNo = DaHelper.GetString(dr, "SpotBillInvoiceNo");
                    ar.Period = DaHelper.GetString(dr, "Period");
                    ar.GroupInvoiceNo = DaHelper.GetString(dr, "GroupInvoiceNo");
                    ar.BillBookId = DaHelper.GetString(dr, "BillBookId");
                    ar.ReceiptCondition = DaHelper.GetString(dr, "ReceiptCondition");
                    ar.DueDt = DaHelper.GetDate(dr, "DueDt");
                    ar.MainSub = DaHelper.GetString(dr, "MainSub");
                    ar.Name = DaHelper.GetString(dr, "Name");
                    ar.Address = DaHelper.GetString(dr, "Address");
                    ar.Description = DaHelper.GetString(dr, "Description");
                    ar.Qty = DaHelper.GetDecimal(dr, "Qty");
                    ar.PriceUnit = DaHelper.GetDecimal(dr, "PriceUnit");
                    ar.BaseAmount = DaHelper.GetDecimal(dr, "BaseAmount");
                    ar.VatCode = DaHelper.GetString(dr, "VatCode");
                    ar.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                    ar.DisconnectDoc = DaHelper.GetString(dr, "DisconnectDoc");
                    ar.OriginalInvoiceNo = DaHelper.GetString(dr, "OriginalInvoiceNo");
                    ar.SyncDt = DaHelper.GetDate(dr, "SyncDt");
                    _expList.Add(ar);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return _expList;
        }

        public List<ARn_APPayment> GetAPChequePayment(string branchId)
        {
            List<ARn_APPayment> _expList = new List<ARn_APPayment>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_APChequePayment");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);  //TODO: Add branch id when integration complete.
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                decimal? TotalActualAmount = 0;
                decimal? TotalAmount = 0;
                decimal? TotalOverUnder = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    ARn_APPayment ar = new ARn_APPayment();
                    ar.Action = DaHelper.GetString(dr, "Action");
                    ar.PosRSG = DaHelper.GetString(dr, "PosRSG");
                    ar.PtId = DaHelper.GetString(dr, "PtId");
                    ar.ClearingAccNo = DaHelper.GetString(dr, "ClearingAccNo");
                    ar.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                    ar.PosBA = DaHelper.GetString(dr, "PosBA");
                    ar.PosBP = DaHelper.GetString(dr, "PosBP");
                    ar.CaId = DaHelper.GetString(dr, "CaId");
                    ar.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                    ar.CaDoc = DaHelper.GetString(dr, "CaDoc");
                    ar.ReceiptType = DaHelper.GetString(dr, "ReceiptType");
                    ar.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                    ar.PosId = DaHelper.GetString(dr, "PosId");
                    ar.CashierId = DaHelper.GetString(dr, "CashierId");
                    ar.ActualAmount = DaHelper.GetDecimal(dr, "ActualAmount");
                    ar.Amount = DaHelper.GetDecimal(dr, "Amount");
                    ar.OverUnder = DaHelper.GetDecimal(dr, "OverUnder");
                    TotalActualAmount = TotalActualAmount + ((ar.ActualAmount == null) ? 0 : ar.ActualAmount);
                    TotalAmount = TotalAmount + ((ar.Amount == null) ? 0 : ar.Amount);
                    TotalOverUnder = TotalOverUnder + ((ar.OverUnder == null) ? 0 : ar.OverUnder);
                    ar.TotalActualAmount = TotalActualAmount;
                    ar.TotalAmount = TotalAmount;
                    ar.TotalOverUnder = TotalOverUnder;
                    ar.PartialPayment = DaHelper.GetString(dr, "PartialPayment");
                    ar.Fee = DaHelper.GetDecimal(dr, "Fee");
                    ar.BankKey = DaHelper.GetString(dr, "BankKey");
                    ar.ChqNo = DaHelper.GetString(dr, "ChqNo");
                    ar.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                    ar.TransfDt = DaHelper.GetDate(dr, "TranfDt");
                    ar.SpotBillInvoiceNo = DaHelper.GetString(dr, "SpotBillInvoiceNo");
                    ar.Period = DaHelper.GetString(dr, "Period");
                    ar.GroupInvoiceNo = DaHelper.GetString(dr, "GroupInvoiceNo");
                    ar.BillBookId = DaHelper.GetString(dr, "BillBookId");
                    ar.ReceiptCondition = DaHelper.GetString(dr, "ReceiptCondition");
                    ar.DueDt = DaHelper.GetDate(dr, "DueDt");
                    ar.MainSub = DaHelper.GetString(dr, "MainSub");
                    ar.Name = DaHelper.GetString(dr, "Name");
                    ar.Address = DaHelper.GetString(dr, "Address");
                    ar.Description = DaHelper.GetString(dr, "Description");
                    ar.Qty = DaHelper.GetInt(dr, "Qty");
                    ar.PriceUnit = DaHelper.GetDecimal(dr, "PriceUnit");
                    ar.BaseAmount = DaHelper.GetDecimal(dr, "BaseAmount");
                    ar.VatCode = DaHelper.GetString(dr, "VatCode");
                    ar.VatAmount = DaHelper.GetDecimal(dr, "VatAmount");
                    ar.DisconnectDoc = DaHelper.GetString(dr, "DisconnectDoc");
                    ar.OriginalInvoiceNo = DaHelper.GetString(dr, "OriginalInvoiceNo");
                    ar.SyncDt = DaHelper.GetDate(dr, "SyncDt");
                    _expList.Add(ar);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return _expList;
        }

        public List<AR_Reconnect> GetARReconnect(string branchId)
        {
            List<AR_Reconnect> _expList = new List<AR_Reconnect>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_ARReconnect");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);  //TODO: Add branch id when integration complete.
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                decimal? TotalActualAmount = 0;
                decimal? TotalAmount = 0;
                decimal? TotalOverUnder = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    AR_Reconnect ar = new AR_Reconnect();
                    ar.Action = DaHelper.GetString(dr, "Action");
                    ar.PosRSG = DaHelper.GetString(dr, "PosRSG");
                    ar.PtId = DaHelper.GetString(dr, "PtId");
                    ar.ClearingAccNo = DaHelper.GetString(dr, "ClearingAccNo");
                    ar.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                    ar.PosBA = DaHelper.GetString(dr, "PosBA");
                    ar.PosBP = DaHelper.GetString(dr, "PosBP");
                    ar.CaId = DaHelper.GetString(dr, "CaId");
                    ar.InvoiceNo = DaHelper.GetString(dr, "InvoiceNo");
                    ar.CaDoc = DaHelper.GetString(dr, "CaDoc");
                    ar.ReceiptType = DaHelper.GetString(dr, "ReceiptType");
                    ar.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                    ar.PosId = DaHelper.GetString(dr, "PosId");
                    ar.CashierId = DaHelper.GetString(dr, "CashierId");
                    ar.ActualAmount = DaHelper.GetDecimal(dr, "ActualAmount");
                    ar.Amount = DaHelper.GetDecimal(dr, "Amount");
                    ar.OverUnder = DaHelper.GetDecimal(dr, "OverUnder");
                    TotalActualAmount = TotalActualAmount + ((ar.ActualAmount == null) ? 0 : ar.ActualAmount);
                    TotalAmount = TotalAmount + ((ar.Amount == null) ? 0 : ar.Amount);
                    TotalOverUnder = TotalOverUnder + ((ar.OverUnder == null) ? 0 : ar.OverUnder);
                    ar.TotalActualAmount = TotalActualAmount;
                    ar.TotalAmount = TotalAmount;
                    ar.TotalOverUnder = TotalOverUnder;
                    ar.PartialPayment = DaHelper.GetString(dr, "PartialPayment");
                    ar.Fee = DaHelper.GetDecimal(dr, "Fee");
                    ar.BankKey = DaHelper.GetString(dr, "BankKey");
                    ar.ChqNo = DaHelper.GetString(dr, "ChqNo");
                    ar.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                    ar.TransfDt = DaHelper.GetDate(dr, "TranfDt");
                    ar.SpotBillInvoiceNo = DaHelper.GetString(dr, "SpotBillInvoiceNo");
                    ar.Period = DaHelper.GetString(dr, "Period");
                    ar.GroupInvoiceNo = DaHelper.GetString(dr, "GroupInvoiceNo");
                    ar.BillBookId = DaHelper.GetString(dr, "BillBookId");
                    ar.ReceiptCondition = DaHelper.GetString(dr, "ReceiptCondition");
                    ar.DueDt = DaHelper.GetDate(dr, "DueDt");
                    ar.Name = DaHelper.GetString(dr, "Name");
                    ar.Address = DaHelper.GetString(dr, "Address");
                    ar.DisconnectDoc = DaHelper.GetString(dr, "DisconnectDoc");
                    ar.SyncDt = DaHelper.GetDate(dr, "SyncDt");
                    _expList.Add(ar);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return _expList;
        }


        #endregion

        #region AutoExport

        public void MarkBranchPrefix()
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_ins_MarkBranchPrefix");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.ExecuteNonQuery(cmd);
        }

        public void ProcessExportDone()
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_ins_ProcessExportDone");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.ExecuteNonQuery(cmd);
        }

        public bool CheckAnotherRegionalToExport()
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_get_AnyRegionalToExport");
            cmd.CommandTimeout = CMD_TIMEOUT;
            return (int)db.ExecuteScalar(cmd) == 1 ? true : false;
        }

        #endregion

        #region Set Sync_Flag

        public void SetSyncAgencyCompensation(SetSyncParam param)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_SetAgencyCompensationFlag");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "SyncDt", DbType.DateTime, param.SyncDate);
                foreach (string b in param.SyncBranches)
                {
                    db.AddInParameter(cmd, "BranchId", DbType.String, b);
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SetSyncARNormal(SetSyncParam param)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_SetARNormalFlag");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "SyncDt", DbType.DateTime, param.SyncDate);
                foreach (string b in param.SyncBranches)
                {
                    db.AddInParameter(cmd, "BranchId", DbType.String, b);
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SetSyncARSpotBill(SetSyncParam param)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_SetARSpotBillFlag");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "SyncDt", DbType.DateTime, param.SyncDate);
                foreach (string b in param.SyncBranches)
                {
                    db.AddInParameter(cmd, "BranchId", DbType.String, b);
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SetSyncARGroupInvoice(SetSyncParam param)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_SetARGroupInvoiceFlag");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "SyncDt", DbType.DateTime, param.SyncDate);
                foreach (string b in param.SyncBranches)
                {
                    db.AddInParameter(cmd, "BranchId", DbType.String, b);
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SetSyncARPaidByAgency(SetSyncParam param)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_SetARPaidByAgencyFlag");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "SyncDt", DbType.DateTime, param.SyncDate);
                foreach (string b in param.SyncBranches)
                {
                    db.AddInParameter(cmd, "BranchId", DbType.String, b);
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SetSyncARPartialPayment(SetSyncParam param)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_SetARPartialPaymentFlag");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "SyncDt", DbType.DateTime, param.SyncDate);
                foreach (string b in param.SyncBranches)
                {
                    db.AddInParameter(cmd, "BranchId", DbType.String, b);
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SetSyncARNonInvNormal(SetSyncParam param)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_SetARNonInvNormalFlag");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "SyncDt", DbType.DateTime, param.SyncDate);
                foreach (string b in param.SyncBranches)
                {
                    db.AddInParameter(cmd, "BranchId", DbType.String, b);
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SetSyncARNonInvCashSale(SetSyncParam param)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_SetARNonInvCashSaleFlag");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "SyncDt", DbType.DateTime, param.SyncDate);
                foreach (string b in param.SyncBranches)
                {
                    db.AddInParameter(cmd, "BranchId", DbType.String, b);
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SetSyncARNonInvAdvancePayment(SetSyncParam param)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_SetARNonInvAdvancePaymentFlag");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "SyncDt", DbType.DateTime, param.SyncDate);
                foreach (string b in param.SyncBranches)
                {
                    db.AddInParameter(cmd, "BranchId", DbType.String, b);
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SetSyncAPPayment(SetSyncParam param)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_SetAPPaymentFlag");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "SyncDt", DbType.DateTime, param.SyncDate);
                foreach (string b in param.SyncBranches)
                {
                    db.AddInParameter(cmd, "BranchId", DbType.String, b);
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SetSyncARDepositReceipt(SetSyncParam param)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_SetARDepositReceiptFlag");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "SyncDt", DbType.DateTime, param.SyncDate);
                foreach (string b in param.SyncBranches)
                {
                    db.AddInParameter(cmd, "BranchId", DbType.String, b);
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SetSyncAPVoucher(SetSyncParam param)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_SetAPVoucherFlag");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "SyncDt", DbType.DateTime, param.SyncDate);
                foreach (string b in param.SyncBranches)
                {
                    db.AddInParameter(cmd, "BranchId", DbType.String, b);
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SetSyncAPBankPayIn(SetSyncParam param)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_exp_SetAPBankPayInFlag");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "SyncDt", DbType.DateTime, param.SyncDate);
                foreach (string b in param.SyncBranches)
                {
                    db.AddInParameter(cmd, "BranchId", DbType.String, b);
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string GetExportFileName(string interfaceName)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_get_InterfaceRunningNumber");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "IntId", DbType.String, interfaceName);
                int running = (int)db.ExecuteScalar(cmd);

                string fname = string.Format("{0}{1}.txt", interfaceName, running.ToString().PadLeft(10, '0'));
                return fname;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string GetExportFileNameWithoutExtension(string interfaceName)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_get_InterfaceRunningNumber");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "IntId", DbType.String, interfaceName);
                int running = (int)db.ExecuteScalar(cmd);

                string fname = string.Format("{0}{1}", interfaceName, running.ToString().PadLeft(10, '0'));
                return fname;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //Get Export file name for BillBook interface 
        public string GetExportFileNameTimestamp(string interfaceName, string billBookId)
        {
          
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetSqlStringCommand("SELECT Getdate()");
                cmd.CommandTimeout = 10000;
                DateTime servDt = (DateTime)db.ExecuteScalar(cmd);

                IFormatProvider formatDate = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
                string fname = string.Format("{0}{1}_{2}.txt", interfaceName, servDt.ToString("yyyyMMddHHmmssfff", formatDate), billBookId.Substring(0, 4));
                return fname;
        }
        
        #endregion

        #region ClearSpotBillCase

        public void ClearSpotBillCase()
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbTransaction trans;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                {
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_clear_SpotBillCase");
                        cmd.CommandTimeout = 10000;
                        db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        throw e;
                    }
                }
                trans.Commit();
            }
        }

        #endregion

        #region Decrease Interface Running Number

        public void DecreaseInterfaceRunningNumber(string interfaceNumber)
        {
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbTransaction trans;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                {
                    try
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_upd_InterfaceNumber");
                        db.AddInParameter(cmd, "InterfaceNumber", DbType.String, interfaceNumber);
                        db.ExecuteNonQuery(cmd, trans);
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        throw e;
                    }
                }
                trans.Commit();
            }
        }

        #endregion

        #region GetListOfBranchIdForExportingData

        public List<String> GetListOfBranchIdForExportingData()
        {
            List<String> list = new List<String>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("ACABatch_SQL");
                DbCommand cmd = db.GetStoredProcCommand("ACA_INT_GetListOfBranchIdForExportingData");
                cmd.CommandTimeout = 10000;
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
              
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(DaHelper.GetString(dr, "BranchId"));                    
                    
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return list;
        }

        public void ClearListOfBranchIdForExportingData(List<string> branchIdList)
        {
            Database db = DatabaseFactory.CreateDatabase("ACABatch_SQL");
            DbTransaction trans;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                {
                    foreach (String branchId in branchIdList)
                    {
                        try
                        {
                            DbCommand cmd = db.GetStoredProcCommand("ACA_INT_DelBranchIdForExportingData");
                            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
                            db.ExecuteNonQuery(cmd, trans);
                        }
                        catch (Exception e)
                        {
                            trans.Rollback();
                            throw e;
                        }
                    }
                }
                trans.Commit();
            }
        }

        #endregion

        #region Reconnection

        public List<BillInfo> GetReconnection(string BranchId, DateTime fromDate, DateTime toDate)
        {
            List<BillInfo> bill = new List<BillInfo>();
            Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
            DbCommand cmd = db.GetStoredProcCommand("rc_get_ReconnectTransaction");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "BranchId", DbType.String, BranchId);
            db.AddInParameter(cmd, "FromDate", DbType.DateTime, fromDate);
            db.AddInParameter(cmd, "ToDate", DbType.DateTime, toDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                BillInfo b = new BillInfo();
                b.CaId = DaHelper.GetString(dr, "CaId");
                b.DiscNo = DaHelper.GetString(dr, "DiscNo");
                b.Main = DaHelper.GetString(dr, "Main");
                b.Sub = DaHelper.GetString(dr, "Sub");
                b.CaDoc = DaHelper.GetString(dr, "CaDoc");
                b.ReceiptId = DaHelper.GetString(dr, "ReceiptId");
                b.GAmount = DaHelper.GetDecimal(dr, "ReceiptType");
                b.CashierId = DaHelper.GetString(dr, "CashierId");
                b.PaymentDt = DaHelper.GetDateTime(dr, "PaymentDt");
                b.TechBranchId = DaHelper.GetString(dr, "TechBranchId");
                b.DueDt = DaHelper.GetDateTime(dr, "DueDt");
                bill.Add(b);
            }

            return bill;
        }

        #endregion

        #region Other

        public DateTime GetDBDateTime()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_get_ServerTime");
                cmd.CommandTimeout = 10000;
                DateTime DBDateTime = (DateTime)db.ExecuteScalar(cmd);

                return DBDateTime;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string GetDBDateTime(string format)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase("BPMDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_get_ServerTime");
                cmd.CommandTimeout = 10000;
                DateTime dbDateTime = (DateTime)db.ExecuteScalar(cmd);

                IFormatProvider fp = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
                string fname = dbDateTime.ToString(format, fp); //yyyyMMdd_HHmmsss
                return fname;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}

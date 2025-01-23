using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

using PEA.BPM.Architecture.CommonUtilities;
using System.Data.SqlClient;
using PEA.BPM.Integration.ACABatchService;
using System.IO;
using Avanade.ACA.Batch;
using Avanade.ACA.Batch.Configuration;

namespace PEA.BPM.Integration.BPMIntegration.DA
{
    public class BPMServerDataAccess
    {

        private const string DOWNLOADED_DB_NAME = "BPMDatabase";
        private const string UPLOADED_DB_NAME = "BPMDatabase";        

        public DateTime GetServerTime()
        {
            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_get_ServerTime");

            return (DateTime)db.ExecuteScalar(cmd);
        }

        #region Download BusinessPartner data from BPM

        public List<BusinessPartnerInfo> StartProcessTableJoining(string branchId, DateTime fromDt, DateTime toDt)
        {
            // Local Variables Declaration
            DataTable objDTBizPartner = default(DataTable);
            DataTable objDTBranch = default(DataTable);
            DataTable objDTConAcct = default(DataTable);
            DataTable objResult = default(DataTable);
            string strTmp = "";

            try
            {
                objDTBizPartner = GetModifiedBusinessPartner(fromDt, toDt);
                objDTBizPartner.TableName = "BusinessPartner";
                objDTBranch = GetBranchList(branchId);
                objDTBranch.TableName = "Branch";

                foreach (DataRow objDR in objDTBranch.Rows)
                {
                    if (strTmp != "") strTmp += ",";
                    strTmp += "'" + objDR["BranchId"] + "'";
                }

                objDTConAcct = GetContractAccount(strTmp);
                objDTConAcct.TableName = "ContractAccount";

                objResult = Join(objDTBizPartner, objDTConAcct,
                       new DataColumn[] { objDTBizPartner.Columns["BpId"] },
                       new DataColumn[] { objDTConAcct.Columns["BpId"] });

                //dataGridView1.DataSource = objResult;
                //textBox5.Text = "Total records = " + objDTBizPartner.Rows.Count.ToString() + " Done at " + DateTime.Now.ToString();

                List<BusinessPartnerInfo> list = new List<BusinessPartnerInfo>();

                foreach (DataRow dr in objResult.Rows)
                {
                    BusinessPartnerInfo obj = new BusinessPartnerInfo();
                    obj.BpId = DaHelper.GetString(dr, "BpId");
                    obj.BpTypeId = DaHelper.GetString(dr, "BpTypeId");
                    obj.TaxCode = DaHelper.GetString(dr, "TaxCode");
                    obj.CitizenId = DaHelper.GetString(dr, "CitizenId");
                    obj.PassportNo = DaHelper.GetString(dr, "PassportNo");
                    obj.RegisterId = DaHelper.GetString(dr, "RegisterId");
                    obj.VatCode = DaHelper.GetString(dr, "VatCode");
                    obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                    obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    list.Add(obj);
                }

                //textBox4.Text = "Object Mapping Done at " + DateTime.Now.ToString();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>Joins 2 datatables, similar to T-SQL inner join</summary>
        /// <param name="First">First datatable to be joined</param>
        /// <param name="Second">Second datatable to be joined</param>
        /// <param name="FJC">First column (Primary key column)</param>
        /// <param name="SJC">Second column (Foreign key column)</param>
        /// <returns></returns>
        public static DataTable Join(DataTable First, DataTable Second, DataColumn[] FJC, DataColumn[] SJC)
        {
            //Create Empty Table
            DataTable table = new DataTable("Join");

            try
            {
                // Use a DataSet to leverage DataRelation
                using (DataSet ds = new DataSet())
                {
                    //Add Copy of Tables
                    ds.Tables.AddRange(new DataTable[] { First.Copy(), Second.Copy() });

                    //Identify Joining Columns from First
                    DataColumn[] parentcolumns = new DataColumn[FJC.Length];

                    for (int i = 0; i < parentcolumns.Length; i++)
                    {
                        parentcolumns[i] = ds.Tables[0].Columns[FJC[i].ColumnName];
                    }

                    //Identify Joining Columns from Second
                    DataColumn[] childcolumns = new DataColumn[SJC.Length];

                    for (int i = 0; i < childcolumns.Length; i++)
                    {
                        childcolumns[i] = ds.Tables[1].Columns[SJC[i].ColumnName];
                    }


                    //Create DataRelation
                    DataRelation r = new DataRelation(string.Empty, parentcolumns, childcolumns, false);
                    ds.Relations.Add(r);


                    //Create Columns for JOIN table

                    for (int i = 0; i < First.Columns.Count; i++)
                    {
                        table.Columns.Add(First.Columns[i].ColumnName, First.Columns[i].DataType);
                    }

                    for (int i = 0; i < Second.Columns.Count; i++)
                    {

                        //Beware Duplicates

                        if (!table.Columns.Contains(Second.Columns[i].ColumnName))

                            table.Columns.Add(Second.Columns[i].ColumnName, Second.Columns[i].DataType);

                        else

                            table.Columns.Add(Second.Columns[i].ColumnName + "_Second", Second.Columns[i].DataType);

                    }


                    //Loop through First table

                    table.BeginLoadData();

                    foreach (DataRow firstrow in ds.Tables[0].Rows)
                    {
                        //Get "joined" rows
                        DataRow[] childrows = firstrow.GetChildRows(r);

                        if (childrows != null && childrows.Length > 0)
                        {

                            object[] parentarray = firstrow.ItemArray;

                            foreach (DataRow secondrow in childrows)
                            {

                                object[] secondarray = secondrow.ItemArray;

                                object[] joinarray = new object[parentarray.Length + secondarray.Length];

                                Array.Copy(parentarray, 0, joinarray, 0, parentarray.Length);

                                Array.Copy(secondarray, 0, joinarray, parentarray.Length, secondarray.Length);

                                table.LoadDataRow(joinarray, true);

                            }

                        }

                    }

                    table.EndLoadData();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return table;

        }

        /// <summary>Get the list of Contract Account</summary>
        /// <param name="strBranchIds"></param>
        /// <returns></returns>
        private DataTable GetContractAccount(String strBranchIds)
        {
            try
            {
                //List<ContractAccountInfo> retVals = new List<ContractAccountInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_ADOGetContractAccount");
                db.AddInParameter(cmd, "pBranchIds", DbType.String, strBranchIds);
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private DataTable GetBranchList(string branchId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_ADOGetBranchTree");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>To get the list of modified business partners</summary>
        /// <returns>False if error, true otherwise</returns>
        private DataTable GetModifiedBusinessPartner(DateTime lastModifiedDt, DateTime serverDt)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_ADOGetModifiedBusinessPartner");
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDt);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDt);
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        #endregion     

        #region Download master data DL10 from BPM

        //*********************************************DL01 ISOLATE*******************************************************
        public List<NonWorkingDayInfo> GetUpdateNonworkingDay(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<NonWorkingDayInfo> retVals = new List<NonWorkingDayInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);                
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_NonWorkingDay");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                //if (dt.Rows.Count == 0) return null;
                foreach (DataRow dr in dt.Rows)
                {
                    NonWorkingDayInfo obj = new NonWorkingDayInfo();
                    obj.NwId = DaHelper.GetString(dr, "NwId");
                    obj.CdType = DaHelper.GetString(dr, "CdType");
                    obj.NwDay = DaHelper.GetDate(dr, "NwDay");
                    obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                    obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AccountClassInfo> GetUpdateAccountClass(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<AccountClassInfo> retVals = new List<AccountClassInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_AccountClass");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                //if (dt.Rows.Count == 0) return null;
                foreach (DataRow dr in dt.Rows)
                {
                    AccountClassInfo obj = new AccountClassInfo();
                    obj.AccountClassId = DaHelper.GetString(dr, "AccountClassId");
                    obj.AccountClassName = DaHelper.GetString(dr, "AccountClassName");
                    obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                    obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ContractTypeInfo> GetUpdateContractType(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<ContractTypeInfo> retVals = new List<ContractTypeInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_ContractType");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                //if (dt.Rows.Count == 0) return null;
                foreach (DataRow dr in dt.Rows)
                {
                    ContractTypeInfo obj = new ContractTypeInfo();
                    obj.CtId = DaHelper.GetString(dr, "CtId");
                    obj.CtName = DaHelper.GetString(dr, "CtName");
                    obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                    obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
    }

        public List<MeterSizeInfo> GetUpdateMeterSize(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<MeterSizeInfo> retVals = new List<MeterSizeInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_MeterSize");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                //if (dt.Rows.Count == 0) return null;
                foreach (DataRow dr in dt.Rows)
                {
                    MeterSizeInfo obj = new MeterSizeInfo();
                    obj.MeterSizeId = DaHelper.GetString(dr, "MeterSizeId");
                    obj.MeterSizeName = DaHelper.GetString(dr, "MeterSizeName");
                    obj.ReConnectMeterRate = DaHelper.GetDecimal(dr, "ReConnectMeterRate");
                    obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                    obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<PaymentMethodInfo> GetUpdatePaymentMethod(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<PaymentMethodInfo> retVals = new List<PaymentMethodInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_PaymentMethod");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                //if (dt.Rows.Count == 0) return null;
                foreach (DataRow dr in dt.Rows)
                {
                    PaymentMethodInfo obj = new PaymentMethodInfo();
                    obj.PmId = DaHelper.GetString(dr, "PmId");
                    obj.PmName = DaHelper.GetString(dr, "PmName");
                    obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                    obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<TaxCodeInfo> GetUpdateTaxCode(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<TaxCodeInfo> retVals = new List<TaxCodeInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_TaxCode");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                //if (dt.Rows.Count == 0) return null;
                foreach (DataRow dr in dt.Rows)
                {
                    TaxCodeInfo obj = new TaxCodeInfo();
                    obj.TaxCode = DaHelper.GetString(dr, "TaxCode");
                    obj.TaxName = DaHelper.GetString(dr, "TaxName");
                    obj.TaxRate = DaHelper.GetDecimal(dr, "TaxRate");
                    obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                    obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<UnitTypeInfo> GetUpdateUnitType(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<UnitTypeInfo> retVals = new List<UnitTypeInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_UnitType");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                //if (dt.Rows.Count == 0) return null;
                foreach (DataRow dr in dt.Rows)
                {
                    UnitTypeInfo obj = new UnitTypeInfo();
                    obj.UnitTypeId = DaHelper.GetString(dr, "UnitTypeId");
                    obj.UnitTypeName = DaHelper.GetString(dr, "UnitTypeName");
                    obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                    obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        //new arrival
        public List<BusinessPartnerTypeInfo> GetUpdateBusinessPartnerType(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<BusinessPartnerTypeInfo> retVals = new List<BusinessPartnerTypeInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_BusinessPartnerType");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                //if (dt.Rows.Count == 0) return null;
                foreach (DataRow dr in dt.Rows)
                {
                    BusinessPartnerTypeInfo obj = new BusinessPartnerTypeInfo();
                    obj.BpTypeId = DaHelper.GetString(dr, "BpTypeId");
                    obj.BpTypeDesc = DaHelper.GetString(dr, "BpTypeDesc");
                    obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                    obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CalendarInfo> GetUpdateCalendar(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<CalendarInfo> retVals = new List<CalendarInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_Calendar");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                //if (dt.Rows.Count == 0) return null;
                foreach (DataRow dr in dt.Rows)
                {
                    CalendarInfo obj = new CalendarInfo();
                    obj.CdType = DaHelper.GetString(dr, "CdType");
                    obj.CdName = DaHelper.GetString(dr, "CdName");
                    obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                    obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<PaymentTypeInfo> GetUpdatePaymentType(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<PaymentTypeInfo> retVals = new List<PaymentTypeInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_PaymentType");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            //if (dt.Rows.Count == 0) return null;
            foreach (DataRow dr in dt.Rows)
            {
                PaymentTypeInfo obj = new PaymentTypeInfo();
                obj.PtId = DaHelper.GetString(dr, "PtId");
                obj.PtName = DaHelper.GetString(dr, "PtName");
                obj.PaymentSeq = DaHelper.GetString(dr, "PaymentSeq");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                retVals.Add(obj);
            }
            dt.Dispose();
            return retVals;
        }

        public List<InterestKeyInfo> GetUpdateInterestKey(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<InterestKeyInfo> retVals = new List<InterestKeyInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_InterestKey");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                InterestKeyInfo obj = new InterestKeyInfo();
                obj.InterestKey = DaHelper.GetString(dr, "InterestKey");
                obj.InterestName = DaHelper.GetString(dr, "InterestName");
                obj.InterestRate = DaHelper.GetDecimal(dr, "InterestRate");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                retVals.Add(obj);
            }
            dt.Dispose();
            return retVals;
        }

        public List<AgencyBillCollectionStatusInfo> GetUpdateAgencyBillCollectionStatus(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<AgencyBillCollectionStatusInfo> retVals = new List<AgencyBillCollectionStatusInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_AgencyBillCollectionStatus");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                AgencyBillCollectionStatusInfo obj = new AgencyBillCollectionStatusInfo();
                obj.AbsId = DaHelper.GetString(dr, "AbsId");
                obj.AbsName = DaHelper.GetString(dr, "AbsName");
                obj.Description = DaHelper.GetString(dr, "Description");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;             
                
                retVals.Add(obj);
            }
            dt.Dispose();
            return retVals;
        }

        public List<AgencyBillOutStatusInfo> GetUpdateAgencyBillOutStatus(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<AgencyBillOutStatusInfo> retVals = new List<AgencyBillOutStatusInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_AgencyBillOutStatus");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                AgencyBillOutStatusInfo obj = new AgencyBillOutStatusInfo();
                obj.AboId = DaHelper.GetString(dr, "AboId");
                obj.AboName = DaHelper.GetString(dr, "AboName");
                obj.Description = DaHelper.GetString(dr, "Description");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;

                retVals.Add(obj);
            }
            dt.Dispose();
            return retVals;
        }

        public List<AgencyCommissionRateInfo> GetUpdateAgencyCommissionRate(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<AgencyCommissionRateInfo> retVals = new List<AgencyCommissionRateInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_AgencyCommissionRate");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                AgencyCommissionRateInfo obj = new AgencyCommissionRateInfo();
                obj.RtId = DaHelper.GetInt(dr, "RtId");
                obj.BranchId = DaHelper.GetString(dr, "BranchId");
                obj.HouseRegRate = DaHelper.GetDecimal(dr, "HouseRegRate");
                obj.HouseGrpRate = DaHelper.GetDecimal(dr, "HouseGrpRate");
                obj.CorpRegRate = DaHelper.GetDecimal(dr, "CorpRegRate");
                obj.CorpGrpRate = DaHelper.GetDecimal(dr, "CorpGrpRate");
                obj.GovRegRate = DaHelper.GetDecimal(dr, "GovRegRate");
                obj.GovGrpRate = DaHelper.GetDecimal(dr, "GovGrpRate");
                obj.BillingNinetyPercent = DaHelper.GetDecimal(dr, "BillingNinetyPercent");
                obj.BillingNinetyNinePercent = DaHelper.GetDecimal(dr, "BillingNinetyNinePercent");
                obj.BillingHundredPercent = DaHelper.GetDecimal(dr, "BillingHundredPercent");
                obj.MaxInvPercent = DaHelper.GetDecimal(dr, "MaxInvPercent");
                obj.InvRate = DaHelper.GetDecimal(dr, "InvRate");
                obj.InvPastThreeMonthRate = DaHelper.GetDecimal(dr, "InvPastThreeMonthRate");
                obj.FineRatePerBill = DaHelper.GetDecimal(dr, "FineRatePerBill");
                obj.CmCountBlock = DaHelper.GetInt(dr, "CmCountBlock");
                obj.CmCountLimit = DaHelper.GetInt(dr, "CmCountLimit");
                obj.VatRate = DaHelper.GetDecimal(dr, "VatRate");
                obj.CollectedPercent = DaHelper.GetDecimal(dr, "CollectedPercent");
                obj.PenaltyWaive = DaHelper.GetString(dr, "PenaltyWaive");
                obj.CaCount = DaHelper.GetInt(dr, "CaCount");
                obj.UpperRate = DaHelper.GetDecimal(dr, "UpperRate");
                obj.LowerRate = DaHelper.GetDecimal(dr, "LowerRate");
                obj.TaxRate = DaHelper.GetDecimal(dr, "TaxRate");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;

                retVals.Add(obj);
            }
            dt.Dispose();
            return retVals;
        }

        public List<BillBookStatusInfo> GetUpdateBillBookStatus(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<BillBookStatusInfo> retVals = new List<BillBookStatusInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_BillBookStatus");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                BillBookStatusInfo obj = new BillBookStatusInfo();
                obj.BsId = DaHelper.GetString(dr, "BsId");
                obj.BsName = DaHelper.GetString(dr, "BsName");
                obj.BsDesc = DaHelper.GetString(dr, "BsDesc");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                retVals.Add(obj);
            }
            dt.Dispose();
            return retVals;
        }

        public List<AgencyCollectAreaInfo> GetUpdateAgencyCollectArea(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<AgencyCollectAreaInfo> retVals = new List<AgencyCollectAreaInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_AgencyCollectArea");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                AgencyCollectAreaInfo obj = new AgencyCollectAreaInfo();
                db.AddInParameter(cmd, "pCaId", DbType.String, obj.CaId);
                db.AddInParameter(cmd, "pCollectArea", DbType.String, obj.CollectArea);
                db.AddInParameter(cmd, "pSyncFlag", DbType.String, "0");
                db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                retVals.Add(obj);
            }
            dt.Dispose();
            return retVals;
        }

        public List<DisconnectionStatusInfo> GetUpdateDisconnectionStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<DisconnectionStatusInfo> retVals = new List<DisconnectionStatusInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_DisconnectionStatus");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                DisconnectionStatusInfo obj = new DisconnectionStatusInfo();
                obj.DiscStatusId = DaHelper.GetString(dr, "DiscStatusId");
                obj.Description = DaHelper.GetString(dr, "Description");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                retVals.Add(obj);
            }
            dt.Dispose();
            return retVals;
        }

        public List<CashierMoneyFlowTypeInfo> GetUpdateCashierMoneyFlowType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<CashierMoneyFlowTypeInfo> retVals = new List<CashierMoneyFlowTypeInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_CashierMoneyFlowType");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                CashierMoneyFlowTypeInfo obj = new CashierMoneyFlowTypeInfo();
                obj.FlowType = DaHelper.GetString(dr, "FlowType");
                obj.FlowDesc = DaHelper.GetString(dr, "FlowDesc");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                retVals.Add(obj);
            }
            dt.Dispose();
            return retVals;
        }

        public List<OwnBankInfo> GetUpdateOwnBank(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<OwnBankInfo> retVals = new List<OwnBankInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_OwnBank");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                OwnBankInfo obj = new OwnBankInfo();
                obj.HouseBank = DaHelper.GetString(dr, "HouseBank");
                obj.BankName = DaHelper.GetString(dr, "BankName");
                obj.Filter = DaHelper.GetString(dr, "Filter");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                retVals.Add(obj);
            }
            dt.Dispose();
            return retVals;
        }

        #endregion

        #region Download master data DL20 from BPM

        //*********************************************DL02 MASTER********************************************************

        public List<BusinessPartnerInfo> GetUpdateBusinessPartner(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                //return StartProcessTableJoining(branchid, lastModifiedDate, serverDate);
                //-----------------------------------------------------------------------------

                List<BusinessPartnerInfo> retVals = new List<BusinessPartnerInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_BusinessPartner");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    BusinessPartnerInfo b = new BusinessPartnerInfo();
                    b.BpId = DaHelper.GetString(dr, "BpId");
                    b.BpTypeId = DaHelper.GetString(dr, "BpTypeId");
                    b.TaxCode = DaHelper.GetString(dr, "TaxCode");
                    b.CitizenId = DaHelper.GetString(dr, "CitizenId");
                    b.PassportNo = DaHelper.GetString(dr, "PassportNo");
                    b.RegisterId = DaHelper.GetString(dr, "RegisterId");
                    b.VatCode = DaHelper.GetString(dr, "VatCode");
                    b.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    b.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                    b.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    b.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    retVals.Add(b);
                }
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

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_Branch");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                //if (dt.Rows.Count == 0) return null;
                foreach (DataRow dr in dt.Rows)
                {
                    BranchInfo obj = new BranchInfo();
                    obj.BranchId = DaHelper.GetString(dr, "BranchId");
                    obj.BranchName = DaHelper.GetString(dr, "BranchName");
                    obj.BranchName2 = DaHelper.GetString(dr, "BranchName2");
                    obj.BranchNo = DaHelper.GetString(dr, "BranchNo");
                    obj.Address = DaHelper.GetString(dr, "Address");
                    obj.BranchLevel = DaHelper.GetString(dr, "BranchLevel");
                    obj.BusinessArea = DaHelper.GetString(dr, "BusinessArea");
                    obj.BusinessPlace = DaHelper.GetString(dr, "BusinessPlace");
                    obj.CdType = DaHelper.GetString(dr, "CdType");
                    //obj.ParentBranchId = DaHelper.GetString(dr, "ParentBranchId");
                    obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                    obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<MRUInfo> GetUpdateMru(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<MRUInfo> retVals = new List<MRUInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_MRU");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                //if (dt.Rows.Count == 0) return null;
                foreach (DataRow dr in dt.Rows)
                {
                    MRUInfo obj = new MRUInfo();
                    obj.BranchId = DaHelper.GetString(dr, "BranchId");
                    obj.MruId = DaHelper.GetString(dr, "MruId");
                    obj.MruName = DaHelper.GetString(dr, "MruName");
                    obj.AdvPay = DaHelper.GetString(dr, "AdvPay");
                    obj.PortionId = DaHelper.GetString(dr, "PortionId");
                    obj.PortionDesc = DaHelper.GetString(dr, "PortionDesc");
                    obj.PtcNo = DaHelper.GetString(dr, "PtcNo");
                    obj.IntownFlag = DaHelper.GetString(dr, "IntownFlag");
                    obj.ReaderType = DaHelper.GetString(dr, "ReaderType");
                    obj.TravelHelp = DaHelper.GetString(dr, "TravelHelp");
                    obj.HelpValidFrom = DaHelper.GetDate(dr, "HelpValidFrom");
                    obj.HelpValidTo = DaHelper.GetDate(dr, "HelpValidTo");
                    obj.MeterReaderId = DaHelper.GetString(dr, "MeterReaderId");
                    obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                    obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<MRUPlanInfo> GetUpdateMRUPlan(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<MRUPlanInfo> retVals = new List<MRUPlanInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_MruPlan");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                //if (dt.Rows.Count == 0) return null;
                foreach (DataRow dr in dt.Rows)
                {
                    MRUPlanInfo b = new MRUPlanInfo();
                    b.MruPlanId = DaHelper.GetString(dr, "MruPlanId");
                    b.PortionId = DaHelper.GetString(dr, "PortionId");
                    b.BranchId = DaHelper.GetString(dr, "BranchId");
                    b.MruId = DaHelper.GetString(dr, "MruId");
                    b.Period = DaHelper.GetString(dr, "Period");
                    b.MeterReadDt = DaHelper.GetDate(dr, "MeterReadDt");
                    b.MeterReadActDt = DaHelper.GetDate(dr, "MeterReadActDt");
                    b.TransferDt = DaHelper.GetDate(dr, "TransferDt");
                    b.TransferActDt = DaHelper.GetDate(dr, "TransferActDt");
                    b.BillPrintDt = DaHelper.GetDate(dr, "BillPrintDt");
                    b.BillPrintActDt = DaHelper.GetDate(dr, "BillPrintActDt");
                    b.BookCreateDt = DaHelper.GetDate(dr, "BookCreateDt");
                    b.BookCreateActDt = DaHelper.GetDate(dr, "BookCreateActDt");
                    b.BookCheckInDt = DaHelper.GetDate(dr, "BookCheckInDt"); 
                    b.BookCheckInActDt = DaHelper.GetDate(dr, "BookCheckInActDt");
                    b.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    b.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                    b.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    b.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    retVals.Add(b);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ContractAccountInfo> GetUpdateContractAccount(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<ContractAccountInfo> retVals = new List<ContractAccountInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_ContractAccount");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                //if (dt.Rows.Count == 0) return null;
                foreach (DataRow dr in dt.Rows)
                {
                    ContractAccountInfo obj = new ContractAccountInfo();
                    obj.CaId = DaHelper.GetString(dr, "CaId");
                    obj.TechBranchId = DaHelper.GetString(dr, "TechBranchId");
                    obj.CommBranchId = DaHelper.GetString(dr, "CommBranchId");
                    obj.MruId = DaHelper.GetString(dr, "MruId");
                    obj.BpId = DaHelper.GetString(dr, "BpId");
                    obj.CaName = DaHelper.GetString(dr, "CaName");
                    obj.ReceiptPrintName = DaHelper.GetString(dr, "ReceiptPrintName");
                    obj.CaAddress = DaHelper.GetString(dr, "CaAddress");
                    obj.CtId = DaHelper.GetString(dr, "CtId");
                    obj.PmId = DaHelper.GetString(dr, "PmId");
                    obj.AccountClassId = DaHelper.GetString(dr, "AccountClassId");
                    obj.SecurityDeposit = DaHelper.GetDecimal(dr, "SecurityDeposit");
                    obj.InterestKey = DaHelper.GetString(dr, "InterestKey");
                    obj.PaidBy = DaHelper.GetString(dr, "PaidBy");
                    obj.ContractValidFrom = DaHelper.GetDate(dr, "ContractValidFrom");
                    obj.ContractValidTo = DaHelper.GetDate(dr, "ContractValidTo");
                    obj.MeterSizeId = DaHelper.GetString(dr, "MeterSizeId");
                    obj.MeterInstallDt = DaHelper.GetDate(dr, "MeterInstallDt");
                    obj.ControllerId = DaHelper.GetString(dr, "ControllerId");
                    obj.GroupIvReceiptType = DaHelper.GetString(dr, "GroupIvReceiptType");
                    obj.TransportHelp = DaHelper.GetDecimal(dr, "TransportHelp");
                    obj.PenaltyWaiveFlag = DaHelper.GetString(dr, "PenaltyWaiveFlag");
                    obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                    obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<EmployeeInfo> GetUpdateEmployee(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<EmployeeInfo> retVals = new List<EmployeeInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_Employee");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                //if (dt.Rows.Count == 0) return null;
                foreach (DataRow dr in dt.Rows)
                {
                    EmployeeInfo obj = new EmployeeInfo();
                    obj.EmployeeId = DaHelper.GetString(dr, "EmployeeId");
                    obj.FirstName = DaHelper.GetString(dr, "FirstName");
                    obj.LastName = DaHelper.GetString(dr, "LastName");
                    obj.Department = DaHelper.GetString(dr, "Department");
                    obj.BranchId = DaHelper.GetString(dr, "BranchId");
                    obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                    obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AgencyAssetInfo> GetUpdateAgencyAsset(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<AgencyAssetInfo> retVals = new List<AgencyAssetInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_AgencyAsset");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                //if (dt.Rows.Count == 0) return null;
                foreach (DataRow dr in dt.Rows)
                {
                    AgencyAssetInfo obj = new AgencyAssetInfo();
                    obj.AssetId = DaHelper.GetString(dr, "AssetId");
                    obj.CaId = DaHelper.GetString(dr, "CaId");
                    obj.AssetType = DaHelper.GetString(dr, "AssetType");
                    obj.AssetTypeDesc = DaHelper.GetString(dr, "AssetTypeDesc");
                    obj.AssetValue = DaHelper.GetDecimal(dr, "AssetValue");
                    obj.Status = DaHelper.GetString(dr, "Status");
                    obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                    obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    retVals.Add(obj);
                }
                dt.Dispose();
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

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_Bank");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                //if (dt.Rows.Count == 0) return null;
                foreach (DataRow dr in dt.Rows)
                {
                    BankInfo obj = new BankInfo();
                    obj.BankKey = DaHelper.GetString(dr, "BankKey");
                    obj.BankName = DaHelper.GetString(dr, "BankName");
                    obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                    obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    retVals.Add(obj);
                }
                dt.Dispose();
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

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_BankAccount");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                //if (dt.Rows.Count == 0) return null;
                foreach (DataRow dr in dt.Rows)
                {
                    BankAccountInfo obj = new BankAccountInfo();
                    obj.BankKey = DaHelper.GetString(dr, "Bankkey");
                    obj.AccountNo = DaHelper.GetString(dr, "AccountNo");
                    obj.BusinessPlace = DaHelper.GetString(dr, "BusinessPlace");
                    obj.ClearingAccNo = DaHelper.GetString(dr, "ClearingAccNo");
                    obj.HouseBank = DaHelper.GetString(dr, "HouseBank");
                    obj.AccountType = DaHelper.GetString(dr, "AccountType");
                    obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                    obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<MainSubInfo> GetUpdateDebtType(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<MainSubInfo> retVals = new List<MainSubInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_DebtType");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                //if (dt.Rows.Count == 0) return null;
                foreach (DataRow dr in dt.Rows)
                {
                    MainSubInfo obj = new MainSubInfo();
                    obj.DtId = DaHelper.GetString(dr, "DtId");
                    obj.DtName = DaHelper.GetString(dr, "DtName");
                    obj.DefaultPaperSize = DaHelper.GetString(dr, "DefaultPaperSize");
                    obj.DefaultTaxCode = DaHelper.GetString(dr, "DefaultTaxCode");
                    obj.DefaultInterestKey = DaHelper.GetString(dr, "DefaultInterestKey");
                    obj.NonInvoicePaymentFlag = DaHelper.GetString(dr, "NonInvoicePaymentFlag");
                    obj.CategoryPaymentCode = DaHelper.GetString(dr, "CategoryPaymentCode");
                    obj.ModReceiptHeaderFlag = DaHelper.GetString(dr, "ModReceiptHeaderFlag");
                    obj.IndividualReceiptFlag = DaHelper.GetString(dr, "IndividualReceiptFlag");
                    obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                    obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    retVals.Add(obj);
                }
                dt.Dispose();
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

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_PaymentSequence");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                //if (dt.Rows.Count == 0) return null;
                foreach (DataRow dr in dt.Rows)
                {
                    PaymentSequenceInfo obj = new PaymentSequenceInfo();
                    obj.PaymentSequence = DaHelper.GetString(dr, "PaymentSequence");
                    obj.DtId = DaHelper.GetString(dr, "DtId");
                    obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                    obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        public string GetParentBranch(string branchId)
        {
            List<BranchInfo> retVals = new List<BranchInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_get_ParentBranch");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            object result = db.ExecuteScalar(cmd);
            if (result == null)
                return string.Empty;
            else
                return result.ToString();
        }

        public List<OldCaMappingInfo> GetUpdateOldCaMapping(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<OldCaMappingInfo> retVals = new List<OldCaMappingInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_OldCaMapping");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    OldCaMappingInfo b = new OldCaMappingInfo();
                    b.OldCaID = DaHelper.GetString(dr, "OldCaID");
                    b.NewCaID = DaHelper.GetString(dr, "NewCaID");
                    b.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    //b.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    retVals.Add(b);
                }
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        #endregion

        #region Download billing data DL30 from BPM

        //ts.BillDetail
        public List<BillingDetailInfo> GetUpdateBillingDetail(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
            List<BillingDetailInfo> retVals = new List<BillingDetailInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_BillingDetail");
            cmd.CommandTimeout = 10000;
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                BillingDetailInfo obj = new BillingDetailInfo();
                obj.W_01_print_doc = DaHelper.GetString(dr, "W_01_print_doc");
                obj.W_10_doc_date = DaHelper.GetString(dr, "W_10_doc_date");
                obj.W_20_buss_place = DaHelper.GetString(dr, "W_20_buss_place");
                obj.W_30_office_code = DaHelper.GetString(dr, "W_30_office_code");
                obj.W_40_sname = DaHelper.GetString(dr, "W_40_sname");
                obj.W_80_cust_name1 = DaHelper.GetString(dr, "W_80_cust_name1");
                obj.W_80_cust_name2 = DaHelper.GetString(dr, "W_80_cust_name2");
                obj.W_90_cust_name1 = DaHelper.GetString(dr, "W_90_cust_name1");
                obj.W_90_cust_name2 = DaHelper.GetString(dr, "W_90_cust_name2");
                obj.W_100_sender = DaHelper.GetString(dr, "W_100_sender");
                obj.W_110_post_code = DaHelper.GetString(dr, "W_110_post_code");
                obj.W_121_mailing_person = DaHelper.GetString(dr, "W_121_mailing_person");
                obj.W_122_mailing_person = DaHelper.GetString(dr, "W_122_mailing_person");
                obj.W_130_period = DaHelper.GetString(dr, "W_130_period");
                obj.W_140_reg = DaHelper.GetString(dr, "W_140_reg");
                obj.W_150_cont_acct = DaHelper.GetString(dr, "W_150_cont_acct");
                obj.W_160_device = DaHelper.GetString(dr, "W_160_device");
                obj.W_170_rate_cat = DaHelper.GetString(dr, "W_170_rate_cat");
                obj.W_171_ettat_code = DaHelper.GetString(dr, "W_171_ettat_code");
                obj.W_180_voltage = DaHelper.GetString(dr, "W_180_voltage");
                obj.W_190_multi_n = DaHelper.GetString(dr, "W_190_multi_n");
                obj.W_191_multi_o = DaHelper.GetString(dr, "W_191_multi_o");
                obj.W_200_mr_date = DaHelper.GetString(dr, "W_200_mr_date");
                obj.W_216_address = DaHelper.GetString(dr, "W_216_address");
                obj.W_217_address = DaHelper.GetString(dr, "W_217_address");
                obj.W_218_address = DaHelper.GetString(dr, "W_218_address");
                obj.W_221_address = DaHelper.GetString(dr, "W_221_address");
                obj.W_222_address = DaHelper.GetString(dr, "W_222_address");
                obj.W_223_address = DaHelper.GetString(dr, "W_223_address");
                obj.W_230_post_code = DaHelper.GetString(dr, "W_230_post_code");
                obj.W_241_address = DaHelper.GetString(dr, "W_241_address");
                obj.W_242_address = DaHelper.GetString(dr, "W_242_address");
                obj.W_243_address = DaHelper.GetString(dr, "W_243_address");
                obj.W_250_post_code = DaHelper.GetString(dr, "W_250_post_code");
                obj.W_255_device_1 = DaHelper.GetString(dr, "W_255_device_1");
                obj.W_260_zwstand_1_txt = DaHelper.GetString(dr, "W_260_zwstand_1_txt");
                obj.W_270_zwstvor_1_txt = DaHelper.GetString(dr, "W_270_zwstvor_1_txt");
                obj.W_280_umwfakt_1_txt = DaHelper.GetString(dr, "W_280_umwfakt_1_txt");
                obj.W_290_abrmenge_1_txt = DaHelper.GetString(dr, "W_290_abrmenge_1_txt");
                obj.W_295_device_2 = DaHelper.GetString(dr, "W_295_device_2");
                obj.W_300_zwstand_2_txt = DaHelper.GetString(dr, "W_300_zwstand_2_txt");
                obj.W_310_zwstvor_2_txt = DaHelper.GetString(dr, "W_310_zwstvor_2_txt");
                obj.W_320_umwfakt_2_txt = DaHelper.GetString(dr, "W_320_umwfakt_2_txt");
                obj.W_330_abrmenge_2_txt = DaHelper.GetString(dr, "W_330_abrmenge_2_txt");
                obj.W_340_peak_txt = DaHelper.GetString(dr, "W_340_peak_txt");
                obj.W_350_dash_txt = DaHelper.GetString(dr, "W_350_dash_txt");
                obj.W_355_device_3 = DaHelper.GetString(dr, "W_355_device_3");
                obj.W_360_zwstand_3_txt = DaHelper.GetString(dr, "W_360_zwstand_3_txt");
                obj.W_370_zwstvor_3_txt = DaHelper.GetString(dr, "W_370_zwstvor_3_txt");
                obj.W_380_umwfakt_3_txt = DaHelper.GetString(dr, "W_380_umwfakt_3_txt");
                obj.W_390_abrmenge_3_txt = DaHelper.GetString(dr, "W_390_abrmenge_3_txt");
                obj.W_400_off_peak_txt = DaHelper.GetString(dr, "W_400_off_peak_txt");
                obj.W_410_ene_txt = DaHelper.GetString(dr, "W_410_ene_txt");
                obj.W_415_device_4 = DaHelper.GetString(dr, "W_415_device_4");
                obj.W_420_zwstand_4_txt = DaHelper.GetString(dr, "W_420_zwstand_4_txt");
                obj.W_430_zwstvor_4_txt = DaHelper.GetString(dr, "W_430_zwstvor_4_txt");
                obj.W_440_umwfakt_4_txt = DaHelper.GetString(dr, "W_440_umwfakt_4_txt");
                obj.W_450_abrmenge_4_txt = DaHelper.GetString(dr, "W_450_abrmenge_4_txt");
                obj.W_460_hol_txt = DaHelper.GetString(dr, "W_460_hol_txt");
                obj.W_470_zwstand_1_txt = DaHelper.GetString(dr, "W_470_zwstand_1_txt");
                obj.W_480_zwstvor_1_txt = DaHelper.GetString(dr, "W_480_zwstvor_1_txt");
                obj.W_490_consumption_txt = DaHelper.GetString(dr, "W_490_consumption_txt");
                obj.W_500_txt6 = DaHelper.GetString(dr, "W_500_txt6");
                obj.W_510_mr_kw_6_1_txt = DaHelper.GetString(dr, "W_510_mr_kW_6_1_txt");
                obj.W_520_mr_kw_6_2_txt = DaHelper.GetString(dr, "W_520_mr_kW_6_2_txt");
                obj.W_530_mr_kw_6_3_txt = DaHelper.GetString(dr, "W_530_mr_kW_6_3_txt");
                obj.W_540_mr_kw_6_4_txt = DaHelper.GetString(dr, "W_540_mr_kW_6_4_txt");
                obj.W_550_mr_kw_6_5 = DaHelper.GetString(dr, "W_550_mr_kW_6_5");
                obj.W_555_device_6_7 = DaHelper.GetString(dr, "W_555_device_6_7");
                obj.W_560_mr_kw_7_1_txt = DaHelper.GetString(dr, "W_560_mr_kW_7_1_txt");
                obj.W_570_mr_kw_7_2_txt = DaHelper.GetString(dr, "W_570_mr_kW_7_2_txt");
                obj.W_580_mr_kw_7_3_txt = DaHelper.GetString(dr, "W_580_mr_kW_7_3_txt");
                obj.W_590_mr_kw_7_4_txt = DaHelper.GetString(dr, "W_590_mr_kW_7_4_txt");
                obj.W_600_mr_kw_7_5 = DaHelper.GetString(dr, "W_600_mr_kW_7_5");
                obj.W_610_mr_kw_8_1_txt = DaHelper.GetString(dr, "W_610_mr_kW_8_1_txt");
                obj.W_620_mr_kw_8_2_txt = DaHelper.GetString(dr, "W_620_mr_kW_8_2_txt");
                obj.W_630_mr_kw_8_3_txt = DaHelper.GetString(dr, "W_630_mr_kW_8_3_txt");
                obj.W_635_mr_kw_8_4_txt = DaHelper.GetString(dr, "W_635_mr_kW_8_4_txt");
                obj.W_640_mr_kw_8_5 = DaHelper.GetString(dr, "W_640_mr_kW_8_5");
                obj.W_650_mr_kw_9_1_txt = DaHelper.GetString(dr, "W_650_mr_kW_9_1_txt");
                obj.W_660_mr_kw_9_2_txt = DaHelper.GetString(dr, "W_660_mr_kW_9_2_txt");
                obj.W_670_mr_kw_9_3_txt = DaHelper.GetString(dr, "W_670_mr_kW_9_3_txt");
                obj.W_680_mr_kw_9_4_txt = DaHelper.GetString(dr, "W_680_mr_kW_9_4_txt");
                obj.W_690_mr_kw_9_5 = DaHelper.GetString(dr, "W_690_mr_kW_9_5");
                obj.W_695_device_9_7 = DaHelper.GetString(dr, "W_695_device_9_7");
                obj.W_700_mr_kw_10_1_txt = DaHelper.GetString(dr, "W_700_mr_kW_10_1_txt");
                obj.W_710_mr_kw_10_2_txt = DaHelper.GetString(dr, "W_710_mr_kW_10_2_txt");
                obj.W_720_mr_kw_10_3_txt = DaHelper.GetString(dr, "W_720_mr_kW_10_3_txt");
                obj.W_730_mr_kw_10_4_txt = DaHelper.GetString(dr, "W_730_mr_kW_10_4_txt");
                obj.W_740_mr_kw_10_5 = DaHelper.GetString(dr, "W_740_mr_kW_10_5");
                obj.W_750_mr_kw_11_1_txt = DaHelper.GetString(dr, "W_750_mr_kW_11_1_txt");
                obj.W_760_mr_kw_11_2_txt = DaHelper.GetString(dr, "W_760_mr_kW_11_2_txt");
                obj.W_770_mr_kw_11_3_txt = DaHelper.GetString(dr, "W_770_mr_kW_11_3_txt");
                obj.W_775_mr_kw_11_4_txt = DaHelper.GetString(dr, "W_775_mr_kW_11_4_txt");
                obj.W_780_mr_kw_11_5 = DaHelper.GetString(dr, "W_780_mr_kW_11_5");
                obj.W_790_mr_kw_12_1_txt = DaHelper.GetString(dr, "W_790_mr_kW_12_1_txt");
                obj.W_800_mr_kw_12_2_txt = DaHelper.GetString(dr, "W_800_mr_kW_12_2_txt");
                obj.W_810_mr_kw_12_3_txt = DaHelper.GetString(dr, "W_810_mr_kW_12_3_txt");
                obj.W_820_mr_kw_12_4_txt = DaHelper.GetString(dr, "W_820_mr_kW_12_4_txt");
                obj.W_830_mr_kw_12_5 = DaHelper.GetString(dr, "W_830_mr_kW_12_5");
                obj.W_835_device_12_7 = DaHelper.GetString(dr, "W_835_device_12_7");
                obj.W_840_mr_kw_13_1_txt = DaHelper.GetString(dr, "W_840_mr_kW_13_1_txt");
                obj.W_850_mr_kw_13_2_txt = DaHelper.GetString(dr, "W_850_mr_kW_13_2_txt");
                obj.W_860_mr_kw_13_3_txt = DaHelper.GetString(dr, "W_860_mr_kW_13_3_txt");
                obj.W_870_mr_kw_13_4_txt = DaHelper.GetString(dr, "W_870_mr_kW_13_4_txt");
                obj.W_890_mr_kw_13_5 = DaHelper.GetString(dr, "W_890_mr_kW_13_5");
                obj.W_900_mr_kw_14_1_txt = DaHelper.GetString(dr, "W_900_mr_kW_14_1_txt");
                obj.W_910_mr_kw_14_2_txt = DaHelper.GetString(dr, "W_910_mr_kW_14_2_txt");
                obj.W_920_mr_kw_14_3_txt = DaHelper.GetString(dr, "W_920_mr_kW_14_3_txt");
                obj.W_925_mr_kw_14_4_txt = DaHelper.GetString(dr, "W_925_mr_kW_14_4_txt");
                obj.W_930_mr_kw_14_5 = DaHelper.GetString(dr, "W_930_mr_kW_14_5");
                obj.W_940_mr_kw_15_1_txt = DaHelper.GetString(dr, "W_940_mr_kW_15_1_txt");
                obj.W_950_mr_kw_15_2_txt = DaHelper.GetString(dr, "W_950_mr_kW_15_2_txt");
                obj.W_960_mr_kw_15_3_txt = DaHelper.GetString(dr, "W_960_mr_kW_15_3_txt");
                obj.W_970_mr_kw_15_4_txt = DaHelper.GetString(dr, "W_970_mr_kW_15_4_txt");
                obj.W_980_mr_kw_15_5 = DaHelper.GetString(dr, "W_980_mr_kW_15_5");
                obj.W_985_device_15_7 = DaHelper.GetString(dr, "W_985_device_15_7");
                obj.W_990_mr_kw_16_1_txt = DaHelper.GetString(dr, "W_990_mr_kW_16_1_txt");
                obj.W_1000_mr_kw_16_2_txt = DaHelper.GetString(dr, "W_1000_mr_kW_16_2_txt");
                obj.W_1010_mr_kw_16_3_txt = DaHelper.GetString(dr, "W_1010_mr_kW_16_3_txt");
                obj.W_1020_mr_kw_16_4_txt = DaHelper.GetString(dr, "W_1020_mr_kW_16_4_txt");
                obj.W_1030_mr_kw_16_5 = DaHelper.GetString(dr, "W_1030_mr_kW_16_5");
                obj.W_1040_mr_kw_17_1_txt = DaHelper.GetString(dr, "W_1040_mr_kW_17_1_txt");
                obj.W_1050_mr_kw_17_2_txt = DaHelper.GetString(dr, "W_1050_mr_kW_17_2_txt");
                obj.W_1060_mr_kw_17_3_txt = DaHelper.GetString(dr, "W_1060_mr_kW_17_3_txt");
                obj.W_1065_mr_kw_17_4_txt = DaHelper.GetString(dr, "W_1065_mr_kW_17_4_txt");
                obj.W_1070_mr_kw_17_5 = DaHelper.GetString(dr, "W_1070_mr_kW_17_5");
                obj.W_1080_service_txt = DaHelper.GetString(dr, "W_1080_service_txt");
                obj.W_1090_service_support_txt = DaHelper.GetString(dr, "W_1090_service_support_txt");
                obj.W_1100_sum_service_support_txt = DaHelper.GetString(dr, "W_1100_sum_service_support_txt");
                obj.W_1110_basic_19_1_txt = DaHelper.GetString(dr, "W_1110_basic_19_1_txt");
                obj.W_1120_description = DaHelper.GetString(dr, "W_1120_description");
                obj.W_1130_kvar_20_1_txt = DaHelper.GetString(dr, "W_1130_kvar_20_1_txt");
                obj.W_1140_kvar_20_2_txt = DaHelper.GetString(dr, "W_1140_kvar_20_2_txt");
                obj.W_1150_kvar_20_3_txt = DaHelper.GetString(dr, "W_1150_kvar_20_3_txt");
                obj.W_1160_kvar_20_4_txt = DaHelper.GetString(dr, "W_1160_kvar_20_4_txt");
                obj.W_1170_kvar_21_1_txt = DaHelper.GetString(dr, "W_1170_kvar_21_1_txt");
                obj.W_1180_kvar_21_2_txt = DaHelper.GetString(dr, "W_1180_kvar_21_2_txt");
                obj.W_1190_kvar_21_3_txt = DaHelper.GetString(dr, "W_1190_kvar_21_3_txt");
                obj.W_1200_kvar_21_4_txt = DaHelper.GetString(dr, "W_1200_kvar_21_4_txt");
                obj.W_1210_gen_kw_amt_txt = DaHelper.GetString(dr, "W_1210_gen_kW_amt_txt");
                obj.W_1220_trans_kw_amt_txt = DaHelper.GetString(dr, "W_1220_trans_kW_amt_txt");
                obj.W_1230_dist_kw_amt_txt = DaHelper.GetString(dr, "W_1230_dist_kW_amt_txt");
                obj.W_1240_gen_kwh_amt_txt = DaHelper.GetString(dr, "W_1240_gen_kwh_amt_txt");
                obj.W_1250_trans_kwh_amt_txt = DaHelper.GetString(dr, "W_1250_trans_kwh_amt_txt");
                obj.W_1260_dist_kwh_amt_txt = DaHelper.GetString(dr, "W_1260_dist_kwh_amt_txt");
                obj.W_1270_dis_supp_amt_txt = DaHelper.GetString(dr, "W_1270_dis_supp_amt_txt");
                obj.W_1280_gen_ft_amt_txt = DaHelper.GetString(dr, "W_1280_gen_ft_amt_txt");
                obj.W_1290_trans_ft_amt_txt = DaHelper.GetString(dr, "W_1290_trans_ft_amt_txt");
                obj.W_1300_dist_ft_amt_txt = DaHelper.GetString(dr, "W_1300_dist_ft_amt_txt");
                obj.W_1310_amount_txt = DaHelper.GetString(dr, "W_1310_amount_txt");
                obj.W_1320_foreign_amt = DaHelper.GetString(dr, "W_1320_foreign_amt");
                obj.W_1330_foreign_txt = DaHelper.GetString(dr, "W_1330_foreign_txt");
                obj.W_1340_foreign_uit = DaHelper.GetString(dr, "W_1340_foreign_uit");
                obj.W_1345_blue_txt1 = DaHelper.GetString(dr, "W_1345_blue_txt1");
                obj.W_1350_ftgen_txt = DaHelper.GetString(dr, "W_1350_ftgen_txt");
                obj.W_1360_fttrn_txt = DaHelper.GetString(dr, "W_1360_fttrn_txt");
                obj.W_1370_ftdis_txt = DaHelper.GetString(dr, "W_1370_ftdis_txt");
                obj.W_1380_fttot_txt = DaHelper.GetString(dr, "W_1380_fttot_txt");
                obj.W_1381_ft_peiod_txt = DaHelper.GetString(dr, "W_1381_ft_peiod_txt");
                obj.W_1390_ftunit_txt = DaHelper.GetString(dr, "W_1390_ftunit_txt");
                obj.W_1400_ftchg_txt = DaHelper.GetString(dr, "W_1400_ftchg_txt");
                obj.W_1410_basic_14_6_txt = DaHelper.GetString(dr, "W_1410_basic_14_6_txt");
                obj.W_1420_amin_14_7 = DaHelper.GetString(dr, "W_1420_amin_14_7");
                obj.W_1430_ft_basic_txt = DaHelper.GetString(dr, "W_1430_ft_basic_txt");
                obj.W_1440_power_txt = DaHelper.GetString(dr, "W_1440_power_txt");
                obj.W_1450_mr_kw_17_6_txt = DaHelper.GetString(dr, "W_1450_mr_kW_17_6_txt");
                obj.W_1460_mr_kw_17_7 = DaHelper.GetString(dr, "W_1460_mr_kW_17_7");
                obj.W_1470_baht = DaHelper.GetString(dr, "W_1470_baht");
                obj.W_1480_net_before_vat_txt = DaHelper.GetString(dr, "W_1480_net_before_vat_txt");
                obj.W_1485_net_before_vat_left = DaHelper.GetString(dr, "W_1485_net_before_vat_left");
                obj.W_1490_tax_rate_txt = DaHelper.GetString(dr, "W_1490_tax_rate_txt");
                obj.W_1500_tax_amount_txt = DaHelper.GetString(dr, "W_1500_tax_amount_txt");
                obj.W_1505_tax_amount_left = DaHelper.GetString(dr, "W_1505_tax_amount_left");
                obj.W_1510_total_amnt_txt = DaHelper.GetString(dr, "W_1510_total_amnt_txt");
                obj.W_1550_case_text1 = DaHelper.GetString(dr, "W_1550_case_text1");
                obj.W_1550_case_text2 = DaHelper.GetString(dr, "W_1550_case_text2");
                obj.W_1550_case_text3 = DaHelper.GetString(dr, "W_1550_case_text3");
                obj.W_1550_case_text4 = DaHelper.GetString(dr, "W_1550_case_text4");
                obj.W_1550_case_text5 = DaHelper.GetString(dr, "W_1550_case_text5");
                obj.W_1550_case_text6 = DaHelper.GetString(dr, "W_1550_case_text6");
                obj.W_1550_case_text7 = DaHelper.GetString(dr, "W_1550_case_text7");
                obj.W_1550_case_text8 = DaHelper.GetString(dr, "W_1550_case_text8");
                obj.W_1560_spell_amount = DaHelper.GetString(dr, "W_1560_spell_amount");
                obj.W_1570_account_number = DaHelper.GetString(dr, "W_1570_account_number");
                obj.W_1580_payment_due_date = DaHelper.GetString(dr, "W_1580_payment_due_date");
                obj.W_1581_bank_due_date = DaHelper.GetString(dr, "W_1581_bank_due_date");
                obj.W_1590_barcode1 = DaHelper.GetString(dr, "W_1590_barcode1");
                obj.W_1600_barcode2 = DaHelper.GetString(dr, "W_1600_barcode2");
                obj.W_1830_payment_method = DaHelper.GetString(dr, "W_1830_payment_method");
                obj.W_1840_mru = DaHelper.GetString(dr, "W_1840_mru");
                obj.W_1841_mru_full = DaHelper.GetString(dr, "W_1841_mru_full");
                obj.W_1850_adjust_amt = DaHelper.GetString(dr, "W_1850_adjust_amt");
                obj.W_1851_adjust_amt = DaHelper.GetString(dr, "W_1851_adjust_amt");
                obj.W_1860_trsg = DaHelper.GetString(dr, "W_1860_trsg");
                obj.W_1861_crsg = DaHelper.GetString(dr, "W_1861_crsg");
                obj.W_1880_payment_lot = DaHelper.GetString(dr, "W_1880_payment_lot");
                obj.W_1890_payer = DaHelper.GetString(dr, "W_1890_payer");
                obj.W_1900_absorb_amt_mod = DaHelper.GetString(dr, "W_1900_absorb_amt_mod");
                obj.W_1901_discount_amt_pea = DaHelper.GetString(dr, "W_1901_discount_amt_pea");
                obj.W_1902_absorb_qty = DaHelper.GetString(dr, "W_1902_absorb_qty");
                obj.W_1910_org_doc = DaHelper.GetString(dr, "W_1910_org_doc");
                obj.W_1911_reverse = DaHelper.GetString(dr, "W_1911_reverse");
                obj.W_1950_collector_id = DaHelper.GetString(dr, "W_1950_collector_id");
                obj.W_1960_acct_class = DaHelper.GetString(dr, "W_1960_acct_class");
                obj.W_1970_print_dt = DaHelper.GetString(dr, "W_1970_print_dt");
                obj.W_1971_print_time = DaHelper.GetDate(dr, "W_1971_print_time");
                obj.W_1980_spotbill = DaHelper.GetString(dr, "W_1980_spotbill");
                obj.W_1990_addition = DaHelper.GetString(dr, "W_1990_addition");
                obj.W_2000_dispctrl = DaHelper.GetString(dr, "W_2000_dispctrl");
                obj.W_2010_noprnt = DaHelper.GetString(dr, "W_2010_noprnt");
                obj.W_2020_noprnt_txt = DaHelper.GetString(dr, "W_2020_noprnt_txt");
                obj.W_2030_barcode_a4 = DaHelper.GetString(dr, "W_2030_barcode_a4");
                obj.W_2040_portion = DaHelper.GetString(dr, "W_2040_portion");
                obj.W_2050_mdenr = DaHelper.GetString(dr, "W_2050_mdenr");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                obj.CreatedDt = DaHelper.GetDate(dr, "CreatedDt");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                obj.FileName = DaHelper.GetString(dr, "FileName");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                retVals.Add(obj);
            }
            dt.Dispose();
            return retVals;
        }
            catch (Exception e)
            {
                throw;
            }
        }
        //ts.PrintHistory
        public List<PrintHistoryInfo> GetUpdatePrintHistory(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<PrintHistoryInfo> retVals = new List<PrintHistoryInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_PrintHistory");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //ts.DeliveryHistory
        public List<DeliveryHistoryInfo> GetUpdateDeliveryHistory(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<DeliveryHistoryInfo> retVals = new List<DeliveryHistoryInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_DeliveryHistory");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //ts.PrintPool
        public List<PrintPoolInfo> GetUpdatePrintPool(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<PrintPoolInfo> retVals = new List<PrintPoolInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_PrintPool");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //ts.GrpPrintPool
        public List<GrpPrintPoolInfo> GetUpdateGrpPrintPool(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<GrpPrintPoolInfo> retVals = new List<GrpPrintPoolInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_GrpPrintPool");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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

                    obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                    obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;

                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //ts.GreenReceiptDetail
        public List<GreenReceiptDetailInfo> GetUpdateGreenReceiptDetail(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<GreenReceiptDetailInfo> retVals = new List<GreenReceiptDetailInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_GreenReceiptDetail");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //ts.GreenReceiptPrintHistory
        //public List<GreenReceiptPrintHistoryInfo> GetUpdateGreenReceiptPrintHistory(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        //{
        //    try
        //    {
        //        List<GreenReceiptPrintHistoryInfo> retVals = new List<GreenReceiptPrintHistoryInfo>();

        //        Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
        //        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_GreenReceiptPrintHistory");
        //        db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
        //        db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
        //        db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
        //        DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            GreenReceiptPrintHistoryInfo obj = new GreenReceiptPrintHistoryInfo();
        //            obj.ReceiptNo = DaHelper.GetString(dr, "ReceiptNo");
        //            obj.PrintBranch = DaHelper.GetString(dr, "PrintBranch");
        //            obj.PrintLog = DaHelper.GetInt(dr, "PrintLog");
        //            obj.PrintDate = DaHelper.GetDate(dr, "PrintDate");
        //            obj.BillSeqNo = DaHelper.GetInt(dr, "BillSeqNo");
        //            obj.BranchId = DaHelper.GetString(dr, "BranchId");
        //            obj.MruId = DaHelper.GetString(dr, "MruId");
        //            obj.ReceiptPeriod = DaHelper.GetString(dr, "ReceiptPeriod");
        //            obj.CaId = DaHelper.GetString(dr, "CaId");
        //            obj.BankId = DaHelper.GetString(dr, "BankId");
        //            obj.OrgDoc = DaHelper.GetString(dr, "OrgDoc");
        //            obj.PostDt = DaHelper.GetDate(dr, "PostDt");
        //            obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
        //            obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
        //            obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
        //            obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
        //            obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
        //            retVals.Add(obj);
        //        }
        //        dt.Dispose();
        //        return retVals;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
        //ta.MaxBillSeqNo
        public List<MaxBillSeqNoInfo> GetUpdateMaxBillSeqNo(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<MaxBillSeqNoInfo> retVals = new List<MaxBillSeqNoInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_MaxBillSeqNo");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }        
        //mt.Approver
        public List<ApproverInfo> GetUpdateApprover(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<ApproverInfo> retVals = new List<ApproverInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_Approver");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //mt.DeliveryPlace
        public List<DeliveryPlaceInfo> GetUpdateDeliveryPlace(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<DeliveryPlaceInfo> retVals = new List<DeliveryPlaceInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_DeliveryPlace");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //mt.BillStatus
        public List<PWBBillStatusInfo> GetUpdatePWBBillStatus(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<PWBBillStatusInfo> retVals = new List<PWBBillStatusInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_PWBBillStatus");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    PWBBillStatusInfo obj = new PWBBillStatusInfo();
                    obj.BillPred = DaHelper.GetString(dr, "BillPred");
                    obj.BranchId = DaHelper.GetString(dr, "BranchId");
                    obj.MruId = DaHelper.GetString(dr, "MruId");
                    obj.Portion = DaHelper.GetString(dr, "Portion");
                    obj.ReaderNo = DaHelper.GetString(dr, "ReaderNo");
                    obj.BranchName = DaHelper.GetString(dr, "BranchName");
                    obj.CaBlue = DaHelper.GetInt(dr, "CaBlue");
                    obj.CaGreen = DaHelper.GetInt(dr, "CaGreen");
                    obj.CaA4 = DaHelper.GetInt(dr, "CaA4");
                    obj.CaSpotBill = DaHelper.GetInt(dr, "CaSpotBill");
                    obj.CaGrpInv = DaHelper.GetInt(dr, "CaGrpInv");
                    obj.CaTypeF = DaHelper.GetInt(dr, "CaTypeF");
                    obj.CaOther = DaHelper.GetInt(dr, "CaOther");
                    obj.CaTotal = DaHelper.GetInt(dr, "CaTotal");
                    obj.TotPrintBlue = DaHelper.GetInt(dr, "TotPrintBlue");
                    obj.NoPrintBlue = DaHelper.GetInt(dr, "NoPrintBlue");
                    obj.TotPrintGreen = DaHelper.GetInt(dr, "TotPrintGreen");
                    obj.NoPrintGreen = DaHelper.GetInt(dr, "NoPrintGreen");
                    obj.TotPrintA4 = DaHelper.GetInt(dr, "TotPrintA4");
                    obj.NoPrintA4 = DaHelper.GetInt(dr, "NoPrintA4");
                    obj.TotPrintSpotBill = DaHelper.GetInt(dr, "TotPrintSpotBill");
                    obj.NoPrintSpotBill = DaHelper.GetInt(dr, "NoPrintSpotBill");
                    obj.TotPrintGrpInv = DaHelper.GetInt(dr, "TotPrintGrpInv");
                    obj.NoPrintGrpInv = DaHelper.GetInt(dr, "NoPrintGrpInv");
                    obj.TotPrintTypeF = DaHelper.GetInt(dr, "TotPrintTypeF");
                    obj.NoPrintTypeF = DaHelper.GetInt(dr, "NoPrintTypeF");
                    obj.AnyOther = DaHelper.GetInt(dr, "AnyOther");
                    obj.TotalPrint = DaHelper.GetInt(dr, "TotalPrint");
                    obj.TotalNoPrint = DaHelper.GetInt(dr, "TotalNoPrint");
                    obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                    obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    obj.Action = DaHelper.GetString(dr, "Action");

                    //obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //ta.BillUpdate
        public List<BillUpdateInfo> GetUpdateBillUpdate(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<BillUpdateInfo> retVals = new List<BillUpdateInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_BillUpdate");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    BillUpdateInfo obj = new BillUpdateInfo();
                    obj.PrintDoc = DaHelper.GetString(dr, "PrintDoc");
                    obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    obj.Crsg = DaHelper.GetString(dr, "Crsg");
                    obj.Trsg = DaHelper.GetString(dr, "Trsg");
                    obj.MruId = DaHelper.GetString(dr, "MruId");
                    obj.CaId = DaHelper.GetString(dr, "CaId");
                    obj.PmId = DaHelper.GetString(dr, "PmId");
                    obj.ReceiptNo = DaHelper.GetString(dr, "ReceiptNo");
                    obj.BankId = DaHelper.GetString(dr, "BankId");
                    obj.BankDueDate = DaHelper.GetString(dr, "BankDueDate");
                    obj.MtNo = DaHelper.GetString(dr, "MtNo");
                    obj.GrpInvPaymentDueDate = DaHelper.GetString(dr, "GrpInvPaymentDueDate");
                    obj.GroupingDate = DaHelper.GetString(dr, "GroupingDate");
                    obj.FileName = DaHelper.GetString(dr, "FileName");
                    obj.Action = DaHelper.GetString(dr, "Action");

                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }            
        }
        //mt.BarcodeMRU
        public List<BarcodeMRUInfo> GetUpdateBarcodeMRU(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<BarcodeMRUInfo> retVals = new List<BarcodeMRUInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_BarcodeMRU");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<BillStatusInfo> GetUpdateBillStatus(string branchid, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<BillStatusInfo> retVals = new List<BillStatusInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_BillStatus");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchid);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }        
        #endregion

        #region Download ar data DL40 from BPM

        public List<AR> GetUpdateAR(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<AR> retVals = new List<AR>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_AR");
                cmd.CommandTimeout = 10000;
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<DisconnectionDocInfo> GetUpdateDisconnectionDoc(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<DisconnectionDocInfo> retVals = new List<DisconnectionDocInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_DisconnectionDoc");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    DisconnectionDocInfo obj = new DisconnectionDocInfo();
                    obj.DiscNo = DaHelper.GetString(dr, "DiscNo");
                    obj.CreatedDt = DaHelper.GetDate(dr, "CreatedDt");
                    obj.DiscStatusId = DaHelper.GetString(dr, "DiscStatusId");
                    obj.ReleaseDt = DaHelper.GetDate(dr, "ReleaseDt");
                    obj.WorkOrderNo = DaHelper.GetString(dr, "WorkOrderNo");
                    obj.DiscPlanStart = DaHelper.GetDate(dr, "DiscPlanStart");
                    obj.DiscPlanEnd = DaHelper.GetDate(dr, "DiscPlanEnd");
                    obj.WorkCenter = DaHelper.GetString(dr, "WorkCenter");
                    obj.PostpConfirm = DaHelper.GetDate(dr, "PostpConfirm");
                    obj.FuseRemConfirm = DaHelper.GetDate(dr, "FuseRemConfirm");
                    obj.MeterRemConfirm = DaHelper.GetDate(dr, "MeterRemConfirm");
                    obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                    obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");

                    obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;


                    retVals.Add(obj);
                }

                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<RTDisconnectionDocCaDocInfo> GetUpdateRTDisconnectionDocCaDoc(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<RTDisconnectionDocCaDocInfo> retVals = new List<RTDisconnectionDocCaDocInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_RTDisconnectionDocCaDoc");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    RTDisconnectionDocCaDocInfo obj = new RTDisconnectionDocCaDocInfo();
                    obj.DiscNo = DaHelper.GetString(dr, "DiscNo");
                    obj.CaDocNo = DaHelper.GetString(dr, "CaDocNo");
                    obj.CancelFlag = DaHelper.GetString(dr, "CancelFlag");
                    obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                    obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");

                    obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    retVals.Add(obj);
                }
                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<APInfo> GetUpdateAP(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<APInfo> retVals = new List<APInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_AP");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                    retVals.Add(obj);
                }

                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<APChequePaymentInfo> GetUpdateAPChequePayment(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            try
            {
                List<APChequePaymentInfo> retVals = new List<APChequePaymentInfo>();

                Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_APChequePayment");
                db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
                db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                    obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                    obj.PostDt = DaHelper.GetDate(dr, "PostDt");
                    obj.PostBranchServerId = DaHelper.GetString(dr, "PostBranchServerId");
                    obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                    obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                    obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                    retVals.Add(obj);
                }

                dt.Dispose();
                return retVals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region Download payfromsap data DL50 from BPM

        public List<Payment> GetUpdatePayment(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<Payment> retVals = new List<Payment>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_Payment");
            cmd.CommandTimeout = 10000;
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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

                retVals.Add(obj);
            }

            return retVals;
        }

        public List<ARPaymentType> GetUpdateARPaymentType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<ARPaymentType> retVals = new List<ARPaymentType>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_ARPaymentType");
            cmd.CommandTimeout = 10000;
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                retVals.Add(obj);
            }

            return retVals;
        }

        public List<ARPayment> GetUpdateARPayment(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            //return StartProcessARPaymentTableJoining(branchId, lastModifiedDate, serverDate);
            List<ARPayment> retVals = new List<ARPayment>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_ARPayment");
            cmd.CommandTimeout = 10000;
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                retVals.Add(obj);
            }

            return retVals;
        }
        
        public List<RTARPaymentTypeARPayment> GetUpdateRTARPaymentTypeARPayment(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<RTARPaymentTypeARPayment> retVals = new List<RTARPaymentTypeARPayment>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_RTARPaymentTypeARPayment");
            cmd.CommandTimeout = 10000;
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                retVals.Add(obj);
            }

            return retVals;
        }

        public List<Receipt> GetUpdateReceipt(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<Receipt> retVals = new List<Receipt>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_Receipt");
            cmd.CommandTimeout = 10000;
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                retVals.Add(obj);
            }

            return retVals;
        }

        public List<ReceiptItem> GetUpdateReceiptItem(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<ReceiptItem> retVals = new List<ReceiptItem>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_ReceiptItem");
            cmd.CommandTimeout = 10000;
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                retVals.Add(obj);
            }

            return retVals;
        }

        #endregion

        #region Download cash management data DL51 from BPM

        public List<CashierWorkStatusInfo> GetUpdateCashierWorkStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<CashierWorkStatusInfo> retVals = new List<CashierWorkStatusInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_CashierWorkStatus");
            cmd.CommandTimeout = 10000;
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                retVals.Add(obj);
            }

            return retVals;
        }

        public List<CashierMoneyTransferInfo> GetUpdateCashierMoneyTransfer(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<CashierMoneyTransferInfo> retVals = new List<CashierMoneyTransferInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_CashierMoneyTransfer");
            cmd.CommandTimeout = 10000;
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                retVals.Add(obj);
            }

            return retVals;
        }

        public List<CashierMoneyFlowInfo> GetUpdateCashierMoneyFlow(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<CashierMoneyFlowInfo> retVals = new List<CashierMoneyFlowInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_CashierMoneyFlow");
            cmd.CommandTimeout = 10000;
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                retVals.Add(obj);
            }

            return retVals;
        }

        public List<CashierMoneyFlowItemInfo> GetUpdateCashierMoneyFlowItem(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<CashierMoneyFlowItemInfo> retVals = new List<CashierMoneyFlowItemInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_CashierMoneyFlowItem");
            cmd.CommandTimeout = 10000;
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                retVals.Add(obj);
            }

            return retVals;
        }

        #endregion

        #region Download agency data DL60 from BPM

        public List<BillBookInfo> GetUpdateBillBook(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<BillBookInfo> retVals = new List<BillBookInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_BillBook");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                retVals.Add(obj);
            }

            return retVals;
        }

        public List<BillStatusInfoInfo> GetUpdateBillStatusInfo(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<BillStatusInfoInfo> retVals = new List<BillStatusInfoInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_BillStatusInfo");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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

                retVals.Add(obj);
            }

            return retVals;
        }

        public List<BillBookDetailInfo> GetUpdateBillBookDetail(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<BillBookDetailInfo> retVals = new List<BillBookDetailInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_BillBookDetail");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                
                retVals.Add(obj);
            }

            return retVals;
        }

        public List<BillBookInputItemInfo> GetUpdateBillBookInputItem(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<BillBookInputItemInfo> retVals = new List<BillBookInputItemInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_BillBookInputItem");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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

                retVals.Add(obj);
            }

            return retVals;
        }

        public List<BillBookInputSetInfo> GetUpdateBillBookInputSet(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<BillBookInputSetInfo> retVals = new List<BillBookInputSetInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_BillBookInputSet");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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

                retVals.Add(obj);
            }

            return retVals;
        }

        public List<AgencyCommissionInfo> GetUpdateAgencyCommission(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<AgencyCommissionInfo> retVals = new List<AgencyCommissionInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_AgencyCommission");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                
                retVals.Add(obj);
            }

            return retVals;
        }

        public List<RTAgencyContractMruInfo> GetUpdateRTAgencyContractMru(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<RTAgencyContractMruInfo> retVals = new List<RTAgencyContractMruInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_RTAgencyContractMru");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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

                retVals.Add(obj);
            }

            return retVals;
        }

        public List<RTAgencyCommissionBillBookInfo> GetUpdateRTAgencyCommissionBillBook(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<RTAgencyCommissionBillBookInfo> retVals = new List<RTAgencyCommissionBillBookInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_RTAgencyCommissionBillBook");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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

                retVals.Add(obj);
            }

            return retVals;
        }

        //discharge
        //public List<AgencyModuleConfigInfo> GetUpdateAgencyModuleConfig(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        //{
        //    List<AgencyModuleConfigInfo> retVals = new List<AgencyModuleConfigInfo>();

        //    Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
        //    DbCommand cmd = db.GetStoredProcCommand("ta_syncs_AgencyModuleConfig");
        //    db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
        //    db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
        //    db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
        //    DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        AgencyModuleConfigInfo obj = new AgencyModuleConfigInfo();

        //        obj.DebitAcc = DaHelper.GetString(dr, "DebitAcc");
        //        obj.CmCountBlock = DaHelper.GetString(dr, "CmCountBlock");
        //        obj.CmCountLimit = DaHelper.GetInt(dr, "CmCountLimit");
        //        obj.VatRate = DaHelper.GetDecimal(dr, "VatRate");
        //        obj.CollectedPercent = DaHelper.GetDecimal(dr, "CollectedPercent");
        //        obj.CaCount = DaHelper.GetInt(dr, "CaCount");
        //        obj.UpperRate = DaHelper.GetDecimal(dr, "UpperRate");
        //        obj.LowerRate = DaHelper.GetDecimal(dr, "LowerRate");
        //        obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
        //        obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
        //        obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
        //        obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;

        //        retVals.Add(obj);
        //    }

        //    return retVals;
        //}

        #endregion

        #region Download technical data DL70 from BPM

        public List<ServiceInfo> DownloadService(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<ServiceInfo> rsList = new List<ServiceInfo>();
            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_Service");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                ServiceInfo obj = new ServiceInfo();
                obj.SvcId = DaHelper.GetString(dr, "SvcId");
                obj.Module = DaHelper.GetString(dr, "Module");
                obj.SvcName = DaHelper.GetString(dr, "SvcName");
                obj.MethodName = DaHelper.GetString(dr, "MethodName");
                obj.Description = DaHelper.GetString(dr, "Description");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false; ;
                rsList.Add(obj);
            }

            return rsList;
        }

        public List<FunctionInfo> DownloadFunction(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<FunctionInfo> rsList = new List<FunctionInfo>();
            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_Function");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                FunctionInfo obj = new FunctionInfo();
                obj.FncId = DaHelper.GetString(dr, "FncId");
                obj.Module = DaHelper.GetString(dr, "Module");
                obj.FncName = DaHelper.GetString(dr, "FncName");
                obj.SubFncName = DaHelper.GetString(dr, "SubFncName");
                obj.Description = DaHelper.GetString(dr, "Description");
                obj.UnlockableFlag = DaHelper.GetString(dr, "UnlockableFlag");
                obj.UnlockRemarkFlag = DaHelper.GetString(dr, "UnlockRemarkFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false; ;
                rsList.Add(obj);
            }

            return rsList;
        }

        public List<RoleInfo> DownloadRole(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<RoleInfo> rsList = new List<RoleInfo>();
            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_Role");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                RoleInfo obj = new RoleInfo();
                obj.RoleId = DaHelper.GetString(dr, "RoleId");
                obj.RoleName = DaHelper.GetString(dr, "RoleName");
                obj.Description = DaHelper.GetString(dr, "Description");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false; ;
                rsList.Add(obj);
            }

            return rsList;
        }

        public List<UserInfo> DownloadUser(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<UserInfo> rsList = new List<UserInfo>();
            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_User");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                rsList.Add(obj);
            }

            return rsList;
        }

        public List<FunctionServiceInfo> DownloadFunctionService(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<FunctionServiceInfo> rsList = new List<FunctionServiceInfo>();
            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_FunctionService");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                FunctionServiceInfo obj = new FunctionServiceInfo();
                obj.RTId = DaHelper.GetString(dr, "RTId");
                obj.FncId = DaHelper.GetString(dr, "FncId");
                obj.SvcId = DaHelper.GetString(dr, "SvcId");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                rsList.Add(obj);
            }

            return rsList;
        }

        public List<RoleFunctionInfo> DownloadRoleFunction(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<RoleFunctionInfo> rsList = new List<RoleFunctionInfo>();
            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_RoleFunction");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                RoleFunctionInfo obj = new RoleFunctionInfo();
                obj.RTId = DaHelper.GetString(dr, "RTId");
                obj.RoleId = DaHelper.GetString(dr, "RoleId");
                obj.FncId = DaHelper.GetString(dr, "FncId");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                rsList.Add(obj);
            }

            return rsList;
        }

        public List<RoleUserInfo> DownloadRoleUser(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<RoleUserInfo> rsList = new List<RoleUserInfo>();
            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_RoleUser");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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

        public List<UnlockLogInfo> DownloadUnlockLog(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<UnlockLogInfo> rsList = new List<UnlockLogInfo>();
            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_UnlockLog");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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

        public List<AppSettingInfo> DownloadAppSetting(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<AppSettingInfo> rsList = new List<AppSettingInfo>();
            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_AppSetting");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                AppSettingInfo obj = new AppSettingInfo();
                obj.SettingCode = DaHelper.GetString(dr, "SettingCode");
                obj.SettingValue = DaHelper.GetString(dr, "SettingValue");
                obj.Module = DaHelper.GetString(dr, "Module");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                rsList.Add(obj);
            }

            return rsList;
        }

        public List<Terminal> DownloadTerminal(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<Terminal> rsList = new List<Terminal>();
            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_Terminal");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Terminal obj = new Terminal();
                obj.TerminalId = DaHelper.GetString(dr, "TerminalId");
                obj.TerminalCode = DaHelper.GetString(dr, "TerminalCode");
                obj.BranchId = DaHelper.GetString(dr, "BranchId");
                obj.TaxCode = DaHelper.GetString(dr, "TaxCode");
                obj.IP = DaHelper.GetString(dr, "IP");
                obj.SyncFlag = DaHelper.GetString(dr, "SyncFlag");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;

                rsList.Add(obj);
            }

            return rsList;
        }

        public List<UnlockRemarkInfo> DownloadUnlockRemark(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<UnlockRemarkInfo> list = new List<UnlockRemarkInfo>();
            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_UnlockRemark");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                UnlockRemarkInfo obj = new UnlockRemarkInfo();
                obj.FncId = DaHelper.GetString(dr, "FncId");
                obj.RemarkId = DaHelper.GetString(dr, "RemarkId");
                obj.Description = DaHelper.GetString(dr, "Description");
                obj.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                obj.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");

                obj.Active = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                list.Add(obj);
            }

            return list;
        }

        #endregion

        #region Download ePayment data DL61 from BPM

        public List<EPayClarifyInfo> GetUpdateEPayClearify(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<EPayClarifyInfo> retVals = new List<EPayClarifyInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_EPayClarify");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                retVals.Add(obj);
            }

            return retVals;
        }

        public List<EPayUploadInfo> GetUpdateEPayUpload(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<EPayUploadInfo> retVals = new List<EPayUploadInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_EPayUpload");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                retVals.Add(obj);
            }

            return retVals;
        }

        public List<EPayUploadItemInfo> GetUpdateEPayUploadItem(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<EPayUploadItemInfo> retVals = new List<EPayUploadItemInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_EPayUploadItem");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                retVals.Add(obj);
            }

            return retVals;
        }

        public List<RTEPayUploadPaymentInfo> GetUpdateRTEPayUploadPayment(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            List<RTEPayUploadPaymentInfo> retVals = new List<RTEPayUploadPaymentInfo>();

            Database db = DatabaseFactory.CreateDatabase(DOWNLOADED_DB_NAME);
            DbCommand cmd = db.GetStoredProcCommand("ta_syncs_RTEPayUploadPayment");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pLastModifiedDt", DbType.DateTime, lastModifiedDate);
            db.AddInParameter(cmd, "pServerDate", DbType.DateTime, serverDate);
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
                retVals.Add(obj);
            }

            return retVals;
        }

        #endregion

        #region Update BPM Server (MRUPlan) from branch

        public int UploadMRUPlan(List<MRUPlanInfo> arList, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (MRUPlanInfo obj in arList)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveMRUPlan");
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
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        #endregion

        #region Update BPM Server (AR) from branch

        public int UploadAR(List<AR> arList, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (AR obj in arList)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveAR");
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
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        //db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pCreatedDt", DbType.DateTime, obj.CreatedDt);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);
                        db.AddInParameter(cmd, "pFileName", DbType.String, obj.FileName);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);

                    }

                    arList = null;
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadAP(List<APInfo> arList, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (APInfo obj in arList)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveAP");
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
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        //db.AddInParameter(cmd, "pExportedOnce", DbType.String, obj.ExportedOnce);
                        db.AddInParameter(cmd, "pPaidFlag", DbType.String, obj.PaidFlag);
                        db.AddInParameter(cmd, "pCanceledFlag", DbType.String, obj.CanceledFlag);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    arList = null;
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadAPChequePayment(List<APChequePaymentInfo> arList, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (APChequePaymentInfo obj in arList)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveAPChequePayment");
                        db.AddInParameter(cmd, "pAPId", DbType.String, obj.APId);
                        db.AddInParameter(cmd, "pChqItemId", DbType.String, obj.ChqItemId);
                        db.AddInParameter(cmd, "pBankKey", DbType.String, obj.BankKey);
                        db.AddInParameter(cmd, "pBankName", DbType.String, obj.BankName);
                        db.AddInParameter(cmd, "pChqAccNo", DbType.String, obj.ChqAccNo);
                        db.AddInParameter(cmd, "pChqNo", DbType.String, obj.ChqNo);
                        db.AddInParameter(cmd, "pChqDt", DbType.DateTime, obj.ChqDt);
                        db.AddInParameter(cmd, "pChqAmt", DbType.Decimal, obj.ChqAmt);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    arList = null;
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        #endregion

        #region Update BPM Server(Payment) from branch
        
        public int UploadARPayment(List<ARPayment> arPaymentList, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    GC.Collect();
                    foreach (ARPayment obj in arPaymentList)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveARPayment");
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
                        db.AddInParameter(cmd, "pPaidChannel", DbType.Byte, obj.PaidChannel);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pTechBranchId", DbType.String, obj.TechBranchId);
                        db.AddInParameter(cmd, "pCommBranchId", DbType.String, obj.CommBranchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    arPaymentList = null;                    
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadPayment(List<Payment> paymentList, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (Payment obj in paymentList)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SavePayment");
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
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    paymentList = null;
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadARPaymentType(List<ARPaymentType> arPaymentTypeList, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (ARPaymentType obj in arPaymentTypeList)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveARPaymentType");
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
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    arPaymentTypeList = null;
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadRTARPaymentTypeARPayment(List<RTARPaymentTypeARPayment> rtList, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (RTARPaymentTypeARPayment obj in rtList)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveRTARPaymentTypeARPayment");
                        db.AddInParameter(cmd, "pARPtId", DbType.String, obj.ARPtId);
                        db.AddInParameter(cmd, "pARPmId", DbType.String, obj.ARPmId);
                        db.AddInParameter(cmd, "pAmount", DbType.Decimal, obj.Amount);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pTechBranchId", DbType.String, obj.TechBranchId);
                        db.AddInParameter(cmd, "pCommBranchId", DbType.String, obj.CommBranchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    rtList = null;
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadReceipt(List<Receipt> receiptList, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (Receipt obj in receiptList)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveReceipt");
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
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pPaymentBranchId", DbType.String, obj.PaymentBranchId);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");

                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    receiptList = null;
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadReceiptItem(List<ReceiptItem> receiptList, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (ReceiptItem obj in receiptList)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveReceiptItem");
                        db.AddInParameter(cmd, "pReceiptId", DbType.String, obj.ReceiptId);
                        db.AddInParameter(cmd, "pARPmId", DbType.String, obj.ARPmId);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pTechBranchId", DbType.String, obj.TechBranchId);
                        db.AddInParameter(cmd, "pCommBranchId", DbType.String, obj.CommBranchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    receiptList = null;
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadPaymentLog(List<PaymentLogInfo> receiptList, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (PaymentLogInfo obj in receiptList)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SavePaymentLog");
                        db.AddInParameter(cmd, "pCorrectedId", DbType.String, obj.CorrectedId);
                        db.AddInParameter(cmd, "pCorrectedPairId", DbType.String, obj.CorrectedPairId);
                        db.AddInParameter(cmd, "pCorrectedCaseCode", DbType.String, obj.CorrectedCaseCode);
                        db.AddInParameter(cmd, "pCorrectedStage", DbType.Byte, obj.CorrectedStage);
                        db.AddInParameter(cmd, "pCorrectedBy", DbType.String, obj.CorrectedBy);
                        db.AddInParameter(cmd, "pCorrectedDt", DbType.DateTime, obj.CorrectedDt);
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
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }
                    receiptList = null;
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        #endregion

        #region Update BPM Server(Cash Management) from branch

        public int UploadCashierWorkStatus(List<CashierWorkStatusInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (CashierWorkStatusInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveCashierWorkStatus");
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
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadCashierMoneyTransfer(List<CashierMoneyTransferInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (CashierMoneyTransferInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveCashierMoneyTransfer");
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
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadCashierMoneyFlow(List<CashierMoneyFlowInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (CashierMoneyFlowInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveCashierMoneyFlow");
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
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadCashierMoneyFlowItem(List<CashierMoneyFlowItemInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (CashierMoneyFlowItemInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveCashierMoneyFlowItem");
                        db.AddInParameter(cmd, "pFlowId", DbType.String, obj.FlowId);
                        db.AddInParameter(cmd, "pChqItemId", DbType.String, obj.ChqItemId);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pBankKey", DbType.String, obj.BankKey);
                        db.AddInParameter(cmd, "pChqNo", DbType.String, obj.ChqNo);
                        db.AddInParameter(cmd, "pChqAccNo", DbType.String, obj.ChqAccNo);
                        db.AddInParameter(cmd, "pChqDt", DbType.DateTime, obj.ChqDt);
                        db.AddInParameter(cmd, "pAmount", DbType.Decimal, obj.Amount);
                        db.AddInParameter(cmd, "pValidFlag", DbType.String, obj.ValidFlag);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        #endregion

        #region Update BPM Server(Agency) from branch

        public int UploadBillBook(List<BillBookInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (BillBookInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveBillBook");
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
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadBillStatusInfo(List<BillStatusInfoInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (BillStatusInfoInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveBillStatusInfo");
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
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadBillBookDetail(List<BillBookDetailInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (BillBookDetailInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveBillBookDetail");
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
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadBillBookInputItem(List<BillBookInputItemInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (BillBookInputItemInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveBillBookInputItem");
                        db.AddInParameter(cmd, "pBIpItemId", DbType.String, obj.BIpItemId);
                        db.AddInParameter(cmd, "pFilterType", DbType.String, obj.FilterType);
                        db.AddInParameter(cmd, "pBillBookId", DbType.String, obj.BillBookId);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pMruId", DbType.String, obj.MruId);
                        db.AddInParameter(cmd, "pCaId", DbType.String, obj.CaId);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadBillBookInputSet(List<BillBookInputSetInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (BillBookInputSetInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveBillBookInputSet");
                        db.AddInParameter(cmd, "pBIpSetId", DbType.String, obj.BIpSetId);
                        db.AddInParameter(cmd, "pBillBookId", DbType.String, obj.BillBookId);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pMruId", DbType.String, obj.MruId);
                        db.AddInParameter(cmd, "pBillPeriodType", DbType.String, obj.BillPeriodType);
                        db.AddInParameter(cmd, "pBillOutType", DbType.String, obj.BillOutType);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadAgencyCommission(List<AgencyCommissionInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (AgencyCommissionInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveAgencyCommission");
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
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadRTAgencyContractMru(List<RTAgencyContractMruInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (RTAgencyContractMruInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveRTAgencyContractMru");
                        db.AddInParameter(cmd, "pAgentMruId", DbType.String, obj.AgentMruId);
                        db.AddInParameter(cmd, "pCaId", DbType.String, obj.CaId);
                        db.AddInParameter(cmd, "pCaName", DbType.String, obj.CaName);
                        db.AddInParameter(cmd, "pCaBranchId", DbType.String, obj.CaBranchId);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pBranchName", DbType.String, obj.BranchName);
                        db.AddInParameter(cmd, "pMruId", DbType.String, obj.MruId);
                        db.AddInParameter(cmd, "pSecurityDeposit", DbType.Decimal, obj.SecurityDeposit);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadRTAgencyCommissionBillBook(List<RTAgencyCommissionBillBookInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (RTAgencyCommissionBillBookInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveRTAgencyCommissionBillBook");
                        db.AddInParameter(cmd, "pCmId", DbType.String, obj.CmId);
                        db.AddInParameter(cmd, "pBillBookId", DbType.String, obj.BillBookId);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime,obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        #endregion

        #region Update BPM Server(TA) from branch

        public int UploadRoleUser(List<RoleUserInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (RoleUserInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveRoleUser");
                        db.AddInParameter(cmd, "pRTId", DbType.String, obj.RTId);
                        db.AddInParameter(cmd, "pRoleId", DbType.String, obj.RoleId);
                        db.AddInParameter(cmd, "pUserId", DbType.String, obj.UserId);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active);
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                        
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadUser(List<UserInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (UserInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveUser");
                        db.AddInParameter(cmd, "pUserId", DbType.String, obj.UserId);
                        db.AddInParameter(cmd, "pPassword", DbType.String, obj.Password);
                        db.AddInParameter(cmd, "pFullName", DbType.String, obj.FullName);
                        db.AddInParameter(cmd, "pDepartment", DbType.String, obj.Department);
                        db.AddInParameter(cmd, "pToken", DbType.String, obj.Token);
                        db.AddInParameter(cmd, "pLastRequest", DbType.DateTime, obj.LastRequest);
                        db.AddInParameter(cmd, "pLastLogin", DbType.DateTime, obj.LastLogin);
                        db.AddInParameter(cmd, "pPwdExpiredDt", DbType.DateTime, obj.PwdExpiredDt);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pPermission", DbType.String, obj.Permission);
                        db.AddInParameter(cmd, "pCurIP", DbType.String, obj.CurIP);
                        db.AddInParameter(cmd, "pReqIP", DbType.String, obj.ReqIP);
                        db.AddInParameter(cmd, "pReqFlag", DbType.String, obj.ReqFlag);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
        }

        return lineSuccess;
    }

        public int UploadUnlockLog(List<UnlockLogInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (UnlockLogInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveUnlockLog");
                        db.AddInParameter(cmd, "pUnlockId", DbType.String, obj.UnlockId);
                        db.AddInParameter(cmd, "pFncId", DbType.String, obj.FncId);
                        db.AddInParameter(cmd, "pUnlockDt", DbType.DateTime, obj.UnlockDt);
                        db.AddInParameter(cmd, "pCurrentUserId", DbType.String, obj.CurrentUserId);
                        db.AddInParameter(cmd, "pUnlockUserId", DbType.String, obj.UnlockUserId);
                        db.AddInParameter(cmd, "pDescription", DbType.String, obj.Description);
                        db.AddInParameter(cmd, "pRemark", DbType.String, obj.Remark);
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pBranchName", DbType.String, obj.BranchName);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);                      
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        #endregion              

        #region Update BPM Server(Billing) from branch

        public int UploadPrintPool(List<PrintPoolInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (PrintPoolInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SavePrintPool");
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
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadGrpPrintPool(List<GrpPrintPoolInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (GrpPrintPoolInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveGrpPrintPool");
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
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadGreenReceiptDetail(List<GreenReceiptDetailInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (GreenReceiptDetailInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveGreenReceiptDetail");
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
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pCreatedDt", DbType.DateTime, obj.CreatedDt);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);
                        db.AddInParameter(cmd, "pFileName", DbType.String, obj.FileName);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        //public int UploadGreenReceiptPrintHistory(List<GreenReceiptPrintHistoryInfo> list, string branchId)
        //{
        //    Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

        //    int lineSuccess = 0;

        //    using (DbConnection conn = db.CreateConnection())
        //    {
        //        conn.Open();
        //        DbTransaction trans = conn.BeginTransaction();
        //        try
        //        {
        //            foreach (GreenReceiptPrintHistoryInfo obj in list)
        //            {
        //                DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveGreenReceiptPrintHistory");
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
        //                db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
        //                db.AddInParameter(cmd, "pSyncFlag", DbType.String, obj.SyncFlag);
        //                db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
        //                db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);
        //                db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
        //                lineSuccess += db.ExecuteNonQuery(cmd, trans);
        //            }

        //            trans.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            trans.Rollback();
        //            throw ex;
        //        }
        //    }

        //    return lineSuccess;
        //}

        public int UploadPrintHistory(List<PrintHistoryInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (PrintHistoryInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SavePrintHistory");
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
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadDeliveryHistory(List<DeliveryHistoryInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (DeliveryHistoryInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveDeliveryHistory");
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
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadMaxBillSeqNo(List<MaxBillSeqNoInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (MaxBillSeqNoInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveMaxBillSeqNo");
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pPrintType", DbType.Byte, obj.PrintType);
                        db.AddInParameter(cmd, "pMaxNo", DbType.Int32, obj.MaxNo);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadApprover(List<ApproverInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (ApproverInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveApprover");
                        db.AddInParameter(cmd, "pApproverId", DbType.String, obj.ApproverId);
                        db.AddInParameter(cmd, "pApproverName", DbType.String, obj.ApproverName);
                        db.AddInParameter(cmd, "pPosition", DbType.String, obj.Position);
                        db.AddInParameter(cmd, "pCreateBranchId", DbType.String, obj.CreateBranchId);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadDeliveryPlace(List<DeliveryPlaceInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (DeliveryPlaceInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveDeliveryPlace");
                        db.AddInParameter(cmd, "pDeliveryPlaceId", DbType.String, obj.DeliveryPlaceId);
                        db.AddInParameter(cmd, "pDeliveryBranchId", DbType.String, obj.DeliveryBranchId);
                        db.AddInParameter(cmd, "pDeliveryPlaceName", DbType.String, obj.DeliveryPlaceName);
                        db.AddInParameter(cmd, "pCreateBranchId", DbType.String, obj.CreateBranchId);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadBarcodeMRU(List<BarcodeMRUInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (BarcodeMRUInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveBarcodeMRU");
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pMruId", DbType.String, obj.MruId);
                        db.AddInParameter(cmd, "pIsPrinted", DbType.String, obj.IsPrinted);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadBillStatus(List<BillStatusInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (BillStatusInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveBillStatus");
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pMruId", DbType.String, obj.MruId);
                        db.AddInParameter(cmd, "pBillPred", DbType.String, obj.BillPred);
                        db.AddInParameter(cmd, "pBranchName", DbType.String, obj.BranchName);
                        db.AddInParameter(cmd, "pPortionId", DbType.String, obj.PortionId);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        #endregion        

        #region Update BPMServer(ePayment) from branch

        public int UploadEPayClearify(List<EPayClarifyInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (EPayClarifyInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveEPayClarify");
                        db.AddInParameter(cmd, "pIssueId", DbType.String, obj.IssueId);
                        db.AddInParameter(cmd, "pEPayItemId", DbType.String, obj.EPayItemId);
                        db.AddInParameter(cmd, "pInvoiceNo", DbType.String, obj.InvoiceNo);
                        db.AddInParameter(cmd, "pNewInvoiceNo", DbType.String, obj.NewInvoiceNo);
                        db.AddInParameter(cmd, "pReceiptInvoiceNo", DbType.String, obj.ReceiptInvoiceNo);
                        db.AddInParameter(cmd, "pFixedType", DbType.String, obj.FixedType);
                        db.AddInParameter(cmd, "pFixedDt", DbType.DateTime, obj.FixedDt);
                        db.AddInParameter(cmd, "pFixedBy", DbType.String, obj.FixedBy);
                        db.AddInParameter(cmd, "pRefDocNo", DbType.String, obj.RefDocNo);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadEPayUpload(List<EPayUploadInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (EPayUploadInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveEPayUpload");
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
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadEPayUploadItem(List<EPayUploadItemInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (EPayUploadItemInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveEPayUploadItem");
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
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        public int UploadRTEPayUploadPayment(List<RTEPayUploadPaymentInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (RTEPayUploadPaymentInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveRTEPayUploadPayment");
                        db.AddInParameter(cmd, "pUploadDt", DbType.DateTime, obj.UploadDt);
                        db.AddInParameter(cmd, "pCompanyId", DbType.String, obj.CompanyId);
                        db.AddInParameter(cmd, "pPaymentId", DbType.String, obj.PaymentId);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, obj.PostDt);
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);
                        db.AddInParameter(cmd, "pModifiedBy", DbType.String, obj.ModifiedBy);

                        db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        #endregion

        #region Update BPMServer(ExportTable) from branch

        public int UploadExportTransactionLog(List<ExportTransactionLogInfo> list, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase(UPLOADED_DB_NAME);

            int lineSuccess = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (ExportTransactionLogInfo obj in list)
                    {
                        DbCommand cmd = db.GetStoredProcCommand("ta_syncs_SaveExportTransactionLog");
                        db.AddInParameter(cmd, "pBranchId", DbType.String, obj.BranchId);
                        db.AddInParameter(cmd, "pCreatedDt", DbType.DateTime, obj.CreatedDt);
                        db.AddInParameter(cmd, "pExportType", DbType.String, obj.ExportType);
                        db.AddInParameter(cmd, "pItemId", DbType.String, obj.ItemId);
                        db.AddInParameter(cmd, "pARPmId", DbType.String, obj.ARPmId);
                        db.AddInParameter(cmd, "pARPtId", DbType.String, obj.ARPtId);
                        db.AddInParameter(cmd, "pPaymentId", DbType.String, obj.PaymentId);
                        db.AddInParameter(cmd, "pReceiptId", DbType.String, obj.ReceiptId);
                        db.AddInParameter(cmd, "pAPId", DbType.String, obj.APId);
                        db.AddInParameter(cmd, "pChqItemId", DbType.String, obj.ChqItemId);
                        db.AddInParameter(cmd, "pExportFlag", DbType.Int32, obj.ExportFlag);
                        db.AddInParameter(cmd, "pExportDt", DbType.DateTime, obj.ExportDt);
                        db.AddInParameter(cmd, "pWorkId", DbType.String, obj.WorkId);
                        db.AddInParameter(cmd, "pCloseWorkDt", DbType.DateTime, obj.CloseWorkDt);
                        db.AddInParameter(cmd, "pSyncFlag", DbType.String, "1");
                        db.AddInParameter(cmd, "pPostBranchServerId", DbType.String, branchId);
                        db.AddInParameter(cmd, "pPostDt", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "pModifiedDt", DbType.DateTime, obj.ModifiedDt);

                        //db.AddInParameter(cmd, "pActive", DbType.String, obj.Active == true ? "1" : "0");
                        lineSuccess += db.ExecuteNonQuery(cmd, trans);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return lineSuccess;
        }

        #endregion

        #region Enqueue Batch
        
        public void EnqueueBatchJob(string batchName, string destination, string[] param)
        {
            try
            {
                BatchExecutionRequest request = new BatchExecutionRequest(batchName);
                request.BatchClientName = "";
                request.Destination = destination;
                request.StartPollingForResultAfter = TimeSpan.FromMinutes(5);

                ParameterData parem = new ParameterData();
                parem.Name = "BranchId";
                parem.Value = param[0];
                request.Parameters.Add(parem);

                BatchExecutionRequestQueue queue = new BatchExecutionRequestQueue("ACABatch_SQL");
                queue.Enqueue(request);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion        

    }
}


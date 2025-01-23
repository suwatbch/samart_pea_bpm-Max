using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace PEA.BPM.Integration.ACABatchService
{
    public class ACABatchScheduler
    {
        public static DateTime GetLastModifiedDate(String entitiesName)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("ACABatch_SQL");
                DbCommand cmd = db.GetStoredProcCommand("dbo.ACA_INT_GetScheduler");
                db.AddInParameter(cmd, "pEntityname", DbType.String, entitiesName);
                db.AddInParameter(cmd, "pJobType", DbType.String, "DL");
                object result = db.ExecuteScalar(cmd);
                if (result == null)
                {
                    return DateTime.Now.AddYears(-10);
                }
                else
                {
                    return (DateTime)result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DateTime GetLastUploadedDate(String entitiesName)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("ACABatch_SQL");
                DbCommand cmd = db.GetStoredProcCommand("dbo.ACA_INT_GetScheduler");
                db.AddInParameter(cmd, "pEntityname", DbType.String, entitiesName);
                db.AddInParameter(cmd, "pJobType", DbType.String, "UL");
                object result = db.ExecuteScalar(cmd);
                if (result == null)
                {
                    return DateTime.Now.AddYears(-10);
                }
                else
                {
                    return (DateTime)result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool UpdateScheduler(Scheduler scheduler)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("ACABatch_SQL");
                DbCommand cmd = db.GetStoredProcCommand("dbo.ACA_INT_UpdScheduler");
                db.AddInParameter(cmd, "pEntityname", DbType.String, scheduler.EntityName);
                db.AddInParameter(cmd, "pJobType", DbType.String, scheduler.JobType);
                db.AddInParameter(cmd, "pLastUpdateDt", DbType.DateTime, scheduler.LastUpdateDt);
                db.AddInParameter(cmd, "pModifiedBy", DbType.String, "BATCH");
                int result = db.ExecuteNonQuery(cmd);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

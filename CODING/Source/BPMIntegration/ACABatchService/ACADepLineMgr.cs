using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Collections;

namespace PEA.BPM.Integration.ACABatchService
{
    public class ACADepLineMgr
    {
        //In used: DL01
        public bool IsErrorRemain(string dlId)
        {
            Database db = DatabaseFactory.CreateDatabase("ACABatch_SQL");
            DbCommand cmd = db.GetStoredProcCommand("dbo.ACA_INT_CheckError");
            db.AddInParameter(cmd, "DepLineId", DbType.String, dlId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            if (dt.Rows.Count == 0) return false;
            else return true;
        }

        //In used: DL01
        public ACADepLineInfo GetDepedencyLineStatus(string dlId)
        {
            try
            {
                ACADepLineInfo dep = new ACADepLineInfo();
                Database db = DatabaseFactory.CreateDatabase("ACABatch_SQL");
                DbCommand cmd = db.GetStoredProcCommand("dbo.ACA_INT_GetDepLine");
                db.AddInParameter(cmd, "DepLineId", DbType.String, dlId);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dep.DLId = dlId;
                    dep.Status = "0";
                }
                else
                {
                    dep.DLId = dt.Rows[0]["DepLineId"].ToString();
                    dep.FileKey = dt.Rows[0]["FileKey"].ToString();
                    dep.Status = dt.Rows[0]["Status"].ToString();
                }
                return dep;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<ACADepLineInfo> GetAllDLFailTb(string dlId)
        {
            try
            {
                List<ACADepLineInfo> depList = new List<ACADepLineInfo>();
                Database db = DatabaseFactory.CreateDatabase("ACABatch_SQL");
                DbCommand cmd = db.GetStoredProcCommand("dbo.ACA_INT_GetDepLineAll");
                db.AddInParameter(cmd, "DepLineId", DbType.String, dlId);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
               
                foreach (DataRow r in dt.Rows)
                {
                    ACADepLineInfo dep = new ACADepLineInfo();
                    dep.DLId = r["DepLineId"].ToString();
                    dep.FileKey = r["FileKey"].ToString();
                    dep.Status = r["Status"].ToString();
                    dep.LastFailTb = r["LastFailTb"].ToString();
                    depList.Add(dep);
                }

                return depList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Hashtable GetAllLastFailHt(string dlId)
        {
            try
            {
                Hashtable ht = new Hashtable();
                Database db = DatabaseFactory.CreateDatabase("ACABatch_SQL");
                DbCommand cmd = db.GetStoredProcCommand("dbo.ACA_INT_GetDepLineAll");
                db.AddInParameter(cmd, "DepLineId", DbType.String, dlId);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

                foreach (DataRow r in dt.Rows)
                {
                    string lastFailTb = r["LastFailTb"].ToString();
                    string fileKey = r["FileKey"].ToString();
                    ht.Add(lastFailTb, fileKey);
                }

                return ht;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //In used: ALL
        public List<string> GetDependencyLineTable(string dlId)
        {
            try
            {
                List<string> tbList = new List<string>();
                Database db = DatabaseFactory.CreateDatabase("ACABatch_SQL");
                DbCommand cmd = db.GetStoredProcCommand("dbo.ACA_INT_GetDepLineTable");
                db.AddInParameter(cmd, "DepLineId", DbType.String, dlId);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

                foreach (DataRow r in dt.Rows)
                {
                    tbList.Add(r["TableName"].ToString());
                }

                return tbList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //In used: DL01
        public void SetStatus(ACADepLineInfo depInfo, bool isUpd)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("ACABatch_SQL");
                DbCommand cmd = db.GetStoredProcCommand("dbo.ACA_INT_UpdDepLineStatus");
                db.AddInParameter(cmd, "DepLineId", DbType.String, depInfo.DLId);
                db.AddInParameter(cmd, "FileKey", DbType.String, depInfo.FileKey);
                db.AddInParameter(cmd, "Status", DbType.String, depInfo.Status);
                db.AddInParameter(cmd, "LastFailTb", DbType.String, depInfo.LastFailTb);
                db.AddInParameter(cmd, "IsUpdate", DbType.String, isUpd ? "1": "0");
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Used by ALL
        public void ResetLastFailTb(string dlId, string tb)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("ACABatch_SQL");
                DbCommand cmd = db.GetStoredProcCommand("dbo.ACA_INT_ResetDLStatus");
                db.AddInParameter(cmd, "DepLineId", DbType.String, dlId);
                db.AddInParameter(cmd, "LastFailTb", DbType.String, tb);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InQueue(string batchKey, string dlName)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("ACABatch_SQL");
                DbCommand cmd = db.GetStoredProcCommand("dbo.ACA_INT_InQueue");
                db.AddInParameter(cmd, "DepLine", DbType.String, dlName);
                db.AddInParameter(cmd, "BatchKey", DbType.String, batchKey);
                int? ret = (int?)db.ExecuteScalar(cmd);

                return ret == 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Threading;
using System.Security.Principal;
using System.Diagnostics;

namespace Avanade.ACA.Batch
{
    /// <summary>
    /// Class for manage false tolerance in execution server to dequeue batch 
    /// </summary>
    public class BatchExecutionServerSwitcher
    {
        #region Constants
        private const string ACA_BATCH_DATABASE = "ACABatch_SQL";
        private const string ENABLE_FALSE_TOLERANCE = "ENABLE_FALSE_TOLERANCE";
        private const string LATENCY_SECOND = "LATENCY_SECOND";
        #endregion

        #region Global variables
        Database _db;
        string _batchName;
        string[] _destFilter;
        #endregion

        #region Constructor
        public BatchExecutionServerSwitcher(Database db, string[] destFilter)
        {
            _db = db;
            _destFilter = destFilter;
        }
        #endregion Constructor

        #region Method
        /// <summary>
        /// Manage Executing Server/False Tolalance by using config in database
        /// </summary>
        /// <returns>True=Dequeue, False=Not Dequeue</returns>
        public bool ExecuteServer()
        {
            #region Prepare "DestinationFilter" parameter
            StringCollection nonEmptyDestination = new StringCollection();
            foreach (string dest in _destFilter)
            {
                string realDest = dest.Trim();
                if (realDest.Length > 0)
                {
                    nonEmptyDestination.Add(realDest);
                }
            }

            StringBuilder builder = new StringBuilder();
            int length = nonEmptyDestination.Count;
            for (int i = 0; ; )
            {
                string destination = nonEmptyDestination[i];
                builder.Append(destination);

                i++;

                if (i != length)
                {
                    builder.Append(',');
                }
                else
                {
                    break;
                }
            }
            #endregion Prepare "DestinationFilter" parameter

            #region Declare variables
            //Variables for get queue
            string destinationFilter = builder.ToString();
            Guid batchQueueKey;

            // Variables for check latest execution
            int execServId = 0;
            string execServName;
            Guid execBatchQueueKey;
            DateTime executedDt;

            // Variables for check updated of execution
            int newExecServId = 0;
            string newExecServName;
            Guid newExecBatchQuueueKey;
            DateTime newExecutedDt;

            //Variables for config
            int servPriority = 0;
            bool enableExecFalseTolalance = false;
            bool enablePriorityMode = false;
            bool enableProgressLog = false;
            int syncCycleInterval = 0;
            int timeOut = 0;
            int numberOfServer = 2;

            DbTransaction trans;
            #endregion Declare variables

            if (ConfigurationManager.ConnectionStrings[ACA_BATCH_DATABASE] == null)
                return false;

            using (DbConnection conn = _db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                try
                {

                    DefaultBatchManager.GetExecServerConfig(_db, trans,
                        out enableExecFalseTolalance,
                        out enablePriorityMode,
                        out enableProgressLog,
                        out syncCycleInterval,
                        out timeOut,
                        out numberOfServer);

                    if (!enableExecFalseTolalance) return true;

                    if (!DefaultBatchManager.GetBatchQueue(_db, trans,
                        destinationFilter,
                        out batchQueueKey,
                        out _batchName))
                    {
                        trans.Commit();
                        conn.Close();
                        return false;
                    }

                    DefaultBatchManager.InsertExecServerSystem(_db, trans,
                        Environment.MachineName);

                    servPriority = DefaultBatchManager.GetExecServerSystemPriority(_db, trans,
                        Environment.MachineName);

                    DefaultBatchManager.InsertExecServer(_db, trans,
                        Environment.MachineName,
                        _batchName,
                        new Guid());

                    trans.Commit();
                    trans = conn.BeginTransaction();

                    DefaultBatchManager.GetExecServer(_db, trans,
                        _batchName,
                        out execServId,
                        out execServName,
                        out execBatchQueueKey,
                        out executedDt);

                    DataTable dt0 = new DataTable();
                    dt0 = DefaultBatchManager.GetExecServerSync(_db, trans,
                        execServId);
                    if (dt0.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt0.Rows)
                        {
                            if ((string)dr[BatchDbConstants.Parm.EXEC_SERV_EXECUTION_SERVER_NAME] == Environment.MachineName)
                            {
                                SaveLog(enableProgressLog, _db, Environment.MachineName, _batchName,
                                    "Duplicate sync server name. -> Exit");
                                trans.Commit();
                                conn.Close();
                                return false;
                            }
                        }
                    }

                    if (batchQueueKey == execBatchQueueKey)
                    {
                        SaveLog(enableProgressLog, _db, Environment.MachineName, _batchName,
                            "Batch Queue Key [" + batchQueueKey + "] has been dequeued -> Exit");
                        trans.Commit();
                        conn.Close();
                        return false;
                    }

                    DefaultBatchManager.UpdateExecServerSync(_db, trans,
                        execServId,
                         Environment.MachineName,
                        true);

                    trans.Commit();
                    trans = conn.BeginTransaction();

                    int numberOfCycle = timeOut / syncCycleInterval;

                    //CHECK EXECUTION SERVER LOOP
                    for (int i = 0; i <= numberOfCycle; i++)
                    {
                        DefaultBatchManager.GetExecServer(_db, trans,
                                  _batchName,
                                  out newExecServId,
                                  out newExecServName,
                                  out newExecBatchQuueueKey,
                                  out newExecutedDt);
                        if (executedDt < newExecutedDt)
                        {
                            SaveLog(enableProgressLog, _db, Environment.MachineName, _batchName,
                                    "Enqueued by '" + newExecServName + "' server. -> Exit");
                            trans.Commit();
                            conn.Close();
                            return false;
                        }

                        DataTable dt = new DataTable();
                        dt = DefaultBatchManager.GetExecServerSync(_db, trans,
                            execServId);

                        trans.Commit();
                        trans = conn.BeginTransaction();

                        //Check number of server that running is equal in config
                        if (dt.Rows.Count == numberOfServer)
                        {
                            if (enablePriorityMode)
                            {
                                DataRow[] dr = dt.Select(String.Format("{0} = min({0})", BatchDbConstants.Parm.EXEC_SERV_PRIORITY));

                                if (servPriority == (int)dr[0][BatchDbConstants.Parm.EXEC_SERV_PRIORITY])
                                {
                                    DefaultBatchManager.UpdateExecServer(_db, trans,
                                                execServId,
                                                Environment.MachineName,
                                                batchQueueKey);

                                    DefaultBatchManager.ClearExecServerSync(_db, trans,
                                        execServId);


                                    SaveLog(enableProgressLog, _db, Environment.MachineName, _batchName,
                                        "Top of priority. -> Dequeue");
                                    trans.Commit();
                                    conn.Close();
                                    return true;
                                }
                                else
                                {
                                    SaveLog(enableProgressLog, _db, Environment.MachineName, _batchName,
                                        "Lower priority. -> Exit");
                                    trans.Commit();
                                    conn.Close();
                                    return false;

                                }
                            }
                            else
                            {
                                if (execServName == Environment.MachineName)
                                {
                                    DefaultBatchManager.UpdateExecServer(_db, trans,
                                           execServId,
                                           Environment.MachineName,
                                           batchQueueKey);

                                    DefaultBatchManager.ClearExecServerSync(_db, trans,
                                        execServId);

                                    SaveLog(enableProgressLog, _db, Environment.MachineName, _batchName,
                                        "Last execution is this server. -> Dequeue");
                                    trans.Commit();
                                    conn.Close();
                                    return true;
                                }
                                else
                                {
                                    SaveLog(enableProgressLog, _db, Environment.MachineName, _batchName,
                                        "Last execution is '" + execServName + "' server. -> Exit");
                                    trans.Commit();
                                    conn.Close();
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                string syncExecServName = (string)dr[BatchDbConstants.Parm.EXEC_SERV_EXECUTION_SERVER_NAME];
                                int syncExecServPriority = (int)dr[BatchDbConstants.Parm.EXEC_SERV_PRIORITY];
                                if (enablePriorityMode)
                                {
                                    if (syncExecServName != Environment.MachineName
                                        && syncExecServPriority > servPriority)
                                    {
                                        SaveLog(enableProgressLog, _db, Environment.MachineName, _batchName,
                                            "This server is lower priotity. -> Exit");
                                        trans.Commit();
                                        conn.Close();
                                        return false;
                                    }
                                }
                                else
                                {
                                    if (syncExecServName != Environment.MachineName
                                        && syncExecServName == execServName)
                                    {
                                        SaveLog(enableProgressLog, _db, Environment.MachineName, _batchName,
                                            "This server is not last execution server. -> Exit");
                                        trans.Commit();
                                        conn.Close();
                                        return false;
                                    }
                                }
                            }
                        }
                        Thread.Sleep(syncCycleInterval);
                    }

                    //Time Out
                    DefaultBatchManager.UpdateExecServer(_db, trans,
                        execServId,
                        Environment.MachineName,
                        batchQueueKey);

                    DefaultBatchManager.ClearExecServerSync(_db, trans,
                               execServId);
                    SaveLog(enableProgressLog, _db, Environment.MachineName, _batchName,
                        "Waiting for other server execution is timeout. -> Dequeue");
                    trans.Commit();
                    conn.Close();
                    return true;
                }
                catch (Exception e)
                {
                    SaveLog(enableProgressLog, _db, Environment.MachineName, _batchName,
                        "Exception: " + e.Source + " | " + e.TargetSite + " | " + e.Message + " | " + e.StackTrace);
                    trans.Rollback();
                    conn.Close();
                    return false;
                }

            }

        }

        /// <summary>
        /// Saving log for execution server histories.
        /// </summary>
        /// <param name="enableLog">Enable to use loging</param>
        /// <param name="db">Database</param>
        /// <param name="execServerName">Execution Server Name</param>
        /// <param name="batchName">Batch Name</param>
        /// <param name="errMessage">Message</param>
        private void SaveLog(bool enableLog, Database db, string execServerName, string batchName, string message)
        {
            try
            {
                if (enableLog)
                {
                    DefaultBatchManager.InsertExecLog(
                        db,
                        execServerName,
                        batchName,
                        message);
                }
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("BPM Batch Execution Server", e.Message + e.StackTrace, EventLogEntryType.Error);
                throw e;
            }
        }

        #endregion Method
    }
}

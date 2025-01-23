using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Configuration;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Integration.BPMIntegration.SG;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;

namespace PEA.BPM.Integration.ACABatchService
{
    public class ACABatchLogger
    {
        private const string S_SOURCE = "BPM Online";
        private const string S_LOG = "Application";
        private const string S_SOURCE_NOTIFICATION = "BPM Notification";
        private const string S_LOG_NOTIFICATION = "BPM Notification";
         private const string WS_BPM_ADDR = "WS_BPM_ADDR";

        private static string _moduleName;
        private static ACABatchLogger _instance;
        private BpmNotificationSG sg;
        
        private string _interfaceId = "N/A";
        private int _severity = 1;
        private string _branchId = "Center";

        public enum LogType
        {
            FileValidation=1,
            ProcessData,
            ProcessingError,
            Success
        }

        public string InterfaceId
        {
            get { return _interfaceId; }
            set { _interfaceId = value; }
        }

        public int Severity
        {
            get { return _severity; }
            set { _severity = value; }
        }

        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        public ACABatchLogger()
        {
            if (!EventLog.SourceExists(S_SOURCE))
                EventLog.CreateEventSource(S_SOURCE, S_LOG);
            if (!EventLog.SourceExists(S_SOURCE_NOTIFICATION))
                EventLog.CreateEventSource(S_SOURCE_NOTIFICATION, S_LOG_NOTIFICATION);
            sg = new BpmNotificationSG();
        }

        public static ACABatchLogger GetInstance()
        {
            if (_instance == null)
                _instance = new ACABatchLogger();

            return _instance;
        }

        public static ACABatchLogger GetInstance(string moduleName)
        {
            _moduleName = moduleName;
            if (_instance == null)
                _instance = new ACABatchLogger();

            return _instance;
        }

        public void WriteLog(Guid batchKey, LogType errType, string msg)
        {
            try
            {
                if (batchKey == Guid.Empty)
                {
                    if (errType == LogType.Success)
                        EventLog.WriteEntry(S_SOURCE, msg, EventLogEntryType.Information);
                    else
                        EventLog.WriteEntry(S_SOURCE, msg, EventLogEntryType.Error);
                }
                else 
                    InsertErrorLog(batchKey, GetErrorType(errType), _moduleName, msg, "N/A", 0);
            }
            catch (Exception)
            {
                throw;
            }
            try
            {
                if (errType == LogType.ProcessingError)
                {
                    msg = msg == null ? String.Empty : msg;
                    _moduleName = _moduleName == null ? String.Empty : _moduleName;
                    _interfaceId = _interfaceId == null ? String.Empty : _interfaceId;
                    _branchId = _branchId == null ? String.Empty : _branchId;
                    if (batchKey == null || batchKey == Guid.Empty)
                    {
                        string emptyKey = string.Empty;
                        sg.SendNotificationToEmail(emptyKey, msg, _severity, _moduleName, _interfaceId, 0, _branchId, "N/A");
                    }
                    else
                        sg.SendNotificationToEmail(batchKey.ToString(), msg, _severity, _moduleName, _interfaceId, 0, _branchId, "N/A");
                }
            }
            catch (Exception ex)
            {
                InsertNotificationErrorLog(ex.Message);
            }
        }

        public void WriteLog(Guid batchKey, LogType errType, string msg, int errLine)
        {
            try
            {
                if (batchKey == Guid.Empty)
                {
                    if (errType == LogType.Success)
                        EventLog.WriteEntry(S_SOURCE,string.Format("Message: {0}, Line: {1} ", msg, errLine.ToString()), EventLogEntryType.Information);
                    else
                        EventLog.WriteEntry(S_SOURCE,string.Format("Message: {0}, Line: {1} ", msg, errLine.ToString()), EventLogEntryType.Error);
                }
                else 
                    InsertErrorLog(batchKey, GetErrorType(errType), _moduleName, msg, "N/A", errLine);
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                if (errType == LogType.ProcessingError)
                {
                    msg = msg == null ? String.Empty : msg;
                    _moduleName = _moduleName == null ? String.Empty : _moduleName;
                    _interfaceId = _interfaceId == null ? String.Empty : _interfaceId;
                    _branchId = _branchId == null ? String.Empty : _branchId;
                    if (batchKey == null || batchKey == Guid.Empty)
                    {
                        string emptyKey = string.Empty;
                        sg.SendNotificationToEmail(emptyKey, msg, _severity, _moduleName, _interfaceId, errLine, _branchId, "N/A");
                    }
                    else
                        sg.SendNotificationToEmail(batchKey.ToString(), msg, _severity, _moduleName, _interfaceId, errLine, _branchId, "N/A");
                }
            }
            catch (Exception ex)
            {
                InsertNotificationErrorLog(ex.Message);
            }
        }

        public void WriteLog(Guid batchKey, LogType errType, string msg, string suggestion)
        {
            try
            {
                if (batchKey == Guid.Empty)
                {
                    if (errType == LogType.Success)
                        EventLog.WriteEntry(S_SOURCE,string.Format("Message: {0}, Suggestion: {1} ", msg, suggestion), EventLogEntryType.Information);
                    else
                        EventLog.WriteEntry(S_SOURCE,string.Format("Message: {0}, Suggestion: {1} ", msg, suggestion), EventLogEntryType.Error);
                }
                else 
                    InsertErrorLog(batchKey, GetErrorType(errType), _moduleName, msg, suggestion, 0);
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                if (errType == LogType.ProcessingError)
                {
                    msg = msg == null ? String.Empty : msg;
                    _moduleName = _moduleName == null ? String.Empty : _moduleName;
                    _interfaceId = _interfaceId == null ? String.Empty : _interfaceId;
                    _branchId = _branchId == null ? String.Empty : _branchId;
                    if (batchKey == null || batchKey == Guid.Empty)
                    {
                        string emptyKey = string.Empty;
                        sg.SendNotificationToEmail(emptyKey, msg, _severity, _moduleName, _interfaceId, 0, _branchId, suggestion);
                    }
                    else
                        sg.SendNotificationToEmail(batchKey.ToString(), msg, _severity, _moduleName, _interfaceId, 0, _branchId, suggestion);
                }
            }
            catch (Exception ex)
            {
                InsertNotificationErrorLog(ex.Message);
            }
        }

        public void WriteLog(Guid batchKey, LogType errType, string msg, string suggestion, int errLine)
        {
            try
            {
                if (batchKey == Guid.Empty)
                {
                    if (errType == LogType.Success)
                        EventLog.WriteEntry(S_SOURCE,string.Format("Message: {0}, Suggestion: {1}, Line: {2} ", msg, suggestion, errLine.ToString()), EventLogEntryType.Information);
                    else
                        EventLog.WriteEntry(S_SOURCE,string.Format("Message: {0}, Suggestion: {1}, Line: {2} ", msg, suggestion, errLine.ToString()), EventLogEntryType.Error);
                }
                else 
                    InsertErrorLog(batchKey, GetErrorType(errType), _moduleName, msg, suggestion, errLine);
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                if (errType == LogType.ProcessingError)
                {
                    msg = msg == null ? String.Empty : msg;
                    _moduleName = _moduleName == null ? String.Empty : _moduleName;
                    _interfaceId = _interfaceId == null ? String.Empty : _interfaceId;
                    _branchId = _branchId == null ? String.Empty : _branchId;
                    if (batchKey == null || batchKey == Guid.Empty)
                    {
                        string emptyKey = string.Empty;
                        sg.SendNotificationToEmail(emptyKey, msg, _severity, _moduleName, _interfaceId, errLine, _branchId, suggestion);
                    }
                    else
                        sg.SendNotificationToEmail(batchKey.ToString(), msg, _severity, _moduleName, _interfaceId, errLine, _branchId, suggestion);
                }
            }
            catch (Exception ex)
            {
                InsertNotificationErrorLog(ex.Message);
            }
        }

        public bool HandleLog(Guid batchKey, string type, string infName, string entityName, DateTime localDt, DateTime serverDt, int rows)
        {
            bool result = false;            

            if (type == "0")
            {
                WriteLog(batchKey, ACABatchLogger.LogType.ProcessData,
                    string.Format("Getting Data Completed {0}", infName),
                    string.Format("Total rows = {0}",rows.ToString()));
                result = true;
            }
            else if (type == "1")
            {
                WriteLog(batchKey, ACABatchLogger.LogType.Success,
                    string.Format("Updating Data Completed {0}", infName),
                    string.Format("Total rows = {0}", rows.ToString()), rows);

                Scheduler sch = new Scheduler();
                sch.EntityName = entityName;
                sch.LastUpdateDt = localDt;
                sch.JobType = "UL";
                result = ACABatchScheduler.UpdateScheduler(sch);
            }
            else if (type == "2")
            {
                WriteLog(batchKey, ACABatchLogger.LogType.Success,
                    string.Format("There is 0 record to upload at {0}", serverDt),
                    infName);
                result = true;
            }
            else if (type == "3")
            {
                WriteLog(batchKey, ACABatchLogger.LogType.ProcessData,
                    string.Format("Failed to update {0} on BPM Server. SyncFlags were set back to '1'.", infName),
                    string.Format("Total rows = {0}", rows.ToString()), rows);
                result = false;
            }

            return result;
        }

        public bool HandleLog(Guid batchKey, string type, string infName, string entityName,
                                DateTime localDt, DateTime serverDt, int rows, string errMsg)
        {
            bool result = false;            

            if (type == "0")
            {
                WriteLog(batchKey, ACABatchLogger.LogType.ProcessData,
                    string.Format("Getting Data Completed {0}", infName),
                    string.Format("Total rows = {0}", rows.ToString()), rows);
                result = true;
            }
            else if (type == "1")
            {
                WriteLog(batchKey, ACABatchLogger.LogType.Success,
                    string.Format("Updating Data Completed {0}", infName),
                    string.Format("Total rows = {0}", rows.ToString()), rows);

                Scheduler sch = new Scheduler();
                sch.EntityName = entityName;
                sch.LastUpdateDt = localDt;
                sch.JobType = "UL";
                result = ACABatchScheduler.UpdateScheduler(sch);
            }
            else if (type == "2")
            {
                WriteLog(batchKey, ACABatchLogger.LogType.Success, "There is 0 record to upload", infName);
                result = true;
            }
            else if (type == "3")
            {
                WriteLog(batchKey, ACABatchLogger.LogType.ProcessData,
                    string.Format("Failed to update {0} on BPM Server. Error Message : {1}", infName, errMsg),
                    string.Format("Total rows = {0}", rows), rows);
                result = false;
            }

            return result;
        }

        private string GetErrorType(LogType errType)
        {
            if (errType == LogType.FileValidation)
                return "File Validation Error";
            else if (errType == LogType.ProcessData)
                return "Processing Batch";
            else if (errType == LogType.ProcessingError)
                return "Batch Processing Error";
            else if (errType == LogType.Success)
                return "Process Batch Successed";
            else
                return "Unknown Error";
        }

        private void InsertErrorLog(Guid batchKey, string errType, string module, string errMsg, string suggestMsg, int errLine)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("ACABatch_SQL");
                DbCommand cmd = db.GetStoredProcCommand("dbo.ACA_INT_BatchLog");
                db.AddInParameter(cmd, "BatchKey", DbType.Guid, batchKey);
                db.AddInParameter(cmd, "ErrorType", DbType.String, errType);
                db.AddInParameter(cmd, "ModuleName", DbType.String, module);
                db.AddInParameter(cmd, "ErrorMsg", DbType.String, errMsg);
                db.AddInParameter(cmd, "Suggestion", DbType.String, suggestMsg);
                db.AddInParameter(cmd, "ErrorLine", DbType.Int32, errLine);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception)
            {
                throw;
            }
        }

       /// <summary>
       /// ใช้สำหรับการเก็บ Error กรณี Batch Server ไม่สามารถเชื่อมต่อ BPM WebService ได้
       /// </summary>
       /// <param name="errorMsg"></param>
        private void InsertNotificationErrorLog(string errorMsg)
        {
            try{
                    //ตรวจสอบว่ามี Config หรือไม่  ถ้าไม่มีจะถือว่าเป็น Branch Server ให้บันทึกลง Windows Event Log แทน
                    if (ConfigurationManager.ConnectionStrings["NotificationDatabase"] == null)
                    {
                        EventLog.WriteEntry(S_SOURCE_NOTIFICATION, errorMsg, EventLogEntryType.Error);
                        return;
                    }
                    else //กรณีของ Center Server
                    {
                        try
                        {
                            string wsUrl = ConfigurationManager.AppSettings[WS_BPM_ADDR];
                            if (string.IsNullOrEmpty(wsUrl)) return; // ถ้า AppConfig ถูก Comment ทิ้ง(บังคับปิดการทำงาน)ให้ return
                           
                            Database db = DatabaseFactory.CreateDatabase("NotificationDatabase");
                            DbCommand cmd = db.GetStoredProcCommand("ins_BatchErrorLog");
                            db.AddInParameter(cmd, "errMessage", DbType.String, errorMsg);
                            db.ExecuteNonQuery(cmd);
                        }
                        catch (Exception ex)
                        {
                            //ถ้ายังเชื่อมต่อ Database ของ Notification ไม่ได้อีก (อาจจะเพราะ Network ภายในมีปัญหา) ให้เก็บลง Windows Event Log
                            EventLog.WriteEntry(S_SOURCE_NOTIFICATION, ex.Message, EventLogEntryType.Error);
                        }
                    }
                }catch(Exception)
                { //Do nothing
                }
                
        }
    }
}

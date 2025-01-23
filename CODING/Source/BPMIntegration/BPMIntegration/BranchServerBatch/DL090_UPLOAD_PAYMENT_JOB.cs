using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Configuration;
using Avanade.ACA.Batch;
using PEA.BPM.Integration.ACABatchService;
using PEA.BPM.Integration.BPMIntegration.BS;
using PEA.BPM.Integration.BPMIntegration.SG;
using PEA.BPM.Integration.BPMIntegration.Interface.Services;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;


namespace PEA.BPM.Integration.BPMIntegration.BranchServerBatch
{
    public class DL090_UPLOAD_PAYMENT_JOB : IJob
    {
        private const string DL_NAME_ID = "DL090";
        private const string DL_FULL_NAME = "DL090_UPLOAD_PAYMENT";
        private const string BRANCH_ID = "BRANCH_ID";
        private const string UPLOAD_LOT = "UPLOAD_LOT";
        private const string INTERVAL = "INTERVAL";
        private string _activeFileKey;
        private ACADepLineMgr _depMgr;
        private ACABatchLogger _logger;

        private IBpmBranchService _bpmBranchService;
        private IBpmServerService _bpmServerSG;
        private string _entityName;
        private DateTime _serverDate;
        private DateTime _localDate;
        private string _branchId;
        private int _uploadLot;
        private int _interval;

        public DL090_UPLOAD_PAYMENT_JOB()
        {
            _depMgr = new ACADepLineMgr();
            _logger = ACABatchLogger.GetInstance(DL_FULL_NAME);

            _bpmBranchService = new BPMBranchService();
            _bpmServerSG = new BpmServerUploadSG();
            _branchId = ConfigurationManager.AppSettings[BRANCH_ID];
            _uploadLot = Convert.ToInt32(ConfigurationManager.AppSettings[UPLOAD_LOT]);
            _interval =  Convert.ToInt32(ConfigurationManager.AppSettings[INTERVAL]);
            _localDate = DateTime.Now;//_service.GetDbServerTime();
            _logger.BranchId = _branchId;
        }

        #region Additional Members

        //wrapper method
        private bool ExportData(Guid batchKey, string tb, bool overwrite)
        {
            //set _hasSkipError 
            bool ret = false;
            ACABatchParam param = new ACABatchParam();
            param.Overwrite = overwrite;
            param.BatchKey = batchKey;
            _logger.InterfaceId = tb;

            try
            {
                switch (tb)
                {
                    case "AR":
                        {
                            param.FileName = "AR";
                            ret = UploadAR(param);
                            //ret = true;
                        }
                        break;
                    case "Payment":
                        {
                            param.FileName = "Payment";
                            ret = UploadPayment(param);
                        }
                        break;
                    case "ARPaymentType":
                        {
                            param.FileName = "ARPaymentType";
                            ret = UploadARPaymentType(param);
                        }
                        break;
                    case "ARPayment":
                        {
                            param.FileName = "ARPayment";
                            ret = UploadARPayment(param);
                        }
                        break;
                    case "RTARPaymentTypeARPayment":
                        {
                            param.FileName = "RTARPaymentTypeARPayment";
                            ret = UploadRTARPaymentTypeARPayment(param);
                        }
                        break;
                    case "Receipt":
                        {
                            param.FileName = "Receipt";
                            ret = UploadReceipt(param);
                        }
                        break;
                    case "ReceiptItem":
                        {
                            param.FileName = "ReceiptItem";
                            ret = UploadReceiptItem(param);
                        }
                        break;
                    case "PaymentLog":
                        {
                            param.FileName = "PaymentLog";
                            ret = UploadPaymentLog(param);
                        }
                        break;          
                    default:
                        {
                            _logger.Severity = Severity.Level8;
                            _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, string.Format("Dependency Line {0} ไม่รองรับการนำเข้าข้อมูลในตารางนี้", DL_NAME_ID), tb);
                            return false;
                        }
                }

                return ret;
            }
            catch (Exception e)
            {
                _logger.Severity = Severity.Level9;
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, e.ToString(), string.Format("Uploading data failed, {0}", tb));
                return false;
            }
        }

        private bool HandleError(Guid batchKey, string key, string tb)
        {
            ACADepLineInfo dep = new ACADepLineInfo();
            dep.DLId = DL_NAME_ID;
            dep.FileKey = key;
            dep.LastFailTb = tb;
            dep.Status = "1"; //fail

            try
            {
                _depMgr.SetStatus(dep, false); //create new record

                return true;
            }
            catch (Exception e)
            {
                _logger.Severity = Severity.Level6;
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, e.Message, "มีปัญหาเกี่ยวกับฟังก์ชันในการ HandleError");
                return false;
            }
        }

        private bool HandleSuccess(Guid batchKey, string tb)
        {
            try
            {
                _depMgr.ResetLastFailTb(DL_NAME_ID, tb);

                return true;
            }
            catch (Exception e)
            {
                _logger.Severity = Severity.Level6;
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, e.Message, "มีปัญหาเกี่ยวกับฟังก์ชันในการ HandleSuccess");
                return false;
            }
        }

        private bool UploadAR(ACABatchParam param)
        {
            _entityName = typeof(AR).Name;
            //_serverDate = _bpmServerSG.GetServerTime();
            string _infName = "AR";

            try
            {
                List<AR> list = new List<AR>();
                list = _bpmBranchService.GetToUploadAR(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadAR(list, _branchId);

                    if (rows == list.Count)
                        return _logger.HandleLog(param.BatchKey, "1", _infName, _entityName, _localDate, _serverDate, rows);
                    else
                        return false;
                }
                else
                    return _logger.HandleLog(param.BatchKey, "2", _infName, _entityName, _localDate, _serverDate, rows);
            }
            catch (Exception ex)
            {                
                int syncRows = _bpmBranchService.SetSyncAR(_serverDate);
                return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
            }

            //_entityName = typeof(AR).Name;
      
            //_lastModifiedDt = _bpmServerSG.GetServerTime();

            //List<AR> ars = new List<AR>();
            //ars = _bpmBranchService.GetToUploadAR(_lastModifiedDt);
            //int resultRows = ars.Count;

            //if (ars.Count > 0)
            //{
            //    int totalRows = 0;

            //    _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Getting Data Completed [AR]", "Total rows = " + ars.Count.ToString());

            //    while (ars.Count > 0)
            //    {                   
            //        if (ars.Count >= _uploadLot)                    
            //        {
            //            List<AR> tmp = ars.GetRange(0, _uploadLot);                        
                            
            //            int rows = _bpmServerSG.UploadAR(tmp, _branchId);
            //            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Updating Data Completed [AR]", "Rows = " + rows, rows);

            //            string tmpFirst = tmp[0].ItemId;
            //            string tmpLast = tmp[tmp.Count - 1].ItemId;
                        
            //            _bpmBranchService.SetSyncAR(_lastModifiedDt, tmpFirst, tmpLast);
            //            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Setting SyncFlag Completed [AR]", "Rows = " + rows, rows);
                        
            //            ars.RemoveRange(0, _uploadLot);
                       
            //            totalRows += rows;
            //            tmp = null;
                        
            //        }
            //        else
            //        {
            //            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessData,"Last Lot", 0);
            //            int rows = _bpmServerSG.UploadAR(ars, _branchId);
            //            _bpmBranchService.SetSyncAR(ars[ars.Count - 1].ModifiedDt.Value, ars[0].ItemId, ars[ars.Count-1].ItemId);
            //            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Updating Data Completed [AR]", "Rows = " + rows, rows);
            //            ars.RemoveRange(0, ars.Count);
            //            totalRows += rows;
            //        }
            //    }


            //    if (totalRows == resultRows)
            //    {
            //        ars = null;

            //        _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Updating Data Completed [AR]", "Total rows = " + totalRows, totalRows);

            //        Scheduler sch = new Scheduler();
            //        sch.EntityName = _entityName;
            //        sch.LastUpdateDt = _serverDate;
            //        sch.JobType = "UL";
                    
            //        bool result = _service.UpdateScheduler(sch);
            //        return result;
            //    }
            //    else
            //        return false;


            //}
            //else
            //{
            //    _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already uploaded at " + _lastModifiedDt, "AR");
            //    return true;
            //}

        }

        private bool UploadPayment(ACABatchParam param)
        {
            _entityName = typeof(Payment).Name;
            //_serverDate = _bpmServerSG.GetServerTime();
            string _infName = "Payment";

            try
            {
                List<Payment> list = new List<Payment>();
                list = _bpmBranchService.GetToUploadPayment(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadPayment(list, _branchId);

                    if (rows == list.Count)
                        return _logger.HandleLog(param.BatchKey, "1", _infName, _entityName, _localDate, _serverDate, rows);
                    else
                        return false;
                }
                else
                    return _logger.HandleLog(param.BatchKey, "2", _infName, _entityName, _localDate, _serverDate, rows);
            }
            catch (Exception ex)
            {
                int syncRows = _bpmBranchService.SetSyncPayment(_serverDate);
                return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
            }
            //_entityName = typeof(Payment).Name;
            
            ////Modified on 29 November 2008; 
            ////Used server time as the condition for selecting data and updating syncFlag
            ////_lastModifiedDt = _service.GetLastUploadedDate(_entityName);            
            //_lastModifiedDt = _bpmServerSG.GetServerTime();

            //List<Payment> list = new List<Payment>();
            //list = _bpmBranchService.GetToUploadPayment(_lastModifiedDt);
            //int resultRows = list.Count;

            //if (list.Count > 0)
            //{
            //    int totalRows = 0;

            //    _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Getting Data Completed [Payment]", "Total rows = " + list.Count.ToString());

            //    while (list.Count > 0)
            //    {
            //        if (list.Count >= _uploadLot)
            //        {
            //            List<Payment> tmp = list.GetRange(0, _uploadLot);

            //            int rows = _bpmServerSG.UploadPayment(tmp, _branchId);
            //            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Updating Data Completed [Payment]", "Rows = " + rows, rows);

            //            string tmpFirst = tmp[0].PaymentId;
            //            string tmpLast = tmp[tmp.Count - 1].PaymentId;

            //            _bpmBranchService.SetSyncPayment(_lastModifiedDt, tmpFirst, tmpLast);
            //            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Setting SyncFlag Completed [Payment]", "Rows = " + rows, rows);

            //            list.RemoveRange(0, _uploadLot);

            //            totalRows += rows;
            //            tmp = null;

            //        }
            //        else
            //        {
            //            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessData, "Last Lot", 0);
            //            int rows = _bpmServerSG.UploadPayment(list, _branchId);
            //            _bpmBranchService.SetSyncPayment(list[list.Count - 1].ModifiedDt.Value, list[0].PaymentId, list[list.Count - 1].PaymentId);
            //            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Updating Data Completed [Payment]", "Rows = " + rows, rows);
            //            list.RemoveRange(0, list.Count);
            //            totalRows += rows;
            //        }
            //    }

            //    if (totalRows == resultRows)
            //    {
            //        list = null;

            //        _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Updating Data Completed [Payment]", "Total rows = " + totalRows, totalRows);

            //        Scheduler sch = new Scheduler();
            //        sch.EntityName = _entityName;
            //        sch.LastUpdateDt = _serverDate;
            //        sch.JobType = "UL";

            //        bool result = _service.UpdateScheduler(sch);
            //        return result;
            //    }
            //    else
            //        return false;
            //}
            //else
            //{
            //    _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already uploaded at " + _lastModifiedDt, "Payment");
            //    return true;
            //}

        }

        private bool UploadARPaymentType(ACABatchParam param)
        {
            _entityName = typeof(ARPaymentType).Name;
            //_serverDate = _bpmServerSG.GetServerTime();
            string _infName = "ARPaymentType";

            try
            {
                List<ARPaymentType> list = new List<ARPaymentType>();
                list = _bpmBranchService.GetToUploadARPaymentType(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadARPaymentType(list, _branchId);

                    if (rows == list.Count)
                        return _logger.HandleLog(param.BatchKey, "1", _infName, _entityName, _localDate, _serverDate, rows);
                    else
                        return false;
                }
                else
                    return _logger.HandleLog(param.BatchKey, "2", _infName, _entityName, _localDate, _serverDate, rows);
            }
            catch(Exception ex)
            {
                int syncRows = _bpmBranchService.SetSyncARPaymentType(_serverDate);
                return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());                
            }
            //_entityName = typeof(ARPaymentType).Name;

            ////Modified on 29 November 2008; 
            ////Used server time as the condition for selecting data and updating syncFlag
            ////_lastModifiedDt = _service.GetLastUploadedDate(_entityName);            
            //_lastModifiedDt = _bpmServerSG.GetServerTime();
            
            //List<ARPaymentType> list = new List<ARPaymentType>();
            //list = _bpmBranchService.GetToUploadARPaymentType(_lastModifiedDt);
            //int resultRows = list.Count;

            //if (list.Count > 0)
            //{
            //    int totalRows = 0;

            //    _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Getting Data Completed [ARPaymentType]", "Total rows = " + list.Count.ToString());

            //    while (list.Count > 0)
            //    {
            //        if (list.Count >= _uploadLot)
            //        {
            //            List<ARPaymentType> tmp = list.GetRange(0, _uploadLot);

            //            int rows = _bpmServerSG.UploadARPaymentType(tmp, _branchId);
            //            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Updating Data Completed [ARPaymentType]", "Rows = " + rows, rows);

            //            string tmpFirst = tmp[0].ARPtId;
            //            string tmpLast = tmp[tmp.Count - 1].ARPtId;

            //            _bpmBranchService.SetSyncARPaymentType(_lastModifiedDt, tmpFirst, tmpLast);
            //            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Setting SyncFlag Completed [ARPaymentType]", "Rows = " + rows, rows);

            //            list.RemoveRange(0, _uploadLot);

            //            totalRows += rows;
            //            tmp = null;

            //        }
            //        else
            //        {
            //            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessData, "Last Lot", 0);
            //            int rows = _bpmServerSG.UploadARPaymentType(list, _branchId);
            //            _bpmBranchService.SetSyncARPaymentType(list[list.Count - 1].ModifiedDt.Value, list[0].ARPtId, list[list.Count - 1].ARPtId);
            //            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Updating Data Completed [ARPaymentType]", "Rows = " + rows, rows);
            //            list.RemoveRange(0, list.Count);
            //            totalRows += rows;
            //        }
            //    }

            //    if (totalRows == resultRows)
            //    {
            //        list = null;

            //        _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Updating Data Completed [ARPaymentType]", "Total rows = " + totalRows, totalRows);

            //        Scheduler sch = new Scheduler();
            //        sch.EntityName = _entityName;
            //        sch.LastUpdateDt = _serverDate;
            //        sch.JobType = "UL";

            //        bool result = _service.UpdateScheduler(sch);
            //        return result;
            //    }
            //    else
            //        return false;
            //}
            //else
            //{
            //    _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already uploaded at " + _lastModifiedDt, "ARPaymentType");
            //    return true;
            //}
        }

        private bool UploadARPayment(ACABatchParam param)
        {
             _entityName = typeof(ARPayment).Name;
             //_serverDate = _bpmServerSG.GetServerTime();
             string _infName = "ARPayment";

             try
             {
                 List<ARPayment> list = new List<ARPayment>();
                 list = _bpmBranchService.GetToUploadARPayment(_serverDate);
                 int listCount = list.Count;
                 int rows = 0;

                 if (listCount > 0)
                 {
                     _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                     rows = _bpmServerSG.UploadARPayment(list, _branchId);

                     if (rows == list.Count)
                         return _logger.HandleLog(param.BatchKey, "1", _infName, _entityName, _localDate, _serverDate, rows);
                     else
                         return false;
                 }
                 else
                     return _logger.HandleLog(param.BatchKey, "2", _infName, _entityName, _localDate, _serverDate, rows);
             }
             catch (Exception ex)
             {
                 int syncRows = _bpmBranchService.SetSyncARPayment(_serverDate);
                 return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
             }
            //_entityName = typeof(ARPayment).Name;

            ////Modified on 29 November 2008; 
            ////Used server time as the condition for selecting data and updating syncFlag
            ////_lastModifiedDt = _service.GetLastUploadedDate(_entityName);            
            //_lastModifiedDt = _bpmServerSG.GetServerTime();

            //List<ARPayment> list = new List<ARPayment>();
            //list = _bpmBranchService.GetToUploadARPayment(_lastModifiedDt);
            //int resultRows = list.Count;

            //if (list.Count > 0)
            //{
            //    int totalRows = 0;

            //    _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Getting Data Completed [ARPayment]", "Total rows = " + list.Count.ToString());

            //    while (list.Count > 0)
            //    {
            //        if (list.Count >= _uploadLot)
            //        {
            //            List<ARPayment> tmp = list.GetRange(0, _uploadLot);

            //            int rows = _bpmServerSG.UploadARPayment(tmp, _branchId);
            //            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Updating Data Completed [ARPayment]", "Rows = " + rows, rows);

            //            string tmpFirst = tmp[0].ArpmId;
            //            string tmpLast = tmp[tmp.Count - 1].ArpmId;

            //            _bpmBranchService.SetSyncARPayment(_lastModifiedDt, tmpFirst, tmpLast);
            //            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Setting SyncFlag Completed [ARPayment]", "Rows = " + rows, rows);

            //            list.RemoveRange(0, _uploadLot);

            //            totalRows += rows;
            //            tmp = null;

            //        }
            //        else
            //        {
            //            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessData, "Last Lot", 0);
            //            int rows = _bpmServerSG.UploadARPayment(list, _branchId);
            //            _bpmBranchService.SetSyncARPayment(list[list.Count - 1].ModifiedDt.Value, list[0].ArpmId, list[list.Count - 1].ArpmId);
            //            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Updating Data Completed [ARPayment]", "Rows = " + rows, rows);
            //            list.RemoveRange(0, list.Count);
            //            totalRows += rows;
            //        }
            //    }

            //    if (totalRows == resultRows)
            //    {
            //        list = null;

            //        _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Updating Data Completed [ARPayment]", "Total rows = " + totalRows, totalRows);

            //        Scheduler sch = new Scheduler();
            //        sch.EntityName = _entityName;
            //        sch.LastUpdateDt = _serverDate;
            //        sch.JobType = "UL";

            //        bool result = _service.UpdateScheduler(sch);
            //        return result;
            //    }
            //    else
            //        return false;
            //}
            //else
            //{
            //    _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already uploaded at " + _lastModifiedDt, "ARPayment");
            //    return true;
            //}
        }

        private bool UploadRTARPaymentTypeARPayment(ACABatchParam param)
        {
            _entityName = typeof(RTARPaymentTypeARPayment).Name;
            //_serverDate = _bpmServerSG.GetServerTime();
            string _infName = "RTARPaymentTypeARPayment";

            try
            {
                List<RTARPaymentTypeARPayment> list = new List<RTARPaymentTypeARPayment>();
                list = _bpmBranchService.GetToUploadRTARPaymentTypeARPayment(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadRTARPaymentTypeARPayment(list, _branchId);

                    if (rows == list.Count)
                        return _logger.HandleLog(param.BatchKey, "1", _infName, _entityName, _localDate, _serverDate, rows);
                    else
                        return false;
                }
                else
                    return _logger.HandleLog(param.BatchKey, "2", _infName, _entityName, _localDate, _serverDate, rows);
            }
            catch(Exception ex)
            {
                int syncRows = _bpmBranchService.SetSyncRTARPaymentTypeARPayment(_serverDate);
                return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
                
            }
            //_entityName = typeof(RTARPaymentTypeARPayment).Name;

            ////Modified on 29 November 2008; 
            ////Used server time as the condition for selecting data and updating syncFlag
            ////_lastModifiedDt = _service.GetLastUploadedDate(_entityName);            
            //_lastModifiedDt = _bpmServerSG.GetServerTime();

            //List<RTARPaymentTypeARPayment> rtARs = new List<RTARPaymentTypeARPayment>();
            //rtARs = _bpmBranchService.GetToUploadRTARPaymentTypeARPayment(_lastModifiedDt);

            //if (rtARs.Count > 0)
            //{
            //    _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Getting Data Completed [RTARPaymentTypeARPayment]", "Total rows = " + rtARs.Count.ToString());
                
            //    int rows = _bpmServerSG.UploadRTARPaymentTypeARPayment(rtARs, _branchId);
            //    _bpmBranchService.SetSyncRTARPaymentTypeARPayment(_lastModifiedDt);

            //    if (rows == rtARs.Count)
            //    {
            //        _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Updating Data Completed [RTARPaymentTypeARPayment]", "Total rows = " + rows, rows);

            //        Scheduler sch = new Scheduler();
            //        sch.EntityName = _entityName;
            //        sch.LastUpdateDt = _serverDate;
            //        sch.JobType = "UL";
            //        bool result = _service.UpdateScheduler(sch);

            //        return result;
            //    }
            //    else
            //        return false;
            //}
            //else
            //{
            //    _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already uploaded at " + _lastModifiedDt, "RTARPaymentTypeARPayment");
            //    return true;
            //}
        }

        private bool UploadReceipt(ACABatchParam param)
        {
            _entityName = typeof(Receipt).Name;
            //_serverDate = _bpmServerSG.GetServerTime();
            string _infName = "Receipt";

            try
            {
                List<Receipt> list = new List<Receipt>();
                list = _bpmBranchService.GetToUploadReceipt(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadReceipt(list, _branchId);

                    if (rows == list.Count)
                        return _logger.HandleLog(param.BatchKey, "1", _infName, _entityName, _localDate, _serverDate, rows);
                    else
                        return false;
                }
                else
                    return _logger.HandleLog(param.BatchKey, "2", _infName, _entityName, _localDate, _serverDate, rows);
            }
            catch (Exception ex)
            {
                int syncRows = _bpmBranchService.SetSyncReceipt(_serverDate);
                return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
            }
            //_entityName = typeof(Receipt).Name;

            ////Modified on 29 November 2008; 
            ////Used server time as the condition for selecting data and updating syncFlag
            ////_lastModifiedDt = _service.GetLastUploadedDate(_entityName);            
            //_lastModifiedDt = _bpmServerSG.GetServerTime();

            //List<Receipt> list = new List<Receipt>();
            //list = _bpmBranchService.GetToUploadReceipt(_lastModifiedDt);
            //int resultRows = list.Count;

            //if (list.Count > 0)
            //{
            //    int totalRows = 0;

            //    _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Getting Data Completed [Receipt]", "Total rows = " + list.Count.ToString());

            //    while (list.Count > 0)
            //    {
            //        if (list.Count >= _uploadLot)
            //        {
            //            List<Receipt> tmp = list.GetRange(0, _uploadLot);

            //            int rows = _bpmServerSG.UploadReceipt(tmp, _branchId);
            //            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Updating Data Completed [Receipt]", "Rows = " + rows, rows);

            //            string tmpFirst = tmp[0].ReceiptId;
            //            string tmpLast = tmp[tmp.Count - 1].ReceiptId;

            //            _bpmBranchService.SetSyncReceipt(_lastModifiedDt, tmpFirst, tmpLast);
            //            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Setting SyncFlag Completed [Receipt]", "Rows = " + rows, rows);

            //            list.RemoveRange(0, _uploadLot);

            //            totalRows += rows;
            //            tmp = null;

            //        }
            //        else
            //        {
            //            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.ProcessData, "Last Lot", 0);
            //            int rows = _bpmServerSG.UploadReceipt(list, _branchId);
            //            _bpmBranchService.SetSyncReceipt(list[list.Count - 1].ModifiedDt.Value, list[0].ReceiptId, list[list.Count - 1].ReceiptId);
            //            _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Updating Data Completed [Receipt]", "Rows = " + rows, rows);
            //            list.RemoveRange(0, list.Count);
            //            totalRows += rows;
            //        }
            //    }

            //    if (totalRows == resultRows)
            //    {
            //        list = null;

            //        _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Updating Data Completed [Receipt]", "Total rows = " + totalRows, totalRows);

            //        Scheduler sch = new Scheduler();
            //        sch.EntityName = _entityName;
            //        sch.LastUpdateDt = _serverDate;
            //        sch.JobType = "UL";

            //        bool result = _service.UpdateScheduler(sch);
            //        return result;
            //    }
            //    else
            //        return false;
            //}
            //else
            //{
            //    _logger.WriteLog(param.BatchKey, ACABatchLogger.LogType.Success, "Data were already uploaded at " + _lastModifiedDt, "Receipt");
            //    return true;
            //}
        }

        private bool UploadReceiptItem(ACABatchParam param)
        {
            _entityName = typeof(ReceiptItem).Name;
            //_serverDate = _bpmServerSG.GetServerTime();
            string _infName = "ReceiptItem";

            try
            {
                List<ReceiptItem> list = new List<ReceiptItem>();
                list = _bpmBranchService.GetToUploadReceiptItem(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadReceiptItem(list, _branchId);

                    if (rows == list.Count)
                        return _logger.HandleLog(param.BatchKey, "1", _infName, _entityName, _localDate, _serverDate, rows);
                    else
                        return false;
                }
                else
                    return _logger.HandleLog(param.BatchKey, "2", _infName, _entityName, _localDate, _serverDate, rows);
            }
            catch (Exception ex)
            {
                int syncRows = _bpmBranchService.SetSyncReceiptItem(_serverDate);
                return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
            }
        }

        private bool UploadPaymentLog(ACABatchParam param)
        {
            _entityName = typeof(ReceiptItem).Name;
            //_serverDate = _bpmServerSG.GetServerTime();
            string _infName = "PaymentLog";

            try
            {
                List<PaymentLogInfo> list = new List<PaymentLogInfo>();
                list = _bpmBranchService.GetToUploadPaymentLog(_serverDate);
                int listCount = list.Count;
                int rows = 0;

                if (listCount > 0)
                {
                    _logger.HandleLog(param.BatchKey, "0", _infName, _entityName, _localDate, _serverDate, listCount);

                    rows = _bpmServerSG.UploadPaymentLog(list, _branchId);

                    if (rows == list.Count)
                        return _logger.HandleLog(param.BatchKey, "1", _infName, _entityName, _localDate, _serverDate, rows);
                    else
                        return false;
                }
                else
                    return _logger.HandleLog(param.BatchKey, "2", _infName, _entityName, _localDate, _serverDate, rows);
            }
            catch (Exception ex)
            {
                int syncRows = _bpmBranchService.SetSyncPaymentLog(_serverDate);
                return _logger.HandleLog(param.BatchKey, "3", _infName, _entityName, _localDate, _serverDate, syncRows, ex.ToString());
            }
        }     

        #endregion        

        #region IJob Members

        public void Execute(JobExecutionContext context)
        {
            context.Status = StatusCode.Executing;
            context.Commit();
            Guid batchKey = context.BatchExecutionContext.BatchExecutionContextData.Key;

            try
            {
                

                _depMgr = new ACADepLineMgr();
                _logger = ACABatchLogger.GetInstance(DL_FULL_NAME);
                _serverDate = _bpmServerSG.GetServerTime();

                ACADepLineInfo depLine = _depMgr.GetDepedencyLineStatus(DL_NAME_ID);

                if (depLine.Status == "0") // success
                {
                    _activeFileKey = SAPFile.FindSuitableKey();
                }
                else //last error 
                {
                    string fileKey = depLine.FileKey;

                    if (string.IsNullOrEmpty(fileKey))
                    {
                        _logger.Severity = Severity.Level7;
                        _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, "ระบบไม่พบ key ของการนำเข้าที่ผิดพลาดก่อนหน้านี้");
                        context.Status = StatusCode.Failed;
                        context.Commit();
                        return;
                    }

                    _activeFileKey = depLine.FileKey;
                }                

                //*** start process data to tables
                List<string> tables = _depMgr.GetDependencyLineTable(DL_NAME_ID);
                Hashtable failHt = _depMgr.GetAllLastFailHt(DL_NAME_ID);

                foreach (string tb in tables)
                {

                    if (!ExportData(batchKey, tb, true))
                    {
                        if (!failHt.ContainsKey(tb))
                        {
                            HandleError(batchKey, _activeFileKey, tb);
                        }
                        break;
                    }
                    else
                    {
                        if (!HandleSuccess(batchKey, tb))
                            throw new Exception("ERROR Stop!");
                    }

                }

                if (_depMgr.IsErrorRemain(DL_NAME_ID))
                {
                    List<ACADepLineInfo> depList = _depMgr.GetAllDLFailTb(DL_NAME_ID);
                    _logger.Severity = Severity.Level7;
                    foreach (ACADepLineInfo dep in depList)
                        _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, string.Format("ยังคงเหลือข้อมูลที่ยังไม่ถูกแก้ไขและนำเข้าโดยสมบูรณ์, Interface: {0}, Key: {1} ",
                                                    dep.LastFailTb, dep.FileKey), "");

                    context.Status = StatusCode.Failed;
                    context.Commit();
                }
                else
                {
                    context.Status = StatusCode.Success;
                    context.Commit();
                }

            }
            catch (Exception e)
            {
                _logger.Severity = Severity.Level10;
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, e.ToString(), "เกิดปัญหาการเรียกฟังก์ชันในการประมวลผลแบ็ช");
                context.Status = StatusCode.Failed;
                context.Commit();
            }
        }

        #endregion
    }
}

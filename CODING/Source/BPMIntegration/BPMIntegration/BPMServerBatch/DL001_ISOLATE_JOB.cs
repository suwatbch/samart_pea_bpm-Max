using System;
using System.Collections.Generic;
using System.Text;
using Avanade.ACA.Batch;
using PEA.BPM.Integration.BPMIntegration.Interface.Services;
using System.IO;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using PEA.BPM.Integration.BPMIntegration.BS;
using System.Collections;
using System.Configuration;
using PEA.BPM.Integration.ACABatchService;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;

namespace PEA.BPM.Integration.BPMIntegration.BPMServerBatch
{
    /// <summary>
    /// NonWorkingDay,AccountClass,ContractType,MeterSizeType,PaymentMethod,TaxCode,UnitType (sequencially)
    /// </summary>
    public class DL001_ISOLATE_JOB : IJob
    {
        private const string DL_NAME_ID = "DL001";
        private const string DL_FULL_NAME = "DL001_ISOLATE";

        private SAPPort _sapPort;
        private ACADepLineMgr _depMgr;
        private ACABatchLogger _logger;
        private IBpmSAPService _bpmSAPService;
        private string _filePool;
        private string _activeFileKey;

        public DL001_ISOLATE_JOB()
        {
            _sapPort = new SAPPort();
            _bpmSAPService = new BPMSAPService();
            _filePool = BPMServerBatchHelper.GetDataProcessingPath(DL_FULL_NAME);
            _depMgr = new ACADepLineMgr();
            _logger = ACABatchLogger.GetInstance(DL_FULL_NAME);
        }

        #region Additional Members

        //wrapper method
        private bool ImportData(Guid batchKey, string tb, string file, bool overwrite)
        {
            //set _hasSkipError 
            bool ret = false;
            _sapPort.InFileName = file;
            ACABatchParam param = new ACABatchParam();
            param.Overwrite = overwrite;
            param.FileName = file;
            param.BatchKey = batchKey;
            _logger.InterfaceId = tb;

            if (file.Contains(@"\"))
            {
                string[] tmpFileName = file.Split(new char[] { '\\' });
                param.ShortFileName = tmpFileName[tmpFileName.Length - 1];
            }
            else
                param.ShortFileName = file;

            if (param.ShortFileName.Contains("-"))
            {
                int first = param.ShortFileName.IndexOf('-');
                int last = param.ShortFileName.LastIndexOf('-');
                param.ShortFileName = param.ShortFileName.Remove(first, last - first + 1);
            }

            try
            {
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessData, string.Format("Import {0} Started at {1}", tb, DateTime.Now.ToLongTimeString()));

                switch (tb)
                {
                    case "MTR0080": //NonWorkingDay
                        {
                            List<NonWorkingDayInfo> dataList = _sapPort.Receive<NonWorkingDayInfo>();
                            ret = _bpmSAPService.Import_NonWorkingDay(dataList, param);
                        }
                        break;
                    case "CFR0010": //AccountClass
                        {
                            List<AccountClassInfo> dataList = _sapPort.Receive<AccountClassInfo>();
                            ret = _bpmSAPService.Import_AccountClass(dataList, param);
                        }
                        break;
                    case "CFR0030": //ContractType
                        {
                            List<ContractTypeInfo> dataList = _sapPort.Receive<ContractTypeInfo>();
                            ret = _bpmSAPService.Import_ContractType(dataList, param);
                        }
                        break;
                    case "CFR0050": //MeterSizeType 
                        {
                            List<MeterSizeInfo> dataList = _sapPort.Receive<MeterSizeInfo>();
                            ret = _bpmSAPService.Import_MeterSizeType(dataList, param);
                        }
                        break;
                    case "CFR0060": //PaymentMethod
                        {
                            List<PaymentMethodInfo> dataList = _sapPort.Receive<PaymentMethodInfo>();
                            ret = _bpmSAPService.Import_PaymentMethod(dataList, param);
                        }
                        break;
                    case "CFR0070": //TaxCode
                        {
                            List<TaxCodeInfo> dataList = _sapPort.Receive<TaxCodeInfo>();
                            ret = _bpmSAPService.Import_TaxCode(dataList, param);
                        }
                        break;
                    case "CFR0080": //UnitType
                        {
                            List<UnitTypeInfo> dataList = _sapPort.Receive<UnitTypeInfo>();
                            ret = _bpmSAPService.Import_UnitType(dataList, param);
                        }
                        break;
                    default:
                        {
                            _logger.Severity = Severity.Level8;
                            _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessData, string.Format("Dependency Line {0} ไม่รองรับการนำเข้าข้อมูลในตารางนี้", DL_NAME_ID), file);
                            return false;
                        }
                }

                _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success, string.Format("Import {0} Done at {1}", tb, DateTime.Now.ToLongTimeString()));

                return ret;
            }
            catch (Exception e)
            {
                _logger.Severity = Severity.Level10;
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, e.ToString(), string.Format("Extracting file failed, {0}", file));
                return false;
            }
        }

        private bool HandleError(Guid batchKey, string path, string file, string key, string tb)
        {
            string destFile = null;
            string source = null;

            ACADepLineInfo dep = new ACADepLineInfo();
            dep.DLId = DL_NAME_ID;
            dep.FileKey = key;
            dep.LastFailTb = tb;
            dep.Status = "1"; //fail

            try
            {
                source = file;
                string[] sp = file.Split('\\');
                destFile = string.Format("{0}.Failed\\{1}", path, sp[sp.Length - 1]);
                _depMgr.SetStatus(dep, false); //create new record

                if (File.Exists(destFile))
                {
                    string altFileName = string.Format("{0}.old", destFile);
                    if (!File.Exists(altFileName))
                        File.Move(source, altFileName);

                    //if file still exist, just ignore saving
                    return true;
                }

                File.Move(source, destFile);
                return true;
            }
            catch (Exception e)
            {
                _logger.Severity = Severity.Level6;
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError , e.Message, string.Format("มีปัญหาในการย้ายไฟล์หรือการบันทึกข้อผิดพลาดของการนำเข้าไฟล์ : {0}",
                        string.Format("ต้นทาง: {0} \nปลายทาง: {1}", source, destFile)));
                return false;
            }
        }

        private bool HandleSuccess(Guid batchKey, string path, string file, string tb)
        {
            string destFile = null;
            string source = null;
            try
            {
                source = file;
                string[] sp = file.Split('\\');
                destFile = string.Format("{0}.Succeeded\\{1}", path, sp[sp.Length - 1]);
                _depMgr.ResetLastFailTb(DL_NAME_ID, tb);

                if (File.Exists(destFile))
                {
                    string altFileName = string.Format("{0}.old", destFile);
                    if (!File.Exists(altFileName))
                        File.Move(source, altFileName);

                    //if file still exist, just ignore saving
                    return true;
                }

                File.Move(source, destFile);
                return true;
            }
            catch (Exception e)
            {
                _logger.Severity = Severity.Level6;
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, e.Message, string.Format("มีปัญหาในการย้ายไฟล์หรือการบันทึกข้อผิดพลาดของการนำเข้าไฟล์ : {0}",
                        string.Format("ต้นทาง: {0} \nปลายทาง: {1}", source, destFile)));
                return false;
            }
        }

        #endregion

        #region IJob Members

        public void Execute(JobExecutionContext context)
        {
            Guid batchKey = context.BatchExecutionContext.BatchExecutionContextData.Key;
            context.Status = StatusCode.Executing;
            context.Commit();

            try
            {
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
                    BPMServerBatchHelper.MoveFileToProcess(DL_FULL_NAME, tb);

                    //build target path for each table
                    string filePath = string.Format("{0}\\{1}", _filePool, tb);
                    List<string> ifiles = SAPFile.GetFileName(filePath, _activeFileKey);

                    //need manual select, should be auto selected?
                    if ((ifiles.Count == 0) && failHt.ContainsKey(tb))
                    {
                        _logger.Severity = Severity.Level7;
                        _logger.WriteLog(batchKey, ACABatchLogger.LogType.FileValidation, "ไม่พบไฟล์ที่ต้องการนำเข้า เพื่อแก้ไขข้อผิดพลาดก่อนหน้านี้ กรุณาตรวจสอบที่: ", filePath);
                        continue;
                    }
                    else if (ifiles.Count == 0) continue;

                    //overwrite by the latest file
                    string errMsg = string.Empty;
                    foreach (string file in ifiles)
                    {
                        //whole table request so it MUST has only one file per interface
                        if (!SAPFile.Validate(file,out errMsg))
                        {
                            _logger.Severity = Severity.Level6;
                            _logger.WriteLog(batchKey, ACABatchLogger.LogType.FileValidation, "รูปแบบของข้อมูลในไฟล์ไม่ถูกต้องหรือข้อมูลมีเกิดการเสียหาย (" + errMsg + ")", file);
                            HandleError(batchKey, string.Format("{0}\\{1}", _filePool, tb), file, _activeFileKey, tb);
                            continue;
                        }

                        if (!ImportData(batchKey, tb, file, true)) //always overwrite
                            HandleError(batchKey, string.Format("{0}\\{1}", _filePool, tb), file, _activeFileKey, tb);
                        else
                        {
                            if (!HandleSuccess(batchKey, string.Format("{0}\\{1}", _filePool, tb), file, tb))
                                throw new Exception("ERROR Stop!");
                        }

                        GC.Collect();
                    }
                }

                if (_depMgr.IsErrorRemain(DL_NAME_ID))
                {
                    List<ACADepLineInfo> depList = _depMgr.GetAllDLFailTb(DL_NAME_ID);
                    _logger.Severity = Severity.Level7;
                    foreach (ACADepLineInfo dep in depList)
                        _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, string.Format("ยังคงเหลือไฟล์ที่ยังไม่ถูกแก้ไขและนำเข้าโดยสมบูรณ์, Interface: {0}, Key: {1} ",
                                                    dep.LastFailTb, dep.FileKey ), "");

                    context.Status = StatusCode.Failed;
                    context.Commit();
                }
                else
                {
                    context.Status = StatusCode.Success;
                    context.Commit();                    
                }
                GC.Collect();
            }
            catch (Exception e)
            {
                _logger.Severity = Severity.Level10;
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, e.Message, "เกิดปัญหาการเรียกฟังก์ชันในการประมวลผลแบ็ช");
                context.Status = StatusCode.Failed;
                context.Commit();
                GC.Collect();
            }
        }

        #endregion

    }

}

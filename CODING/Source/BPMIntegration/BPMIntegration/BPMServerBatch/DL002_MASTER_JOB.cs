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
using System.Text.RegularExpressions;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;

namespace PEA.BPM.Integration.BPMIntegration.BPMServerBatch
{
    /// <summary>
    /// BusinessPartner,Branch,MRU,MRUPlan,ContractAccount,Employee,AgencyAsset,Bank,BankAccount,MainSub
    /// </summary>
    public class DL002_MASTER_JOB : IJob
    {
        private const string DL_NAME_ID = "DL002"; 
        private const string DL_FULL_NAME = "DL002_MASTER";

        private SAPPort _sapPort;
        private ACADepLineMgr _depMgr;
        private ACABatchLogger _logger;
        private IBpmSAPService _bpmSAPService;
        private string _filePool;
        private string _activeFileKey;
        private bool _hasSkipError = false;

        public DL002_MASTER_JOB()
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
            param.BulkFormatPath = BPMServerBatchHelper.BulkFormatPath;
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
                    case "MTR0060A":  //BusinessPartner
                        {   
                            List<BusinessPartnerInfo> dataList = _sapPort.Receive<BusinessPartnerInfo>();
                            ret = _bpmSAPService.Import_BusinessPartner(dataList, param, ref _hasSkipError, "A");                            
                        }
                        break;
                    case "MTR0060B":  //BusinessPartner
                        {
                            List<BusinessPartnerInfo> dataList = _sapPort.Receive<BusinessPartnerInfo>();
                            ret = _bpmSAPService.Import_BusinessPartner(dataList, param, ref _hasSkipError, "B");                            
                        }
                        break;
                    case "MTR0050": //Branch
                        {
                            List<BranchInfo> dataList = _sapPort.Receive<BranchInfo>();
                            //dataList.Sort(new BranchComparator());
                            ret = _bpmSAPService.Import_Branch(dataList, param, ref _hasSkipError);                            
                        }
                        break;
                    case "MTR0120": //MRU
                        {
                            List<MRUInfo> dataList = _sapPort.Receive<MRUInfo>();
                            ret = _bpmSAPService.Import_MRU(dataList, param, ref _hasSkipError);
                        }
                        break;
                    case "MTR0130": //MRUPlanBulk    
                        {
                            BPMServerBatchHelper.RemoveHeaderFooter(file);
                            ret = _bpmSAPService.Import_MRUPlanByBulk(param);
                        }
                        break;
                    case "MTR0090A": //ContractAccount
                        {
                            BPMServerBatchHelper.RemoveHeaderFooter(file);
                            ret = _bpmSAPService.Import_CABulk(param);
                        }
                        break;
                    case "MTR0090B": //ContractAccount
                        {
                            List<ContractAccountInfo> dataList = _sapPort.Receive<ContractAccountInfo>();
                            ret = _bpmSAPService.Import_ContractAccount(dataList, param, ref _hasSkipError, "B");
                        }
                        break;
                    case "MTR0090C": //ContractAccount
                        {
                            List<ContractAccountInfo> dataList = _sapPort.Receive<ContractAccountInfo>();
                            ret = _bpmSAPService.Import_ContractAccount(dataList, param, ref _hasSkipError, "C");
                        }
                        break;
                    case "MTR0090D": //ContractAccount
                        {
                            List<ContractAccountInfo> dataList = _sapPort.Receive<ContractAccountInfo>();
                            ret = _bpmSAPService.Import_ContractAccount(dataList, param, ref _hasSkipError, "D");
                        }
                        break;
                    case "MTR0090E": //ContractAccount, ControllerId
                        {
                            BPMServerBatchHelper.RemoveHeaderFooter(file);
                            ret = _bpmSAPService.Import_ControllerBulk(param);
                        }
                        break;
                    case "MTR0100": //Employee
                        {
                            List<EmployeeInfo> dataList = _sapPort.Receive<EmployeeInfo>();
                            ret = _bpmSAPService.Import_Employee(dataList, param, ref _hasSkipError);
                        }
                        break;
                    case "MTR0020": //AgencyAsset
                        {
                            List<AgencyAssetInfo> dataList = _sapPort.Receive<AgencyAssetInfo>();
                            ret = _bpmSAPService.Import_AgencyAsset(dataList, param, ref _hasSkipError);
                        }
                        break;
                    case "MTR0030": //Bank
                        {
                            List<BankInfo> dataList = _sapPort.Receive<BankInfo>();                          
                            ret = _bpmSAPService.Import_Bank(dataList, param, ref _hasSkipError);
                        }
                        break;
                    case "MTR0040": //BankAccount
                        {
                            List<BankAccountInfo> dataList = _sapPort.Receive<BankAccountInfo>();
                            ret = _bpmSAPService.Import_BankAccount(dataList, param, ref _hasSkipError);
                        }
                        break;
                    case "MTR0110": //MainSub
                        {
                            List<MainSubInfo> dataList = _sapPort.Receive<MainSubInfo>();
                            ret = _bpmSAPService.Import_MainSub(dataList, param, ref _hasSkipError);
                        }
                        break;
                    default:
                        {
                            _logger.Severity = Severity.Level8;
                            _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, string.Format("Dependency Line {0} ไม่รองรับการนำเข้าข้อมูลในตารางนี้", DL_NAME_ID), file);
                            return false;
                        }
                }

                _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success, string.Format("Import {0} Done at {1}", tb, DateTime.Now.ToLongTimeString()));

                return ret;
            }
            catch (Exception e)
            {
                _logger.Severity = Severity.Level9;
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
                _depMgr.SetStatus(dep, false); //create new record
                source = file;
                string[] sp = file.Split('\\');
                destFile = string.Format("{0}.Failed\\{1}", path, sp[sp.Length - 1]);

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

        private bool HandleSuccess(Guid batchKey, string path, string file, string tb)
        {
            string destFile = null;
            string source = null;
            try
            {
                //_depMgr.ResetLastFailTb(DL_NAME_ID, tb);
                source = file;
                string[] sp = file.Split('\\');
                destFile = string.Format("{0}.Succeeded\\{1}", path, sp[sp.Length - 1]);

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
            bool overwrite = true;

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
                Hashtable lastFailHt = _depMgr.GetAllLastFailHt(DL_NAME_ID);

                foreach (string tb in tables)
                {
                    //this table failed last time, so we don't need to overwrite data in table
                    overwrite = true;
                    if (lastFailHt.ContainsKey(tb))
                        overwrite = false;

                    BPMServerBatchHelper.MoveFileToProcess(DL_FULL_NAME, tb);
                    //build target path for each table
                    string filePath = string.Format("{0}\\{1}", _filePool, tb);
                    List<string> ifiles = SAPFile.GetFileName(filePath, _activeFileKey);

                    if ((ifiles.Count == 0) && lastFailHt.ContainsKey(tb)) //not found match key of reedit file
                    {
                        _logger.Severity = Severity.Level7;
                        _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, "ไม่พบไฟล์แก้ไขที่มี Key ตรงกับไฟล์ที่นำเข้าผิดพลาดล่าสุด", 
                                string.Format("Interface: {0}, Key: {1}", tb, _activeFileKey));                        
                        context.Status = StatusCode.Failed;
                        context.Commit();
                        return;
                    }

                    //iterate all files
                    int numOfSuccess = 0;
                    string errMsg = string.Empty;
                    foreach (string file in ifiles)
                    {
                        //if (!SAPFile.Validate(file, out errMsg))
                        //{
                        //    _logger.WriteLog(batchKey, ACABatchLogger.LogType.FileValidation, "รูปแบบของข้อมูลในไฟล์ไม่ถูกต้องหรือข้อมูลมีเกิดการเสียหาย (" + errMsg + ")", file);
                        //    HandleError(batchKey, string.Format("{0}\\{1}", _filePool, tb), file, _activeFileKey, tb);
                        //    context.Status = StatusCode.Failed;
                        //    context.Commit();
                        //    return;
                        //}

                        if (!ImportData(batchKey, tb, file, overwrite)) //unacceptable error
                        {
                            HandleError(batchKey, string.Format("{0}\\{1}", _filePool, tb), file, _activeFileKey, tb);
                            context.Status = StatusCode.Failed;
                            context.Commit();
                            return;
                        }
                        else if (_hasSkipError) //check each file
                        {
                             HandleError(batchKey, string.Format("{0}\\{1}", _filePool, tb), file, _activeFileKey, tb);
                            _hasSkipError = false;
                        }
                        else 
                        {
                            HandleSuccess(batchKey, string.Format("{0}\\{1}", _filePool, tb), file, tb);
                            numOfSuccess++;
                            if(numOfSuccess == ifiles.Count) //reset only all files of this tb successed
                                _depMgr.ResetLastFailTb(DL_NAME_ID, tb);
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
                                                    dep.LastFailTb, dep.FileKey), "");

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

        #endregion

        }
    }
}

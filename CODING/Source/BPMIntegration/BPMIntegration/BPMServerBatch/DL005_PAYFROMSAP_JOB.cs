using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Integration.ACABatchService;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using PEA.BPM.Integration.BPMIntegration.Interface.Services;
using PEA.BPM.Integration.BPMIntegration.BS;
using System.Configuration;
using Avanade.ACA.Batch;
using System.IO;
using System.Windows.Forms;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;
using System.Collections;


namespace PEA.BPM.Integration.BPMIntegration.BPMServerBatch
{
    /// <summary>
    /// Payment by another by through SAP, so we sync up the payment to our system
    /// </summary>
    public class DL005_PAYFROMSAP_JOB : IJob
    {
        private const string DL_NAME_ID = "DL005";
        private const string DL_FULL_NAME = "DL005_PAYFROMSAP";

        private SAPPort _sapPort;
        private ACADepLineMgr _depMgr;
        private ACABatchLogger _logger;
        private IBpmSAPService _bpmSAPService;
        private string _filePool;
        private string _activeFileKey;
        private bool _overwrite = true;

        public DL005_PAYFROMSAP_JOB()
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
                    case "TRR0020A":
                    case "TRR0020B":
                    case "TRR0020C":
                    case "TRR00200":
                        {
                            _bpmSAPService.Import_PayFromSAP(param, tb);
                        }
                        break;
                    case "TRR00500":
                        {
                            BPMServerBatchHelper.RemoveHeaderFooter(file);
                            _bpmSAPService.Import_CancelBPMPayment(param);
                        }
                        break;
                    case "TRR0050A": //ยกเลิกการรับเงินที่เกิดจากการโยกหนี้  ที่มีแต่เลขที่ใบเสร็จส่งมา  
                        {
                            BPMServerBatchHelper.RemoveHeaderFooter(file);
                            _bpmSAPService.Import_CancelBPMPayment_50A(param);
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

                return true;
            }
            catch (Exception ex)
            {
                _logger.Severity = Severity.Level9;
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, ex.ToString(), string.Format("Extracting file failed, {0}", file));
                return false;
            }
        }

        private bool HandleError(Guid batchKey, string path, string file, string key, string tb)
        {
            string destFile = null;
            string source = null;
            try
            {
                ACADepLineInfo dep = new ACADepLineInfo();
                dep.DLId = DL_NAME_ID;
                dep.FileKey = key;
                dep.LastFailTb = tb;
                dep.Status = "1"; //fail
                _depMgr.SetStatus(dep, false);

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
            catch (Exception)
            {
                _logger.Severity = Severity.Level6;
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, "เคลื่อนย้ายไฟล์หลังการประมวลผลเกิดข้อผิดพลาด กรุณาตรวจสอบสิทธิการเขียนข้อมูลปลายทาง",
                        string.Format("ต้นทาง: {0} \nปลายทาง: {1}", source, destFile));
                return false;
            }
        }

        private bool HandleSuccess(Guid batchKey, string path, string file)
        {
            string destFile = null;
            string source = null;
            try
            {
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
            catch (Exception)
            {
                _logger.Severity = Severity.Level6;
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, "เคลื่อนย้ายไฟล์หลังการประมวลผลเกิดข้อผิดพลาด กรุณาตรวจสอบสิทธิการเขียนข้อมูลปลายทาง",
                        string.Format("ต้นทาง: {0} \nปลายทาง: {1}", source, destFile));
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
                    if (string.IsNullOrEmpty(fileKey)) //critical error
                    {
                        _logger.Severity = Severity.Level7;
                        _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, "Critical error, please call Avanade.");
                        context.Status = StatusCode.Failed;
                        context.Commit();
                        return;
                    }
                    else //recover last fail table
                    {
                        _activeFileKey = depLine.FileKey;
                    }
                }
                //*** start process data to tables
                List<string> tables = _depMgr.GetDependencyLineTable(DL_NAME_ID);
                Hashtable lastFailHt = _depMgr.GetAllLastFailHt(DL_NAME_ID);

                foreach (string tb in tables)
                {
                    //this table failed last time, so we don't need to overwrite data in table
                    if (lastFailHt.ContainsKey(tb) && lastFailHt.ContainsValue(_activeFileKey))
                        _overwrite = false;
                    else
                        _overwrite = true;

                    BPMServerBatchHelper.MoveFileToProcess(DL_FULL_NAME, tb);
                    //build target path for each table
                    string filePath = string.Format("{0}\\{1}", _filePool, tb);
                    List<string> ifiles = SAPFile.GetFileNameX(filePath, _activeFileKey);

                    //iterate all files
                    int numOfSuccess = 0;
                    string errMsg = string.Empty;
                    foreach (string file in ifiles)
                    {
                        if (!ImportData(batchKey, tb, file, _overwrite)) //unacceptable error
                        {
                            HandleError(batchKey, string.Format("{0}\\{1}", _filePool, tb), file, _activeFileKey, tb);
                        }
                        else
                        {
                            HandleSuccess(batchKey, string.Format("{0}\\{1}", _filePool, tb), file);
                            numOfSuccess++;
                            if (numOfSuccess == ifiles.Count)
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
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, e.ToString(), "เกิดปัญหาการเรียกฟังก์ชันในการประมวลผลแบ็ช");
                context.Status = StatusCode.Failed;
                context.Commit();
                GC.Collect();
            }
        }

        #endregion
    }
}

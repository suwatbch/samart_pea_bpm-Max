using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Avanade.ACA.Batch;
using System.Configuration;
using System.Collections;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using PEA.BPM.Integration.BPMIntegration.Interface.Services;
using PEA.BPM.Integration.BPMIntegration.BS;
using PEA.BPM.Integration.ACABatchService;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;

namespace PEA.BPM.Integration.BPMIntegration.BPMServerBatch
{
    public class DL003_BILLING_JOB : IJob
    {
        private const string DL_NAME_ID = "DL003";
        private const string DL_FULL_NAME = "DL003_BILLING";
        private const string _masterFileName = "billdata.txt";

        private ACABatchLogger _logger;
        private ACADepLineMgr _depMgr;
        private IBpmSAPService _bpmSAPService;
        private string _filePool;
        private string _billPath;
        private FileInfo _procFile;
        private string _errMsg;
        private int numOfCol = -1;
        private bool _skipError= false;
        private bool _success = true;


        public DL003_BILLING_JOB()
        {
            _bpmSAPService = new BPMSAPService();
            _filePool = BPMServerBatchHelper.GetDataProcessingPath(DL_FULL_NAME);
            _logger = ACABatchLogger.GetInstance(DL_FULL_NAME);
            _depMgr = new ACADepLineMgr();
        }

        public bool Validate(string fileName, ref string errMsg)
        {
            return true; //no validation now!
            bool ret = true;
            try
            {
                using (StreamReader sd = new StreamReader(fileName, Encoding.Default))
                {
                    //Remove Header and Footer
                    while (!sd.EndOfStream)
                    {
                        string fields = sd.ReadLine();
                        string[] sp = fields.Split('\t');
                        if (numOfCol == -1)
                            numOfCol = sp.Length;
                        else if(numOfCol != sp.Length)
                            ret = false;
                    }
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return false;
            }

            return ret;
        }

        private bool MoveFile(Guid batchKey, string source, string destination)
        {
            try
            {
                if (File.Exists(destination))
                {
                    string altFileName =  string.Format("{0}.old", destination);
                    if (!File.Exists(altFileName))
                        File.Move(source, altFileName);

                    //if file still exist, just ignore saving
                    return true;
                }
                    
                File.Move(source, destination);
                return true;
            }
            catch (Exception e)
            {
                _logger.Severity = Severity.Level6;
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, e.Message, string.Format("มีปัญหาในการย้ายไฟล์ กรุณาตรวจสอบสิทธิการเขียนข้อมูลปลายทาง : {0}", 
                    string.Format("ต้นทาง: {0} \nปลายทาง: {1}", source, destination)));
                return false;
            }
        }

        #region IJob Members
        //******** this job MUST be run on database server only

        public void Execute(JobExecutionContext context)
        {
            Guid batchKey = context.BatchExecutionContext.BatchExecutionContextData.Key;
            context.Status = StatusCode.Executing;
            context.Commit();

            try
            {
                //only one subfolder in pool for billing
                List<string> tables = _depMgr.GetDependencyLineTable(DL_NAME_ID);
                BPMServerBatchHelper.MoveFileToProcess(DL_FULL_NAME, tables[0], "INVOICE");

                _billPath = string.Format("{0}\\{1}", _filePool, tables[0]);
                string[] fileList = Directory.GetFiles(_billPath);
                Array.Sort(fileList, new CompareFileInfo());
                _logger.InterfaceId = "INVOICE";

                if (fileList.Length >= 1) //contain master file & at least one billing data file
                {
                    //process each file in the directory
                    foreach (string ifile in fileList)
                    {
                        try
                        {
                            _procFile = new FileInfo(ifile);
                            ACABatchParam param = new ACABatchParam();
                            param.FileName = ifile;
                            param.ShortFileName = _procFile.Name;
                            param.BulkFormatPath = BPMServerBatchHelper.BulkFormatPath;

                            _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessData, string.Format("Import {0} Started at {1}", _procFile.Name, DateTime.Now.ToLongTimeString()));

                            //*** validate sap file 
                            if (!Validate(_procFile.FullName, ref _errMsg))
                            {
                                //write log
                                _logger.Severity = Severity.Level6;
                                _logger.WriteLog(batchKey, ACABatchLogger.LogType.FileValidation, _errMsg, _procFile.FullName);
                                _success = MoveFile(batchKey, _procFile.FullName, string.Format("{0}.Failed\\{1}", _billPath, _procFile.Name));
                                if (!_success) throw new Exception("ย้ายไฟล์ผิดพลาด"); //logged in movefile function
                                _skipError = true;
                                continue; //read next file
                            }

                            int rows = _bpmSAPService.Import_BillingData(param);

                            GC.Collect();

                            _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success, "Import Success", _procFile.Name, rows);

                            //move the file to new name - with time stamp
                            _success = MoveFile(batchKey, _procFile.FullName, string.Format("{0}.Succeeded\\{1}", _billPath, _procFile.Name));                            
                            if (!_success) throw new Exception("ย้ายไฟล์ผิดพลาด"); //logged in movefile function

                            _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success, string.Format("Import {0} Done at {1}", _procFile.Name, DateTime.Now.ToLongTimeString()));

                        }
                        catch (Exception e)
                        {
                            _logger.Severity = Severity.Level6;
                            _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, e.ToString(), _procFile.FullName);
                            _success = MoveFile(batchKey, _procFile.FullName, string.Format("{0}.Failed\\{1}", _billPath, _procFile.Name));
                            if (!_success) throw new Exception("ย้ายไฟล์ผิดพลาด");
                            _skipError = true; 
                            continue; //read next file                           
                        }
                    }
                }

                if (_skipError)
                    context.Status = StatusCode.Failed;
                else
                    context.Status = StatusCode.Success;

                context.Commit();

            }
            catch (Exception ex) //error write log
            {
                _logger.Severity = Severity.Level10;
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.FileValidation, ex.ToString(), "เกิดปัญหาการเรียกฟังก์ชันในการประมวลผลแบ็ช");
                context.Status = StatusCode.Failed;
                context.Commit();
                GC.Collect();
            }
        }

        #endregion      
     
    }


}

using System;
using System.Collections.Generic;
using System.Text;
using Avanade.ACA.Batch;
using PEA.BPM.Integration.ACABatchService;
using System.Configuration;
using PEA.BPM.Integration.BPMIntegration.BS;
using PEA.BPM.Integration.BPMIntegration.Interface.Services;
using PEA.BPM.Integration.BPMIntegration.Interface.ExportEntities;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using BPMSAPConnector;
using System.ComponentModel;
using System.Threading;
using System.IO;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;
using System.Linq;

namespace PEA.BPM.Integration.BPMIntegration.BPMServerBatch
{
    /// <summary>
    /// AgencyCommission (comm, fine),PaymentData(join tbs)
    /// </summary>
    public class DL007_EXPORT_TO_SAP_JOB : IJob
    {
        private const string DL_FULL_NAME = "DL007_EXPORT_TO_SAP";

        private SAPPort _sapPort;
        private ACADepLineMgr _depMgr;
        private ACABatchLogger _logger;
        private IBpmSAPService _bpmSAPService;
        private string _filePool;
        
        public DL007_EXPORT_TO_SAP_JOB()
        {
            _sapPort = new SAPPort();
            _bpmSAPService = new BPMSAPService();
            _depMgr = new ACADepLineMgr();
            _logger = ACABatchLogger.GetInstance(DL_FULL_NAME);
            _filePool = BPMServerBatchHelper.GetDataProcessingPath(DL_FULL_NAME);
        }

        private void TriggerSAP(string interfaceName)
        {
            // Wait Robocopy to synch file.
            Thread.Sleep(60000);

            string conn = ConfigurationManager.AppSettings["SAP_CONN"];            
            //BPMSAPSUBMIT submit = new BPMSAPSUBMIT(conn);
            //submit.Timeout = 60 * 1000;
            //submit.Zpos_Submit(interfaceName);          
        }

        private string ExportData<T>(List<T> exportList, string interfaceName,
            decimal? actualAmount, decimal? amount, decimal? overUnder) where T : IListUtility<T>, new()
        {
            string outputFileName = string.Format(@"{0}\{1}\{2}", _filePool, interfaceName, _bpmSAPService.GetExportFileName(interfaceName));
            _sapPort.OutFileName = outputFileName;
            _sapPort.SendList<T>(exportList, actualAmount, amount, overUnder);
            //TODO: Uncomment for OK File  ***** BEGIN
            ////Create OK File
            //_sapPort.OutFileName = outputFileName.Replace(".txt",".OK");
            //_sapPort.Send("");
            //TODO: Uncomment for OK File  ***** END

            return outputFileName;
        }


        private void EnqueueBatchJob(string batchName, string destination)
        {
            try
            {
                BatchExecutionRequest request = new BatchExecutionRequest(batchName);
                request.BatchClientName = "";
                request.Destination = destination;
                request.StartPollingForResultAfter = TimeSpan.FromMinutes(5);
                BatchExecutionRequestQueue queue = new BatchExecutionRequestQueue("ACABatch_SQL");
                queue.Enqueue(request);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    
        private void SendFileToOutBound(string interfaceName, Guid batchKey)
        {
            BPMServerBatchHelper.MoveFileToOutbound(DL_FULL_NAME, interfaceName);
            //TODO: Uncomment for OK File  ***** BEGIN
            //DateTime now = DateTime.Now;
            //string year = now.ToString("yyyy", new System.Globalization.CultureInfo("en-US"));
            //string month = now.ToString("yyyyMM", new System.Globalization.CultureInfo("en-US"));
            //string day = now.ToString("yyyyMMdd", new System.Globalization.CultureInfo("en-US"));
            //string preOutboundPath = string.Format(@"{0}", BPMServerBatchHelper.GetPreOutboundPath());
            //string backupPath = string.Format(@"{0}{1}\{2}\{3}\", preOutboundPath, year, month, day);

            //if (!Directory.Exists(string.Format(@"{0}{1}", preOutboundPath, year)))
            //{
            //    Directory.CreateDirectory(string.Format(@"{0}{1}", preOutboundPath, year));
            //}

            //if (!Directory.Exists(string.Format(@"{0}{1}\{2}\", preOutboundPath, year, month)))
            //{
            //    Directory.CreateDirectory(string.Format(@"{0}{1}\{2}\", preOutboundPath, year, month));
            //}

            //if (!Directory.Exists(string.Format(@"{0}{1}\{2}\{3}\", preOutboundPath, year, month, day)))
            //{
            //    Directory.CreateDirectory(string.Format(@"{0}{1}\{2}\{3}\", preOutboundPath, year, month, day));
            //}
            

            //BPMServerBatchHelper.MoveFileToOutbound(DL_FULL_NAME, interfaceName, backupPath);
            //BPMServerBatchHelper.MoveFileOKToOutbound(DL_FULL_NAME, interfaceName, backupPath);
            //TODO: Uncomment for OK File  ***** END

            //TriggerSAP(interfaceName);
            //_logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessData, string.Format("Trigger SAP for interface {0} at {1}", interfaceName, DateTime.Now.ToLongTimeString()));
        }

        #region IJob Members

        public void Execute(JobExecutionContext context)
        {
            string interfaceName = "";
            string outputFileName = "";
            string suggt = "";
            context.Status = StatusCode.Executing;
            context.Commit();
            Guid batchKey = context.BatchExecutionContext.BatchExecutionContextData.Key;

            try
            {
                ACABatchParam param = new ACABatchParam();
                param.BatchKey = batchKey;
                param.BranchId = null;

                //check regional close work
                _bpmSAPService.MarkBranchPrefix();
                
                suggt = "(1)��¡�ä�һ�Ѻ��Ф�ҵͺ᷹�����俿�ҵ�ͧ���������᷹���Թ";
                List<AG_Compensation> exportList1 = _bpmSAPService.Export_AgencyCompensation(null, param);
                if (exportList1.Count > 0)
                {
                    interfaceName = "TRP0010";
                    outputFileName = string.Format(@"{0}\{1}\{2}", _filePool, interfaceName, _bpmSAPService.GetExportFileName(interfaceName));
                    _sapPort.OutFileName = outputFileName;
                    _sapPort.SendList<AG_Compensation>(exportList1);
                    SendFileToOutBound(interfaceName, batchKey);

                    _logger.InterfaceId = interfaceName;
                    _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success,
                        string.Format("Export {0} {1} Done at {2}", suggt, outputFileName, DateTime.Now.ToLongTimeString()));
                }

                suggt = "(2)�����š���Ѻ����˹�����к�˹�ҧҹ ����Ѻ��¡�û��Է���ա�õ��˹������� SAP (���Ţ������˹��)";
                List<AR_Normal> exportList2 = _bpmSAPService.Export_ARNormal(null);
                if (exportList2.Count > 0)
                {
                    interfaceName = "TRP0020A";
                    AR_Normal data = exportList2[exportList2.Count - 1];
                    outputFileName = ExportData<AR_Normal>(exportList2, interfaceName,
                        data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                    SendFileToOutBound(interfaceName, batchKey);

                    _logger.InterfaceId = interfaceName;
                    _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success,
                        string.Format("Export {0} {1} Done at {2}", suggt, outputFileName, DateTime.Now.ToLongTimeString()));
                }

                suggt = "(3)�����š���Ѻ����˹�����к�˹�ҧҹ ��¡�èҡ Spot Bill ����ѧ������Ѻ�ҡ SAP";
                List<AR_SpotBill> exportList3 = _bpmSAPService.Export_ARSpotBill(null);
                if (exportList3.Count > 0)
                {
                    interfaceName = "TRP0020B";
                    AR_SpotBill data = exportList3[exportList3.Count - 1];
                    outputFileName = ExportData<AR_SpotBill>(exportList3, interfaceName,
                        data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                    SendFileToOutBound(interfaceName, batchKey);

                    _logger.InterfaceId = interfaceName;
                    _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success,
                        string.Format("Export {0} {1} Done at {2}", suggt, outputFileName, DateTime.Now.ToLongTimeString()));

                }

                suggt = "(4)�����š���Ѻ����˹�����к�˹�ҧҹ ����Ѻ��¡�÷���Ѻ���м�ҹ Group Invoice";
                List<AR_GrpInv> exportList4 = _bpmSAPService.Export_ARGroupInvoice(null);

                //// CBS2 Defect Log #103 
                //// "��� ��� �ӡ�� export DL007_EXPORT_TO_SAP_JOB
                //// �� ⿡�� ��� TRP0020C
                //// �зӡ���¡��� ��� Export �����͹� ��� 
                //// 1) CaId 092 ���� 091
                //// 2) DisconnectionDoc = ""CS"""
                //// 2021-Nov-11 Uthen.P
                if (exportList4.Count > 0)
                {
                    interfaceName = "TRP0020C";

                    //AR_GrpInv data;

                    // 2021-11-03  : DCR Group invoice.
                    // ���俿��

                    // ���俿��
                    var exportList4_Elec = new List<AR_GrpInv>();

                    exportList4_Elec = (from e in exportList4
                                        where (e.CaId.Substring(0, 3) != "091" && e.CaId.Substring(0, 3) != "092") && e.DisconnectDoc != "CS"
                                        select e).ToList();
                    if (exportList4_Elec.Count > 0)
                    {   //201803241011 Kanokwan.L ��� Sum Footer ������ç�Ѻ Data
                        //data = exportList4_Elec[exportList4_Elec.Count - 1];
                        //outputFileName = ExportData<AR_GrpInv>(exportList4_Elec, interfaceName,
                        //                        data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);

                        decimal elec_ActualAmount = exportList4_Elec.Select(s => s.ActualAmount).Sum().GetValueOrDefault(0);
                        decimal elec_Amount = exportList4_Elec.Select(s => s.Amount).Sum().GetValueOrDefault(0);
                        decimal elec_OverUnder = exportList4_Elec.Select(s => s.OverUnder).Sum().GetValueOrDefault(0);

                        outputFileName = ExportData<AR_GrpInv>(exportList4_Elec, interfaceName,
                                               elec_ActualAmount, elec_Amount, elec_OverUnder);
                        SendFileToOutBound(interfaceName, batchKey);
                    }
                    _logger.InterfaceId = interfaceName;
                    _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success,
                        string.Format("Export {0} {1} Done at {2}", suggt, outputFileName, DateTime.Now.ToLongTimeString()));

                    // ��Һ�ԡ�� 
                    var exportList4_Service = new List<AR_GrpInv>();
                    exportList4_Service = (from e in exportList4
                                           where (e.CaId.Substring(0, 3) == "091" || e.CaId.Substring(0, 3) == "092") && e.DisconnectDoc != "CS"
                                           select e).ToList();
                    if (exportList4_Service.Count > 0)
                    {   //201803241011 Kanokwan.L ��� Sum Footer ������ç�Ѻ Data
                        //data = exportList4_Service[exportList4_Service.Count - 1];
                        //outputFileName = ExportData<AR_GrpInv>(exportList4_Service, interfaceName,
                        //                        data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);

                        decimal service_ActualAmount = exportList4_Service.Select(s => s.ActualAmount).Sum().GetValueOrDefault(0);
                        decimal service_Amount = exportList4_Service.Select(s => s.Amount).Sum().GetValueOrDefault(0);
                        decimal service_OverUnder = exportList4_Service.Select(s => s.OverUnder).Sum().GetValueOrDefault(0);

                        outputFileName = ExportData<AR_GrpInv>(exportList4_Service, interfaceName,
                                                  service_ActualAmount, service_Amount, service_OverUnder);
                        SendFileToOutBound(interfaceName, batchKey);
                    }
                    _logger.InterfaceId = interfaceName;
                    _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success,
                        string.Format("Export {0} {1} Done at {2}", suggt, outputFileName, DateTime.Now.ToLongTimeString()));


                    // 20211203 : Savake ������� export ������ Group invoice.
                    // Group invoice ��� DisconnectDoc = "CS"
                    var exportList4_GroupService = new List<AR_GrpInv>();
                    exportList4_GroupService = (from e in exportList4
                                           where (e.CaId.Substring(0, 3) == "091" || e.CaId.Substring(0, 3) == "092") && e.DisconnectDoc == "CS"
                                           select e).ToList();
                    if (exportList4_GroupService.Count > 0)
                    {  

                        decimal groupInv_ActualAmount   = exportList4_GroupService.Select(s => s.ActualAmount).Sum().GetValueOrDefault(0);
                        decimal groupInv_Amount         = exportList4_GroupService.Select(s => s.Amount).Sum().GetValueOrDefault(0);
                        decimal groupInv_OverUnder      = exportList4_GroupService.Select(s => s.OverUnder).Sum().GetValueOrDefault(0);

                        outputFileName = ExportData<AR_GrpInv>(exportList4_GroupService, interfaceName,
                                                  groupInv_ActualAmount, groupInv_Amount, groupInv_OverUnder);
                        SendFileToOutBound(interfaceName, batchKey);
                    }
                    _logger.InterfaceId = interfaceName;
                    _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success,
                        string.Format("Export {0} {1} Done at {2}", suggt, outputFileName, DateTime.Now.ToLongTimeString()));


                }


                suggt = "(5)�����š���Ѻ����˹�����к�˹�ҧҹ ����Ѻ��¡�÷���ա���Ѻ���м�ҹ���᷹���Թ";
                List<AR_Agency> exportList5 = _bpmSAPService.Export_ARPaidByAgency(null);
                if (exportList5.Count > 0)
                {
                    interfaceName = "TRP0020D";
                    AR_Agency data = exportList5[exportList5.Count - 1];
                    outputFileName = ExportData<AR_Agency>(exportList5, interfaceName,
                        data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                    SendFileToOutBound(interfaceName, batchKey);

                    _logger.InterfaceId = interfaceName;
                    _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success,
                        string.Format("Export {0} {1} Done at {2}", suggt, outputFileName, DateTime.Now.ToLongTimeString()));

                }


                suggt = "(6)�����š���Ѻ����˹�����к�˹�ҧҹ ����Ѻ��¡�ü�͹����";
                List<AR_PartialPay> exportList6 = _bpmSAPService.Export_ARPartialPayment(null);
                if (exportList6.Count > 0)
                {
                    interfaceName = "TRP0020E";
                    AR_PartialPay data = exportList6[exportList6.Count - 1];
                    outputFileName = ExportData<AR_PartialPay>(exportList6, interfaceName,
                        data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                    SendFileToOutBound(interfaceName, batchKey);

                    _logger.InterfaceId = interfaceName;
                    _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success,
                        string.Format("Export {0} {1} Done at {2}", suggt, outputFileName, DateTime.Now.ToLongTimeString()));
                }


                suggt = "(7)��¡��˹�����Ѻ����˹�ҧҹ ���ѧ����ա�õ��˹����к� ���� (������Ţ������˹��)";
                List<ARn_Normal> exportList7 = _bpmSAPService.Export_ARNonInvNormal(null);
                if (exportList7.Count > 0)
                {
                    interfaceName = "TRP0030A";
                    ARn_Normal data = exportList7[exportList7.Count - 1];
                    outputFileName = ExportData<ARn_Normal>(exportList7, interfaceName,
                        data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                    SendFileToOutBound(interfaceName, batchKey);

                    _logger.InterfaceId = interfaceName;
                    _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success,
                        string.Format("Export {0} {1} Done at {2}", suggt, outputFileName, DateTime.Now.ToLongTimeString()));

                }


                suggt = "(8)��¡��˹�����Ѻ����˹�ҧҹ ���ѧ����ա�õ��˹����к� - Cash Sales (������Ţ������˹��)";
                List<ARn_CashSale> exportList8 = _bpmSAPService.Export_ARNonInvCashSale(null);
                if (exportList8.Count > 0)
                {
                    interfaceName = "TRP0030B";
                    ARn_CashSale data = exportList8[exportList8.Count - 1];
                    outputFileName = ExportData<ARn_CashSale>(exportList8, interfaceName,
                        data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                    SendFileToOutBound(interfaceName, batchKey);

                    _logger.InterfaceId = interfaceName;
                    _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success,
                        string.Format("Export {0} {1} Done at {2}", suggt, outputFileName, DateTime.Now.ToLongTimeString()));

                }


                suggt = "(9)��¡��˹�����Ѻ����˹�ҧҹ ���ѧ����ա�õ��˹����к� �Թ��ǧ˹�� 30%";
                List<ARn_AdvPayment> exportList9 = _bpmSAPService.Export_ARNonInvAdvancePayment(null);
                if (exportList9.Count > 0)
                {
                    interfaceName = "TRP0030C";
                    ARn_AdvPayment data = exportList9[exportList9.Count - 1];
                    outputFileName = ExportData<ARn_AdvPayment>(exportList9, interfaceName,
                        data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                    SendFileToOutBound(interfaceName, batchKey);

                    _logger.InterfaceId = interfaceName;
                    _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success,
                        string.Format("Export {0} {1} Done at {2}", suggt, outputFileName, DateTime.Now.ToLongTimeString()));
                }


                suggt = "(10)��¡��˹�����Ѻ����˹�ҧҹ ���ѧ����ա�õ��˹����к�  ����Ѻ�Ҩҡ���˹��";
                List<ARn_APPayment> exportList10 = _bpmSAPService.Export_APPayment(null);
                if (exportList10.Count > 0)
                {
                    interfaceName = "TRP0030E";
                    ARn_APPayment data = exportList10[exportList10.Count - 1];
                    outputFileName = ExportData<ARn_APPayment>(exportList10, interfaceName,
                        data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                    SendFileToOutBound(interfaceName, batchKey);

                    _logger.InterfaceId = interfaceName;
                    _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success,
                        string.Format("Export {0} {1} Done at {2}", suggt, outputFileName, DateTime.Now.ToLongTimeString()));
                }


                suggt = "(11)��¡��˹�����Ѻ����˹�ҧҹ ���ѧ����ա�õ��˹����к� - ������Ѻ�ҡ";
                List<AR_DepositReceipt> exportList11 = _bpmSAPService.Export_ARDepositReceipt(null);
                if (exportList11.Count > 0)
                {
                    interfaceName = "TRP0040A";
                    AR_DepositReceipt data = exportList11[exportList11.Count - 1];
                    outputFileName = ExportData<AR_DepositReceipt>(exportList11, interfaceName,
                        data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                    SendFileToOutBound(interfaceName, batchKey);

                    _logger.InterfaceId = interfaceName;
                    _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success,
                        string.Format("Export {0} {1} Done at {2}", suggt, outputFileName, DateTime.Now.ToLongTimeString()));
                }


                suggt = "(12)��¡�÷������Թ���˹������Ӥѭ����";
                List<AP_Payment> exportList12 = _bpmSAPService.Export_APVoucher(null);
                if (exportList12.Count > 0)
                {
                    interfaceName = "TRP0050A";
                    AP_Payment data = exportList12[exportList12.Count - 1];
                    outputFileName = ExportData<AP_Payment>(exportList12, interfaceName,
                        data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                    SendFileToOutBound(interfaceName, batchKey);

                    _logger.InterfaceId = interfaceName;
                    _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success,
                        string.Format("Export {0} {1} Done at {2}", suggt, outputFileName, DateTime.Now.ToLongTimeString()));
                }


                suggt = "(13)��¡�ù��Թ�ҡ��Ҥ��";
                List<AP_BankPayIn> exportList13 = _bpmSAPService.Export_APBankPayIn(null);
                if (exportList13.Count > 0)
                {
                    interfaceName = "TRP0050B";
                    AP_BankPayIn data = exportList13[exportList13.Count - 1];
                    outputFileName = ExportData<AP_BankPayIn>(exportList13, interfaceName,
                        data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                    SendFileToOutBound(interfaceName, batchKey);

                    _logger.InterfaceId = interfaceName;
                    _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success,
                        string.Format("Export {0} {1} Done at {2}", suggt, outputFileName, DateTime.Now.ToLongTimeString()));
                }


                suggt = "(14)�����š���Ѻ����˹����俿�Ңͧ�к� E-Payment";
                List<AR_Normal> exportList14 = _bpmSAPService.Export_MPayARNormal(null);
                if (exportList14.Count > 0)
                {
                    interfaceName = "TRP0020A";
                    AR_Normal data = exportList14[exportList14.Count - 1];
                    outputFileName = ExportData<AR_Normal>(exportList14, interfaceName,
                        data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                    SendFileToOutBound(interfaceName, batchKey);

                    _logger.InterfaceId = interfaceName;
                    _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success,
                        string.Format("Export {0} {1} Done at {2}", suggt, outputFileName, DateTime.Now.ToLongTimeString()));
                }


                suggt = "(15)��¡��˹�����Ѻ����˹��ͧ���᷹��к� E-Payment";
                List<ARn_AdvPayment> exportList15 = _bpmSAPService.Export_MPayARNonInvAdvancePayment(null);
                if (exportList15.Count > 0)
                {
                    interfaceName = "TRP0030C";
                    ARn_AdvPayment data = exportList15[exportList15.Count - 1];
                    outputFileName = ExportData<ARn_AdvPayment>(exportList15, interfaceName,
                        data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                    SendFileToOutBound(interfaceName, batchKey);

                    _logger.InterfaceId = interfaceName;
                    _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success,
                        string.Format("Export {0} {1} Done at {2}", suggt, outputFileName, DateTime.Now.ToLongTimeString()));
                }


                suggt = "(16)��¡��˹�����Ѻ����˹����������Ѻ�ҡ��к� E-Payment";
                List<AR_DepositReceipt> exportList16 = _bpmSAPService.Export_MPayARDepositReceipt(null);
                if (exportList16.Count > 0)
                {
                    interfaceName = "TRP0040A";
                    AR_DepositReceipt data = exportList16[exportList16.Count - 1];
                    outputFileName = ExportData<AR_DepositReceipt>(exportList16, interfaceName,
                        data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                    SendFileToOutBound(interfaceName, batchKey);

                    _logger.InterfaceId = interfaceName;
                    _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success,
                        string.Format("Export {0} {1} Done at {2}", suggt, outputFileName, DateTime.Now.ToLongTimeString()));
                }


                suggt = "(17)��¡��˹�����Ѻ����˹�ҧҹ ���ѧ����ա�õ��˹����к�  ����Ѻ�Ҩҡ���˹��";
                List<ARn_APPayment> exportList17 = _bpmSAPService.Export_APChequePayment(null);
                if (exportList17.Count > 0)
                {
                    interfaceName = "TRP0030E";
                    ARn_APPayment data = exportList17[exportList17.Count - 1];
                    outputFileName = ExportData<ARn_APPayment>(exportList17, interfaceName,
                        data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                    SendFileToOutBound(interfaceName, batchKey);

                    _logger.InterfaceId = interfaceName;
                    _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success,
                        string.Format("Export {0} {1} Done at {2}", suggt, outputFileName, DateTime.Now.ToLongTimeString()));
                }

                suggt = "(18)�����š���Ѻ����˹�����к�˹�ҧҹ ����Ѻ��¡�û��Է���ա�õ��˹������� SAP (���Ţ������˹��) ����¡�ä�ҵ�͡�Ѻ������";
                List<AR_Reconnect> exportList18 = _bpmSAPService.Export_ARReconnect(null);
                if (exportList18.Count > 0)
                {
                    interfaceName = "TRP0020F";
                    AR_Reconnect data = exportList18[exportList18.Count - 1];
                    outputFileName = ExportData<AR_Reconnect>(exportList18, interfaceName,
                        data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                    SendFileToOutBound(interfaceName, batchKey);

                    _logger.InterfaceId = interfaceName;
                    _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success,
                        string.Format("Export {0} {1} Done at {2}", suggt, outputFileName, DateTime.Now.ToLongTimeString()));
                }

                //logging the exported file, update running and reset mark
                _bpmSAPService.ProcessExportDone();

                context.Status = StatusCode.Success;
                context.Commit();

                if (_bpmSAPService.CheckAnotherRegionalToExport())
                {
                    //EnqueueBatchJob("DL007_EXPORT_TO_SAP_BATCH", "BPMDbServer");
                    _logger.WriteLog(batchKey, ACABatchLogger.LogType.Success, string.Format("Enqueued DL007_EXPORT_TO_SAP_BATCH ����Ѻ export ��¡�õ���"));
                }
            }
            catch (Exception e)
            {
                if (File.Exists(outputFileName))
                {
                    _bpmSAPService.DecreaseInterfaceRunningNumber(interfaceName);
                    File.Delete(outputFileName);
                }
                _logger.Severity = Severity.Level9;
                _logger.WriteLog(batchKey, ACABatchLogger.LogType.ProcessingError, e.ToString(), string.Format("�Դ�ѭ�ҡ�����¡�ѧ��ѹ㹡�û����ż��� - {0}", suggt));
                context.Status = StatusCode.Failed;
                context.Commit();
            }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Integration.BPMIntegration.BPMServerBatch;
using PEA.BPM.Integration.BPMIntegration;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using PEA.BPM.Integration.BPMIntegration.Interface.Services;
using PEA.BPM.Integration.BPMIntegration.BS;
using PEA.BPM.Integration.BPMIntegration.Interface.ExportEntities;
using PEA.BPM.Integration.ACABatchService;
using System.IO;
using System.Configuration;
using System.Globalization;

namespace BPMconsole
{
    public class Program
    {
        private const string DL_NAME_ID = "DL05";
        private const string DL_FULL_NAME = "DL05_PAYFROMSAP";


        static void Main()
        {
            ////***********************************************
            ////*******************Import**********************
            ////***********************************************
            //SAPPort _sapPort = new SAPPort();
            //IBpmSAPService _bpmSAPService = new BPMSAPService();
            //ACADepLineMgr _depMgr = new ACADepLineMgr();
            //Guid batchKey = Guid.NewGuid();

            //bool ret = false;
            ////AR
            //string file = @"C:\TRR0010B0000000015.txt";
            //string bulkFile = @"C:\Interface\ardata.txt";
            ////PayFromSAP
            //string PSfile = @"C:\TRR0020B000019410.txt";
            //string PSbulkFile = @"C:\Interface\pfsdata.txt";
            //string _filePool = @"C:\";
            //_sapPort.InFileName = file;
            //ACABatchParam param = new ACABatchParam();
            //param.Overwrite = false;
            //param.FileName = file;
            //param.BatchKey = Guid.NewGuid();

            //char[] spliters = { '\\' };
            //string[] fileList = file.Split(spliters);
            //string fileType = GetARFileType(fileList[fileList.Length - 1]);


            //try
            //{
            //    //List<AR> dataList = _sapPort.Receive<AR>();
            //    //ret = _bpmSAPService.Import_AR(dataList, param, fileType);

            //    //List<PayFromSAPInfo> dataList = _sapPort.Receive<PayFromSAPInfo>();
            //    ret = _bpmSAPService.Import_PayFromSAP(PSfile, PSbulkFile, param, _filePool, "TRR0020B");
            //    //ret = _bpmSAPService.Import_AR(file, bulkFile, param, _filePool, fileType);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}






            //**********************************************************
            //***************************Export*************************
            //**********************************************************
            SAPPort _sapPort = new SAPPort();
            IBpmSAPService _bpmSAPService = new BPMSAPService();
            ACADepLineMgr _depMgr = new ACADepLineMgr();
            string _filePool = ConfigurationManager.AppSettings["EXPORT_PATH"];
            string interfaceName = "";

            ACABatchParam param = new ACABatchParam();
            param.BatchKey = Guid.Empty;
            param.BranchId = null;


            try
            {
                //List<AG_Compensation> exportList = _bpmSAPService.Export_AgencyCompensation(null, param);
                //if (exportList.Count > 0)
                //{
                //    interfaceName = "TRP0010";
                //    _sapPort.OutFileName = string.Format("{0}\\{1}", _filePool, _bpmSAPService.GetExportFileName("TRP0010"));
                //    _sapPort.SendList<AG_Compensation>(exportList);
                //}

                //// CBS2 Defect Log#103 Uthen.P
                //List<AR_Normal> exportList1 = _bpmSAPService.Export_ARNormal("A10101");
                //List<AR_Normal> exportList1 = _bpmSAPService.Export_ARNormal(null);
                //if (exportList1.Count > 0)
                //{
                //    interfaceName = "TRP0020A";
                //    AR_Normal data = exportList1[exportList1.Count - 1];
                //    _sapPort.OutFileName = string.Format("{0}\\{1}", _filePool, _bpmSAPService.GetExportFileName("TRP0020A"));
                //    _sapPort.SendList<AR_Normal>(exportList1, data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                //    //Create OK File
                //    _sapPort.OutFileName = _sapPort.OutFileName.Replace(".txt", ".OK");
                //    _sapPort.Send("");
                //    //SendFileToOutBound(interfaceName);
                //}


                //List<AR_SpotBill> exportList2 = _bpmSAPService.Export_ARSpotBill(null);
                //if (exportList2.Count > 0)
                //{
                //    interfaceName = "TRP0020B";
                //    AR_SpotBill data = exportList2[exportList2.Count - 1];
                //    _sapPort.OutFileName = string.Format("{0}\\{1}", _filePool, _bpmSAPService.GetExportFileName("TRP0020B"));
                //    _sapPort.SendList<AR_SpotBill>(exportList2, data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                //}

                //// CBS2 Defect Log#103 Uthen.P
                List<AR_GrpInv> exportList3 = _bpmSAPService.Export_ARGroupInvoice("A10101");
                //List<AR_GrpInv> exportList3 = _bpmSAPService.Export_ARGroupInvoice(null);
                if (exportList3.Count > 0)
                {
                    interfaceName = "TRP0020C";
                    AR_GrpInv data = exportList3[exportList3.Count - 1];
                    _sapPort.OutFileName = string.Format("{0}\\{1}", _filePool, _bpmSAPService.GetExportFileName("TRP0020C"));
                    _sapPort.SendList<AR_GrpInv>(exportList3, data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                }


                //List<AR_Agency> exportList4 = _bpmSAPService.Export_ARPaidByAgency(null);
                //if (exportList4.Count > 0)
                //{
                //    interfaceName = "TRP0020D";
                //    AR_Agency data = exportList4[exportList4.Count - 1];
                //    _sapPort.OutFileName = string.Format("{0}\\{1}", _filePool, _bpmSAPService.GetExportFileName("TRP0020D"));
                //    _sapPort.SendList<AR_Agency>(exportList4, data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                //}


                //List<AR_PartialPay> exportList5 = _bpmSAPService.Export_ARPartialPayment(null);
                //if (exportList5.Count > 0)
                //{
                //    interfaceName = "TRP0020E";
                //    AR_PartialPay data = exportList5[exportList5.Count - 1];
                //    _sapPort.OutFileName = string.Format("{0}\\{1}", _filePool, _bpmSAPService.GetExportFileName("TRP0020E"));
                //    _sapPort.SendList<AR_PartialPay>(exportList5, data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                //}


                //List<ARn_Normal> exportList6 = _bpmSAPService.Export_ARNonInvNormal(null);
                //if (exportList6.Count > 0)
                //{
                //    interfaceName = "TRP0030A";
                //    ARn_Normal data = exportList6[exportList6.Count - 1];
                //    _sapPort.OutFileName = string.Format("{0}\\{1}", _filePool, _bpmSAPService.GetExportFileName("TRP0030A"));
                //    _sapPort.SendList<ARn_Normal>(exportList6, data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                //}


                //List<ARn_CashSale> exportList7 = _bpmSAPService.Export_ARNonInvCashSale(null);
                //if (exportList7.Count > 0)
                //{
                //    interfaceName = "TRP0030B";
                //    ARn_CashSale data = exportList7[exportList7.Count - 1];
                //    _sapPort.OutFileName = string.Format("{0}\\{1}", _filePool, _bpmSAPService.GetExportFileName("TRP0030B"));
                //    _sapPort.SendList<ARn_CashSale>(exportList7, data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                //}


                //List<ARn_AdvPayment> exportList8 = _bpmSAPService.Export_ARNonInvAdvancePayment(null);
                //if (exportList8.Count > 0)
                //{
                //    interfaceName = "TRP0030C";
                //    ARn_AdvPayment data = exportList8[exportList8.Count - 1];
                //    _sapPort.OutFileName = string.Format("{0}\\{1}", _filePool, _bpmSAPService.GetExportFileName("TRP0030C"));
                //    _sapPort.SendList<ARn_AdvPayment>(exportList8, data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                //}


                //List<ARn_APPayment> exportList9 = _bpmSAPService.Export_APPayment(null);
                //if (exportList9.Count > 0)
                //{
                //    interfaceName = "TRP0030E";
                //    ARn_APPayment data = exportList9[exportList9.Count - 1];
                //    _sapPort.OutFileName = string.Format("{0}\\{1}", _filePool, _bpmSAPService.GetExportFileName("TRP0030E"));
                //    _sapPort.SendList<ARn_APPayment>(exportList9, data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                //}


                //List<AR_DepositReceipt> exportList10 = _bpmSAPService.Export_ARDepositReceipt(null);
                //if (exportList10.Count > 0)
                //{
                //    interfaceName = "TRP0040A";
                //    AR_DepositReceipt data = exportList10[exportList10.Count - 1];
                //    _sapPort.OutFileName = string.Format("{0}\\{1}", _filePool, _bpmSAPService.GetExportFileName("TRP0040A"));
                //    _sapPort.SendList<AR_DepositReceipt>(exportList10, data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                //}


                //List<AP_Payment> exportList11 = _bpmSAPService.Export_APVoucher(null);
                //if (exportList11.Count > 0)
                //{
                //    interfaceName = "TRP0050A";
                //    AP_Payment data = exportList11[exportList11.Count - 1];
                //    _sapPort.OutFileName = string.Format("{0}\\{1}", _filePool, _bpmSAPService.GetExportFileName("TRP0050A"));
                //    _sapPort.SendList<AP_Payment>(exportList11, data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                //}


                //List<AP_BankPayIn> exportList12 = _bpmSAPService.Export_APBankPayIn(null);
                //if (exportList12.Count > 0)
                //{
                //    interfaceName = "TRP0050B";
                //    AP_BankPayIn data = exportList12[exportList12.Count - 1];
                //    _sapPort.OutFileName = string.Format("{0}\\{1}", _filePool, _bpmSAPService.GetExportFileName("TRP0050B"));
                //    _sapPort.SendList<AP_BankPayIn>(exportList12, data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                //}


                //List<AR_Normal> exportList13 = _bpmSAPService.Export_MPayARNormal(null);
                //if (exportList13.Count > 0)
                //{
                //    interfaceName = "TRP0020A";
                //    AR_Normal data = exportList13[exportList13.Count - 1];
                //    _sapPort.OutFileName = string.Format("{0}\\{1}", _filePool, _bpmSAPService.GetExportFileName("TRP0020A"));
                //    _sapPort.SendList<AR_Normal>(exportList13, data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                //}


                //List<ARn_AdvPayment> exportList14 = _bpmSAPService.Export_MPayARNonInvAdvancePayment(null);
                //if (exportList14.Count > 0)
                //{
                //    interfaceName = "TRP0030C";
                //    ARn_AdvPayment data = exportList14[exportList14.Count - 1];
                //    _sapPort.OutFileName = string.Format("{0}\\{1}", _filePool, _bpmSAPService.GetExportFileName("TRP0030C"));
                //    _sapPort.SendList<ARn_AdvPayment>(exportList14, data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                //}


                //List<AR_DepositReceipt> exportList15 = _bpmSAPService.Export_MPayARDepositReceipt(null);
                //if (exportList15.Count > 0)
                //{
                //    interfaceName = "TRP0040A";
                //    AR_DepositReceipt data = exportList15[exportList15.Count - 1];
                //    _sapPort.OutFileName = string.Format("{0}\\{1}", _filePool, _bpmSAPService.GetExportFileName("TRP0040A"));
                //    _sapPort.SendList<AR_DepositReceipt>(exportList15, data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                //}


                //List<ARn_APPayment> exportList16 = _bpmSAPService.Export_APChequePayment(null);
                //if (exportList16.Count > 0)
                //{
                //    interfaceName = "TRP0030E";
                //    ARn_APPayment data = exportList16[exportList16.Count - 1];
                //    _sapPort.OutFileName = string.Format("{0}\\{1}", _filePool, _bpmSAPService.GetExportFileName("TRP0030E"));
                //    _sapPort.SendList<ARn_APPayment>(exportList16, data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                //}


                //List<AR_Reconnect> exportList17 = _bpmSAPService.Export_ARReconnect(null);
                //if (exportList17.Count > 0)
                //{
                //    interfaceName = "TRP0020F";
                //    AR_Reconnect data = exportList17[exportList17.Count - 1];
                //    _sapPort.OutFileName = string.Format("{0}\\{1}", _filePool, _bpmSAPService.GetExportFileName("TRP0020F"));
                //    _sapPort.SendList<AR_Reconnect>(exportList17, data.TotalActualAmount, data.TotalAmount, data.TotalOverUnder);
                //}

            }
            catch (Exception ex)
            {
                if (File.Exists(_sapPort.OutFileName))
                {
                    _bpmSAPService.DecreaseInterfaceRunningNumber(interfaceName);
                    File.Delete(_sapPort.OutFileName);
                }
                throw ex;
            }





            ////**********************************************************
            ////*******************Export Reconnect***********************
            ////**********************************************************
            //SAPPort _sapPort = new SAPPort();
            //IBpmSAPService _bpmSAPService = new BPMSAPService();
            //ACADepLineMgr _depMgr = new ACADepLineMgr();
            //string _filePool = ConfigurationManager.AppSettings["EXPORT_RECONNECT_PATH"];

            //string branchId = ConfigurationManager.AppSettings["EXPORT_RECONNECT_BRANCHID"];

            //IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("en-US");
            //string fileName = string.Format("PAID_{0}_{1}.txt", branchId, DateTime.Now.ToString("yyyyMMdd_HHmm", formatDate));


            //ACABatchParam param = new ACABatchParam();
            //param.BatchKey = Guid.Empty;
            //param.BranchId = null;


            //try
            //{
            //    if (Directory.Exists(_filePool))
            //    {
            //        //start early the day
            //        DateTime now = DateTime.Now;
            //        //DateTime startDt = new DateTime(now.Year, now.Month, now.Year, 0, 0, 0);
            //        DateTime startDt = DateTime.Parse(now.Month + "/" + now.Day + "/" + now.Year);
            //        DateTime endDt = DateTime.Parse(now.Month + "/" + now.AddDays(1).Day + "/" + now.Year);
            //        List<BillInfo> reconList = _bpmSAPService.GetReconnection(branchId, startDt, endDt);

            //        if (reconList.Count > 0)
            //        {
            //            _sapPort.OutFileName = _filePool;
            //            _sapPort.SendList<BillInfo>(reconList, branchId, fileName);
            //        }
            //    }
            //    else
            //    {
            //        throw new Exception("Couldn't connect mapped drive. Please check your connection.");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

        }


        static void SendFileToOutBound(string interfaceName)
        {
            DateTime now = DateTime.Now;            
            string year = now.ToString("yyyy", new System.Globalization.CultureInfo("en-US"));
            string month = now.ToString("yyyyMM", new System.Globalization.CultureInfo("en-US"));
            string day = now.ToString("yyyyMMdd", new System.Globalization.CultureInfo("en-US"));
            string preOutboundPath = string.Format(@"{0}", ConfigurationManager.AppSettings["PREOUTBOUND_PATH"]);
            string backupPath = string.Format(@"{0}{1}\{2}\{3}\", preOutboundPath, year, month, day);


            if (!Directory.Exists(string.Format(@"{0}{1}", preOutboundPath, year)))
            {
                Directory.CreateDirectory(string.Format(@"{0}{1}", preOutboundPath, year));
            }

            if (!Directory.Exists(string.Format(@"{0}{1}\{2}\", preOutboundPath, year, month)))
            {
                Directory.CreateDirectory(string.Format(@"{0}{1}\{2}\", preOutboundPath, year, month));
            }

            if (!Directory.Exists(string.Format(@"{0}{1}\{2}\{3}\", preOutboundPath, year, month, day)))
            {
                Directory.CreateDirectory(string.Format(@"{0}{1}\{2}\{3}\", preOutboundPath, year, month, day));
            }


            MoveFileToOutbound(interfaceName, ConfigurationManager.AppSettings["EXPORT_PATH"], backupPath);
            MoveFileOKToOutbound(interfaceName, ConfigurationManager.AppSettings["EXPORT_PATH"], backupPath);
        }


        static void MoveFileToOutbound(string interfaceName, string preOutboundPath, string backupPath)
        {
            string processPath = string.Format(@"{0}", preOutboundPath);

            string[] fileList = Directory.GetFiles(processPath,
                string.Format("{0}*.txt", interfaceName));

            foreach (string ifile in fileList)
            {
                FileInfo finfo = new FileInfo(ifile);

                File.Copy(finfo.FullName, string.Format(@"{0}{1}", backupPath, finfo.Name), true);

                File.Move(finfo.FullName, string.Format(@"{0}{1}", ConfigurationManager.AppSettings["OUTBOUND_PATH"], finfo.Name));
            }
        }

        static void MoveFileOKToOutbound(string interfaceName, string preOutboundPath, string backupPath)
        {
            string processPath = string.Format(@"{0}", preOutboundPath);

            string[] fileList = Directory.GetFiles(processPath,
                string.Format("{0}*.OK", interfaceName));

            foreach (string ifile in fileList)
            {
                FileInfo finfo = new FileInfo(ifile);

                File.Copy(finfo.FullName, string.Format(@"{0}{1}", backupPath, finfo.Name), true);

                File.Move(finfo.FullName, string.Format(@"{0}{1}", ConfigurationManager.AppSettings["OUTBOUND_PATH"], finfo.Name));
            }
        }

        static string GetARFileType(string fileName)
        {
            string f = fileName.Substring(7, 1);
            return f;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using Avanade.ACA.Batch;

namespace PEA.BPM.Integration.BPMIntegration.BPMServerBatch
{
    internal class BPMServerBatchHelper
    {
        // C:\BPM\BPMCenterBatchData\InBound\
        private static string _inboundPath = ConfigurationManager.AppSettings["INBOUND_PATH"];
        // C:\BPM\BPMCenterBatchData\OutBound\
        private static string _outboundPath = ConfigurationManager.AppSettings["OUTBOUND_PATH"];
        //TODO: Uncomment for OK File  ***** BEGIN
        //// C:\BPM\BPMCenterBatchData\PreOutBound\
        //private static string _preOutboundPath = ConfigurationManager.AppSettings["PREOUTBOUND_PATH"];
        //TODO: Uncomment for OK File  ***** END
        // C:\BPM\BPMCenterBatchData\Process\
        private static string _processPath = ConfigurationManager.AppSettings["PROCESS_PATH"];
        // C:\BPM\BPMCenterBatchData\Process\
        private static string _bulkFormatPath = ConfigurationManager.AppSettings["BULK_FORMAT_PATH"];
        // C:\BPM\BPMCenterBatchData\AGOutBound\
        //OUTBOUND_AG_PATH no any code path using this parameter. It should be obsolete soon.
        private static string _agOutboundPath = ConfigurationManager.AppSettings["OUTBOUND_AG_PATH"];
        private static string _reconnectionPath = ConfigurationManager.AppSettings["RECONNECT_PATH"];
        private static string _billBookInvoiceOutboundPath = ConfigurationManager.AppSettings["OUTBOUND_BILLBOOK_INVOICE_PATH"];

        internal static string GetDataProcessingPath(string batchName)
        {
            return string.Format("{0}{1}", _processPath, batchName);
        }

        internal static string GetReconnectionPath()
        {
            return _reconnectionPath;
        }

        internal static string BulkFormatPath
        {
            get { return _bulkFormatPath; }
        }

        internal static void MoveFileToProcessWithRemoveHeader(string batchName, string interfaceName)
        {
            MoveFileToProcess(batchName, interfaceName, interfaceName);
        }

        internal static void MoveFileToProcess(string batchName, string interfaceName)
        {
            MoveFileToProcess(batchName, interfaceName, interfaceName);
        }

        internal static void RemoveHeaderFooter(string fullPathFileName)
        {
            if (File.Exists(fullPathFileName))
            {
                StringBuilder sb = new StringBuilder();
                foreach (string s in File.ReadAllLines(fullPathFileName, Encoding.Default))
                {
                    if (s.TrimStart(' ').StartsWith("HH", StringComparison.CurrentCultureIgnoreCase) || s.TrimStart(' ').StartsWith("FF", StringComparison.CurrentCultureIgnoreCase))
                        continue;

                    sb.AppendLine(s);
                }

                File.WriteAllText(fullPathFileName, sb.ToString(), Encoding.Default);
            }
        }

        internal static void RemoveHeaderFooterOpenItem(string fullPathFileName)
        {
            if (File.Exists(fullPathFileName))
            {
                StringBuilder sb = new StringBuilder();
                foreach (string s in File.ReadAllLines(fullPathFileName, Encoding.Default))
                {
                    if (s.TrimStart(' ').StartsWith("HH", StringComparison.CurrentCultureIgnoreCase) || s.TrimStart(' ').StartsWith("FF", StringComparison.CurrentCultureIgnoreCase))
                        continue;

                    AR ar = new AR();
                    ar = ar.ParseExtract(s);
                    sb.AppendLine(ar.ToBulkLoadString());
                }

                File.WriteAllText(fullPathFileName, sb.ToString(), Encoding.Default);
            }
        }

        /// <summary>
        /// Move data files from inbound path to processing path
        /// </summary>
        /// <param name="batchName">ex. DL001_ISOLATE</param>
        /// <param name="interfaceName">ex. MTR0080</param>
        internal static void MoveFileToProcess(string batchName, string interfaceName, string fileprefix)
        {
            string[] fileList = Directory.GetFiles(_inboundPath,
                string.Format("{0}*.txt", fileprefix));

            // {C:\BPM\BPMCenterBatchData\Process\}{DL001_ISOLATE}\{CFR0010}\
            string tarGetPath = string.Format(@"{0}{1}\{2}\", _processPath, batchName, interfaceName);

            foreach (string ifile in fileList)
            {
                FileInfo finfo = new FileInfo(ifile);
                // C:\BPM\BPMCenterBatchData\Inbound\xxx.txt
                // {C:\BPM\BPMCenterBatchData\Process\DL001_ISOLATE\CFR0010\}{xxxx.txt}
                File.Move(finfo.FullName, string.Format(@"{0}{1}", tarGetPath, finfo.Name));
            }            
        }

        internal static void MoveFileToOutbound(string batchName, string interfaceName)
        {
            // {C:\BPM\BPMCenterBatchData\Process\}{DL007_EXPORT_TO_SAP}\{TRP0020E}
            string processPath = string.Format(@"{0}{1}\{2}", _processPath, batchName, interfaceName);

            string[] fileList = Directory.GetFiles(processPath,
                string.Format("{0}*.txt", interfaceName));

            foreach (string ifile in fileList)
            {
                FileInfo finfo = new FileInfo(ifile);

                File.Copy(finfo.FullName, string.Format(@"{0}.Succeeded\{1}", processPath, finfo.Name), true);
                // {C:\BPM\BPMCenterBatchData\Process\DL007_EXPORT_TO_SAP\TRP0020E\xxxx.txt}
                // {C:\BPM\BPMCenterBatchData\Outbound\}{xxx.txt}

                //backup file in U:\
                string backupPath = string.Format(@"{0}Backup\{1}", _outboundPath , DateTime.Now.ToString("yyyyMMdd", new System.Globalization.CultureInfo("en-US")));
                if(!Directory.Exists(backupPath))
                    Directory.CreateDirectory(backupPath);
                
                File.Copy(finfo.FullName, string.Format(@"{0}\{1}", backupPath, finfo.Name));

                File.Move(finfo.FullName, string.Format(@"{0}{1}", _outboundPath, finfo.Name));

                //create
                using (System.IO.File.Create(string.Format(@"{0}{1}", _outboundPath, finfo.Name.Replace(".txt", ".OK")))) { };
            }
        }

        //TODO: Uncomment for OK File  ***** BEGIN
        //internal static void MoveFileToOutbound(string batchName, string interfaceName, string backupPath)
        //{
        //    // {C:\BPM\BPMCenterBatchData\Process\}{DL007_EXPORT_TO_SAP}\{TRP0020E}
        //    string processPath = string.Format(@"{0}{1}\{2}", _processPath, batchName, interfaceName);

        //    string[] fileList = Directory.GetFiles(processPath,
        //        string.Format("{0}*.txt", interfaceName));

        //    foreach (string ifile in fileList)
        //    {
        //        FileInfo finfo = new FileInfo(ifile);

        //        File.Copy(finfo.FullName, string.Format(@"{0}.Succeeded\{1}", processPath, finfo.Name), true);

        //        File.Copy(finfo.FullName, string.Format(@"{0}{1}", backupPath, finfo.Name), true);

        //        // {C:\BPM\BPMCenterBatchData\Process\DL007_EXPORT_TO_SAP\TRP0020E\xxxx.txt}
        //        // {C:\BPM\BPMCenterBatchData\Outbound\}{xxx.txt}
        //        File.Move(finfo.FullName, string.Format(@"{0}{1}", _outboundPath, finfo.Name));
        //    }
        //}

        //internal static void MoveFileOKToOutbound(string batchName, string interfaceName, string backupPath)
        //{
        //    // {C:\BPM\BPMCenterBatchData\Process\}{DL007_EXPORT_TO_SAP}\{TRP0020E}
        //    string processPath = string.Format(@"{0}{1}\{2}", _processPath, batchName, interfaceName);

        //    string[] fileList = Directory.GetFiles(processPath,
        //        string.Format("{0}*.OK", interfaceName));

        //    foreach (string ifile in fileList)
        //    {
        //        FileInfo finfo = new FileInfo(ifile);

        //        File.Copy(finfo.FullName, string.Format(@"{0}.Succeeded\{1}", processPath, finfo.Name), true);

        //        File.Copy(finfo.FullName, string.Format(@"{0}{1}", backupPath, finfo.Name), true);

        //        // {C:\BPM\BPMCenterBatchData\Process\DL007_EXPORT_TO_SAP\TRP0020E\xxxx.txt}
        //        // {C:\BPM\BPMCenterBatchData\Outbound\}{xxx.txt}
        //        File.Move(finfo.FullName, string.Format(@"{0}{1}", _outboundPath, finfo.Name));
        //    }
        //}
        //TODO: Uncomment for OK File  ***** END

        internal static void MoveFileToAGOutbound(string batchName, string interfaceName)
        {
            // {C:\BPM\BPMCenterBatchData\Process\}{DL007_EXPORT_TO_SAP}\{TRP0020E}
            string processPath = string.Format(@"{0}{1}\{2}", _processPath, batchName, interfaceName);

            string[] fileList = Directory.GetFiles(processPath,
                string.Format("{0}*.txt", interfaceName));

            foreach (string ifile in fileList)
            {
                FileInfo finfo = new FileInfo(ifile);

                File.Move(finfo.FullName, string.Format(@"{0}.Succeeded\{1}", processPath, finfo.Name));

                // {C:\BPM\BPMCenterBatchData\Process\DL007_EXPORT_TO_SAP\TRP0020E\xxxx.txt}
                // {C:\BPM\BPMCenterBatchData\Outbound\}{xxx.txt}
                //File.Move(finfo.FullName, string.Format(@"{0}{1}", _agOutboundPath, finfo.Name));
                
            }
        }

        //internal static void MoveFileToAGOutbound(string batchName, string interfaceName, string fileName)
        //{
        //    // {C:\BPM\BPMCenterBatchData\Process\}{DL008_EXPORT_AG_TO_SAP}\{TRP0010}
        //    string processPath = string.Format(@"{0}{1}\{2}", _processPath, batchName, interfaceName);

        //    string[] fileList = Directory.GetFiles(processPath, fileName);
        //    foreach (string ifile in fileList)
        //    {
        //        FileInfo finfo = new FileInfo(ifile);
        //        File.Move(finfo.FullName, string.Format(@"{0}.Succeeded\{1}", processPath, finfo.Name));
        //    }
        //}

        internal static void MoveFileToAGOutbound(string batchName, string interfaceName, string fileName)
        {
            // {C:\BPM\BPMCenterBatchData\Process\}{DL008_EXPORT_AG_TO_SAP}\{TRP0010}
            string processPath = string.Format(@"{0}{1}\{2}", _processPath, batchName, interfaceName);

            string[] fileList = Directory.GetFiles(processPath, fileName);
            foreach (string ifile in fileList)
            {
                FileInfo finfo = new FileInfo(ifile);

                File.Copy(finfo.FullName, string.Format(@"{0}.Succeeded\{1}", processPath, finfo.Name), true);

                // {C:\BPM\BPMCenterBatchData\Process\}{DL010_EXPORT_BILLBOOK_INVOICE}\{TRP0060}\{xxx.txt}
                // {C:\BPM\BPMCenterBatchData\BillBookOutbound\}{xxx.txt}
                File.Move(finfo.FullName, string.Format(@"{0}{1}", _agOutboundPath, finfo.Name));
                File.Create(string.Format(@"{0}{1}.{2}", _agOutboundPath, Path.GetFileNameWithoutExtension(finfo.Name), "OK"));
            }
        }

        internal static void MoveFileToFailedOutbound(string batchName, string interfaceName, string fileName)
        {
            // {C:\BPM\BPMCenterBatchData\Process\}{DL008_EXPORT_AG_TO_SAP}\{TRP0010}
            string processPath = string.Format(@"{0}{1}\{2}", _processPath, batchName, interfaceName);
            string SucceededPath = string.Format(@"{0}{1}\{2}.Succeeded", _processPath, batchName, interfaceName);

            string[] fileList = Directory.GetFiles(processPath, fileName);
            foreach (string ifile in fileList)
            {
                FileInfo finfo = new FileInfo(ifile);
                File.Move(finfo.FullName, string.Format(@"{0}.Failed\{1}", processPath, finfo.Name));
            }

            fileList = Directory.GetFiles(SucceededPath, fileName);
            foreach (string ifile in fileList)
            {
                FileInfo finfo = new FileInfo(ifile);
                File.Move(finfo.FullName, string.Format(@"{0}.Failed\{1}", processPath, finfo.Name));
            }
        }

        internal static void MoveFileToBillBookOutbound(string batchName, string interfaceName)
        {
            // {C:\BPM\BPMCenterBatchData\Process\}{DL010_EXPORT_BILLBOOK_INVOICE}\{TRP0060}
            string processPath = string.Format(@"{0}{1}\{2}", _processPath, batchName, interfaceName);

            string[] fileList = Directory.GetFiles(processPath,
                string.Format("{0}*.txt", interfaceName));

            foreach (string ifile in fileList)
            {
                FileInfo finfo = new FileInfo(ifile);

                File.Copy(finfo.FullName, string.Format(@"{0}.Succeeded\{1}", processPath, finfo.Name), true);

                // {C:\BPM\BPMCenterBatchData\Process\}{DL010_EXPORT_BILLBOOK_INVOICE}\{TRP0060}\{xxx.txt}
                // {C:\BPM\BPMCenterBatchData\BillBookOutbound\}{xxx.txt}
                File.Move(finfo.FullName, string.Format(@"{0}{1}", _billBookInvoiceOutboundPath, finfo.Name));
                File.Create(string.Format(@"{0}{1}.{2}", _billBookInvoiceOutboundPath, Path.GetFileNameWithoutExtension(finfo.Name),"OK"));
            }
        }

        internal static bool IsMappedDriveExist(string batchName, string interfaceName)
        {
            string processPath = string.Format(@"{0}{1}\{2}", _processPath, batchName, interfaceName);
            if (Directory.Exists(processPath))
                return true;
            else
                return false;
        }

        internal static bool IsReconnectPathExist()
        {
            return Directory.Exists(_reconnectionPath);
        }
    }
}

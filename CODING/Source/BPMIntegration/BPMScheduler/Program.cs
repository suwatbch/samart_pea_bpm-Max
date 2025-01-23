// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================

using System;
using System.IO;
using Avanade.ACA.Batch.Configuration;
using System.Configuration;
using System.Collections;
using Avanade.ACA.Batch;
using System.Collections.Generic;

namespace BPMScheduler
{
    /// <summary>
    /// The command line client application class
    /// </summary>
    class Program
    {
        #region constants
        private const string INVALID_ARGUMENTS = "Usage: BPMScheduler <Batch Name> <Destination> <BranchId> \ne.g. BPMScheduler MyBatch01 MyDestination B00001\n";
        private const string PROGRAM_CANNOT_CONTINUE = "Program cannot continue.";
        private const int EXIT_CODE_IN_ABSENCE_OF_EXITCODESTRATEGY = -5555;
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">string[]</param>
        /// <returns>int</returns>
        [STAThread]
        static int Main(string[] args)
        {
            BatchExecutionRequest request = null;

            try
            {
                if (args.Length == 0 || args.Length > 3)
                {
                    Console.WriteLine(INVALID_ARGUMENTS);
                    return (int)1;
                }

                string batchName = args[0];

                //Of this dependency line, find if there is any file to process
                if (!AnyFileToProcess(batchName)) return 0;

                request = new BatchExecutionRequest(batchName);
                request.BatchClientName = "";
                request.Destination = args[1]; //destination
                request.StartPollingForResultAfter = TimeSpan.FromMinutes(5);
                BatchExecutionRequestQueue queue = new BatchExecutionRequestQueue("ACABatch_SQL");

                queue.Enqueue(request);
            }
            catch (Exception e)
            {
                throw e;
            }

            return (int)1;

        }

        private static bool AnyFileToProcess(string batchName)
        {
            bool ret = false;
            string inboundPath = ConfigurationManager.AppSettings["InBound"].ToString();

            switch (batchName)
            {
                case "DL001_ISOLATE_BATCH":
                    return Exist(inboundPath, "CFR*.txt|MTR0080*.txt");
                case "DL002_MASTER_BATCH":
                    return Exist(inboundPath, "MTR*.txt");
                case "DL003_BILLING_BATCH":
                    return Exist(inboundPath, "INVOICE*.txt");
                case "DL004_TRANSACTION_BATCH":
                    return Exist(inboundPath, "TRR*.txt");
                case "DL005_PAYFROMSAP_BATCH": //obsolete
                    return Exist(inboundPath, "TRR0050*.txt");
                case "DL006_DIRECTDEBIT_BATCH":
                    return Exist(inboundPath, "RECEIPTX*.txt|billReverse*.txt|INVREPORT*.txt|INVSTATUS*.txt|billupdatx*.txt");
                default:
                    break;
            }

            return ret;
        }

        public static bool Exist(string path, string searchPattern)
        {
            string[] searchPatterns = searchPattern.Split('|');
            foreach (string sp in searchPatterns)
            {
                string[] files = System.IO.Directory.GetFiles(path, sp, SearchOption.TopDirectoryOnly);
                if (files.Length > 0) return true;
            }

            return false;
        }

        class CompareFileInfo : IComparer
        {
            public int Compare(object x, object y)
            {
                FileInfo file = new FileInfo((string)x);
                FileInfo file2 = new FileInfo((string)y);
                return DateTime.Compare(file.LastWriteTime, file2.LastWriteTime);
            }
        }
    }
}

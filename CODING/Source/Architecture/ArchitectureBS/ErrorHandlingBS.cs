using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureDA;

namespace PEA.BPM.Architecture.ArchitectureBS
{
    public class ErrorHandlingBS : IErrorHandlingService
    {
        public ExceptionDebugInfo ReportAndSaveException(BPMApplicationExceptionInfo exceptioninfo, bool clientack)
        {
            try
            {
                exceptioninfo.Message = exceptioninfo.Message.Replace("\r", "").Replace("\n", Environment.NewLine);
                exceptioninfo.StackTrace = exceptioninfo.StackTrace.Replace("\r", "").Replace("\n", Environment.NewLine);

                ErrorHandlingDA da = new ErrorHandlingDA();
                return da.ReportAndSaveException(exceptioninfo, clientack);
            }
            catch (Exception ee)
            {
               // TODO: save data ลง database, log error เก็บไว้
                ExceptionDebugInfo dummyedinfo = new ExceptionDebugInfo();
                return dummyedinfo;
            }
        }

        public void AcknowledgeException(string debuggingid, string updatestacktrace)
        {
            try
            {
                updatestacktrace = updatestacktrace.Replace("\r", "").Replace("\n", Environment.NewLine);

                ErrorHandlingDA da = new ErrorHandlingDA();
                da.AcknowledgeException(debuggingid, updatestacktrace);
            }
            catch (Exception)
            {
               // TODO: save data ลง database, log error เก็บไว้
            }
        }

    }
}

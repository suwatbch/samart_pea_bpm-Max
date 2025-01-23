using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Architecture.CommonUtilities
{
    public class Logger
    {
        public class Module
        {
            public const string ARCH = "ARCH";
            public const string POS = "POS";
            public const string BLAN = "BLAN";
            public const string AGENCY = "AGENCY";
            public const string TOOL = "TOOL";
            public const string EPAYMENT = "EPAYMENT";
        }

        private Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry _errorEntry;
        private Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry _warningEntry;
        private Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry _infoEntry;

        private static Logger _instant = new Logger();

        private Logger()
        {
            _errorEntry = new Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry();
            _errorEntry.Severity = System.Diagnostics.TraceEventType.Error;

            _warningEntry = new Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry();
            _warningEntry.Severity = System.Diagnostics.TraceEventType.Warning;

            _infoEntry = new Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry();
            _infoEntry.Severity = System.Diagnostics.TraceEventType.Information;
        }

        public static void WriteError(string module, string title, string msg)
        {
            _instant._errorEntry.AppDomainName = module;
            _instant._errorEntry.Title = title;
            _instant._errorEntry.Message = msg;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(_instant._errorEntry);
        }

        public static void WriteWarning(string module, string title, string msg)
        {
            _instant._warningEntry.AppDomainName = module;
            _instant._warningEntry.Title = title;
            _instant._warningEntry.Message = msg;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(_instant._warningEntry);
        }

        public static void WriteInfo(string module, string title, string msg)
        {
            _instant._infoEntry.AppDomainName = module;
            _instant._infoEntry.Title = title;
            _instant._infoEntry.Message = msg;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(_instant._infoEntry);
        }
    }
}

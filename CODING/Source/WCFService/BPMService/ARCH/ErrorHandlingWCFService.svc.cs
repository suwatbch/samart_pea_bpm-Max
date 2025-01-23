using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using System.ComponentModel;
using WCFExtras.Soap;
using System.Data;


namespace BPMService.ARCH
{

    public class ErrorHandlingWCFService : IErrorHandlingWCFService
    {
        private ErrorHandlingBS _bs;
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public ErrorHandlingWCFService()
        {
            _bs = new ErrorHandlingBS();
        }

        #region การจัดการ Exception

        public ExceptionDebugInfo ReportAndSaveException(BPMApplicationExceptionInfo excpetioninfo)
        {
            if (this.authInfo != null)
            {
                if (!string.IsNullOrEmpty(this.authInfo.UserId)) excpetioninfo.UserId = this.authInfo.UserId;
                if (!string.IsNullOrEmpty(this.authInfo.AuthToken)) excpetioninfo.AuthToken = this.authInfo.AuthToken;
            }
            return _bs.ReportAndSaveException(excpetioninfo, true);
        }

        public void AcknowledgeException(string debuggingid, string updatestacktrace)
        {
            _bs.AcknowledgeException(debuggingid, updatestacktrace);
        }

        #endregion
    }
}

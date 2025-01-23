using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;

using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using PEA.BPM.Integration.BPMIntegration.BS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.WebService.Integration;

namespace BPMIntegrationService
{
    /// <summary>
    /// Summary description for ExportTableIntegrationWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ExportTableIntegrationWebService : BaseWebService
    {
        private BPMServerService _bs;

        public ExportTableIntegrationWebService()
        {
            _bs = new BPMServerService();
        }

        #region Upload DL200 to server

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadExportTransactionLog(CompressData cds, string branchId)
        {          
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return new BPMServerService().UploadExportTransactionLog(ServiceHelper.DecompressData<List<ExportTransactionLogInfo>>(cds), branchId);
        }

        #endregion
    }
}

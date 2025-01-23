using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;

namespace PEA.BPM.ePayService
{

    /// <summary>
    /// Summary description for CodeTableWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class CommonWebService : BaseWebService
    {

        private CommonBS _bs;
        public CommonWebService()
        {
            _bs = new CommonBS();
        }

        [WebMethod]
        public string IsAuthenticated(string userId, string hashPwd)
        {
            return _bs.IsAuthenticated(userId, hashPwd, "EPayกสส");
        }

        [WebMethod]
        public string IsAuthenticatedInBranch(string userId, string hashPwd, string regBranchId)
        {
            return _bs.IsAuthenticated(userId, hashPwd, "EPayกสส", regBranchId);
        }

        [WebMethod]
        public string GetToken(string userId, string hashPwd)
        {
            return _bs.GetToken(userId, hashPwd);
        }

        [WebMethod]
        public DateTime GetServerTime()
        {
            return _bs.GetServerTime();
        }

        [WebMethod]
        public BPMVersion GetVersion()
        {
            return _bs.GetVersion();
        }

        [WebMethod]
        public void SignalExport(string batchName, string branchId, string modifiedBy)
        {
            _bs.SignalExport(batchName, branchId, modifiedBy);
        }

        [WebMethod]
        public void SignalSyncup(string batchName)
        {
            _bs.SignalSyncup(batchName);
        }

        [WebMethod]
        public WorkStatus IsForcedToCloseWork(string workId)
        {
            return _bs.IsForcedToCloseWork(workId);
        }

    }
}


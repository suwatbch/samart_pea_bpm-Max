using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.WebService.Security.Cashier;
using System.Collections.Generic;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;

namespace PEA.BPM.WebService.Security
{
    /// <summary>
    /// Summary description for CashierCachingWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class CashierCachingWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string CacheOpenWork(string workid, string cashierid)
        {
            CashierCachingController.Instance.CacheOpenWork(workid, cashierid);
            return "";
        }
        
        [WebMethod]
        public string CacheCloseWork(string workid, string cashierid)
        {
            CashierCachingController.Instance.CacheCloseWork(workid, cashierid);
            return "";
        }

        [WebMethod]
        public List<CashierWorkStatusInfo> IsOpenedWork(OpenWorkParam param)        
        {
            List<CashierWorkStatusInfo> res = CashierCachingController.Instance.IsOpenedWork(param.BranchId, param.CashierId, param.PosId);
            return res;
        }

        [WebMethod]
        public CashierWorkStatusInfo TestIsOpenedWork(string BranchId, string CashierId, string PosId)
        {
            List<CashierWorkStatusInfo> res = CashierCachingController.Instance.IsOpenedWork(BranchId, CashierId, PosId);
            if (res.Count != 0) return res[0];
            else return null;
        }
    }
}

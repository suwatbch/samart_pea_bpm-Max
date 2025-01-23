using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Collections.Generic;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.WebService.Security.Cashier;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;


namespace PEA.BPM.WebService.Security
{
    public class CashierCachingService : ICashierCachingService
    {

        public string CacheOpenWork(string workid, string cashierid)
        {
            try
            {
                CashierCachingController.Instance.CacheOpenWork(workid, cashierid);
                return "";
            }
            catch
            {
                throw;
            }
        }

        public string CacheCloseWork(string workid, string cashierid)
        {
            try
            {
                CashierCachingController.Instance.CacheCloseWork(workid, cashierid);
                return "";
            }
            catch
            {
                throw;
            }
        }

        public System.Collections.Generic.List<CashierWorkStatusInfo> IsOpenedWork(OpenWorkParam param)
        {
            try
            {
                List<CashierWorkStatusInfo> res = CashierCachingController.Instance.IsOpenedWork(param.BranchId, param.CashierId, param.PosId);
                return res;
            }
            catch
            {
                throw;
            }
        }

        public CashierWorkStatusInfo TestIsOpenedWork(string BranchId, string CashierId, string PosId)
        {
            try
            {
                List<CashierWorkStatusInfo> res = CashierCachingController.Instance.IsOpenedWork(BranchId, CashierId, PosId);
                if (res.Count != 0) return res[0];
                else return null;
            }
            catch
            {
                throw;
            }
        }

    }
}

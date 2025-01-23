using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.WebService.Security.Cashier;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;

namespace PEA.BPM.WebService.Security
{
    [ServiceContract(Namespace = "http://tempuri.org")]
    public interface ICashierCachingService
    {
        [OperationContract]
        string CacheOpenWork(string workid, string cashierid);

        [OperationContract]
        string CacheCloseWork(string workid, string cashierid);

        [OperationContract]
        List<CashierWorkStatusInfo> IsOpenedWork(OpenWorkParam param);

        [OperationContract]
        CashierWorkStatusInfo TestIsOpenedWork(string BranchId, string CashierId, string PosId);
    }
}

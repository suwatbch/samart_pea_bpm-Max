using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPMInternalServicePools.Model;
using BPMInternalServicePools.DA;
                                      
namespace BPMInternalServicePools.BS
{
    public class WebBillingBS
    {
        public List<WebBillingModel> GetElectricBillingByCaid(string caid)
        {
            var result = new List<WebBillingModel>();
            if (String.IsNullOrEmpty(caid) || String.IsNullOrWhiteSpace(caid))
            {
                return result;
            }
            else
            {
                var service = new WebBillingDA();
                result = service.GetBillingByCaid(caid);
                return result;
            }
        }
    }
}

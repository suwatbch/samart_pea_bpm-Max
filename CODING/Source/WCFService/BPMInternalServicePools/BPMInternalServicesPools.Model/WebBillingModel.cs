using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMInternalServicePools.Model
{
    public class WebBillingModel
    {
        public string Caid { get; set; }
        public string InvoiceNo { get; set; }
        public string CaName { get; set; }
        public string CaAddress { get; set; }
        public string Period { get; set; }
        public float GAmount { get; set; }
        public string Active { get; set; }
    }
}

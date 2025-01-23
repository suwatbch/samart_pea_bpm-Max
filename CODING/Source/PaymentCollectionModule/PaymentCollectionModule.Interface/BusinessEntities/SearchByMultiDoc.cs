using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    public class SearchByMultiDoc
    {
        public string DocumentNo { get; set; }
        public string Status { get; set; }
        public int InvoiceCount { get; set; }
        public string Result { get; set; }
    }
}

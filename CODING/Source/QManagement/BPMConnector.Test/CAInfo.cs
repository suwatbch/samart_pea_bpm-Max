using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEA.BPM.BPMConnector.Test
{
    public class CAInfo
    {
        public string CaId { set; get; }
        public string CaName { set; get; }
        public string CaAddress { set; get; }
        public string DtName { set; get; }
        public string Period { set; get; }
        public string DueDate { set; get; }
        public decimal ToPayTotalAmount { set; get; }
    }
}

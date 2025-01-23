using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class TaxCalculationInfo
    {
        private decimal? _totalTax;


        [DataMember(Order=1)]
        public decimal? TotalTax
        {
            get { return _totalTax; }
            set { _totalTax = value; }
        }
    }
}

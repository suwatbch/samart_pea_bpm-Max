using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class GreenBillSummaryParam
    {
        string _approvedBy;


        [DataMember(Order=1)]
        public string ApprovedBy
        {
            get { return _approvedBy; }
            set { _approvedBy = value; }
        }

        List<GreenBillSummary> _greenBillSummary = new List<GreenBillSummary>();


        [DataMember(Order=2)]
        public List<GreenBillSummary> GreenBillSummary
        {
            get { return _greenBillSummary; }
            set { _greenBillSummary = value; }
        }
    }
}

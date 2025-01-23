using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CBillStatusBatchInfo
    {
        private string _invoiceNo;
        private string _pmId;


        [DataMember(Order=1)]
        public string InvoiceNo
        {
            set { _invoiceNo = value; }
            get { return _invoiceNo; }
        }


        [DataMember(Order=2)]
        public string PmId
        {
            set { _pmId = value; }
            get { return _pmId; }
        }

    }
}

using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities
{
    [DataContract]
    public class GreenReceiptParam
    {
        private string _caId;
        private string _branch;
        private string _period;
        private string _invoiceNo;


        [DataMember(Order=1)]
        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }


        [DataMember(Order=2)]
        public string Branch
        {
            get { return this._branch; }
            set { this._branch = value; }
        }


        [DataMember(Order=3)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }


        [DataMember(Order=4)]
        public string InvoiceNo
        {
            get { return this._invoiceNo; }
            set { this._invoiceNo = value; }
        }
    }
}

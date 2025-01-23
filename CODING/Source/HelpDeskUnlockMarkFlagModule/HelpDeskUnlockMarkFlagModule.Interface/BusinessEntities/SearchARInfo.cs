using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.HelpDeskUnlockMarkFlagModule.Interface.BusinessEntities
{
    [DataContract]
    public class SearchARInfo
    {
        private string _caId;
        private string _invoiceNo;
        private string _period;
        private string _amount;
        private string _markflagStatus;

        [DataMember(Order=1)]
        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }

        [DataMember(Order=2)]
        public string InvoiceNo
        {
            get { return this._invoiceNo; }
            set { this._invoiceNo = value; }
        }

        [DataMember(Order=3)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }

        [DataMember(Order=4)]
        public string Amount
        {
            get { return this._amount; }
            set { this._amount = value; }
        }

        [DataMember(Order=5)]
        public string MarkflagStatus
        {
            get { return this._markflagStatus; }
            set { this._markflagStatus = value; }
        }
    }
}

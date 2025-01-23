using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities
{
    [DataContract]
    public class PaymentTransParam
    {
        private string _fileId;
        private string _prefix;
        private string _posId;		
	    private string _ptId;
        private string _receiptType;
        private DateTime? _modifiedDt;
        private string _modifiedBy;


        [DataMember(Order=1)]
        public string FileId
        {
            get { return this._fileId; }
            set { this._fileId = value; }
        }


        [DataMember(Order=2)]
        public string Prefix
        {
            get { return this._prefix; }
            set { this._prefix = value; }
        }


        [DataMember(Order=3)]
        public string PosId
        {
            get { return this._posId; }
            set { this._posId = value; }
        }


        [DataMember(Order=4)]
        public string PtId
        {
            get { return this._ptId; }
            set { this._ptId = value; }
        }


        [DataMember(Order=5)]
        public string ReceiptType
        {
            get { return this._receiptType; }
            set { this._receiptType = value; }
        }


        [DataMember(Order=6)]
        public DateTime? ModifiedDt
        {
            get { return this._modifiedDt; }
            set { this._modifiedDt = value; }
        }


        [DataMember(Order=7)]
        public string ModifiedBy
        {
            get { return this._modifiedBy; }
            set { this._modifiedBy = value; }
        }

    }
}

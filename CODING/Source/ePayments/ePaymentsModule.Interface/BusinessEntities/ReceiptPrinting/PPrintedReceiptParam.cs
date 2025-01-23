using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities.ReceiptPrinting
{
    [DataContract]
    public class PPrintedReceiptParam
    {
        private DateTime? _uploadDt;
        private DateTime? _paymentDt;
        private string _companyId;
        private string _receiptId;
        private string _branchId;


        [DataMember(Order=1)]
        public DateTime? UploadDt
        {
            get { return this._uploadDt; }
            set { this._uploadDt = value; }
        }


        [DataMember(Order=2)]
        public DateTime? PaymentDt
        {
            get { return this._paymentDt; }
            set { this._paymentDt = value; }
        }


        [DataMember(Order=3)]
        public string CompanyId
        {
            get { return this._companyId; }
            set { this._companyId = value; }
        }


        [DataMember(Order=4)]
        public string ReceiptId
        {
            get { return this._receiptId; }
            set { this._receiptId = value; }
        }


        [DataMember(Order=5)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }

    }
}

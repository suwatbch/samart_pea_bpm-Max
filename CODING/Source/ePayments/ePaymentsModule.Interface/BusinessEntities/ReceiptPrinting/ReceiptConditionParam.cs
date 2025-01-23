using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities.ReceiptPrinting
{
    [DataContract]
    public class ReceiptConditionParam
    {
        private string _beginCaId;
        private string _endCaId;
        private string _invoiceNo;
        private string _receiptId;
        private DateTime? _uploadDt;
        private string _companyId;



        [DataMember(Order=1)]
        public string BeginCaId
        {
            get { return this._beginCaId; }
            set { this._beginCaId = value; }
        }


        [DataMember(Order=2)]
        public string EndCaId
        {
            get { return this._endCaId; }
            set { this._endCaId = value; }
        }


        [DataMember(Order=3)]
        public string InvoiceNo
        {
            get { return this._invoiceNo; }
            set { this._invoiceNo = value; }
        }


        [DataMember(Order=4)]
        public string ReceiptId
        {
            get { return this._receiptId; }
            set { this._receiptId = value; }
        }


        [DataMember(Order=5)]
        public DateTime? UploadDt
        {
            get { return this._uploadDt; }
            set { this._uploadDt = value; }
        }


        [DataMember(Order=6)]
        public string CompanyId
        {
            get { return this._companyId; }
            set { this._companyId = value; }
        }

    }
}

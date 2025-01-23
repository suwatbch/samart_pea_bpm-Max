using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities
{
    [DataContract]
    public class BPMClearifyInfo
    {
        string _branchId = String.Empty;
        string _caId = String.Empty;
        string _caName = String.Empty;
        string _invoiceNo = String.Empty;
        string _period = String.Empty;
        decimal _debtAmount = 0 ;
        DateTime? _dueDt;
        DateTime? _PaymentDt;


        [DataMember(Order=1)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }

        [DataMember(Order=2)]
        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }

        [DataMember(Order=3)]
        public string CaName
        {
            get { return this._caName; }
            set { this._caName = value; }
        }


        [DataMember(Order=4)]
        public string InvoiceNo
        {
            get { return this._invoiceNo; }
            set { this._invoiceNo = value; }
        }


        public string DisplayInvoiceNo
        {
            get
            {
                string retVal = String.Empty;
                if (InvoiceNo != String.Empty)
                {
                    retVal = InvoiceNo.Substring(4, 12);
                }
                return retVal;
            }
        }


        [DataMember(Order=5)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }

        [DataMember(Order=6)]
        public decimal DebtAmount
        {
            get { return this._debtAmount; }
            set { this._debtAmount = value; }
        }

        [DataMember(Order=7)]
        public DateTime? DueDt
        {
            get { return this._dueDt; }
            set { this._dueDt = value; }
        }


        [DataMember(Order=8)]
        public DateTime? PaymentDt
        {
            get { return this._PaymentDt; }
            set { this._PaymentDt = value; }
        }

    }
}

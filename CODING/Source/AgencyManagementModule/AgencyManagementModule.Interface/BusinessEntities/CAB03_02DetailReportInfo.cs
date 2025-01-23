using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CAB03_02DetailReportInfo
    {
        List<BillInfoInEachBillBookIdInfo> _billLineItem = new List<BillInfoInEachBillBookIdInfo>();
        List<BillBookPrePaidInfo> _prePaidItem = new List<BillBookPrePaidInfo>();
        decimal? _totalAmount = 0;        
        decimal? _cashAmount = 0;
        decimal? _chequeAmount = 0;
        decimal? _totalNetAmount = 0;
        string _billReturnDate;       


        [DataMember(Order=1)]
        public List<BillInfoInEachBillBookIdInfo> BillLineItem
        {
            get { return this._billLineItem; }
            set { this._billLineItem = value; }
        }

        [DataMember(Order=2)]
        public List<BillBookPrePaidInfo> PrePaidItem
        {
            get { return this._prePaidItem; }
            set { this._prePaidItem = value; }
        }


        [DataMember(Order=3)]
        public decimal?  TotalAmount
        {
            get { return this._totalAmount;}
            set { this._totalAmount = value;}
        }
      

        [DataMember(Order=4)]
        public decimal? CashAmount
        {
            get { return this._cashAmount;}
            set { this._cashAmount = value;}
        }

        [DataMember(Order=5)]
        public decimal? ChequeAmount
        {
            get { return this._chequeAmount;}
            set { this._chequeAmount = value;}
        }

        [DataMember(Order=6)]
        public decimal? TotalNetAmount
        {
            get { return this._totalNetAmount; }
            set { this._totalNetAmount = value; }
        }

        //Checked TongKung
        //[DataMember(Order=7)]
        public decimal? PrePaidAmount
        {
            get 
            {
                decimal? retVal = 0;
                foreach (BillBookPrePaidInfo b in PrePaidItem)
                {
                    retVal += b.Amount;
                }
                return retVal;
            }
        }
    }
}

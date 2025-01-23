using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class GreenBillSummary
    {
        string _bankId;


        [DataMember(Order=1)]
        public string BankId
        {
            get { return _bankId; }
            set { _bankId = value; }
        }
        string _dueDate;


        [DataMember(Order=2)]
        public string DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value; }
        }
        string _fromCaId;


        [DataMember(Order=3)]
        public string FromCaId
        {
            get { return _fromCaId; }
            set { _fromCaId = value; }
        }
        string _toCaId;


        [DataMember(Order=4)]
        public string ToCaId
        {
            get { return _toCaId; }
            set { _toCaId = value; }
        }
        string _billPeriod;


        [DataMember(Order=5)]
        public string BillPeriod
        {
            get { return _billPeriod; }
            set { _billPeriod = value; }
        }
        string _mtNo;


        [DataMember(Order=6)]
        public string MtNo
        {
            get { return _mtNo; }
            set { _mtNo = value; }
        }
        int? _totBill;


        [DataMember(Order=7)]
        public int? TotBill
        {
            get { return _totBill; }
            set { _totBill = value; }
        }
        decimal? _totAmt;


        [DataMember(Order=8)]
        public decimal? TotAmt
        {
            get { return _totAmt; }
            set { _totAmt = value; }
        }
    }
}

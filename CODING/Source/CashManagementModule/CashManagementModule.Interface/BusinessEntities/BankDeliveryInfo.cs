using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BankDeliveryInfo
    {
        private string _APId;
        private int _order;
        private string _bankDesc;
        private string _bankAccNo;
        private decimal? _cashAmt;
        private decimal? _chequeAmt;
        private decimal? _totalAmt;
        private DateTime? _paymentDt;



        [DataMember(Order=1)]
        public string APId
        {
            set { _APId = value; }
            get { return _APId; }
        }


        [DataMember(Order=2)]
        public int Order
        {
            set { _order = value; }
            get { return _order; }
        }


        [DataMember(Order=3)]
        public string BankDesc
        {
            set { _bankDesc = value; }
            get { return _bankDesc; }
        }


        [DataMember(Order=4)]
        public string BankAccNo
        {
            set { _bankAccNo = value; }
            get { return _bankAccNo; }
        }


        [DataMember(Order=5)]
        public decimal? CashAmt
        {
            set { _cashAmt = value; }
            get { return _cashAmt; }
        }


        [DataMember(Order=6)]
        public decimal? ChequeAmt
        {
            set { _chequeAmt = value; }
            get { return _chequeAmt; }
        }


        [DataMember(Order=7)]
        public decimal? TotalAmt
        {
            set { _totalAmt = value; }
            get { return _totalAmt; }
        }


        [DataMember(Order=8)]
        public DateTime? PaymentDt
        {
            set { _paymentDt = value; }
            get { return _paymentDt; }
        }

        //Checked TongKung
        //[DataMember(Order=9)]
        public string PaymentDate
        {
            get { return _paymentDt.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH")); }
        }


    
    }
}

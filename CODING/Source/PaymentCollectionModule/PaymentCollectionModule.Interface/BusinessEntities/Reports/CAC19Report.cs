using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Globalization;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports
{
    [DataContract, Serializable]
    public class CAC19Report
    {
        private string _cashierId;
        private string _cashierName;
        private decimal? _gAmount;
        private DateTime? _paymentDt;
        private int? _numOfTrans;
        private int? _record_qty_new;

        private string _qrRef1;
        private string _qrRef2;

        private int _completedCount;
        private decimal _completedGAmount; 

        // Bank session. 
        private string _qrRef1Bank;
        private string _qrRef2Bank;
        private decimal? _gAmountBank;
        private string _qrCompleted;
        private string _note;


        [DataMember(Order = 1)]
        public string CashierId
        {
            get { return _cashierId; }
            set { _cashierId = value; }
        }

        [DataMember(Order = 2)]
        public string ControllerName
        {
            get { return _cashierName; }
            set { _cashierName = value; }
        }


        [DataMember(Order = 3)]
        public decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }

        //Checked TongKung
        //[DataMember(Order=23)]
        public string PaymentDate
        {
            get { return  _paymentDt == null ? "-" : _paymentDt.Value.ToString("dd/MM/yyyy  HH:mm:ss", new CultureInfo("th-TH")); }
        }


        [DataMember(Order = 4)]
        public int? NumOfTrans
        {
            get { return _numOfTrans; }
            set { _numOfTrans = value; }
        }

        [DataMember(Order = 5)]
        public string QRRef1
        {
            get { return _qrRef1; }
            set { _qrRef1 = value; }
        }

        [DataMember(Order = 6)]
        public string QRRef2
        {
            get { return _qrRef2; }
            set { _qrRef2 = value; }
        }


        [DataMember(Order = 7)]
        public DateTime? PaymentDt
        {
            get { return _paymentDt; }
            set { _paymentDt = value; }
        }
        // Bank session. 
        //private string _qrRef1Bank;
        //private string _qrRef2Bank;
        //private decimal? _gAmountBank;
        [DataMember(Order = 8)]
        public string QRRef1Bank
        {
            get { return _qrRef1Bank; }
            set { _qrRef1Bank = value; }
        }

        [DataMember(Order = 9)]
        public string QRRef2Bank
        {
            get { return _qrRef2Bank; }
            set { _qrRef2Bank = value; }
        }

        [DataMember(Order = 10)]
        public decimal? GAmountBank
        {
            get { return _gAmountBank; }
            set { _gAmountBank = value; }
        }

        //private string _qrCompleted;
        [DataMember(Order = 11)]
        public string QRCompleted
        {
            get { return _qrCompleted; }
            set { _qrCompleted = value; }
        }


        //private int completedCount;
        //private decimal completedGAmount; 
        [DataMember(Order = 12)]
        public int QRCompletedCount
        {
            get { return _completedCount; }
            set { _completedCount = value; }
        }

        [DataMember(Order = 13)]
        public decimal QRCompletedGAmount
        {
            get { return _completedGAmount; }
            set { _completedGAmount = value; }
        }

        [DataMember(Order = 14)]
        public int? Record_Qty_New
        {
            get { return _record_qty_new; }
            set { _record_qty_new = value; }
        }

        [DataMember(Order = 15)]
        public string Note
        {
            get { return _note; }
            set { _note = value; }
        }

    }
}

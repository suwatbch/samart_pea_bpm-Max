using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports
{
    [DataContract, Serializable]
    public class CAC18Report
    {
        private string _paymentId;
        private string _mru;
        private string _branchId;
        private string _period;
        private string _controllerName;
        private string _caId;
        private string _debtType;
        private string _receiptId;
        private int? _quantity;
        private decimal? _baseAmount;
        private decimal? _ftAmount;
        private decimal? _vatAmount;
        private decimal? _gAmount;
        private decimal? _cashAmount;
        private decimal? _chequeAmount;
        private decimal? _transferAmount;
        private decimal? _creditDebitAmount;
        private decimal? _qrPaymentAmount;
        private decimal? _totalAmount;
        private decimal? _adjAmount;
        private decimal? _feeAmount;
        private string _type;
        private string _dtId;
        private DateTime? _paymentDt;
        private int? _caId_Qty;
        private int? _caId_Qty_New;
        private int? _numOfTrans;

        private string _qrRef1;
        private string _qrRef2;
        private string _qrPaymentRefNo;
        private string _qrOnlineFlag;
        private string _invoiceNo;

        [DataMember(Order = 1)]
        public string PaymentId
        {
            get { return _paymentId; }
            set { _paymentId = value; }
        }


        [DataMember(Order = 2)]
        public string Mru
        {
            get { return _mru; }
            set { _mru = value; }
        }


        [DataMember(Order = 3)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order = 4)]
        public string Period
        {
            get { return _period; }
            set { _period = value; }
        }


        [DataMember(Order = 5)]
        public string ControllerName
        {
            get { return _controllerName; }
            set { _controllerName = value; }
        }


        [DataMember(Order = 6)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }


        [DataMember(Order = 7)]
        public string DebtType
        {
            get { return _debtType; }
            set { _debtType = value; }
        }


        [DataMember(Order = 8)]
        public string ReceiptId
        {
            get { return _receiptId; }
            set { _receiptId = value; }
        }


        [DataMember(Order = 9)]
        public int? Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }


        [DataMember(Order = 10)]
        public decimal? BaseAmount
        {
            get { return _baseAmount; }
            set { _baseAmount = value; }
        }


        [DataMember(Order = 11)]
        public decimal? FtAmount
        {
            get { return _ftAmount; }
            set { _ftAmount = value; }
        }


        [DataMember(Order = 12)]
        public decimal? VatAmount
        {
            get { return _vatAmount; }
            set { _vatAmount = value; }
        }


        [DataMember(Order = 13)]
        public decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }


        [DataMember(Order = 14)]
        public decimal? CashAmount
        {
            get { return _cashAmount; }
            set { _cashAmount = value; }
        }


        [DataMember(Order = 15)]
        public decimal? ChequeAmount
        {
            get { return _chequeAmount; }
            set { _chequeAmount = value; }
        }


        [DataMember(Order = 16)]
        public decimal? TransferAmount
        {
            get { return _transferAmount; }
            set { _transferAmount = value; }
        }


        [DataMember(Order = 17)]
        public decimal? CreditDebitAmount
        {
            get { return _creditDebitAmount; }
            set { _creditDebitAmount = value; }
        }

        [DataMember(Order = 18)]
        public decimal? QRPaymentAmount
        {
            get { return _qrPaymentAmount; }
            set { _qrPaymentAmount = value; }
        }

        [DataMember(Order = 19)]
        public decimal? TotalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; }
        }

        [DataMember(Order = 20)]
        public decimal? AdjAmount
        {
            get { return _adjAmount; }
            set { _adjAmount = value; }
        }


        [DataMember(Order = 21)]
        public decimal? FeeAmount
        {
            get { return _feeAmount; }
            set { _feeAmount = value; }
        }


        [DataMember(Order = 22)]
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }


        [DataMember(Order = 23)]
        public string DtId
        {
            get { return _dtId; }
            set { _dtId = value; }
        }


        [DataMember(Order = 24)]
        public DateTime? PaymentDt
        {
            get { return _paymentDt; }
            set { _paymentDt = value; }
        }

        //Checked TongKung
        //[DataMember(Order=23)]
        public string PaymentDate
        {
            get { return _paymentDt.Value.ToString("dd/MM/yyyy  HH:mm:ss", new CultureInfo("th-TH")); }
        }


        [DataMember(Order = 25)]
        public int? CaId_Qty
        {
            get { return _caId_Qty; }
            set { _caId_Qty = value; }
        }


        [DataMember(Order = 26)]
        public int? NumOfTrans
        {
            get { return _numOfTrans; }
            set { _numOfTrans = value; }
        }

        [DataMember(Order = 27)]
        public int? CaId_Qty_New
        {
            get { return _caId_Qty_New; }
            set { _caId_Qty_New = value; }
        }

        [DataMember(Order = 28)]
        public string QRRef1
        {
            get { return _qrRef1; }
            set { _qrRef1 = value; }
        }

        [DataMember(Order = 29)]
        public string QRRef2
        {
            get { return _qrRef2; }
            set { _qrRef2 = value; }
        }

        [DataMember(Order = 30)]
        public string QRPaymentRefNo
        {
            get { return _qrPaymentRefNo; }
            set { _qrPaymentRefNo = value; }
        }

        [DataMember(Order = 31)]
        public string QROnlineFlag
        {
            get { return _qrOnlineFlag; }
            set { _qrOnlineFlag = value; }
        }

        [DataMember(Order = 32)]
        public string InvoinceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }

    }
}

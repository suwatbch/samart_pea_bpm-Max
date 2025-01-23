using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class PaidMethod
    {
        private string _methodId;

        [DataMember(Order=1)]
        public string MethodId
        {
            get { return _methodId; }
            set { _methodId = value; }
        }

        private string _bankName;

        [DataMember(Order=2)]
        public string BankName
        {
            get { return _bankName; }
            set { _bankName = value; }
        }

        private string _chqNo;

        [DataMember(Order=3)]
        public string ChqNo
        {
            get { return _chqNo; }
            set { _chqNo = value; }
        }

        private string _chqAccNo;

        [DataMember(Order=4)]
        public string ChqAccNo
        {
            get { return _chqAccNo; }
            set { _chqAccNo = value; }
        }

        private DateTime? _chqDate;

        [DataMember(Order=5)]
        public DateTime? ChqDate
        {
            get { return _chqDate; }
            set { _chqDate = value; }
        }

        private string _transferAccNo;

        [DataMember(Order=6)]
        public string TransferAccNo
        {
            get { return _transferAccNo; }
            set { _transferAccNo = value; }
        }

        private DateTime? _transferDate;

        [DataMember(Order=7)]
        public DateTime? TransferDate
        {
            get { return _transferDate; }
            set { _transferDate = value; }
        }

        private decimal? _amount;

        [DataMember(Order=8)]
        public decimal? Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }


        public string DisplayAmount
        {
            get {
                decimal amt = -_amount.Value;
                return amt.ToString("#,##0.00"); 
            }
        }


        //[DataMember(Order=10)]
        public string Description
        {
            get
            {
                switch (_methodId)
                {
                    case CodeNames.PaymentType.Cash.Id:
                        return "เงินสด";
                    case CodeNames.PaymentType.Cheque.Id:
                        return string.Format("เช็ค {0} เลขที่เช็ค:{1} เลขที่บัญชีเช็ค:{2} วันที่เช็ค:{3}",
                            _bankName,
                            _chqNo,
                            _chqAccNo,
                            _chqDate.Value.ToString("dd/MM/yyyy"));
                    case CodeNames.PaymentType.Deposit.Id:
                        return string.Format("ใบรับฝาก เลขที่บัญชี:{1} วันที่นำฝาก:{2}",
                            _bankName,
                            _transferAccNo,
                            _transferDate.Value.ToString("dd/MM/yyyy"));
                    case CodeNames.PaymentType.QRPayment.Id:
                        return "QR Payment";
                    default:
                        return "";
                }
            }
        }
    }
}

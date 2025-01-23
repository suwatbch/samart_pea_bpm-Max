using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract, Serializable]
    public class PaymentMethod: CloneBase<PaymentMethod>, IComparable<PaymentMethod>
    {
        private int _uiRefId; 
        

        [DataMember(Order=1)]
        public int UiRefId
        {
            get { return _uiRefId; }
            set { _uiRefId = value; }
        }

        private string _ptId;

        [DataMember(Order=2)]
        public string PtId
        {
            get { return _ptId; }
            set { _ptId = value; }
        }

        private string _ptName;

        [DataMember(Order=3)]
        public string PtName
        {
            get { return _ptName; }
            set { _ptName = value; }
        }

        //Checked TongKung
        //[DataMember(Order=4)]  
        public string Description
        {
            get
            {
                switch (_ptId)
                {
                    case CodeNames.PaymentType.Cash.Id:
                        if (_feeAmount != 0)
                        {
                            return string.Format("ค่าธรรมเนียม: {0} บาท", (_feeAmount == null) ? "0" : _feeAmount.Value.ToString("#,###.##"));
                        }
                        else
                            return "-";
                    case CodeNames.PaymentType.Cheque.Id:
                        return string.Format("{0} เลขที่เช็ค:{1} เลขที่บัญชีเช็ค:{2} วันที่เช็ค:{3} ค่าธรรมเนียม: {4} บาท",
                            _bank.BankName,
                            _chqNo,
                            _chqAccNo,
                            _chqDt.Value.ToString("dd/MM/yyyy"),
                            (_feeAmount == null) ? "0" : _feeAmount.Value.ToString("#,###.##"));
                    case CodeNames.PaymentType.Deposit.Id:
                        return string.Format("{0} เลขที่บัญชี:{1} วันที่นำฝาก:{2} ค่าธรรมเนียม: {3} บาท",
                            _bank.BankName,
                            _depositAccNoDesc,
                            _depositDt.Value.ToString("dd/MM/yyyy"),
                            (_feeAmount == null) ? "0" : _feeAmount.Value.ToString("#,###.##"));
                    case CodeNames.PaymentType.QRPayment.Id:
                        return "-";
                    default:
                        return "";
                }
            }
        }

        private decimal? _toPayAmount;
        /// <summary>
        /// จำนวนเงินที่ยื่นให้เจ้าหน้าที่
        /// </summary>

        [DataMember(Order=5)]
        public decimal? ToPayAmount
        {
            get { return _toPayAmount; }
            set { _toPayAmount = value; }
        }

        private decimal? _changeAmount;
        /// <summary>
        /// จำนวนเงินทอน
        /// </summary>

        [DataMember(Order=6)]
        public decimal? ChangeAmount
        {
            get { return _changeAmount; }
            set { _changeAmount = value; }
        }

        private decimal? _feeAmount = 0;
        /// <summary>
        /// จำนวนเงินค่าธรรมเนียม
        /// </summary>

        [DataMember(Order=7)]
        public decimal? FeeAmount
        {
            get { return _feeAmount; }
            set { _feeAmount = value; }
        }

        //Checked TongKung
        //[DataMember(Order=8)]      
        public decimal? ToPayAmountWithFee
        {
            get
            {
                return _toPayAmount + _feeAmount;
            }
        }
        /// <summary>
        /// จำนวนเงินตัดหนี้จริง (จำนวนเงินยื่นให้เจ้าหน้าที่ - เงินทอน + ค่าธรรมเนียม)
        /// </summary>

        //Checked TongKung
        //[DataMember(Order=9)]     
        public decimal? ActualAmount
        {
            get
            {
                if (null == _changeAmount)
                {
                    return ToPayAmountWithFee;
                }
                else
                {
                    return ToPayAmountWithFee - _changeAmount;
                }                
            }
        }

        private Bank _bank;

        [DataMember(Order=10)]
        public Bank Bank
        {
            get { return _bank; }
            set { _bank = value; }
        }

        //Checked TongKung
        //[DataMember(Order=11)]
        public string BankId
        {
            get
            {
                return null==_bank? null: _bank.BankKey;
            }
        }

        //Checked TongKung
        //[DataMember(Order=12)]
        public string BankName
        {
            get
            {
                return null==_bank? null: _bank.BankName;
            }
            set 
            {
                _bank.BankName = value;             
            }
        }

        private string _chqNo;

        [DataMember(Order=13)]
        public string ChqNo
        {
            get { return _chqNo; }
            set { _chqNo = value; }
        }

        private string _chqAccNo;

        [DataMember(Order=14)]
        public string ChqAccNo
        {
            get { return _chqAccNo; }
            set { _chqAccNo = value; }
        }

        private DateTime? _chqDt;

        [DataMember(Order=15)]
        public DateTime? ChqDt
        {
            get { return _chqDt; }
            set { _chqDt = value; }
        }

        private string _depositAccNo;

        [DataMember(Order=16)]
        public string DepositAccNo
        {
            get { return _depositAccNo; }
            set { _depositAccNo = value; }
        }

        private DateTime? _depositDt;

        [DataMember(Order=17)]
        public DateTime? DepositDt
        {
            get { return _depositDt; }
            set { _depositDt = value; }
        }

        private bool? _isAGPayment;

        [DataMember(Order=18)]
        public bool? IsAGPayment
        {
            get { return _isAGPayment; }
            set { _isAGPayment = value; }
        }

        private string _draftFlag;

        [DataMember(Order=19)]
        public string DraftFlag
        {
            get { return _draftFlag; }
            set { _draftFlag = value; }
        }

        private string _cashierChequeFlag;

        [DataMember(Order=20)]
        public string CashierChequeFlag
        {
            get { return _cashierChequeFlag; }
            set { _cashierChequeFlag = value; }
        }

        private string _arptId;

        [DataMember(Order=21)]
        public string ARPtId
        {
            get { return _arptId; }
            set { _arptId = value; }
        }

        private List<InvoicePaymentMethod> _toPayInvoices = new List<InvoicePaymentMethod>();

        [DataMember(Order=22)]
        public List<InvoicePaymentMethod> ToPayInvoices
        {
            get { return _toPayInvoices; }
            set { _toPayInvoices = value; }
        }

        private string _depositAccType;

        [DataMember(Order = 23)]
        public string DepositAccType
        {
            get { return _depositAccType; }
            set { _depositAccType = value; }
        }

        private string _depositAccNoDesc;

        [DataMember(Order = 24)]
        public string DepositAccNoDesc
        {
            get { return _depositAccNoDesc; }
            set { _depositAccNoDesc = value; }
        }

        //Checked TongKung
        //[DataMember(Order=25)]
        public decimal TotalPayInvoiceAmount
        {
            get
            {
                decimal result = 0;
                if (_toPayInvoices != null)
                {
                    foreach (InvoicePaymentMethod ivpm in _toPayInvoices)
                    {
                        result += ivpm.Amount;
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// จำนวนเงินที่เหลือหลังจากกำหนดให้ Invoice
        /// </summary>
        //Checked TongKung
        //[DataMember(Order=26)]
        public decimal TotalRemainAmount
        {
            get
            {
                return ActualAmount.Value - TotalPayInvoiceAmount;
            }
        }

        //Checked TongKung
        //[DataMember(Order=27)]
        public string GroupBankName
        {
            get
            {
                return null == _bank ? null : _bank.GroupBankName;
            }
            set {
                _bank.GroupBankName = value;
            }
        }

        //Checked TongKung
        //[DataMember(Order=28)]
        public string ClaringAccNo
        {
            get
            {
                return null == _bank ? null : _bank.ClearingAccNo;
            }
        }

        public static int GetOrderPtNumber(string ptId)
        {
            switch (ptId)
            {
                case "1":
                    return 3;
                case "2":
                    return 1;
                case "3":
                    return 2;
                default:
                    return 9;
            }
        }

        public PaymentMethod() { }

        public PaymentMethod(PaymentMethod pm)
        {
            this.ARPtId = pm.ARPtId;
            this.Bank = pm.Bank;
            this.CashierChequeFlag = pm.CashierChequeFlag;
            this.ChangeAmount = pm.ChangeAmount;
            this.ChqAccNo = pm.ChqAccNo;
            this.ChqDt = pm.ChqDt;
            this.ChqNo = pm.ChqNo;
            this.DepositAccNo = pm.DepositAccNo;
            this.DepositDt = pm.DepositDt;
            this.DraftFlag = pm.DraftFlag;
            this.FeeAmount = pm.FeeAmount;
            this.IsAGPayment = pm.IsAGPayment;
            this.PtId = pm.PtId;
            this.PtName = pm.PtName;
            this.ToPayAmount = pm.ToPayAmount;
            this.ToPayInvoices = pm.ToPayInvoices;
            this.UiRefId = pm.UiRefId;
            this.DepositAccType = pm.DepositAccType;
            this.DepositAccNoDesc = pm.DepositAccNoDesc;
        }

        #region IComparable<PaymentMethod> Members

        public int CompareTo(PaymentMethod other)
        {
            int t1 = GetOrderPtNumber(_ptId);
            int t2 = GetOrderPtNumber(other.PtId);

            return t1.CompareTo(t2);
        }

        #endregion
    }
}

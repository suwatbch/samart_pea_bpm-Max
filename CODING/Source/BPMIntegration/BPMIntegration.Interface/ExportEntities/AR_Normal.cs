using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using System.Globalization;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;

namespace PEA.BPM.Integration.BPMIntegration.Interface.ExportEntities
{
    public class AR_Normal : IListUtility<AR_Normal>
    {
        string _action;
        string _posRSG;
        string _ptId;
        string _clearingAccNo;
        DateTime? _paymentDt;
        string _posBA;
        string _posBP;
        string _caId;
        string _invoiceNo;
        string _caDoc;
        string _receiptType;
        string _receiptId;
        string _posId;
        string _cashierId;
        decimal? _actualAmount;
        decimal? _amount;
        decimal? _overUnder;
        decimal? _totalActualAmount;
        decimal? _totalAmount;
        decimal? _totalOverUnder;
        string _partialPayment;
        decimal? _fee;       
        string _bankKey;
        string _chqNo;
        string _chqAccNo;
        DateTime? _transfDt;
        string _spotBillInvoiceNo;
        string _period;
        string _groupInvoiceNo;
        string _billBookId;
        string _receiptCondition;
        DateTime? _dueDt;
        string _mainSub;
        string _name;
        string _address;
        string _description;
        int? _qty;
        decimal? _priceUnit;
        decimal? _baseAmount;
        string _vatCode;
        decimal? _vatAmount;
        string _disconnectDoc;
        string _originalInvoiceNo;
        DateTime? _syncDt;


        public string Action
        {
            get { return this._action; }
            set { this._action = value; }
        }

        public string PosRSG
        {
            get { return this._posRSG; }
            set { this._posRSG = value; }
        }

        public string PtId
        {
            get { return this._ptId; }
            set { this._ptId = value; }
        }

        public string ClearingAccNo
        {
            get { return this._clearingAccNo; }
            set { this._clearingAccNo = value; }
        }

        public DateTime? PaymentDt
        {
            get { return this._paymentDt; }
            set { this._paymentDt = value; }
        }

        public string PosBA
        {
            get { return this._posBA; }
            set { this._posBA = value; }
        }

        public string PosBP
        {
            get { return this._posBP; }
            set { this._posBP = value; }
        }

        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }

        public string InvoiceNo
        {
            get 
            {
                if (_invoiceNo == null ||
                    (_invoiceNo != null && _invoiceNo.Length == 22))
                {
                    int suffix = _invoiceNo.IndexOf("---");
                    if (suffix > -1)
                    {
                        return _invoiceNo.Substring(0, suffix);
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return _invoiceNo;
                }
            }
            set { this._invoiceNo = value; }
        }

        public string CaDoc
        {
            get { return this._caDoc; }
            set { this._caDoc = value; }
        }

        public string ReceiptType
        {
            get { return this._receiptType; }
            set { this._receiptType = value; }
        }

        public string ReceiptId
        {
            get { return this._receiptId; }
            set { this._receiptId = value; }
        }

        public string PosId
        {
            get { return this._posId; }
            set { this._posId = value; }
        }

        public string CashierId
        {
            get { return this._cashierId; }
            set { this._cashierId = value; }
        }

        public decimal? ActualAmount
        {
            get { return this._actualAmount; }
            set { this._actualAmount = value; }
        }

        public decimal? Amount
        {
            get { return this._amount; }
            set { this._amount = value; }
        }

        public decimal? OverUnder
        {
            get { return this._overUnder; }
            set { this._overUnder = value; }
        }

        public decimal? TotalActualAmount
        {
            get { return this._totalActualAmount; }
            set { this._totalActualAmount = value; }
        }

        public decimal? TotalAmount
        {
            get { return this._totalAmount; }
            set { this._totalAmount = value; }
        }

        public decimal? TotalOverUnder
        {
            get { return this._totalOverUnder; }
            set { this._totalOverUnder = value; }
        }

        public string PartialPayment
        {
            get { return this._partialPayment; }
            set { this._partialPayment = value; }
        }

        public decimal? Fee
        {
            get { return this._fee; }
            set { this._fee = value; }
        }

        public string BankKey
        {
            get { return this._bankKey; }
            set { this._bankKey = value; }
        }

        public string ChqNo
        {
            get { return this._chqNo; }
            set { this._chqNo = value; }
        }

        public string ChqAccNo
        {
            get { return this._chqAccNo; }
            set { this._chqAccNo = value; }
        }

        public DateTime? TransfDt
        {
            get { return this._transfDt; }
            set { this._transfDt = value; }
        }

        public string SpotBillInvoiceNo
        {
            get { return this._spotBillInvoiceNo ; }
            set { this._spotBillInvoiceNo = value; }
        }

        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }

        public string GroupInvoiceNo
        {
            get { return this._groupInvoiceNo; }
            set { this._groupInvoiceNo = value; }
        }

        public string BillBookId
        {
            get { return this._billBookId; }
            set { this._billBookId = value; }
        }

        public string ReceiptCondition
        {
            get { return this._receiptCondition; }
            set { this._receiptCondition = value; }
        }

        public DateTime? DueDt
        {
            get { return this._dueDt; }
            set { this._dueDt = value; }
        }

        public string MainSub
        {
            get { return this._mainSub; }
            set { this._mainSub = value; }
        }

        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        public string Address
        {
            get { return this._address; }
            set { this._address = value; }
        }

        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        public int? Qty
        {
            get { return this._qty; }
            set { this._qty = value; }
        }

        public decimal? PriceUnit
        {
            get { return this._priceUnit; }
            set { this._priceUnit = value; }
        }

        public decimal? BaseAmount
        {
            get { return this._baseAmount; }
            set { this._baseAmount = value; }
        }

        public string VatCode
        {
            get { return this._vatCode; }
            set { this._vatCode = value; }
        }

        public decimal? VatAmount
        {
            get { return this._vatAmount; }
            set { this._vatAmount = value; }
        }

        public string DisconnectDoc
        {
            get { return this._disconnectDoc; }
            set { this._disconnectDoc = value; }
        }

        public string OriginalInvoiceNo
        {
            get { return this._originalInvoiceNo; }
            set { this._originalInvoiceNo = value; }
        }   

        public DateTime? SyncDt
        {
            get { return this._syncDt; }
            set { this._syncDt = value; }
        }


        #region IListUtility<AR_Normal> Members

        public string ToStream()
        {
            IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("en-US");
            string[] template = { Action == null ? string.Empty : Action, 
                                    PosRSG == null ? string.Empty : PosRSG, 
                                    PtId  == null ? string.Empty : PtId, 
                                    ClearingAccNo == null ? string.Empty : ClearingAccNo, 
                                    PaymentDt  == null ? string.Empty : PaymentDt.Value.ToString("yyyyMMdd", formatDate),
                                    PosBA == null ? string.Empty : PosBA, 
                                    PosBP == null ? string.Empty : PosBP, 
                                    CaId == null ? string.Empty : CaId, 
                                    InvoiceNo  == null ? string.Empty : InvoiceNo.IndexOf("---") < 0 ? InvoiceNo : InvoiceNo.Substring(0, InvoiceNo.IndexOf("---")), //201802261720 Kanokwan.L แก้ไข Export to SAP ex. invoiceno= 81-0140 กบ.---017 เดิมระบบ Export เหลือแค่ "81" แก้ไขเป็น "81-0140 กบ."
                                    CaDoc == null ? string.Empty : CaDoc, 
                                    ReceiptType == null ? string.Empty : ReceiptType, 
                                    ReceiptId == null ? string.Empty : ReceiptId, 
                                    PosId == null ? string.Empty : PosId, 
                                    CashierId == null ? string.Empty : CashierId, 
                                    ActualAmount == null ? string.Empty : ActualAmount.Value.ToString("#0.00"), 
                                    Amount == null ? string.Empty : Amount.Value.ToString("#0.00"), 
                                    OverUnder == null ? string.Empty : OverUnder.Value.ToString("#0.00"), 
                                    PartialPayment == null ? string.Empty : PartialPayment,
                                    Fee == null ? string.Empty : Fee.Value.ToString("#0.00"), 
                                    BankKey == null ? string.Empty : BankKey, 
                                    ChqNo == null ? string.Empty : ChqNo, 
                                    ChqAccNo == null ? string.Empty : ChqAccNo, 
                                    TransfDt == null ? string.Empty : TransfDt.Value.ToString("yyyyMMdd", formatDate),
                                    SpotBillInvoiceNo == null ? string.Empty : SpotBillInvoiceNo, 
                                    Period == null ? string.Empty : Period, 
                                    GroupInvoiceNo == null ? string.Empty : GroupInvoiceNo, 
                                    BillBookId == null ? string.Empty : BillBookId, 
                                    ReceiptCondition == null ? string.Empty : ReceiptCondition,
                                    DueDt == null ? string.Empty : DueDt.Value.ToString("yyyyMMdd", formatDate),
                                    string.Empty,
                                    Name == null ? string.Empty : Name, 
                                    Address == null ? string.Empty : Address,
                                    string.Empty,
                                    string.Empty,
                                    string.Empty,
                                    string.Empty,
                                    string.Empty,
                                    string.Empty,
                                    string.Empty,
                                    string.Empty,
                                    PaymentDt  == null ? string.Empty : PaymentDt.Value.ToString("HH:mm:ss", formatDate) };         
            return string.Join("|", template);
        }

        public AR_Normal ParseExtract(string txt)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}

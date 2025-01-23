using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

using PEA.BPM.AgencyManagementModule.Interface.Constants;
using System.Globalization;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillBookCheckinDetailInfo
    {
        string _bookId = String.Empty;
        string _invoiceNo = String.Empty;
        string _period = String.Empty;
        string _mruId = String.Empty;
        string _caId = String.Empty;
        string _caName = String.Empty;
        string _absId = String.Empty;
        string _aboId = String.Empty;
        string _pmId = String.Empty;
        string _inBook = String.Empty;
        decimal? _paidAmount = 0;
        decimal? _totalAmount = 0;
        decimal? _gAmount = 0;
        decimal? _vat = 0;
        string _branchId = String.Empty;
        int _paidType;
        DateTime? _lastPaidDt;
        List<ChequeInfo> _chequeList = new List<ChequeInfo>();
        DateTime? _modifiedDt;
        string _modifiedBy = String.Empty;
        bool _isCheckIn;
        decimal? _totalDebtAmount;
        bool _arActive;
        bool _invSel;
        decimal? _ItemPaid;

        string _DtName = String.Empty;
        string _SubGroupInvoiceNo = String.Empty;

        [DataMember(Order = 1)]
        public string BookId
        {
            get { return this._bookId; }
            set { this._bookId = value; }
        }


        [DataMember(Order = 2)]
        public string InvoiceNo
        {
            get { return this._invoiceNo; }
            set { this._invoiceNo = value; }
        }

        [DataMember(Order = 3)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }

        [DataMember(Order = 4)]
        public string MruId
        {
            get { return this._mruId; }
            set { this._mruId = value; }
        }

        [DataMember(Order = 5)]
        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }

        [DataMember(Order = 6)]
        public string CaName
        {
            get { return this._caName; }
            set { this._caName = value; }
        }

        [DataMember(Order = 7)]
        public string AboId
        {
            get { return this._aboId; }
            set { this._aboId = value; }
        }

        [DataMember(Order = 8)]
        public string PmId
        {
            get { return this._pmId; }
            set { this._pmId = value; }
        }

        [DataMember(Order = 9)]
        public string InBook
        {
            get { return this._inBook; }
            set { this._inBook = value; }
        }

        [DataMember(Order = 10)]
        public decimal? PaidAmount
        {
            get
            {
                return this._paidAmount;
            }
            set { this._paidAmount = value; }
        }

        [DataMember(Order = 11)]
        public decimal? TotalAmount
        {
            get { return this._totalAmount; }
            set { this._totalAmount = value; }
        }

        [DataMember(Order = 12)]
        public decimal? GAmount
        {
            get { return this._gAmount; }
            set { this._gAmount = value; }
        }

        [DataMember(Order = 13)]
        public decimal? Vat
        {
            get { return this._vat; }
            set { this._vat = value; }
        }

        [DataMember(Order = 14)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }

        [DataMember(Order = 15)]
        public string AbsId
        {
            get { return this._absId; }
            set { this._absId = value; }
        }

        [DataMember(Order = 16)]
        public DateTime? LastPaidDt
        {
            get { return this._lastPaidDt; }
            set { this._lastPaidDt = value; }
        }

        //Checked TongKung
        //[DataMember(Order=17)]
        public string LastPaidDtText
        {
            get
            {
                if (LastPaidDt == null)
                {
                    return String.Empty;
                }
                else
                {
                    return this.LastPaidDt.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                }
            }
        }

        [DataMember(Order = 18)]
        public int PaidType
        {
            get { return this._paidType; }
            set { this._paidType = value; }
        }


        [DataMember(Order = 19)]
        public List<ChequeInfo> ChequeList
        {
            get { return this._chequeList; }
            set { this._chequeList = value; }
        }


        [DataMember(Order = 20)]
        public DateTime? ModifiedDt
        {
            get { return this._modifiedDt; }
            set { this._modifiedDt = value; }
        }


        [DataMember(Order = 21)]
        public string ModifiedBy
        {
            get { return this._modifiedBy; }
            set { this._modifiedBy = value; }
        }

        // GroupInvoice use only

        [DataMember(Order = 22)]
        public bool IsCheckIn
        {
            get { return this._isCheckIn; }
            set { this._isCheckIn = value; }
        }

        // GroupInvoice use only

        [DataMember(Order = 23)]
        public decimal? TotalDebtAmount
        {
            get
            {
                return this._totalDebtAmount;
            }
            set
            {
                this._totalDebtAmount = value;
            }
        }

        [DataMember(Order = 24)]
        public bool ARActive
        {
            get { return this._arActive; }
            set { this._arActive = value; }
        }

        [DataMember(Order = 25)]
        public bool InvSel
        {
            get { return this._invSel; }
            set { this._invSel = value; }
        }

        [DataMember(Order = 26)]
        public decimal? ItemPaid
        {
            get
            {
                return this._ItemPaid;
            }
            set { this._ItemPaid = value; }
        }

        [DataMember(Order = 27)]
        public string DtName
        {
            get
            {
                return this._DtName;
            }
            set { this._DtName = value; }
        }

        [DataMember(Order = 28)]
        public string SubGroupInvoiceNo
        {
            get
            {
                return this._SubGroupInvoiceNo;
            }
            set { this._SubGroupInvoiceNo = value; }
        }
    }
}

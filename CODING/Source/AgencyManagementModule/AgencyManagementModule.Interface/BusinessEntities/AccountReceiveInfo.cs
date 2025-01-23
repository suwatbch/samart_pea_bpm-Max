using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.Constants;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class AccountReceiveInfo
    {
        string _itemId;
		string _caId;

        int _bookType;
        string _billBookId;
        string _groupInvoiceNo;
        string _branchId;
        string _dtId;
		string _description;
		string _invoiceNo;
		string _period;	
		string _taxCode ;
        decimal? _vatAmount = 0;
        decimal? _amount = 0;
        decimal? _unitPrice = 0;
        decimal? _gAmount = 0;        
		DateTime? _dueDt;
		string  _controllerId;
        decimal? _paidAmount = 0;
        DateTime? _cutOffDt;
        string _modifiedBy;
        DateTime? _modifiedDt;


        [DataMember(Order=1)]
        public string ItemId
        {
            get { return this._itemId; }
            set { this._itemId = value; }
        }

        [DataMember(Order=2)]
        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }


        [DataMember(Order=3)]
        public int BookType
        {
            get { return this._bookType; }
            set { this._bookType = value; }
        }


        [DataMember(Order=4)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }

        [DataMember(Order=5)]
        public string BillBookId
        {
            get { return this._billBookId; }
            set { this._billBookId = value; }
        }

        [DataMember(Order=6)]
        public string GroupInvoiceNo
        {
            get { return this._groupInvoiceNo; }
            set { this._groupInvoiceNo = value; }
        }

        [DataMember(Order=7)]
        public string DtId
        {
            get { return this._dtId; }
            set { this._dtId = value; }
        }

        [DataMember(Order=8)]
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        [DataMember(Order=9)]
        public string InvoiceNo
        {
            get { return this._invoiceNo; }
            set { this._invoiceNo = value; }
        }

        [DataMember(Order=10)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }            

        [DataMember(Order=11)]
        public string TaxCode
        {
            get { return this._taxCode; }
            set { this._taxCode = value; }
        }

        [DataMember(Order=12)]
        public decimal? VatAmount
        {
            get { return this._vatAmount; }
            set { this._vatAmount = value; }
        }

        [DataMember(Order=13)]
        public decimal? Amount
        {
            get { return this._amount; }
            set { this._amount = value; }
        }

        [DataMember(Order=14)]
        public decimal? UnitPrice 
        {
            get { return this._unitPrice; }
            set { this._unitPrice = value; }
        }

        [DataMember(Order=15)]
        public decimal? GAmount
        {
            get { return this._gAmount; }
            set { this._gAmount = value; }
        } 

        [DataMember(Order=16)]
        public DateTime? DueDt
        {
            get { return this._dueDt; }
            set { this._dueDt = value; }
        }

        [DataMember(Order=17)]
        public string ControllerId
        {
            get { return this._controllerId; }
            set { this._controllerId = value; }
        }

        [DataMember(Order=18)]
        public decimal? PaidAmount
        {
            get { return this._paidAmount; }
            set { this._paidAmount = value; }
        }

        [DataMember(Order=19)]
        public DateTime? CutOffDt
        {
            get { return this._cutOffDt; }
            set { this._cutOffDt = value; }
        }

        [DataMember(Order=20)]
        public string ModifiedBy
        {
            get { return this._modifiedBy; }
            set { this._modifiedBy = value; }
        }


        [DataMember(Order=21)]
        public DateTime? ModifiedDt
        {
            get { return this._modifiedDt; }
            set { this._modifiedDt = value; }
        }
    }
}

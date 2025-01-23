using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities
{
    [DataContract]
    public class AgentARInfo
    {
        string _itemId;
        string _caId;                
        string _branchId;
        string _dtId;        
        string _invoiceNo;        
        string _taxCode;        
        decimal? _amount = 0;      
        decimal? _gAmount = 0;                
        decimal? _paidGAmount = 0;
        string _postBranchServerId;
        DateTime? _postDt;
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
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }
         

        [DataMember(Order=4)]
        public string DtId
        {
            get { return this._dtId; }
            set { this._dtId = value; }
        }
      

        [DataMember(Order=5)]
        public string InvoiceNo
        {
            get { return this._invoiceNo; }
            set { this._invoiceNo = value; }
        }
       

        [DataMember(Order=6)]
        public string TaxCode
        {
            get { return this._taxCode; }
            set { this._taxCode = value; }
        }
       

        [DataMember(Order=7)]
        public decimal? Amount
        {
            get { return this._amount; }
            set { this._amount = value; }
        }


        [DataMember(Order=8)]
        public decimal? PaidGAmount
        {
            get { return this._paidGAmount; }
            set { this._paidGAmount = value; }
        }

      

        [DataMember(Order=9)]
        public decimal? GAmount
        {
            get { return this._gAmount; }
            set { this._gAmount = value; }
        }
      

        [DataMember(Order=10)]
        public string PostBranchServerId
        {
            get { return this._postBranchServerId; }
            set { this._postBranchServerId = value; }
        }

        [DataMember(Order=11)]
        public DateTime? PostDt
        {
            get { return this._postDt; }
            set { this._postDt = value; }
        }


        [DataMember(Order=12)]
        public string ModifiedBy
        {
            get { return this._modifiedBy; }
            set { this._modifiedBy = value; }
        }


        [DataMember(Order=13)]
        public DateTime? ModifiedDt
        {
            get { return this._modifiedDt; }
            set { this._modifiedDt = value; }
        }
    }

}

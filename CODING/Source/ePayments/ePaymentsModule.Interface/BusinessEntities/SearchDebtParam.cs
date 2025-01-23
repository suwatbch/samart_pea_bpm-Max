using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities
{
    [DataContract]
    public class SearchDebtParam
    {        
        private string _caId = String.Empty;
        private string _invoiceNo = String.Empty;
        private string _period = String.Empty;
        private string _branchId = String.Empty;
        private decimal? _debtAmount = 0;        


        [DataMember(Order=1)]
        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }


        [DataMember(Order=2)]
        public string InvoiceNo
        {
            get { return this._invoiceNo; }
            set { this._invoiceNo = value; }
        }


        [DataMember(Order=3)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }


        [DataMember(Order=4)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }


        [DataMember(Order=5)]
        public decimal? DebtAmount
        {
            get { return this._debtAmount; }
            set { this._debtAmount = value; }
        }
       
        public bool IsValidParam
        {
            get 
            {
                if (CaId == String.Empty && InvoiceNo == String.Empty
                        && Period == String.Empty && BranchId == String.Empty && DebtAmount == 0)
                    return false;
                else
                    return true;
            }
        }
    }
}

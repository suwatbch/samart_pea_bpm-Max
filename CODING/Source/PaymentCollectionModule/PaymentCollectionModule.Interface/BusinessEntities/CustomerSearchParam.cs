using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class CustomerSearchParam: BaseParam
    {
        private string _caId;

        [DataMember(Order=1)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }

        private string _invoiceNo;

        [DataMember(Order=2)]
        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }

        private DateTime? _toPayDate;

        [DataMember(Order=3)]
        public DateTime? ToPayDate
        {
            get { return _toPayDate; }
            set { _toPayDate = value; }
        }

        private string _name;

        [DataMember(Order=4)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _address;

        [DataMember(Order=5)]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private string _regId;

        [DataMember(Order=6)]
        public string RegId
        {
            get { return _regId; }
            set { _regId = value; }
        }

        private string _BpCaId;
        /// <summary>
        /// ทุก CA ภายใต้ BP เดียวกันของ CA นี้
        /// </summary>

        [DataMember(Order=7)]
        public string BpCaId
        {
            get { return _BpCaId; }
            set { _BpCaId = value; }
        }

        private string _branch;

        [DataMember(Order=8)]
        public string Branch
        {
            get { return _branch; }
            set { _branch = value; }
        }

        private bool _isSearByBP = false;

        [DataMember(Order = 9)]
        public bool IsSearByBP
        {
            get { return _isSearByBP; }
            set { _isSearByBP = value; }
        }

        //private string _CaTaxId;

        //[DataMember(Order = 10)]
        //public string CaTaxId
        //{
        //    get { return _CaTaxId; }
        //    set { _CaTaxId = value; }
        //}

        //private string _CaTaxBranch;

        //[DataMember(Order = 11)]
        //public string CaTaxBranch
        //{
        //    get { return _CaTaxBranch; }
        //    set { _CaTaxBranch = value; }
        //}

        public CustomerSearchParam(){}

        public CustomerSearchParam(string customerId)
        {
            _caId = customerId;
        }
    }
}

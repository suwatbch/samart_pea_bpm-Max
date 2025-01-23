using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class GroupInvoiceSearchParam
    {
        private string _invoiceNo;

        [DataMember(Order=1)]
        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }

        private string _branchId;

        [DataMember(Order=2)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        private DateTime? _toPayDate;

        [DataMember(Order=3)]
        public DateTime? ToPayDate
        {
            get { return _toPayDate; }
            set { _toPayDate = value; }
        }

        public GroupInvoiceSearchParam(){}

        public GroupInvoiceSearchParam(string invoiceNo)
        {
            _invoiceNo = invoiceNo;
        }
    }
}

using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class GroupInvoiceParam
    {
        string _paymentDueDate;
        int? _printingCondition;
        List<String> _Id = new List<string>();

        string _branchId;
        string _mruId;
        string _toMruId;
        string _paidById;
        string _mtNo;
        string _commBranchId;
        string _printedBy;
        string _approverId;
        string _approverName;
        string _approverPosition;
        int? _billType;

        //new add for reprint 
        string _fromCaId;
        string _toCaId;


        [DataMember(Order=1)]
        public int? BillType
        {
            get { return _billType; }
            set { _billType = value; }
        }


        [DataMember(Order=2)]
        public string FromCaId
        {
            get { return _fromCaId; }
            set { _fromCaId = value; }
        }


        [DataMember(Order=3)]
        public string ToCaId
        {
            get { return _toCaId; }
            set { _toCaId = value; }
        }
        

        [DataMember(Order=4)]
        public string CommBranchId
        {
            get { return _commBranchId; }
            set { _commBranchId = value; }
        }


        [DataMember(Order=5)]
        public string ApproverName
        {
            get { return _approverName; }
            set { _approverName = value; }
        }


        [DataMember(Order=6)]
        public string ApproverPosition
        {
            get { return _approverPosition; }
            set { _approverPosition = value; }
        }


        [DataMember(Order=7)]
        public string PrintedBy
        {
            get { return _printedBy; }
            set { _printedBy = value; }
        }


        [DataMember(Order=8)]
        public string MtNo
        {
            get { return _mtNo; }
            set { _mtNo = value; }
        }


        [DataMember(Order=9)]
        public string PaidById
        {
            get { return _paidById; }
            set { _paidById = value; }
        }


        [DataMember(Order=10)]
        public string ToMruId
        {
            get { return _toMruId; }
            set { _toMruId = value; }
        }


        [DataMember(Order=11)]
        public string MruId
        {
            get { return _mruId; }
            set { _mruId = value; }
        }


        [DataMember(Order=12)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=13)]
        public string PaymentDueDate
        {
            get { return _paymentDueDate; }
            set { _paymentDueDate = value; }
        }


        [DataMember(Order=14)]
        public int? PrintingCondition
        {
            get { return _printingCondition; }
            set { _printingCondition = value; }
        }


        [DataMember(Order=15)]
        public List<String> Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        [DataMember(Order = 16)]
        public string ApproverId
        {
            get { return _approverId; }
            set { _approverId = value; }
        }
    }
}

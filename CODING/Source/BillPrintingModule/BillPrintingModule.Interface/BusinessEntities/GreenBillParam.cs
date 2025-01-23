using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class GreenBillParam
    {
        //used for checkExistGreenBill
        int? _billType;
        string _bankDueDate;
        string _bankId;
        string _billPred;
        string _commBranchId;
        string _printedBy;


        [DataMember(Order=1)]
        public string CommBranchId
        {
            get { return _commBranchId; }
            set { _commBranchId = value; }
        }


        [DataMember(Order=2)]
        public string PrintedBy
        {
            get { return _printedBy; }
            set { _printedBy = value; }
        }


        [DataMember(Order=3)]
        public int? BillType
        {
            get { return _billType; }
            set { _billType = value; }
        } 
        

        [DataMember(Order=4)]
        public string BankDueDate
        {
            get { return _bankDueDate; }
            set { _bankDueDate = value; }
        }


        [DataMember(Order=5)]
        public string BankId
        {
            get { return _bankId; }
            set { _bankId = value; }
        }


        [DataMember(Order=6)]
        public string BillPred
        {
            get { return _billPred; }
            set { _billPred = value; }
        }
    }
}

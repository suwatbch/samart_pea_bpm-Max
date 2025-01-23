using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ToolModule.Interface.BusinessEntities
{
    [DataContract]
    public class SyncCrossCheckInfo
    {
        private string _invId;
        private string _caId;
        private string _caName;
        private string _branchId;
        private string _branchName;
        private string _mruId;
        private string _amount;

        //bill only
        private string _billPred;
        private string _billType;

        //AR only
        private string _dtId;
        private string _dtName;


        [DataMember(Order=1)]
        public string InvId
        {
            set { _invId = value; }
            get { return _invId; }
        }


        [DataMember(Order=2)]
        public string CaId
        {
            set { _caId = value; }
            get { return _caId; }
        }


        [DataMember(Order=3)]
        public string CaName
        {
            set { _caName = value; }
            get { return _caName; }
        }


        [DataMember(Order=4)]
        public string BranchId
        {
            set { _branchId = value; }
            get { return _branchId; }
        }


        [DataMember(Order=5)]
        public string BranchName
        {
            set { _branchName = value; }
            get { return _branchName; }
        }


        [DataMember(Order=6)]
        public string MruId
        {
            set { _mruId = value; }
            get { return _mruId; }
        }


        [DataMember(Order=7)]
        public string BillPred
        {
            set { _billPred = value; }
            get { return _billPred; }
        }


        [DataMember(Order=8)]
        public string BillType
        {
            set { _billType = value; }
            get { return _billType; }
        }


        public string BillTypeDesc
        {
            get {
                if (_billType == "0")
                    return "A4";
                else if (_billType == "1")
                    return "ฟ้า";
                else if (_billType == "2")
                    return "เขียว";
                else if (_billType == "3")
                    return "ผสม";
                else 
                    return "ไม่ระบุ";
            }
        }   


        [DataMember(Order=10)]
        public string Amount
        {
            set { _amount = value; }
            get { return _amount; }
        }


    }
}

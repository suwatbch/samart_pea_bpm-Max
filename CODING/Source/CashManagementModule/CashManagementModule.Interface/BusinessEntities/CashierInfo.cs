using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CashierInfo
    {
        private string _cashierId;
        private string _cashierName;
        private string _cashierLongName;
        private string _status;
        private string _posId;
        private string _flag; //0 - sender , 1 - receiver 
        //special used
        private string _workId;
        private string _branchName2;
        private decimal? _totalAmt;
 

        [DataMember(Order=1)]
        public string CashierId
        {
            set { _cashierId = value; }
            get { return _cashierId; }
        }


        [DataMember(Order=2)]
        public string CashierName
        {
            set { _cashierName = value; }
            get { return _cashierName; }
        }


        [DataMember(Order=3)]
        public string LongName
        {
            set { _cashierLongName = value; }
            get { return _cashierLongName; }
        }


        [DataMember(Order=4)]
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }


        [DataMember(Order=5)]
        public string PosId
        {
            set { _posId = value; }
            get {
                if (_posId == null ||_posId.Trim() == string.Empty)
                    return "-";
                else return _posId;
            }
        }


        [DataMember(Order=6)]
        public string Flag
        {
            set { _flag = value; }
            get { return _flag; }
        }


        //Checked TongKung
        public string StatusTxt
        {
            get
            {
                if (_status == "1") return "à»Ô´¡Ð";
                else if (_status == "0") return "»Ô´¡Ð";
                else return "-";
            }
        }


        [DataMember(Order=8)]
        public string WorkId
        {
            set { _workId = value; }
            get { return _workId; }
        }


        [DataMember(Order=9)]
        public string BranchName2
        {
            set { _branchName2 = value; }
            get { return _branchName2; }
        }


        [DataMember(Order=10)]
        public decimal? TotalAmt
        {
            set { _totalAmt = value; }
            get { return _totalAmt; }
        }


        //Checked TongKung
        public string TotalAmtTxt
        {
            get
            {
                if (_totalAmt == null || _totalAmt == 0)
                    return "-";
                else
                    return _totalAmt.Value.ToString("#,###.00");
            }
        }


    }
}

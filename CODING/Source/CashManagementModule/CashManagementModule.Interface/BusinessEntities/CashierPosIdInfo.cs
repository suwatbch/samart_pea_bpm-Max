using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CashierPosIdInfo
    {
        private List<string> _posList = new List<string>();
        private List<CashierInfo> _cashierList = new List<CashierInfo>();


        [DataMember(Order=1)]
        public List<string> PosList
        {
            set { _posList = value; }
            get { return _posList; }
        }


        [DataMember(Order=2)]
        public List<CashierInfo> CashierList
        {
            set { _cashierList = value; }
            get { return _cashierList; }
        }
    }
}

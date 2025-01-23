using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class Portion
    {
        string _portionKey;
        string _portionNo;


        [DataMember(Order=1)]
        public string PortionKey
        {
            get { return _portionKey; }
            set { _portionKey = value; }
        }


        [DataMember(Order=2)]
        public string PortionNo
        {
            get { return _portionNo; }
            set { _portionNo = value; }
        }
    }
}

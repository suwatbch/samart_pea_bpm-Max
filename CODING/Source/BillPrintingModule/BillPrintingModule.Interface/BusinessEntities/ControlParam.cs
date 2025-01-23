using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class ControlParam
    {
        private List<Invoice> _caList = new List<Invoice>();
        private string _printDoc;
        private string _modifiedBy;


        [DataMember(Order=1)]
        public List<Invoice> CaList
        {
            get { return _caList; }
            set { _caList = value; }
        }


        [DataMember(Order=2)]
        public string PrintDoc
        {
            get { return _printDoc; }
            set { _printDoc = value; }
        }


        [DataMember(Order=3)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }
    }
}

using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ToolModule.Interface.BusinessEntities
{
    [DataContract]
    public class UnlockRemark
    {
        private string _fncId;

        [DataMember(Order=1)]
        public string FncId
        {
            get { return _fncId; }
            set { _fncId = value; }
        }

        private string _remarkId;

        [DataMember(Order=2)]
        public string RemarkId
        {
            get { return _remarkId; }
            set { _remarkId = value; }
        }

        private string _description;

        [DataMember(Order=3)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }


      
        
    }
}

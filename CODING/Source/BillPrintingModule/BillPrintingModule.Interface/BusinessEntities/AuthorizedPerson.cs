using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class AuthorizedPerson
    {
        string _approverId;
        string _approverName;
        string _position;
        string _modifiedBy;
        string _idPrefix;
        string _createBranch;


        [DataMember(Order=1)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }


        [DataMember(Order=2)]
        public string Position
        {
            get { return _position; }
            set { _position = value; }
        }


        [DataMember(Order=3)]
        public string ApproverName
        {
            get { return _approverName; }
            set { _approverName = value; }
        }


        [DataMember(Order=4)]
        public string ApproverId
        {
            get { return _approverId; }
            set { _approverId = value; }
        }


        [DataMember(Order=5)]
        public string IdPrefix
        {
            get { return _idPrefix; }
            set { _idPrefix = value; }
        }


        [DataMember(Order=6)]
        public string CreateBranchId
        {
            get { return _createBranch; }
            set { _createBranch = value; }
        }

    }
}

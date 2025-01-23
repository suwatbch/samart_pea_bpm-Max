using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ToolModule.Interface.BusinessEntities
{
    [DataContract]
    public class Role
    {
        private string _roleId;
        private string _roleName;
        private string _initModule;
        private string _description;
        private DateTime? _modifiedDt;
        private string _modifiedBy;
        private int? _userCount;

        private List<Function> _fncList = new List<Function>();


        [DataMember(Order=1)]
        public List<Function> FncList
        {
            get { return _fncList; }
            set { _fncList = value; }
        }


        [DataMember(Order=2)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }


        [DataMember(Order=3)]
        public string RoleId
        {
            get { return _roleId; }
            set { _roleId = value; }
        }


        [DataMember(Order=4)]
        public string RoleName
        {
            get { return _roleName; }
            set { _roleName = value; }
        }


        [DataMember(Order=5)]
        public string InitModule
        {
            get { return _initModule; }
            set { _initModule = value; }
        }


        [DataMember(Order=6)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }


        [DataMember(Order=7)]
        public DateTime? ModifiedDt
        {
            get { return _modifiedDt; }
            set { _modifiedDt = value; }
        }

        [DataMember(Order = 8)]
        public int? UserCount
        {
            get { return _userCount; }
            set { _userCount = value; }
        }


        //[DataMember(Order=8)]
        public string FullName
        {
            get { return string.Format("{0} - {1}", _roleId, _roleName); }
        }
    }
}

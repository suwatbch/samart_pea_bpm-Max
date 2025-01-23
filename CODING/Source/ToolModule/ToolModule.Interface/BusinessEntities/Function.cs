using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ToolModule.Interface.BusinessEntities
{
    [DataContract]
    public class Function
    {
        private string _functionId;
        private string _functionName;
        private string _subFunctionName;
        private string _moduleName;
        private DateTime? _modifiedDt;
        private bool _check;
        private bool _internal;

        private string _descripttion;

        //purpose: assign function to role
        private string _roleId;


        [DataMember(Order=1)]
        public string FunctionId
        {
            set { _functionId = value; }
            get { return _functionId; }
        }


        [DataMember(Order=2)]
        public string FunctionName
        {
            set { _functionName = value; }
            get { return _functionName; }
        }


        [DataMember(Order=3)]
        public string SubFunctionName
        {
            set { _subFunctionName = value; }
            get { return _subFunctionName; }
        }


        [DataMember(Order=4)]
        public string ModuleName
        {
            set { _moduleName = value; }
            get { return _moduleName; }
        }


        [DataMember(Order=5)]
        public DateTime? ModifiedDt
        {
            set { _modifiedDt = value; }
            get { return _modifiedDt; }
        }


        [DataMember(Order=6)]
        public bool Check
        {
            set { _check = value; }
            get { return _check; }
        }


        [DataMember(Order=7)]
        public string RoleId
        {
            set { _roleId = value; }
            get { return _roleId; }
        }


        [DataMember(Order=8)]
        public string Description
        {
            set { _descripttion = value; }
            get { return _descripttion; }
        }

        [DataMember(Order = 9)]
        public bool Internal
        {
            set { _internal = value; }
            get { return _internal; }
        }
        
    }
}

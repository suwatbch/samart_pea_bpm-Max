using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ToolModule.Interface.BusinessEntities
{
    [DataContract]
    public class Employee
    {
        private string _employeeId;
        private string _employeeName;
        private string _department;
        private string _userFlag;
        private string _branchId;


        [DataMember(Order=1)]
        public string EmployeeId
        {
            set { _employeeId = value; }
            get { return _employeeId;  }
        }


        [DataMember(Order=2)]
        public string EmployeeName
        {
            set { _employeeName = value; }
            get { return _employeeName; }
        }


        [DataMember(Order=3)]
        public string Department
        {
            set { _department = value; }
            get { return _department; }
        }


        [DataMember(Order=4)]
        public string UserFlag
        {
            set { _userFlag = value; }
            get { return _userFlag; }
        }


        [DataMember(Order=5)]
        public string BranchId
        {
            set { _branchId = value; }
            get { return _branchId; }
        }

    }
}

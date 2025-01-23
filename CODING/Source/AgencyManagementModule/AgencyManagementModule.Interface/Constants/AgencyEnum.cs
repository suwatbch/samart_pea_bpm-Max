using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.AgencyManagementModule.Interface.Constants
{
    [DataContract]
    public enum ContractTypeEnum : int
    {
        UNDEFINE = 0,
        CUSTOMER = 1,
        EMPLOYEE = 52,
        AGENCY = 51
    }

    [DataContract]
    public  class ARDeptType
    {

        public const string ADVPAID = "P00010001";
        public static string NETPAID = "P00010002";     
        public static string GROUPINVOICE = "P00020001";        
    }
    [DataContract]
    public class AbsIdEnum
    {
        public static string UNCOLLECTED = "C";
        public static string COLLECTED = "Y";
        public static string PAST = "I";
        public static string DOUBLE = "D";
        public static string UNDEFINE = "N";
    }
    [DataContract]
    public class PmIdEnum
    {
        public static string NONE = "0";
        public static string AGENCY = "B";
        public static string GROUPINVOICE = "G";
        public static string POS = "C";
        public static string DOUBLE = "Z";
    }
    [DataContract]
    public class BsIdEnum
    {
        public static string CUT = "T";
        public static string NOT_CUT = "N";
        public static string CANCEL = "C";
    }
    [DataContract]
    public class AboIdEnum
    {
        public static string FIRST = "N";
        public static string REPEAT = "R";
    }

    [DataContract]
    public enum ARPaymentTypeEnum : int
    {
        UNDEFINE = 0,
        CASH = 1,
        CHEQUE = 2,
        PAYSLIP = 3,
        AGENCY = 4
    }

    [DataContract]
    public enum BillOutStatusEnum
    {
        NEW,
        REPEAT
    }

    [DataContract]
    public enum BookTypeEnum : int
    {
        UNDEFINE = 0,
        BILLBOOK = 1,
        GROUP_INVOICE = 2
    }

    [DataContract]
    public enum PaidTypeEnum : int
    {
        UNDEFINE = 0,
        CASH = 1,
        CHEQUE = 2
    }

    [DataContract]
    public enum BookSearchStatusEnum : int
    {
        UNDEFINE = 0,
        ALL = 1,
        CUT = 2,
        NORMAL = 3,
        CANCEL = 4
    }

    [DataContract]
    public enum TravelHelpEnum : int  
    {
        UNDEFINE = 0,
        NORMALTRAVELHELP = 1,
        WATERTRAVELHELP = 2,
        FARLANDHELP = 3
    }

    [DataContract]
    public enum BookHolderState : int
    {
        Employee = 0,
        Agent = 1,
    };
}

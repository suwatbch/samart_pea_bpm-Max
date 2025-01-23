using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class ReportParam
    {
        /// <summary>
        /// Report condition    1 = ออกทั้งหมดของการไฟฟ้านั้น ในวันที่กำหนด
        ///                     2 = ออกโดยระบุ cashier
        ///                     3 = ออกโดยระบุ POS Id
        /// 
        /// DailyPayIn  1 = All
        ///             2 = Select list
        /// </summary>

        private string _reportCondition;
        private List<string> _inputList = new List<string>();
        private DateTime _fromDate;
        private DateTime _toDate;
        private string _cashierId;
        private string _posId;
        private string _branchId;
        private string _dateStr;
        private string _conditionDesc;

        /// <summary>
        /// 1 = รายงานบันทึกการตรวจนับเงินคงเหลือ (Ref. กง. 112 ร.48)
        /// 2 = รายงานการรับ – จ่ายเงิน (รายละเอียดเงินเข้า-ออกในลิ้นชัก) (Ref. cash_101) 
        /// 3 = รายงานรายละเอียดแนบการนำเงินฝากธนาคาร 
        /// 4 = the others
        /// </summary>
        private int _reportType;

        //special used
        private List<BaselineInfo> _baseList = new List<BaselineInfo>();
        private List<ReportAvailableInfo> _avList = new List<ReportAvailableInfo>();


        [DataMember(Order=1)]
        public List<BaselineInfo> Baseline
        {
            set { _baseList = value; }
            get { return _baseList; }
        }


        [DataMember(Order=2)]
        public List<ReportAvailableInfo> AvList
        {
            set { _avList = value; }
            get { return _avList; }
        }


        [DataMember(Order=3)]
        public int ReportType
        {
            set { _reportType = value; }
            get { return _reportType; }
        }


        [DataMember(Order=4)]
        public string DateString
        {
            set { _dateStr = value; }
            get { return _dateStr; }
        }


        [DataMember(Order=5)]
        public string ReportCondition
        {
            set { _reportCondition = value; }
            get { return _reportCondition; }
        }


        [DataMember(Order=6)]
        public List<string> InputList
        {
            set { _inputList = value; }
            get { return _inputList; }
        }


        [DataMember(Order=7)]
        public DateTime FromDate
        {
            set { _fromDate = value; }
            get { return _fromDate; }
        }


        [DataMember(Order=8)]
        public DateTime ToDate
        {
            set { _toDate = value; }
            get { return _toDate; }
        }


        [DataMember(Order=9)]
        public string CashierId
        {
            set { _cashierId = value; }
            get { return _cashierId; }
        }


        [DataMember(Order=10)]
        public string PosId
        {
            set { _posId = value; }
            get { return _posId; }
        }


        [DataMember(Order=11)]
        public string BranchId
        {
            set { _branchId = value; }
            get { return _branchId; }
        }


        [DataMember(Order=12)]
        public string ConditionDesc
        {
            set { _conditionDesc = value; }
            get { return _conditionDesc; }
        }


    }
}

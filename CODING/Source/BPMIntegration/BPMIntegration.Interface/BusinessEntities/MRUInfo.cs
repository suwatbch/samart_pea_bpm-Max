using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using System.Globalization;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;

using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class MRUInfo : IListUtility<MRUInfo>
    {
        private string _BranchId;
        private string _MruId;
        private string _MruName;
        private string _AdvPay;
        private string _PortionId;
        private string _PortionDesc;
        private string _PtcNo;
        private string _IntownFlag;
        private string _ReaderType;
        private string _TravelHelp;
        private DateTime? _HelpValidFrom;
        private DateTime? _HelpValidTo;
        private string _MeterReaderId;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _Action;

        [DataMember(Order = 1)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 2)]
        public string MruId
        {
            get { return _MruId; }
            set { _MruId = value; }
        }
        [DataMember(Order = 3)]
        public string MruName
        {
            get { return _MruName; }
            set { _MruName = value; }
        }
        [DataMember(Order = 4)]
        public string AdvPay
        {
            get { return _AdvPay; }
            set { _AdvPay = value; }
        }
        [DataMember(Order = 5)]
        public string PortionId
        {
            get { return _PortionId; }
            set { _PortionId = value; }
        }
        [DataMember(Order = 6)]
        public string PortionDesc
        {
            get { return _PortionDesc; }
            set { _PortionDesc = value; }
        }
        [DataMember(Order = 7)]
        public string PtcNo
        {
            get { return _PtcNo; }
            set { _PtcNo = value; }
        }
        [DataMember(Order = 8)]
        public string IntownFlag
        {
            get { return _IntownFlag; }
            set { _IntownFlag = value; }
        }
        [DataMember(Order = 9)]
        public string ReaderType
        {
            get { return _ReaderType; }
            set { _ReaderType = value; }
        }
        [DataMember(Order = 10)]
        public string TravelHelp
        {
            get { return _TravelHelp; }
            set { _TravelHelp = value; }
        }
        [DataMember(Order = 11)]
        public DateTime? HelpValidFrom
        {
            get { return _HelpValidFrom; }
            set { _HelpValidFrom = value; }
        }
        [DataMember(Order = 12)]
        public DateTime? HelpValidTo
        {
            get { return _HelpValidTo; }
            set { _HelpValidTo = value; }
        }
        [DataMember(Order = 13)]
        public string MeterReaderId
        {
            get { return _MeterReaderId; }
            set { _MeterReaderId = value; }
        }
        [DataMember(Order = 14)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 15)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 16)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 17)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 18)]
        string _action;
        public string Action
        {
            get { return this._action; }
            set { this._action = value; }
        }


        #region IListUtility<MRUInfo> Members

        public string ToStream()
        {
            //IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("th-TH");
            //string[] template = { MruId, BranchId, MruName, EmployeeId, Zone, AdvPay,PortionNo, PtcNo,
            //                ReaderType, MeterReadDay.Value.ToString(), DataTransferDay.Value.ToString(),
            //                BillPrintDay.Value.ToString(), BillOutDay.Value.ToString(),
            //                MeterReaderId, BillControllerId, Action };
            //return string.Join("\t", template);
            throw new Exception("The Method is not implement yet");
        }

        public MRUInfo ParseExtract(string txt)
        {
            IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("th-TH");
            MRUInfo it = new MRUInfo();
            string[] sp = txt.Split('|');

            int colFixed = InterfaceColumns.MRU;
            int colCounted = sp.Length - 1;
            if (colCounted != colFixed)
                throw new Exception("ไม่สามารถทำรายการได้ เนื่องจาก Columns ใน text file มีจำนวน " + colCounted.ToString() + " แต่ Columns ที่กำหนดไว้มีจำนวน " + colFixed.ToString());


            int i = 1;
            string tmp = sp[i++].Trim();            
            it.MruId = StringConvert.ToString(tmp.Substring(tmp.Length-4,4));            
            it.BranchId = StringConvert.ToString(sp[i++].Trim());
            it.MruName = StringConvert.ToString(sp[i++].Trim());
            //it.EmployeeId = sp[i++].Trim();
            //it.Zone = sp[i++].Trim();
            //it.AdvPay = sp[i++].Trim();
            it.PortionId = StringConvert.ToString(sp[i++].Trim());
            it.PortionDesc = StringConvert.ToString(sp[i++].Trim());
            it.PtcNo = StringConvert.ToString(sp[i++].Trim());
            it.ReaderType = StringConvert.ToString(sp[i++].Trim());
            //it.MeterReadDay = Convert.ToInt32(sp[i++].Trim());
            //it.DataTransferDay = Convert.ToInt32(sp[i++].Trim());
            //it.BillPrintDay = Convert.ToInt32(sp[i++].Trim());
            //it.BillOutDay = Convert.ToInt32(sp[i++].Trim());
            //it.BillControllerId = sp[i++].Trim();
            it.IntownFlag = StringConvert.ToString(sp[i++]);  
            it.Action = sp[i++].Trim();

            if (it.Action != "O" && it.Action != "1" && it.Action != "2" && it.Action != "3")
                throw new Exception("ไม่สามารถทำรายการได้ เนื่องจากข้อมูลของ [Action] มีค่าเท่ากับ " + it.Action + "] ซึ่งไม่ตรงตามรูปแบบที่กำหนดไว้");

            it.ModifiedBy = "BATCH";
            it.ModifiedDt = DateTime.Now;
            it.SyncFlag = "1";
            it.Active = true;
            return it;
        }

        #endregion
    }
}

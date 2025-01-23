using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class MeterSizeInfo : IListUtility<MeterSizeInfo>
    {
        private string _MeterSizeId;
        private string _MeterSizeName;
        private decimal? _ReConnectMeterRate;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string MeterSizeId
        {
            get { return _MeterSizeId; }
            set { _MeterSizeId = value; }
        }
        [DataMember(Order = 2)]
        public string MeterSizeName
        {
            get { return _MeterSizeName; }
            set { _MeterSizeName = value; }
        }
        [DataMember(Order = 3)]
        public decimal? ReConnectMeterRate
        {
            get { return _ReConnectMeterRate; }
            set { _ReConnectMeterRate = value; }
        }
        [DataMember(Order = 4)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 5)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 6)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 7)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }

        [DataMember(Order = 8)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }


        #region IListUtility<MeterSizeInfo> Members

        public string ToStream()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public MeterSizeInfo ParseExtract(string txt)
        {
            MeterSizeInfo it = new MeterSizeInfo();
            string[] sp = txt.Split('|');

            int colFixed = InterfaceColumns.MeterSizeType;
            int colCounted = sp.Length - 1;
            if (colCounted != colFixed)
                throw new Exception("ไม่สามารถทำรายการได้ เนื่องจาก Columns ใน text file มีจำนวน " + colCounted.ToString() + " แต่ Columns ที่กำหนดไว้มีจำนวน " + colFixed.ToString());


            int i = 1;
            it.MeterSizeId = StringConvert.ToString(sp[i++].Trim());
            it.MeterSizeName = StringConvert.ToString(sp[i++].Trim());
            string meterRate = sp[i++].Trim();
            //it.ReConnectMeterRate = Convert.ToDecimal(sp[i++].Trim());
            it.Action = sp[i++].Trim();

            //cancel is no need the rate
            if(it.Action != "3")
                it.ReConnectMeterRate = Convert.ToDecimal(meterRate);

            if (it.Action != "O" && it.Action != "1" && it.Action != "2" && it.Action != "3")
                throw new Exception("ไม่สามารถทำรายการได้ เนื่องจากข้อมูลของ [Action] มีค่าเท่ากับ " + it.Action + "] ซึ่งไม่ตรงตามรูปแบบที่กำหนดไว้");

            it.SyncFlag = "1";
            it.ModifiedBy = "BATCH";
            it.ModifiedDt = DateTime.Now;
            it.Active = true;
            return it;
        }

        #endregion
    }
}

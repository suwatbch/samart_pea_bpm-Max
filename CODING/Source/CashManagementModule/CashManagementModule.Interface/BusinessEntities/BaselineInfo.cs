using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BaselineInfo
    {
        private int? _baselineId;
        private string _workId;
        private bool _aboveTimeline;
        private DateTime? _baselineDt;

        public BaselineInfo Clone()
        {
            BaselineInfo that = new BaselineInfo();
            that.BaselineId = this.BaselineId;
            that.WorkId = this.WorkId;
            that.AboveTimeline = this.AboveTimeline;
            that.BaselineDt = this.BaselineDt;
            return that;
        }


        [DataMember(Order=1)]
        public string WorkId
        {
            get { return _workId; }
            set { _workId = value; }
        }


        [DataMember(Order=2)]
        public int? BaselineId
        {
            get { return _baselineId; }
            set { _baselineId = value; }
        }


        [DataMember(Order=3)]
        public bool AboveTimeline
        {
            get { return _aboveTimeline; }
            set { _aboveTimeline = value; }
        }


        [DataMember(Order=4)]
        public DateTime? BaselineDt
        {
            get { return _baselineDt; }
            set { _baselineDt = value; }
        }

        //Checked TongKung
        //[DataMember(Order=5)]
        public string BaselineTime
        {
            get { return _baselineDt.Value.ToString("HH:mm:ss", new CultureInfo("th-TH")); }
        }

    }
}

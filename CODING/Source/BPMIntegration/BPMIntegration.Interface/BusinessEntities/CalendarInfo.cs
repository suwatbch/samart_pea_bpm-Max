using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using PEA.BPM.Architecture.CommonUtilities;

using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class CalendarInfo : IListUtility<CalendarInfo>
    {
        private string _CdType;
        private string _CdName;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        string _action;

        [DataMember(Order = 1)]
        public string CdType
        {
            get { return _CdType; }
            set { _CdType = value; }
        }
        [DataMember(Order = 2)]
        public string CdName
        {
            get { return _CdName; }
            set { _CdName = value; }
        }
        [DataMember(Order = 3)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 4)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 5)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 6)]
        public bool Active
        {
            get { return this._Active; }
            set { this._Active = value; }
        }
        [DataMember(Order = 7)]
        public string Action
        {
            get { return this._action; }
            set { this._action = value; }
        }

        #region IListUtility<CalendarInfo> Members

        public string ToStream()
        {
            string[] template = { CdType, CdName, Action };
            return string.Join("\t", template);
        }

        public CalendarInfo ParseExtract(string txt)
        {
            CalendarInfo it = new CalendarInfo();
            string[] sp = txt.Split('|');

            it.CdType = StringConvert.ToString(sp[0].Trim());
            it.CdName = StringConvert.ToString(sp[1].Trim());
            it.Action = sp[2].Trim();
            it.SyncFlag = "1";
            it.ModifiedBy = "BATCH";
            it.ModifiedDt = DateTime.Now;
            it.Active = true;
            return it;
        }

        #endregion
    }
}

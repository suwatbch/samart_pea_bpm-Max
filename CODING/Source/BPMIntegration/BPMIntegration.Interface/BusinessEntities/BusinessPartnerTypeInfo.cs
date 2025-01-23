using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using System.Globalization;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class BusinessPartnerTypeInfo : IListUtility<BusinessPartnerTypeInfo>
    {
        private string _BpTypeId;
        private string _BpTypeDesc;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string BpTypeId
        {
            get { return _BpTypeId; }
            set { _BpTypeId = value; }
        }
        [DataMember(Order = 2)]
        public string BpTypeDesc
        {
            get { return _BpTypeDesc; }
            set { _BpTypeDesc = value; }
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
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 7)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }


        #region IListUtility<BusinessPartnerTypeInfo> Members

        public string ToStream()
        {
            IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("th-TH");
            string[] template = { BpTypeId, BpTypeDesc, Action };
            return string.Join("\t", template);
        }

        public BusinessPartnerTypeInfo ParseExtract(string txt)
        {
            IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("th-TH");
            BusinessPartnerTypeInfo it = new BusinessPartnerTypeInfo();
            string[] sp = txt.Split('|');

            it.BpTypeId = sp[0].Trim();
            it.BpTypeDesc = sp[1].Trim();
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

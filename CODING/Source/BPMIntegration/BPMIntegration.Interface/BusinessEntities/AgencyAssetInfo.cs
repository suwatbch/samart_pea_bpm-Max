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
    public class AgencyAssetInfo : IListUtility<AgencyAssetInfo>
    {
        private string _AssetId;
        private string _CaId;
        private string _AssetType;
        private string _AssetTypeDesc;
        private decimal? _AssetValue;
        private string _Status;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string AssetId
        {
            get { return _AssetId; }
            set { _AssetId = value; }
        }
        [DataMember(Order = 2)]
        public string CaId
        {
            get { return _CaId; }
            set { _CaId = value; }
        }
        [DataMember(Order = 3)]
        public string AssetType
        {
            get { return _AssetType; }
            set { _AssetType = value; }
        }
        [DataMember(Order = 4)]
        public string AssetTypeDesc
        {
            get { return _AssetTypeDesc; }
            set { _AssetTypeDesc = value; }
        }
        [DataMember(Order = 5)]
        public decimal? AssetValue
        {
            get { return _AssetValue; }
            set { _AssetValue = value; }
        }
        [DataMember(Order = 6)]
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        [DataMember(Order = 7)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 8)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 9)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 10)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 11)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

        #region IListUtility<AgencyAssetInfo> Members

        public string ToStream()
        {
            //string[] template = { AssetId, CaId, AssetType, AssetTypeDesc, AssetPercent.ToString(),
            //                      AssetValue.ToString(), AssetLimit.ToString(), Action };
            //return string.Join("\t", template);
            throw new Exception("The Method is not implement yet");
        }

        public AgencyAssetInfo ParseExtract(string txt)
        {
            AgencyAssetInfo it = new AgencyAssetInfo();
            string[] sp = txt.Split('|');

            int colFixed = InterfaceColumns.AgencyAsset;
            int colCounted = sp.Length - 1;
            if (colCounted != colFixed)
                throw new Exception("ไม่สามารถทำรายการได้ เนื่องจาก Columns ใน text file มีจำนวน " + colCounted.ToString() + " แต่ Columns ที่กำหนดไว้มีจำนวน " + colFixed.ToString());


            int i = 1;
            it.AssetId = StringConvert.ToString(sp[i++].Trim());
            it.CaId = StringConvert.ToString(sp[i++].Trim());
            it.AssetType = StringConvert.ToString(sp[i++].Trim());
            it.AssetTypeDesc = StringConvert.ToString(sp[i++].Trim());
            it.AssetValue = StringConvert.ToDecimal(sp[i++].Trim());
            it.Status = StringConvert.ToString(sp[i++].Trim());
            it.Action = sp[i++].Trim();

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

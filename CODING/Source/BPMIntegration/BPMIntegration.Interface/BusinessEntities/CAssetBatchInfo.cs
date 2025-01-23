using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    public class CAssetBatchInfo : IListUtility<CAssetBatchInfo>
    {
        private string _assetId;
        private string _agentId;
        private string _type;
        private string _typeDesc;
        private decimal? _percent;
        private decimal? _value;
        private decimal? _limit;

        //file format
        private char _dm = ','; //default delimeter
        private char[] spliters = { ',', ';', ' '};  //comma , colon, semi-colon, one-tab
               

        public char DefaultDelimeter
        {
            set { _dm = value; }
            get { return _dm; }
        }

        public string Id
        {
            set { _assetId = value; }
            get { return _assetId; }
        }

        public string AgentId
        {
            set { _agentId = value; }
            get { return _agentId; }
        }

        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }

        public string TypeDesc
        {
            set { _typeDesc = value; }
            get { return _typeDesc; }
        }

        public decimal? Percent
        {
            set { _percent = value; }
            get { return _percent; }
        }

        public decimal? AssetValue
        {
            set { _value = value; }
            get { return _value; }
        }

        public decimal? Limit
        {
            set { _limit = value; }
            get { return _limit; }
        }


        #region IListUtility<CAssetBatchInfo> Members

        public string ToStream()
        {
            return _assetId + _dm + _agentId + _dm + _type + _dm + _typeDesc + _dm + _percent.Value.ToString() + _dm +
                _value.Value.ToString() + _dm + _limit.Value.ToString();
        }

        public CAssetBatchInfo ParseExtract(string txt)
        {
            CAssetBatchInfo asset = new CAssetBatchInfo();
            string[] sp = txt.Split(spliters);
            asset.Id = sp[0].Trim();
            asset.AgentId = sp[1].Trim();
            asset.Type = sp[2].Trim();
            asset.TypeDesc = sp[3].Trim();
            asset.Percent = Convert.ToDecimal(sp[4].Trim());
            asset.AssetValue = Convert.ToDecimal(sp[5].Trim());
            asset.Limit = Convert.ToDecimal(sp[6].Trim());
            return asset;
        }

        #endregion
    }
}

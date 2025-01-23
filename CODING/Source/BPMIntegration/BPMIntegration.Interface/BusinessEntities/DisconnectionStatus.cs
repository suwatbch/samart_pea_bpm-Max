using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;
using PEA.BPM.Architecture.CommonUtilities;
using System.Globalization;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class DisconnectionStatus : IListUtility<DisconnectionStatus>
    {
        private string _CaId;
        private string _DiscNo;
        private string _DiscStatusId;
        private string _FixValue;
        private DateTime _PostDt;
        private string _PostBy;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private string _FileName;
        private bool _Active;


        [DataMember(Order = 1)]
        public string CaId
        {
            get { return _CaId; }
            set { _CaId = value; }
        }
        [DataMember(Order = 2)]
        public string DiscNo
        {
            get { return _DiscNo; }
            set { _DiscNo = value; }
        }
        [DataMember(Order = 3)]
        public string DiscStatusId
        {
            get { return _DiscStatusId; }
            set { _DiscStatusId = value; }
        }
        [DataMember(Order = 4)]
        public string FixValue
        {
            get { return _FixValue; }
            set { _FixValue = value; }
        
        }
        [DataMember(Order = 5)]
        public DateTime PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 6)]
        public string PostBy
        {
            get { return _PostBy; }
            set { _PostBy = value; }
        }
        [DataMember(Order = 7)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 8)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }

        [DataMember(Order = 9)]
        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }
        
        [DataMember(Order = 10)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }

        #region IListUtility<DisconnectionStatus> Members

        public string ToStream()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public DisconnectionStatus ParseExtract(string txt)
        {
            IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("en-EN");
            DisconnectionStatus obj = new DisconnectionStatus();
            string[] sp = txt.Split('|');

            int colFixed = InterfaceColumns.DisconnectionStatus;
            int colCounted = sp.Length;
            if (colCounted != colFixed)
                throw new Exception("ไม่สามารถทำรายการได้ เนื่องจาก Columns ใน text file มีจำนวน " + colCounted.ToString() + " แต่ Columns ที่กำหนดไว้มีจำนวน " + colFixed.ToString());

            int i = 1;
            obj.DiscNo = StringConvert.ToString(sp[i++].Trim());
            obj.CaId = StringConvert.ToString(sp[i++].Trim());
            obj.DiscStatusId = StringConvert.ToString(sp[i++].Trim());
            obj.FixValue = StringConvert.ToString(sp[i++].Trim());
        

            return obj;
        }

        #endregion
    }
}

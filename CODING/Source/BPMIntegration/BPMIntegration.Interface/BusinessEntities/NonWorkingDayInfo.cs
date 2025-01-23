using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using System.Globalization;
using System.Collections;
using PEA.BPM.Architecture.CommonUtilities;
using System.IO;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;

using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class NonWorkingDayInfo : IListUtility<NonWorkingDayInfo>
    {
        private string _NwId;
        private string _CdType;
        private DateTime? _NwDay;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string NwId
        {
            get { return _NwId; }
            set { _NwId = value; }
        }
        [DataMember(Order = 2)]
        public string CdType
        {
            get { return _CdType; }
            set { _CdType = value; }
        }
        [DataMember(Order = 3)]
        public DateTime? NwDay
        {
            get { return _NwDay; }
            set { _NwDay = value; }
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
            get { return this._action; }
            set { this._action = value; }
        }

        List<NonWorkingDayForYear> _listNw;
        public List<NonWorkingDayForYear> ListNw
        {
            get { return _listNw; }
            set { _listNw = value; }
        }

        #region IListUtility<NonWorkingDayInfo> Members

        public string ToStream()
        {
            //IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("th-TH");
            //string[] template = { NwId, CdType, NwDay.Value.ToString("ddMMyyyy", formatDate), Note, Action };
            //return string.Join("\t", template);
            throw new Exception("The Method is not implemented yet");
        }

        public NonWorkingDayInfo ParseExtract(string txt)
        {
            NonWorkingDayInfo it = new NonWorkingDayInfo();
            string[] sp = txt.Split('|');

            int colFixed = InterfaceColumns.NonworkingDays;
            int colCounted = sp.Length - 1;
            if (colCounted != colFixed)
                throw new Exception("ไม่สามารถทำรายการได้ เนื่องจาก Columns ใน text file มีจำนวน " + colCounted.ToString() + " แต่ Columns ที่กำหนดไว้มีจำนวน " + colFixed.ToString());


            string days = "";
            int i = 1;                        

            it.CdType = StringConvert.ToString(sp[i++]);
            string year = StringConvert.ToString(sp[i++]);

            for (int j = 0; j < 12; j++)
            {
                if (j != 11)
                    days += sp[i++].Trim() + "|";
                else
                    days += sp[i++].Trim();
            }
            ///SAP send a chunk of 11101111101..., we must extract them into dateTime variable
            it.ListNw = ExtractDate(days, it.CdType, year);

            //throw new Exception("Date"+it.ListNw[0].NwDate.ToString());

            i++;
            it.Action = StringConvert.ToString(sp[i++]);

            if (it.Action != "O" && it.Action != "1" && it.Action != "2" && it.Action != "3")
                throw new Exception("ไม่สามารถทำรายการได้ เนื่องจากข้อมูลของ [Action] มีค่าเท่ากับ " + it.Action + "] ซึ่งไม่ตรงตามรูปแบบที่กำหนดไว้");

            it.SyncFlag = "1";
            it.ModifiedBy = "BATCH";
            it.ModifiedDt = DateTime.Now;
            it.Active = true;
            return it;
        }

        private List<NonWorkingDayForYear> ExtractDate(string txt, string cdType, string year)
        {
            //split each month to array
            string[] dm = txt.Split('|');

            //create list to store entity
            List<NonWorkingDayForYear> nws = new List<NonWorkingDayForYear>();

            for (int i = 0; i < dm.Length; i++)
            {
                for (int j = 0; j < dm[i].Length; j++)
                {
                    if (dm[i].Substring(j, 1) == "0")
                    {
                        //generate dt
                        DateTime? dt = new DateTime(Convert.ToInt16(year),Convert.ToInt16((i + 1).ToString().PadLeft(2, '0')),Convert.ToInt16((j + 1).ToString().PadLeft(2, '0')));
                        //generate key where PK = cdType + yyyy + mm + dd;                                                
                        string PK = cdType + year + (i + 1).ToString().PadLeft(2, '0') + (j + 1).ToString().PadLeft(2, '0');

                        if (dt == null)
                            throw new Exception("Error in converting date function (ExtractDate)");

                        //pack into entity
                        NonWorkingDayForYear nw = new NonWorkingDayForYear();
                        nw.NwKey = PK;
                        nw.NwDate = dt;

                        //pack into list<>
                        nws.Add(nw);
                    }
                }
            }

            return nws;
        }

        #endregion

        #region Class for Extracting Method

        public class NonWorkingDayForYear
        {
            string _nwKey;
            public string NwKey
            {
                get { return _nwKey; }
                set { _nwKey = value; }
            }

            DateTime? _nwDate;
            public DateTime? NwDate
            {
                get { return _nwDate; }
                set { _nwDate = value; }
            }
        }

        #endregion
    }
}

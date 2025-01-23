using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class AgencyBillCollectionMasterReport
    {
        int _quarterNo;
        string _printDate;
        string _periodName;
        string _peaCode;
        string _peaName;        
        List <AgencyBillCollectionDetailReport> _billCollectionDetail= new List<AgencyBillCollectionDetailReport>();


        [DataMember(Order=1)]
        public string PrintDate
        {
            get { return this._printDate; }
            set { this._printDate = value; }
        }


        [DataMember(Order=2)]
        public int QuarterNo
        {
            get { return this._quarterNo; }
            set { this._quarterNo = value; }
        }

        [DataMember(Order=3)]
        public string PeriodName
        {
            get { return this._periodName; }
            set { this._periodName = value; }
        }

        [DataMember(Order=4)]
        public string PEACode
        {
            get { return this._peaCode ; }
            set { this._peaCode = value; }
        }

        [DataMember(Order=5)]
        public string PEAName
        {
            get { return this._peaName; }
            set { this._peaName = value; }
        }        

        [DataMember(Order=6)]
        public List<AgencyBillCollectionDetailReport> BillCollectionDetail
        {
            get { return this._billCollectionDetail; }
            set { this._billCollectionDetail = value; }
        }
    }
}

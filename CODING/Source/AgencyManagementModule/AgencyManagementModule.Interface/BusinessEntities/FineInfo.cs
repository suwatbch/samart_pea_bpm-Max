using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class FineInfo
    {
        private decimal? _amount=0;
        private FineDetailInfo _fineDetail;
        private List<FineBookInfo> _fineList = new List<FineBookInfo>();
        private bool _enabled = true;


        [DataMember(Order=1)]
        public List<FineBookInfo> FineList
        {
            set { _fineList = value; }
            get { return _fineList; }
        }


        [DataMember(Order=2)]
        public decimal? Amount
        {
            set { _amount = value; }
            get { return _amount; }
        }


        [DataMember(Order=3)]
        public FineDetailInfo FineDetail
        {
            set { _fineDetail = value; }
            get { return _fineDetail; }
        }


        [DataMember(Order=4)]
        public bool Enabled
        {
            set { _enabled = value; }
            get { return _enabled; }
        }



    }
}

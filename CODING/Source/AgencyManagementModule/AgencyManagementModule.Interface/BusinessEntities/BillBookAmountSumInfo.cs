using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillBookAmountSumInfo
    {
        private int _canCollectBillCount;
        private int _cannotCollectBillCount;
        private double _canCollectBillAmount;
        private double _cannotCollectBillAmount;


        [DataMember(Order=1)]
        public int CanCollectBillCount
        {
            get { return _canCollectBillCount; }
            set { _canCollectBillCount = value; }
        }


        [DataMember(Order=2)]
        public int ConnotCollectBillCount
        {
            get { return _cannotCollectBillCount; }
            set { _cannotCollectBillCount = value; }
        }


        [DataMember(Order=3)]
        public double CanCollectBillAmount
        {
            get { return _canCollectBillAmount; }
            set { _canCollectBillAmount = value; }
        }


        [DataMember(Order=4)]
        public double CannotCollectBillAmount
        {
            get { return _cannotCollectBillAmount; }
            set { _cannotCollectBillAmount = value; }
        }
    }
}

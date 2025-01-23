using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class APInfo
    {
        private string _paymentVoucher;

        [DataMember(Order=1)]
        public string PaymentVoucher
        {
            get { return _paymentVoucher; }
            set { _paymentVoucher = value; }
        }

        private string _caId;

        [DataMember(Order=2)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }

        //Checked TongKung
        //[DataMember(Order = 3)]
        public string DisplayCaId
        {
            get
            {
                return (_caId == null) ? "" : _caId.TrimStart('0');
            }
        }        

        private string _caName;

        [DataMember(Order=4)]
        public string CaName
        {
            get { return _caName; }
            set { _caName = value; }
        }

        private decimal? _gAmount;

        [DataMember(Order=5)]
        public decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }

        private decimal? _adjAmount;

        [DataMember(Order=6)]
        public decimal? AdjAmount
        {
            get { return _adjAmount; }
            set { _adjAmount = value; }
        }
    }
}

using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillSearchDetail
    {
        private string _customerId;

        [DataMember(Order=1)]
        public string CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        //Checked TongKung
        //[DataMember(Order=2)]
        public string DisplayCustomerId
        {
            get { return _customerId.TrimStart('0'); }
        }

        private string _name;

        [DataMember(Order=3)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _address;

        [DataMember(Order=4)]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private decimal? _toPayAmount;

        [DataMember(Order=5)]
        public decimal? ToPayAmount
        {
            get { return _toPayAmount; }
            set { _toPayAmount = value; }
        }

        private NetworkMode _networkMode = NetworkMode.LocalDatabaseServer;

        [DataMember(Order=6)]
        public NetworkMode NetworkMode
        {
            get { return _networkMode; }
            set { _networkMode = value; }
        }
    }
}

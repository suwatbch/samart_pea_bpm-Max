using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Infrastructure.Interface.BusinessEntities
{
    [DataContract]
    public class MeterSize
    {
        private string _meterSizeId;
        private string _meterSizeName;
        private decimal? _reconnectMeterRate;

        public MeterSize(string meterSizeId, string meterSizeName, decimal? reConnectMeterRate)
        {
            _meterSizeId = meterSizeId;
            _meterSizeName = meterSizeName;
            _reconnectMeterRate = reConnectMeterRate;
        }


        [DataMember(Order=1)]
        public string MeterSizeId
        {
            get { return _meterSizeId; }
            set { _meterSizeId = value; }
        }


        [DataMember(Order=2)]
        public string MeterSizeName
        {
            get { return _meterSizeName; }
            set { _meterSizeName = value; }
        }


        [DataMember(Order=3)]
        public decimal? ReconnectMeterRate
        {
            get { return _reconnectMeterRate; }
            set { _reconnectMeterRate = value; }
        }
    }
}

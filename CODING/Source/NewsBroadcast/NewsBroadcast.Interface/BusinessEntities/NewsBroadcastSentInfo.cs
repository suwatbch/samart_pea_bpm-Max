using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.NewsBroadcast.Interface.BusinessEntities
{

    [DataContract]
    public class NewsBroadcastSentInfo
    {
        private int broadcastId;
        private string broadcastTopic;
        private string detail;
        private DateTime sentDt;
        private DateTime expireDt;


        [DataMember(Order=1)]
        public int BroadcastId
        {
            get { return broadcastId; }
            set { broadcastId = value; }
        }

        [DataMember(Order=2)]
        public string BroadcastTopic
        {
            get { return broadcastTopic; }
            set { broadcastTopic = value; }
        }

        [DataMember(Order=3)]
        public string Detail
        {
            get { return detail; }
            set { detail = value; }
        }

        [DataMember(Order=4)]
        public DateTime SentDt
        {
            get { return sentDt; }
            set { sentDt = value; }
        }

        [DataMember(Order=5)]
        public DateTime ExpireDt
        {
            get { return expireDt; }
            set { expireDt = value; }
        }
    }
}

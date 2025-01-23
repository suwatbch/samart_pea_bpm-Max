using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.NewsBroadcast.Interface.BusinessEntities
{

    [DataContract]
    public class NewsBroadcastInfo
    {
        private int broadcastId;
        private string broadcastTopic;
        private string detail;
        private DateTime sentDt;
        private DateTime expireDt;
        private int cmdId;
        private bool isRead;
        private bool isOpened;
        private string readSymbol;


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

        [DataMember(Order=6)]
        public int CmdId
        {
            get { return cmdId; }
            set { cmdId = value; }
        }

        [DataMember(Order=7)]
        public bool IsRead
        {
            get { return isRead; }
            set { isRead = value; }
        }

        [DataMember(Order=8)]
        public bool IsOpened
        {
            get { return isOpened; }
            set { isOpened = value; }
        }

        [DataMember(Order=9)]
        public string ReadSymbol
        {
            get { return readSymbol; }
            set { readSymbol = value; }
        }
    }
}

using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.NewsBroadcast.Interface.BusinessEntities
{
    [DataContract]
    public class AreaInfo
    {
        private string areaId;
        private string areaName;


        [DataMember(Order=1)]
        public string AreaId
        {
            get { return areaId; }
            set { areaId = value; }
        }


        [DataMember(Order=2)]
        public string AreaName
        {
            get { return areaName; }
            set { areaName = value; }
        }
     
    }
}

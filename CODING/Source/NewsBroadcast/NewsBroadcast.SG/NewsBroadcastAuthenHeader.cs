using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;
using WCFExtras.Soap;


namespace PEA.BPM.NewsBroadcast.SG.BroadcastWCF 
{
    public partial class BroadcastWCFServiceClient
    {
        public AuthInfo AuthInfoValue
        {
            get
            {
                return InnerChannel.GetHeader<AuthInfo>("AuthInfoValue");
            }
            set
            {
                InnerChannel.SetHeader("AuthInfoValue", value);
            }
        }
    }
}














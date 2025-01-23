using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;
using WCFExtras.Soap;


namespace PEA.BPM.CashManagementModule.SG.CashierWCF
{
    public partial class CashierWCFServiceClient
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













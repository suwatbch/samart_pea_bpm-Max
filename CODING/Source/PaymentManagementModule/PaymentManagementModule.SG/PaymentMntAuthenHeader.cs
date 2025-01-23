using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;
using WCFExtras.Soap;


namespace PEA.BPM.PaymentManagementModule.SG.PaymentMntWCF
{
    public partial class PaymentMntWCFServiceClient
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

namespace PEA.BPM.PaymentManagementModule.SG.APReportWCF
{
    public partial class APReportWCFServiceClient
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













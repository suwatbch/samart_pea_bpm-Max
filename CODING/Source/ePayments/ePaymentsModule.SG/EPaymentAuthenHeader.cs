using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;
using WCFExtras.Soap;


namespace PEA.BPM.ePaymentsModule.SG.EPayBillingWCF
{
    public partial class EPayBillingWCFServiceClient
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

namespace PEA.BPM.ePaymentsModule.SG.EPayCommonWCF
{
    public partial class EPayCommonWCFServiceClient
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

namespace PEA.BPM.ePaymentsModule.SG.ReceiptPrintingWCF
{
    public partial class ReceiptPrintingWCFServiceClient
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


namespace PEA.BPM.ePaymentsModule.SG.EPayReportWCF
{
    public partial class EPayReportWCFServiceClient
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












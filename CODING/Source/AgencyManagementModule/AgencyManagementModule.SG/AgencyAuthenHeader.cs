using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;
using WCFExtras.Soap;

namespace PEA.BPM.AgencyManagementModule.SG.AgencyCommonWCF
{
    public partial class AgencyCommonWCFServiceClient
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

namespace PEA.BPM.AgencyManagementModule.SG.AgencyConfigWCF 
{
    public partial class AgencyConfigWCFServiceClient
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

namespace PEA.BPM.AgencyManagementModule.SG.AgencyPlanningWCF
{
    public partial class AgencyPlanningWCFServiceClient
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

namespace PEA.BPM.AgencyManagementModule.SG.BillBookCheckInWCF
{
    public partial class BillbookCheckInWCFServiceClient
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

namespace PEA.BPM.AgencyManagementModule.SG.CommissionMgtWCF 
{
    public partial class CommissionMgtWCFServiceClient
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

namespace PEA.BPM.AgencyManagementModule.SG.CreateBillbookWCF
{
    public partial class CreateBillbookWCFServiceClient
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

namespace PEA.BPM.AgencyManagementModule.SG.ReportMgtWCF
{
    public partial class ReportMgtWCFServiceClient
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







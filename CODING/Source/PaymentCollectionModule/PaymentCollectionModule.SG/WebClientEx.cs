using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using PEA.BPM.Architecture.ArchitectureTool;

namespace PaymentCollectionModule.SG
{
    public class WebClientEx : WebClient
    {
        public int Timeout { get; set; }


        protected override WebRequest GetWebRequest(Uri address)
        {
            string stringTimeout = CodeTable.Instant.GetAppSettingValue("QRPayment_URL_TimeOut");
            var request = base.GetWebRequest(address);

            bool ret;
            int _timeout = 0;
            ret = int.TryParse(stringTimeout, out _timeout);

            if (ret)
                request.Timeout = _timeout;
            else
                request.Timeout = 5000;

            return request;
        }
    }
}

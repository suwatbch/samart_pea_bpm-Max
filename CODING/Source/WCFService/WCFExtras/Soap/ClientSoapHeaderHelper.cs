﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace WCFExtras.Soap
{
    public static class ClientSoapHeaderHelper
    {
        public static void SetHeader(this IClientChannel channel, string headerName, object value)
        {
            channel.Extensions.Find<SoapHeadersClientHook>().Headers[headerName] = value;
        }

        public static T GetHeader<T>(this IClientChannel channel, string headerName) where T : class
        {
            return (T)channel.Extensions.Find<SoapHeadersClientHook>().Headers[headerName];
        }
    }
}

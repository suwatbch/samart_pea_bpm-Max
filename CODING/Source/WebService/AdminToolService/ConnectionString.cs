using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace AdminToolService
{
    public sealed class ConnectionString
    {
        private static readonly ConnectionString instance = new ConnectionString();
        private ConnectionString() { }
        public static ConnectionString Instance
        {
            get
            {
                return instance;
            }
        }

        public string GetConnectionString(string DatabaseServer)
        {
            return ConfigurationManager.ConnectionStrings[DatabaseServer].ConnectionString;
        }
    }

}

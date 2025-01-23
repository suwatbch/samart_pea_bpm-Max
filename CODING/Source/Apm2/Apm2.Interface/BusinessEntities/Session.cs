using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PEA.BPM.Apm2.Interface.BusinessEntities
{
    public class Session
    {
        public class User
        {
            public static string Id;
            public static string Pwd;
            public static string Name;
            public static string ScopeId;
            //public static string Token;

            public class Token
            {
                public static string Id;
            }
        }

    }
}

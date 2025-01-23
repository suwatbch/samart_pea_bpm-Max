using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using System.Windows.Forms;
using System.Threading;
using System.Configuration;
using PEA.BPM.Infrastructure.Interface.Constants;




namespace PEA.BPM.Architecture.CommonUtilities
{
    public class Session
    {
        public static bool IsNetworkConnectionAvailable = true;
        public static bool IsNetworkConnectionAvailableReal = true; //Offline by User
        public static bool IsForcedOffline = false;    //Offline by User
        public static int NetworkLatency = 0;
        public static int GetServerTimeRetryCount = 0;

        //// รวมใบเสร็จแผนผ่อน 2021-10-07 Check ใบเสร็จรวม Enable Status from ta.AppSetting
        public static string IsGroupReceiptEnable = "N";        

        //TODO: Set Port (Bue)
        //public static string PortNumber = "81";

        public class BpmDateTime
        {
            private static DateTime _start = DateTime.Now;
            private static System.Timers.Timer _timer;
            private static TimeSpan _timeSpan = new TimeSpan(0,0,0);

            public static DateTime Now
            {
                get
                {
                    //Revise By Nick
                    //if (_timeSpan.TotalSeconds == 0)
                    if (_timer == null)
                    {
                        DateTime dt = DateTime.Now;
                        return DateTime.SpecifyKind(dt, DateTimeKind.Unspecified);
                    }
                    else
                    {
                        return _start.Add(_timeSpan);
                    }

                }
                set
                {
                    if (_timer == null)
                    {
                        _timer = new System.Timers.Timer();
                        _timer.Interval = 500;
                        _timer.Elapsed += new System.Timers.ElapsedEventHandler(_timer_Elapsed);
                    }
                    else
                    {
                        _timer.Stop();
                    }
                    
                    _timeSpan = new TimeSpan(0, 0, 0);
                    _start = value;                           
                    _timer.Start();
                }
            }

            static void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
            {
                _timeSpan = _timeSpan.Add(new TimeSpan(0, 0, 0, 0, 500));                
            }
        }

        public class User
        {
            private static string _id;

            public static string Id
            {
                get
                {
                    if (Thread.CurrentPrincipal.Identity.Name.StartsWith("WS-"))
                    {
                        return Thread.CurrentPrincipal.Identity.Name.Replace("WS-", "");
                    }
                    else
                    {
                        return _id;                        
                    }
                }
                set
                {
                    _id = value;
                }
            }

            public static string Pwd;
            public static string Name;
            public static string ScopeId;
            //public static string Token;

            public class Token
            {
                public static string Center;
                public static string Branch;
            }
        }

        public class Terminal
        {
            /// <summary>
            /// Internal used ID - char(5)
            /// </summary>
            public static string Id
            {
                get
                {
                    return LocalSettingHelper.Instance().ReadString("PosId");
                }
            }

            /// <summary>
            /// Pos Code registered to RD
            /// </summary>
            public static string Code
            {
                get
                {
                    return LocalSettingHelper.Instance().ReadString("PosNo");
                }
            }

            public static string TaxId
            {
                get
                {
                    return LocalSettingHelper.Instance().ReadString("TaxId");
                }
            }
        }

        public class Branch
        {
            public static string Id
            {
                get
                {
                    return LocalSettingHelper.Instance().ReadString("BranchId");
                }
            }

            public static string PostedServerId
            {
                get
                {
                    return ConfigurationManager.AppSettings["BranchID"];
                }

            }

            public static string Name
            {
                get
                {
                    return LocalSettingHelper.Instance().ReadString("BranchName");
                }
            }

            public static string Name2
            {
                get
                {
                    return LocalSettingHelper.Instance().ReadString("BranchName2");
                }
            }

            public static string Number
            {

                get
                {
                    return LocalSettingHelper.Instance().ReadString("BranchNo");
                }
            }

            public static string Address
            {
                get
                {
                    LocalSettingHelper hp = LocalSettingHelper.Instance();
                    return string.Format("{0}", hp.ReadString("BranchAddress"));
                }
            }

            public static string BranchLevel
            {
                get
                {
                    return LocalSettingHelper.Instance().ReadString("BranchLevel");
                }
            }

            public static bool OnlineConnection
            {
                get
                {
                    return !(LocalSettingHelper.Instance().ReadString("Online")=="False");
                }
            }

            public static string BACode
            {
                get
                {
                    return LocalSettingHelper.Instance().ReadString("BACode");
                }
            }
        }

        public class Server
        {
            public static string RegisterProxyAddress = "http://localhost/BPMService/";

            public class Address
            {
                public static string Center
                {
                    get
                    {
                        #if(!DEBUG)
                            return LocalSettingHelper.Instance().ReadString("CenterServerWsAddress");
                        #else 
                            return LocalSettingHelper.Instance().ReadString("CenterServerWsAddress");
                        #endif
                    }
                }

                public static string Report
                {
                    get
                    {
                        #if(!DEBUG)
                            return LocalSettingHelper.Instance().ReadString("ReportServerWsAddress");
                        #else 
                            return LocalSettingHelper.Instance().ReadString("ReportServerWsAddress");
                        #endif
                    }
                }

                public static string CenterBackup
                {
                    get
                    {
                        string s = LocalSettingHelper.Instance().ReadString("BackupUri");

#if(!DEBUG)
                        //backup of the line
                        if (string.IsNullOrEmpty(s))
                           s = BPMConnection.ProdConnIp;
#else 
                        //if (string.IsNullOrEmpty(s))
                        //    s = BPMConnection.TestConnIp;
                        if (string.IsNullOrEmpty(s))
                        {
                            ServerAddressInputForm form = new ServerAddressInputForm(ServerAddressInputForm.ServerType.Center);
                            if (form.ShowDialog() == DialogResult.OK)
                            {
                                s = form.ServerAddress;
                                if (form.IsSaveInputServerAddressAsDefault)
                                {
                                    LocalSettingHelper.Instance().Add("BackupUri", s);
                                }
                            }
                        }
#endif
                        return s;
                    }
                }

                public static string Branch
                {
                    get
                    {
                        string s = LocalSettingHelper.Instance().ReadString("BranchServerWsAddress");
                        if (string.IsNullOrEmpty(s))
                        {
                            ServerAddressInputForm form = new ServerAddressInputForm(ServerAddressInputForm.ServerType.Branch);
                            if (form.ShowDialog() == DialogResult.OK)
                            {
                                s = form.ServerAddress;
                                if (form.IsSaveInputServerAddressAsDefault)
                                {
                                    LocalSettingHelper.Instance().Add("BranchServerWsAddress", s);
                                }
                            }
                        }

                        return s;
                    }
                }

            }

            private static string _connectioninfo;
            public static string ConnectionInfo
            {
                get { return _connectioninfo; }
                set
                {
                    _connectioninfo = "";
#if DEBUG
                    _connectioninfo = value;
                    //TODO: Set Port (Bue)
                    //_connectioninfo = _connectioninfo == null ? null : _connectioninfo.Replace(":" + PortNumber, "");
                    if (!string.IsNullOrEmpty(_connectioninfo))
                    {
                        if (Session.Branch.OnlineConnection)
                        {
                            if (_connectioninfo.ToLower().StartsWith("http://cbsbpm00.cbsproj.pea.co.th/bpmcenterappservices/"))
                                _connectioninfo = "Production";
                            else
                                _connectioninfo = "CENTER : " + _connectioninfo;
                        }
                        else
                            _connectioninfo = "BRANCH : " + _connectioninfo;
                    }     
#endif
                }
            }
        }

        public class NewsBroadcast
        {
            private static bool _enabled = true;

            public static bool Enabled
            {
                set { _enabled = value; }
                get { return _enabled; }
            }
        }

        public class Application
        {
            private static string _version;
            private static string _name;

            public static string Version
            {
                set { _version = value; }
                get { return _version; }
            }

            public static string Name
            {
                set { _name = value; }
                get { return _name; }
            }
        }

        public class Work
        {
            private static string _workId;
            private static int _dayCount;
            private static DateTime _openWorkDt;
            private static bool _onCloseNotify = true;

            public static string Id
            {
                set { _workId = value; }
                get { return _workId; }
            }

            public static int DayCount
            {
                set { _dayCount = value; }
                get { return _dayCount; }
            }

            public static DateTime OpenWorkDt
            {
                set { _openWorkDt = value; }
                get { return _openWorkDt; }
            }

            public static bool OnCloseNotify
            {
                set { _onCloseNotify = value; }
                get { return _onCloseNotify; }
            }
        }

        public class EDCComPort
        {
            /// <summary>
            /// COMPort Name (Read from BPMSetting.Dat)
            /// </summary>
            public static string PortName
            {
                get
                {
                    return LocalSettingHelper.Instance().ReadString("EDCComPort");

                }
            }
            public static string BaudRate
            {
                get
                {
                    return LocalSettingHelper.Instance().ReadString("EDCBaudRate");
                }

            }
            public static string DataBits
            {
                get
                {
                    return LocalSettingHelper.Instance().ReadString("EDCDataBits");
                }

            }
            public static string StopBits
            {
                get
                {
                    return LocalSettingHelper.Instance().ReadString("EDCStopBits");
                }

            }
            public static string HandShake
            {
                get
                {
                    return LocalSettingHelper.Instance().ReadString("EDCHandShake");
                }

            }
            public static string Parity
            {
                get
                {
                    return LocalSettingHelper.Instance().ReadString("EDCParity");
                }

            }
            public static string ComportDataReceived;
            public static bool   SendDataToEDC;
            public static bool   WaitDataFromEDC;
            public static int    ResponseCount;

        }

    }
}

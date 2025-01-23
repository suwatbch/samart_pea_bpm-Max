using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace PEA.BPM.Architecture.CommonUtilities
{
    public sealed class Time
    {
        static readonly Time instance = new Time();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Time()
        {
        }

        Time()
        {
        }

        public static Time Instance
        {
            get
            {
                return instance;
            }
        }

        public static void SetClientTime(DateTime serverTime)
        {
            Win32.SYSTEMTIME current = new Win32.SYSTEMTIME();
            Win32.GetLocalTime(ref current);
            current.Year = Convert.ToUInt16(serverTime.Year);
            current.Month = Convert.ToUInt16(serverTime.Month);
            current.Day = Convert.ToUInt16(serverTime.Day);
            current.Hour = Convert.ToUInt16(serverTime.Hour);
            current.Minute = Convert.ToUInt16(serverTime.Minute);
            current.Second = Convert.ToUInt16(serverTime.Second);
            current.Milliseconds = Convert.ToUInt16(serverTime.Millisecond);
            current.DayOfWeek = Convert.ToUInt16(serverTime.DayOfWeek);
            Win32.SetLocalTime(ref current);
        }

        public static void SetClientTimeBySessionTime()
        {
            SetClientTime(Session.BpmDateTime.Now);
        }

        public static void SetToBangkokTimeZone()
        {
            string osInfo = MachineInfo.getOSInfo();
            if (osInfo.ToUpper().Replace(" ", "").IndexOf("WINDOWSXP") != -1)
            {
                //--TIME ZONE--XP
                TimeZoneInformation tzi = ClassTimeZone.GetTimeZone();
                TimeZoneInformation tz = new TimeZoneInformation();
                tz.standardName = "SE Asia Standard Time";
                tz.bias = -420;
                ClassTimeZone.SetTimeZone(tz);
            }
            else if (osInfo.ToUpper().Replace(" ", "").IndexOf("WINDOWS7") != -1)
            {
                //--TIME ZONE--7
                System.Diagnostics.ProcessStartInfo procStartInfo =
                    new System.Diagnostics.ProcessStartInfo("cmd", "/c " + "TZUTIL /s \"SE Asia Standard Time\"");
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                string result = proc.StandardOutput.ReadToEnd();
                Console.WriteLine(result);
            }
        }

    }
    
    //public sealed class Singleton
    //{
    //    static readonly Singleton instance = new Singleton();

    //    // Explicit static constructor to tell C# compiler
    //    // not to mark type as beforefieldinit
    //    static Singleton()
    //    {
    //    }

    //    Singleton()
    //    {
    //    }

    //    public static Singleton Instance
    //    {
    //        get
    //        {
    //            return instance;
    //        }
    //    }
    //}


    public class Win32
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 16)]
        public struct SYSTEMTIME
        {
            public ushort Year;
            public ushort Month;
            public ushort DayOfWeek;
            public ushort Day;
            public ushort Hour;
            public ushort Minute;
            public ushort Second;
            public ushort Milliseconds;
        }
        [DllImport("kernel32.dll", EntryPoint = "SetLocalTime", SetLastError = true)]
        public static extern int SetLocalTime(ref SYSTEMTIME lpSystemTime);
        [DllImport("kernel32.dll", EntryPoint = "GetLocalTime", SetLastError = true)]
        public static extern int GetLocalTime(ref SYSTEMTIME lpSystemTime);
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct TimeZoneInformation
    {
        public int bias;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string standardName;

        //SystemTime standardDate;
        PEA.BPM.Architecture.CommonUtilities.Win32.SYSTEMTIME standardDate;

        public int standardBias;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string daylightName;

        //SystemTime daylightDate;
        PEA.BPM.Architecture.CommonUtilities.Win32.SYSTEMTIME daylightDate;

        public int daylightBias;
    }

    public class ClassTimeZone
    {

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern int GetTimeZoneInformation(out TimeZoneInformation lpTimeZoneInformation);


        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern bool SetTimeZoneInformation(ref TimeZoneInformation lpTimeZoneInformation);

        public static void SetTimeZone(TimeZoneInformation tzi)
        {
            // set local system timezone
            SetTimeZoneInformation(ref tzi);
        }


        public static TimeZoneInformation GetTimeZone()
        {
            // create struct instance
            TimeZoneInformation tzi;

            // retrieve timezone info
            int currentTimeZone = GetTimeZoneInformation(out tzi);
            return tzi;
        }

    }
}

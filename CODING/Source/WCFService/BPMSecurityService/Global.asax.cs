using System;
using System.Timers;
using PEA.BPM.WebService.Security.Core;

namespace PEA.BPM.WebService.Security
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            ServiceLog.Instance.WriteEvent(LogType.System, "System", "Start Security Service.");
            SystemInfo.Instance.LastStartUpTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            Timer tsync = new Timer();
            tsync.Interval = SystemInfo.Instance.Feature.SyncToDatabaseInterval;
            tsync.Elapsed += t_SyncToDatabase;
            tsync.Start();

            Timer tcheckoffline = new Timer();
            tcheckoffline.Interval = SystemInfo.Instance.Feature.CheckUserOfflineInterval;
            tcheckoffline.Elapsed += t_CheckOffline;
            tcheckoffline.Start();

            //Timer tupdatenews = new Timer();
            //tupdatenews.Interval = SystemInfo.Instance.Feature.NewsUpdateInterval;
            //tupdatenews.Elapsed += new ElapsedEventHandler(t_NewsUpdate);
            //tupdatenews.Start();

            Timer trecordstats = new Timer();
            trecordstats.Interval = SystemInfo.Instance.Feature.RecodeStatInterval;
            trecordstats.Elapsed += t_RecordStats;
            trecordstats.Start();
        }

        static void t_SyncToDatabase(object sender, ElapsedEventArgs e)
        {
            AuthenticationController.Instance.OnSyncToDatabase();
        }

        static void t_CheckOffline(object sender, ElapsedEventArgs e)
        {
            AuthenticationController.Instance.OnCheckOffline();
        }

        static void t_RecordStats(object sender, ElapsedEventArgs e)
        {
            SystemInfo.Instance.OnRecodeStats();
        }

        //void t_NewsUpdate(object sender, ElapsedEventArgs e)
        //{
        //    NewsController.Instance.OnUpdateNews();
        //}

        protected void Application_End(object sender, EventArgs e)
        {
            AuthenticationController.Instance.OnSyncToDatabase();
            ServiceLog.Instance.WriteEvent(LogType.System, "System", "Stop Security Service.");
        }
    }
}
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using PEA.BPM.WebService.Security.BPMLogDSTableAdapters;
using PEA.BPM.WebService.Security.Core;

namespace PEA.BPM.WebService.Security
{
    public enum LogType { Normal = 0, System = 1, Error = 2 }

    public class ServiceLog
    {
        #region Singleton
        private static readonly ServiceLog _instance = new ServiceLog();
        private ServiceLog()
        {
        }

        public static ServiceLog Instance
        {
            get { return _instance; }
        }
        #endregion
        
        private List<string> _errorlist = new List<string>();
        public List<string> GetInternalError()
        {
            List<string> errlist = new List<string>();
            lock (this)
            {
                foreach (string str in _errorlist) errlist.Add(str);
            }
            return errlist;
        }

        public void WriteNetwork(string reqip, string userid, string ipaddress, int latency, int retrycount)
        {
            if (!SystemInfo.Instance.Feature.IsSaveLogToDatabase) return;
            try
            {
                tNetworkStatTableAdapter tnetworkada = new tNetworkStatTableAdapter();
                tnetworkada.Insert(DateTime.Now, reqip, userid, ipaddress, latency, retrycount);
            }
            catch (Exception ee)
            {
                lock (this)
                {
                    AddInternalErrorLog(ee.Message);
                }
            }
        }

        public void WriteEvent(LogType logtype, string userid, string msg)
        {
            if (!SystemInfo.Instance.Feature.IsSaveLogToDatabase) return;
            try
            {
                tEventTableAdapter teventada = new tEventTableAdapter();
                teventada.Insert(DateTime.Now, userid, msg, (int)logtype);
            }
            catch (Exception ee)
            {
                lock (this)
                {
                    AddInternalErrorLog(ee.Message);
                }
            }
        }
        public void WriteUserStat(int usercount, int cachecusercount)
        {
            if (!SystemInfo.Instance.Feature.IsSaveLogToDatabase) return;
            try
            {
                tUserStatTableAdapter tuserada = new tUserStatTableAdapter();
                tuserada.Insert(DateTime.Now, usercount, cachecusercount);
            }
            catch (Exception ee)
            {
                lock (this)
                {
                    AddInternalErrorLog(ee.Message);
                }
            }
        }
        

        public string GetTodayEvent(LogType logtype)
        {
            try
            {
                string res = "";
                tEventTableAdapter teventada = new tEventTableAdapter();
                BPMLogDS.tEventDataTable dt = teventada.Get100ByETypeAndEDate((int)logtype, DateTime.Now);
                foreach (BPMLogDS.tEventRow drow in dt)
                {
                    res += drow.EDate.ToString("dd/MM/yyyy HH:mm:ss : ") + drow.EText + Environment.NewLine;
                }
                return res;
            }
            catch (Exception ee)
            {
                lock (this)
                {
                    if (SystemInfo.Instance.Feature.IsSaveLogToDatabase) AddInternalErrorLog(ee.Message);
                }
                return "";
            }
        }


        private void AddInternalErrorLog(string msg)
        {
            lock (this)
            {
                _errorlist.Add(string.Format("{0}: {1}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), msg));
            }
        }

    }
}

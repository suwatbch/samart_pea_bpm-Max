using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PEA.BPM.WebService.Security.BPMNewsDSTableAdapters;
using System.Collections.Generic;

namespace PEA.BPM.WebService.Security.News
{
    public class NewsController
    {
        private nb_sel_NewsBroadcastBySentDateTableAdapter _newsada;
        private Dictionary<DateTime, DataTable> _newshash = null; 

        #region Singleton
        private static readonly NewsController _instance = new NewsController();
        private NewsController()
        {
            _newsada = new nb_sel_NewsBroadcastBySentDateTableAdapter();
            _newshash = new Dictionary<DateTime, DataTable>();
        }

        public static NewsController Instance
        {
            get { return _instance; }
        }
        #endregion


        private DataTable GetNewsInfoFromDB(DateTime sentdt)
        {
            ServiceLog.Instance.WriteEvent(LogType.Normal, "", "Get news [" + sentdt.ToString("dd/MM/yyyy") + "] from database.");

            BPMNewsDS.nb_sel_NewsBroadcastBySentDateDataTable dt = _newsada.GetData(sentdt);
            return dt as DataTable;
        }

        private DataTable GetNewsInfo(DateTime sentdt, bool forceupdate)
        {
            lock (this)
            {
                DataTable dt;
                if (_newshash.ContainsKey(sentdt))
                {
                    if (forceupdate)
                    {
                        _newshash.Remove(sentdt);
                        dt = GetNewsInfoFromDB(sentdt);
                        _newshash.Add(sentdt, dt);
                    }
                    else
                    {
                        dt = _newshash[sentdt];
                    }
                }
                else
                {
                    dt = GetNewsInfoFromDB(sentdt);
                    if (dt != null) _newshash.Add(sentdt, dt);
                }
                return dt;
            }
        }

        internal DataTable GetBroadcastListBySentDate(DateTime _sentDate, bool forceupdate)
        {
            return GetNewsInfo(_sentDate, forceupdate);
        }

        internal DataTable GetBroadcastListBySentDate(DateTime _sentDate)
        {
            return GetNewsInfo(_sentDate, false);
        }

        internal void OnUpdateNews()
        {
            GetNewsInfo(DateTime.Now, true);
        }
    }
}

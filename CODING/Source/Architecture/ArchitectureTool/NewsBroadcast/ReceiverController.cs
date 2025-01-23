using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Globalization;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.NewsBroadcast.Interface.BusinessEntities;
using PEA.BPM.NewsBroadcast.Interface.Constants;
using PEA.BPM.NewsBroadcast.SG;
using System.Threading;

namespace PEA.BPM.Architecture.ArchitectureTool.NewsBroadcast
{
    public class ReceiverController
    {

        #region Variable
        private NewsBroadcastSG ws;
        private bool isRefreshing = false;
        #endregion

        #region Singletron
        private static ReceiverController _instant = new ReceiverController();
        public static ReceiverController Instant
        {
            get
            {
                return _instant;
            }
        }
        #endregion

        #region Constructor
        public ReceiverController()
        {
            try
            {
                isRefreshing = false;
                CultureInfo appCulture = new CultureInfo("th-TH");
                Thread.CurrentThread.CurrentCulture = appCulture;
                Thread.CurrentThread.CurrentUICulture = appCulture;
            }
            catch
            {
                //do nothing
            }
        }
        #endregion

        #region Method
        /// <summary>
        /// Read the News that entry in database and display dialog in to BPM Client
        /// </summary>
        public void RefreshNews()
        {
            if (isRefreshing) return;
            isRefreshing = true;
            List<NewsBroadcastInfo> listNews = new List<NewsBroadcastInfo>();
            try
            {
                 ws = new NewsBroadcastSG();
                 listNews = ws.GetNewsBroadcastNow(Session.BpmDateTime.Now, Session.User.Id, 1);

                 foreach (NewsBroadcastInfo news in listNews)
                 {
                     if (!news.IsOpened)
                     {
                         ws = new NewsBroadcastSG();
                         ws.UpdateNewsBroadcastUserOpened(news.BroadcastId, Session.User.Id);
                         news.IsOpened = true;

                         RecieverDialogForm recDlg = new RecieverDialogForm(news);
                         recDlg.ShowDialog();
                     }
                 }
            }
            catch
            {
               //Do nothing
            }
            isRefreshing = false;
        }
        #endregion

    }
    
   
}

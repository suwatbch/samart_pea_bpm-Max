using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;
using System.Windows.Forms;
using PEA.BPM.Architecture.ArchitectureTool.NewsBroadcast;
using System.Net;

namespace PEA.BPM.Architecture.ArchitectureTool
{
    public class NewsBroadcastConnectionMonitor
    {
        private static NewsBroadcastConnectionMonitor _instant = new NewsBroadcastConnectionMonitor();
        private System.Threading.Thread _thread; 

        private NewsBroadcastConnectionMonitor()
        {
        }

        public static NewsBroadcastConnectionMonitor Instant
        {
            get
            {
                return _instant;
            }
        }

        private Timer _monitorTimer;

        public void Start()
        {
            _monitorTimer = new Timer();
            _monitorTimer.Enabled = true;
            _monitorTimer.Interval = 1000;
            _monitorTimer.Tick += new EventHandler(_monitorTimer_Tick);
        }

        private void _monitorTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                //อ่านข่าวทุกๆ 1 ชั่วโมง
                _monitorTimer.Interval = 3600000;

                if (Session.User.Id != null && Session.IsNetworkConnectionAvailable && Session.Terminal.Id != null && Session.NewsBroadcast.Enabled)
                {
                    //แตก Thread อ่านข้อมูลเพื่อไม่ให้หน้า UI ค้าง
                    _thread = new System.Threading.Thread(new System.Threading.ThreadStart(MonitorThread));
                    _thread.Start();
                  
                    _monitorTimer.Enabled = true;
                }
            }
            catch
            {
                //ignore error 
            }
        }

        public void MonitorThread()
        {
            try
            {
                new PEA.BPM.Architecture.ArchitectureTool.NewsBroadcast.ReceiverController().RefreshNews();
            }
            catch
            {
                //ignore error 
            }
            finally
            {
                _thread.Abort();
            }
        }


    }
}

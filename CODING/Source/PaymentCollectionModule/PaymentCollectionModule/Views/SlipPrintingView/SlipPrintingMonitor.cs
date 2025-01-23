using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using System.IO.Ports;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureInterface.Constants;
using System.Globalization;

namespace PEA.BPM.PaymentCollectionModule.Views.SlipPrintingView
{
    internal class SlipPrintingMonitor
    {
        private WorkItem _workItem;
        private Queue<string> _queue = new Queue<string>();
        private System.Windows.Forms.Timer _timer;
        object _syncRoot = new object();
        
        string _poolDir = string.Format("{0}/{1}", BPMPath.ConfigPath, CodeNames.SlipPrintintPoolDir);
        private string yearMonth = Session.BpmDateTime.Now.ToString("yyyyMM", new CultureInfo("th-TH").DateTimeFormat);

        public SlipPrintingMonitor(WorkItem workItem)
        {
            _workItem = workItem;            

            if (!Directory.Exists(_poolDir))
            {
                Directory.CreateDirectory(_poolDir);
            }

            //if (!Directory.Exists(_poolDir + "/done"))
            //{
            //    Directory.CreateDirectory(_poolDir + "/done");
            //}
            if (!Directory.Exists(_poolDir + "/done" + yearMonth))
            {
                Directory.CreateDirectory(_poolDir + "/done" + yearMonth);
            }

            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 500;
            _timer.Tick += new EventHandler(timer_Tick);
            _timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            _timer.Enabled = false;
            CheckFiles();
            _timer.Enabled = true;
        }

        private void CheckFiles()
        {
            string[] fn = Directory.GetFiles(_poolDir, "*.txt");

            if (fn.Length > 0)
            {
                for (int i = 0; i < fn.Length; i++)
                {
                    if (!_queue.Contains(fn[i]))
                    {
                        _queue.Enqueue(fn[i]);
                    }
                }
                BackgroundWorker bgw = new BackgroundWorker();
                bgw.DoWork += new DoWorkEventHandler(bgw_DoPrint);
                bgw.RunWorkerAsync();
            }
        }

        private void bgw_DoPrint(object sender, DoWorkEventArgs e)
        {
            lock (_syncRoot)
            {
                while (_queue.Count > 0)
                {
                    string fullPath = _queue.Dequeue();

                    if (File.Exists(fullPath))
                    {
                        using (StreamReader sr = new StreamReader(fullPath))
                        {
                            List<string> toPrintData = new List<string>();
                            string data = sr.ReadLine();
                            while (null != data)
                            {
                                toPrintData.Add(data);
                                data = sr.ReadLine();
                            }
                            sr.Close();
                            
                            Fp410Receipt.Printer.Instant.Print(toPrintData);
                        }

                        FileInfo f = new FileInfo(fullPath);
                        //f.MoveTo(string.Format("{0}/done/{1}", _poolDir, f.Name));
                        f.MoveTo(string.Format("{0}/done{1}/{2}", _poolDir, yearMonth ,f.Name));
                        //File.Delete(fullPath);
                        Thread.Sleep(1000); //// DELAY การ Print Uthen.P 2021-OCT-21 19.11
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using System.Windows.Forms;
using PEA.BPM.Architecture.CommonUtilities;
using System.Xml.Serialization;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.PaymentCollectionModule.Views.SlipPrintingView;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface;
using System.Runtime.Serialization;
using PEA.BPM.Architecture.ArchitectureInterface.Constants;
using PEA.BPM.PaymentCollectionModule.Services;
using System.Net;
using System.Globalization;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using System.Text.RegularExpressions;
using ProtoBuf;

namespace PEA.BPM.PaymentCollectionModule.Views.PaymentMethodSelectionView
{
    public class PaymentOfflineMonitor
    {
        private WorkItemController _workItem;
        private IBillingService _billingService;
        private System.Windows.Forms.Timer _timer;
        private object _syncRoot = new object();
        private string _offlineDataDir = string.Format("{0}\\{1}", BPMPath.ConfigPath, CodeNames.OfflineDataDir);
        private bool _inProgress = false;
        private Regex rg;
        private bool _hasStarted = false;
        private bool _once = true;

        //fake folder
        private string _offlineFakeDir = string.Format("{0}\\{1}", BPMPath.ConfigPath, CodeNames.OfflineFakeDir);

        public PaymentOfflineMonitor(IBillingService billingService, WorkItemController workItem)
        {
            _billingService = billingService;
            _workItem = workItem;

            rg = new Regex("[0-9]*-[0-9]*-[0-9]*.txt", RegexOptions.IgnoreCase);

            if (!Directory.Exists(_offlineDataDir))
                Directory.CreateDirectory(_offlineDataDir);

            //create fake folder
            if (!Directory.Exists(_offlineFakeDir))
                Directory.CreateDirectory(_offlineFakeDir);

            //delete old
            string[] fakeFiles = Directory.GetFiles(_offlineFakeDir);
            foreach (string fake in fakeFiles)
                File.Delete(fake);

            //write a fake file
            //IFormatter serializer = new BinaryFormatter();
            string fileName = string.Format(_offlineFakeDir + "\\{0}-{1}.txt", Session.Terminal.Id, DateTime.Now.ToString("yyyyMMdd-hhmmss"));
            using (Stream writer = new FileStream(fileName, FileMode.Create))
            {
                Serializer.Serialize<OfflineItems>(writer, new OfflineItems());
                //serializer.Serialize(writer, new OfflineItems());
                writer.Close();
            }

            if (!Directory.Exists(_offlineDataDir + "\\Done"))
                Directory.CreateDirectory(_offlineDataDir + "\\Done");

            //if (!Directory.Exists(_offlineDataDir + "\\Failed"))
            //    Directory.CreateDirectory(_offlineDataDir + "\\Failed");

            DirectoryInfo dirInfo = new DirectoryInfo(_offlineDataDir);
            dirInfo.Attributes = FileAttributes.Hidden;

            dirInfo = new DirectoryInfo(_offlineDataDir + "\\Done");
            dirInfo.Attributes = FileAttributes.Hidden;

            //dirInfo = new DirectoryInfo(_offlineDataDir+"\\Failed");
            //dirInfo.Attributes = FileAttributes.Hidden;

            //retry fail
            //string[] failFiles = Directory.GetFiles(_offlineDataDir + "\\Failed");
            //foreach(string file in failFiles)
            //{
            //    FileInfo f = new FileInfo(file);
            //    File.Move(file, _offlineDataDir + "\\" + f.Name);
            //}

            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 2000;  //  2 secs at start
            _timer.Tick += new EventHandler(timer_Tick);
            _timer.Start();
        }

        //adhoc call
        //two methods call now. 1) click 1.8 menu, 2)change from offline to online.
        //1) refreshwork = false
        //2) refreshwork = true
        public void CheckFileNow(bool refreshWork)
        {
            if (_inProgress) return;

            _inProgress = true;
            if (Session.IsNetworkConnectionAvailable && Session.Branch.Id != null && Session.User.Id != null) //already registered
            {
                _timer.Enabled = false;
                CheckFiles(refreshWork);
                _timer.Enabled = true;
            }
            _inProgress = false;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (_inProgress) return;

            _inProgress = true;
            _timer.Enabled = false;

            if (Session.IsNetworkConnectionAvailable && Session.Branch.Id != null && Session.User.Id != null) //already registered
            {
                //trigger only first time
                CheckFiles(_once);

                //just retry timer, set it longer
                _timer.Interval = 1800000; //30 minutes
                _once = false;
            }

            _timer.Interval += 2000;
            _timer.Enabled = true;
            _inProgress = false;
        }

        private string[] Sort(string[] entries)
        {
            //by name
            List<string> ret = new List<string>();
            foreach (string st in entries)
                ret.Add(st);

            ret.Sort();

            return ret.ToArray();
        }

        private void SetErrorLog(string logKey, string msg)
        {
            OfflineLogInfo log = new OfflineLogInfo();
            log.FileName = logKey;
            log.ClientIP = MachineInfo.GetLocalIP();
            log.PosId = Session.Terminal.Id;
            log.RunCashier = Session.User.Id;
            log.BranchId = Session.Branch.Id;
            log.BranchName = Session.Branch.Name;
            log.Solved = "1";
            log.ErrorMsg = msg;
            _billingService.OfflineLog(log);
        }

        private void SetSuccessLog(string logKey, string msg)
        {
            OfflineLogInfo log = new OfflineLogInfo();
            log.FileName = logKey;
            log.ClientIP = MachineInfo.GetLocalIP();
            log.PosId = Session.Terminal.Id;
            log.RunCashier = Session.User.Id;
            log.BranchId = Session.Branch.Id;
            log.BranchName = Session.Branch.Name;
            log.Solved = "0";
            log.ErrorMsg = "Success";
            _billingService.OfflineLog(log);
        }

        private void CheckFiles(bool refreshWork)
        {
            FileStream fs = null;
            bool doneFistFile = false;
            string status = null;
            string[] files = Directory.GetFiles(_offlineDataDir);
            //important sort! order by filename (time stamp), we are going to use the first file to check work status
            string[] fileEntries = Sort(files);

            if (fileEntries.Length > 0)
            {
                _workItem.SetStatusText("เริ่มส่งข้อมูลการรับชำระเงินแบบ Offline ไปยังเครื่องแม่ข่าย...");

                foreach (string onePayment in fileEntries)
                {
                    FileInfo file = new FileInfo(onePayment);
                    file.Attributes = FileAttributes.Normal;

                    //restrict only offline format
                    if (!rg.IsMatch(file.Name))
                        continue;

                    try
                    {
                        //read offline file
                        fs = new FileStream(onePayment, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        //IFormatter formatter = new BinaryFormatter();
                        // OfflineItems items = (OfflineItems)formatter.Deserialize(fs);
                        OfflineItems items = Serializer.Deserialize<OfflineItems>(fs);
                        fs.Close();

                        ProcessFile(items);
                        SetSuccessLog(file.Name, "Success");
                        File.Move(onePayment, string.Format("{0}\\Done\\D-{1}", _offlineDataDir, Path.GetFileName(onePayment)));

                        if (!doneFistFile)
                        {
                            //check work status
                            OpenWorkParam param = new OpenWorkParam();
                            param.CashierId = Session.User.Id;
                            param.PosId = Session.Terminal.Id;
                            param.PaymentDt = items.PaymentDt.Value;
                            param.BranchId = Session.Branch.Id;
                            param.ModifiedBy = Session.User.Id;
                            status = _billingService.CheckWorkStatus(param);

                            //log to center, but offline payment will be always processed

                            if (status == "Error")
                            {
                                string logKey = string.Format("{0}-{1}.txt", Session.Terminal.Id, DateTime.Now.ToString("yyyyMMdd-hhmmss", new CultureInfo("th-TH")));
                                SetErrorLog(logKey, "Could not open work for offline payment! ");
                            }
                            else if (status == "Success")
                            {
                                //notify user
                                MessageBox.Show("ระบบได้ทำการเปิดกะให้โดยอัตโนมัติ" +
                                "\nเพื่อรับรายการชำระเงินแบบ offline ที่คงค้างในระบบ\n", "เปิดกะอัตโนมัติ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }

                        //if cash in used, trigger this work
                        //important! 
                        //what if failed at started? cashier must open work by his/herself.
                        //if (!doneFistFile && (status == "Success" || status == "Ready") && refreshWork)
                        if (!doneFistFile && refreshWork)
                        {
                            _workItem.SignalFinishOfflineUpload();
                            doneFistFile = true;
                        }
                    }
                    catch (Exception ex) // TODO: ไว้ย้อนกลับมาทำ
                    {
                        if (fs != null) fs.Close();

                        if (ex.Message.IndexOf("Violation of PRIMARY KEY constraint") > -1)
                        {
                            try
                            {
                                File.Move(onePayment, string.Format("{0}\\Done\\D-{1}", _offlineDataDir, Path.GetFileName(onePayment)));
                                SetSuccessLog(file.Name, "Success with Violation of PRIMARY KEY constraint");
                            }
                            catch (Exception ek) // TODO: จะไม่ทำอะไรจริงๆ เหรอ ?
                            { }
                        }
                        else
                        {
                            //write to center log
                            SetErrorLog(file.Name, ex.Message);
                            file.Attributes = FileAttributes.Hidden;
                        }
                        //File.Move(onePayment, string.Format("{0}\\Failed\\{1}", _offlineDataDir, Path.GetFileName(onePayment)));
                    }
                }

                //if all failed, go signal work
                if (!doneFistFile && refreshWork)
                    _workItem.SignalFinishOfflineUpload();

                _workItem.SetStatusText("Ready");
            }
            else
            {
                //only first time
                if (!_hasStarted)
                    _workItem.SignalFinishOfflineUpload();

                _hasStarted = true;
            }
        }

        private void ProcessFile(OfflineItems items)
        {
            if (items.Invoices.Count > 0 && items.PaymentMethods.Count > 0)
            {
                _billingService.PayInvoice(null, items.Invoices, items.PaymentMethods, items.Receipts, items.GroupDividualReceipts,
                                           items.ExtReceipt, items.PaymentDt.Value, Session.Terminal.Id, Session.Terminal.Code,
                                           Session.Branch.Id, Session.Branch.Name, items.PayingAmount, items.ScreenType,
                                           items.CashierId, items.CashierName, items.WorkId, true);
            }
        }
    }
}

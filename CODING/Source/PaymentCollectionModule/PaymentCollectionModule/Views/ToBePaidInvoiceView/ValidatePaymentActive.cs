using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Constants;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using System.IO;
using System.Runtime.Serialization;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using System.Runtime.Serialization.Formatters.Binary;
using PEA.BPM.Architecture.CommonUtilities;
using System.Globalization;
using ProtoBuf;

namespace PEA.BPM.PaymentCollectionModule.Views.ToBePaidInvoiceView
{
    public class ValidatePaymentActive
    {
        private IBillingService _billingService;

        public void ValidatePaymentActiveFromOffline()
        {
            string _offlineDataDir = string.Format("{0}\\{1}", BPMPath.ConfigPath, CodeNames.OfflineDataDir);
            string[] files = Directory.GetFiles(_offlineDataDir);
            string[] fileOfflineEntries = Sort(files);
            List<string> offlineReceipts = new List<string>();

            if (fileOfflineEntries.Length > 0)
            {
                foreach (string onePayment in fileOfflineEntries)
                {
                    FileInfo file = new FileInfo(onePayment);
                    file.Attributes = FileAttributes.Normal;
                    //has a receipt so we have payment
                    //check InUsed file. If cannot open, it is inused.
                    try
                    {
                        FileStream fs = new FileStream(onePayment, FileMode.Open);
                        fs.Close();
                    }
                    catch
                    {
                        continue;
                    }


                    {
                        //read offline file
                        FileStream fs = new FileStream(onePayment, FileMode.Open);
                        IFormatter formatter = new BinaryFormatter();
                        //OfflineItems items = (OfflineItems)formatter.Deserialize(fs);
                        OfflineItems items = Serializer.Deserialize<OfflineItems>(fs);
                        fs.Close();


                        foreach (PrintingReceipt pr in items.Receipts)
                        {
                            offlineReceipts.Add(pr.ReceiptId);
                            //TODO: Comment for action of not validate payment before offline payment make diff in 2.17
                            //offlineReceipts.Add(pr.ReceiptId + ":" + pr.CustomerId);
                        }
                    }
                }
            }



            if (offlineReceipts.Count > 0)
            {
                string transactionPath = BPMPath.ConfigPath + "\\TransactionData";
                string fileName = string.Format("{0}-{1}-{2}.txt", Session.Terminal.Code.ToString().Split('-')[0], Session.BpmDateTime.Now.ToString("yyyyMMdd", new CultureInfo("th-TH")), "*");

                if (!Directory.Exists(transactionPath))
                {
                    Directory.CreateDirectory(transactionPath);
                }

                string[] fileEntries = Directory.GetFiles(transactionPath, fileName);


                if (fileEntries.Length > 0)
                {
                    foreach (string todayPath in fileEntries)
                    {
                        List<CAC15Report> report = new List<CAC15Report>();
                        bool isSave = false;

                        if (File.Exists(todayPath))
                        {
                            //Read existing transactions from text file
                            FileStream fs = new FileStream(todayPath, FileMode.Open);
                            IFormatter formatter = new BinaryFormatter();
                            report = (List<CAC15Report>)formatter.Deserialize(fs);
                            fs.Close();
                        }

                        if (report.Count > 0)
                        {
                            List<CAC15Report> rp = report.FindAll(delegate(CAC15Report r)
                                                    {
                                                        return r.ValidateFlag == "0";
                                                    }
                                                );

                            if (rp.Count > 0)
                            {
                                foreach (CAC15Report r in report)
                                {
                                    if (r.ValidateFlag == "0")
                                    {
                                        foreach (string rc in offlineReceipts)
                                        {
                                            //TODO: Comment for action of not validate payment before offline payment make diff in 2.17
                                            //if (r.RealReceiptId == rc.Split(':')[0] && r.CaId == rc.Split(':')[1])
                                            if (r.RealReceiptId == rc)
                                            {
                                                r.ValidateFlag = "1";
                                                r.PaymentActive = "1";
                                                r.Active = "1";
                                                isSave = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }


                        //Save to text file
                        if (isSave)
                        {
                            IFormatter serializer = new BinaryFormatter();
                            using (Stream writer = new FileStream(todayPath, FileMode.Create))
                            {
                                serializer.Serialize(writer, report);
                                writer.Close();
                            }
                        }
                    }
                }
            }

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

        public void ValidatePaymentActiveFromOnline(IBillingService billingService)
        {
            string transactionPath = BPMPath.ConfigPath + "\\TransactionData";
            string fileName = string.Format("{0}-{1}-{2}.txt", Session.Terminal.Code.ToString().Split('-')[0], Session.BpmDateTime.Now.ToString("yyyyMMdd", new CultureInfo("th-TH")), "*");

            if (!Directory.Exists(transactionPath))
            {
                Directory.CreateDirectory(transactionPath);
            }

            string[] fileEntries = Directory.GetFiles(transactionPath, fileName);


            if (fileEntries.Length > 0)
            {
                foreach (string todayPath in fileEntries)
                {
                    List<CAC15Report> report = new List<CAC15Report>();
                    bool isSave = false;

                    if (File.Exists(todayPath))
                    {
                        //Read existing transactions from text file
                        FileStream fs = new FileStream(todayPath, FileMode.Open);
                        IFormatter formatter = new BinaryFormatter();
                        report = (List<CAC15Report>)formatter.Deserialize(fs);
                        fs.Close();
                    }

                    if (report.Count > 0)
                    {
                        List<CAC15Report> rp = report.FindAll(delegate(CAC15Report r)
                                                {
                                                    return r.ValidateFlag == "0";
                                                }
                                            );

                        if (rp.Count > 0)
                        {
                            foreach (CAC15Report r in report)
                            {
                                if (r.ValidateFlag == "0")
                                {
                                    if ((r.PaymentActive == null ? "1" : r.PaymentActive) == "0")
                                    {
                                        if (CheckValidatePaymentActive(billingService, r.RealReceiptId, true) == true)
                                        {
                                            r.PaymentActive = "1";
                                            r.Active = "1";
                                        }
                                    }

                                    if ((r.CancelActive == null ? "1" : r.CancelActive) == "0")
                                    {
                                        if (CheckValidatePaymentActive(billingService, r.RealReceiptId, false) == true)
                                        {
                                            r.CancelActive = "1";
                                            if (r.GAmount > 0)
                                            {
                                                r.Active = "0";
                                            }
                                            else
                                            {
                                                r.Active = "1";
                                            }
                                        }
                                    }

                                    if ((r.RepayActive == null ? "1" : r.RepayActive) == "0")
                                    {
                                        List<LastReceiptPayment> lrp = new List<LastReceiptPayment>();
                                        lrp = CheckValidateRepayActive(billingService, r.RealReceiptId);

                                        if (lrp.Count > 0)
                                        {
                                            r.GAmount = lrp[0].GAmount + lrp[0].AdjAmount;
                                            r.AdjAmount = lrp[0].AdjAmount;
                                            r.Amount = lrp[0].GAmount;
                                            r.PaidCashAmount = (lrp[0].CashAmount == null) ? 0 : lrp[0].CashAmount;
                                            r.PaidChqAmount = (lrp[0].ChqAmount == null) ? 0 : lrp[0].ChqAmount;
                                            r.PaidDepositAmount = (lrp[0].TransAmount == null) ? 0 : lrp[0].TransAmount;
                                            r.RepayActive = "1";
                                            r.Active = "1";
                                        }
                                    }

                                    r.ValidateFlag = "1";
                                    isSave = true;
                                }
                            }
                        }
                    }


                    //Save to text file
                    if (isSave)
                    {
                        IFormatter serializer = new BinaryFormatter();
                        using (Stream writer = new FileStream(todayPath, FileMode.Create))
                        {
                            serializer.Serialize(writer, report);
                            writer.Close();
                        }
                    }
                }
            }
        }

        public bool CheckValidatePaymentActive(IBillingService billingService, string receiptId, bool isPayment)
        {
            _billingService = billingService;

            return _billingService.ValidatePaymentActive(receiptId, isPayment);
        }

        public List<LastReceiptPayment> CheckValidateRepayActive(IBillingService billingService, string receiptId)
        {
            _billingService = billingService;

            return _billingService.GetRepayLastReceiptData(receiptId);
        }

        public void CheckReceiptRunning(bool isOnline)
        {
            IDSettingHelper hp = IDSettingHelper.Instance();

            string transactionPath = BPMPath.ConfigPath + "\\TransactionData";
            string posId = Session.Terminal.Code.ToString().Split('-')[1];
            string todayPath = string.Format(transactionPath + "\\{0}-{1}-{2}.txt", Session.Terminal.Code.ToString().Split('-')[0], Session.BpmDateTime.Now.ToString("yyyyMMdd", new CultureInfo("th-TH")), posId);
            bool isSave = false;

            if (!Directory.Exists(transactionPath))
            {
                Directory.CreateDirectory(transactionPath);
            }

            List<CAC15Report> report = new List<CAC15Report>();

            if (File.Exists(todayPath))
            {
                //Read existing transactions from text file
                FileStream fs = new FileStream(todayPath, FileMode.Open);
                IFormatter formatter = new BinaryFormatter();
                report = (List<CAC15Report>)formatter.Deserialize(fs);
                fs.Close();
            }


            if (report.Count > 0)
            { 
                report.Sort(delegate(CAC15Report r1, CAC15Report r2) { return r1.RealReceiptId.CompareTo(r2.RealReceiptId); });
                    
                CAC15Report rp = report.Find(delegate(CAC15Report c)
                                                 {
                                                     return c.ValidateFlag == "1"
                                                            && ((c.PaymentActive == null) ? "1" : c.PaymentActive) == "1"
                                                            && ((c.CancelActive == null) ? "1" : c.CancelActive) == "1"
                                                            && ((c.RepayActive == null) ? "1" : c.RepayActive) == "1"
                                                            && c.RealReceiptId.Substring(1,5) == posId;
                                                 }
                    );

                if (rp != null)
                {
                    // ตัวแปรสำหรับเก็บข้อมูล Running แยกตามแต่บะ prefix เพื่อเปรียยเทียบค่า MAX. 
                    Dictionary<string, int> maxRunningByPrefix = new Dictionary<string, int>(); 
                    int currentRunningByPrefix = 0 ;
                    bool resultCurrentRunning = false;  

                    foreach (CAC15Report re in report)
                    {
                        string name ;

                        if (re.RealReceiptId.Substring(0, 1) == "X")
                            name = string.Format("R-{0}", re.ReceiptId.Substring(0, 1)); // text file 2.17 start RealReceiptId เริ่มต้นด้วย 'X' ต้องใช้ Receipt 
                        else
                            name= string.Format("R-{0}", re.RealReceiptId.Substring(0, 1));
                        
                        Int32 runningNumber = 0;

                        if (re.ValidateFlag == "1"
                            && ((re.PaymentActive == null) ? "1" : re.PaymentActive) == "1"
                            && ((re.CancelActive == null) ? "1" : re.CancelActive) == "1"
                            && ((re.RepayActive == null) ? "1" : re.RepayActive) == "1"
                            && re.RealReceiptId.Substring(1, 5) == posId)
                        {
                            if (re.RealReceiptId.Substring(0, 1) == "X")
                                runningNumber = Convert.ToInt32(re.ReceiptId.Substring(12, 4));
                            else
                                runningNumber = Convert.ToInt32(re.RealReceiptId.Substring(12, 4));

                            Running r = new Running(Session.BpmDateTime.Now.Date, runningNumber);

                            object o = hp.Read(name, hp);
                            if (o == null)
                            {
                                hp.Add(name, r);

                                // Add to maxRunningByPrefix
                                maxRunningByPrefix.Add(name, runningNumber);
                            }
                            else
                            {
                                // Check current running by prefix name,
                                resultCurrentRunning = maxRunningByPrefix.TryGetValue(name, out currentRunningByPrefix);
                                if (resultCurrentRunning)
                                {
                                    // เปรียบเทียบ max value ก่อนทำการ update
                                    if (currentRunningByPrefix < runningNumber) {
                                        hp.Update(name, r);
                                        maxRunningByPrefix[name] = runningNumber;
                                    }
                                }
                                else
                                {
                                    hp.Update(name, r);

                                    maxRunningByPrefix.Add(name, runningNumber);
                                }

                            }

                            // 20210927 : Support รวมใบเสร็จแผนผ่อน. 
                            //&& (re.RealReceiptId == re.InstallmentReceiptId)
                            if (re.InstallmentReceiptId != null  && re.InstallmentReceiptId.StartsWith("X"))
                            {
                                //name = string.Format("R-{0}", re.ReceiptId.Substring(0, 1));
                                name = string.Format("R-{0}", re.InstallmentReceiptId.Substring(0, 1));

                                // Update receiptId to hp เพื่อให้มีการ update 
                                runningNumber = Convert.ToInt32(re.InstallmentReceiptId.Substring(12, 4));

                                Running rn = new Running(Session.BpmDateTime.Now.Date, runningNumber);

                                object obj = hp.Read(name, hp);
                                if (o == null)
                                {
                                    hp.Add(name, rn);

                                    // Add to maxRunningByPrefix
                                    maxRunningByPrefix.Add(name, runningNumber);
                                }
                                else
                                {
                                    // Check current running by prefix name,
                                    resultCurrentRunning = maxRunningByPrefix.TryGetValue(name, out currentRunningByPrefix);
                                    if (resultCurrentRunning)
                                    {
                                        // เปรียบเทียบ max value ก่อนทำการ update
                                        if (currentRunningByPrefix < runningNumber)
                                        {
                                            hp.Update(name, rn);
                                            maxRunningByPrefix[name] = runningNumber;
                                        }
                                    }
                                    else
                                    {
                                        hp.Update(name, rn);

                                        maxRunningByPrefix.Add(name, runningNumber);
                                    }
                                }
                            }


                            isSave = true;
                        }
                    }
                }
                else
                {
                    for (int i = report.Count - 1; i >= 0; i--)
                    {
                        if (report[i].RealReceiptId.Substring(1, 5) == posId)
                        {
                            string name = string.Format("R-{0}", report[i].RealReceiptId.Substring(0, 1));
                            Int32 runningNumber = 0;

                            runningNumber = Convert.ToInt32(report[i].RealReceiptId.Substring(12, 4)) - 1;

                            Running r = new Running(Session.BpmDateTime.Now.Date, runningNumber);

                            object o = hp.Read(name, hp);
                            if (o == null)
                            {
                                hp.Add(name, r);
                            }
                            else
                            {
                                hp.Update(name, r);
                            }
                            isSave = true;
                        }
                    }
                }

                if (isSave)
                {
                    hp.Save(hp);
                }
            }
        }
        
    }
}

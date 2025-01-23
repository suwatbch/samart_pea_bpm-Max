using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.ePaymentsModule.Utilities
{
    [Serializable]
    public class Running
    {
        private DateTime _date;
        private int _lastNumber;

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public int LastNumber
        {
            get { return _lastNumber; }
            set { _lastNumber = value; }
        }

        private Running(DateTime date, int lastNumber)
        {
            this._date = date;
            this._lastNumber = lastNumber;
        }

        public string NextRunningNumber(string recepitType)
        {
            _lastNumber = _lastNumber + 1;
            if (recepitType == "1")
            {
                return string.Format("{0}{1}", _date.ToString("yyMMdd"), _lastNumber.ToString("0000"));
            }
            else
            {
                return string.Format("{0}{1}", _date.ToString("yyMMdd"), _lastNumber.ToString("00000000"));
            }
        }


        public static string GetReceiptId(string prefix, string receiptType)
        {
            string runningNo = "";

            string name = string.Format("R-{0}", prefix);

            LocalSettingHelper hp = LocalSettingHelper.Instance();
            object o = hp.Read(name);
            if (o == null)
            {
                Running r = new Running(DateTime.Today, 0);
                runningNo = r.NextRunningNumber(receiptType);
                hp.Add(name, r);
            }
            else
            {
                Running r = (Running)o;
                if (!r.Date.Equals(DateTime.Today))
                {
                    r.Date = DateTime.Today;
                    r.LastNumber = 0;
                }
                runningNo = r.NextRunningNumber(receiptType);
                hp.Update(name, r);
            }

            if (receiptType == "1")
                return string.Format("{0}{1}{2}", prefix, Session.Terminal.Id, runningNo);
            else 
                return string.Format("{0}{1}", prefix, runningNo);
        }
    }
}


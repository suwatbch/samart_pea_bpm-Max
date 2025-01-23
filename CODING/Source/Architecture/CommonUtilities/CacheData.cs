using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Architecture.CommonUtilities
{
    [Serializable]
    public class CacheData<T>
    {
        private DateTime _date;
        private T _value;

        public CacheData(T value, DateTime now)
        {
            _value = value;
            _date = now;
        }

        public bool IsExpired(DateTime now, int noOfDays)
        {
            TimeSpan ts = now.Subtract(_date);
            return ts.TotalDays > noOfDays;
        }

        public T Value
        {
            get
            {
                return _value;
            }
        }
    }
}

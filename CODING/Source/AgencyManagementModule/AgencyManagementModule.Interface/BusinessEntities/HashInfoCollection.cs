using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class HashInfoCollection
    {
        List<HashInfo> _list = new List<HashInfo>();


        [DataMember(Order = 1)]
        public List<HashInfo> HashList
        {
            get { return this._list; }
            set { this._list = value; }
        }

        public void Add(HashInfo hValue)
        {
            if (_list == null)
                _list = new List<HashInfo>();
            _list.Add(hValue);
        }

        public void Remove(HashInfo hValue)
        {
            if (_list.Contains(hValue))
            {
                _list.Remove(hValue);
            }
        }

        public string GetHashValueById(string Id)
        {
            string retVal = String.Empty;
            foreach (HashInfo h in _list)
            {
                if (h.Id == Id)
                {
                    retVal = h.Value;
                    break;
                }
            }
            return retVal;
        }

        public string GetHashIdByValue(string Value)
        {
            string retVal = String.Empty;
            foreach (HashInfo h in _list)
            {
                if (h.Value == Value)
                {
                    retVal = h.Id;
                    break;
                }
            }
            return retVal;
        }

        //Checked TongKung
        public int Count
        {
            get
            {
                if (_list == null)
                    return 0;
                else
                {
                    return _list.Count;
                }
            }
        }

    }
}

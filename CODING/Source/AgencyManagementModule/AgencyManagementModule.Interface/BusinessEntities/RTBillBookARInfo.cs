using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    class RTBillBookARInfo
    {
        string _billBookId;
        string _itemId;


        [DataMember(Order=1)]
        public string BillBookId
        {
            get { return this._billBookId; }
            set { this._billBookId = value; }
        }


        [DataMember(Order=2)]
        public string ItemId
        {
            get { return this._itemId; }
            set { this._itemId = value; } 
        }

    }
}

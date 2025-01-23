using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Infrastructure.Interface.BusinessEntities
{
    [DataContract]
    public class UnlockRemark: IListItem
    {
        private string _fncId;

        [DataMember(Order=1)]
        public string FncId
        {
            get { return _fncId; }
            set { _fncId = value; }
        }

        private string _remarkId;

        [DataMember(Order=2)]
        public string RemarkId
        {
            get { return _remarkId; }
            set { _remarkId = value; }
        }

        private string _description;

        [DataMember(Order=3)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }


        public UnlockRemark(string fncId, string remarkId, string description)
        {
            this._fncId = fncId;
            this._remarkId = remarkId;
            this._description = description;
        }

        #region IListItem Members


        //[DataMember(Order=4)]
        public string DisplayText
        {
            get { return _description; }
        }

        #endregion
    }
}

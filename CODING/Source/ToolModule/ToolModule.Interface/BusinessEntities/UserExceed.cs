using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ToolModule.Interface.BusinessEntities
{
    #region #Issue User

    [DataContract]
    public class UserExceed
    {
        private bool    _IsExceed;
        private int     _UserLimit;
        private int     _UserCurrentUsed;

        [DataMember(Order=1)]
        public bool IsExceed
        {
            get { return _IsExceed; }
            set { _IsExceed = value; }
        }

        [DataMember(Order = 2)]
        public int UserLimit
        {
            get { return _UserLimit; }
            set { _UserLimit = value; }
        }

        [DataMember(Order = 3)]
        public int UserCurrentUsed
        {
            get { return _UserCurrentUsed; }
            set { _UserCurrentUsed = value; }
        }


    }

    #endregion
}

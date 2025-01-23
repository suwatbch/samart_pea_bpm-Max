using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities
{
    [DataContract, Serializable]
    public class Function : IComparable<Function>
    {
        private string _id;
        private bool _authorized;


        [DataMember(Order=1)]
        public bool Authorized
        {
            get { return _authorized; }
            set { _authorized = value; }
        }


        [DataMember(Order=2)]
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private bool _unlockable;


        [DataMember(Order=3)]
        public bool Unlockable
        {
            get { return _unlockable; }
            set { _unlockable = value; }
        }
        private bool _unlockRemark;


        [DataMember(Order=4)]
        public bool UnlockRemark
        {
            get { return _unlockRemark; }
            set { _unlockRemark = value; }
        }

        public Function()
        {
        }

        #region IComparable<Function> Members

        public int CompareTo(Function other)
        {
            return _id.CompareTo(other._id);
        }

        #endregion
    }
}

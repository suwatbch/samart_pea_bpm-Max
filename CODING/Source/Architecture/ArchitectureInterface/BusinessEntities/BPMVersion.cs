using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Constants;

namespace PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities
{
    [DataContract]
    public class BPMVersion
    {
        private string _productVersion;
        private string _productName;
        private string _companyName;
        private string _productCopyRight;
        private string _posdatabase;

        [DataMember(Order=1)]
        public string Version
        {
            get { return _productVersion; }
            set { _productVersion = value; }
        }

        [DataMember(Order=2)]
        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }

        [DataMember(Order=3)]
        public string Company
        {
            get { return _companyName; }
            set { _companyName = value; }
        }

        [DataMember(Order=4)]
        public string CopyRight
        {
            get { return _productCopyRight; }
            set { _productCopyRight = value; }
        }

        [DataMember(Order = 5)]
        public string POSDatabase
        {
            get { return _posdatabase; }
            set { _posdatabase = value; }
        }

    }
}

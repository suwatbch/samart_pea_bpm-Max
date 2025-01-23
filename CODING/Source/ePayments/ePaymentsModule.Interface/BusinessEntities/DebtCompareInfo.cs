using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities
{
    [DataContract]
    public class DebtCompareInfo
    {
        string _title = String.Empty;
        string _bpmValue = String.Empty;
        string _fileValue = String.Empty;
        bool _formatStatus = false;

        public DebtCompareInfo()
        { 

        }

        public DebtCompareInfo(string title, string BPMValue, string fileValue, bool formatStatus)
        {
            this._formatStatus = formatStatus;
            this._title = title;
            this._bpmValue = BPMValue;
            this._fileValue = fileValue;
        }


        [DataMember(Order=1)]
        public string Title
        {
            get { return this._title; }
            set { this._title = value; }
        }
        

        [DataMember(Order=2)]
        public string BPMValue
        {
            get { return this._bpmValue; }
            set { this._bpmValue = value; }
        }


        [DataMember(Order=3)]
        public string FileValue
        {
            get { return this._fileValue; }
            set { this._fileValue = value; }
        }


        [DataMember(Order=4)]
        public bool FormatStatus
        {
            get { return this._formatStatus; }
            set { this._formatStatus = value; }
        }
    }

}

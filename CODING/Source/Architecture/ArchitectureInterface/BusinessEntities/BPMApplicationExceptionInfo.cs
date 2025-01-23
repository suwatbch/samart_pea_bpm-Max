using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities
{
    [DataContract]
    public class BPMApplicationExceptionInfo
    {
        private string _errorcode = "";
        private string _originaltype = "";
        private DateTime _occurwhen;
        private int _module;
        private int _layer;
        private string _debuggingid = "";

        private string _message = "";
        private string _stacktrace = "";
        private string _source = "";
        private bool _cancontinue = true;
        private List<SimpleExceptionInfo> _infolist = new List<SimpleExceptionInfo>();

        private string _userid = "NotLogin";
        private string _authentoken = "NoToken";

        private string _thmessage = "";
        private string _cause = "";
        private string _resolve = "";
        private string _helpurl = "";


        [DataMember(Order=1)]
        public string ErrorCode
        {
            get { return _errorcode; }
            set { _errorcode = value; }
        }

        [DataMember(Order=2)]
        public string OriginalType
        {
            get { return _originaltype; }
            set { _originaltype = value; }
        }

        [DataMember(Order=3)]
        public DateTime OccurWhen
        {
            get { return _occurwhen; }
            set { _occurwhen = value; }
        }

        [DataMember(Order=4)]
        public int Module
        {
            get { return _module; }
            set { _module = value; }
        }

        [DataMember(Order=5)]
        public int Layer
        {
            get { return _layer; }
            set { _layer = value; }
        }

        [DataMember(Order=6)]
        public string DebuggingId
        {
            get { return _debuggingid; }
            set { _debuggingid = value; }
        }


        [DataMember(Order=7)]
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        [DataMember(Order=8)]
        public string StackTrace
        {
            get { return _stacktrace; }
            set { _stacktrace = value; }
        }

        [DataMember(Order=9)]
        public string Source
        {
            get { return _source; }
            set { _source = value ?? string.Empty; }
        }

        [DataMember(Order=10)]
        public bool CanContinue
        {
            get { return _cancontinue; }
            set { _cancontinue = value; }
        }

        [DataMember(Order=11)]
        public string UserId
        {
            get { return _userid; }
            set { _userid = value; }
        }

        [DataMember(Order=12)]
        public string AuthToken
        {
            get { return _authentoken; }
            set { _authentoken = value; }
        }


        [DataMember(Order=13)]
        public List<SimpleExceptionInfo> AdditionalInfo
        {
            get { return _infolist; }
            set { _infolist = value; }
        }


        [DataMember(Order=14)]
        public string THMessage
        {
            get { return _thmessage; }
            set { _thmessage = value; }
        }

        [DataMember(Order=15)]
        public string Cause
        {
            get { return _cause; }
            set { _cause = value; }
        }

        [DataMember(Order=16)]
        public string Resolve
        {
            get { return _resolve; }
            set { _resolve = value; }
        }

        [DataMember(Order=17)]
        public string HelpURL
        {
            get { return _helpurl; }
            set { _helpurl = value; }
        }

    }
}

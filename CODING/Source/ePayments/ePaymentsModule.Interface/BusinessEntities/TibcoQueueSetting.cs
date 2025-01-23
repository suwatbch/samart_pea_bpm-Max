using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BPMOnlineCp.Interface.BusinessEntities
{
    [DataContract]
    public class TibcoQueueSetting
    {
        private string _ConIP;
        private string _ConPassword;
        private string _ConPort;
        private string _ConQueueName;
        private string _ConServerUrl;
        private string _ConUserName;
        private int _ConLimitConnection;

        private double _SleepBIncStep;
        private double _SleepBInit;
        private double _SleepCurrentTime;
        private double _SleepMIncStep;
        private double _SleepMInit;
        private int _SleepQueueLimit;
        private double _SleepTimeInit;
        private double _SleepXIncStep;
        private double _SleepXInit;
        private double _SleepYInit;
        private string _Active;

        [DataMember(Order = 1)]
        public string ConIP
        {
            get { return this._ConIP; }
            set { this._ConIP = value; }
        }

        [DataMember(Order = 2)]
        public string ConPassword
        {
            get { return this._ConPassword; }
            set { this._ConPassword = value; }
        }

        [DataMember(Order = 3)]
        public string ConPort
        {
            get { return this._ConPort; }
            set { this._ConPort = value; }
        }

        [DataMember(Order = 4)]
        public string ConQueueName
        {
            get { return this._ConQueueName; }
            set { this._ConQueueName = value; }
        }

        [DataMember(Order = 5)]
        public string ConServerUrl
        {
            get { return this._ConServerUrl; }
            set { this._ConServerUrl = value; }
        }

        [DataMember(Order = 6)]
        public string ConUserName
        {
            get { return this._ConUserName; }
            set { this._ConUserName = value; }
        }

        [DataMember(Order = 7)]
        public double SleepBIncStep
        {
            get { return this._SleepBIncStep; }
            set { this._SleepBIncStep = value; }
        }

        [DataMember(Order = 8)]
        public double SleepBInit
        {
            get { return this._SleepBInit; }
            set { this._SleepBInit = value; }
        }

        [DataMember(Order = 9)]
        public double SleepCurrentTime
        {
            get { return this._SleepCurrentTime; }
            set { this._SleepCurrentTime = value; }
        }

        [DataMember(Order = 10)]
        public double SleepMIncStep
        {
            get { return this._SleepMIncStep; }
            set { this._SleepMIncStep = value; }
        }

        [DataMember(Order = 11)]
        public double SleepMInit
        {
            get { return this._SleepMInit; }
            set { this._SleepMInit = value; }
        }

        [DataMember(Order = 12)]
        public int SleepQueueLimit
        {
            get { return this._SleepQueueLimit; }
            set { this._SleepQueueLimit = value; }
        }

        [DataMember(Order = 13)]
        public double SleepTimeInit
        {
            get { return this._SleepTimeInit; }
            set { this._SleepTimeInit = value; }
        }

        [DataMember(Order = 14)]
        public double SleepXIncStep
        {
            get { return this._SleepXIncStep; }
            set { this._SleepXIncStep = value; }
        }

        [DataMember(Order = 15)]
        public double SleepXInit
        {
            get { return this._SleepXInit; }
            set { this._SleepXInit = value; }
        }

        [DataMember(Order = 16)]
        public double SleepYInit
        {
            get { return this._SleepYInit; }
            set { this._SleepYInit = value; }
        }

        
        [DataMember(Order = 17)]
        public int ConLimitConnection
        {
            get { return this._ConLimitConnection; }
            set { this._ConLimitConnection = value; }
        }

        [DataMember(Order = 18)]
        public string Active
        {
            get { return this._Active; }
            set { this._Active = value; }
        }        
    }

    [DataContract]
    public class QuotaCallResult
    {
        private string _IsOverConLimit;
        private int _OverValue;

        [DataMember(Order = 1)]
        public string IsOverConLimit
        {
            get { return this._IsOverConLimit; }
            set { this._IsOverConLimit = value; }
        }
        [DataMember(Order = 2)]
        public int OverValue
        {
            get { return this._OverValue; }
            set { this._OverValue = value; }
        }

    }
}

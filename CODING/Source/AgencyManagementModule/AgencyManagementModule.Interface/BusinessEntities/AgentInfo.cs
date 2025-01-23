using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.AgencyManagementModule.Interface.Constants;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class AgentInfo
    {        

        //use when parent form call to search agency only
        private int _sendType = 0;
        private string _id;
        private string _name;
        private string _type;
        private bool _isAgency;
        private decimal? _deposit = 0;
        private decimal? _useDeposit = 0;
        private string _address;
        private string _contact;
        private string _techBranchId;
        private int _travelHelp;
        private int _bookHolderState = (int)BookHolderState.Agent;
        private bool _penaltyWaiveFlag;
        private DateTime? _contractValidTo;
        private DateTime? _contractValidFrom;
        private int _receiveCount = 0;
        private bool _isPersonalBpType = true;
        private int _bookLot = 0;

        public AgentInfo Clone()
        {
            AgentInfo that = new AgentInfo();
            that.SendType = this.SendType;
            that.Id = this.Id;
            that.Name = this.Name;
            that.Type = this.Type;
            that.IsAgency = this.IsAgency;
            that.Deposit = this.Deposit;
            that.UseDeposit = this.UseDeposit;
            that.Address = this.Address;
            that.Contact = this.Contact;
            that.TechBranchId = this.TechBranchId;
            that.TravelHelp = this.TravelHelp;
            that.BookHolder = this.BookHolder;
            that.PenaltyWaiveFlag = this.PenaltyWaiveFlag;
            that.ContractValidFrom = this.ContractValidFrom;
            that.ContractValidTo = this.ContractValidTo;
            return that;
        }


        [DataMember(Order=1)]
        public int SendType
        {
            get { return this._sendType; }
            set { this._sendType = value; }
        }


        [DataMember(Order=2)]
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }


        [DataMember(Order=3)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        [DataMember(Order=4)]
        public bool IsAgency
        {
            get { return _isAgency; }
            set { _isAgency = value; }
        }


        [DataMember(Order=5)]
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }


        [DataMember(Order=6)]
        public decimal? Deposit
        {
            get { return _deposit; }
            set { _deposit = value; }
        }


        [DataMember(Order=7)]
        public decimal? UseDeposit
        {
            get { return _useDeposit; }
            set { _useDeposit = value; }
        }


        [DataMember(Order=8)]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }


        [DataMember(Order=9)]
        public string Contact
        {
            get { return _contact; }
            set { _contact = value; }
        }


        [DataMember(Order=10)]
        public string TechBranchId
        {
            get { return _techBranchId; }
            set { _techBranchId = value; }
        }


        [DataMember(Order=11)]
        public int TravelHelp
        {
            get { return _travelHelp; }
            set { _travelHelp = value; }
        }
       


        [DataMember(Order=12)]
        public int BookHolder
        {
            get { return _bookHolderState; }
            set { _bookHolderState = value; }
        }


        [DataMember(Order=13)]
        public bool PenaltyWaiveFlag
        {
            get { return _penaltyWaiveFlag; }
            set { _penaltyWaiveFlag = value; }
        }


        [DataMember(Order=14)]
        public DateTime? ContractValidTo
        {
            get { return this._contractValidTo; }
            set { this._contractValidTo = value; }
        }

        [DataMember(Order=15)]
        public DateTime? ContractValidFrom
        {
            get { return this._contractValidFrom; }
            set { this._contractValidFrom = value; }
        }

        //Checked TongKung
        public string AgencyStatus
        {
            get
            {
                string retVal;
                if (ContractValidFrom == null || _contractValidTo == null)
                {
                    retVal = "ไม่ระบุ";
                }
                else if ((ContractValidFrom > Session.BpmDateTime.Now) || (_contractValidTo < Session.BpmDateTime.Now))
                {
                    retVal = "ยกเลิก";
                }
                else
                {
                    retVal = "ปกติ";
                }
                return retVal;
            }
        }


        [DataMember(Order=16)]
        public int ReceiveCount
        {
            get { return this._receiveCount; }
            set { this._receiveCount = value; }
        }


        [DataMember(Order=17)]
        public int BookLot
        {
            get { return this._bookLot; }
            set { this._bookLot = value; }
        }


        [DataMember(Order=18)]
        public bool IsPersonalBpType
        {
            get { return this._isPersonalBpType; }
            set { this._isPersonalBpType = value; }
        }
    }

    
}

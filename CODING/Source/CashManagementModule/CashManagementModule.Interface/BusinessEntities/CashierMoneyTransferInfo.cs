using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CashierMoneyTransferInfo
    {
        string _noOfChq;

        [DataMember(Order=1)]
        public string NoOfChq
        {
            get { return _noOfChq; }
            set { _noOfChq = value; }
        }

        bool _chosen = false;

        [DataMember(Order=2)]
        public bool Chosen
        {
            get { return _chosen; }
            set { _chosen = value; }
        }

        string _transferId;


        [DataMember(Order=3)]
        public string TransferId
        {
            get { return _transferId; }
            set { _transferId = value; }
        }
        string _sender;


        [DataMember(Order=4)]
        public string Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }

        string _senderName;


        [DataMember(Order=5)]
        public string SenderName
        {
            get { return _senderName; }
            set { _senderName = value; }
        }


        string _receiver;
        

        [DataMember(Order=6)]
        public string Receiver
        {
            get { return _receiver; }
            set { _receiver = value; }
        }
        string _status;
        

        [DataMember(Order=7)]
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
        DateTime? _requestDt;


        [DataMember(Order=8)]
        public DateTime? RequestDt
        {
            get { return _requestDt; }
            set { _requestDt = value; }
        }
        string _requestPosId;


        [DataMember(Order=9)]
        public string RequestPosId
        {
            get { return _requestPosId; }
            set { _requestPosId = value; }
        }
        DateTime? _responseDt;


        [DataMember(Order=10)]
        public DateTime? ResponseDt
        {
            get { return _responseDt; }
            set { _responseDt = value; }
        }
        string _responsePosId;


        [DataMember(Order=11)]
        public string ResponsePosId
        {
            get { return _responsePosId; }
            set { _responsePosId = value; }
        }
        decimal? _cashAmt;


        [DataMember(Order=12)]
        public decimal? CashAmt
        {
            get { return _cashAmt; }
            set { _cashAmt = value; }
        }
        decimal? _chequeAmt;


        [DataMember(Order=13)]
        public decimal? ChequeAmt
        {
            get { return _chequeAmt; }
            set { _chequeAmt = value; }
        }
        string _branchId;


        [DataMember(Order=14)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }
        string _syncFlag;


        [DataMember(Order=15)]
        public string SyncFlag
        {
            get { return _syncFlag; }
            set { _syncFlag = value; }
        }
        string _modifiedBy;


        [DataMember(Order=16)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }
        DateTime? _modifiedDt;


        [DataMember(Order=17)]
        public DateTime? ModifiedDt
        {
            get { return _modifiedDt; }
            set { _modifiedDt = value; }
        }
        string _active;


        [DataMember(Order=18)]
        public string Active
        {
            get { return _active; }
            set { _active = value; }
        }
        decimal? _amount;


        [DataMember(Order=19)]
        public decimal? Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        string _description;


        [DataMember(Order=20)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        string _fullName;


        [DataMember(Order=21)]
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        string _dispStatus;

        [DataMember(Order=22)]
        public string DispStatus
        {
            get { return _dispStatus; }
            set { _dispStatus = value; }
        }


    }
}

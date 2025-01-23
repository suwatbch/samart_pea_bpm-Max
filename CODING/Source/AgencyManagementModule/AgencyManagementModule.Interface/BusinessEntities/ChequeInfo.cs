using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;


namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class ChequeInfo
    {
        #region "Private variable"
        private string _caId;
        private string _BookId;
        private string _period;
        private string _bankKey;
        private string _bankName;
        private string _chequeNo;
        private decimal? _chequeAmount = 0;
        private decimal? _actualAmount = 0;
        private string _chequeAccountNo;
        private DateTime? _chequeDt;
        #endregion

        #region "Public properties"

        [DataMember(Order=1)]
        public string CaId
        {
            get
            {
                return this._caId;
            }
            set
            {
                this._caId = value;
            }
        }

        [DataMember(Order=2)]
        public string BookId
        {
            get 
            {
                return this._BookId;
            }
            set 
            {
                this._BookId = value;
            }
        }


        [DataMember(Order=3)]
        public string Period
        {
            get
            {
                return this._period;
            }
            set 
            {
                this._period = value;
            }
        }

        [DataMember(Order=4)]
        public string BankKey
        {
            get
            {
                return this._bankKey;
            }
            set
            {
                this._bankKey = value;
            }
        }


        [DataMember(Order=5)]
        public string BankName
        {
            get 
            {
                return this._bankName;
            }
            set 
            {
                this._bankName = value;
            }
        }

        [DataMember(Order=6)]
        public string ChequeNo
        {
            get
            {
                return this._chequeNo;
            }
            set
            {
                this._chequeNo = value;
            }
        }

        [DataMember(Order=7)]
        public decimal? ChequeAmount
        {
            get 
            {
                return this._chequeAmount;
            }
            set 
            {
                this._chequeAmount = value;
            }
        }


        [DataMember(Order=8)]
        public decimal? ActualAmount
        {
            get 
            {
                return this._actualAmount;
            }
            set 
            {
                this._actualAmount = value;
            }
        }


        [DataMember(Order=9)]
        public string ChequeAccountNo
        {
            get 
            {
                return this._chequeAccountNo;
            }
            set 
            {
                this._chequeAccountNo = value;
            }
        }

        [DataMember(Order=10)]
        public DateTime? ChequeDt
        {
            get 
            {
                return this._chequeDt;
            }
            set
            {
                this._chequeDt = value;
            }
        }
        #endregion
    }
}

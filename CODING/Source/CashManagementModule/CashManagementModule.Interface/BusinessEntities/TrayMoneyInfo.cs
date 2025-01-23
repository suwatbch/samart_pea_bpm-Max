using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class TrayMoneyInfo
    {
        private decimal? _cashAmount;
        private decimal? _cashPendingAmount;
        private BindingList<ChequeInfo> _chequeList = new BindingList<ChequeInfo>();
        private BindingList<PayInInfo> _payInList = new BindingList<PayInInfo>();

        //special used
        private decimal? _chequeAmount;
        private decimal? _chequePendingAmount;


        [DataMember(Order=1)]
        public decimal? ChequeAmount
        {
            set { _chequeAmount = value; }
            //get { return _chequeAmount; }
            get
            {
                decimal? chqAmt = 0;
                foreach (ChequeInfo chq in _chequeList)
                    chqAmt += chq.Amount.Value;
                return chqAmt;
            }
        }


        [DataMember(Order=2)]
        public decimal? ChequePendingAmount
        {
            set { _chequePendingAmount = value; }
            get { return _chequePendingAmount; }
        }


        [DataMember(Order=3)]
        public decimal? CashAmount
        {
            set { _cashAmount = value; }
            get { return _cashAmount; }
        }


        [DataMember(Order=4)]
        public decimal? CashPendingAmount
        {
            set { _cashPendingAmount = value; }
            get { return _cashPendingAmount; }
        }


        [DataMember(Order=5)]
        public BindingList<ChequeInfo> ChequeList
        {
            set { _chequeList = value; }
            get { return _chequeList; }
        }

        [DataMember(Order = 6)]
        public BindingList<PayInInfo> PayInList
        {
            set { _payInList = value; }
            get { return _payInList; }
        }

        public void CopyFrom(TrayMoneyInfo source)
        {
            this.CashAmount = source.CashAmount;
            this.CashPendingAmount = source.CashPendingAmount;
            this.ChequeAmount = source.ChequeAmount;
            this.ChequePendingAmount = source.CashAmount;
            foreach (ChequeInfo item in source.ChequeList)
            {
                this.ChequeList.Add(item);
            }
        }

        public TrayMoneyInfo ToNewObject()
        {
            TrayMoneyInfo trayMoneyInfo_temp = new TrayMoneyInfo();
            trayMoneyInfo_temp.CopyFrom(this);
            return trayMoneyInfo_temp;
        }

    }
}

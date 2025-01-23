using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class SortByBankKeyAces : IComparer<ChequeInfo>
    {

        #region IComparer<ChequeInfo> Members

        public int Compare(ChequeInfo x, ChequeInfo y)
        {
            return string.Compare(x.BankKey, y.BankKey);
        }

        #endregion
    }

    [DataContract]
    public class SortByBankKeyDesc : IComparer<ChequeInfo>
    {

        #region IComparer<ChequeInfo> Members

        public int Compare(ChequeInfo x, ChequeInfo y)
        {
            return string.Compare(y.BankKey, x.BankKey);
        }

        #endregion
    }

    [DataContract]
    public class SortByChqNoAces : IComparer<ChequeInfo>
    {

        #region IComparer<ChequeInfo> Members

        public int Compare(ChequeInfo x, ChequeInfo y)
        {
            return string.Compare(x.ChqNo, y.ChqNo);
        }

        #endregion
    }

    [DataContract]
    public class SortByChqNoDesc : IComparer<ChequeInfo>
    {


        #region IComparer<ChequeInfo> Members

        public int Compare(ChequeInfo x, ChequeInfo y)
        {
            return string.Compare(y.ChqNo, x.ChqNo);
        }

        #endregion
    }

    [DataContract]
    public class SortByChqAmtDesc : IComparer<ChequeInfo>
    {
        #region IComparer<ChequeInfo> Members

        public int Compare(ChequeInfo x, ChequeInfo y)
        {
            return decimal.Compare(y.Amount.Value, x.Amount.Value);
        }

        #endregion
    }


    [DataContract]
    public class SortByChqAmtAces : IComparer<ChequeInfo>
    {
        #region IComparer<ChequeInfo> Members

        public int Compare(ChequeInfo x, ChequeInfo y)
        {
            return decimal.Compare(x.Amount.Value, y.Amount.Value);
        }

        #endregion
    }
    




}

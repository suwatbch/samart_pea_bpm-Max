using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;

namespace PEA.BPM.PaymentManagementModule
{
    [Serializable]
    public class ToBePaidAP: APInfo
    {
        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set { _isChecked = value; }
        }

        public ToBePaidAP() { }

        public ToBePaidAP(APInfo ap)
        {
            this.PaymentVoucher = ap.PaymentVoucher;
            this.CaId = ap.CaId;
            this.CaName = ap.CaName;
            this.GAmount = ap.GAmount;
            this.IsChecked = true;
        }

        public APInfo ToAP()
        {
            APInfo ap = new APInfo();
            ap.PaymentVoucher = this.PaymentVoucher;
            ap.CaId = this.CaId;
            ap.CaName = this.CaName;
            ap.GAmount = this.GAmount;

            return ap;
        }

    }
}

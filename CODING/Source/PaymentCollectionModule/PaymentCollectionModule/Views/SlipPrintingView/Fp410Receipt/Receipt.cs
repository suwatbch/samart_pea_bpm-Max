using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Views.SlipPrintingView.Fp410Receipt
{
    public class Receipt
    {
        private string _body;

        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }
    }
}

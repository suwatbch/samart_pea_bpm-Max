using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.ReportViews.CAC19CriteriaView
{
    public interface ICAC19CriteriaView
    {
        void OnWaitCursor(bool set);
    }
}

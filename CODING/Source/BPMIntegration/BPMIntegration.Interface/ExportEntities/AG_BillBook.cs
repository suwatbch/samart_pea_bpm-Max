using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;

namespace PEA.BPM.Integration.BPMIntegration.Interface.ExportEntities
{
    public class AG_BillBook 
    {
        string _billBookId;

        public string BillBookId
        {
            get { return this._billBookId; }
            set { this._billBookId = value; }
        }

    }
}

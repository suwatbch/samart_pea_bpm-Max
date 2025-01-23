using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [Serializable]
    public class ReverseHistoryInfo
    {
        private string _printDoc;
        public string PrintDoc
        {
            get { return _printDoc; }
            set { _printDoc = value; }
        }

        private string _w_1970_print_dt;
        public string W_1970_print_dt
        {
            get { return _w_1970_print_dt; }
            set { _w_1970_print_dt = value; }
        }

        private string _w_1910_org_doc;
        public string W_1910_org_doc
        {
            get { return _w_1910_org_doc; }
            set { _w_1910_org_doc = value; }
        }

        private string _sto_budat;
        public string Sto_budat
        {
            get { return _sto_budat; }
            set { _sto_budat = value; }
        }

        private string _intoopbel;
        public string Intoopbel
        {
            get { return _intoopbel; }
            set { _intoopbel = value; }
        }

        private string _icreason;
        public string Icreason
        {
            get { return _icreason; }
            set { _icreason = value; }
        }

        private string _stokz;
        public string Stokz
        {
            get { return _stokz; }
            set { _stokz = value; }
        }

        private DateTime? _modifiedDt;
        public DateTime? ModifiedDt
        {
            get { return _modifiedDt; }
            set { _modifiedDt = value; }
        }

        private string _modifiedBy;
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        private bool? _active;
        public bool? Active
        {
            get { return _active; }
            set { _active = value; }
        }

    }
}

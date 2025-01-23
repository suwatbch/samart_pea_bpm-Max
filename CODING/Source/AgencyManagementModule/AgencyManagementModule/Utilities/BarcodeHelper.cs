using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using PEA.BPM.AgencyManagementModule.Interface.Constants;

namespace PEA.BPM.AgencyManagementModule.Utilities
{
    public class BarcodeHelper
    {
        ICreateBillbookService _createBillBookService;        
        string _branchId;
        string _caId;
        string[] _mruId;
        string _period;

        public string BranchId
        {
 
            get { return this._branchId;}
            set { this._branchId = value;}
        }

        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }

        public string[] Mru
        {
            get { return this._mruId; }
            set { this._mruId = value; }
        }

        public BarcodeHelper(string barCode)
        {
            if (barCode.Length == ModuleConfigurationNames.CustomerBarCodeLength)
            {
                string branchCode = TextUtility.MapBranch(barCode.Substring(0, 3));
                string branchNo = barCode.Substring(3,5);
                string caId = barCode.Substring(8, 12);
                string period = barCode.Substring(20, 4);
                Period = String.Format("{0}/25{1}", period.Substring(0, 2), period.Substring(2, 2));

                //_createBillBookService = new CreateBillbookService();
                //BranchId = String.Format("{0}{1}", branchCode, branchNo);
                //CaId = caId;
                //Mru = _createBillBookService.GetMruByCaId(caId);

            }
        }

        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }

    }
}

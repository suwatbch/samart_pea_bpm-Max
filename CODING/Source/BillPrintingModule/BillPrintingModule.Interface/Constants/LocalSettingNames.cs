using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.Constants
{
    public class LocalSettingNames : PEA.BPM.Infrastructure.Interface.Constants.LocalSettingNames
    {
        public const string PrintTarget = "PrintTarget";
        public const string FilePrintTargetPath = "FilePrintTargetPath";
        public const string ToPersonText = "ToPersonText";
        public const int BankIdLength = 5;       

        //public const string Barcode1 = "Barcode1";
        //public const string Barcode2 = "Barcode2";
        //public const string BarcodeTxt1Mapping = "BarcodeTxt1Mapping";
        //public const string BarcodeTxt2Mapping = "BarcodeTxt2Mapping";
    }
}

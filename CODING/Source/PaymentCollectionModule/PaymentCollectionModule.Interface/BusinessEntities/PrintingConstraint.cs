using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class PrintingConstraint
    {
        [DataContract]
        public class PaperSize
        {

            [DataMember(Order = 1)]
            public const string Default = "D";

            [DataMember(Order = 2)]
            public const string PosSlip = "S";

            [DataMember(Order = 3)]
            public const string PrePrinted = "P";
        }

        private string _defaultPaperSize;

        [DataMember(Order = 4)]
        public string DefaultPaperSize
        {
            get { return _defaultPaperSize; }
            set { _defaultPaperSize = value; }
        }

        private List<string> _optionPaperSizes = new List<string>();

        [DataMember(Order = 5)]
        public List<string> OptionPaperSizes
        {
            get { return _optionPaperSizes; }
            set { _optionPaperSizes = value; }
        }

        public PrintingConstraint()
        {
        }

        public PrintingConstraint(string paperSize)
        {
            switch (paperSize)
            {
                case "1": // Pre-Printed
                    _defaultPaperSize = PaperSize.PrePrinted;
                    _optionPaperSizes.Add(PaperSize.PrePrinted);
                    break;
                case "2": // Slip
                    _defaultPaperSize = PaperSize.PosSlip;
                    _optionPaperSizes.Add(PaperSize.PosSlip);
                    break;
                case "3": // Pre-Printed, Option Slip
                    _defaultPaperSize = PaperSize.PrePrinted;
                    _optionPaperSizes.Add(PaperSize.PrePrinted);
                    _optionPaperSizes.Add(PaperSize.PosSlip);
                    break;
                case "4": // Slip, Option Pre-Printed
                    _defaultPaperSize = PaperSize.PosSlip;
                    _optionPaperSizes.Add(PaperSize.PrePrinted);
                    _optionPaperSizes.Add(PaperSize.PosSlip);
                    break;
                default:
                    break;
            }
        }
    }
}

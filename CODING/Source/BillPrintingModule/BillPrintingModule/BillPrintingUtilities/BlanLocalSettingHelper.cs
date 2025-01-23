using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.BillPrintingModule.Interface.Constants;

namespace PEA.BPM.BillPrintingModule.BillPrintingUtilities
{
    public class BlanLocalSettingHelper
    {
        public static void SaveToWhom(string toWhom)
        {
            string tmp = string.Empty;
            LocalSettingHelper hp = LocalSettingHelper.Instance();
            if (hp.Read(LocalSettingNames.ToPersonText) != null)
            {
                string[] txt = hp.Read(LocalSettingNames.ToPersonText).ToString().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (string.IsNullOrEmpty(Array.Find<string>(txt, delegate(string t) { return t == toWhom; })))
                {
                    if (txt.Length < 5)
                    {
                        tmp = hp.Read(LocalSettingNames.ToPersonText).ToString();
                    }
                    else
                    {
                        for (int i = 1; i < txt.Length; i++)
                            tmp += "|" + txt[i];
                    }

                    tmp += "|" + toWhom;
                    hp.Add(LocalSettingNames.ToPersonText, tmp);
                }
            }
            else
            {
                hp.Add(LocalSettingNames.ToPersonText, toWhom);
            }

        }

        public static Dictionary<string, string> GetCommandCode(int printerChoice)
        {
            var dict = new Dictionary<string, string>();

            switch (printerChoice)
            {
                case 0:
                    dict.Add("BarcodeTxt1Start", "\x1B\x28\x42\x26\x00\x02\x02\xFD\x32\x00\x00");
                    dict.Add("BarcodeTxt1Stop", "");
                    dict.Add("BarcodeTxt2Start", "\x1B\x28\x42\x24\x00\x02\x02\xFD\x32\x00\x00");
                    dict.Add("BarcodeTxt2Stop", "");
                    dict.Add("BarcodeTxt3Start", "\x1B\x28\x42\x12\x00\x02\x02\xFD\x32\x00\x00");
                    dict.Add("BarcodeTxt3Stop", "");
                    dict.Add("BarcodeA4Start", "\x1B\x28\x42\x27\x00\x02\x02\xFD\x32\x00\x00");
                    dict.Add("BarcodeA4Stop", "");
                    break;
                case 1:
                    dict.Add("BarcodeTxt1Start", "27#20#38#82#54#11#40#1");
                    dict.Add("BarcodeTxt1Stop", "");
                    dict.Add("BarcodeTxt2Start", "27#20#36#82#54#11#40#1");
                    dict.Add("BarcodeTxt2Stop", "");                                     
                    dict.Add("BarcodeTxt3Start", "27#20#18#82#54#11#40#1");
                    dict.Add("BarcodeTxt3Stop", "");                   

                    dict.Add("BarcodeA4Start", "27#20#39#82#54#11#40#1");
                    dict.Add("BarcodeA4Stop", "");
                    break;
                case 2:
                    dict.Add("BarcodeTxt1Start", "27#124#125#59#99#D#59#42");
                    dict.Add("BarcodeTxt1Stop", "42#59#78#$3#59#$0000#59#$0002#59#$X1A#59#$PB#59#67#59#72#$04");
                    dict.Add("BarcodeTxt2Start", "27#124#125#59#99#D#59#42");
                    dict.Add("BarcodeTxt2Stop", "42#59#78#$3#59#$0000#59#$0002#59#$X1A#59#$PB#59#67#59#72#$04");

                    dict.Add("BarcodeTxt3Start", "27#124#125#59#99#D#59#42");
                    dict.Add("BarcodeTxt3Stop", "42#59#78#$3#59#$0000#59#$0002#59#$X1A#59#$PB#59#67#59#72#$04");

                    dict.Add("BarcodeA4Start", "27#124#125#59#99#D#59#42");
                    dict.Add("BarcodeA4Stop", "42#59#78#$3#59#$0000#59#$0002#59#$X1A#59#$PB#59#67#59#72#$04");
                    break;
                case 3:
                    dict.Add("BarcodeTxt1Start", "^PN   ^T0380^M0405000 ^IBARC,C128,B,");
                    dict.Add("BarcodeTxt1Stop", "^G^-^T0");
                    dict.Add("BarcodeTxt2Start", "^PN   ^T0380^M0405000 ^IBARC,C128,B,");
                    dict.Add("BarcodeTxt2Stop", "^G^-^T0");

                    dict.Add("BarcodeTxt3Start", "^PN   ^T0380^M0405000 ^IBARC,C128,B,");
                    dict.Add("BarcodeTxt3Stop", "^G^-^T0");

                    dict.Add("BarcodeA4Start", "^PN   ^T0380^M0405000 ^IBARC,C128,B,");
                    dict.Add("BarcodeA4Stop", "^G^-^T0");
                    break;
                case 4:
                    dict.Add("BarcodeTxt1Start", "27#[16;3;1;;;;;;;0}#27#[3t");
                    dict.Add("BarcodeTxt1Stop", "27#[0t");
                    dict.Add("BarcodeTxt2Start", "27#[16;3;1;;;;;;;0}#27#[3t");
                    dict.Add("BarcodeTxt2Stop", "27#[0t");

                    dict.Add("BarcodeTxt3Start", "27#[16;3;1;;;;;;;0}#27#[3t");
                    dict.Add("BarcodeTxt3Stop", "27#[0t");

                    dict.Add("BarcodeA4Start", "27#[16;3;1;;;;;;;0}#27#[3t");
                    dict.Add("BarcodeA4Stop", "27#[0t");
                    break;
                case 5:
                    dict.Add("BarcodeTxt1Start", "^PY #27#jl^M0505000^T0430^IBARC,C128,B,");
                    dict.Add("BarcodeTxt1Stop", "^G^-^T0^PN");
                    dict.Add("BarcodeTxt2Start", "^PY #27#jl^M0505000^T0430^IBARC,C128,B,");
                    dict.Add("BarcodeTxt2Stop", "^G^-^T0^PN");

                    dict.Add("BarcodeTxt3Start", "^PY #27#jl^M0505000^T0430^IBARC,C128,B,");
                    dict.Add("BarcodeTxt3Stop", "^G^-^T0^PN");

                    dict.Add("BarcodeA4Start", "^PY #27#jl^M0505000^T0430^IBARC,C128,B,");
                    dict.Add("BarcodeA4Stop", "^G^-^T0^PN");
                    break;

                case 6:
                    dict.Add("BarcodeTxt1Start", "27#}h03080075#27#}P516");
                    dict.Add("BarcodeTxt1Stop", "27#}P520");
                    dict.Add("BarcodeTxt2Start", "27#}h03080075#27#}P516");
                    dict.Add("BarcodeTxt2Stop", "27#}P520");

                    dict.Add("BarcodeTxt3Start", "27#}h03080075#27#}P516");
                    dict.Add("BarcodeTxt3Stop", "27#}P520");

                    dict.Add("BarcodeA4Start", "27#}h03080075#27#}P516");
                    dict.Add("BarcodeA4Stop", "27#}P520");
                    break;
                default:
                    break;
            }

            return dict;
        }
    }

    public class CommandCodeForBarcode
    {
        //public string BarcodeTxt1Start { get; set; }

        //public string BarcodeTxt1Stop { get; set; }

        //public string BarcodeTxt2Start { get; set; }

        //public string BarcodeTxt2Stop { get; set; }

        //public string BarcodeA4Start { get; set; }

        //public string BarcodeA4Stop { get; set; }

        //public CommandCodeForBarcode(int printerChoice)
        //{
        //    BarcodeA4Stop = "";
        //    BarcodeA4Start = "";
        //    BarcodeTxt2Stop = "";
        //    BarcodeTxt2Start = "";
        //    BarcodeTxt1Stop = "";
        //    BarcodeTxt1Start = "";
        //    this.GetCommandCode(printerChoice);
        //}

        public static Dictionary<string, string> GetCommandCode(int printerChoice)
        {
            var dict = new Dictionary<string, string>();

            switch (printerChoice)
            {
                case 0:
                    dict.Add("BarcodeTxt1Start", "\x1B\x28\x42\x26\x00\x02\x02\xFD\x32\x00\x00");
                    dict.Add("BarcodeTxt1Stop", "");
                    dict.Add("BarcodeTxt2Start", "\x1B\x28\x42\x24\x00\x02\x02\xFD\x32\x00\x00");
                    dict.Add("BarcodeTxt2Stop", "");
                    dict.Add("BarcodeTxt3Start", "\x1B\x28\x42\x12\x00\x02\x02\xFD\x32\x00\x00");
                    dict.Add("BarcodeTxt3Stop", "");
                    dict.Add("BarcodeA4Start", "\x1B\x28\x42\x27\x00\x02\x02\xFD\x32\x00\x00");
                    dict.Add("BarcodeA4Stop", "");
                    break;
                case 1:
                    dict.Add("BarcodeTxt1Start", "27#20#38#82#54#11#40#1");
                    dict.Add("BarcodeTxt1Stop", "");
                    dict.Add("BarcodeTxt2Start", "27#20#36#82#54#11#40#1");
                    dict.Add("BarcodeTxt2Stop", "");
                    dict.Add("BarcodeA4Start", "27#20#39#82#54#11#40#1");
                    dict.Add("BarcodeA4Stop", "");
                    break;
                case 2:
                    dict.Add("BarcodeTxt1Start", "27#124#125#59#99#D#59#42");
                    dict.Add("BarcodeTxt1Stop", "42#59#78#$3#59#$0000#59#$0002#59#$X1A#59#$PB#59#67#59#72#$04");
                    dict.Add("BarcodeTxt2Start", "27#124#125#59#99#D#59#42");
                    dict.Add("BarcodeTxt2Stop", "42#59#78#$3#59#$0000#59#$0002#59#$X1A#59#$PB#59#67#59#72#$04");
                    dict.Add("BarcodeA4Start", "27#124#125#59#99#D#59#42");
                    dict.Add("BarcodeA4Stop", "42#59#78#$3#59#$0000#59#$0002#59#$X1A#59#$PB#59#67#59#72#$04");
                    break;
                case 3:
                    dict.Add("BarcodeTxt1Start", "^PN   ^T0380^M0405000 ^IBARC,C128,B,");
                    dict.Add("BarcodeTxt1Stop", "^G^-^T0");
                    dict.Add("BarcodeTxt2Start", "^PN   ^T0380^M0405000 ^IBARC,C128,B,");
                    dict.Add("BarcodeTxt2Stop", "^G^-^T0");
                    dict.Add("BarcodeA4Start", "^PN   ^T0380^M0405000 ^IBARC,C128,B,");
                    dict.Add("BarcodeA4Stop", "^G^-^T0");
                    break;
                case 4:
                    dict.Add("BarcodeTxt1Start", "27#[16;3;1;;;;;;;0}#27#[3t");
                    dict.Add("BarcodeTxt1Stop", "27#[0t");
                    dict.Add("BarcodeTxt2Start", "27#[16;3;1;;;;;;;0}#27#[3t");
                    dict.Add("BarcodeTxt2Stop", "27#[0t");
                    dict.Add("BarcodeA4Start", "27#[16;3;1;;;;;;;0}#27#[3t");
                    dict.Add("BarcodeA4Stop", "27#[0t");
                    break;
                case 5:
                    dict.Add("BarcodeTxt1Start", "^PY #27#jl^M0505000^T0430^IBARC,C128,B,");
                    dict.Add("BarcodeTxt1Stop", "^G^-^T0^PN");
                    dict.Add("BarcodeTxt2Start", "^PY #27#jl^M0505000^T0430^IBARC,C128,B,");
                    dict.Add("BarcodeTxt2Stop", "^G^-^T0^PN");
                    dict.Add("BarcodeA4Start", "^PY #27#jl^M0505000^T0430^IBARC,C128,B,");
                    dict.Add("BarcodeA4Stop", "^G^-^T0^PN");
                    break;

                case 6:
                    dict.Add("BarcodeTxt1Start", "27#}h03080075#27#}P516");
                    dict.Add("BarcodeTxt1Stop", "27#}P520");
                    dict.Add("BarcodeTxt2Start", "27#}h03080075#27#}P516");
                    dict.Add("BarcodeTxt2Stop", "27#}P520");
                    dict.Add("BarcodeA4Start", "27#}h03080075#27#}P516");
                    dict.Add("BarcodeA4Stop", "27#}P520");
                    break;
                default:
                    break;
            }

            return dict;
        }
    }
}

using System;
using System.Text;
using PEA.BPM.Architecture.PrintUtilities;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.BillPrintingModule.Interface.Constants;

namespace PEA.BPM.BillPrintingModule.BillPrintingUtilities
{
    public class TestPrintBill
    {
        public static void BlueBill()
        {
            string data = GenerateBlueBill();
            RawPrinterHelper.SendStringToPrinter(GetPrinterName(), data);
        }

        public static void GreenBill()
        {
            string data = GenerateGreenBill();
            RawPrinterHelper.SendStringToPrinter(GetPrinterName(), data);
        }

        public static void A4Bill()
        {
            string data = GenerateA4Bill();
            RawPrinterHelper.SendStringToPrinter(GetPrinterName(), data);
        }

        private static string GetPrinterName()
        {
            string printerName = "";
            //LocalSettingHelper hp = LocalSettingHelper.Instance();

            //if (hp.Read(LocalSettingNames.PrinterName) != null)
            //{
            //    printerName = hp.Read(LocalSettingNames.PrinterName).ToString();
            //}

            return printerName;
        }

        private static string GenerateBlueBill()
        {
            LocalSettingHelper hp = LocalSettingHelper.Instance();
            string resultTxt;
            string blank = " ";
            //field totalWidth in Pad property = column width in real receipt.
            StringBuilder sb = new StringBuilder();
            sb.Append("\r\n");
            sb.AppendFormat(string.Format("{0}{1}",
                blank.PadRight(80, ' '),
                "XXXX"));//1 R1
            sb.Append("\r\n");//CR&LF to line 2                
            sb.AppendFormat(String.Format("{0}{1}",
                blank.PadRight(13, ' '),
                "XXXX"));//2 L1
            sb.Append("\r\n");//CR&LF to line 3
            sb.Append("\r\n");//CR&LF to line 4
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}",
                blank.PadRight(35, ' '),
                //**********Right Part**********//
                "XXXX".PadLeft(8, ' '),
                "XXXX".PadLeft(19, ' '),
                blank.PadRight(1, ' '),
                "XXXX".PadRight(11, ' '),
                "XXXX".PadRight(6, ' '),
                "XXXX".PadRight(9, ' '),
                "XXXX".PadRight(7, ' '));//4 R1-6
            sb.Append("\r\n");//CR&LF to line 5
            sb.Append("\r\n");//CR&LF to line 6
            sb.AppendFormat("{0}{1}{2}{3}",
                blank.PadRight(26, ' '),
                "XXXX".PadRight(9, ' '),
                //**********Right Part**********//
                blank.PadRight(5, ' '),
                "XXXX");//6 L1,R1(substring90)
            sb.Append("\r\n");//CR&LF to line 7
            sb.AppendFormat("{0}{1}{2}{3}",
                blank.PadRight(4, ' '),
                ("XXXX".Length > 30 ? StringConvert.PadRight("XXXX".Substring(0, 29), 31) : StringConvert.PadRight("XXXX", 31)),
                //**********Right Part**********//
                blank.PadRight(5, ' '),
                "XXXX");//8 L1(substring90),R1
            sb.Append("\r\n");//CR&LF to line 8
            sb.AppendFormat("{0}{1}{2}{3}",
                blank.PadRight(4, ' '),
                ("XXXX".Length > 30 ? StringConvert.PadRight("XXXX".Substring(29, "XXXX".Length - 29), 31) : blank.PadRight(31, ' ')),
                //**********Right Part**********//
                blank.PadRight(5, ' '),
                "XXXX");//9 L1,R1
            sb.Append("\r\n");//CR&LF to line9
            sb.AppendFormat("{0}{1}{2}{3}{4}",
                blank.PadRight(4, ' '),
                StringConvert.PadRight("XXXX", 31),
                //**********Right Part**********//
                blank.PadRight(5, ' '),
                StringConvert.PadRight("XXXX", 45),
                "XXXX");//10 L1,R1,R2
            sb.Append("\r\n");//CR&LF to line10
            sb.AppendFormat("{0}{1}{2}",
                blank.PadRight(4, ' '),
                StringConvert.PadRight("XXXX", 31),
                //**********Right Part**********//
                "XXXX".PadLeft(12, ' '));//11 L1,R1
            sb.Append("\r\n");//CR&LF to line11
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}",
                blank.PadRight(4, ' '),
                StringConvert.PadRight("XXXX", 31),
                //**********Right Part**********//
                blank.PadRight(14, ' '),
                "XXXX".PadLeft(11, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(10, ' '),
                "XXXX".PadLeft(14, ' '));//12 L1,L2,R1-4
            sb.Append("\r\n");//CR&LF to line12
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}",
                blank.PadRight(8, ' '),
                StringConvert.PadRight("XXXX", 18),
                "XXXX".PadRight(9, ' '),
                //**********Right Part**********//
                blank.PadRight(5, ' '),
                "XXXX".PadRight(6, ' '),
                "XX".PadLeft(2, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(10, ' '),
                "XXXX".PadLeft(14, ' '));//13 L1,R1-6
            sb.Append("\r\n");//CR&LF to line13
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                blank.PadRight(11, ' '),
                "XXXX".PadRight(24, ' '),
                //**********Right Part**********//
                blank.PadRight(5, ' '),
                "XXXX".PadRight(6, ' '),
                "XX".PadLeft(2, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(10, ' '),
                "XXXX".PadLeft(14, ' '));//14 L1,L2,R1-6
            sb.Append("\r\n");//CR&LF to line14
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                blank.PadRight(8, ' '),
                "XXXX".PadRight(19, ' '),
                "XXXX".PadRight(8, ' '),
                //**********Right Part**********//
                blank.PadRight(11, ' '),
                "XX".PadLeft(2, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(10, ' '),
                "XXXX".PadLeft(14, ' '));//15 L1,L2,R1-5
            sb.Append("\r\n");//CR&LF to line15
            sb.AppendFormat("{0}{1}{2}{3}",
                "XXXX".PadLeft(18, ' '),
                "XXXX".PadLeft(17, ' '),
                //**********Right Part**********//
                blank.PadRight(2, ' '),
                "XXXX");//16 L1,N1
            sb.Append("\r\n");//CR&LF to line16
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}",
                "XXXX".PadLeft(31, ' '),
                blank.PadRight(4, ' '),
                //**********Right Part**********//
                blank.PadRight(2, ' '),
                StringConvert.PadRight("XXXX", 30),
                blank.PadRight(16, ' '),
                "XXXX".PadLeft(13, ' '));//17 N1,L1,R1
            sb.Append("\r\n");//CR&LF to line17                
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}",
                "XXXX".PadLeft(31, ' '),
                blank.PadRight(4, ' '),
                //**********Right Part**********//
                blank.PadRight(2, ' '),
                StringConvert.PadRight("XXXX", 31),
                StringConvert.PadLeft("XXXX", 15),
                "XXXX".PadLeft(13, ' '));//18 N1,L1,L2,L3,R1,R2
            sb.Append("\r\n");//CR&LF to line18                
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}",
                blank.PadRight(2, ' '),
                StringConvert.PadRight("XXXX", 16),
                StringConvert.PadLeft("XXXX", 13),
                blank.PadRight(1, ' '),
                "XXX".PadRight(3, ' '),
                //**********Right Part**********//
                blank.PadRight(2, ' '),
                StringConvert.PadRight("XXXX", 30),
                "XXXX".PadLeft(9, ' '),
                blank.PadRight(7, ' '),
                "XXXX".PadLeft(13, ' '));//19 N1,L1,L2,R1,R2
            sb.Append("\r\n");//CR&LF to line19
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}",
                blank.PadRight(5, ' '),
                "XXXX".PadRight(10, ' '),
                "XXXX".PadLeft(16, ' '),
                blank.PadRight(4, ' '),
                //**********Right Part**********//
                blank.PadRight(48, ' '),
                "XXXX".PadLeft(13, ' '));//20 L1,R1
            sb.Append("\r\n");//CR&LF to line20
            sb.AppendFormat("{0}{1}{2}{3}{4}",
                "XXXX".PadLeft(31, ' '),
                blank.PadRight(4, ' '),
                //**********Right Part**********//
                blank.PadRight(44, ' '),
                "XXXX".PadRight(4, ' '),
                "XXXX".PadLeft(13, ' '));//21 L1,L2,R1,R2
            sb.Append("\r\n");//CR&LF to line21
            sb.AppendFormat("{0}{1}{2}{3}{4}",
                "XXXX".PadLeft(13, ' '),
                "XXXX".PadLeft(18, ' '),
                blank.PadRight(4, ' '),
                //**********Right Part**********//
                blank.PadRight(48, ' '),
                "XXXX".PadLeft(13, '*'));//22 L1,R1
            sb.Append("\r\n");//CR&LF to line22
            //********************************************************************************************************************
            //string barcode1 = BarcodeMapping(hp.Read(LocalSettingNames.Barcode1Start).ToString()) + "12345678901234567890123456789012" + BarcodeMapping(hp.Read(LocalSettingNames.Barcode1Stop).ToString());
            string barcode1 = string.Empty;
            sb.AppendFormat("{0}{1}{2}{3}{4}",
                blank.PadRight(18, ' '),
                "XXXX".PadLeft(13, '*'),
                blank.PadRight(4, ' '),
                //**********Right Part**********//
                blank.PadRight(1, ' '),
                barcode1);//23 B1,L1-3
            //********************************************************************************************************************

            sb.Append("\r\n");//CR&LF to line23           
            sb.Append("\r\n");//CR&LF to line24
            sb.Append("\r\n");

            //********************************************************************************************************************
            //string barcode2 = BarcodeMapping(hp.Read(LocalSettingNames.Barcode2Start).ToString()) + "123456789012345678901234567890" + BarcodeMapping(hp.Read(LocalSettingNames.Barcode2Stop).ToString());
            string barcode2 = string.Empty;
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}",
                StringConvert.PadRight("XXXX", 20),
                "XXXX".PadLeft(10, ' '),
                blank.PadRight(2, ' '),
                "XXX".PadRight(3, ' '),
                //**********Right Part**********//
                blank.PadRight(1, ' '),
                barcode2);//25 B2********Barcode**********
            //********************************************************************************************************************

            sb.Append("\r\n");//CR&LF to line25                    
            sb.Append("\r\n");
            sb.Append("\r\n");//CR&LF to line26
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}",
                blank.PadRight(9, ' '),
                "XXXX".PadRight(13, ' '),
                "XXXX".PadLeft(7, ' '),
                blank.PadRight(6, ' '),
                //**********Right Part**********//
                blank.PadRight(51, ' '),
                "XXXX".PadRight(10, ' '));//27 R1
            sb.Append("\r\n");//CR&LF to line27                
            sb.Append("\r\n");//CR&LF to line28     
            resultTxt = sb.ToString();

            return resultTxt;
        }

        private static string GenerateGreenBill()
        {
            LocalSettingHelper hp = LocalSettingHelper.Instance();
            string resultTxt;
            string blank = " ";

            StringBuilder sb = new StringBuilder();
            sb.Append("\r\n");
            sb.AppendFormat(string.Format("{0}{1}",
                blank.PadRight(78, ' '),
                "XXXX"));//1 R1
            sb.Append("\r\n");//CR&LF to line 2
            sb.Append("\r\n");//CR&LF to line 3
            sb.AppendFormat(String.Format("{0}{1}{2}",
                blank.PadRight(4, ' '),
                "XXXX".PadRight(21, ' '),
                "XXXX"));//3 L1
            sb.Append("\r\n");//CR&LF to line 4
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}",
                blank.PadRight(35, ' '),
                //**********Right Part**********//
                "XXXX".PadLeft(8, ' '),
                blank.PadRight(1, ' '),
                "XXXX".PadRight(18, ' '),
                "XXXX".PadRight(11, ' '),
                "XXXX".PadRight(6, ' '),
                "XXXX".PadRight(9, ' '),
                "XXXX".PadRight(7, ' '));//4 R1-6
            sb.Append("\r\n");//CR&LF to line 5
            sb.Append("\r\n");//CR&LF to line 6
            sb.AppendFormat("{0}{1}{2}{3}{4}",
                blank.PadRight(23, ' '),
                "XXXX".PadRight(12, ' '),
                //**********Right Part**********//
                blank.PadRight(6, ' '),
                StringConvert.PadRight("XXXX", 44),
                "XXXX");//6 L1,R1,R2
            sb.Append("\r\n");//CR&LF to line 7
            sb.AppendFormat("{0}{1}{2}{3}",
                blank.PadRight(4, ' '),
                StringConvert.PadRight("XXXX", 31),
                //**********Right Part**********//
                blank.PadRight(6, ' '),
                "XXXX");//7 L1,R1
            sb.Append("\r\n");//CR&LF to line 8
            sb.AppendFormat("{0}{1}{2}{3}",
                blank.PadRight(4, ' '),
                StringConvert.PadRight("XXXX", 31),
                //**********Right Part**********//
                blank.PadRight(6, ' '),
                "XXXX");//8 L1,R1
            sb.Append("\r\n");//CR&LF to line 9
            sb.AppendFormat("{0}{1}{2}{3}",
                blank.PadRight(4, ' '),
                StringConvert.PadRight("XXXX", 31),
                //**********Right Part**********//
                blank.PadRight(6, ' '),
                "XXXX");//9 L1,R1
            sb.Append("\r\n");//CR&LF to line 10
            sb.AppendFormat("{0}{1}{2}{3}{4}",
                blank.PadRight(4, ' '),
                StringConvert.PadRight("XXXX", 31),
                //**********Right Part**********//
                blank.PadRight(6, ' '),
                StringConvert.PadRight("XXXX", 44),
                "XXXX");//10 L1,R1,R2
            sb.Append("\r\n");//CR&LF to line 11
            sb.AppendFormat("{0}{1}{2}",
                blank.PadRight(4, ' '),
                StringConvert.PadRight("XXXX", 31),
                //**********Right Part**********//
                "XXXX".PadLeft(12, ' '));//11 L1,R1
            sb.Append("\r\n");//CR&LF to line 12
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}",
                blank.PadRight(8, ' '),
                StringConvert.PadRight("XXXX", 18),
                "XXXX".PadRight(9, ' '),
                //**********Right Part**********//
                blank.PadRight(13, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(10, ' '),
                "XXXX".PadLeft(14, ' '));//12 L1,L2,R1-4
            sb.Append("\r\n");//CR&LF to line 13
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                blank.PadRight(11, ' '),
                "XXXX".PadRight(24, ' '),
                //**********Right Part**********//
                blank.PadRight(5, ' '),
                "XXXX".PadRight(6, ' '),
                "XX".PadLeft(2, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(10, ' '),
                "XXXX".PadLeft(14, ' '));//13 L1,R1-6
            sb.Append("\r\n");//CR&LF to line 14
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}",
                blank.PadRight(8, ' '),
                "XXXX".PadRight(19, ' '),
                "XXXX".PadRight(8, ' '),
                //**********Right Part**********//
                blank.PadRight(5, ' '),
                "XXXX".PadRight(6, ' '),
                "XX".PadLeft(2, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(10, ' '),
                "XXXX".PadLeft(14, ' '));//14 L1,L2,R1-6
            sb.Append("\r\n");//CR&LF to line 15
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}",
                "XXXX".PadLeft(18, ' '),
                "XXXX".PadLeft(17, ' '),
                //**********Right Part**********//
                blank.PadRight(11, ' '),
                "XX".PadLeft(2, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(10, ' '),
                "XXXX".PadLeft(14, ' '));//15 L1,L2,R1-5
            sb.Append("\r\n");//CR&LF to line 16
            sb.AppendFormat("{0}{1}{2}{3}",
                "XXXX".PadLeft(31, ' '),
                blank.PadRight(4, ' '),
                //**********Right Part**********//
                blank.PadRight(1, ' '),
                StringConvert.PadRight("XXXX", 30));//16 L1,N1
            sb.Append("\r\n");//CR&LF to line 17
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}",
                "XXXX".PadLeft(31, ' '),
                blank.PadRight(4, ' '),
                //**********Right Part**********//
                blank.PadRight(2, ' '),
                StringConvert.PadRight("XXXX", 30),
                blank.PadRight(16, ' '),
                "XXXX".PadLeft(13, ' '));//17 L1,N1,R1
            sb.Append("\r\n");//CR&LF to line 18
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}",
                StringConvert.PadRight("XXXX", 17),
                "XXXX".PadLeft(14, ' '),
                blank.PadRight(1, ' '),
                StringConvert.PadRight("XXX", 3),
                //**********Right Part**********//
                blank.PadRight(2, ' '),
                StringConvert.PadRight("XXXX", 30),
                StringConvert.PadLeft("XXXX", 15),
                "XXXX".PadLeft(14, ' '));//18 L1-3,N1,R1,R2
            sb.Append("\r\n");//CR&LF to line 19
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}",
                blank.PadRight(5, ' '),
                "XXXX".PadRight(10, ' '),
                "XXXX".PadLeft(16, ' '),
                blank.PadRight(4, ' '),
                //**********Right Part**********//
                blank.PadRight(2, ' '),
                StringConvert.PadRight("XXXX", 30),
                blank.PadRight(3, ' '),
                "XXXX".PadRight(6, ' '),
                blank.PadRight(6, ' '),
                "XXXX".PadLeft(14, ' '));//19 L1,L2,N1,R1,R2
            sb.Append("\r\n");//CR&LF to line 20
            sb.AppendFormat("{0}{1}{2}{3}",
                "XXXX".PadLeft(31, ' '),
                blank.PadRight(4, ' '),
                //**********Right Part**********//
                blank.PadRight(48, ' '),
                "XXXX".PadLeft(13, ' '));//20 L1,R1
            sb.Append("\r\n");//CR&LF to line 21
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}",
                blank.PadRight(9, ' '),
                "XXXX".PadRight(5, ' '),
                "XXXX".PadLeft(17, ' '),
                blank.PadRight(4, ' '),
                //**********Right Part**********//
                blank.PadRight(14, ' '),
                "XXXX".PadRight(28, ' '),
                "XXXX".PadRight(6, ' '),
                "XXXX".PadLeft(13, ' '));//21 L1,L2,R1-3
            sb.Append("\r\n");//CR&LF to line 22
            sb.AppendFormat("{0}{1}{2}{3}{4}",
                "XXXX".PadLeft(31, ' '),
                blank.PadRight(4, ' '),
                //**********Right Part**********//
                blank.PadRight(20, ' '),
                "XXXX".PadRight(28, ' '),
                "XXXX".PadLeft(13, '*'));//22 L1,R1,R2
            sb.Append("\r\n");//CR&LF to line 23
            sb.AppendFormat("{0}{1}{2}",
                blank.PadRight(18, ' '),
                blank.PadRight(9, ' '),
                "XXXX".PadLeft(7, ' '));//23 L1
            sb.Append("\r\n");//CR&LF to line 24                    
            resultTxt = sb.ToString();


            return resultTxt;
        }

        private static string GenerateA4Bill()
        {
            LocalSettingHelper hp = LocalSettingHelper.Instance();
            string resultTxt;
            string blank = " ";
            //A4Bill has very strange line, don't forget to take a look na krub
            StringBuilder sb = new StringBuilder();
            sb.Append("\r\n");
            sb.Append("\r\n");
            sb.Append("\r\n");
            sb.Append("\r\n");
            sb.Append("\r\n");
            sb.Append("\r\n");
            sb.Append("\r\n");
            sb.Append("\r\n");
            sb.Append("\r\n");
            sb.AppendFormat("{0}{1}{2}",
                blank.PadRight(7, ' '),
                "XXXX".PadRight(50, ' '),
                "XXXX");//1 1,2
            sb.Append("\r\n");//CR&LF to line 2
            sb.Append("\r\n");
            sb.AppendFormat("{0}{1}{2}{3}",
                blank.PadRight(67, ' '),
                "XXXX".PadRight(8, ' '),
                StringConvert.PadRight("XXXX", 14),
                "XXXX");//2 1-3
            sb.Append("\r\n");//CR&LF to line 3
            sb.Append("\r\n");
            sb.AppendFormat("{0}{1}",
                blank.PadRight(7, ' '),
                "XXXX");//3 1
            sb.Append("\r\n");//CR&LF to line 4
            sb.Append("\r\n");
            sb.AppendFormat("{0}{1}",
                blank.PadRight(44, ' '),
                "XXXX");//4 1
            sb.Append("\r\n");//CR&LF to line 5
            sb.Append("\r\n");
            sb.Append("\r\n");
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}",
                blank.PadRight(5, ' '),
                "XXXX".PadRight(11, ' '),
                "XXXX".PadRight(21, ' '),
                StringConvert.PadRight("XXXX", 14),
                "XXXX".PadRight(11, ' '),
                "XXXX".PadRight(13, ' '),
                "XXXX".PadRight(8, ' '),
                "XXXX".PadRight(9, ' '));//5 1-7
            sb.Append("\r\n");//CR&LF to line 6_1
            sb.Append("\r\n");
            sb.AppendFormat("{0}", "XXXX".PadLeft(11, ' '));
            sb.Append("\r\n");//CR&LF to line 6_2
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}",
                "XXXX".PadLeft(11, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(13, ' '),
                "XXXX".PadLeft(14, ' '),
                blank.PadRight(16, ' '),
                "XXXX".PadLeft(14, ' '));//6 1-7
            sb.Append("\r\n");//CR&LF to line 7 
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}",
                "XXXX".PadLeft(11, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(13, ' '),
                "XXXX".PadLeft(14, ' '),
                blank.PadRight(16, ' '),
                "XXXX".PadLeft(14, ' '));//7 1-6
            sb.Append("\r\n");//CR&LF to line 8
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}",
                "XXXX".PadLeft(11, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(13, ' '),
                blank.PadLeft(14, ' '),
                blank.PadRight(16, ' '),
                "XXXX".PadLeft(14, ' '));//8 1-6 no 4
            sb.Append("\r\n");//CR&LF to line 9
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}",
                "XXXX".PadLeft(11, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(13, ' '),
                "XXXX".PadLeft(14, ' '),
                blank.PadRight(16, ' '),
                "XXXX".PadLeft(14, ' '));//9 1-6
            sb.Append("\r\n");//CR&LF to line 10
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}",
                "XXXX".PadLeft(11, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(13, ' '),
                "XXXX".PadLeft(14, ' '),
                blank.PadRight(16, ' '),
                "XXXX".PadLeft(14, ' '));//10 1-6
            sb.Append("\r\n");//CR&LF to line 11
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}",
                "XXXX".PadLeft(11, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(13, ' '),
                blank.PadLeft(14, ' '),
                blank.PadRight(16, ' '),
                "XXXX".PadLeft(14, ' '));//11 1-6 no 4
            sb.Append("\r\n");//CR&LF to line 12
            sb.AppendFormat("{0}{1}{2}{3}{4}",
                "XXXX".PadLeft(11, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(13, ' '),
                "XXXX".PadLeft(14, ' '));//12 1-5
            sb.Append("\r\n");//CR&LF to line 13
            sb.AppendFormat("{0}{1}{2}{3}{4}",
                "XXXX".PadLeft(11, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(13, ' '),
                "XXXX".PadLeft(14, ' '));//13 1-5
            sb.Append("\r\n");//CR&LF to line 14
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                "XXXX".PadLeft(11, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(13, ' '),
                blank.PadLeft(14, ' '),
                blank.PadRight(2, ' '),
                StringConvert.PadLeft("XXXX", 8),
                blank.PadRight(6, ' '),
                "XXXX".PadLeft(14, ' '));//14 1-7 no 4
            sb.Append("\r\n");//CR&LF to line 15
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}",
                "XXXX".PadLeft(11, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(13, ' '),
                "XXXX".PadLeft(14, ' '),
                blank.PadRight(16, ' '),
                "XXXX".PadLeft(14, ' '));//15 1-6
            sb.Append("\r\n");//CR&LF to line 16
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}",
                "XXXX".PadLeft(11, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(13, ' '),
                "XXXX".PadLeft(14, ' '),
                blank.PadRight(16, ' '),
                "XXXX".PadLeft(14, ' '));//16 1-6
            sb.Append("\r\n");//CR&LF to line 17
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}",
                "XXXX".PadLeft(11, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(13, ' '),
                blank.PadLeft(14, ' '),
                StringConvert.PadLeft("XXXX", 16),
                "XXXX".PadLeft(14, ' '));//17 1-7 no 4
            sb.Append("\r\n");//CR&LF to line 18
            sb.AppendFormat("{0}{1}{2}{3}{4}",
                "XXXX".PadLeft(26, ' '),
                "XXXX".PadLeft(18, ' '),
                "XXXX".PadLeft(18, ' '),
                blank.PadRight(16, ' '),
                "XXXX".PadLeft(14, ' '));//18 1-4
            sb.Append("\r\n");//CR&LF to line 19
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}",
                blank.PadRight(11, ' '),
                "XXXX".PadRight(38, ' '),
                "XXXX".PadLeft(13, ' '),
                blank.PadRight(12, ' '),
                "XXXX".PadRight(4, ' '),
                "XXXX".PadLeft(14, ' '));//19 1-4
            sb.Append("\r\n");//CR&LF to line 20
            sb.AppendFormat("{0}{1}{2}{3}",
                "XXXX".PadLeft(23, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(13, ' '),
                "XXXX".PadLeft(14, ' '));//20 1-4
            sb.Append("\r\n");//CR&LF to line 21
            sb.AppendFormat("{0}{1}{2}{3}{4}{5}",
                "XXXX".PadLeft(23, ' '),
                "XXXX".PadLeft(12, ' '),
                "XXXX".PadLeft(13, ' '),
                "XXXX".PadLeft(14, ' '),
                blank.PadRight(16, ' '),
                "XXXX".PadLeft(14, ' '));//21 1-5
            sb.Append("\r\n");//CR&LF to line 22
            sb.Append("\r\n");//CR&LF to line 23
            sb.Append("\r\n");
            sb.AppendFormat("{0}{1}{2}{3}{4}",
                "XXXX".PadLeft(25, ' '),
                "XXXX".PadLeft(16, ' '),
                "XXXX".PadLeft(15, ' '),
                blank.PadRight(2, ' '),
                "XXXX");//23 1-4
            sb.Append("\r\n");//CR&LF to line 24
            sb.AppendFormat("{0}{1}{2}{3}{4}",
                "XXXX".PadLeft(25, ' '),
                "XXXX".PadLeft(16, ' '),
                "XXXX".PadLeft(15, ' '),
                blank.PadRight(2, ' '),
                "XXXX");//24 1-4
            sb.Append("\r\n");//CR&LF to line 25
            sb.AppendFormat("{0}{1}{2}{3}",
                blank.PadLeft(41, ' '),
                "XXXX".PadLeft(15, ' '),
                blank.PadRight(2, ' '),
                "XXXX");//25 3
            sb.Append("\r\n");//CR&LF to line 26
            sb.AppendFormat("{0}{1}{2}{3}{4}",
                "XXXX".PadLeft(25, ' '),
                "XXXX".PadLeft(16, ' '),
                "XXXX".PadLeft(15, ' '),
                blank.PadRight(2, ' '),
                "XXXX");//26 1-3
            sb.Append("\r\n");//CR&LF to line 27
            sb.AppendFormat("{0}{1}",
                blank.PadRight(58, ' '),
                "XXXX");//27 1
            sb.Append("\r\n");//CR&LF to line 28
            sb.AppendFormat("{0}{1}",
                blank.PadRight(58, ' '),
                "XXXX");//28 1
            sb.Append("\r\n");//CR&LF to line 29
            sb.AppendFormat("{0}{1}",
                blank.PadRight(58, ' '),
                "XXXX");//29 1
            sb.Append("\r\n");//CR&LF to line 30
            sb.AppendFormat("{0}{1}",
                blank.PadRight(58, ' '),
                "XXXX");//30 1
            sb.Append("\r\n");//CR&LF to line 31
            sb.AppendFormat("{0}{1}",
                blank.PadRight(22, ' '),
                "(" + "XXXX" + ")");//31 1
            sb.Append("\r\n");//CR&LF to line 32
            sb.Append("\r\n");
            sb.AppendFormat("{0}{1}",
                blank.PadRight(25, ' '),
                "XXXX");//32 1
            sb.Append("\r\n");//CR&LF to line 33
            sb.Append("\r\n");
            sb.AppendFormat("{0}{1}",
                blank.PadRight(12, ' '),
                "XXXX");//33 1
            sb.Append("\r\n");//CR&LF to line 34
            sb.Append("\r\n");
            sb.Append("\r\n");
            sb.Append("\r\n");
            sb.Append("\r\n");
            sb.Append("\r\n");
            sb.Append("\r\n");
            sb.Append("\r\n");
            sb.Append("\r\n");
            sb.Append("\r\n");
            sb.AppendFormat("{0}{1}",
                blank.PadRight(65, ' '),
                "XXXX");
            sb.Append("\r\n");
            sb.Append("\r\n");
            sb.AppendFormat("{0}{1}",
                blank.PadRight(65, ' '),
                "XXXX");
            sb.Append("\r\n");
            sb.Append("\r\n");
            sb.Append("\r\n");
            resultTxt = sb.ToString();

            return resultTxt;
        }

        private static string BarcodeMapping(string bCode)
        {
            string realBarCode = string.Empty;

            if (bCode != null || bCode != string.Empty)
            {
                char[] seperator = new char[] { '#' };
                string[] p1 = bCode.Trim().Split(seperator, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i <= p1.Length - 1; i++)
                {
                    try
                    {
                        realBarCode = realBarCode + Ch(Convert.ToInt32(p1[i]));
                    }
                    catch
                    {
                        if (p1[i].Substring(0, 1) == "$")
                            p1[i] = p1[i].Replace("$", "");

                        realBarCode = realBarCode + p1[i];
                    }
                }
            }

            return realBarCode;
        }

        private static string Ch(int value)
        {
            return ((char)value).ToString();
        }


    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using System.Drawing;
using System.Drawing.Printing;

namespace PEA.BPM.PaymentCollectionModule.Views.SlipPrintingView.Fp410Receipt
{
    internal class Printer
    {
        private static Printer _printer = new Printer();
        private byte[] _logoData;

        public static Printer Instant
        {
            get
            {
                return _printer;
            }
        }

        private Printer()
        {
            _logoData = PeaLogo.GetLogoData();
        }

        Font _printFont = new Font("BrowalliaUPC", 12, FontStyle.Regular, GraphicsUnit.Point);
        List<string> _printData;

        public bool Print(List<string> printData)
        {
            bool result = true;

            try
            {
                _printData = printData;

                LocalSettingHelper hp = LocalSettingHelper.Instance();

                string posPrinterName = hp.ReadString(LocalSettingNames.SlipPOSPrinterName);
                if (null == posPrinterName || posPrinterName == string.Empty)
                {
                    PrinterSelectionForm form = new PrinterSelectionForm("ใบเสร็จรับเงิน POS Slip");
                    if (DialogResult.OK == form.ShowDialog())
                    {
                        posPrinterName = form.SelectedPrinterName;
                        hp.Update(LocalSettingNames.SlipPOSPrinterName, posPrinterName);
                    }
                    else
                    {
                        return false;
                    }
                }

                PrintDocument pd = new PrintDocument();

                var countPrintData = printData.Count;
                for (int i = 0; i < printData.Count; i++)
                {
                    if (printData[i].ToString().Contains("เช็ค"))
                    {
                        countPrintData = countPrintData + 2;
                    }
                    else
                    {
                        if (i < 15 || i > printData.Count - 7)
                        {
                            if (printData[i].Length / 50 > 0)
                            {
                                countPrintData = countPrintData + (printData[i].Length / 50);
                            }
                        }
                        else
                        {
                            if (printData[i].Length / 40 > 0)
                            {
                                countPrintData = countPrintData + (printData[i].Length / 40);
                            }
                        }
                    }
                }
                pd.DefaultPageSettings.PaperSize = new PaperSize("Custom", 827, countPrintData * 23);

                //pd.PrinterSettings.PrinterName = "TH200(S)";
                //pd.PrinterSettings.PrinterName = "Microsoft XPS Document Writer";
                pd.PrinterSettings.PrinterName = posPrinterName;
                pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);

                pd.PrintController = new StandardPrintController(); //By Nick
                pd.Print();
                
            }
            catch
            {
                result = false;
            }
            return result;
        }

        private const float MARGIN = 5;
        float _paperWidth = 70 * 4 - MARGIN;

        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {   
            e.Graphics.DrawImage(Properties.Resources.peaLogo, new RectangleF( new PointF(110, 5), new SizeF(70,70)));
            
            float leftMargin = 0;
            float topMargin = 80;
            float lineHeight = _printFont.GetHeight(e.Graphics);

            Pen dashPen = new Pen(Brushes.Black);
            dashPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            StringFormat rightFormat = new StringFormat();
            rightFormat.Alignment = StringAlignment.Far;

            StringFormat leftFormat = new StringFormat();
            leftFormat.Alignment = StringAlignment.Near;

            string data = null;
            for (int i = 0; i < _printData.Count; i++)
            {
                data = _printData[i];
                if (data == string.Empty)
                {
                    topMargin += lineHeight;
                }
                if (data.IndexOf("---") > -1)
                {                    
                    e.Graphics.DrawLine(dashPen, leftMargin, topMargin+(lineHeight/2), leftMargin + _paperWidth, topMargin+(lineHeight/2));
                    topMargin += lineHeight;
                }
                else
                {
                    string[] toPrintData = data.Split('\t');
                    if (toPrintData.Length == 1)
                    {
                        SizeF size = e.Graphics.MeasureString(toPrintData[0], _printFont, new SizeF(_paperWidth, 2000),
                            new StringFormat());
                        e.Graphics.DrawString(toPrintData[0], _printFont, Brushes.Black,
                            new RectangleF(new PointF(leftMargin, topMargin), new SizeF(_paperWidth, 2000)),
                            leftFormat);

                        topMargin += size.Height;
                    }
                    else
                    {
                        SizeF rSize = e.Graphics.MeasureString(toPrintData[1], _printFont, new SizeF(_paperWidth/2, 2000),
                            new StringFormat());
                        e.Graphics.DrawString(toPrintData[1], _printFont, Brushes.Black,
                            new RectangleF(new PointF(leftMargin, topMargin), new SizeF(_paperWidth, 2000)),
                            rightFormat);


                        SizeF lSize = e.Graphics.MeasureString(toPrintData[0], _printFont, new SizeF(_paperWidth-rSize.Width-10, 2000),
                            new StringFormat());
                        e.Graphics.DrawString(toPrintData[0], _printFont, Brushes.Black,
                            new RectangleF(new PointF(leftMargin, topMargin), new SizeF(_paperWidth-rSize.Width-10, 2000)),
                            leftFormat);
                        
                        
                        topMargin += rSize.Height>lSize.Height? rSize.Height: lSize.Height;
                    }
                }                
            }
        }
                
        private bool CheckPrinterStatus(Form parentForm, SerialPort sp)
        {
            //return true;

            string dle = "\x10";
            string eot = "\x04";

            string qMsg = "\n\nกดปุ่ม 'Retry' เพื่อพิมพ์ หรือกดปุ่ม 'Cancel' เพื่อยกเลิก";
            while (true)
            {
                sp.Write(dle + eot + "\x01");

                byte result = ReadResult(sp);
                if (result != 22)
                {
                    string msg = "";

                    if (result == 99)
                    {
                        msg = "เครื่องพิมพ์ยังไม่ได้เปิดสวิทซ์ใช้งาน หรือไม่ได้ต่อสายเครื่องพิมพ์\nโปรดตรวจสอบความถูกต้องอีกครั้ง" + qMsg;
                    }
                    else
                    {
                        sp.Write("\x10" + "\x04" + "\x02");
                        result = ReadResult(sp);

                        switch (result)
                        {
                            case 18:
                                msg = "เครื่องพิมพ์กระดาษหมด หรือป้อนกระดาษไม่ถูกต้อง โปรดตรวจสอบความถูกต้องอีกครั้ง" + qMsg;
                                break;
                            case 22:
                                msg = "ฝาเครื่องพิมพ์ปิดไม่สนิท โปรดตรวจสอบความถูกต้องอีกครั้ง" + qMsg;
                                break;
                            default:
                                msg = "เครื่องพิมพ์ผิดพลาด โปรดตรวจสอบความถูกต้องอีกครั้ง" + qMsg;
                                break;
                        }
                    }

                    if (MessageBox.Show(parentForm, msg, "ข้อผิดพลาด", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) != DialogResult.Retry)
                    {
                        return false;
                    }

                }
                else
                {
                    return true;
                }
            }
        }

        private byte ReadResult(SerialPort sp)
        {
            string resultString = "";
            int count = 0;
            while (resultString.Length == 0)
            {
                if (count++ > 20)
                {
                    resultString = ((char)99).ToString();
                }
                else
                {
                    Thread.Sleep(100);
                    resultString = sp.ReadExisting();
                }
            }
            byte result = (byte)resultString[0];

            return result;
        }

        private byte[] GetPrintSetting()
        {
            byte _esc = 0x1b;

            List<byte> byteArray = new List<byte>();
            byteArray.AddRange(new byte[] { _esc, 0x20, 1 }); // Set letter spacing
            byteArray.AddRange(new byte[] { _esc, 0x33, 65 }); // Set line spacing
            //byteArray.AddRange(new byte[] { _esc, 0x21, 1 }); // Set font to 7X9
            byteArray.AddRange(new byte[] { _esc, 0x21, 17 }); // Set font to 7X9 with double height
            byteArray.AddRange(new byte[] { _esc, 0x61, 0 }); // Specify a lefted printing position

            return byteArray.ToArray();
        }
    }
}

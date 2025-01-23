using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Reporting.WinForms;
using PEA.BPM.Architecture.CommonUtilities;
using System.IO.Ports;
using System.Threading;
using System.Runtime.Serialization;

namespace PEA.BPM.Architecture.PrintUtilities
{
    [DataContract]
    public class PrintUtility
    {
        string _printerName;
        int _printerWidth;
        int _printerHeight;
        int _baudRate;
        private IList<Stream> m_streams;
        private int m_currentPageIndex;

        [DataMember]
        public int CurrentPageIndex
        {
            get { return this.m_currentPageIndex; }
            set { this.m_currentPageIndex = value; }
        }

        //public PrintUtility(string printerName)
        //{
        //    string pName = printerName;
        //    LocalSettingHelper hp = LocalSettingHelper.Instance();
        //    if (hp.Read(IntegrationFileNames.PrinterName) != null)
        //    {
        //        pName = hp.Read(IntegrationFileNames.PrinterName).ToString();
        //    }
        //    this._printerName = pName;
        //}

        public PrintUtility(string printerName, int width, int height)
        {
            string pName = printerName;
            LocalSettingHelper hp = LocalSettingHelper.Instance();
            if (hp.Read(printerName) != null)
            {
                pName = hp.Read(printerName).ToString();
            }
            this._printerName = pName;
            this._printerHeight = height;
            this._printerWidth = width;
        }

        public PrintUtility(string printerName, int width, int height, int baudRate)
        {
            string pName = printerName;
            LocalSettingHelper hp = LocalSettingHelper.Instance();
            if (hp.Read(printerName) != null)
            {
                pName = hp.Read(printerName).ToString();
            }
            this._printerName = pName;
            this._printerHeight = height;
            this._printerWidth = width;
            this._baudRate = baudRate;
        }

        // Export the given report as an EMF (Enhanced Metafile) file.
        public void Export(LocalReport report)
        {
            StringBuilder deviceInfosb = new StringBuilder();
            deviceInfosb.Append("<DeviceInfo>");
            deviceInfosb.Append("<OutputFormat>EMF</OutputFormat>");       
            deviceInfosb.Append(string.Format("</DeviceInfo>"));
            string deviceInfo = deviceInfosb.ToString();

            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream,
               out warnings);
            foreach (Stream stream in m_streams)
            {
                stream.Position = 0;               
            }
        }

        public void Print()
        {

            try
            {
                if (m_streams == null || m_streams.Count == 0)
                    return;
                PrintDocument printDoc = new PrintDocument();
                printDoc.PrinterSettings.PrinterName = _printerName;
                PrinterSettings ps = new PrinterSettings();
                ps.PrinterName = _printerName;
                PaperSize size = new PaperSize();
                size.Height = _printerHeight;
                size.Width = _printerWidth;
                Margins margin = new Margins();
                margin.Top = 0;
                margin.Bottom = 0;
                margin.Left = 0;
                margin.Right = 0;
                ps.DefaultPageSettings.Margins = margin;
                ps.DefaultPageSettings.PaperSize = size;                
                printDoc.PrinterSettings = ps;

                if (!printDoc.PrinterSettings.IsValid)
                {
                    string msg = String.Format(
                       "Can't find printer \"{0}\".", _printerName);
                    MessageBox.Show(msg, "Print Error");
                    return;
                }
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                printDoc.PrintController = new StandardPrintController(); //By Nick 
                printDoc.Print();
                printDoc.Dispose();
                m_streams.Clear();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        // Routine to provide to the report renderer, in order to
        //    save an image for each page of the report.
        private Stream CreateStream(string name,
          string fileNameExtension, Encoding encoding,
          string mimeType, bool willSeek)
        {
            string _path = @"Temp\{0}";
            _path = string.Format(_path, Guid.NewGuid().ToString());
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
            string _fileName = @"{0}.{1}";
            _path = String.Format(String.Format(@"{0}\{1}", _path, _fileName) , name, fileNameExtension);
                  
            Stream stream = new FileStream(_path, FileMode.Create);
            m_streams.Add(stream);
            return stream;
        }

        // Handler for PrintPageEvents
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[CurrentPageIndex]);
            ev.Graphics.DrawImage(pageImage, ev.PageBounds);
            CurrentPageIndex++;
            ev.HasMorePages = (CurrentPageIndex < m_streams.Count);
        }

    }
}

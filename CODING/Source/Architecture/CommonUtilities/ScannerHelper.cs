using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;

namespace PEA.BPM.Architecture.CommonUtilities
{
    /// <summary>
    /// For SerialPort Scanner
    /// </summary>
    public class ScannerHelper
    {
        private static SerialPort _sp;
        private static TextBoxBase _input;

        public static void SetInputTextBox(TextBoxBase textBox)
        {
            string portName = LocalSettingHelper.Instance().ReadString("ScannerPort");

            if (portName != null && portName != "USB")
            {
                if (_sp == null)
                {
                    _sp = new SerialPort(portName);
                    _sp.Open();
                    _sp.DataReceived += new SerialDataReceivedEventHandler(_sp_DataReceived);
                }
                _input = textBox;
            }
        }

        public static void CloseScanner()
        {
            if (_sp != null)
            {
                if (_sp.IsOpen)
                {
                    _sp.Close();
                }
                _sp.Dispose();
                _sp = null;
            }
        }

        static void _sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string scannedValue = _sp.ReadLine();
                _input.Invoke(new SetTextValueHandler(SetTextValue), new object[] { scannedValue });
            }
            catch { }
        }

        delegate void SetTextValueHandler(string value);
        static void SetTextValue(string value)
        {
            try
            {

                if (value.IndexOf('\r') > -1)
                {
                    value = value.Replace("\r", "");
                    _input.Focus();
                    _input.Text = value;
                    SendKeys.Send("{ENTER}");
                }
            }
            catch { }
        }

        static bool _switched = false;

        public static void SwitchKeyBoardToEnglish()
        {
            if (InputLanguage.CurrentInputLanguage.LayoutName.StartsWith("Thai"))
            {
                InputLanguageCollection installedLanguages = InputLanguage.InstalledInputLanguages;
                foreach (InputLanguage language in installedLanguages)
                    if (language.LayoutName.StartsWith("US"))
                    {
                        InputLanguage.CurrentInputLanguage = language;
                        _switched = true;
                    }
            }
        }

        public static void SwitchKeyBoardToDefault()
        {
            if (_switched)
            {
                InputLanguageCollection installedLanguages = InputLanguage.InstalledInputLanguages;
                foreach (InputLanguage language in installedLanguages)
                    if (language.LayoutName.StartsWith("Thai"))
                        InputLanguage.CurrentInputLanguage = language;
                _switched = false;
            }
        }
    }
}

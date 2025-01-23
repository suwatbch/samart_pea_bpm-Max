using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using PEA.BPM.Architecture.ArchitectureInterface.Constants;
using ProtoBuf;

namespace PEA.BPM.Architecture.CommonUtilities
{
    [DataContract]
    public class CompressData
    {
        private long _originalSize;
        private int _compressSize;
        private byte[] _data;

        [DataMember(Order = 1)]
        public long OriginalSize
        {
            get { return _originalSize; }
            set { _originalSize = value; }
        }

        [DataMember(Order = 2)]
        public int CompressSize
        {
            get { return _compressSize; }
            set { _compressSize = value; }
        }

        [DataMember(Order = 3)]
        public byte[] Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public CompressData() { }

        public CompressData(long originalSize, int compressSize, byte[] data)
        {
            _originalSize = originalSize;
            _compressSize = compressSize;
            _data = data;
        }
    }

    public class ServiceHelper
    {

        public static CompressData CompressData<T>(T c1s)
        {
            MemoryStream ptMs = new MemoryStream();
            Serializer.Serialize<T>(ptMs, c1s);

            MemoryStream ptCompressData = new MemoryStream();
            GZipStream ptGs = new GZipStream(ptCompressData, CompressionMode.Compress, true);

            byte[] raw = ptMs.ToArray();
            ptGs.Write(raw, 0, raw.Length);
            ptGs.Close();

            raw = ptCompressData.ToArray();

            return new CompressData(ptMs.Length, raw.Length, raw);
        }


        public static T DecompressData<T>(CompressData cds)
        {
            MemoryStream compressData = new MemoryStream(cds.Data);
            compressData.Position = 0;
            GZipStream gZip = new GZipStream(compressData, CompressionMode.Decompress);
            MemoryStream mx = ReadBytes(gZip);
            T c1s = Serializer.Deserialize<T>(mx);

            if(c1s == null)
            {
                return (T)Activator.CreateInstance(typeof(T));
            }
            else
            {
                return c1s;
            }
        }

        private static MemoryStream ReadBytes(Stream stream)
        {
            MemoryStream de = new MemoryStream();
            byte[] buffer = new byte[100];
            int readBytes = stream.Read(buffer, 0, buffer.Length);
            while (readBytes != 0)
            {
                de.Write(buffer, 0, readBytes);
                readBytes = stream.Read(buffer, 0, buffer.Length);
            }
            de.Position = 0;
            return de;
        }

        private static void ShowStopMsg(string caption, string msg)
        {
            MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }

        private static void ShowErrorMsg(string caption, string msg)
        {
            MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }

        private static void ShowWarningMsg(string caption, string msg)
        {
            MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }

        public static void TransformErrorMessage(Exception ex)
        {
            string x = ex.Message;
            if (x.IndexOf("TokenNotMatch") > -1)
            {
                WaitingFormHelper.HideWaitingForm();
                ShowStopMsg("Token not match", BPMException.TokenNotMatch);
                Environment.Exit(0);
            }
            else if (x.IndexOf("Unable to connect to the remote server") > -1)
            {
                WaitingFormHelper.HideWaitingForm();
                ShowErrorMsg("Connect fail", BPMException.ConnectionFail);
            }
            else if (x.IndexOf("NotFoundUser") > -1)
            {
                WaitingFormHelper.HideWaitingForm();
                ShowStopMsg("User not found", BPMException.NotFoundUser);
                Environment.Exit(0);
            }
            else if (x.IndexOf("TokenExpired") > -1)
            {
                WaitingFormHelper.HideWaitingForm();
                ShowStopMsg("Token expired", BPMException.TokenExpired);
                Environment.Exit(0);
            }
            else if (x.IndexOf("BPM Support") > -1)
            {
                WaitingFormHelper.HideWaitingForm();
                ShowStopMsg("ไม่อนุญาติ", BPMException.WrongConnection);
            }
            else
            {
                WaitingFormHelper.HideWaitingForm();
                ShowErrorMsg("ผิดพลาด", x);
            }
        }

        #region BinaryFormatter Compression

        public static CompressData CompressDataBF<T>(T c1s)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, c1s);

            MemoryStream compressData = new MemoryStream();
            GZipStream gs = new GZipStream(compressData, CompressionMode.Compress, true);

            byte[] b = ms.ToArray();
            gs.Write(b, 0, b.Length);
            gs.Close();

            b = compressData.ToArray();

            return new CompressData(ms.Length, b.Length, b);
        }

        public static T DecompressDataBF<T>(CompressData cds)
        {
            MemoryStream compressData = new MemoryStream(cds.Data);
            compressData.Position = 0;
            GZipStream gZip = new GZipStream(compressData, CompressionMode.Decompress);
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream mx = ReadBytes(gZip);
            T c1s = (T)bf.Deserialize(mx);

            return c1s;
        }

        #endregion
    }
}

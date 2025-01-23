using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;
using System.Collections;


namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    public class ListUtility
    {
        public static void WriteList<T>(List<T> t, string filePath) where T : IListUtility<T>
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                //Header
                IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("en-US");
                char[] spliter = { '.', '\\' };
                string[] sp = filePath.Split(spliter);
                string interfaceName = sp[sp.Length - 2].Substring(0, sp[sp.Length - 2].Length - 10);
                string running = sp[sp.Length - 2].Substring(interfaceName.Length, 10);
                string[] hh = { "HH", interfaceName, DateTime.Now.ToString("yyyyMMddhhmmss", formatDate), running };
                sb.Append(string.Join("|", hh));
                sb.Append("\r\n");

                foreach (T obj in t)
                {
                    sb.Append(string.Format("BB|{0}", obj.ToStream()));
                    sb.Append("\r\n");
                }

                //Footer            
                sb.Append(string.Format("FF|{0}", t.Count.ToString().PadLeft(10, '0')));
                sb.Append("\r\n");

                using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default))
                {
                    sw.Write(sb.ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static ArrayList WriteFileGenerateList<T>(List<T> t, string filePath) where T : IListUtility<T>
        {
            try
            {
                ArrayList al = new ArrayList();
                StringBuilder sb = new StringBuilder();

                //Header
                IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("en-US");
                char[] spliter = { '.', '\\' };
                string[] sp = filePath.Split(spliter);
                string interfaceName = sp[sp.Length - 2].Substring(0, sp[sp.Length - 2].Length - 10);
                string running = sp[sp.Length - 2].Substring(interfaceName.Length, 10);
                string[] hh = { "HH", interfaceName, DateTime.Now.ToString("yyyyMMddhhmmss", formatDate), running };
                string fileHeader = string.Join("|", hh);
                al.Add(fileHeader);
                sb.Append(fileHeader);
                sb.Append("\r\n");

                foreach (T obj in t)
                {
                    string fileBody = string.Format("BB|{0}", obj.ToStream());
                    al.Add(fileBody);
                    sb.Append(fileBody);
                    sb.Append("\r\n");
                }

                //Footer
                string fileFooter = string.Format("FF|{0}", t.Count.ToString().PadLeft(10, '0'));
                al.Add(fileFooter);
                sb.Append(fileFooter);
                sb.Append("\r\n");

                using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default))
                {
                    sw.Write(sb.ToString());
                }

                return al;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void WriteList<T>(List<T> t, string filePath, decimal? actualAmount, decimal? amount, decimal? overUnder) where T : IListUtility<T>, new()
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                //Header
                IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("en-US");
                char[] spliter = { '.', '\\' };
                string[] sp = filePath.Split(spliter);
                string interfaceName = sp[sp.Length - 2].Substring(0, sp[sp.Length - 2].Length - 10);
                string running = sp[sp.Length - 2].Substring(interfaceName.Length, 10);
                string[] hh = { "HH", interfaceName, DateTime.Now.ToString("yyyyMMddhhmmss", formatDate), running };
                sb.Append(string.Join("|", hh));
                sb.Append("\r\n");

                foreach (T obj in t)
                {
                    sb.Append(string.Format("BB|{0}", obj.ToStream()));
                    sb.Append("\r\n");
                }

                //Footer            
                sb.Append(string.Format("FF|{0}|{1}|{2}|{3}", t.Count.ToString().PadLeft(10, '0'), actualAmount.Value.ToString("#0.00"), amount.Value.ToString("#0.00"), overUnder.Value.ToString("#0.00")));
                sb.Append("\r\n");

                using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default))
                {
                    sw.Write(sb.ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //BPM disconnection format
        public static void WriteList<T>(List<T> t, string branchId, string filePath, string fileName) where T : IListUtility<T>
        {
            try
            {
                string destination = string.Format("{0}\\{1}", filePath, fileName);
                StringBuilder sb = new StringBuilder();

                //create things in memory
                foreach (T obj in t)
                {
                    sb.Append(string.Format("{0}", obj.ToStream()));
                    sb.Append("\r\n");
                }

                //write memory to destination
                using (StreamWriter sw = new StreamWriter(destination, false, Encoding.Default))
                {
                    sw.Write(sb.ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static void Write(string str, string filePath) 
        {
            try
            {
                StringBuilder sb = new StringBuilder(str);
                using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default))
                {
                    sw.Write(sb.ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<T> ReadToList<T>(string filePath) where T : IListUtility<T>, new()
        {
            List<T> _ts = new List<T>();

            try
            {
                using (StreamReader sd = new StreamReader(filePath, Encoding.Default))
                {
                    //Remove Header and Footer
                    while (!sd.EndOfStream)
                    {
                        string x = sd.ReadLine();
                        if (x.TrimStart(' ').StartsWith("HH", StringComparison.CurrentCultureIgnoreCase) ||
                            x.TrimStart(' ').StartsWith("FF", StringComparison.CurrentCultureIgnoreCase))
                            continue;

                        T _t = new T();
                        _ts.Add(_t.ParseExtract(x));
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }

            return _ts;
        }
    }
}

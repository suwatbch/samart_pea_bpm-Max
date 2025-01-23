using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Globalization;

namespace PEA.BPM.Integration.BPMIntegration
{
    public class SAPFile
    {
        public static void MoveToHistory(string file)
        {
            string dtstr = DateTime.Now.ToString("ddMMyyyyhhmmsstt");
            char[] spliters = {'/', '\\', '.' };
            string []sp = file.Split(spliters);
            string destinationFilePath = string.Format("{0}{1}.{2}.{3}", IntegrationFileNames.SyncFileHistoryDir + sp[sp.Length-2], dtstr , sp[sp.Length-1]);
            File.Move(file, destinationFilePath);
        }

        //handle DL01, DL02
        public static List<string> GetFileName(string path, string key)
        {
            //get all file in the specified directory and insert key to file name
            List<string> keyFiles = new List<string>();
            string[] fileList = Directory.GetFiles(path);
            char[] spliter = { '.', '\\' };

            Array.Sort(fileList, new CompareFileInfo());
            for (int j = 0; j < fileList.Length; j++)
            {
                string fileName = fileList[j];

                if (!fileList[j].Contains("-"))
                {
                    string[] sp = fileList[j].Split(spliter);
                    string name = sp[sp.Length - 2];

                    int infLength = 7;
                    if (name.StartsWith("MTR0090A") || name.StartsWith("MTR0090B") || name.StartsWith("MTR0090C") || name.StartsWith("MTR0090D") || name.StartsWith("MTR0090E")
                        || name.StartsWith("MTR0060A") || name.StartsWith("MTR0060B"))
                        infLength = 8;                    
                    
                    string infName = name.Substring(0, infLength);
                    string running = name.Substring(infLength, name.Length - infLength);
                    string keyName = string.Format("{0}-{1}-{2}.txt", infName, key, running);
                    
                    fileName = string.Format("{0}\\{1}", path, keyName);

                    File.Move(fileList[j], fileName );
                }

                keyFiles.Add(fileName);
            }

            return keyFiles;
        }

        public static List<string> GetFileNameX(string path, string key)
        {
            //INVSTATUS
            //BILLUPDATX200902051646085116830.txt
            //TRR0011X200902111022352760660.txt

            //get all file in the specified directory and insert key to file name
            List<string> keyFiles = new List<string>();
            string[] fileList = Directory.GetFiles(path);
            char[] spliter = { '.', '\\' };

            Array.Sort(fileList, new CompareFileInfo());
            for (int j = 0; j < fileList.Length; j++)
            {
                string fileName = fileList[j];

                if (!fileList[j].Contains("-"))
                {
                    string[] sp = fileList[j].Split(spliter);
                    string name = sp[sp.Length - 2];

                    int infLength;
                    if (name.StartsWith("TRR0010A") || name.StartsWith("TRR0010B") || name.StartsWith("TRR0010C") || name.StartsWith("TRR0010D") || name.StartsWith("TRR0010E")
                        || name.StartsWith("TRR0010F") || name.StartsWith("TRR0010N") || name.StartsWith("TRR0010X") || name.StartsWith("TRR0011A") || name.StartsWith("TRR0011X")
                        || name.StartsWith("TRR0020A") || name.StartsWith("TRR0020B") || name.StartsWith("TRR0020C") || name.StartsWith("TRR00200") || name.StartsWith("RECEIPTX")
                        || name.StartsWith("TRR00500") || name.StartsWith("TRR0050A"))
                        infLength = 8;
                    else if (name.Contains("BILLUPDATX") || name.Contains("BILLSTATUS") || name.Contains("BILLUPDATE"))
                        infLength = 10;
                    else if (name.Contains("billreverse"))
                        infLength = 11;
                    else if (name.Contains("INVSTATUS") || name.Contains("INVREPORT"))
                        infLength = 9;
                    else //TRR0040, TRR0045, TRR0050
                        infLength = 7;
                    

                    string infName = name.Substring(0, infLength);
                    string running = name.Substring(infLength, name.Length - infLength);
                    string keyName = string.Format("{0}-{1}-{2}.txt", infName, key, running);
                    fileName = string.Format("{0}\\{1}", path, keyName);
                    File.Move(fileList[j], fileName);
                }

                keyFiles.Add(fileName);
            }

            return keyFiles;
        }

        //find suitable key in file pool
        public static string FindSuitableKey()
        {
            //just use datetime as a key
            IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("en-US");
            string suitKey = DateTime.Now.ToString("yyyyMMddhhmmss", formatDate);
            return suitKey;
        }

        public static bool ValidateOnline(string fileName)
        {
            bool ret = true;
            int recordCount = 0;
            int recordSum = 0;
            int numOfSimbol = -1;

            try
            {
                char[] spliter = { '\\', '.'};
                string[] fp = fileName.Split(spliter);
                string tail = fp[fp.Length - 2];
                string infName = tail.Substring(0, 8);
                string docNum = tail.Substring(8, 12);
                string crDate = tail.Substring(20, 14);

                using (StreamReader sd = new StreamReader(fileName, Encoding.Default))
                {
                    //Remove Header and Footer
                    while (!sd.EndOfStream)
                    {
                        string x = sd.ReadLine();
                        if (x.TrimStart(' ').StartsWith("HH", StringComparison.CurrentCultureIgnoreCase))
                        {
                            string [] sp = x.Split('|');
                            if ((sp[1] != infName) || (sp[2] != crDate) || (sp[3] != docNum)) 
                                ret = false;
                        }
                        else if(x.TrimStart(' ').StartsWith("FF", StringComparison.CurrentCultureIgnoreCase))
                        {
                            string[] sp = x.Split('|');
                            recordSum = Convert.ToInt32(sp[1]);
                        }
                        else if (x.TrimStart(' ').StartsWith("BB", StringComparison.CurrentCultureIgnoreCase))
                        {
                            recordCount++;
                            //verify number of "|" sign
                            string[] sp = x.Split('|');
                            int simbolCountOfLine = sp.Length - 1;
                            if (numOfSimbol == -1)
                                numOfSimbol = simbolCountOfLine;
                            else if (numOfSimbol != simbolCountOfLine)
                                ret = false;
                        }
                        else //start with other simbols
                        {
                            ret = false;
                        }
                    }
                }

                if (recordCount != recordSum)
                    return false;
                else
                    return ret;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public static bool Validate(string fileName, out string errMsg)
        {
            //bool ret = true;
            int recordCount = 0;
            int recordSum = 0;
            int numOfSimbol = -1;

            try
            {
                char[] spliter = { '\\', '.', '-' };
                string[] fp = fileName.Split(spliter);
                string interfaceName = fp[fp.Length - 4];
                string running = fp[fp.Length - 2];

                using (StreamReader sd = new StreamReader(fileName, Encoding.Default))
                {
                    //Remove Header and Footer
                    while (!sd.EndOfStream)
                    {
                        string x = sd.ReadLine();
                        if (x.TrimStart(' ').StartsWith("HH", StringComparison.CurrentCultureIgnoreCase))
                        {
                            string [] sp = x.Split('|');
                            if( (sp[1] != interfaceName) || (sp[3] != running))
                            {
                                errMsg = "ข้อมูลในส่วนของ Header ผิดพลาด";
                                return false;
                            }
                        }
                        else if(x.TrimStart(' ').StartsWith("FF", StringComparison.CurrentCultureIgnoreCase))
                        {
                            string[] sp = x.Split('|');
                            recordSum = Convert.ToInt32(sp[1]);
                        }
                        else if (x.TrimStart(' ').StartsWith("BB", StringComparison.CurrentCultureIgnoreCase))
                        {
                            recordCount++;
                            //verify number of "|" sign
                            string[] sp = x.Split('|');
                            int simbolCountOfLine = sp.Length - 1;
                            if (numOfSimbol == -1)
                            {
                                numOfSimbol = simbolCountOfLine;
                            }
                            else if (numOfSimbol != simbolCountOfLine)
                            {
                                errMsg = "ข้อมูลในส่วนของ Body ผิดพลาด";
                                return false;
                            }
                        }
                        else //start with other simbols
                        {
                            errMsg = "ส่วนหัวของ record ไม่ได้เริ่มต้นด้วย HH, BB หรือ FF";
                            return false;                            
                        }
                    }
                }

                if (recordCount != recordSum)
                {
                    errMsg = "จำนวนรวม record ที่ระบุไว้ที่ FF ไม่ตรงกับ จำนวน record ที่ระบุไว้ที่ BB";
                    return false;
                }

                //no error detected
                errMsg = "";
                return true;
            }
            catch (Exception e)
            {
                errMsg = e.ToString();
                return false;
            }
        }
    }

    public class CompareFileInfo : IComparer
    {
        public int Compare(object x, object y)
        {
            return string.Compare((string)x, (string)y);
        }
    }
}

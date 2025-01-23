using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

namespace PEA.BPM.ePaymentsModule.Utilities
{
    public static class FileManagement
    {
        public static ArrayList ReadTextFile(string filePath)
        {
            StreamReader objReader = new StreamReader(filePath);
            string sLine = "";
            ArrayList arrText = new ArrayList();

            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null)
                    arrText.Add(sLine);
            }
            objReader.Close();
            return arrText;
        }
    }
}

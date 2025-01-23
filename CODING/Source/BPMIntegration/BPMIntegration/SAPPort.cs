using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using System.Collections;


namespace PEA.BPM.Integration.BPMIntegration
{
    public class SAPPort
    {
        private string _outFileName;
        private string _inFileName;

        public SAPPort()
        {

        }

        public SAPPort(string outFileName)
        {
            _outFileName = outFileName;
        }

        public SAPPort(string inFileName, string outFileName)
        {
            _inFileName = inFileName;
            _outFileName = outFileName;
        }

        public string OutFileName
        {
            get { return _outFileName; }
            set { _outFileName = value; }
        }

        public string InFileName
        {
            get { return _inFileName; }
            set { _inFileName = value; }
        }

        public void SendList<T>(List<T> obj) where T : IListUtility<T>
        {
            ListUtility.WriteList<T>(obj, _outFileName);
        }

        public ArrayList SendListAG<T>(List<T> obj) where T : IListUtility<T>
        {
            return ListUtility.WriteFileGenerateList<T>(obj, _outFileName);
        }

        public void SendList<T>(List<T> obj, decimal? actualAmount, decimal? amount, decimal? overUnder) where T : IListUtility<T>, new()
        {
            ListUtility.WriteList<T>(obj, _outFileName, actualAmount, amount, overUnder);
        }

        //reconnection
        public void SendList<T>(List<T> obj, string branchId, string fileName) where T : IListUtility<T>, new()
        {
            ListUtility.WriteList<T>(obj, branchId, _outFileName, fileName);
        }

        public void Send(string obj) 
        {
            ListUtility.Write(obj, _outFileName);
        }

        public List<T> Receive<T>() where T : IListUtility<T>, new()
        {
            List<T> ret = null;
            try
            {
                ret = ListUtility.ReadToList<T>(_inFileName);
            }
            catch(Exception)
            {
                throw;
            }

            return ret;
        }


    }
}

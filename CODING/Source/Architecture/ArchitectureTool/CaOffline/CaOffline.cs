using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using System.IO;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using PEA.BPM.Architecture.ArchitectureSG;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureInterface.Constants;
using System.Globalization;
using System.Collections;
using System.Windows.Forms;
using System.Linq;
using PEA.BPM.Architecture.ArchitectureTool.Resources;

namespace PEA.BPM.Architecture.ArchitectureTool
{
    [Serializable]
    public class CaOffline
    {
      
        private static CaOffline _co = new CaOffline();
        private List<ContractAccountOffline> _contractAccountOffline;

        private DataSet _dtCa;

        const string _cacheDataPath = BPMPath.ConfigPath + "\\XMLData";
        string _cacheDataFileName = _cacheDataPath + "\\CAOffline.XML";
        //string _cacheSchemaFileName = _cacheDataPath + "\\CAOffline.xsd";

        

        public static CaOffline Instant
        {
            get
            {
                return _co;
            }
        }

        private CaOffline()
        {
            if (!Directory.Exists(_cacheDataPath))
            {
                Directory.CreateDirectory(_cacheDataPath);
            }

            if (this.LoadData())
            {
                _contractAccountOffline = (List<ContractAccountOffline>)this.GetData("ContractAccount");
                
            }
            else
            {
                MessageBox.Show("โปรแกรมไม่สามารถอ่านไฟล์  "+_cacheDataFileName+ " ได้",  "ผิดพลาด",
                                MessageBoxButtons.OK, MessageBoxIcon.Error,   MessageBoxDefaultButton.Button1, 
                                MessageBoxOptions.DefaultDesktopOnly);

                //If Error then Add initial data 1 row
                _contractAccountOffline = new List<ContractAccountOffline>();

                    //_contractAccountOffline.Add(
                    //    new ContractAccountOffline(
                    //        "" ,  // Caid
                    //        "" ,  // CaName
                    //        "" ,  // CaTaxId
                    //        "" ,  // CaTaxBranch
                    //        "" ,  // CaAddress
                    //        ""    // TechBranchId
                    //    )
                    //    );

            }
        }

        //public ICodeTableService GetService()
        //{
        //    return CodeTableSG.Instance(Session.Branch.OnlineConnection);
        //}

        public bool LoadData()
        {

            bool ret = false;
            _dtCa = new DataSet();

            if (!Session.IsNetworkConnectionAvailable && Session.User.Id != null)
            {
                if (File.Exists(_cacheDataFileName))
                {
                    try
                    {
                        _dtCa = LoadDataFromFile();
                        ret = true;
                    }
                    catch (Exception e)
                    {
                        //ret = false;
                        //MessageBox.Show(e.Message.ToString(), "ผิดพลาด",
                        //        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                        //        MessageBoxOptions.DefaultDesktopOnly);
                    }
                }

            }
            else //load from file
            {
                if (File.Exists(_cacheDataFileName))
                {
                    try
                    {
                        _dtCa = LoadDataFromFile();
                        ret = true;
                    }
                    catch (Exception e)
                    {
                        //ret = false;
                        //MessageBox.Show(e.Message.ToString(), "ผิดพลาด",
                        //        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                        //        MessageBoxOptions.DefaultDesktopOnly);
                    }
                }
            }

            return ret;
        }
 
        private DataSet LoadDataFromFile()
        {
            NewDataSet _dtCa = new NewDataSet();  //เพิ่ม DataSet โดย Import จากไฟล์ CaOffline.xsd

            //_dtCa.ReadXmlSchema(_cacheSchemaFileName);
            _dtCa.ReadXml(_cacheDataFileName);

            return _dtCa;
        }


        internal object GetData(string type)
        {
            DataTable dt;
            dt = _dtCa.Tables["ContractAccount"];

            _contractAccountOffline = new List<ContractAccountOffline>();
            foreach (DataRow dr in dt.Rows)
            {
                _contractAccountOffline.Add(
                    new ContractAccountOffline(
                        DaHelper.GetString(dr, "CaId"),
                        DaHelper.GetString(dr, "CaName"),
                        DaHelper.GetString(dr, "CaTaxId"),
                        DaHelper.GetString(dr, "CaTaxBranch"),
                        DaHelper.GetString(dr, "CaAddress"),
                        DaHelper.GetString(dr, "TechBranchId")
                    )
                    );
            }
            return _contractAccountOffline;
        }



        public List<ContractAccountOffline> ListContractAccountOffline(string caid)
        {
            if (_contractAccountOffline != null)
            {
                return this._contractAccountOffline.FindAll(delegate(ContractAccountOffline cao)
                {
                    return cao.CaId == caid;
                }
                );
            }
            return _contractAccountOffline;
        }

    }
}

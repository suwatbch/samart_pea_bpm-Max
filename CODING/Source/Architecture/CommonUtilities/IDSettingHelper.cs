using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO.IsolatedStorage;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using PEA.BPM.Architecture.ArchitectureInterface.Constants;

namespace PEA.BPM.Architecture.CommonUtilities
{
    public class IDSettingHelper
    {
        private string m_SettingsFileName = BPMPath.ConfigPath + "\\AppConfig\\BpmIDSetting.dat"; 
        public Hashtable m_Data;
        private static IDSettingHelper uch;
        public const string BranchId = "BranchId";
        

        public static IDSettingHelper Instance()
        {          
            uch = new IDSettingHelper();
            return uch;
        }

        private IDSettingHelper()
        {
            try
            {
                string configFolder = BPMPath.ConfigPath + "\\AppConfig";
                if (!Directory.Exists(configFolder))
                    Directory.CreateDirectory(configFolder);
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "Create config folder failed!", string.Format("Source: {0}, Stack: {1}", ex.Source, ex.ToString()));
            }

            m_Data = new Hashtable();
            Load();
        }

        public object Read(string key, IDSettingHelper hp)
        {
            object ret = null;
            //reload value
            m_Data = hp.m_Data;
            ret = m_Data[key];

            return ret;
        }

        public void Add(string key, object value)
        {
            if (m_Data.Contains(key))
                m_Data[key] = value;
            else
                m_Data.Add(key, value);
        }

        public void Update(string key, object value)
        {
            m_Data[key] = value;
        }

        private void Load()
        {
            FileStream fs = null;

            try
            {
                fs = new FileStream(m_SettingsFileName, FileMode.OpenOrCreate);
                if (fs.Length > 0)
                {
                    IFormatter formatter = new BinaryFormatter();
                    m_Data = (Hashtable)formatter.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                //warning! setting value will be gone, not good tell programmer instead of auto new
                Logger.WriteError(Logger.Module.POS, "Loading Config file currupted!", string.Format("Source: {0}, Stack: {1}", ex.Source, ex.ToString()));
            }
            finally
            {
                if (null != fs)
                {
                    fs.Flush();
                    fs.Close();
                }
            }
        }

        public void Save(IDSettingHelper hp)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(m_SettingsFileName, FileMode.Create);
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, hp.m_Data);
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "Saving Config file currupted!", string.Format("Source: {0}, Stack: {1}", ex.Source, ex.ToString()));
            }
            finally
            {
                if (null != fs)
                {
                    fs.Flush();
                    fs.Close();
                }
            }
        }
    }
}

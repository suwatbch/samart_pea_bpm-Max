using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO.IsolatedStorage;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using PEA.BPM.Architecture.ArchitectureInterface.Constants;

namespace PEA.BPM.Architecture.CommonUtilities
{
    public class LocalSettingHelper
    {
        private object syncRoot = new object();

        private string m_SettingsFileName = BPMPath.ConfigPath + "\\BpmSetting.dat";
        private Hashtable m_Data;
        private static LocalSettingHelper uch;
        private Semaphore m_sm;

        public const string BranchId = "BranchId";

        public static LocalSettingHelper Instance()
        {            
            if (uch == null)
            {
                uch = new LocalSettingHelper();
            }

            return uch;
        }

        private LocalSettingHelper()
        {
            m_sm = new Semaphore(1, 1);
            m_Data = new Hashtable();
        }

        public object Read(string key)
        {
            m_sm.WaitOne();
            object ret = null;
            //reload value
            Load();
            ret = m_Data[key];
            m_sm.Release();

            return ret;
        }

        public string ReadString(string key)
        {
            m_sm.WaitOne();

            string ret = null;
            Load();
            ret = (string)m_Data[key];
            m_sm.Release();

            return ret;
        }

        public void Add(string key, object value)
        {
            m_sm.WaitOne();
            Load();
            if (m_Data.Contains(key))
                m_Data[key] = value;
            else
                m_Data.Add(key, value);

            Save();

            m_sm.Release();
        }

        public void Update(string key, object value)
        {
            m_sm.WaitOne();

            Load();
            m_Data[key] = value;
            Save();

            m_sm.Release();
        }

        public void Remove(string key)
        {
            m_sm.WaitOne();
            Load();
            m_Data.Remove(key);
            Save();
            m_sm.Release();
        }

        public void ClearRegistered()
        {
            Remove("BranchId");
            Remove("BranchLevel");
            Remove("BranchName");
            Remove("BranchName2");
            Remove("BranchAddress");
            Remove("BranchNo");
            Remove("PosId");
            Remove("PosNo");
            Remove("TaxId");
            Remove("BACode");
            Remove("CenterServerWsAddress");
            Remove("BranchServerWsAddress");
        }

        public void Clear()
        {
            m_sm.WaitOne();
            m_Data.Clear();
            Save();
            m_sm.Release();
        }

        private void Load()
        {
            //System.Diagnostics.Debugger.Break();

            FileStream fs = null;

            try
            {
                fs = new FileStream(m_SettingsFileName, FileMode.Open);
                if (fs.Length > 0)
                {
                    IFormatter formatter = new BinaryFormatter();
                    m_Data = (Hashtable)formatter.Deserialize(fs);
                }
                
            }
            catch (Exception ex)
            {
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

            /*
            IsolatedStorageFile isoStore = IsolatedStorageFile.GetMachineStoreForAssembly();
                IsolatedStorageFile.GetStore(
                IsolatedStorageScope.Machine | IsolatedStorageScope.Assembly,
                null, null);

            Stream stream;
            try
            {
                stream = new IsolatedStorageFileStream(m_SettingsFileName,
                    FileMode.Open, isoStore);

                if (stream != null && stream.Length > 0)
                {
                    try
                    {
                        IFormatter formatter = new BinaryFormatter();
                        m_Data = (Hashtable)formatter.Deserialize(stream);
                    }
                    finally
                    {
                        stream.Flush();
                        stream.Close();
                    }
                }
                isoStore.Close();
            }
            catch (Exception ex)
            {
                m_Data = new Hashtable();
            }
             */
        }

        private void Save()
        {
            //System.Diagnostics.Debugger.Break();

            lock (syncRoot)
            {
                FileStream fs = null;
                try
                {
                    fs = new FileStream(m_SettingsFileName, FileMode.Create);
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, m_Data);
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

                /*
                IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(
                    IsolatedStorageScope.Machine | IsolatedStorageScope.Assembly,
                    null, null);

                Stream stream = new IsolatedStorageFileStream(m_SettingsFileName,
                    FileMode.Create, isoStore);

                if (stream != null)
                {
                    try
                    {
                        IFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(stream, m_Data);
                    }
                    finally
                    {
                        stream.Close();
                    }
                }
                 */
            }
        }
    }
}

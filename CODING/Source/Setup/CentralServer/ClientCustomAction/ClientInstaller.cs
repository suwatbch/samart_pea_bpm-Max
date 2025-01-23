using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace PEA.BPM.Setup.Client.CustomAction
{
    [RunInstaller(true)]
    public class ClientInstaller: System.Configuration.Install.Installer
    {

        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);

            string oldConfigPath = "C:\\Program Files\\Portalnet\\BPMClient";

            string targetDir = Context.Parameters["targetDir"];
            string connMode = Context.Parameters["connMode"];
            string centerServerWsUrl = Context.Parameters["centerServerWsUrl"];
            string branchServerWsUrl = Context.Parameters["branchServerWsUrl"];

            try
            {
                //create a new config path
                string configPath = string.Format("C:\\BPM\\BPMClient");
                if (!Directory.Exists(configPath))
                    Directory.CreateDirectory(configPath);

                //migrate the current configuration for last version
                //BPmSetting.dat
                string settingsFileName = string.Format("{0}\\BpmSetting.dat", configPath);
                string oldSettingFileName = string.Format("{0}\\BpmSetting.dat", oldConfigPath);


                if (!File.Exists(settingsFileName) && !File.Exists(oldSettingFileName))
                {
                    Hashtable settingData = new Hashtable();
                    settingData.Add("Online", ((bool)(connMode == "ONLINE")).ToString());
                    settingData.Add("CenterServerWsAddress", centerServerWsUrl);
                    settingData.Add("BranchServerWsAddress", branchServerWsUrl);
                    FileStream fs = new FileStream(settingsFileName, FileMode.Create);
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, settingData);
                    fs.Flush();
                    fs.Close();
                }
                else if (!File.Exists(settingsFileName) && File.Exists(oldSettingFileName))
                {
                    File.Move(oldSettingFileName, settingsFileName);
                }

                //receipt Id
                string receiptPath = string.Format("{0}\\AppConfig", configPath);
                string oldReceiptPath = string.Format("{0}\\AppConfig", oldConfigPath);

                if (!Directory.Exists(receiptPath))
                {
                    //port to new destination
                    if (Directory.Exists(oldReceiptPath))
                        Directory.Move(oldReceiptPath, receiptPath);
                    else
                        Directory.CreateDirectory(receiptPath);
                }


                //offlineData
                string offlinePath = string.Format("{0}\\offlineData", configPath);
                string oldOfflinePath = string.Format("{0}\\offlineData", oldConfigPath);

                if (!Directory.Exists(offlinePath))
                {
                    //port to new destination
                    if (Directory.Exists(oldOfflinePath))
                        Directory.Move(oldOfflinePath, offlinePath);
                    else
                        Directory.CreateDirectory(offlinePath);
                }


                //slipPrintingPool
                string slipPrintingPoolPath = string.Format("{0}\\slipPrintingPool", configPath);
                string oldSlipPrintingPoolPath = string.Format("{0}\\slipPrintingPool", oldConfigPath);

                if (!Directory.Exists(slipPrintingPoolPath))
                {
                    //port to new destination
                    if (Directory.Exists(oldSlipPrintingPoolPath))
                        Directory.Move(oldSlipPrintingPoolPath, slipPrintingPoolPath);
                    else
                        Directory.CreateDirectory(slipPrintingPoolPath);
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                throw;
            }
        }
    }
}

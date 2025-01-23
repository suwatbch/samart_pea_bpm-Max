using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using TaskScheduler;

namespace BaseCustomAction
{
    public class DatabaseAccount
    {
        public string SettingName;
        public string ServerDbName;
        public string ServerName;
        public string UserID;
        public string Password;

        public DatabaseAccount(string settingName, string serverDbName, 
            string serverName, string userID, string password)
        {
            SettingName = settingName;
            ServerDbName = serverDbName;
            ServerName = serverName;
            UserID = userID;
            Password = password;
        }

        public string GetConnectionString()
        {
            return string.Format("Database={0};Server={1};Integrated Security=False;uid={2};pwd={3};Connection Timeout=300;",
                    new object[] { ServerDbName, ServerName, UserID, Password });
        }
    }

    public class InstallerHelper
    {
        public static void ValidateConnectionString(string connString)
        {
            using (SqlConnection conn =
                    new SqlConnection(connString))
            {
                conn.Open();
                conn.Close();
            }
        }

        public static void AddConnnectionString(Configuration config, List<DatabaseAccount> dbAccounts)
        {
            AddConnnectionString(config, dbAccounts, true);
        }

        public static void AddConnnectionString(Configuration config, List<DatabaseAccount> dbAccounts, bool encryptConnectionString)
        {
            // Clear existing connection string
            config.ConnectionStrings.ConnectionStrings.Clear();

            foreach (DatabaseAccount dbAccount in dbAccounts)
            {
                string connString = dbAccount.GetConnectionString();                
                ValidateConnectionString(connString);

                ConnectionStringSettings setting = new ConnectionStringSettings();
                setting.Name = dbAccount.SettingName;
                setting.ProviderName = "System.Data.SqlClient";
                setting.ConnectionString = connString;                
                config.ConnectionStrings.ConnectionStrings.Add(setting);
            }

            if (encryptConnectionString)
            {
                // Encrypt the connection strings section
                ConfigurationSection section = config.GetSection("connectionStrings");
                if (section != null && section.IsReadOnly() == false)
                {
                    section.SectionInformation.ProtectSection("RsaProtectedConfigurationProvider");
                    section.SectionInformation.ForceSave = true;
                }
            }
        }


        public static void AddScheduledTask(ScheduledTasks st, string targetDir, string taskUserID, string taskPassword,
            string taskName, int interval, short startingMin)
        {
            Task t = st.OpenTask(taskName);
            if (null != t)
            {
                st.DeleteTask(taskName);
            }
            t = st.CreateTask(taskName);

            t.WorkingDirectory = targetDir;
            t.ApplicationName = string.Format("{0}BPMScheduler.exe", targetDir);
            t.Parameters = string.Format("{0} BPMDbServer", taskName);
            t.SetAccountInformation(taskUserID, taskPassword);

            if (interval > 0)
            {
                DailyTrigger dt = new DailyTrigger(1, startingMin);
                dt.DurationMinutes = 1439;
                dt.IntervalMinutes = interval;
                t.Triggers.Add(dt);
            }

            t.Save();
        }

        public static void RemoveScheduledTask(ScheduledTasks st, string taskName)
        {
            Task t = st.OpenTask(taskName);
            if (null != t)
            {
                st.DeleteTask(taskName);
            }
        }
    }
}

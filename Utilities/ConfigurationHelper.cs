using System;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DialysisManagement.Utilities
{
    /// <summary>
    /// Helper per gestione configurazioni
    /// </summary>
    public static class ConfigurationHelper
    {
        private static readonly string ConfigFilePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

        /// <summary>
        /// Legge impostazione da app.config o appsettings.json
        /// </summary>
        public static string GetAppSetting(string key, string defaultValue = null)
        {
            try
            {
                // Prova prima da app.config
                var value = ConfigurationManager.AppSettings[key];
                if (!string.IsNullOrEmpty(value))
                    return value;

                // Prova da appsettings.json
                if (File.Exists(ConfigFilePath))
                {
                    var json = File.ReadAllText(ConfigFilePath);
                    var config = JObject.Parse(json);
                    var settingValue = config.SelectToken($"AppSettings.{key}")?.ToString();

                    if (!string.IsNullOrEmpty(settingValue))
                        return settingValue;
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Legge impostazione come booleano
        /// </summary>
        public static bool GetAppSettingBool(string key, bool defaultValue = false)
        {
            var value = GetAppSetting(key);
            if (bool.TryParse(value, out bool result))
                return result;

            return defaultValue;
        }

        /// <summary>
        /// Legge impostazione come intero
        /// </summary>
        public static int GetAppSettingInt(string key, int defaultValue = 0)
        {
            var value = GetAppSetting(key);
            if (int.TryParse(value, out int result))
                return result;

            return defaultValue;
        }

        /// <summary>
        /// Imposta valore in appsettings.json
        /// </summary>
        public static bool SetAppSetting(string key, string value)
        {
            try
            {
                JObject config;

                if (File.Exists(ConfigFilePath))
                {
                    var json = File.ReadAllText(ConfigFilePath);
                    config = JObject.Parse(json);
                }
                else
                {
                    config = new JObject();
                }

                // Crea nodo AppSettings se non esiste
                if (config["AppSettings"] == null)
                {
                    config["AppSettings"] = new JObject();
                }

                config["AppSettings"][key] = value;

                File.WriteAllText(ConfigFilePath, config.ToString(Formatting.Indented));
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Legge connection string
        /// </summary>
        public static string GetConnectionString(string name = "DefaultConnection")
        {
            try
            {
                // Prova da app.config
                var connString = ConfigurationManager.ConnectionStrings[name]?.ConnectionString;
                if (!string.IsNullOrEmpty(connString))
                    return connString;

                // Prova da appsettings.json
                if (File.Exists(ConfigFilePath))
                {
                    var json = File.ReadAllText(ConfigFilePath);
                    var config = JObject.Parse(json);
                    var value = config.SelectToken($"ConnectionStrings.{name}")?.ToString();

                    if (!string.IsNullOrEmpty(value))
                        return value;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Verifica se applicazione è in modalità debug
        /// </summary>
        public static bool IsDebugMode()
        {
#if DEBUG
            return true;
#else
                return GetAppSettingBool("DebugMode", false);
#endif
        }

        /// <summary>
        /// Restituisce percorso base applicazione
        /// </summary>
        public static string GetBasePath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// Restituisce percorso dati
        /// </summary>
        public static string GetDataPath()
        {
            var path = Path.Combine(GetBasePath(), "Data");
            Directory.CreateDirectory(path);
            return path;
        }

        /// <summary>
        /// Restituisce percorso backup
        /// </summary>
        public static string GetBackupPath()
        {
            var path = Path.Combine(GetBasePath(), "Backups");
            Directory.CreateDirectory(path);
            return path;
        }

        /// <summary>
        /// Restituisce percorso log
        /// </summary>
        public static string GetLogPath()
        {
            var path = Path.Combine(GetBasePath(), "Logs");
            Directory.CreateDirectory(path);
            return path;
        }

        /// <summary>
        /// Restituisce percorso reports
        /// </summary>
        public static string GetReportsPath()
        {
            var path = Path.Combine(GetBasePath(), "Reports");
            Directory.CreateDirectory(path);
            return path;
        }

        /// <summary>
        /// Restituisce percorso temp
        /// </summary>
        public static string GetTempPath()
        {
            var path = Path.Combine(GetBasePath(), "Temp");
            Directory.CreateDirectory(path);
            return path;
        }
    }
}

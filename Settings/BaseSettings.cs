using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings
{
    public static class BaseSettings
    {
        /// <summary>
        /// Default settings value
        /// </summary>
        public const string EMPTY_VALUE = "EMPTY_VALUE";
        #region SETTING NAMES
        /// <summary>
        /// Setting names (instead of manually written names)
        /// [NOTE: Those are used only for reading - enum variable name restoration would require Enum.Parse()]
        /// [IDEA: CAN DERIVE A CLASS FROM SETTINGS TO OMIT USING STRING ARGUMENTS]
        /// </summary>
        public enum AppSettingList
        {
            None = 0,
            PostPath
        }
        #endregion
        #region General methods
        /// <summary>
        /// File and containing "init" directory paths are being verified, based on device's AppData
        /// </summary>
        /// <param name="appDataFolder"></param>
        /// <returns>Verified settings path</returns>
        public static string GetAppDataFilePath(string appDataFolder, string filename)
        {
            string appDataInitPath = Path.Combine(appDataFolder);
            if (!Directory.Exists(appDataInitPath)) Directory.CreateDirectory(appDataInitPath);
            string settingsPath = Path.Combine(appDataInitPath, filename);
            if (!File.Exists(settingsPath)) File.Create(settingsPath).Close();
            return settingsPath;
        }
        /// <summary>
        /// Settings are being read using GetSettingsFilePath(string) function, and split to key-value using SETT_SPEC constant character
        /// </summary>
        /// <param name="appDataFolder"></param>
        /// <returns>Setting list in key-value folm</returns>
        public static List<KeyValuePair<string, string>> ReadSettings(string appDataFolder, string filename)
        {
            string settingsPath = GetAppDataFilePath(appDataFolder, filename);
            string[] rawLines = File.ReadAllLines(settingsPath);
            var settings = new List<KeyValuePair<string, string>>();
            for (int i = 0; i < rawLines.Length; i++)
            {
                KeyValuePair<string, string> settingPair = JsonConvert.DeserializeObject<KeyValuePair<string, string>>(rawLines[i]);
                settings.Add(settingPair);
            }
            return settings;
        }
        /// <summary>
        /// Reading settings in a key-value form using ReadSettings(string)
        /// </summary>
        /// <param name="appDataFolder"></param>
        /// <param name="settingID"></param>
        /// <returns>Value half of a key-value pair or SettingDictionary.None-EMPTY_VALUE pair on non-existing setting</returns>
        public static KeyValuePair<string, string> GetSettingByKey(string appDataFolder, string filename, string settingID)
        {
            List<KeyValuePair<string, string>> settings = ReadSettings(appDataFolder, filename);
            for (int i = 0; i < settings.Count; i++)
                if (settings[i].Key.ToString() == settingID)
                    return settings[i];
            return new KeyValuePair<string, string>(AppSettingList.None.ToString(), EMPTY_VALUE);
        }
        /// <summary>
        /// Using key to wonder for a setting with GetSettingByKey(string, SettingDictionary) and returning 
        /// </summary>
        /// <param name="appDataFolder"></param>
        /// <param name="settingID"></param>
        /// <returns></returns>
        public static string GetValueByKey(string appDataFolder, string filename, string settingID)
        {
            KeyValuePair<string, string> keyValue = GetSettingByKey(appDataFolder, filename, settingID);
            return keyValue.Value;
        }
        /// <summary>
        /// Uses default EMPTY_VALUE constant to define settings existance with comparation of returning value from GetValueByKey(string, SettingDictionary) to EMPTY_VALUE constant
        /// </summary>
        /// <param name="appDataFolder"></param>
        /// <param name="settingID"></param>
        /// <returns>Assumtion of setting existance</returns>
        public static bool Exists(string appDataFolder, string filename, string settingID)
        {
            string dictValue = GetValueByKey(appDataFolder, filename, settingID);
            if (dictValue != EMPTY_VALUE)
                return true;
            return false;
        }
        /// <summary>
        /// Uses appDataFolder path to be expanded to filePath and settings key-value list to be written, processed with ParameterMethods.Represent() as strings, then - written 
        /// </summary>
        /// <param name="appDataFolder"></param>
        /// <param name="settings"></param>
        public static void WriteSettings(string appDataFolder, string filename, List<KeyValuePair<string, string>> settings)
        {
            string settingsPath = GetAppDataFilePath(appDataFolder, filename);
            List<string> settingLines = new List<string>();
            /// Suspicious for enum to string and string to enum
            for (int i = 0; i < settings.Count; i++)
                settingLines.Add(JsonConvert.SerializeObject(new KeyValuePair<string, string>(settings[i].Key, settings[i].Value)));
            File.WriteAllLines(settingsPath, settingLines);
        }
        /// <summary>
        /// Reading settings as key-values, adding settingID-settingValue pair and writing settings file anew
        /// </summary>
        /// <param name="appDataFolder"></param>
        /// <param name="settingID"></param>
        /// <param name="settingValue"></param>
        public static void AddSetting(string appDataFolder, string filename, string settingID, string settingValue)
        {
            var settings = ReadSettings(appDataFolder, filename);
            settings.Add(new KeyValuePair<string, string>(settingID, settingValue));
            WriteSettings(appDataFolder, filename, settings);
        }
        /// <summary>
        /// Uses ReadSettings(string) to read settings as key-value pairs and searches for an exact entry of settingsID as a key
        /// </summary>
        /// <param name="appDataFolder"></param>
        /// <param name="settingID"></param>
        /// <param name="settingValue"></param>
        /// <returns>Logical assumption, whether or not key entry was hit</returns>
        public static bool EditSetting(string appDataFolder, string filename, string settingID, string settingValue)
        {
            var settings = ReadSettings(appDataFolder, filename);
            for (int i = 0; i < settings.Count; i++)
                if (settings[i].Key.ToString() == settingID)
                {
                    settings[i] = new KeyValuePair<string, string>(settings[i].Key, settingValue);
                    return true;
                }
            return false;
        }
        #endregion
    }
}

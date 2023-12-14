using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Settings.PostDataSettings;

namespace Settings
{
    public static class AppSettings
    {
        public enum SettingList
        {
            None = 0,
            PostPath
        }
        public static void AddSetting(string appDataFolder, string filename, SettingList settingID, string settingValue)
        {
            BaseSettings.AddSetting(appDataFolder, filename, settingID.ToString(), settingValue);
        }
        public static bool EditSetting(string appDataFolder, string filename, SettingList settingID, string settingValue)
        {
            return BaseSettings.EditSetting(appDataFolder, filename, settingID.ToString(), settingValue);
        }
        public static KeyValuePair<string, string> GetSettingByKey(string appDataFolder, string filename, SettingList settingID)
        {
            return BaseSettings.GetSettingByKey(appDataFolder, filename, settingID.ToString());
        }
        public static string GetSettingValue(string appDataFolder, string filename, SettingList settingID)
        {
            KeyValuePair<string, string> keyValue = GetSettingByKey(appDataFolder, filename, settingID);
            return keyValue.Value;
        }
        #region GET
        public static string GetPostPath(string appDataFolder, string filename)
        {
            return GetSettingValue(appDataFolder, filename, SettingList.PostPath);
        }
        public static void SetPostPath(string appDataFolder, string filename, string settingValue)
        {
            string sid = SettingList.PostPath.ToString();
            if (!BaseSettings.Exists(appDataFolder, filename, sid))
                BaseSettings.AddSetting(appDataFolder, filename, SettingList.PostPath.ToString(), settingValue);
            else
                BaseSettings.EditSetting(appDataFolder, filename, SettingList.PostPath.ToString(), settingValue);
        }
        #endregion
    }
}

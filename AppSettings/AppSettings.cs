using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppSettings
{
    public class AppSettings
    {
        public class SettingsItem
        {
            public bool UpdateUIFromSetting;
            public int ScreenSizeWidth;
            public int ScreenSizeHeight;
            public string Title;
        }
        #region variables

        private SettingsItem _settingsItem;
        public SettingsItem Settings { get { return this._settingsItem; } }
        
        #endregion
        
        public static String filePath = AppDomain.CurrentDomain.BaseDirectory + "Settings.json";

        private static AppSettings appSettings;

        private Delegate _onSettingUpdateEventListener;
        public Delegate OnSettingUpdateEventListener { get { return _onSettingUpdateEventListener; } set { _onSettingUpdateEventListener = value; } }

        public static AppSettings Instance()
        {
            if(appSettings == null)
            {
                appSettings = new AppSettings();
            }
            return appSettings;
        }

        private AppSettings()
        {
            Load(null);
        }

        public void Load(Delegate eventHandler)
        {
            try
            {
                this.OnSettingUpdateEventListener = eventHandler;

                if(_settingsItem == null)
                {
                    string jsonString = File.ReadAllText(filePath);
                    _settingsItem = Newtonsoft.Json.JsonConvert.DeserializeObject<SettingsItem>(jsonString);
                    if (_settingsItem == null)
                    {
                        throw new NotImplementedException();
                    }
                }
                NotifySettingsListener();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                LoadDefault();
                Save();
            }
        }
        public void Save()
        {
            try
            { 
                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(_settingsItem);
                File.WriteAllText(filePath, jsonString);
                NotifySettingsListener();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void LoadDefault()
        {
            this._settingsItem = new SettingsItem();
            this._settingsItem.ScreenSizeHeight = 500;
            this._settingsItem.ScreenSizeWidth = 700;
            this._settingsItem.Title = "Settins Items";
            NotifySettingsListener();
        }

        public void NotifySettingsListener()
        {
            if (_onSettingUpdateEventListener != null)
            {
                _onSettingUpdateEventListener.DynamicInvoke(null);
            }
        }
    }
}

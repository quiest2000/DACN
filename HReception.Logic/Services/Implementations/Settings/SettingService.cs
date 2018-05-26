using System;
using System.IO;
using System.Xml.Serialization;
using HReception.Logic.Services.Interfaces.Settings;

namespace HReception.Logic.Services.Implementations.Settings
{
    public class SettingService : ISettingService
    {
        private const string SettingFileName = "Simulator.config";
        private static readonly object LockObj = new object();
        /// <summary>
        /// Client settings
        /// </summary>
        public SettingModel CurrentSetting { get; private set; }
        public SettingService()
        {
            if (File.Exists(SettingFileName))
            {
                var deserializer = new XmlSerializer(typeof(SettingModel));
                TextReader reader = new StreamReader(SettingFileName);
                var setting = (SettingModel)deserializer.Deserialize(reader);
                reader.Close();
                CurrentSetting = setting;
            }
            else
            {
                throw new Exception("Config file not found");
            }
        }

        public void UpdateClientSettings(SettingModel newSetting)
        {
            lock (LockObj)
            {
                if (!File.Exists(SettingFileName))
                    throw new Exception("Config file not found");

                var serializer = new XmlSerializer(typeof(SettingModel));
                using (TextWriter writer = new StreamWriter(SettingFileName))
                {
                    serializer.Serialize(writer, newSetting);
                }
                CurrentSetting = newSetting;
            }
        }
    }
}


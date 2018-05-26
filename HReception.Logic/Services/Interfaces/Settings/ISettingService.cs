namespace HReception.Logic.Services.Interfaces.Settings
{
    public interface ISettingService
    {
        /// <summary>
        /// Client settings
        /// </summary>
        SettingModel CurrentSetting { get; }
        void UpdateClientSettings(SettingModel newSetting);
    }
}
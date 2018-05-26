using System;

namespace HReception.Logic.Services.Interfaces.Settings
{
    [Serializable]
    public class SettingModel
    {
        public string ServerAddress { get; set; }
        public string MachinePassword { get; set; }
        public string MainPrinter { get; set; }
        public string SecondPrinter { get; set; }
        public bool UseDefaultPrinter { get; set; }
    }
}

using System;
using System.IO;
using HReception.Logic.Context.Infrastructure;

namespace HReception.Droid.Services
{
    public class DbHelper : IDbHelper
    {
        public string GetDbPath()
        {
            //var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "exrin.db");
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(docFolder, "Simulator.db");
        }

        public bool IsDbFileCreated()
        {
            var path = GetDbPath();
            return File.Exists(path);
        }
    }
}

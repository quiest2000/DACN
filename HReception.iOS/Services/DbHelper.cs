using System;
using System.IO;
using HReception.Logic.Context.Infrastructure;

namespace HReception.iOS.Services
{
    public class DbHelper : IDbHelper
    {
        public string GetDbPath()
        {
            //return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", "Simulator.db");
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
                Directory.CreateDirectory(libFolder);

            return Path.Combine(libFolder, "Simulator.db");
        }

        public bool IsDbFileCreated()
        {
            var path = GetDbPath();
            return File.Exists(path);
        }
    }
}

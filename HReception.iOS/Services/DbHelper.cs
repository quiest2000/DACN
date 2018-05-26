using System;
using System.IO;
using HReception.Core.Context.Infrastructure;
namespace HReception.iOS.Services
{
    public class DbHelper : IDbHelper
    {
        public string GetDbPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", "Simulator.db");
        }

        public bool IsDbFileCreated()
        {
            var path = GetDbPath();
            return File.Exists(path);
        }
    }
}

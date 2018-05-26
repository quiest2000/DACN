using System;
namespace HReception.Core.Context.Infrastructure
{
    public interface IDbHelper
    {
        string GetDbPath();
        bool IsDbFileCreated();
    }
}

namespace HReception.Logic.Context.Infrastructure
{
    public interface IDbHelper
    {
        string GetDbPath();
        bool IsDbFileCreated();
    }
}

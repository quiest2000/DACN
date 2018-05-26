using System.Threading.Tasks;

namespace HReception.Logic.Services.Interfaces.Common
{
    public interface ISecurityService
    {
        string Hash(string data, string salt);
        string CreateSalt();
        Task<LoginResultDto> Login(string userName, string password);
        void Logout();
    }
}
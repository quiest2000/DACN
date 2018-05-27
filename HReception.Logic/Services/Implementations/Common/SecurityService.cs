using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HReception.Core;
using HReception.Logic.Context;
using HReception.Logic.Services.Interfaces.Common;
using HReception.Logic.Utils.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HReception.Logic.Services.Implementations.Common
{
    public class SecurityService : ISecurityService
    {
        public string Hash(string data, string salt)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (salt == null) throw new ArgumentNullException(nameof(salt));

            var encoding = new UTF8Encoding();
            var dataBytes = encoding.GetBytes(data).Concat(encoding.GetBytes(salt)).ToArray();

            using (var algorithm = new SHA512Managed())
            {
                var hashBytes = algorithm.ComputeHash(dataBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public string CreateSalt()
        {
            //simple salt
            return Guid.NewGuid().ToString();
        }

        public async Task<LoginResultDto> Login(string userName, string password)
        {
            using (var context = SimulatorContext.Create())
            {
                var user = await context.Users.FirstOrDefaultAsync(aa => aa.UserName == userName);
                if (user is null || !user.IsActive)
                    return new LoginResultDto { IsValid = false };
                //check pass
                var hashed = Hash(password, user.Salt);
                if (hashed != user.Password)
                    return new LoginResultDto { IsValid = false };
                return new LoginResultDto
                {
                    IsValid = true,
                    UserName = userName,
                    DisplayName = user.FullName,
                    Roles = user.Roles.GetFromSeparated()
                };
            }
        }

        public void Logout()
        {
            //todo: cleanup
        }
    }
}

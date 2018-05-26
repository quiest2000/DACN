using System.ComponentModel.DataAnnotations;
using Client.UI.Infrastructure.Context;
using Client.UI.Infrastructure.Context.Enum;

namespace HReception.Core.Context.EfModels
{
    public class User : IEntityBase
    {
        [Key, MaxLength(128)]
        public string Code { get; set; }
        [Required, MaxLength(128)]
        public string UserName { get; set; }

        [MaxLength(256)]
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        [MaxLength(128)]
        public string Salt { get; set; }
        public string Password { get; set; }
        /// <summary>
        /// separated strings
        /// </summary>
        public string Roles { get; set; }
    }
}
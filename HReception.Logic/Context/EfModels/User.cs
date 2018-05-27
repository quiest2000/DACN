using System.ComponentModel.DataAnnotations;
using HReception.Logic.Context.Enum;

namespace HReception.Logic.Context.EfModels
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
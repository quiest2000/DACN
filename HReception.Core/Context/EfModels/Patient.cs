using System.ComponentModel.DataAnnotations;
using Client.UI.Infrastructure.Context;
using Client.UI.Infrastructure.Context.Enum;

namespace HReception.Core.Context.EfModels
{
    public class Patient : IEntityBase
    {
        [MaxLength(50)]
        [Key]
        public string PatientCode { get; set; }
        [MaxLength(500)]
        [Required]
        public string FullName { get; set; }
        public string FullAddress { get; set; }
        public string Gender { get; set; }
        /// <summary>
        /// Có thể để trống
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Có thể để trống
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Số CMND, có thể để trống
        /// </summary>
        public string Pid { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public string DoB { get; set; }
    }
}
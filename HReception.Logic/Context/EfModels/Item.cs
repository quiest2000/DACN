using System.ComponentModel.DataAnnotations;
using HReception.Logic.Context.Enum;

namespace HReception.Logic.Context.EfModels
{
    public class Item: IEntityBase
    {
        /// <summary>
        /// Mã số
        /// </summary>
        [Key]
        [MaxLength(50)]
        public string ItemCode { get; set; }

        /// <summary>
        /// Tên dịch vụ
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string ItemName { get; set; }

        /// <summary>
        /// Đơn vị
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string UnitName { get; set; }

        /// <summary>
        /// Đơn giá
        /// </summary>
        public double UnitPrice { get; set; }
    }
}
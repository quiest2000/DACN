using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Client.UI.Infrastructure.Context;
using Client.UI.Infrastructure.Context.Enum;

namespace HReception.Core.Context.EfModels
{
    public class TransactionDetail: IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int TransactionId { get; set; }
        public virtual Transaction Transaction { get; set; }
        /// <summary>
        /// Mã số
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ItemCode { get; set; }
        public virtual Item Item { get; set; }
        /// <summary>
        /// Đơn vị
        /// </summary>
        [MaxLength(50)]
        public string UnitName { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Đơn giá
        /// </summary>
        public double UnitPrice { get; set; }

        /// <summary>
        /// Thành tiền
        /// </summary>
        public double Total { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Note { get; set; }
    }
}
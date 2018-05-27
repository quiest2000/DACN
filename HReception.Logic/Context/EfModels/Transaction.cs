using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HReception.Logic.Context.Enum;

namespace HReception.Logic.Context.EfModels
{
    public class Transaction: IEntityBase
    {
        /// <summary>
        /// Mã giao dịch của HIS  
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Mã tham chiếu của HIS 
        /// </summary>
        [MaxLength(50)]
        public string ReferenceCode { get; set; }
        /// <summary>
        /// Mã bệnh nhân
        /// </summary>
        [Required]
        public string PatientCode { get; set; }
        public virtual Patient Patient { get; set; }
        /// <summary>
        /// Ngày tháng
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Tổng tiền thanh toán
        /// </summary>
        public double Amount { get; set; }
        /// <summary>
        ///  Thông tin mã hóa dữ liệu
        /// </summary>
        public string Encrypt { get; set; }
        /// <summary>
        /// Số dư tài khoản sau khi xác nhận thanh toán/hoàn trả
        /// </summary>
        public double BalanceAfter { get; set; }
        /// <summary>
        ///  Ghi chú
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// Trạng thái giao dịch
        /// </summary>
        public int StatusId { get; set; }
        [NotMapped]
        public virtual TransactionStatus Status
        {
            get { return (TransactionStatus)StatusId; }
            set { StatusId = (int)value; }
        }
        private ICollection<TransactionDetail> _details;
        public virtual ICollection<TransactionDetail> Details
        {
            get { return _details ?? (_details = new List<TransactionDetail>()); }
            set { _details = value; }
        }
    }
}
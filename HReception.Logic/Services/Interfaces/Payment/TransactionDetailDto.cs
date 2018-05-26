namespace HReception.Logic.Services.Interfaces.Payment
{
    public class TransactionDetailDto
    {
        public int Id { get; set; }

        public int TransactionId { get; set; }

        /// <summary>
        /// Mã số
        /// </summary>

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        /// <summary>
        /// Đơn vị
        /// </summary>
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
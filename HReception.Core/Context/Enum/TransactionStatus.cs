namespace HReception.Core.Context.Enum
{
    public enum TransactionStatus
    {
        /// <summary>
        /// Chờ xác nhận thanh toán từ thẻ CSSK
        /// </summary>
        WaitingForPayment = 0,
        /// <summary>
        /// Đã xác nhận thanh toán
        /// </summary>
        Paid = 1,
        /// <summary>
        /// Chờ xác nhận hoàn thẻ từ thẻ CSSK
        /// </summary>
        WaitingForRefund = 2,
        /// <summary>
        /// Đã xác nhận hoàn trả
        /// </summary>
        Refunded = 3
    }
}
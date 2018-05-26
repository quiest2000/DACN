using System;

namespace HReception.Logic.Services.Interfaces.Payment
{
    public class TransactionReponse
    {
        /// <summary>
        /// Mã giao dịch của HIS  
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Mã tham chiếu của HIS 
        /// </summary>
        public string ReferenceCode { get; set; }

        /// <summary>
        /// Mã bệnh nhân
        /// </summary>
        public string PatientCode { get; set; }

        public string PatientName { get; set; }

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
        ///  Ghi chú
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Trạng thái dịch vụ
        /// </summary>
        public int StatusId { get; set; }

        public string StatusName { get; set; }
    }
}
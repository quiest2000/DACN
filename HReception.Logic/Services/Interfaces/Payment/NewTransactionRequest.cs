using System;
using System.Collections.Generic;

namespace HReception.Logic.Services.Interfaces.Payment
{
    public class NewTransactionRequest
    {
        public NewTransactionRequest()
        {
            ListItems = new List<ItemDetailRequest>();
        }
        /// <summary>
        /// Mã giao dịch của HIS  
        /// </summary>
        public string TransactionId { get; set; }
        /// <summary>
        /// Mã tham chiếu của HIS 
        /// </summary>
        public string ReferenceCode { get; set; }

        /// <summary>
        /// Mã bệnh nhân
        /// </summary>
        public string PatientCode { get; set; }

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
        /// Chi tiết dịch vụ cần thanh toán
        /// </summary>
        public List<ItemDetailRequest> ListItems { get; set; }
    }
}
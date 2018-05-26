namespace HReception.Logic.Services.Interfaces.Payment
{
    public class ItemReponse
    {
        /// <summary>
        /// Mã số
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// Tên dịch vụ
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// Đơn vị
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// Đơn giá
        /// </summary>
        public double UnitPrice { get; set; }
        /// <summary>
        /// so luong
        /// </summary>
        public int Qty { get; set; } 
        public bool IsChecked { get; set; }
        public double Total { get; set; }
        public string SearchField { get; set; }
    }
}
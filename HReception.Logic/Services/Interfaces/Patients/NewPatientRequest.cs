namespace HReception.Logic.Services.Interfaces.Patients
{
    public class NewPatientRequest
    {
        public string PatientCode { get; set; }
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
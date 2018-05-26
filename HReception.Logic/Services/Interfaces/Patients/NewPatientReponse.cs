namespace HReception.Logic.Services.Interfaces.Patients
{
    public class NewPatientReponse
    {
        public NewPatientResults Result { get; set; }
    }

    public enum NewPatientResults
    {
        /// <summary>
        /// Khong thanh cong
        /// </summary>
        Failed,
        /// <summary>
        /// Tạo mới bệnh nhân thành công
        /// </summary>
        Ok,
        /// <summary>
        /// da co msbn
        /// </summary>
        Existed
    }
}
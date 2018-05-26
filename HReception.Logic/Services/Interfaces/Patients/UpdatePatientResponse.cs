namespace HReception.Logic.Services.Interfaces.Patients
{
    public class UpdatePatientResponse
    {
        public UpdatePatientResults Result { get; set; }
    }

    public enum UpdatePatientResults
    {
        NotFound,
        Failed,
        Ok,
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HReception.Logic.Services.Interfaces.Patients
{
    public interface IPatientService
    {
        Task<UpdatePatientResponse> Update(UpdatePatientRequest request);
        /// <summary>
        /// Tạo thông tin bệnh nhân
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<NewPatientReponse> Register(NewPatientRequest request);

        /// <summary>
        /// Lấy thông tin bệnh nhân (local)
        /// </summary>
        /// <param name="patientCode"></param>
        /// <returns></returns>
        Task<IList<PatientDto>> Find(string patientCode);
        /// <summary>
        /// Lấy thông tin bệnh nhân (local)
        /// </summary>
        /// <param name="patientCode"></param>
        /// <returns></returns>
        PatientDto Get(string patientCode);

        Task<bool> Delete(string patientCode);
    }
}
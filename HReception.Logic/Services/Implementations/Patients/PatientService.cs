using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HReception.Core;
using HReception.Logic.Context;
using HReception.Logic.Context.EfModels;
using HReception.Logic.Services.Interfaces.Common;
using HReception.Logic.Services.Interfaces.Patients;
using HReception.Logic.Utils.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HReception.Logic.Services.Implementations.Patients
{
    public class PatientService : IPatientService
    {
        private IGenerator _generator;

        public PatientService(IGenerator generator)
        {
            _generator = generator;
        }

        public async Task<UpdatePatientResponse> Update(UpdatePatientRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            using (var context = SimulatorContext.Create())
            {
                var patient = await context.Patients.FirstOrDefaultAsync(aa => aa.PatientCode == request.PatientCode);
                if (patient == null)
                    return new UpdatePatientResponse { Result = UpdatePatientResults.NotFound };
                request.MapTo(patient);
                await context.SaveChangesAsync();
                return new UpdatePatientResponse { Result = UpdatePatientResults.Ok };
            }
        }

        public async Task<NewPatientReponse> Register(NewPatientRequest request)
        {
            if (request == null || request.FullName.IsNullOrEmpty())
                throw new ArgumentException(nameof(request));
            var patientDtos = await Find(request.PatientCode);
            if (patientDtos.Any())
                return new NewPatientReponse { Result = NewPatientResults.Existed };

            using (var context = SimulatorContext.Create())
            {
                var patient = request.MapTo<Patient>();
                patient.PatientCode = _generator.Next<Patient>();
                context.Patients.Add(patient);
                context.SaveChanges();
            }
            return new NewPatientReponse { Result = NewPatientResults.Ok };
        }

        public Task<IList<PatientDto>> Find(string patientCode)
        {
            var tcs = new TaskCompletionSource<IList<PatientDto>>();
            using (var context = SimulatorContext.Create())
            {
                var condition = patientCode.IsNullOrEmpty() ? string.Empty : patientCode;
                var patients = context.Patients.AsNoTracking().Where(aa => condition == "" || (aa.PatientCode != null && aa.PatientCode.Contains(condition))).ToList();
                if (patients.Any())
                {
                    var rs = patients.MapTo<PatientDto>();
                    tcs.SetResult(rs);
                }
                else
                {
                    tcs.SetResult(new List<PatientDto>());
                }
            }
            return tcs.Task;
        }

        public PatientDto Get(string patientCode)
        {
            if (patientCode.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(patientCode));

            var patient =
                SimulatorContext.Create()
                    .Patients.AsNoTracking()
                    .FirstOrDefault(aa => aa.PatientCode == patientCode);
            return patient == null
                ? new PatientDto()
                : patient.MapTo<PatientDto>();
        }

        public async Task<bool> Delete(string patientCode)
        {
            using (var context=SimulatorContext.Create())
            {
                var patient = await context.Patients.FirstOrDefaultAsync(aa => aa.PatientCode == patientCode);
                if (patient == null) return true;
                context.Patients.Remove(patient);
                await context.SaveChangesAsync();
            }

            return true;
        }
    }
}
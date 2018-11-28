using System;
using AutoMapper;
using HReception.Logic.Context.EfModels;
using HReception.Logic.Services.Interfaces.Patients;
namespace HReception.Logic.Mapping
{
    public class MappingConfig
    {
        public static void Config()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Patient, PatientDto>();
                cfg.CreateMap<PatientDto, NewPatientRequest>();
                cfg.CreateMap<PatientDto, UpdatePatientRequest>();
                cfg.CreateMap<NewPatientRequest, Patient>();
                cfg.CreateMap<PatientDto, PatientDto>();
                cfg.CreateMap<UpdatePatientRequest, Patient>();

            });
        }
    }
}

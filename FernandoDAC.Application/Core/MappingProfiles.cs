using AutoMapper;
using FernandoDAC.Application.Patients;
using FernandoDAC.Domain.Entities;

namespace FernandoDAC.Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Patient, Patient>();
            CreateMap<Patient, PatientDto>();
        }
    }
}
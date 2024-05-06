using AutoMapper;
using FernandoDAC.Application.Core;
using FernandoDAC.Domain.Repositories;
using MediatR;

namespace FernandoDAC.Application.Patients.Queries
{
    public class List
    {
        public class Query : IRequest<Result<List<PatientDto>>>
        { }

        public class Handler : IRequestHandler<Query, Result<List<PatientDto>>>
        {
            private readonly IPatientRepository _patientRepository;

            private readonly IMapper _mapper;

            public Handler(IPatientRepository patientRepository, IMapper mapper)
            {
                _patientRepository = patientRepository;
                _mapper = mapper;
            }

            public async Task<Result<List<PatientDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var patient = await _patientRepository.GetAllPatients();

                var patientDto = _mapper.Map<List<PatientDto>>(patient);

                return Result<List<PatientDto>>.Success(patientDto);
            }
        }
    }
}
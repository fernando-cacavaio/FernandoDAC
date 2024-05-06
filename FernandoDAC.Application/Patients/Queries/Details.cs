using AutoMapper;
using FernandoDAC.Application.Core;
using FernandoDAC.Domain.Repositories;
using MediatR;

namespace FernandoDAC.Application.Patients.Queries
{
    public class Details
    {
        public class Query : IRequest<Result<PatientDto>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PatientDto>>
        {
            private readonly IMapper _mapper;
            private readonly IPatientRepository _patientRepository;

            public Handler(IMapper mapper, IPatientRepository patientRepository)
            {
                _mapper = mapper;
                _patientRepository = patientRepository;
            }

            public async Task<Result<PatientDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var patient = await _patientRepository.GetPatientById(request.Id);

                var patientDto = _mapper.Map<PatientDto>(patient);

                return Result<PatientDto>.Success(patientDto);
            }
        }
    }
}
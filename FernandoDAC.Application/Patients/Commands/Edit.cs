using AutoMapper;
using FernandoDAC.Application.Core;
using FernandoDAC.Domain.Entities;
using FernandoDAC.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace FernandoDAC.Application.Patients.Commands
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Patient Patient { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Patient).SetValidator(new PatientValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IPatientRepository _patientRepository;
            public IMapper _mapper { get; }

            public Handler(IPatientRepository patientRepository, IMapper mapper)
            {
                _mapper = mapper;
                _patientRepository = patientRepository;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var patient = await _patientRepository.GetPatientById(request.Patient.Id);

                if (patient == null) return null;

                _mapper.Map(request.Patient, patient);

                await _patientRepository.UpdatePatient(patient, request.Patient.Id);

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
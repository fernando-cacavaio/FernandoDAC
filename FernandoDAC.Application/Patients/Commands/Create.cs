using FernandoDAC.Application.Core;
using FernandoDAC.Domain.Entities;
using FernandoDAC.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace FernandoDAC.Application.Patients.Commands
{
    public class Create
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

            public Handler(IPatientRepository patientRepository)
            {
                _patientRepository = patientRepository;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                //check if we already have this SIN number
                var patients = await _patientRepository.GetAllPatients();
                if (patients.Any(x => x.SIN == request.Patient.SIN))
                {
                    return Result<Unit>.Failure("Patient already exists with this SIN number");
                }

                await _patientRepository.CreatePatient(request.Patient);

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
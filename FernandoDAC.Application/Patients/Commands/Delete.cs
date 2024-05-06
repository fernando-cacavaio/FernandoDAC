using FernandoDAC.Application.Core;
using FernandoDAC.Domain.Repositories;
using MediatR;

namespace FernandoDAC.Application.Patients.Commands
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public int Id { get; set; }
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
                await _patientRepository.DeletePatient(request.Id);

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
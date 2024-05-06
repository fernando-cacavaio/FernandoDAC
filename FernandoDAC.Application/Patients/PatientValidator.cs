using FernandoDAC.Domain.Entities;
using FluentValidation;

namespace FernandoDAC.Application.Patients
{
    public class PatientValidator : AbstractValidator<Patient>
    {
        public PatientValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .MaximumLength(255);
            RuleFor(x => x.SIN).NotEmpty().Length(9);
            RuleFor(x => x.Address).MaximumLength(255);
            RuleFor(x => x.Phone).NotEmpty().Length(8);
        }
    }
}
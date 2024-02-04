using FluentValidation;
using MediatorBehaviorsFluentValidation.Domain.Commands;

namespace MediatorBehaviorsFluentValidation.Domain.Validation.Customer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(c => c.FirstName)
                .NotNull()
                .NotEmpty()
                .NotEqual("string")
                .MinimumLength(2);

            RuleFor(c => c.LastName)
                .NotNull()
                .NotEqual("string")
                .MinimumLength(2);

            RuleFor(c => c.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .NotEqual("string")
                .MinimumLength(2);
        }
    }
}

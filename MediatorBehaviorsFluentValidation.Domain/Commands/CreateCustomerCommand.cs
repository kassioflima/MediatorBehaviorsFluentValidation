using MediatR;

namespace MediatorBehaviorsFluentValidation.Domain.Commands
{
    public class CreateCustomerCommand : IRequest<int>
    {
        public int CustomerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}

using MediatorBehaviorsFluentValidation.Domain.Responses;
using MediatR;

namespace MediatorBehaviorsFluentValidation.Domain.Queries.Customers
{
    public class GetAllCustomerQuery : IRequest<List<CustomerResponse>>
    {
    }
}

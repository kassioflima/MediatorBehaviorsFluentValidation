using MediatorBehaviorsFluentValidation.Domain.Responses;
using MediatR;

namespace MediatorBehaviorsFluentValidation.Domain.Queries.Customers
{
    public class GetByIdCustomerQuery : IRequest<CustomerResponse?>
    {
        public int Id { get; }

        public GetByIdCustomerQuery(int id)
        {
            Id = id;
        }
    }
}

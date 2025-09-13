using MediatorBehaviorsFluentValidation.Domain.Interfaces;
using MediatorBehaviorsFluentValidation.Domain.Mappings;
using MediatorBehaviorsFluentValidation.Domain.Queries.Customers;
using MediatorBehaviorsFluentValidation.Domain.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MediatorBehaviorsFluentValidation.Domain.Handlers.Customers
{
    public class GetByIdCustomerHandler : IRequestHandler<GetByIdCustomerQuery, CustomerResponse?>
    {
        private readonly ILogger<GetByIdCustomerHandler> _logger;
        private readonly ICustomerRepository _customerRepository;

        public GetByIdCustomerHandler(ILogger<GetByIdCustomerHandler> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        public async Task<CustomerResponse?> Handle(GetByIdCustomerQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler get customer by id");
            var customer = await _customerRepository.GetByAsync(request.Id);
            return customer?.ToResponse();
        }
    }
}

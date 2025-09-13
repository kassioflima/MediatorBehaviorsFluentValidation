using MediatorBehaviorsFluentValidation.Domain.Interfaces;
using MediatorBehaviorsFluentValidation.Domain.Mappings;
using MediatorBehaviorsFluentValidation.Domain.Queries.Customers;
using MediatorBehaviorsFluentValidation.Domain.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MediatorBehaviorsFluentValidation.Domain.Handlers.Customers
{
    public class GetAllCustomerHandler : IRequestHandler<GetAllCustomerQuery, List<CustomerResponse>>
    {
        private readonly ILogger<GetAllCustomerHandler> _logger;
        private readonly ICustomerRepository _customerRepository;

        public GetAllCustomerHandler(ILogger<GetAllCustomerHandler> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        public async Task<List<CustomerResponse>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler list all customer");
            var customers = await _customerRepository.GetAllAsync();
            return customers.ToResponse();
        }
    }
}

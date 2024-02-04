using AutoMapper;
using MediatorBehaviorsFluentValidation.Domain.Interfaces;
using MediatorBehaviorsFluentValidation.Domain.Queries.Customers;
using MediatorBehaviorsFluentValidation.Domain.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MediatorBehaviorsFluentValidation.Domain.Handlers.Customers
{
    public class GetByIdCustomerHandler : IRequestHandler<GetByIdCustomerQuery, CustomerResponse>
    {
        private readonly ILogger<GetByIdCustomerHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public GetByIdCustomerHandler(ILogger<GetByIdCustomerHandler> logger, IMapper mapper, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        public async Task<CustomerResponse> Handle(GetByIdCustomerQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler list all customer");
            var customer = await _customerRepository.GetByAsync(request.Id);
            return _mapper.Map<CustomerResponse>(customer);
        }
    }
}

using AutoMapper;
using MediatorBehaviorsFluentValidation.Domain.Interfaces;
using MediatorBehaviorsFluentValidation.Domain.Queries.Customers;
using MediatorBehaviorsFluentValidation.Domain.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MediatorBehaviorsFluentValidation.Domain.Handlers.Customers
{
    public class GetAllCustomerHandler : IRequestHandler<GetAllCustomerQuery, List<CustomerResponse>>
    {
        private readonly ILogger<GetAllCustomerHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public GetAllCustomerHandler(ILogger<GetAllCustomerHandler> logger, IMapper mapper, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        public async Task<List<CustomerResponse>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler list all customer");
            var customer = await _customerRepository.GetAllAsync();
            return _mapper.Map<List<CustomerResponse>>(customer);
        }
    }
}

using AutoMapper;
using MediatorBehaviorsFluentValidation.Domain.Commands;
using MediatorBehaviorsFluentValidation.Domain.Entity;
using MediatorBehaviorsFluentValidation.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MediatorBehaviorsFluentValidation.Domain.Handlers.Customers
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly ILogger<CreateCustomerHandler> _logger;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CreateCustomerHandler(ILogger<CreateCustomerHandler> logger, ICustomerRepository customerRepository, IMapper mapper)
        {
            _logger = logger;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler create customer");
            var customer = _mapper.Map<Customer>(request);
            return await _customerRepository.Create(customer);
        }
    }
}

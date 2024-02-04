using AutoMapper;
using MediatorBehaviorsFluentValidation.Domain.Commands;
using MediatorBehaviorsFluentValidation.Domain.Entity;
using MediatorBehaviorsFluentValidation.Domain.Responses;

namespace MediatorBehaviorsFluentValidation.Domain.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerResponse>().ReverseMap();
            CreateMap<CreateCustomerCommand, Customer>().ReverseMap();
        }
    }
}

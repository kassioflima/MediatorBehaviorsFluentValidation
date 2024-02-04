using MediatorBehaviorsFluentValidation.Domain.Entity;
using MediatorBehaviorsFluentValidation.Domain.Interfaces;

namespace MediatorBehaviorsFluentValidation.Repository.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public async Task<int> Create(Customer customer)
        {
            return await Task.FromResult(1);
        }

        public async Task<List<Customer>> GetAllAsync() 
            => await Task.FromResult(new List<Customer>()
            {
                new()
                {
                    CustomerId = 1,
                    Email = "roberto.contoso@microsoft.com",
                    FirstName = "Roberto",
                    LastName = "Contoso"
                },
                new()
                {
                    CustomerId = 2,
                    Email = "joao.contoso@microsoft.com",
                    FirstName = "Joao",
                    LastName = "Contoso"
                }
            });

        public async Task<Customer?> GetByAsync(int id)
        {
            if (id == 1)
                return new Customer()
                {
                    CustomerId = 1,
                    Email = "roberto.contoso@microsoft.com",
                    FirstName = "Roberto",
                    LastName = "Contoso"
                };

            return default;
        }
    }
}

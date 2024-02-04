using MediatorBehaviorsFluentValidation.Domain.Entity;

namespace MediatorBehaviorsFluentValidation.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<int> Create(Customer customer);
        Task<List<Customer>> GetAllAsync();
        Task<Customer?> GetByAsync(int id);
    }
}

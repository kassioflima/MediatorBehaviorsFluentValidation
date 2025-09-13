using MediatorBehaviorsFluentValidation.Domain.Commands;
using MediatorBehaviorsFluentValidation.Domain.Entity;
using MediatorBehaviorsFluentValidation.Domain.Responses;

namespace MediatorBehaviorsFluentValidation.Domain.Mappings
{
    /// <summary>
    /// Classe estática para mapeamento de objetos, substituindo o AutoMapper para melhor performance
    /// </summary>
    public static class CustomerMapper
    {
        /// <summary>
        /// Mapeia Customer para CustomerResponse
        /// </summary>
        /// <param name="customer">Entidade Customer</param>
        /// <returns>CustomerResponse</returns>
        public static CustomerResponse ToResponse(this Customer customer)
        {
            if (customer == null)
                return null!;

            return new CustomerResponse
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email
            };
        }

        /// <summary>
        /// Mapeia lista de Customer para lista de CustomerResponse
        /// </summary>
        /// <param name="customers">Lista de entidades Customer</param>
        /// <returns>Lista de CustomerResponse</returns>
        public static List<CustomerResponse> ToResponse(this IEnumerable<Customer> customers)
        {
            if (customers == null)
                return new List<CustomerResponse>();

            return customers.Select(ToResponse).ToList();
        }

        /// <summary>
        /// Mapeia CreateCustomerCommand para Customer
        /// </summary>
        /// <param name="command">Comando CreateCustomerCommand</param>
        /// <returns>Entidade Customer</returns>
        public static Customer ToEntity(this CreateCustomerCommand command)
        {
            if (command == null)
                return null!;

            return new Customer
            {
                CustomerId = command.CustomerId,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email
            };
        }

        /// <summary>
        /// Mapeia Customer para CreateCustomerCommand
        /// </summary>
        /// <param name="customer">Entidade Customer</param>
        /// <returns>CreateCustomerCommand</returns>
        public static CreateCustomerCommand ToCommand(this Customer customer)
        {
            if (customer == null)
                return null!;

            return new CreateCustomerCommand
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email
            };
        }
    }
}

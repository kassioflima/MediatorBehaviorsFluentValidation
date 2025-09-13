using MediatorBehaviorsFluentValidation.Domain.Commands;
using MediatorBehaviorsFluentValidation.Domain.Entity;
using MediatorBehaviorsFluentValidation.Domain.Mappings;
using MediatorBehaviorsFluentValidation.Domain.Responses;
using Xunit;

namespace MediatorBehaviorsFluentValidation.Tests.Mappings
{
    public class CustomerMapperTests
    {
        [Fact]
        public void ToResponse_ValidCustomer_ShouldMapCorrectly()
        {
            // Arrange
            var customer = new Customer
            {
                CustomerId = 1,
                FirstName = "João",
                LastName = "Silva",
                Email = "joao.silva@email.com"
            };

            // Act
            var result = customer.ToResponse();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customer.CustomerId, result.CustomerId);
            Assert.Equal(customer.FirstName, result.FirstName);
            Assert.Equal(customer.LastName, result.LastName);
            Assert.Equal(customer.Email, result.Email);
        }

        [Fact]
        public void ToResponse_NullCustomer_ShouldReturnNull()
        {
            // Arrange
            Customer? customer = null;

            // Act
            var result = customer.ToResponse();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ToResponse_CustomerWithNullValues_ShouldMapCorrectly()
        {
            // Arrange
            var customer = new Customer
            {
                CustomerId = 0,
                FirstName = null,
                LastName = null,
                Email = null
            };

            // Act
            var result = customer.ToResponse();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customer.CustomerId, result.CustomerId);
            Assert.Equal(customer.FirstName, result.FirstName);
            Assert.Equal(customer.LastName, result.LastName);
            Assert.Equal(customer.Email, result.Email);
        }

        [Fact]
        public void ToResponse_EmptyCustomerList_ShouldReturnEmptyList()
        {
            // Arrange
            var customers = new List<Customer>();

            // Act
            var result = customers.ToResponse();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void ToResponse_NullCustomerList_ShouldReturnEmptyList()
        {
            // Arrange
            IEnumerable<Customer>? customers = null;

            // Act
            var result = customers.ToResponse();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void ToResponse_MultipleCustomers_ShouldMapAllCorrectly()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new()
                {
                    CustomerId = 1,
                    FirstName = "João",
                    LastName = "Silva",
                    Email = "joao.silva@email.com"
                },
                new()
                {
                    CustomerId = 2,
                    FirstName = "Maria",
                    LastName = "Santos",
                    Email = "maria.santos@email.com"
                }
            };

            // Act
            var result = customers.ToResponse();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);

            var firstCustomer = result.First();
            Assert.Equal(1, firstCustomer.CustomerId);
            Assert.Equal("João", firstCustomer.FirstName);
            Assert.Equal("Silva", firstCustomer.LastName);
            Assert.Equal("joao.silva@email.com", firstCustomer.Email);

            var secondCustomer = result.Last();
            Assert.Equal(2, secondCustomer.CustomerId);
            Assert.Equal("Maria", secondCustomer.FirstName);
            Assert.Equal("Santos", secondCustomer.LastName);
            Assert.Equal("maria.santos@email.com", secondCustomer.Email);
        }

        [Fact]
        public void ToEntity_ValidCommand_ShouldMapCorrectly()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                CustomerId = 1,
                FirstName = "João",
                LastName = "Silva",
                Email = "joao.silva@email.com"
            };

            // Act
            var result = command.ToEntity();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.CustomerId, result.CustomerId);
            Assert.Equal(command.FirstName, result.FirstName);
            Assert.Equal(command.LastName, result.LastName);
            Assert.Equal(command.Email, result.Email);
        }

        [Fact]
        public void ToEntity_NullCommand_ShouldReturnNull()
        {
            // Arrange
            CreateCustomerCommand? command = null;

            // Act
            var result = command.ToEntity();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ToEntity_CommandWithNullValues_ShouldMapCorrectly()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                CustomerId = 0,
                FirstName = null,
                LastName = null,
                Email = null
            };

            // Act
            var result = command.ToEntity();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.CustomerId, result.CustomerId);
            Assert.Equal(command.FirstName, result.FirstName);
            Assert.Equal(command.LastName, result.LastName);
            Assert.Equal(command.Email, result.Email);
        }

        [Fact]
        public void ToCommand_ValidCustomer_ShouldMapCorrectly()
        {
            // Arrange
            var customer = new Customer
            {
                CustomerId = 1,
                FirstName = "João",
                LastName = "Silva",
                Email = "joao.silva@email.com"
            };

            // Act
            var result = customer.ToCommand();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customer.CustomerId, result.CustomerId);
            Assert.Equal(customer.FirstName, result.FirstName);
            Assert.Equal(customer.LastName, result.LastName);
            Assert.Equal(customer.Email, result.Email);
        }

        [Fact]
        public void ToCommand_NullCustomer_ShouldReturnNull()
        {
            // Arrange
            Customer? customer = null;

            // Act
            var result = customer.ToCommand();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ToCommand_CustomerWithNullValues_ShouldMapCorrectly()
        {
            // Arrange
            var customer = new Customer
            {
                CustomerId = 0,
                FirstName = null,
                LastName = null,
                Email = null
            };

            // Act
            var result = customer.ToCommand();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customer.CustomerId, result.CustomerId);
            Assert.Equal(customer.FirstName, result.FirstName);
            Assert.Equal(customer.LastName, result.LastName);
            Assert.Equal(customer.Email, result.Email);
        }
    }
}

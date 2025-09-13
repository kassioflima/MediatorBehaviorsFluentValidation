using MediatorBehaviorsFluentValidation.Domain.Handlers.Customers;
using MediatorBehaviorsFluentValidation.Domain.Interfaces;
using MediatorBehaviorsFluentValidation.Domain.Queries.Customers;
using MediatorBehaviorsFluentValidation.Domain.Responses;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MediatorBehaviorsFluentValidation.Tests.Handlers
{
    public class GetAllCustomerHandlerTests
    {
        private readonly Mock<ILogger<GetAllCustomerHandler>> _loggerMock;
        private readonly Mock<ICustomerRepository> _repositoryMock;
        private readonly GetAllCustomerHandler _handler;

        public GetAllCustomerHandlerTests()
        {
            _loggerMock = new Mock<ILogger<GetAllCustomerHandler>>();
            _repositoryMock = new Mock<ICustomerRepository>();
            _handler = new GetAllCustomerHandler(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidQuery_ShouldReturnCustomerList()
        {
            // Arrange
            var query = new GetAllCustomerQuery();
            var customers = new List<MediatorBehaviorsFluentValidation.Domain.Entity.Customer>
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

            _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(customers);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

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
        public async Task Handle_EmptyRepository_ShouldReturnEmptyList()
        {
            // Arrange
            var query = new GetAllCustomerQuery();
            var emptyCustomers = new List<MediatorBehaviorsFluentValidation.Domain.Entity.Customer>();
            _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(emptyCustomers);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Handle_RepositoryThrowsException_ShouldPropagateException()
        {
            // Arrange
            var query = new GetAllCustomerQuery();
            var expectedException = new InvalidOperationException("Database connection failed");
            _repositoryMock.Setup(x => x.GetAllAsync()).ThrowsAsync(expectedException);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => 
                _handler.Handle(query, CancellationToken.None));
            
            Assert.Equal("Database connection failed", exception.Message);
        }
    }
}

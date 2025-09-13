using MediatorBehaviorsFluentValidation.Domain.Handlers.Customers;
using MediatorBehaviorsFluentValidation.Domain.Interfaces;
using MediatorBehaviorsFluentValidation.Domain.Queries.Customers;
using MediatorBehaviorsFluentValidation.Domain.Responses;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MediatorBehaviorsFluentValidation.Tests.Handlers
{
    public class GetByIdCustomerHandlerTests
    {
        private readonly Mock<ILogger<GetByIdCustomerHandler>> _loggerMock;
        private readonly Mock<ICustomerRepository> _repositoryMock;
        private readonly GetByIdCustomerHandler _handler;

        public GetByIdCustomerHandlerTests()
        {
            _loggerMock = new Mock<ILogger<GetByIdCustomerHandler>>();
            _repositoryMock = new Mock<ICustomerRepository>();
            _handler = new GetByIdCustomerHandler(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidId_ShouldReturnCustomer()
        {
            // Arrange
            var customerId = 1;
            var query = new GetByIdCustomerQuery(customerId);
            var customer = new MediatorBehaviorsFluentValidation.Domain.Entity.Customer
            {
                CustomerId = customerId,
                FirstName = "João",
                LastName = "Silva",
                Email = "joao.silva@email.com"
            };

            _repositoryMock.Setup(x => x.GetByAsync(customerId)).ReturnsAsync(customer);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerId, result.CustomerId);
            Assert.Equal("João", result.FirstName);
            Assert.Equal("Silva", result.LastName);
            Assert.Equal("joao.silva@email.com", result.Email);
        }

        [Fact]
        public async Task Handle_NonExistentId_ShouldReturnNull()
        {
            // Arrange
            var customerId = 999;
            var query = new GetByIdCustomerQuery(customerId);
            _repositoryMock.Setup(x => x.GetByAsync(customerId)).ReturnsAsync((MediatorBehaviorsFluentValidation.Domain.Entity.Customer?)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_ZeroId_ShouldReturnNull()
        {
            // Arrange
            var customerId = 0;
            var query = new GetByIdCustomerQuery(customerId);
            _repositoryMock.Setup(x => x.GetByAsync(customerId)).ReturnsAsync((MediatorBehaviorsFluentValidation.Domain.Entity.Customer?)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_NegativeId_ShouldReturnNull()
        {
            // Arrange
            var customerId = -1;
            var query = new GetByIdCustomerQuery(customerId);
            _repositoryMock.Setup(x => x.GetByAsync(customerId)).ReturnsAsync((MediatorBehaviorsFluentValidation.Domain.Entity.Customer?)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_RepositoryThrowsException_ShouldPropagateException()
        {
            // Arrange
            var customerId = 1;
            var query = new GetByIdCustomerQuery(customerId);
            var expectedException = new InvalidOperationException("Database error");
            _repositoryMock.Setup(x => x.GetByAsync(customerId)).ThrowsAsync(expectedException);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => 
                _handler.Handle(query, CancellationToken.None));
            
            Assert.Equal("Database error", exception.Message);
        }
    }
}

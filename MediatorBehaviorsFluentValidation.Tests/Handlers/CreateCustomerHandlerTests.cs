using MediatorBehaviorsFluentValidation.Domain.Commands;
using MediatorBehaviorsFluentValidation.Domain.Handlers.Customers;
using MediatorBehaviorsFluentValidation.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MediatorBehaviorsFluentValidation.Tests.Handlers
{
    public class CreateCustomerHandlerTests
    {
        private readonly Mock<ILogger<CreateCustomerHandler>> _loggerMock;
        private readonly Mock<ICustomerRepository> _repositoryMock;
        private readonly CreateCustomerHandler _handler;

        public CreateCustomerHandlerTests()
        {
            _loggerMock = new Mock<ILogger<CreateCustomerHandler>>();
            _repositoryMock = new Mock<ICustomerRepository>();
            _handler = new CreateCustomerHandler(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldReturnCustomerId()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                CustomerId = 1,
                FirstName = "João",
                LastName = "Silva",
                Email = "joao.silva@email.com"
            };

            var expectedCustomerId = 123;
            _repositoryMock.Setup(x => x.Create(It.IsAny<MediatorBehaviorsFluentValidation.Domain.Entity.Customer>()))
                          .ReturnsAsync(expectedCustomerId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedCustomerId, result);
            _repositoryMock.Verify(x => x.Create(It.Is<MediatorBehaviorsFluentValidation.Domain.Entity.Customer>(c =>
                c.CustomerId == command.CustomerId &&
                c.FirstName == command.FirstName &&
                c.LastName == command.LastName &&
                c.Email == command.Email)), Times.Once);
        }

        [Fact]
        public async Task Handle_CommandWithNullValues_ShouldMapCorrectly()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                CustomerId = 0,
                FirstName = null,
                LastName = null,
                Email = null
            };

            var expectedCustomerId = 456;
            _repositoryMock.Setup(x => x.Create(It.IsAny<MediatorBehaviorsFluentValidation.Domain.Entity.Customer>()))
                          .ReturnsAsync(expectedCustomerId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedCustomerId, result);
            _repositoryMock.Verify(x => x.Create(It.Is<MediatorBehaviorsFluentValidation.Domain.Entity.Customer>(c =>
                c.CustomerId == command.CustomerId &&
                c.FirstName == command.FirstName &&
                c.LastName == command.LastName &&
                c.Email == command.Email)), Times.Once);
        }

        [Fact]
        public async Task Handle_RepositoryThrowsException_ShouldPropagateException()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                CustomerId = 1,
                FirstName = "João",
                LastName = "Silva",
                Email = "joao.silva@email.com"
            };

            var expectedException = new InvalidOperationException("Database error");
            _repositoryMock.Setup(x => x.Create(It.IsAny<MediatorBehaviorsFluentValidation.Domain.Entity.Customer>()))
                          .ThrowsAsync(expectedException);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => 
                _handler.Handle(command, CancellationToken.None));
            
            Assert.Equal("Database error", exception.Message);
        }
    }
}

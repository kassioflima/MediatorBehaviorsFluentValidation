using MediatorBehaviorsFluentValidation.Controllers;
using MediatorBehaviorsFluentValidation.Domain.Commands;
using MediatorBehaviorsFluentValidation.Domain.Queries.Customers;
using MediatorBehaviorsFluentValidation.Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MediatorBehaviorsFluentValidation.Tests.Controllers
{
    public class CustomersControllerTests
    {
        private readonly Mock<ILogger<CustomersController>> _loggerMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly CustomersController _controller;

        public CustomersControllerTests()
        {
            _loggerMock = new Mock<ILogger<CustomersController>>();
            _mediatorMock = new Mock<IMediator>();
            _controller = new CustomersController(_loggerMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnOkResult()
        {
            // Arrange
            var customers = new List<CustomerResponse>
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

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetAllCustomerQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(customers);

            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCustomers = Assert.IsAssignableFrom<List<CustomerResponse>>(okResult.Value);
            Assert.Equal(2, returnedCustomers.Count);
            Assert.Equal(1, returnedCustomers.First().CustomerId);
            Assert.Equal(2, returnedCustomers.Last().CustomerId);
        }

        [Fact]
        public async Task GetByAsync_ExistingCustomer_ShouldReturnOkResult()
        {
            // Arrange
            var customerId = 1;
            var customer = new CustomerResponse
            {
                CustomerId = customerId,
                FirstName = "João",
                LastName = "Silva",
                Email = "joao.silva@email.com"
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetByIdCustomerQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(customer);

            // Act
            var result = await _controller.GetByAsync(customerId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCustomer = Assert.IsType<CustomerResponse>(okResult.Value);
            Assert.Equal(customerId, returnedCustomer.CustomerId);
            Assert.Equal("João", returnedCustomer.FirstName);
        }

        [Fact]
        public async Task CreateCustomerAsync_ValidCommand_ShouldReturnCreatedResult()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                CustomerId = 1,
                FirstName = "João",
                LastName = "Silva",
                Email = "joao.silva@email.com"
            };

            var createdCustomerId = 123;
            _mediatorMock.Setup(x => x.Send(command, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(createdCustomerId);

            // Act
            var result = await _controller.CreateCustomerAsync(command);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(createdCustomerId, createdResult.Value);
            
            var routeValues = createdResult.RouteValues;
            Assert.NotNull(routeValues);
            Assert.Equal(createdCustomerId, routeValues["customerId"]);
        }

        [Fact]
        public async Task CreateCustomerAsync_InvalidCommand_ShouldThrowValidationException()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                CustomerId = 1,
                FirstName = "", // Invalid
                LastName = "", // Invalid
                Email = "invalid-email" // Invalid
            };

            var validationException = new FluentValidation.ValidationException("Validation failed");
            _mediatorMock.Setup(x => x.Send(command, It.IsAny<CancellationToken>()))
                        .ThrowsAsync(validationException);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<FluentValidation.ValidationException>(() => 
                _controller.CreateCustomerAsync(command));
            
            Assert.Equal("Validation failed", exception.Message);
        }

        [Fact]
        public async Task CreateCustomerAsync_MediatorThrowsException_ShouldPropagateException()
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
            _mediatorMock.Setup(x => x.Send(command, It.IsAny<CancellationToken>()))
                        .ThrowsAsync(expectedException);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => 
                _controller.CreateCustomerAsync(command));
            
            Assert.Equal("Database error", exception.Message);
        }

        [Fact]
        public async Task GetAllAsync_MediatorThrowsException_ShouldPropagateException()
        {
            // Arrange
            var expectedException = new InvalidOperationException("Service unavailable");
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetAllCustomerQuery>(), It.IsAny<CancellationToken>()))
                        .ThrowsAsync(expectedException);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => 
                _controller.GetAllAsync());
            
            Assert.Equal("Service unavailable", exception.Message);
        }
    }
}

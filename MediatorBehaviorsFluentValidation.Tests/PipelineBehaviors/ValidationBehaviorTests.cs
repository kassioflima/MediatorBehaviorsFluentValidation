using FluentValidation;
using MediatorBehaviorsFluentValidation.Domain.Commands;
using MediatorBehaviorsFluentValidation.Domain.PipeLineBehaviors;
using MediatR;
using Moq;
using Xunit;

namespace MediatorBehaviorsFluentValidation.Tests.PipelineBehaviors
{
    public class ValidationBehaviorTests
    {
        private readonly Mock<IValidator<CreateCustomerCommand>> _validatorMock;
        private readonly ValidationBehavior<CreateCustomerCommand, int> _behavior;

        public ValidationBehaviorTests()
        {
            _validatorMock = new Mock<IValidator<CreateCustomerCommand>>();
            var validators = new List<IValidator<CreateCustomerCommand>> { _validatorMock.Object };
            _behavior = new ValidationBehavior<CreateCustomerCommand, int>(validators);
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldCallNext()
        {
            // Arrange
            var request = new CreateCustomerCommand
            {
                CustomerId = 1,
                FirstName = "João",
                LastName = "Silva",
                Email = "joao.silva@email.com"
            };

            var validationResult = new FluentValidation.Results.ValidationResult();
            _validatorMock.Setup(x => x.Validate(It.IsAny<ValidationContext<CreateCustomerCommand>>()))
                         .Returns(validationResult);

            var nextCalled = false;
            var expectedResult = 123;
            RequestHandlerDelegate<int> next = (CancellationToken ct) =>
            {
                nextCalled = true;
                return Task.FromResult(expectedResult);
            };

            // Act
            var result = await _behavior.Handle(request, next, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResult, result);
            Assert.True(nextCalled);
            _validatorMock.Verify(x => x.Validate(It.IsAny<ValidationContext<CreateCustomerCommand>>()), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ShouldThrowValidationException()
        {
            // Arrange
            var request = new CreateCustomerCommand
            {
                CustomerId = 1,
                FirstName = "", // Invalid
                LastName = "", // Invalid
                Email = "invalid-email" // Invalid
            };

            var validationFailures = new List<FluentValidation.Results.ValidationFailure>
            {
                new("FirstName", "'First Name' must not be empty."),
                new("LastName", "'Last Name' must not be empty."),
                new("Email", "'Email' is not a valid email address.")
            };

            var validationResult = new FluentValidation.Results.ValidationResult(validationFailures);
            _validatorMock.Setup(x => x.Validate(It.IsAny<ValidationContext<CreateCustomerCommand>>()))
                         .Returns(validationResult);

            var nextCalled = false;
            RequestHandlerDelegate<int> next = (CancellationToken ct) =>
            {
                nextCalled = true;
                return Task.FromResult(123);
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(() => 
                _behavior.Handle(request, next, CancellationToken.None));

            Assert.False(nextCalled);
            Assert.Equal(3, exception.Errors.Count());
            Assert.Contains(exception.Errors, e => e.PropertyName == "FirstName");
            Assert.Contains(exception.Errors, e => e.PropertyName == "LastName");
            Assert.Contains(exception.Errors, e => e.PropertyName == "Email");
        }

        [Fact]
        public async Task Handle_NoValidators_ShouldCallNext()
        {
            // Arrange
            var behavior = new ValidationBehavior<CreateCustomerCommand, int>(new List<IValidator<CreateCustomerCommand>>());
            var request = new CreateCustomerCommand
            {
                CustomerId = 1,
                FirstName = "João",
                LastName = "Silva",
                Email = "joao.silva@email.com"
            };

            var nextCalled = false;
            var expectedResult = 456;
            RequestHandlerDelegate<int> next = (CancellationToken ct) =>
            {
                nextCalled = true;
                return Task.FromResult(expectedResult);
            };

            // Act
            var result = await behavior.Handle(request, next, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResult, result);
            Assert.True(nextCalled);
        }

        [Fact]
        public async Task Handle_MultipleValidators_AllValid_ShouldCallNext()
        {
            // Arrange
            var validator1Mock = new Mock<IValidator<CreateCustomerCommand>>();
            var validator2Mock = new Mock<IValidator<CreateCustomerCommand>>();
            
            var validators = new List<IValidator<CreateCustomerCommand>> 
            { 
                validator1Mock.Object, 
                validator2Mock.Object 
            };
            
            var behavior = new ValidationBehavior<CreateCustomerCommand, int>(validators);
            
            var request = new CreateCustomerCommand
            {
                CustomerId = 1,
                FirstName = "João",
                LastName = "Silva",
                Email = "joao.silva@email.com"
            };

            validator1Mock.Setup(x => x.Validate(It.IsAny<ValidationContext<CreateCustomerCommand>>()))
                         .Returns(new FluentValidation.Results.ValidationResult());
            validator2Mock.Setup(x => x.Validate(It.IsAny<ValidationContext<CreateCustomerCommand>>()))
                         .Returns(new FluentValidation.Results.ValidationResult());

            var nextCalled = false;
            var expectedResult = 789;
            RequestHandlerDelegate<int> next = (CancellationToken ct) =>
            {
                nextCalled = true;
                return Task.FromResult(expectedResult);
            };

            // Act
            var result = await behavior.Handle(request, next, CancellationToken.None);

            // Assert
            Assert.Equal(expectedResult, result);
            Assert.True(nextCalled);
            validator1Mock.Verify(x => x.Validate(It.IsAny<ValidationContext<CreateCustomerCommand>>()), Times.Once);
            validator2Mock.Verify(x => x.Validate(It.IsAny<ValidationContext<CreateCustomerCommand>>()), Times.Once);
        }

        [Fact]
        public async Task Handle_MultipleValidators_OneInvalid_ShouldThrowValidationException()
        {
            // Arrange
            var validator1Mock = new Mock<IValidator<CreateCustomerCommand>>();
            var validator2Mock = new Mock<IValidator<CreateCustomerCommand>>();
            
            var validators = new List<IValidator<CreateCustomerCommand>> 
            { 
                validator1Mock.Object, 
                validator2Mock.Object 
            };
            
            var behavior = new ValidationBehavior<CreateCustomerCommand, int>(validators);
            
            var request = new CreateCustomerCommand
            {
                CustomerId = 1,
                FirstName = "João",
                LastName = "Silva",
                Email = "joao.silva@email.com"
            };

            validator1Mock.Setup(x => x.Validate(It.IsAny<ValidationContext<CreateCustomerCommand>>()))
                         .Returns(new FluentValidation.Results.ValidationResult());
            
            var validationFailures = new List<FluentValidation.Results.ValidationFailure>
            {
                new("Email", "Custom validation error")
            };
            validator2Mock.Setup(x => x.Validate(It.IsAny<ValidationContext<CreateCustomerCommand>>()))
                         .Returns(new FluentValidation.Results.ValidationResult(validationFailures));

            var nextCalled = false;
            RequestHandlerDelegate<int> next = (CancellationToken ct) =>
            {
                nextCalled = true;
                return Task.FromResult(123);
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(() => 
                behavior.Handle(request, next, CancellationToken.None));

            Assert.False(nextCalled);
            Assert.Single(exception.Errors);
            Assert.Contains(exception.Errors, e => e.PropertyName == "Email");
        }
    }
}

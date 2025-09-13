using MediatorBehaviorsFluentValidation.Domain.Commands;
using MediatorBehaviorsFluentValidation.Domain.Validation.Customer;
using Xunit;

namespace MediatorBehaviorsFluentValidation.Tests.Validation
{
    public class CreateCustomerCommandValidatorTests
    {
        private readonly CreateCustomerCommandValidator _validator;

        public CreateCustomerCommandValidatorTests()
        {
            _validator = new CreateCustomerCommandValidator();
        }

        [Fact]
        public void Validate_ValidCommand_ShouldPass()
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
            var result = _validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("string")]
        [InlineData("A")]
        public void Validate_InvalidFirstName_ShouldFail(string firstName)
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                CustomerId = 1,
                FirstName = firstName,
                LastName = "Silva",
                Email = "joao.silva@email.com"
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "FirstName");
        }

        [Fact]
        public void Validate_NullFirstName_ShouldFail()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                CustomerId = 1,
                FirstName = null,
                LastName = "Silva",
                Email = "joao.silva@email.com"
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "FirstName");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("string")]
        [InlineData("A")]
        public void Validate_InvalidLastName_ShouldFail(string lastName)
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                CustomerId = 1,
                FirstName = "João",
                LastName = lastName,
                Email = "joao.silva@email.com"
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "LastName");
        }

        [Fact]
        public void Validate_NullLastName_ShouldFail()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                CustomerId = 1,
                FirstName = "João",
                LastName = null,
                Email = "joao.silva@email.com"
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "LastName");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("string")]
        [InlineData("A")]
        [InlineData("invalid-email")]
        [InlineData("@email.com")]
        [InlineData("email@")]
        [InlineData("email.com")]
        public void Validate_InvalidEmail_ShouldFail(string email)
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                CustomerId = 1,
                FirstName = "João",
                LastName = "Silva",
                Email = email
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Email");
        }

        [Fact]
        public void Validate_NullEmail_ShouldFail()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                CustomerId = 1,
                FirstName = "João",
                LastName = "Silva",
                Email = null
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Email");
        }

        [Fact]
        public void Validate_AllFieldsInvalid_ShouldFailWithMultipleErrors()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                CustomerId = 0,
                FirstName = "",
                LastName = "",
                Email = "invalid-email"
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.True(result.Errors.Count >= 3); // Pelo menos 3 erros (FirstName, LastName, Email)
            
            Assert.Contains(result.Errors, e => e.PropertyName == "FirstName");
            Assert.Contains(result.Errors, e => e.PropertyName == "LastName");
            Assert.Contains(result.Errors, e => e.PropertyName == "Email");
        }

        [Theory]
        [InlineData("joao@email.com")]
        [InlineData("maria.santos@empresa.com.br")]
        [InlineData("test123@domain.org")]
        [InlineData("user+tag@example.co.uk")]
        public void Validate_ValidEmails_ShouldPass(string email)
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                CustomerId = 1,
                FirstName = "João",
                LastName = "Silva",
                Email = email
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Theory]
        [InlineData("João")]
        [InlineData("Maria")]
        [InlineData("José da Silva")]
        [InlineData("Ana-Maria")]
        [InlineData("Pedro123")]
        public void Validate_ValidNames_ShouldPass(string name)
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                CustomerId = 1,
                FirstName = name,
                LastName = name,
                Email = "test@email.com"
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }
    }
}

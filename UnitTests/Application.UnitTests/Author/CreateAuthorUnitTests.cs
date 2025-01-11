using Application.CQRS.Author.Command.CreateAuthorCommand;
using Application.Dtos.Author;
using Application.ServiceInterface;
using Domain.Abstraction;

namespace UnitTests.Application.UnitTests.Author
{
    public class CreateAuthorUnitTests
    {
        private readonly Mock<IValidator<CreateAuthorCommand>> _validatorMock;
        private readonly Mock<IAuthorService> _authorServiceMock;
        private readonly CreateAuthorHandler _handler;
        private readonly ValidationBehavior<CreateAuthorCommand, object> _validationBehavior;

        public CreateAuthorUnitTests()
        {
            _validatorMock = new Mock<IValidator<CreateAuthorCommand>>();
            _authorServiceMock = new Mock<IAuthorService>();
            _handler = new CreateAuthorHandler(_authorServiceMock.Object);
            _validationBehavior = new ValidationBehavior<CreateAuthorCommand, object>(new[] { _validatorMock.Object });
        }

        private void SetupValidatorMock(ValidationResult validationResult)
        {
            _validatorMock
                .Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<CreateAuthorCommand>>(), default))
                .ReturnsAsync(validationResult);
        }

        [Fact]
        public async Task Handle_Should_ReturnError_WhenNameIsEmpty()
        {
            // Arrange
            var invalidCommand = new CreateAuthorCommand("", true);
            var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("Nome", "O Nome do autor é obrigatório.")
            });

            SetupValidatorMock(validationResult);

            // Act
            var exception = await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await _validationBehavior.Handle(invalidCommand, () => Task.FromResult<object>(null), default);
            });

            // Assert
            Assert.Contains("O Nome do autor é obrigatório.", exception.Message);
        }

        [Fact]
        public async Task Handle_Should_ReturnError_WhenNameNotContainsLatters()
        {
            // Arrange
            var invalidCommand = new CreateAuthorCommand("Victor123", true);
            var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("Nome", "O Nome deve conter apenas letras.")
            });

            SetupValidatorMock(validationResult);

            // Act
            var exception = await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await _validationBehavior.Handle(invalidCommand, () => Task.FromResult<object>(null), default);
            });

            // Assert
            Assert.Contains("O Nome deve conter apenas letras.", exception.Message);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess_WhenAuthorIsCreated()
        {
            // Arrange
            var command = new CreateAuthorCommand("Victor", true);
            var expectedAuthorDtoResponse = new AuthorDtoResponse(1, "Victor", true);

            _authorServiceMock
                .Setup(service => service.Create(It.IsAny<CreateAuthorCommand>()))
                .ReturnsAsync(Result<AuthorDtoResponse>.Success(expectedAuthorDtoResponse));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedAuthorDtoResponse.Name, result.Data.Name);
            Assert.Equal(expectedAuthorDtoResponse.IsActive, result.Data.IsActive);
        }
    }
}

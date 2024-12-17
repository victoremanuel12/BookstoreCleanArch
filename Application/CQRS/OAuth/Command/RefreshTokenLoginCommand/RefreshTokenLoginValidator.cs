using FluentValidation;

namespace Application.CQRS.OAuth.Command.RefreshTokenLogin
{
    public class RefreshTokenLoginValidator : AbstractValidator<RefreshTokenLoginCommand>
    {
        public RefreshTokenLoginValidator()
        {
            RuleFor(command => command.UserId)
                .NotEmpty().WithMessage("Refresh Token inválido.");
        }
    }
}

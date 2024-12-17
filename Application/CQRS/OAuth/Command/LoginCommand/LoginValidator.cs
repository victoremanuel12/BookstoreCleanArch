using Application.CQRS.OAuth.Command.LoginUserCommand;
using FluentValidation;

namespace Application.CQRS.User.Command.LoginUserCommand
{
    public sealed class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(command => command.Email).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O {PropertyName}  é obrigatório.").EmailAddress().WithMessage("Insira um email valido").
                 MinimumLength(5).WithMessage("O {PropertyName} deve ter entre 3 e 100 caracteres.");

            RuleFor(command => command.Senha)
                .NotNull().WithName("Senha").WithMessage("A {PropertyName} é obrigatória.");


        }

    }
}

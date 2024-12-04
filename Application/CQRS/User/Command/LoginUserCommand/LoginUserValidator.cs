using FluentValidation;

namespace Application.CQRS.User.Command.LoginUserCommand
{
    public sealed class LoginUserValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserValidator()
        {
            RuleFor(command => command.Email).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O {PropertyName}  é obrigatório.").EmailAddress().WithMessage("Insira um email valido").
                 MinimumLength(5).WithMessage("O {PropertyName} deve ter entre 3 e 100 caracteres.");

            RuleFor(command => command.Senha)
                .NotNull().WithName("Senha").WithMessage("A {PropertyName} é obrigatória.");


        }

    }
}

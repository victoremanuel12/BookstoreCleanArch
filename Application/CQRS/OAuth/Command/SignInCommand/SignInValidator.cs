using FluentValidation;

namespace Application.CQRS.OAuth.Command.CreateUserCommand
{
    public sealed class SignInValidator : AbstractValidator<SignInCommand>
    {
        public SignInValidator()
        {
            RuleFor(command => command.Nome)
                .NotNull().WithMessage("O nome é obrigatório.")
                .MinimumLength(5).WithMessage("Informe o nome completo.");

            RuleFor(command => command.Email)
                .NotEmpty().WithMessage("O {PropertyName} é obrigatório.")
                .EmailAddress().WithMessage("Insira um email válido.")
                .MinimumLength(5).WithMessage("O {PropertyName} deve ter entre 3 e 100 caracteres.");

            RuleFor(command => command.Senha)
                .NotNull().WithMessage("A {PropertyName} é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.")
                .Matches(@"[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
                .Matches(@"[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
                .Matches(@"\d").WithMessage("A senha deve conter pelo menos um dígito.")
                .Matches(@"[^\da-zA-Z]").WithMessage("A senha deve conter pelo menos um caractere especial.");

            RuleFor(command => command.SenhaConfirmacao)
                .NotNull().WithMessage("A confirmação da senha é obrigatória.")
                .Equal(command => command.Senha).WithMessage("As senhas devem ser iguais.");
        }
    }
}

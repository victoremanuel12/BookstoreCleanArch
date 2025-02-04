﻿using FluentValidation;

namespace Application.CQRS.Author.Command.CreateAuthorCommand
{
    public sealed class CreateAuthorValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorValidator()
        {
            RuleFor(command => command.Nome).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O {PropertyName} do autor é obrigatório.").
                 Must(IsValidName).WithMessage("O {PropertyName} deve conter apenas letras.").
                 Length(3, 100).WithMessage("O {PropertyName} deve ter entre 3 e 100 caracteres.");
        }

        private bool IsValidName(string name)
        {
            return name.All(Char.IsLetter);
        }
    }
}

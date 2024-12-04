using Application.Dtos.Author;
using Application.ServiceInterface;
using Domain.Abstraction;
using FluentValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.CQRS.Author.Command.CreateAuthorCommand
{
    public sealed class CreateUserHandler : IRequestHandler<CreateAuthorCommand, Result<AuthorDtoResponse>>
    {
        private readonly IAuthorService _authorService;
        public CreateUserHandler(IAuthorService authorService, IValidator<CreateAuthorCommand> validator)
        {
            _authorService = authorService;
        }

        public async Task<Result<AuthorDtoResponse>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            return await _authorService.Create(request);
        }

    }
}

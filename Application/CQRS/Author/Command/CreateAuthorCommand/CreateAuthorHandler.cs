using Application.Dtos.Author;
using Application.ServiceInterface;
using Domain.Abstraction;
using MediatR;

namespace Application.CQRS.Author.Command.CreateAuthorCommand
{
    public sealed class CreateAuthorHandler : IRequestHandler<CreateAuthorCommand, Result<AuthorDtoResponse>>
    {
        private readonly IAuthorService _authorService;
        public CreateAuthorHandler(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<Result<AuthorDtoResponse>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            return await _authorService.Create(request);
        }

    }
}

using Application.Dtos.Author;
using Application.ServiceInterface;
using Domain.Abstraction;
using MediatR;

namespace Application.CQRS.Author.Query.GetAllAuthorWithBooksQuery
{
    public sealed class GetAllAuthorWithBooksHandler : IRequestHandler<GetAllAuthorWithBooksQuery, Result<IEnumerable<AuthorDtoWithBooksResponse>>>
    {
        private readonly IAuthorService _authorService;

        public GetAllAuthorWithBooksHandler(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<Result<IEnumerable<AuthorDtoWithBooksResponse>>> Handle(GetAllAuthorWithBooksQuery request, CancellationToken cancellationToken)
        {
            return await _authorService.GetAllWithBooks(request.Filter,request.Route);
        }
    }
}

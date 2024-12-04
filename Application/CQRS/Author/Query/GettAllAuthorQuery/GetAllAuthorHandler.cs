using Application.Dtos.Author;
using Application.ServiceInterface;
using Domain.Abstraction;
using MediatR;

namespace Application.CQRS.Author.Query.GettAllQuery
{
    public class GetAllAuthorHandler : IRequestHandler<GetAllAuthorQuery, Result<PagedResult<IEnumerable<AuthorDtoResponse>>>>
    {
        private readonly IAuthorService _authorService;

        public GetAllAuthorHandler(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<Result<PagedResult<IEnumerable<AuthorDtoResponse>>>> Handle(GetAllAuthorQuery request, CancellationToken cancellationToken)
        {
            return await _authorService.GetAll(request.Filter, request.Route);
        }
    }
}

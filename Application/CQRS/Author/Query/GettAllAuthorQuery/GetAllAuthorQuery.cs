using Application.Dtos.Author;
using Domain.Abstraction;
using Domain.Abstraction.PaginationFilter;
using MediatR;

namespace Application.CQRS.Author.Query.GettAllQuery
{
    public sealed class GetAllAuthorQuery : IRequest<Result<PagedResult<IEnumerable<AuthorDtoResponse>>>>
    {
        public PaginationFilter Filter { get; }
        public string Route { get; }
        public GetAllAuthorQuery(PaginationFilter filter, string route)
        {
            Filter = filter;
            Route = route;
        }
    }
}

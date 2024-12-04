using Application.Dtos.Author;
using Domain.Abstraction;
using Domain.Abstraction.PaginationFilter;
using MediatR;

namespace Application.CQRS.Author.Query.GetAllAuthorWithBooksQuery
{
    public sealed class GetAllAuthorWithBooksQuery : IRequest<Result<IEnumerable<AuthorDtoWithBooksResponse>>>
    {
        public GetAllAuthorWithBooksQuery(PaginationFilter filter, string route)
        {
            Filter = filter;
            Route = route;
        }

        public PaginationFilter Filter { get; }
        public string Route { get; }
    }
}

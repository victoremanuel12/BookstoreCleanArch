using Application.CQRS.Author.Command.CreateAuthorCommand;
using Application.Dtos.Author;
using Domain.Abstraction;
using Domain.Abstraction.PaginationFilter;

namespace Application.ServiceInterface
{
    public interface IAuthorService
    {
        Task<Result<PagedResult<IEnumerable<AuthorDtoResponse>>>> GetAll(PaginationFilter filter, string route);
        Task<Result<IEnumerable<AuthorDtoWithBooksResponse>>> GetAllWithBooks(PaginationFilter filter, string route);
        Task<Result<AuthorDtoResponse>> Create(CreateAuthorCommand command);
        Task<Result<AuthorDtoResponse>> Update(AuthorDtoRequest authorDtoRequest);
        Task<Result<AuthorDtoResponse>> Diseble(AuthorDtoDisableRequest authorDisableDto);

    }
}

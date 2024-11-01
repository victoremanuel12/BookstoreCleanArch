using Application.Dtos.Author;
using Domain.Abstraction;

namespace Application.ServiceInterface
{
    public interface  IAuthorService
    {
        Task<Result<IEnumerable<AuthorDtoResponse>>> Authors();
        Task<Result<IEnumerable<AuthorWithBooksDtoRequest>>> AuthorsWithBooks();
        Task<Result<long>> NewAuthor(AuthorDtoRequest authorDtoRequest);

    }
}

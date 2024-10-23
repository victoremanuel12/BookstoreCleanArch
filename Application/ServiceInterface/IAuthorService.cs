using Application.Dtos.Author;
using Domain.Abstraction;

namespace Application.ServiceInterface
{
    public interface  IAuthorService
    {
        Task<Result<IEnumerable<AuthorDtoResponse>>> Authors();
        Task<IEnumerable<AuthorDtoResponse>> AuthorsWithBooks();
        Task NewAuthor(AuthorDtoRequest authorDtoRequest);

    }
}

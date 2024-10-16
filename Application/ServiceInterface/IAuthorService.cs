using Application.Dtos.Author;

namespace Application.ServiceInterface
{
    public interface  IAuthorService
    {
        Task<IEnumerable<AuthorDtoResponse>>Authors();
        Task<IEnumerable<AuthorDtoResponse>> AuthorsWithBooks();
        Task NewAuthor(AuthorDtoRequest authorDtoRequest);

    }
}

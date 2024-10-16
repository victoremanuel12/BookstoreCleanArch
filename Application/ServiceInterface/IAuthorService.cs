using Application.Dtos.Author;

namespace Application.ServiceInterface
{
    public interface  IAuthorService
    {
        Task<ICollection<AuthorDtoResponse>>Authors();
        Task<ICollection<AuthorDtoResponse>> AuthorsWithBooks();
        Task NewAuthor(AuthorDtoRequest authorDtoRequest);

    }
}

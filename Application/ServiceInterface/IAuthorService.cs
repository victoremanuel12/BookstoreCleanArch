using Application.Dtos.Author;

namespace Application.ServiceInterface
{
    public interface  IAuthorService
    {
        Task<ICollection<AuthorDto>>Authors();
        Task<ICollection<AuthorDto>> AuthorsWithBooks();


    }
}

using Application.Dtos.Author;

namespace Application.ServiceInterface
{
    public interface  IAuthorService
    {
        Task<IEnumerable<AuthorDto>>Authors();

    }
}

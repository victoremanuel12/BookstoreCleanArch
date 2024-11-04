using Application.Dtos.Author;
using Domain.Abstraction;

namespace Application.ServiceInterface
{
    public interface  IAuthorService
    {
        Task<Result<IEnumerable<AuthorDtoResponse>>> GetAll();
        Task<Result<IEnumerable<AuthorWithBooksDtoRequest>>> GetAllWithBooks();
        Task<Result<AuthorDtoResponse>> NewAuthor(AuthorDtoRequest authorDtoRequest);
        Task<Result<AuthorDtoResponse>> Update(long id,AuthorDtoRequest authorDtoRequest);

    }
}

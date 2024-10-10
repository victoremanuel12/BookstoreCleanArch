using Domain.Entities;

namespace Application.Dtos.Author
{
    public record AuthorDto(string Name,ICollection<Book> Books)
    {
    }
}

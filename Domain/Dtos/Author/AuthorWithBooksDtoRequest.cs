using Domain.Dtos.Book;

namespace Domain.Dtos.Author
{
    public record AuthorWithBooksDtoRequest(long Id, string Name, IEnumerable<BookDtoResponse> Books)
    {
    }
}

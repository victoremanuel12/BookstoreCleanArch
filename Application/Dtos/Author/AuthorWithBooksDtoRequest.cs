using Application.Dtos.Book;

namespace Application.Dtos.Author
{
    public record AuthorWithBooksDtoRequest(long Id, string Name, IEnumerable<BookDtoResponse> Books)
    {
    }
}

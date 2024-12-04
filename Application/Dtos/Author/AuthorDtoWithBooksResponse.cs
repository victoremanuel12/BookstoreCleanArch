using Application.Dtos.Book;

namespace Application.Dtos.Author
{
    public sealed record AuthorDtoWithBooksResponse(long Id, string Name, bool IsActive, IEnumerable<BookDtoResponse> Books)
    {
    }
}

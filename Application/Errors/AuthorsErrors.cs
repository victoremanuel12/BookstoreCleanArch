using Domain.Abstraction;

namespace Application.Errors
{
    public static class AuthorsErrors
    {
        public static readonly Error NotFound = Error.NotFound("AuthorService.Authors", "Nenhum autor encontrado");
    }
}

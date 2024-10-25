using Domain.Abstraction;

namespace Application.Errors
{
    public static class AuthorErrors
    {
            public static readonly Error NotFound = Error.NotFound("AuthorService.Authors", "Nenhum autor encontrado");
    }
}

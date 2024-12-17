using Domain.Abstraction;

namespace Identity.Erros
{
    public class RefreshTokenLoginErrors
    {
        public static readonly Error Blocked = Error.Conflict("IdentityService.RefreshTokenLogin", "Essa conta está bloqueada.");
        public static readonly Error NotFound = Error.Conflict("IdentityService.RefreshTokenLogin", "Usuario não encontrado.");
        public static readonly Error RequireConfirmEmail = Error.Conflict("IdentityService.RefreshTokenLogin", "É necessário confirmar o Email antes de realizar o login.");

    }
}

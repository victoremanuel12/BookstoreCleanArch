using Domain.Abstraction;

namespace Identity.Erros
{
    public class LoginErrors
    {
        public static readonly Error Blocked = Error.Conflict("IdentityService.Login", "Essa conta está bloqueada.");
        public static readonly Error IsNotAllowed = Error.Conflict("IdentityService.Login", "Essa conta não tem permissão para realizar login.");
        public static readonly Error RequiresTwoFactor = Error.Conflict("IdentityService.Login", "É necessário confirmar o login no seu segundo fator de autenticação");
        public static readonly Error InvalidCredentials = Error.Conflict("IdentityService.Login", "Usuário ou senhas incorretos");
        


    }
}

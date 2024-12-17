using Domain.Abstraction;

namespace Identity.Erros
{
    public  class SignInErros
    {
        public static readonly Error InvalidUserName = Error.Conflict("IdentityService.SignIn", "O nome de usuário contém caracteres inválidos.");
        public static readonly Error DuplicateUserName = Error.Conflict("IdentityService.SignIn", "O nome de usuário já está em uso.");
        public static readonly Error DuplicateEmail = Error.Conflict("IdentityService.SignIn", "O endereço de e-mail já está em uso.");
        public static readonly Error InvalidEmail = Error.Conflict("IdentityService.SignIn", "O e-mail fornecido é inválido.");
        public static readonly Error ConcurrencyFailure = Error.Conflict("IdentityService.SignIn", "Falha ao salvar devido a alterações simultâneas.");
        public static readonly Error DefaultError = Error.Conflict("IdentityService.SignIn", "Ocorreu um erro inesperado ao tentar criar o usuário");
    }
}

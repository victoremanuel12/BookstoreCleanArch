using Domain.Abstraction;

namespace Identity.Erros
{
    public  class CadastroErros
    {
        public static readonly Error InvalidUserName = Error.Conflict("IdentityService.Cadastro", "O nome de usuário contém caracteres inválidos.");
        public static readonly Error DuplicateUserName = Error.Conflict("IdentityService.Cadastro", "O nome de usuário já está em uso.");
        public static readonly Error DuplicateEmail = Error.Conflict("IdentityService.Cadastro", "O endereço de e-mail já está em uso.");
        public static readonly Error InvalidEmail = Error.Conflict("IdentityService.Cadastro", "O e-mail fornecido é inválido.");
        public static readonly Error ConcurrencyFailure = Error.Conflict("IdentityService.Cadastro", "Falha ao salvar devido a alterações simultâneas.");
        public static readonly Error DefaultError = Error.Conflict("IdentityService.Cadastro", "Ocorreu um erro inesperado ao tentar criar o usuário");
    }
}

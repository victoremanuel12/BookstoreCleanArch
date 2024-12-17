using Application.CQRS.OAuth.Command.CreateUserCommand;
using Application.CQRS.OAuth.Command.LoginUserCommand;
using Application.Dtos.OAuth;
using Domain.Abstraction;

namespace Application.ServiceInterface
{
    public interface IAuthService
    {
        Task<Result<SignInDtoResponse>> SignIn(SignInCommand command);
        Task<Result<LoginDtoResponse>> Login(LoginCommand command);
        Task<Result<LoginDtoResponse>> RefreshTokenLogin(string idUser);

    }
}

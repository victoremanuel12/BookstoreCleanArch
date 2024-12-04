using Application.CQRS.User.Command.CreateUserCommand;
using Application.CQRS.User.Command.LoginUserCommand;
using Application.Dtos.User;
using Domain.Abstraction;

namespace Application.ServiceInterface
{
    public interface IIdentityService
    {
        Task<Result<CreateUserDtoResponse>> Cadastrar(CreateUserCommand command);
        Task<Result<LoginUserDtoResponse>> Login(LoginUserCommand command);
       

    }
}

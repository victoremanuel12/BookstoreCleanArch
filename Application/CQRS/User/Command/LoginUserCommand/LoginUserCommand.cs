using Application.Dtos.User;
using Domain.Abstraction;
using MediatR;

namespace Application.CQRS.User.Command.LoginUserCommand
{
    public sealed record LoginUserCommand(string Email, string Senha) : IRequest<Result<LoginUserDtoResponse>>
    {
    }
}

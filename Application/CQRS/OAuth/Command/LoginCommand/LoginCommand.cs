using Application.Dtos.OAuth;
using Domain.Abstraction;
using MediatR;

namespace Application.CQRS.OAuth.Command.LoginUserCommand
{
    public sealed record LoginCommand(string Email, string Senha) : IRequest<Result<LoginDtoResponse>>
    {
    }
}

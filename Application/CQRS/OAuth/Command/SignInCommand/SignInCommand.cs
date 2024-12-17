using Application.Dtos.OAuth;
using Domain.Abstraction;
using MediatR;

namespace Application.CQRS.OAuth.Command.CreateUserCommand
{
    public sealed record SignInCommand(string Nome, string Email, string Senha, string SenhaConfirmacao) : IRequest<Result<SignInDtoResponse>>
    {
    }
}

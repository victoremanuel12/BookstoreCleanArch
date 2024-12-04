using Application.Dtos.User;
using Domain.Abstraction;
using MediatR;

namespace Application.CQRS.User.Command.CreateUserCommand
{
    public sealed record CreateUserCommand(string Nome ,string Email, string Senha, string SenhaConfirmacao) : IRequest<Result<CreateUserDtoResponse>>
    {
    }
}

using Application.Dtos.OAuth;
using Domain.Abstraction;
using MediatR;

namespace Application.CQRS.OAuth.Command.RefreshTokenLogin
{
    public record RefreshTokenLoginCommand(string UserId) : IRequest<Result<LoginDtoResponse>>
    {
    }
}

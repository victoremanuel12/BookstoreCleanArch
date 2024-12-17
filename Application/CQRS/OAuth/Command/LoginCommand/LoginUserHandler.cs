using Application.CQRS.OAuth.Command.LoginUserCommand;
using Application.Dtos.OAuth;
using Application.ServiceInterface;
using Domain.Abstraction;
using MediatR;

namespace Application.CQRS.User.Command.LoginUserCommand
{
    public class LoginUserHandler : IRequestHandler<LoginCommand, Result<LoginDtoResponse>>
    {
        private readonly IAuthService _identityService;

        public LoginUserHandler(IAuthService identityService)
        {
            _identityService = identityService;
        }

        public Task<Result<LoginDtoResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return _identityService.Login(request);
        }
    }
}

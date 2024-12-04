using Application.Dtos.User;
using Application.ServiceInterface;
using Domain.Abstraction;
using MediatR;

namespace Application.CQRS.User.Command.LoginUserCommand
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, Result<LoginUserDtoResponse>>
    {
        private readonly IIdentityService _identityService;

        public LoginUserHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public Task<Result<LoginUserDtoResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            return _identityService.Login(request);
        }
    }
}

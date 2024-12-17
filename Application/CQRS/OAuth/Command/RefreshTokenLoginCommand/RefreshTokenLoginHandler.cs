using Application.Dtos.OAuth;
using Application.ServiceInterface;
using Domain.Abstraction;
using MediatR;

namespace Application.CQRS.OAuth.Command.RefreshTokenLogin
{
    public class RefreshTokenLoginHandler : IRequestHandler<RefreshTokenLoginCommand, Result<LoginDtoResponse>>
    {
        private readonly IAuthService _identityService;

        public RefreshTokenLoginHandler(IAuthService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result<LoginDtoResponse>> Handle(RefreshTokenLoginCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.RefreshTokenLogin(request.UserId);
        }
    }
}

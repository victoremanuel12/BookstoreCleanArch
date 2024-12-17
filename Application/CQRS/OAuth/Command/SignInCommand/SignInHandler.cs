using Application.Dtos.OAuth;
using Application.ServiceInterface;
using Domain.Abstraction;
using MediatR;

namespace Application.CQRS.OAuth.Command.CreateUserCommand
{
    public sealed class SignInHandler : IRequestHandler<SignInCommand, Result<SignInDtoResponse>>
    {
        private readonly IAuthService _identityService;
        public SignInHandler(IAuthorService authorService, IAuthService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result<SignInDtoResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.SignIn(request);
        }

    }
}

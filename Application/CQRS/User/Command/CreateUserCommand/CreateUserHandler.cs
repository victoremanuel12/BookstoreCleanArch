using Application.Dtos.User;
using Application.ServiceInterface;
using Domain.Abstraction;
using MediatR;

namespace Application.CQRS.User.Command.CreateUserCommand
{
    public sealed class CreateUserHandler : IRequestHandler<CreateUserCommand, Result<CreateUserDtoResponse>>
    {
        private readonly IIdentityService _identityService;
        public CreateUserHandler(IAuthorService authorService, IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result<CreateUserDtoResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.Cadastrar(request);
        }

    }
}

using Api.Extensions;
using Application.CQRS.User.Command.CreateUserCommand;
using Application.CQRS.User.Command.LoginUserCommand;
using Application.ServiceInterface;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IMediator _mediator;
        public UserController(IIdentityService identityService, IMediator mediator)
        {
            _identityService = identityService;
            _mediator = mediator;
        }

        [HttpPost("Cadastrar")]
        public async Task<IResult> Cadastrar(CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Results.Extensions.MapResult(result);
        }
        [HttpPost("Login")]
        public async Task<IResult> Login(LoginUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Results.Extensions.MapResult(result);
        }
    }
}

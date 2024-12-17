using Api.Extensions;
using Application.CQRS.OAuth.Command.CreateUserCommand;
using Application.CQRS.OAuth.Command.LoginUserCommand;
using Application.CQRS.OAuth.Command.RefreshTokenLogin;
using Application.ServiceInterface;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _identityService;
        private readonly IMediator _mediator;
        public AuthController(IAuthService identityService, IMediator mediator)
        {
            _identityService = identityService;
            _mediator = mediator;
        }

        [HttpPost("SignIn")]
        public async Task<IResult> SignIn(SignInCommand command)
        {
            var result = await _mediator.Send(command);
            return Results.Extensions.MapResult(result);
        }

        [HttpPost("Login")]
        public async Task<IResult> Login(LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return Results.Extensions.MapResult(result);
        }

        [Authorize]
        [HttpPost("RefreshToken_login")]
        public async Task<IResult> RefreshLogin()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var command = new RefreshTokenLoginCommand(userId);
            var result = await _mediator.Send(command);
            return Results.Extensions.MapResult(result);
        }
    }
}

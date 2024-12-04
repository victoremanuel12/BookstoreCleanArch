using Application.CQRS.User.Command.CreateUserCommand;
using Application.CQRS.User.Command.LoginUserCommand;
using Application.Dtos.User;
using Application.ServiceInterface;
using Domain.Abstraction;
using Identity.Configuration;
using Identity.Erros;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<IdentityUser> _singInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtOptions _jwtOptions;


        public IdentityService(SignInManager<IdentityUser> singInManager, UserManager<IdentityUser> userManager, IOptions<JwtOptions> jwtOptions)
        {
            _singInManager = singInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<Result<CreateUserDtoResponse>> Cadastrar(CreateUserCommand command)
        {
            var identityUser = new IdentityUser
            {
                UserName = command.Email,
                Email = command.Email,
                EmailConfirmed = true
            };

            IdentityResult created = await _userManager.CreateAsync(identityUser, command.Senha);

            if (!created.Succeeded)
                return Result<CreateUserDtoResponse>.Failure(GetCreateAsyncErrorResult(created.Errors));
            await _userManager.SetLockoutEnabledAsync(identityUser, false);
            var result = new CreateUserDtoResponse(
                Id: identityUser.Id,
                Nome: identityUser.Email,
                Email: identityUser.Email
            );

            return Result<CreateUserDtoResponse>.Success(result);
        }

        public async Task<Result<LoginUserDtoResponse>> Login(LoginUserCommand command)
        {
            SignInResult login = await _singInManager.PasswordSignInAsync(command.Email, command.Senha, false, true);
            if (!login.Succeeded)
                return Result<LoginUserDtoResponse>.Failure(GetSignInErrorResult(login));
            return Result<LoginUserDtoResponse>.Success(await GenerateToken(command.Email));

        }
        private Error GetSignInErrorResult(SignInResult result)
        {
            return result switch
            {
                { IsLockedOut: true } => LoginErrors.Blocked,

                { IsNotAllowed: true } => LoginErrors.IsNotAllowed,

                { RequiresTwoFactor: true } => LoginErrors.RequiresTwoFactor,

                _ => LoginErrors.InvalidCredentials
            };
        }
        private Error GetCreateAsyncErrorResult(IEnumerable<IdentityError> errors)
        {
            var error = errors.FirstOrDefault();
            if (error == null) return CadastroErros.DefaultError;

            return error.Code switch
            {
                "InvalidUserName" => CadastroErros.InvalidUserName,
                "DuplicateUserName" => CadastroErros.DuplicateUserName,
                "DuplicateEmail" => CadastroErros.DuplicateEmail,
                "InvalidEmail" => CadastroErros.InvalidEmail,
                "ConcurrencyFailure" => CadastroErros.ConcurrencyFailure,
                _ => CadastroErros.DefaultError
            };
        }
        private async Task<LoginUserDtoResponse> GenerateToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null) Result<LoginUserDtoResponse>.Failure(LoginErrors.InvalidCredentials);
            var tokenClaims = await GetClaims(user);
            var dataExpiracao = DateTime.Now.AddSeconds(_jwtOptions.Expiration);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(tokenClaims),
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                Expires = dataExpiracao,
                SigningCredentials = _jwtOptions.SigningCredentials,
                NotBefore = DateTime.Now
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            var result = new LoginUserDtoResponse(
                  Token: token,
                  DataExpiracao: dataExpiracao
             );
            return result;
        }
        private async Task<IList<Claim>> GetClaims(IdentityUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));
            foreach (var role in roles)
            {
                roles.Add(role);
            }
            return claims;

        }
    }
}

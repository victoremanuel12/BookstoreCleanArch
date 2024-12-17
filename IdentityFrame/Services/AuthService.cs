using Application.CQRS.OAuth.Command.CreateUserCommand;
using Application.CQRS.OAuth.Command.LoginUserCommand;
using Application.Dtos.OAuth;
using Application.ServiceInterface;
using Domain.Abstraction;
using Identity.Configuration;
using Identity.Erros;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<IdentityUser> _singInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtOptions _jwtOptions;


        public AuthService(SignInManager<IdentityUser> singInManager, UserManager<IdentityUser> userManager, IOptions<JwtOptions> jwtOptions)
        {
            _singInManager = singInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<Result<SignInDtoResponse>> SignIn(SignInCommand command)
        {
            var identityUser = new IdentityUser
            {
                UserName = command.Email,
                Email = command.Email,
                EmailConfirmed = true
            };

            IdentityResult created = await _userManager.CreateAsync(identityUser, command.Senha);

            if (!created.Succeeded)
                return Result<SignInDtoResponse>.Failure(GetCreateAsyncErrorResult(created.Errors));
            await _userManager.SetLockoutEnabledAsync(identityUser, false);
            var result = new SignInDtoResponse(
                Id: identityUser.Id,
                Nome: identityUser.Email,
                Email: identityUser.Email
            );

            return Result<SignInDtoResponse>.Success(result);
        }

        public async Task<Result<LoginDtoResponse>> Login(LoginCommand command)
        {
            SignInResult login = await _singInManager.PasswordSignInAsync(command.Email, command.Senha, false, true);
            if (!login.Succeeded)
                return Result<LoginDtoResponse>.Failure(GetSignInErrorResult(login));
            return Result<LoginDtoResponse>.Success(await GenerateCredentials(command.Email));
        }

        public async Task<Result<LoginDtoResponse>> RefreshTokenLogin(string idUser)
        {
            IdentityUser user = await _userManager.FindByIdAsync(idUser);
            if (user is null) return Result<LoginDtoResponse>.Failure(RefreshTokenLoginErrors.NotFound);
            if (await _userManager.IsLockedOutAsync(user)) return Result<LoginDtoResponse>.Failure(RefreshTokenLoginErrors.Blocked);
            if (!await _userManager.IsEmailConfirmedAsync(user)) return Result<LoginDtoResponse>.Failure(RefreshTokenLoginErrors.RequireConfirmEmail);
            return Result<LoginDtoResponse>.Success(await GenerateCredentials(user.Email));
        }
        private async Task<LoginDtoResponse> GenerateCredentials(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null) Result<LoginDtoResponse>.Failure(LoginErrors.InvalidCredentials);
            IEnumerable<Claim> acessTokenClaims = await GetClaims(user, adicionarClaimsUsuario: true);
            IEnumerable<Claim> refreshTokenClaims = await GetClaims(user, adicionarClaimsUsuario: false);

            DateTime dataExpiracaoAccessToken = DateTime.Now.AddSeconds(_jwtOptions.AccessTokenExpiration);
            DateTime dataExpiracaoRefreshToken = DateTime.Now.AddSeconds(_jwtOptions.RefreshTokenExpiration);

            string accessToken = GenerateToken(acessTokenClaims, dataExpiracaoAccessToken);
            string refreshToken = GenerateToken(acessTokenClaims, dataExpiracaoRefreshToken);
            return new LoginDtoResponse
            (
                AccessToken: accessToken,
                RefreshToken: refreshToken,
                DataExpiracaoAccessToken: dataExpiracaoAccessToken
            );

        }
        private string GenerateToken(IEnumerable<Claim> claims, DateTime dataExpiracao)
        {

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                Expires = dataExpiracao,
                SigningCredentials = _jwtOptions.SigningCredentials,
                NotBefore = DateTime.Now
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var result = tokenHandler.WriteToken(securityToken);
            return result;
        }
        private async Task<IEnumerable<Claim>> GetClaims(IdentityUser user, bool adicionarClaimsUsuario)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));
            if (adicionarClaimsUsuario)
            {
                var roles = await _userManager.GetRolesAsync(user);
                foreach (var role in roles)
                    claims.Add(new Claim("role", role));

            }
            return claims;

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
            if (error == null) return SignInErros.DefaultError;

            return error.Code switch
            {
                "InvalidUserName" => SignInErros.InvalidUserName,
                "DuplicateUserName" => SignInErros.DuplicateUserName,
                "DuplicateEmail" => SignInErros.DuplicateEmail,
                "InvalidEmail" => SignInErros.InvalidEmail,
                "ConcurrencyFailure" => SignInErros.ConcurrencyFailure,
                _ => SignInErros.DefaultError
            };
        }


    }
}

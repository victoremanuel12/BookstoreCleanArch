namespace Application.Dtos.User
{
    public record LoginUserDtoResponse(string Token, DateTime DataExpiracao)
    {
    }
}

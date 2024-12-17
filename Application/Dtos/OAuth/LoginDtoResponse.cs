namespace Application.Dtos.OAuth
{
    public record LoginDtoResponse(string AccessToken, string RefreshToken, DateTime DataExpiracaoAccessToken)
    {
    }
}

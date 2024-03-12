

namespace Util.Infrastructure.Auth;


public record TokenInfo(string accessToken, string refreshToken)
{
	public string AccessToken { get; init; } = accessToken;
	public string RefreshToken { get; init; } = refreshToken;
}

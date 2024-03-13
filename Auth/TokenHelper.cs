
namespace Util.Infrastructure.Auth;

public static class TokenHelper
{

	public static string GenerateAccessJwt(Int64 userUID, string email, JwtFields fields)
	{
		var claims = new List<Claim>
		{
			new Claim(CustomClaimType.TokenType, CustomTokenType.Access),
			new Claim(CustomClaimType.UserUID, userUID.ToString()),
			new Claim(ClaimTypes.Email, email)
		};

		var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(fields.Secret));

		var tokenOptions = new JwtSecurityToken(
			issuer: fields.Issuer,
			audience: fields.Audience,
		claims: claims,
			expires: DateTime.Now.AddHours(fields.AccessExpiresHours),
			signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
			);

		return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
	}

	public static string GenerateRefreshJwt(Int64 userUID, string email, JwtFields fields)
	{
		var claims = new List<Claim>
		{
			new Claim(CustomClaimType.TokenType, CustomTokenType.Refresh),
			new Claim(CustomClaimType.UserUID, userUID.ToString()),
			new Claim(ClaimTypes.Email, email)
		};

		var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(fields.Secret));

		var tokenOptions = new JwtSecurityToken(
			issuer: fields.Issuer,
			audience: fields.Audience,
		claims: claims,
			expires: DateTime.Now.AddDays(fields.RefreshExpiresDays),
			signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
			);

		return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
	}
}

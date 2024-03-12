namespace Util.Infrastructure.Auth;

public class JwtFields
{
    public string Secret { get; set; } = "";
    public string Audience { get; set; } = "";
    public string Issuer { get; set; } = "";
    public long AccessExpiresHours { get; set; } = 0;
    public long RefreshExpiresDays { get; set; } = 0;
}

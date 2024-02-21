namespace Util.Infrastructure;

public record SharedLoginRequest
{
	public string Email { get; set; } = "";
	public string Password { get; set; } = "";
}
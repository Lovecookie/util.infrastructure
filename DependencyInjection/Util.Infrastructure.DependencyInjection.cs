
namespace Util.Infrastructure.DependencyInjection;

public static class UtilInfrastructureDependencyInjection
{
	public static WebApplication UseUtilInfrastructure(this WebApplication app)
	{
		//app.UseMiddleware<JWTGuardMiddleware>();

		return app;
	}	
}



namespace myhero_dotnet.Infrastructure.Middleware;


public class FromClientMiddleware
{
    private readonly RequestDelegate _next;

    public FromClientMiddleware(RequestDelegate next)
    {
		_next = next;
	}


    public async Task InvokeAsync(HttpContext context)
    {
		await _next(context);
	}
    
}

using Util.Infrastructure.Model;

namespace Util.Infrastructure.Extensions;

public static class UtilInfrastuctureExtensions
{
	public static IHostApplicationBuilder AddUtilInfrastructure(this IHostApplicationBuilder builder)
	{
		builder.AddAuthenticateJwtBearer();
		//builder.AddDefaultJwtBearer();

		builder.Services.AddAuthorization();

		return builder;
	}

	private static void AddDefaultJwtBearer(this IHostApplicationBuilder builder)
	{
		builder.Services.AddAuthentication()
			.AddJwtBearer()
			.AddJwtBearer("LocalAuthIssuer");		
	}


	private static void AddAuthenticateJwtBearer(this IHostApplicationBuilder builder)
	{
		// JwtFields Config binding
		//service or di IOption<JwtFields> jwtFields = new JwtFields();
		builder.Services.Configure<JwtFields>(builder.Configuration.GetSection("JwtTokenSettings:JwtFields"));

		builder.Services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
			.AddJwtBearer(options =>
			{
				var jwtFields = builder.Configuration.GetSection("JwtTokenSettings:JwtFields").Get<JwtFields>();
				if(jwtFields == null)
				{
					throw new ArgumentNullException("JwtFields");
				}

				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = jwtFields.Issuer,
					ValidAudience = jwtFields.Audience,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtFields.Secret))
				};
			});
	}
}

using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;

namespace Util.Infrastructure.Env;

// reference: https://rmauro.dev/read-env-file-in-csharp/

public static class EnvConfigurationExtensions
{
    public static IConfigurationBuilder AddEnvFile(this IConfigurationBuilder builder, string? environment, string? basePath, bool optional = false, bool reloadOnChange = true)
    {
        environment ??= Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
        basePath ??= Directory.GetCurrentDirectory();

        var envFiles = new[]
        {
            Path.Combine(basePath, $".env.{environment}"),
            Path.Combine(basePath, ".env.local"),
            Path.Combine(basePath, ".env"),
        };

        foreach (var filePath in envFiles.Where(File.Exists))
        {
            var fileProvider = new PhysicalFileProvider(AppContext.BaseDirectory, ExclusionFilters.Hidden | ExclusionFilters.System);

            return AddEnvFile(builder, fileProvider, filePath, optional, reloadOnChange);
        }

        return builder;
    }

    public static IConfigurationBuilder AddEnvFile(this IConfigurationBuilder builder, IFileProvider provider, string path, bool optional, bool reloadOnChange)
    {
        if(builder == null)
        {
            throw new ArgumentNullException("builder is null: ", nameof(builder));
        }

        if(string.IsNullOrEmpty(path))
        {
            throw new ArgumentException("invalid path: ", nameof(path));
        }

        return builder.AddEnvFileBySource(source =>
        {
            source.FileProvider = provider;
            source.Path = path;
            source.Optional = optional;
            source.ReloadOnChange = reloadOnChange;
            source.ResolveFileProvider();
        });
    }

    public static IConfigurationBuilder AddEnvFileBySource(this IConfigurationBuilder builder, Action<EnvConfigurationSource> configureSource)
    {
        builder.Add(configureSource);

        return builder;
    }    
}

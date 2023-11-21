using Infrastructure.Data;

namespace WebAPI.Installers;

public class CacheInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration)
    {
        CacheSettings cacheSettings = new CacheSettings();
        configuration.GetSection("Cache")
            .Bind(cacheSettings);
        services.AddSingleton(cacheSettings);
        services.AddMemoryCache();
    }
}

namespace WebAPI.Installers;

public class CacheInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();
    }
}

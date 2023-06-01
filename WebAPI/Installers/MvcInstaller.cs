using Application;
using Infrastructure;

namespace WebAPI.Installers;

public class MvcInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddApplication();
        services.AddInfrastructure();
        services.AddHttpContextAccessor();
    }
}
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Installers;

public class DbInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
        });
    }
}

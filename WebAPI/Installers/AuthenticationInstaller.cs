using Infrastructure.Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebAPI.Installers;

public class AuthenticationInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration)
    {
        AuthenticationSettings authenticationSettings = new AuthenticationSettings();
        configuration.GetSection("Authentication")
            .Bind(authenticationSettings);

        services.AddSingleton(authenticationSettings);
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "Bearer";
            options.DefaultScheme = "Bearer";
            options.DefaultChallengeScheme = "Bearer";
        }).AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = authenticationSettings.JwtIssuer,
                ValidAudience = authenticationSettings.JwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
            };
        });

    }
}
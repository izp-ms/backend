using Infrastructure.Data.Seeder;
using WebAPI.Installers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.InstallServicesInAssembly(configuration);

WebApplication app = builder.Build();

app.UseCors("Cors");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    bool runDataSeeder = configuration.GetSection("DataSeeder")["RunDataSeeder"].ToLower() == "true";
    if (runDataSeeder)
    {
        using IServiceScope scope = app.Services.CreateScope();
        DataSeeder dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
        dataSeeder.Seed();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
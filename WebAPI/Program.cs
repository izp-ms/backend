using Infrastructure.Data.Seeder;
using WebAPI.Installers;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.InstallServicesInAssembly(configuration);

var app = builder.Build();

app.UseCors("Cors");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    bool runDataSeeder = configuration.GetSection("DataSeeder")["RunDataSeeder"].ToLower() == "true";
    if (runDataSeeder)
    {
        ServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
        DataSeeder dataSeeder = serviceProvider.GetService<DataSeeder>();
        dataSeeder.Seed();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
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
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
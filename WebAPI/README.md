# Setup

### Setup project:

1. Based on <b>appsettings.Development.template.json</b> and <b>appsettings.Production.template.json</b> create <b>appsettings.Development.json</b> and <b>appsettings.Production.json</b>
2. Set connection string in appsettings.Development.json
3. Update database: ` dotnet ef database update`` or  `Update-Database`

### Run project:

1. Move to src\WebAPI
2. `dotnet run`

### Create new migration:

1. Open: Package Manager Console
2. Set default project: src\Infrastructure
3. `Add-Migration <InitialCreate>`
4. `Update-Database`
   \*. Remove-Migration

namespace Infrastructure.Data.Seeder;

public class DataSeeder
{
    private readonly DataContext _dataContext;

    public DataSeeder(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public void Seed()
    {
        Console.WriteLine("elo");
        if (_dataContext == null || !_dataContext.Database.CanConnect())
        {
            return;
        }

        if (!_dataContext.Users.Any())
        {
            _dataContext.Users.AddRange(UserSeeder.GetUsersSeeder());
        }

        _dataContext.SaveChanges();
    }
}

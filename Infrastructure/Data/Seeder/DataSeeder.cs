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
        if (_dataContext == null || !_dataContext.Database.CanConnect())
        {
            return;
        }

        if (!_dataContext.Users.Any())
        {
            _dataContext.Users.AddRange(UserSeeder.GetUsersSeeder());
        }

        if (!_dataContext.PostcardsImages.Any())
        {
            _dataContext.PostcardsImages.AddRange(PostcardSeeder.GetPostcardDataSeeder());
        }

        _dataContext.SaveChanges();
    }
}

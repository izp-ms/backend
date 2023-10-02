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
        _dataContext.SaveChanges();
        if (!_dataContext.Users.Any())
        {
            _dataContext.Users.AddRange(UserSeeder.GetUsersSeeder());
        }
        _dataContext.SaveChanges();


        if (!_dataContext.PostcardData.Any())
        {
            _dataContext.PostcardData.AddRange(PostcardDataSeeder.GetPostcardDataSeeder());
        }
        _dataContext.SaveChanges();

        if (!_dataContext.Postcards.Any())
        {
            _dataContext.Postcards.AddRange(PostcardSeeder.GetPostcardSeeder(_dataContext));
        }
        _dataContext.SaveChanges();
        if (!_dataContext.UsersDetails.Any())
        {
            _dataContext.UsersDetails.AddRange(UserDetailSeeder.GetUsersDetailsSeeder(_dataContext));
        }
        _dataContext.SaveChanges();
        if (!_dataContext.UsersStats.Any())
        {
            _dataContext.UsersStats.AddRange(UserStatSeeder.GetUsersStatSeeder(_dataContext));
        }
        _dataContext.SaveChanges();
        if (!_dataContext.Address.Any())
        {
            _dataContext.Address.AddRange(AddressSeeder.GetAddressSeeder(_dataContext));
        }
        _dataContext.SaveChanges();
        if (!_dataContext.UserPostcards.Any())
        {
            _dataContext.UserPostcards.AddRange(UserPostcardSeeder.GetUserPostcardsSeeder(_dataContext));
        }
        _dataContext.SaveChanges();
        if (!_dataContext.UserFriends.Any())
        {
            _dataContext.UserFriends.AddRange(UserFriendSeeder.GetUserFriendsSeeder(_dataContext));
        }
        _dataContext.SaveChanges();
    }
}

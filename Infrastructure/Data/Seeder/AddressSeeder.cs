
using Domain.Entities;

namespace Infrastructure.Data.Seeder;

public class AddressSeeder
{
    public static IEnumerable<Address> GetAddressSeeder(DataContext dataContext)
    {
        List<User> users = dataContext.Users.ToList();
        IEnumerable<Address> address = new List<Address>()
        {
            new Address()
            {
                Id = users[0].Id,
                City = "Gliwice",
                Country = "Poland"
            },
            new Address()
            {
                Id = users[1].Id,
                City = "Dąbrowa Górnicza",
                Country = "Poland"
            },
            new Address()
            {
                Id = users[2].Id,
                City = "Warszawa",
                Country = "Poland"
            },
            new Address()
            {
                Id = users[3].Id,
                City = "Bytom",
                Country = "Poland"
            },
            new Address()
            {
                Id = users[4].Id,
                City = "Mikołów",
                Country = "Poland"
            },
            new Address()
            {
                Id = users[5].Id,
                City = "Gdańsk",
                Country = "Poland"
            },
            new Address()
            {
                Id = users[6].Id,
                City = "Hel",
                Country = "Poland"
            },
            new Address()
            {
                Id = users[7].Id,
                City = "Nowy Sącz",
                Country = "Poland"
            },
            new Address()
            {
                Id = users[8].Id,
                City = "Nysa",
                Country = "Poland"
            },
            new Address()
            {
                Id = users[9].Id,
                City = "Szczecin",
                Country = "Poland"
            },
            new Address()
            {
                Id = users[10].Id,
                City = "Świnoujście",
                Country = "Poland"
            },
            new Address()
            {
                Id = users[11].Id,
                City = "Katowice",
                Country = "Poland"
            },
            new Address()
            {
                Id = users[12].Id,
                City = "Gliwice",
                Country = "Poland"
            },
            new Address()
            {
                Id = users[13].Id,
                City = "Zakopane",
                Country = "Poland"
            },
            new Address()
            {
                Id = users[14].Id,
                City = "Częstochowa",
                Country = "Poland"
            },
            new Address()
            {
                Id = users[15].Id,
                City = "Gliwice",
                Country = "Poland"
            },
            new Address()
            {
                Id = users[16].Id,
                City = "Sopot",
                Country = "Poland"
            },
            new Address()
            {
                Id = users[17].Id,
                City = "łódź",
                Country = "Poland"
            },
            new Address()
            {
                Id = users[18].Id,
                City = "Białystok",
                Country = "Poland"
            },
            new Address()
            {
                Id = users[19].Id,
                City = "Poznań",
                Country = "Poland"
            },
            new Address()
            {
                Id = users[20].Id,
                City = "Wrocław",
                Country = "Poland"
            },
            new Address()
            {
                Id = users[21].Id,
                City = "Bytom",
                Country = "Poland"
            },

        };
        return address;
    }
}

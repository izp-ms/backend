
using Domain.Entities;

namespace Infrastructure.Data.Seeder;

public class AddressSeeder
{
    public static IEnumerable<Address> GetAddressSeeder()
    {
        IEnumerable<Address> address = new List<Address>()
        {
            new Address()// ID: 1
            {
                City = "Gliwice",
                Country = "Poland"
            },
            new Address()// ID: 2
            {
                City = "Dąbrowa Górnicza",
                Country = "Poland"
            },
            new Address()// ID: 3
            {
                City = "Warszawa",
                Country = "Poland"
            },
            new Address()// ID: 4
            {
                City = "Bytom",
                Country = "Poland"
            },
            new Address()// ID: 5
            {
                City = "Mikołów",
                Country = "Poland"
            },
            new Address()// ID: 6
            {
                City = "Gdańsk",
                Country = "Poland"
            },
            new Address()// ID: 7
            {
                City = "Hel",
                Country = "Poland"
            },
            new Address()// ID: 8
            {
                City = "Nowy Sącz",
                Country = "Poland"
            },
            new Address()// ID: 9
            {
                City = "Nysa",
                Country = "Poland"
            },
            new Address()// ID: 10
            {
                City = "Szczecin",
                Country = "Poland"
            },
            new Address()// ID: 12
            {
                City = "Świnoujście",
                Country = "Poland"
            },
            new Address()// ID: 13
            {
                City = "Katowice",
                Country = "Poland"
            },
            new Address()// ID: 14
            {
                City = "Gliwice",
                Country = "Poland"
            },
            new Address()// ID: 15
            {
                City = "Zakopane",
                Country = "Poland"
            },
            new Address()// ID: 16
            {
                City = "Częstochowa",
                Country = "Poland"
            },
            new Address()// ID: 17
            {
                City = "Gliwice",
                Country = "Poland"
            },
            new Address()// ID: 18
            {
                City = "Sopot",
                Country = "Poland"
            },
            new Address()// ID: 19
            {
                City = "łódź",
                Country = "Poland"
            },
            new Address()// ID: 20
            {
                City = "Białystok",
                Country = "Poland"
            },
            new Address()// ID: 21
            {
                City = "Poznań",
                Country = "Poland"
            },
            new Address()// ID: 22
            {
                City = "Wrocław",
                Country = "Poland"
            },

        };
        return address;
    }
}

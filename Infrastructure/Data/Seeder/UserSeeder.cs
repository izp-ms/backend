using Domain.Entities;

namespace Infrastructure.Data.Seeder;

public class UserSeeder
{
    public static IEnumerable<User> GetUsersSeeder()
    {
        IEnumerable<User> users = new List<User>()
        {
            new User()// ID: 0
            {
                NickName = "AdminUser",
                Email = "admin@email.com",
                // string123
                Password = "AQAAAAIAAYagAAAAEF/Vbi6W4FbQcd9TmJKKTjB6gGpNb72wUr36gdM71vDOWrsdNRTGVkUMyjIJr979FA==",
                Role = "ADMIN",
                CreatedAt = DateTime.Now,
            },
            new User()// ID: 1
            {
                NickName = "Admin2",
                Email = "admin2@email.com",
                // string123
                Password = "AQAAAAIAAYagAAAAEF/Vbi6W4FbQcd9TmJKKTjB6gGpNb72wUr36gdM71vDOWrsdNRTGVkUMyjIJr979FA==",
                Role = "ADMIN",
                CreatedAt = DateTime.Now,
            },
            new User()// ID: 2
            {
                NickName = "Oltaracel",
                Email = "john.doe@email.com",
                // string123
                Password = "AQAAAAIAAYagAAAAEF/Vbi6W4FbQcd9TmJKKTjB6gGpNb72wUr36gdM71vDOWrsdNRTGVkUMyjIJr979FA==",
                Role = "USER",
                CreatedAt = DateTime.Now,
            },
            new User()// ID: 3
            {
                NickName = "Tananicol",
                Email = "jane.smith@email.com",
                // string123
                Password = "AQAAAAIAAYagAAAAEF/Vbi6W4FbQcd9TmJKKTjB6gGpNb72wUr36gdM71vDOWrsdNRTGVkUMyjIJr979FA==",
                Role = "USER",
                CreatedAt = DateTime.Now,
            },
            new User()// ID: 4
            {
                NickName = "Suel",
                Email = "mike.johnson@email.com",
                // string123
                Password = "AQAAAAIAAYagAAAAEF/Vbi6W4FbQcd9TmJKKTjB6gGpNb72wUr36gdM71vDOWrsdNRTGVkUMyjIJr979FA==",
                Role = "USER",
                CreatedAt = DateTime.Now,
            },
            new User()// ID: 5
            {
                NickName = "Valance",
                Email = "sarah.brown@email.com",
                // string123
                Password = "AQAAAAIAAYagAAAAEF/Vbi6W4FbQcd9TmJKKTjB6gGpNb72wUr36gdM71vDOWrsdNRTGVkUMyjIJr979FA==",
                Role = "USER",
                CreatedAt = DateTime.Now,
            },
            new User()// ID: 6
            {
                NickName = "DavDavoo",
                Email = "david.wilson@email.com",
                // string123
                Password = "AQAAAAIAAYagAAAAEF/Vbi6W4FbQcd9TmJKKTjB6gGpNb72wUr36gdM71vDOWrsdNRTGVkUMyjIJr979FA==",
                Role = "USER",
                CreatedAt = DateTime.Now,
            },
            new User()// ID: 7
            {
                NickName = "Charllia",
                Email = "emily.taylor@email.com",
                // string123
                Password = "AQAAAAIAAYagAAAAEF/Vbi6W4FbQcd9TmJKKTjB6gGpNb72wUr36gdM71vDOWrsdNRTGVkUMyjIJr979FA==",
                Role = "USER",
                CreatedAt = DateTime.Now,
            },
            new User()// ID: 8
            {
                NickName = "Dan",
                Email = "daniel.davis@email.com",
                // string123
                Password = "AQAAAAIAAYagAAAAEF/Vbi6W4FbQcd9TmJKKTjB6gGpNb72wUr36gdM71vDOWrsdNRTGVkUMyjIJr979FA==",
                Role = "USER",
                CreatedAt = DateTime.Now,
            },
            new User()// ID: 9
            {
                NickName = "Oxonomy",
                Email = "olivia.clark@email.com",
                // string123
                Password = "AQAAAAIAAYagAAAAEF/Vbi6W4FbQcd9TmJKKTjB6gGpNb72wUr36gdM71vDOWrsdNRTGVkUMyjIJr979FA==",
                Role = "USER",
                CreatedAt = DateTime.Now,
            },
            new User()// ID: 10
            {
                NickName = "Protesian",
                Email = "michael.martinez@email.com",
                // string123
                Password = "AQAAAAIAAYagAAAAEF/Vbi6W4FbQcd9TmJKKTjB6gGpNb72wUr36gdM71vDOWrsdNRTGVkUMyjIJr979FA==",
                Role = "USER",
                CreatedAt = DateTime.Now,
            },
            new User()// ID: 11
            {
                NickName = "Robotik",
                Email = "sophia.lewis@email.com",
                // string123
                Password = "AQAAAAIAAYagAAAAEF/Vbi6W4FbQcd9TmJKKTjB6gGpNb72wUr36gdM71vDOWrsdNRTGVkUMyjIJr979FA==",
                Role = "USER",
                CreatedAt = DateTime.Now,
            },
            new User()// ID: 12
            {
                NickName = "Gigadude",
                Email = "william.brown@email.com",
                // string123
                Password = "AQAAAAIAAYagAAAAEF/Vbi6W4FbQcd9TmJKKTjB6gGpNb72wUr36gdM71vDOWrsdNRTGVkUMyjIJr979FA==",
                Role = "USER",
                CreatedAt = DateTime.Now,
            },
            new User()// ID: 13
            {
                NickName = "Slyrack",
                Email = "ava.johnson@email.com",
                // string123
                Password = "AQAAAAIAAYagAAAAEF/Vbi6W4FbQcd9TmJKKTjB6gGpNb72wUr36gdM71vDOWrsdNRTGVkUMyjIJr979FA==",
                Role = "USER",
                CreatedAt = DateTime.Now,
            },
            new User()// ID: 14
            {
                NickName = "Kerplunk",
                Email = "james.smith@email.com",
                // string123
                Password = "AQAAAAIAAYagAAAAEF/Vbi6W4FbQcd9TmJKKTjB6gGpNb72wUr36gdM71vDOWrsdNRTGVkUMyjIJr979FA==",
                Role = "USER",
                CreatedAt = DateTime.Now,
            },
            new User()// ID: 15
            {
                NickName = "Synnetan",
                Email = "emma.wilson@email.com",
                // string123
                Password = "AQAAAAIAAYagAAAAEF/Vbi6W4FbQcd9TmJKKTjB6gGpNb72wUr36gdM71vDOWrsdNRTGVkUMyjIJr979FA==",
                Role = "USER",
                CreatedAt = DateTime.Now,
            },
            new User()// ID: 16
            {
                NickName = "Wrian",
                Email = "daniel.jones@email.com",
                // string123
                Password = "AQAAAAIAAYagAAAAEF/Vbi6W4FbQcd9TmJKKTjB6gGpNb72wUr36gdM71vDOWrsdNRTGVkUMyjIJr979FA==",
                Role = "USER",
                CreatedAt = DateTime.Now,
            },
            new User()// ID: 17
            {
                NickName = "Boone",
                Email = "oliver.smith@email.com",
                // string123
                Password = "AQAAAAIAAYagAAAAEF/Vbi6W4FbQcd9TmJKKTjB6gGpNb72wUr36gdM71vDOWrsdNRTGVkUMyjIJr979FA==",
                Role = "USER",
                CreatedAt = DateTime.Now,
            },
            new User()// ID: 18
            {
                NickName = "Cothurnal",
                Email = "sophia.davis@email.com",
                // string123
                Password = "AQAAAAIAAYagAAAAEF/Vbi6W4FbQcd9TmJKKTjB6gGpNb72wUr36gdM71vDOWrsdNRTGVkUMyjIJr979FA==",
                Role = "USER",
                CreatedAt = DateTime.Now,
            },
            new User()// ID: 19
            {
                NickName = "NarcCop",
                Email = "matthew.johnson@email.com",
                // string123
                Password = "AQAAAAIAAYagAAAAEF/Vbi6W4FbQcd9TmJKKTjB6gGpNb72wUr36gdM71vDOWrsdNRTGVkUMyjIJr979FA==",
                Role = "USER",
                CreatedAt = DateTime.Now,
            },
            new User()// ID: 20
            {
                NickName = "Octopi",
                Email = "amelia.clark@email.com",
                // string123
                Password = "AQAAAAIAAYagAAAAEF/Vbi6W4FbQcd9TmJKKTjB6gGpNb72wUr36gdM71vDOWrsdNRTGVkUMyjIJr979FA==",
                Role = "USER",
                CreatedAt = DateTime.Now,
            },
            new User()// ID: 21
            {
                NickName = "Capitulation",
                Email = "ethan.taylor@email.com",
                // string123
                Password = "AQAAAAIAAYagAAAAEF/Vbi6W4FbQcd9TmJKKTjB6gGpNb72wUr36gdM71vDOWrsdNRTGVkUMyjIJr979FA==",
                Role = "USER",
                CreatedAt = DateTime.Now,
            },
        };
        return users;
    }
}

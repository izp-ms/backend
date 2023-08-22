using Domain.Entities;

namespace Infrastructure.Data.Seeder;

public class UserSeeder
{
    public static IEnumerable<User> GetUsersSeeder()
    {
        IEnumerable<User> users = new List<User>()
        {
            new User()
            {
                NickName = "TestUser",
                Email = "Test@email.com",
                // string123
                Password = "AQAAAAIAAYagAAAAEF/Vbi6W4FbQcd9TmJKKTjB6gGpNb72wUr36gdM71vDOWrsdNRTGVkUMyjIJr979FA==",
                Role = "USER",
                CreatedAt = DateTime.Now,
            },
            new User()
            {
                NickName = "AdminUser",
                Email = "Admin@email.com",
                // string123
                Password = "AQAAAAIAAYagAAAAEF/Vbi6W4FbQcd9TmJKKTjB6gGpNb72wUr36gdM71vDOWrsdNRTGVkUMyjIJr979FA==",
                Role = "ADMIN",
                CreatedAt = DateTime.Now,
            },
            new User()
            {
                NickName = "admin2",
                Email = "admin2@email.com",
                // string123
                Password = "AQAAAAIAAYagAAAAEF/Vbi6W4FbQcd9TmJKKTjB6gGpNb72wUr36gdM71vDOWrsdNRTGVkUMyjIJr979FA==",
                Role = "ADMIN",
                CreatedAt = DateTime.Now,
            }
        };
        return users;
    }
}

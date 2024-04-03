using Microsoft.EntityFrameworkCore;
using financeapp.Data;

namespace financeapp.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider, IConfiguration config)
    {
        using (var context = new UserContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<UserContext>>()))
        {
            var adminUsername = config.GetValue<string>("AdminUser:Username") ?? 
                throw new InvalidOperationException("Username for admin not provided");

            // look for user with the admin name
            if (context.User.Any(u => u.Username == adminUsername))
            {
                return; // DB has been seeded
            }

            // Add the admin user
            context.User.Add(
                new User
                {
                    Username = adminUsername,
                    Password = config.GetValue<string>("AdminUser:Password") ?? 
                        throw new InvalidOperationException("Password for admin not provided."),
                    Email = config.GetValue<string>("AdminUser:Email") ?? 
                        throw new InvalidOperationException("Email for admin not provided.")
                }
            );
            context.SaveChanges();
        }
    }
}

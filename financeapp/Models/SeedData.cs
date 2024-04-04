using Microsoft.EntityFrameworkCore;
using financeapp.Data;

namespace financeapp.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider, IConfiguration config)
    {
        using (var context = new FinancesContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<FinancesContext>>()))
        {
            var adminUsername = config.GetValue<string>("AdminUser:Username") ??
                throw new InvalidOperationException("Username for admin not provided");

            // look for user with the admin name
            if (context.Users.Any(u => u.Username == adminUsername))
            {
                return; // DB has been seeded
            }

            // Add the admin user
            context.Users.Add(
                new User
                {
                    // we do not need to set the ID, as it is auto-generated
                    Username = adminUsername,
                    Password = config.GetValue<string>("AdminUser:Password") ??
                        throw new InvalidOperationException("Password for admin not provided."),
                    Email = config.GetValue<string>("AdminUser:Email") ??
                        throw new InvalidOperationException("Email for admin not provided.")
                    // no need to set a foreign key, to finance, since it gets added later
                    // and we can then access it through the navigation property
                }
            );

            // save changes now, so that the example finance can use the admin user
            context.SaveChanges();

            context.Finances.Add(
                new Finance
                {
                    Title = "Got Stonks",
                    Description = "Got some stonks from work",
                    Category = "Income",
                    Icon = "💰",
                    AmountCents = 10000,
                    User = context.Users.First(u => u.Username == adminUsername)
                });

            context.SaveChanges();
        }
    }
}

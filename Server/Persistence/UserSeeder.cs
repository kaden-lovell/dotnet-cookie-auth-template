using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Server.Models;
using Server.Persistence;

public class UserSeeder {
    public static void Initialize(DataContext context, IServiceProvider services) {
        // Get a logger
        var logger = services.GetRequiredService<ILogger<UserSeeder>>();
        if (context.Database.CanConnect()) {
            logger.LogInformation("Database already exists.");
            return;
        }

        // Make sure the database is created
        context.Database.EnsureCreated();
        logger.LogInformation("Start seeding the database.");

        var users = new List<User>();
        users.Add(new User {
            Email = "alicenewport@kadenlovell.com",
            FirstName = "Alice",
            LastName = "Newport",
            Password = "7eleven!!->",
            Role = "Teacher",
            CreatedDate = DateTime.UtcNow,
        });

        users.Add(new User {
            Email = "billythekid@kadenlovell.com",
            FirstName = "Billy",
            LastName = "Kid",
            Password = "7eleven!!->",
            Role = "Student",
            CreatedDate = DateTime.UtcNow,
        });

        context.User.AddRange(users);
        context.SaveChanges();

        logger.LogInformation("Finished seeding the database.");
    }
}
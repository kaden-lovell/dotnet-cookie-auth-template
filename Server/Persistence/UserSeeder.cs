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
            Email = "TheTeacher@kadenlovell.com",
            FirstName = "Tea",
            LastName = "Cher",
            Password = "teacher",
            Role = "Teacher",
            CreatedDate = DateTime.UtcNow,
        });

        users.Add(new User {
            Email = "TheStudent@kadenlovell.com",
            FirstName = "Stu",
            LastName = "Dent",
            Password = "Student",
            Role = "Student",
            CreatedDate = DateTime.UtcNow,
        });

        context.User.AddRange(users);
        context.SaveChanges();

        logger.LogInformation("Finished seeding the database.");
    }
}

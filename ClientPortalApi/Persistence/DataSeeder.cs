using System;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ClientPortalApi.Models;
using ClientPortalApi.Persistence;
using ClientPortalApi.Services;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace ClientPortalApi.Persistence {
    public class DataSeeder {
        public static void Initialize(DataContext context, IServiceProvider services) {
            var _logger = services.GetRequiredService<ILogger<DataSeeder>>();
            // initial connection test
            if (context.Database.CanConnect()) {
                _logger.LogInformation("Database already exists.");
                return;
            }

            // ensure database creation
            context.Database.EnsureCreated();

            // START TRANSACTIONS
            _logger.LogInformation("Start seeding the database.");
  
            // SERVER
            _logger.LogInformation("Seeding server started...");
            byte[] key;
            byte[] iv;

            // generate hashes
            using (Aes aes = Aes.Create()) {
                key = aes.Key;
                iv = aes.IV;
            }

            var server = new Server {
                Key = key,
                IV = iv,
                CreatedDate = DateTime.UtcNow
            };

            context.Server.Add(server);
            context.SaveChanges();
            _logger.LogInformation("Seeding server finished...");


            // USER
            _logger.LogInformation("Seeding users started...");
            var users = new List<User>();
            users.Add(new User {
                Email = "kadenlovell@live.com",
                FirstName = "kaden",
                LastName = "lovell",
                Password = CryptoService.EncryptString(Role.ADMINISTRATOR, server.Key, server.IV),
                Role = Role.ADMINISTRATOR,
                CreatedDate = DateTime.UtcNow
            });

            users.Add(new User {
                Email = "onebadmuthajama@gmail.com",
                FirstName = "kaden",
                LastName = "lovell",
                Password = CryptoService.EncryptString(Role.CLIENT, server.Key, server.IV),
                Role = Role.CLIENT,
                CreatedDate = DateTime.UtcNow
            });

            context.User.AddRange(users);
            context.SaveChanges();
            _logger.LogInformation("Seeding users finished...");

            // END OF TRANSACTIONS
            _logger.LogInformation("Finished seeding the database.");
        }
    }
}

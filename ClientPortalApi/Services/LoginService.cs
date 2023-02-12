using System;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ClientPortalApi.Models;
using ClientPortalApi.Persistence;

namespace ClientPortalApi.Services {
    public class LoginService {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Server> _serverRepository;
        private readonly ILogger<LoginService> _logger;

        public LoginService(IHttpContextAccessor httpContextAccessor, ILogger<LoginService> logger, IRepository<User> userRepository, IRepository<Server> serverRepository) {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _userRepository = userRepository;
            _serverRepository = serverRepository;
        }

        public async Task<dynamic> LoginAsync(dynamic model) {
            _logger.LogInformation($"LoginService:LoginAsync: {model.email} {model.password}");
            var user = await _userRepository.GetUserByEmailAsync((string) model.email);

            // error handling
            if (user == null) {
                return new {
                    message = "no user with this email exists"
                };
            }

            // password compare
            var server = await _serverRepository.GetServerAsync();
            var decrypted = CryptoUtility.DecryptCypher(user.Password, server.Key, server.IV);

            // invalid password message
            if (decrypted != (string) model.password) {
                return new {
                    message = "email or password combination is incorrect"
                };
            }

            // cookie claims generation
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, $"{user.FirstName}{user.LastName}"),
                new Claim("https://localhost:5000/claims/firstname", user.FirstName),
                new Claim("https://localhost:5000/claims/lastname", user.LastName),
                new Claim("https://localhost:5000/claims/email", user.Email),
                new Claim("https://localhost:5000/claims/id", user.Id.ToString()),
                new Claim("https://localhost:5000/claims/role", user.Role)
            };

            // cookie construction
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var authProperties = new AuthenticationProperties {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(60),
                RedirectUri = "https://localhost:4200/login"
            };

            // sign-in
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await _httpContextAccessor.HttpContext.SignInAsync("Cookies", claimsPrincipal);

            // success response
            var result = new {
                success = true,
                user = new {
                    id = user.Id,
                    email = user.Email,
                    firstname = user.FirstName,
                    lastname = user.LastName,
                    role = user.Role
                }
            };

            return result;
        }

        public async Task LogoutAsync() {
            await _httpContextAccessor.HttpContext.SignOutAsync();
        }
    }
}
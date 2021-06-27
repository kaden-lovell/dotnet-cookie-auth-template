using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Server.Models;
using Server.Persistence;

namespace Server.Services {
    public class LoginService {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<User> _repository;
        public LoginService(IHttpContextAccessor httpContextAccessor, IRepository<User> repository) {
            _httpContextAccessor = httpContextAccessor;
            _repository = repository;
        }

        public async Task<dynamic> LoginAsync(dynamic model) {
            var user = await _repository.GetUserByEmailAsync((string)model.email);

            if (user == null) {
                var error = new {
                    errors = new {
                        userNotFound = true
                    }
                };

                return error;
            }

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, $"{user.FirstName}{user.LastName}"),
                new Claim("https://localhost:4200/claims/firstname", user.FirstName),
                new Claim("https://localhost:4200/claims/lastname", user.LastName),
                new Claim("https://localhost:4200/claims/email", user.Email),
                new Claim("https://localhost:4200/claims/id", user.Id.ToString()),
                new Claim("https://localhost:4200/claims/role", user.Role.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var authProperties = new AuthenticationProperties {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(360),
                RedirectUri = "https://localhost:4200/login"
            };

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await _httpContextAccessor.HttpContext.SignInAsync("Cookies", claimsPrincipal);


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
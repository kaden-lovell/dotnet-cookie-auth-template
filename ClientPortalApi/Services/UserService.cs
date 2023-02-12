using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ClientPortalApi.Models;
using ClientPortalApi.Persistence;

namespace ClientPortalApi.Services {
    public class UserService {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<User> _repository;

        public UserService(IHttpContextAccessor httpContextAccessor, IRepository<User> repository) {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<dynamic> GetActiveUserAsync() {
            var id = _httpContextAccessor.HttpContext.User.Identity.Id();
            var user = await _repository.GetUserByIdAsync(id);

            if (user == null) {
                return null;
            }

            var result = new {
                id = _httpContextAccessor.HttpContext.User.Identity.Id(),
                firstname = user.FirstName,
                lastname = user.LastName,
                email = user.Email,
                role = user.Role
            };

            return result;
        }

        public async Task<dynamic> GetUserAsync(long id) {
            var user = await _repository.GetUserByIdAsync(id);

            var result = new {
                id = user?.Id,
                email = user?.Email,
                role = user?.Role,
                firstName = user?.FirstName,
                lastName = user?.LastName
            };

            return result;
        }

        public async Task<dynamic> AddUserAsync(dynamic model) {
            if (model.email == null) {
                return null;
            }

            var user = await _repository.GetUserByEmailAsync((string) model.email);

            if (user != null) {
                return new {
                  message = "email already in use"
                };
            }

            user = await _repository.AddOrUpdateAsync(new User {
                FirstName = model.firstname,
                LastName = model.lastname,
                Email = model.email,
                Role = Role.CLIENT,
                Password = model.password,
                CreatedDate = DateTime.Now,
                ModifiedDate = null
            });

            var result = new {
                success = true,
                user = new {
                    id = user.Id,
                    email = user.Email,
                    role = user.Role,
                    firstname = user.FirstName,
                    lastname = user.LastName
                }
            };

            return result;
        }

        public async Task<dynamic> UpdateUserAsync(dynamic model) {
            // TODO: add validation here...
            var user = await _repository.AddOrUpdateAsync(new User {
                Email = model.email,
                FirstName = model.firstname,
                LastName = model.lastname
            });

            var updatedUser = await _repository.GetUserByIdAsync(user.Id);

            var result = new {
                email = updatedUser.Email,
                firstName = updatedUser.FirstName,
                lastName = updatedUser.LastName
            };

            return result;
        }
    }
}
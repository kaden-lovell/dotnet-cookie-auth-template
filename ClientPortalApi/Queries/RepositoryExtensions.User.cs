using Microsoft.EntityFrameworkCore;
using ClientPortalApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ClientPortalApi.Persistence {
    // linq syntax: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/
    // queries should be structured requests for data from the database, and should be performance optimized
    public static partial class RepositoryExtensions {
        public static async Task<User?> GetUserByIdAsync(this IRepository<User> repository, long id) {
            var result =
                await repository
                    .AsQueryable()
                    .OfType<User>()
                    .SingleOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public static async Task<User?> GetUserByEmailAsync(this IRepository<User> repository, string email) {
            var result =
                await repository
                    .AsQueryable()
                    .OfType<User>()
                    .SingleOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());

            return result;
        }
    }
}
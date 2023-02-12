using Microsoft.EntityFrameworkCore;
using ClientPortalApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ClientPortalApi.Persistence {
    // linq syntax: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/
    // queries should be structured requests for data from the database, and should be performance optimized
    public static partial class RepositoryExtensions {
        public static async Task<Server> GetServerAsync(this IRepository<Server> repository) {
            var result =
                await repository
                    .AsQueryable()
                    .OfType<Server>()
                    .FirstOrDefaultAsync();

            return result;
        }
    }
}
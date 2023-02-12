using System.Linq;
using System.Threading.Tasks;
using ClientPortalApi.Models;

namespace ClientPortalApi.Persistence {
    public interface IRepository<TModel> where TModel : Model {
        IQueryable AsQueryable();
        Task<TModel> AddOrUpdateAsync(TModel model);
        Task DeleteAsync(TModel model);
    }
}
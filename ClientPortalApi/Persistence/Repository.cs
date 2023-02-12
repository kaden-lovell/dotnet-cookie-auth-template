using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ClientPortalApi.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ClientPortalApi.Persistence {
    public class Repository<TModel> : IRepository<TModel> where TModel : class, Model {
        private readonly IPersistenceContext _persistenceContext;
        private readonly DataContext _dataContext;
        private DbSet<TModel> entities;

        public Repository(IPersistenceContext persistenceContext, DataContext dataContext) {
            _dataContext = dataContext;
            _persistenceContext = persistenceContext;
            entities = dataContext.Set<TModel>();
        }

        public IQueryable AsQueryable() {
            return _persistenceContext.Set<TModel>();
        }

        public async Task<TModel> AddOrUpdateAsync(TModel model) {
            if (model == null) {
                return (await entities.AddAsync(model)).Entity;
            }
            else { 
               return entities.Update(model).Entity;
            }        
        }

        public async Task DeleteAsync(TModel model) {
            if (model == null) throw new ArgumentNullException("entity");
            entities.Remove(model);
            await _dataContext.SaveChangesAsync();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Persistence {
    public class DataContext : DbContext {
        public DataContext(DbContextOptions options) : base(options) { }
        public DbSet<User> User { get; set; }
    }
}

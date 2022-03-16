using FlagsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FlagsApi.Data
{
    public class UserStoreContext : DbContext
    {
        public DbSet<User>? Users { get; set; }

        public UserStoreContext(DbContextOptions<UserStoreContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

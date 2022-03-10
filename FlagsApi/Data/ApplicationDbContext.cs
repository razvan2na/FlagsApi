using FlagsApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlagsApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Country>? Countries { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole 
                { 
                    Id = "e465888f-be57-4544-98f8-4367308dbb5c", 
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
            );
        }
    }
}

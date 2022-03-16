using FlagsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FlagsApi.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Country>? Countries { get; set; }
        public DbSet<UserCountry>? UserCountries { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserCountry>()
                .HasKey(nameof(UserCountry.UserId), nameof(UserCountry.CountryCode));
        }
    }
}

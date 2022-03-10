using FlagsApi.Data;
using FlagsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FlagsApi.Repositories.Implementations
{
    public class CountryRepository : RepositoryBase<Country>
    {
        public CountryRepository(ApplicationDbContext context) : base(context) { }
    }
}

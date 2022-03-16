using FlagsApi.Data;
using FlagsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FlagsApi.Repositories.Implementations
{
    public class CountryRepository : RepositoryBase<Country, ApplicationContext>
    {
        public CountryRepository(ApplicationContext context) : base(context) { }
    }
}

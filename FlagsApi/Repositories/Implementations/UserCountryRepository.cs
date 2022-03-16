using FlagsApi.Data;
using FlagsApi.Models;

namespace FlagsApi.Repositories.Implementations
{
    public class UserCountryRepository : RepositoryBase<UserCountry, ApplicationContext>
    {
        public UserCountryRepository(ApplicationContext context) : base(context) { }
    }
}

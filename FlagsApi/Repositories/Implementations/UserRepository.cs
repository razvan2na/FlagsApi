using FlagsApi.Data;
using FlagsApi.Models;

namespace FlagsApi.Repositories.Implementations
{
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }
    }
}

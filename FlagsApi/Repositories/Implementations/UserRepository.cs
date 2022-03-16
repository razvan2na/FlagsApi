using FlagsApi.Data;
using FlagsApi.Models;

namespace FlagsApi.Repositories.Implementations
{
    public class UserRepository : RepositoryBase<User, UserStoreContext>
    {
        public UserRepository(UserStoreContext context) : base(context) { }
    }
}

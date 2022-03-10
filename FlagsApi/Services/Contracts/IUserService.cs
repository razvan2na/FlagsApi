using FlagsApi.Models;

namespace FlagsApi.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User?> GetUser(string username);
        Task AddCountryToUser(User user, Country country);
        Task RemoveCountryFromUser(User user, Country country);
    }
}

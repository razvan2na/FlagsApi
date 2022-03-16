using FlagsApi.Dtos;
using FlagsApi.Models;

namespace FlagsApi.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User?> GetUser(string username);
        Task<UserDto> GetUserDto(User user);
        Task AddCountryToUser(User user, Country country);
        Task RemoveCountryFromUser(User user, Country country);
    }
}

using FlagsApi.Models;
using FlagsApi.Repositories;
using FlagsApi.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FlagsApi.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryBase<User> _userRepository;

        public UserService(IRepositoryBase<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.Get().ToListAsync();
        }

        public async Task<User?> GetUser(string email)
        {
            return await _userRepository
                .Get(user => user.Email == email)
                .Include(user => user.Countries)
                .FirstOrDefaultAsync();
        }

        public async Task AddCountryToUser(User user, Country country)
        {
            user.Countries.Add(country);

            //_userRepository.Update(user);
            await _userRepository.Save();
        }

        public async Task RemoveCountryFromUser(User user, Country country)
        {
            user.Countries.Remove(country);

            //_userRepository.Update(user);
            await _userRepository.Save();
        }
    }
}

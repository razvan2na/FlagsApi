using AutoMapper;
using FlagsApi.Dtos;
using FlagsApi.Models;
using FlagsApi.Repositories;
using FlagsApi.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FlagsApi.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryBase<User> _userRepository;
        private readonly IRepositoryBase<Country> _countryRepository;
        private readonly IRepositoryBase<UserCountry> _userCountryRepository;
        private readonly IMapper _mapper;

        public UserService(
            IRepositoryBase<User> userRepository, 
            IRepositoryBase<Country> countryRepository,
            IRepositoryBase<UserCountry> userCountryRepository,
            IMapper mapper
        )
        {
            _userRepository = userRepository;
            _countryRepository = countryRepository;
            _userCountryRepository = userCountryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.Get().ToListAsync();
        }

        public async Task<User?> GetUser(string email)
        {
            return await _userRepository
                .Get(user => user.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<UserDto> GetUserDto(User user)
        {
            var dto = _mapper.Map<UserDto>(user);

            var userCountries = await _userCountryRepository
                .Get(uc => uc.UserId == user.Id)
                .ToListAsync();

            var countries = userCountries
                .Select(uc => _countryRepository.Get(country => country.Code == uc.CountryCode).FirstOrDefault());

            dto.Countries = _mapper.Map<IEnumerable<CountryDto>>(countries);

            return dto;
        }

        public async Task AddCountryToUser(User user, Country country)
        {
            _userCountryRepository.Add(new UserCountry
            {
                UserId = user.Id,
                CountryCode = country.Code
            });

            await _userCountryRepository.Save();
        }

        public async Task RemoveCountryFromUser(User user, Country country)
        {
            var userCountry = _userCountryRepository
                .Get(uc => uc.UserId == user.Id && uc.CountryCode == country.Code)
                .FirstOrDefault();

            if (userCountry is null)
            {
                throw new Exception("User does not have given country linked.");
            }

            _userCountryRepository.Delete(userCountry);

            await _userCountryRepository.Save();
        }
    }
}

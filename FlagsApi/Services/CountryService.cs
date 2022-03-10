using FlagsApi.Models;
using FlagsApi.Repositories;
using FlagsApi.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FlagsApi.Services
{
    public class CountryService : ICountryService
    {
        private readonly IRepositoryBase<Country> _countryRepository;

        public CountryService(IRepositoryBase<Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<IEnumerable<Country>> GetCountries()
        {
            return await _countryRepository.Get().ToListAsync();
        }

        public async Task<Country?> GetCountry(string code)
        {
            return await _countryRepository
                .Get(country => country.Code == code)
                .FirstOrDefaultAsync();
        }

        public async Task AddCountry(Country country)
        {
            _countryRepository.Add(country);
            await _countryRepository.Save();
        }

        public async Task UpdateCountry(Country country)
        {
            var countryEntity = _countryRepository.Get(c => c.Code == country.Code).FirstOrDefault();

            if (countryEntity is null) 
                return;

            countryEntity.Name = country.Name;
            countryEntity.FlagLink = country.FlagLink;

            await _countryRepository.Save();
        }

        public async Task DeleteCountry(Country country)
        {
            _countryRepository.Delete(country);
            await _countryRepository.Save();
        }
    }
}

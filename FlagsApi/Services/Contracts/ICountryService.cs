using FlagsApi.Models;

namespace FlagsApi.Services.Contracts
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetCountries();
        Task<Country?> GetCountry(string code);
        Task AddCountry(Country country);
        Task UpdateCountry(Country country);
        Task DeleteCountry(Country country);
    }
}

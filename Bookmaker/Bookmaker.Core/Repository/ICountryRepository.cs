using Bookmaker.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Core.Repository
{
    public interface ICountryRepository
    {
        Task<Country> GetByIdAsync(int id);
        Task<Country> GetByNameAsync(string name);
        Task<IEnumerable<Country>> GetAllAsync();
        Task<IEnumerable<City>> GetCitiesAsync(int countryId);
        Task<IEnumerable<Stadium>> GetStadiumsAsync(int countryId);
        Task CreateAsync(Country country);
        Task UpdateAsync(Country country);
        Task DeleteAsync(int id);
    }
}
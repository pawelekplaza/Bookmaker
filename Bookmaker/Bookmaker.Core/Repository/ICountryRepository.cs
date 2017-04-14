using Bookmaker.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Core.Repository
{
    public interface ICountryRepository
    {
        Task<Country> GetAsync(int id);
        Task<Country> GetAsync(string name);
        Task<IEnumerable<Country>> GetAllAsync();
        Task CreateAsync(Country country);
        Task UpdateAsync(Country country);
        Task DeleteAsync(int id);
    }
}
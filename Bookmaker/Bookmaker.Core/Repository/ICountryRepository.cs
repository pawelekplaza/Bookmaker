using Bookmaker.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Core.Repository
{
    public interface ICountryRepository
    {
        Task<Country> GetAsync(Guid id);
        Task<IEnumerable<Country>> GetAllAsync();
        Task AddAsync(Country country);
        Task UpdateAsync(Country country);
        Task RemoveAsync(Guid id);
    }
}
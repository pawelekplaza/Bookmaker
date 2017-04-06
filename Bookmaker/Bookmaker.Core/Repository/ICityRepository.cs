using Bookmaker.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Core.Repository
{
    public interface ICityRepository
    {
        Task<City> GetAsync(Guid id);
        Task<IEnumerable<City>> GetAsync(string name);
        Task<IEnumerable<City>> GetAllAsync();
        Task AddAsync(City city);
        Task UpdateAsync(City city);
        Task RemoveAsync(Guid id);
    }
}

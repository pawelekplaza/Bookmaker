using Bookmaker.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Core.Repository
{
    public interface ICityRepository
    {
        Task CreateAsync(City city);
        Task<City> GetAsync(int id);
        Task<IEnumerable<City>> GetAsync(string name);
        Task<IEnumerable<City>> GetAllAsync();        
        Task UpdateAsync(City city);
        Task DeleteAsync(int id);
    }
}

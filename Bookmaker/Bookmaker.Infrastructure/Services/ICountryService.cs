using Bookmaker.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Infrastructure.Services
{
    public interface ICountryService
    {
        Task<CountryDto> GetAsync(int id);
        Task<CountryDto> GetAsync(string name);
        Task<IEnumerable<CountryDto>> GetAllAsync();
        Task CreateAsync(CountryCreateDto country);
        Task UpdateAsync(CountryUpdateDto country);
        Task DeleteAsync(int id);
    }
}

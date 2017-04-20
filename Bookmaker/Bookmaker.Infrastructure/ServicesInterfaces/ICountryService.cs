using Bookmaker.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Infrastructure.ServicesInterfaces
{
    public interface ICountryService
    {
        Task<CountryDto> GetByIdAsync(int id);
        Task<CountryDto> GetByNameAsync(string name);
        Task<IEnumerable<CountryDto>> GetAllAsync();
        Task<IEnumerable<CityDto>> GetCitiesAsync(int countryId);
        Task<IEnumerable<StadiumDto>> GetStadiumsAsync(int countryId);
        Task CreateAsync(CountryCreateDto country);
        Task UpdateAsync(CountryUpdateDto country);
        Task DeleteAsync(int id);
    }
}

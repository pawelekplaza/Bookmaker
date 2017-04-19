using Bookmaker.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Infrastructure.Services
{
    public interface ICityService
    {
        Task CreateAsync(CityCreateDto city);
        Task<CityDto> GetAsync(int id);
        Task<IEnumerable<CityDto>> GetAsync(string name);
        Task<IEnumerable<CityDto>> GetAllAsync();
        Task UpdateAsync(CityUpdateDto city);
        Task DeleteAsync(int id);

    }
}

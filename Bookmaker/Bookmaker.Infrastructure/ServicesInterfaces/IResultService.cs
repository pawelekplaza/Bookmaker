using Bookmaker.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Infrastructure.ServicesInterfaces
{
    public interface IResultService
    {
        Task CreateAsync(ResultDto result);
        Task<IEnumerable<ResultDto>> GetAllAsync();
        Task<ResultDto> GetByIdAsync(int id);
        Task UpdateAsync(ResultUpdateDto result);
        Task DeleteAsync(int id);
    }
}

using Bookmaker.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Infrastructure.ServicesInterfaces
{
    public interface IBetService
    {
        Task CreateAsync(BetDto bet);
        Task<IEnumerable<BetDto>> GetAllAsync();
        Task<BetDto> GetByIdAsync(int id);
        Task UpdateAsync(BetUpdateDto bet);
        Task DeleteAsync(int id);
    }
}

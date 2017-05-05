using Bookmaker.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Infrastructure.ServicesInterfaces
{
    public interface IMatchService
    {
        Task CreateAsync(MatchDto match);
        Task<IEnumerable<MatchDto>> GetAllAsync();
        Task<IEnumerable<BetDto>> GetBetsAsync(int matchId);
        Task<MatchDto> GetByIdAsync(int id);
        Task UpdateAsync(MatchUpdateDto match);
        Task DeleteAsync(int id);
    }
}

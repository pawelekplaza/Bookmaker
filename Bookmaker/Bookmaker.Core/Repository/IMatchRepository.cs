using Bookmaker.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Core.Repository
{
    public interface IMatchRepository
    {
        Task CreateAsync(Match match);
        Task<IEnumerable<Match>> GetAllAsync();
        Task<IEnumerable<Bet>> GetBetsAsync(int matchId);
        Task<Match> GetByIdAsync(int id);
        Task UpdateAsync(Match match);
        Task DeleteAsync(int id);
    }
}

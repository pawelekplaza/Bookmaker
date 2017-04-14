using Bookmaker.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Core.Repository
{
    public interface IScoreRepository
    {
        Task CreateAsync(Score score);
        Task<IEnumerable<Score>> GetAllAsync();
        Task<Score> GetAsync(int id);
        Task UpdateAsync(Score score);
        Task DeleteAsync(int id);
    }
}

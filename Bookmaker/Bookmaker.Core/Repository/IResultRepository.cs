using Bookmaker.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Core.Repository
{
    public interface IResultRepository
    {
        Task CreateAsync(Result result);
        Task<IEnumerable<Result>> GetAllAsync();
        Task<IEnumerable<Match>> GetMatchesAsync(int resultId);
        Task<Result> GetByIdAsync(int id);
        Task UpdateAsync(Result result);
        Task DeleteAsync(int id);
    }
}

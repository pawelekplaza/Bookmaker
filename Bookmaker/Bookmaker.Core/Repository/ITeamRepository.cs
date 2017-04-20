using Bookmaker.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Core.Repository
{
    public interface ITeamRepository
    {
        Task CreateAsync(Team team);
        Task<IEnumerable<Team>> GetAllAsync();
        Task<Team> GetByIdAsync(int id);
        Task UpdateAsync(Team team);
        Task DeleteAsync(int id);
    }
}

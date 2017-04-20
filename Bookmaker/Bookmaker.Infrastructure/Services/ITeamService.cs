using Bookmaker.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Infrastructure.Services
{
    public interface ITeamService
    {
        Task CreateAsync(TeamDto team);
        Task<IEnumerable<TeamDto>> GetAllAsync();
        Task<TeamDto> GetByIdAsync(int id);
        Task UpdateAsync(TeamUpdateDto team);
        Task DeleteAsync(int id);
    }
}

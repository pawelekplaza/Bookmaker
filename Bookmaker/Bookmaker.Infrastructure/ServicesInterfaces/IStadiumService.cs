using Bookmaker.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Infrastructure.ServicesInterfaces
{
    public interface IStadiumService
    {
        Task CreateAsync(StadiumDto stadium);
        Task<StadiumDto> GetAsync(int id);
        Task<IEnumerable<StadiumDto>> GetAllAsync();
        Task<IEnumerable<MatchDto>> GetMatchesAsync(int stadiumId);
        Task UpdateAsync(StadiumUpdateDto stadium);
        Task DeleteAsync(int id);
    }
}

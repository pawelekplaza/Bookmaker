using Bookmaker.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Infrastructure.Services
{
    public interface IScoreService
    {
        Task CreateAsync(ScoreCreateDto score);
        Task<IEnumerable<ScoreDto>> GetAllAsync();
        Task<ScoreDto> GetAsync(int id);
        Task UpdateAsync(ScoreUpdateDto score);
        Task DeleteAsync(int id);
    }
}
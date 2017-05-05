using Bookmaker.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Core.Repository
{
    public interface IStadiumRepository
    {
        Task CreateAsync(Stadium stadium);
        Task<IEnumerable<Stadium>> GetAllAsync();
        Task<IEnumerable<Match>> GetMatchesAsync(int stadiumId);
        Task<Stadium> GetAsync(int id);
        Task UpdateAsync(Stadium stadium);
        Task DeleteAsync(int id);
    }
}

using Bookmaker.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Core.Repository
{
    public interface IBetRepository
    {
        Task CreateAsync(Bet bet);
        Task<IEnumerable<Bet>> GetAllAsync();
        Task<Bet> GetByIdAsync(int id);
        Task UpdateAsync(Bet bet);
        Task DeleteAsync(int id);
    }
}

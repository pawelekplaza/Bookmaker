using Bookmaker.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Bookmaker.Core.Domain;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Bookmaker.Infrastructure.Helpers;

namespace Bookmaker.Infrastructure.Repositories
{
    public class DbBetRepository : IBetRepository
    {
        public async Task CreateAsync(Bet bet)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
               
            }
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Bet>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Bet> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Bet bet)
        {
            throw new NotImplementedException();
        }
    }
}

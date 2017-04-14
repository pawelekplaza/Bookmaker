using Bookmaker.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Bookmaker.Core.Domain;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Bookmaker.Infrastructure.Helpers;
using Dapper;
using System.Linq;
using Bookmaker.Core.Utils;

namespace Bookmaker.Infrastructure.Repositories
{
    public class DbScoreRepository : IScoreRepository
    {
        public async Task CreateAsync(Score score)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var listToAdd = new List<Score> { score };
                var executeString = "dbo.Scores_Insert @Goals, @Shots";

                await Task.Factory.StartNew(()
                    => connection.Execute(executeString, listToAdd));
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Scores_DeleteById @Id";

                await Task.Factory.StartNew(()
                    => connection.Execute(executeString, new { Id = id }));
            }
        }

        public async Task<IEnumerable<Score>> GetAllAsync()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                return await Task.Factory.StartNew(()
                    => connection.Query<Score>("dbo.Scores_GetAll"));
            }
        }

        public async Task<Score> GetAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                List<Score> output = new List<Score>();

                await Task.Factory.StartNew(()
                    => output = connection.Query<Score>("dbo.Scores_GetById @Id", new { Id = id }).ToList());

                if (output == null)
                {
                    return null;
                }        

                if (output.Count == 0)
                {
                    return null;
                }

                if (output.Count > 1)
                {
                    throw new InvalidDataException($"More than one score with id'{ id }' found.");
                }

                return output[0];
            }
        }

        public async Task UpdateAsync(Score score)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Scores_UpdateById @Id, @Goals, @Shots";

                await Task.Factory.StartNew(()
                    => connection.Execute(executeString, new { Id = score.Id, Goals = score.Goals, Shots = score.Shots }));
            }
        }
    }
}

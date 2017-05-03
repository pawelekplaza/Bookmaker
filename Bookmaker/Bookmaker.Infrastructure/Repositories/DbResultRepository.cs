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
using Bookmaker.Infrastructure.DTO;
using System.Linq;
using Bookmaker.Core.Utils;

namespace Bookmaker.Infrastructure.Repositories
{
    public class DbResultRepository : IResultRepository
    {
        private readonly ICommonDataProvider _commonDataProvider;

        public DbResultRepository()
        {
            _commonDataProvider = new CommonDataProvider();
        }

        public async Task CreateAsync(Result result)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {                
                var listToAdd = new List<ResultDto>
                {
                    new ResultDto { Id = result.Id, HostScoreId = result.HostScore.Id, GuestScoreId = result.GuestScore.Id }
                };

                var executeString = "dbo.Results_Insert @HostScoreId, @GuestScoreId";

                await connection.ExecuteAsync(executeString, listToAdd);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Results_DeleteById @Id";

                await connection.ExecuteAsync(executeString, new { Id = id });
            }
        }

        public async Task<IEnumerable<Result>> GetAllAsync()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var resultDtos = await connection.QueryAsync<ResultDto>("dbo.Results_GetAll");

                var results = new List<Result>();

                foreach (var result in resultDtos)
                {                    
                    var hostScore = await _commonDataProvider.GetScoreAsync(result.HostScoreId);
                    var guestScore = await _commonDataProvider.GetScoreAsync(result.GuestScoreId);

                    var newResult = new Result(hostScore, guestScore);
                    newResult.SetId(result.Id);

                    results.Add(newResult);
                }

                return results;
            }
        }

        public async Task<Result> GetByIdAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var queryResult = await connection.QueryAsync<ResultDto>("dbo.Results_GetById @Id", new { Id = id });
                var resultDto = queryResult.ToList();

                if (resultDto == null)
                {
                    return null;
                }

                if (resultDto.Count == 0)
                {
                    return null;
                }

                if (resultDto.Count > 1)
                {
                    throw new InvalidDataException($"More than one result with id '{ id }' found.");
                }
                
                var hostScore = await _commonDataProvider.GetScoreAsync(resultDto[0].HostScoreId);
                var guestScore = await _commonDataProvider.GetScoreAsync(resultDto[0].GuestScoreId);
                var result = new Result(hostScore, guestScore);
                result.SetId(resultDto[0].Id);

                return result;
            }
        }

        public async Task UpdateAsync(Result result)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Results_UpdateById @Id, @HostScoreId, @GuestScoreId";

                await connection.ExecuteAsync(executeString, new { Id = result.Id, HostScoreId = result.HostScore.Id, GuestScoreId = result.GuestScore.Id });
            }
        }        
    }
}

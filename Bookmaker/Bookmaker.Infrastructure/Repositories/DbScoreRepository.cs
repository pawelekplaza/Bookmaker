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
using Bookmaker.Infrastructure.DTO;

namespace Bookmaker.Infrastructure.Repositories
{
    public class DbScoreRepository : IScoreRepository
    {
        private readonly ICommonDataProvider _commonDataProvider;

        public DbScoreRepository()
        {
            _commonDataProvider = new CommonDataProvider();
        }

        public async Task CreateAsync(Score score)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var listToAdd = new List<Score> { score };
                var executeString = "dbo.Scores_Insert @Goals, @Shots";

                await connection.ExecuteAsync(executeString, listToAdd);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Scores_DeleteById @Id";

                await connection.ExecuteAsync(executeString, new { Id = id });
            }
        }

        public async Task<IEnumerable<Score>> GetAllAsync()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                return await connection.QueryAsync<Score>("dbo.Scores_GetAll");
            }
        }

        public async Task<Score> GetAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var queryResult = await connection.QueryAsync<Score>("dbo.Scores_GetById @Id", new { Id = id });
                var scoreDto = queryResult.ToList();                

                if (scoreDto == null)
                {
                    return null;
                }        

                if (scoreDto.Count == 0)
                {
                    return null;
                }

                if (scoreDto.Count > 1)
                {
                    throw new InvalidDataException($"More than one score with id'{ id }' found.");
                }

                return scoreDto[0];
            }
        }

        public async Task<IEnumerable<Bet>> GetBetsAsync(int scoreId)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var queryResult = await connection.QueryAsync<BetDto>("dbo.Scores_GetBets @Id", new { Id = scoreId });
                var betDtos = queryResult.ToList();

                var resultList = new List<Bet>();

                foreach (var bet in betDtos)
                {
                    var user = await _commonDataProvider.GetUserAsync(bet.UserId);
                    var match = await _commonDataProvider.GetMatchAsync(bet.MatchId);
                    var team = await _commonDataProvider.GetTeamAsync(bet.TeamId);
                    var score = await _commonDataProvider.GetScoreAsync(bet.ScoreId);

                    var newBet = new Bet(bet.Price, user, match, team, score);
                    newBet.SetId(bet.Id);

                    resultList.Add(newBet);
                }

                return resultList;
            }
        }

        public async Task UpdateAsync(Score score)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Scores_UpdateById @Id, @Goals, @Shots";

                await connection.ExecuteAsync(executeString, new { Id = score.Id, Goals = score.Goals, Shots = score.Shots });
            }
        }
    }
}

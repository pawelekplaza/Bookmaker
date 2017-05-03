using Bookmaker.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Bookmaker.Core.Domain;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Bookmaker.Infrastructure.Helpers;
using Bookmaker.Infrastructure.DTO;
using Dapper;
using System.Linq;
using Bookmaker.Core.Utils;

namespace Bookmaker.Infrastructure.Repositories
{
    public class DbBetRepository : IBetRepository
    {
        private readonly ICommonDataProvider _commonDataProvider;

        public DbBetRepository()
        {
            _commonDataProvider = new CommonDataProvider();
        }

        public async Task CreateAsync(Bet bet)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var listToAdd = new List<BetDto>
                {
                   new BetDto
                   {
                       UserId = bet.User.Id,
                       MatchId = bet.Match.Id,
                       ScoreId = bet.Score.Id,
                       TeamId = bet.Team.Id,
                       Price = bet.Price,
                       CreatedAt = bet.CreatedAt,
                       LastUpdate = bet.LastUpdate
                   }
                };

                var executeString = "dbo.Bets_Insert @UserId, @MatchId, @ScoreId, @TeamId, @Price, @CreatedAt, @LastUpdate";

                await connection.ExecuteAsync(executeString, listToAdd);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Bets_DeleteById @Id";

                await connection.ExecuteAsync(executeString, new { Id = id });
            }
        }

        public async Task<IEnumerable<Bet>> GetAllAsync()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var betsDto = await connection.QueryAsync<BetDto>("dbo.Bets_GetAll");

                var bets = new List<Bet>();

                foreach (var bet in betsDto)
                {
                    var user = await _commonDataProvider.GetUserAsync(bet.UserId);
                    var match = await _commonDataProvider.GetMatchAsync(bet.MatchId);
                    var team = await _commonDataProvider.GetTeamAsync(bet.TeamId);
                    var score = await _commonDataProvider.GetScoreAsync(bet.ScoreId);

                    var newBet = new Bet(bet.Price, user, match, team, score);
                    newBet.SetId(bet.Id);
                    newBet.SetCreatedAt(bet.CreatedAt);
                    newBet.SetLastUpdate(bet.LastUpdate);

                    bets.Add(newBet);
                }

                return bets;
            }
        }

        public async Task<Bet> GetByIdAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var queryResult = await connection.QueryAsync<BetDto>("dbo.Bets_GetById @Id", new { Id = id });
                var betDto = queryResult.ToList();

                if (betDto == null)
                {
                    return null;
                }

                if (betDto.Count == 0)
                {
                    return null;
                }

                if (betDto.Count > 1)
                {
                    throw new InvalidDataException($"More than one bet with id '{ id }' found.");
                }

                var user = await _commonDataProvider.GetUserAsync(betDto[0].UserId);
                var match = await _commonDataProvider.GetMatchAsync(betDto[0].MatchId);
                var team = await _commonDataProvider.GetTeamAsync(betDto[0].TeamId);
                var score = await _commonDataProvider.GetScoreAsync(betDto[0].ScoreId);

                var bet = new Bet(betDto[0].Price, user, match, team, score);
                bet.SetId(betDto[0].Id);
                bet.SetCreatedAt(betDto[0].CreatedAt);
                bet.SetLastUpdate(betDto[0].LastUpdate);

                return bet;
            }
        }

        public async Task UpdateAsync(Bet bet)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Bets_Update @Id, @Price, @UserId, @MatchId, @TeamId, @ScoreId, @CreatedAt, @LastUpdate";

                var executeObject = new BetDto
                {
                    Id = bet.Id,
                    UserId = bet.User.Id,
                    MatchId = bet.Match.Id,
                    TeamId = bet.Team.Id,
                    ScoreId = bet.Score.Id,
                    Price = bet.Price,
                    CreatedAt = bet.CreatedAt,
                    LastUpdate = bet.LastUpdate
                };

                await connection.ExecuteAsync(executeString, executeObject);
            }
        }
    }
}

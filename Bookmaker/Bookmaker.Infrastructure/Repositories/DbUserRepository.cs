using Bookmaker.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Bookmaker.Core.Domain;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Bookmaker.Infrastructure.Helpers;
using System.Data;
using Dapper;
using System.Linq;
using Bookmaker.Core.Utils;
using Bookmaker.Infrastructure.DTO;

namespace Bookmaker.Infrastructure.Repositories
{
    public class DbUserRepository : IUserRepository
    {
        private readonly ICommonDataProvider _commonDataProvider;

        public DbUserRepository()
        {
            _commonDataProvider = new CommonDataProvider();
        }

        public async Task AddAsync(User user)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var listToAdd = new List<User> { user };
                var executeString = "dbo.Users_Insert @Email, @Salt, @Hash, @Username, @Role, @FullName, @WalletPoints, @CreatedAt, @LastUpdate";

                await connection.ExecuteAsync(executeString, listToAdd);
            }            
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {                
                return await connection.QueryAsync<User>("dbo.Users_GetAll");
            }
        }

        public async Task<User> GetAsync(string email)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var queryResult = await connection.QueryAsync<User>("dbo.Users_GetByEmail @Email", new { Email = email });
                var user = queryResult.ToList();                
                
                if (user == null)
                {
                    return null;
                }

                if (user.Count == 0)
                {
                    return null;
                }

                if (user.Count > 1)
                {
                    throw new InvalidDataException($"DbUserRepository: More than one user with email '{ email }' found.");
                }

                return user[0];
            }
        }

        public async Task<IEnumerable<Bet>> GetBetsAsync(string userEmail)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var queryResult = await connection.QueryAsync<BetDto>("dbo.Users_GetBets @Email", new { Email = userEmail });
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

        public async Task<User> GetByIdAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var queryResult = await connection.QueryAsync<User>("dbo.Users_GetById @Id", new { Id = id });
                var user = queryResult.ToList();                

                if (user == null)
                {
                    return null;
                }

                if (user.Count == 0)
                {
                    return null;
                }

                if (user.Count > 1)
                {
                    throw new InvalidDataException($"More than one user with id '{ id }' found.");
                }

                return user[0];
            }
        }

        public async Task RemoveAsync(string email)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Users_RemoveByEmail @Email";

                await connection.ExecuteAsync(executeString, new { Email = email });
            }
        }

        public async Task UpdateAsync(User user)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Users_UpdateUser @Email, @Username, @Role, @Salt, @Hash, @FullName";

                await connection.ExecuteAsync(executeString, new { Email = user.Email, Username = user.Username, Role = user.Role, Salt = user.Salt, Hash = user.Salt, FullName = user.FullName });
            }
        }
    }
}

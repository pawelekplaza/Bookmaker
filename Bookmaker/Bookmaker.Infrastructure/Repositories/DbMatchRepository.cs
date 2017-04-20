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
    public class DbMatchRepository : IMatchRepository
    {
        private readonly ICommonDataProvider _commonDataProvider;

        public DbMatchRepository()
        {
            _commonDataProvider = new CommonDataProvider();
        }

        public async Task CreateAsync(Match match)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var listToAdd = new List<MatchDto>
                {
                    new MatchDto
                    {
                        Id = match.Id,
                        HostTeamId = match.HostTeam.Id,
                        GuestTeamId = match.GuestTeam.Id,
                        StadiumId = match.Stadium.Id,
                        StartTime = match.StartTime
                    }
                };

                if (match.Result != null)
                {
                    listToAdd[0].ResultId = match.Result.Id;
                }

                var executeString = "dbo.Matches_Insert @HostTeamId, @GuestTeamId, @StadiumId, @StartTime, @ResultId";

                await Task.Factory.StartNew(()
                    => connection.Execute(executeString, listToAdd));               
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Matches_DeleteById @Id";

                await Task.Factory.StartNew(()
                    => connection.Execute(executeString, new { Id = id }));
            }
        }

        public async Task<IEnumerable<Match>> GetAllAsync()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var matchesDto = await Task.Factory.StartNew(()
                    => connection.Query<MatchDto>("dbo.Matches_GetAll"));

                var matches = new List<Match>();

                foreach (var match in matchesDto)
                {
                    var hostTeam = await _commonDataProvider.GetTeamAsync(match.HostTeamId);
                    var guestTeam = await _commonDataProvider.GetTeamAsync(match.GuestTeamId);
                    var stadium = await _commonDataProvider.GetStadiumAsync(match.StadiumId);                    

                    var newMatch = new Match(hostTeam, guestTeam, stadium, match.StartTime);
                    newMatch.SetId(match.Id);

                    if (match.ResultId != null)
                    {
                        var result = await _commonDataProvider.GetResultAsync(match.ResultId.Value);
                        newMatch.SetResult(result);
                    }

                    matches.Add(newMatch);
                }

                return matches;
            }
        }

        public async Task<Match> GetByIdAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var matchDto = await Task.Factory.StartNew(()
                    => connection.Query<MatchDto>("dbo.Matches_GetById @Id", new { Id = id }).ToList());

                if (matchDto == null)
                {
                    return null;
                }

                if (matchDto.Count == 0)
                {
                    return null;
                }

                if (matchDto.Count > 1)
                {
                    throw new InvalidDataException($"More than one match with id '{ id }' found.");
                }

                var hostTeam = await _commonDataProvider.GetTeamAsync(matchDto[0].HostTeamId);
                var guestTeam = await _commonDataProvider.GetTeamAsync(matchDto[0].GuestTeamId);
                var stadium = await _commonDataProvider.GetStadiumAsync(matchDto[0].StadiumId);

                var match = new Match(hostTeam, guestTeam, stadium, matchDto[0].StartTime);
                match.SetId(matchDto[0].Id);

                return match;
            }
        }

        public async Task UpdateAsync(Match match)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Matches_Update @Id, @HostTeamId, @GuestTeamId, @StadiumId, @StartTime, @ResultId";

                var executeObject = new MatchDto
                {
                    Id = match.Id,
                    HostTeamId = match.HostTeam.Id,
                    GuestTeamId = match.GuestTeam.Id,
                    StadiumId = match.Stadium.Id,
                    StartTime = match.StartTime
                };

                if (match.Result != null)
                {
                    executeObject.ResultId = match.Result.Id;
                }

                await Task.Factory.StartNew(()
                    => connection.Execute(executeString, executeObject));              
            }
        }        
    }
}

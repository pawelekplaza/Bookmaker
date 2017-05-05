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
    public class DbTeamRepository : ITeamRepository
    {
        private readonly ICommonDataProvider _commonDataProvider;

        public DbTeamRepository()
        {
            _commonDataProvider = new CommonDataProvider();
        }

        public async Task CreateAsync(Team team)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var listToAdd = new List<TeamDto>
                {
                    new TeamDto { Id = team.Id, Name = team.Name, StadiumId = team.Stadium.Id }
                };

                var executeString = "dbo.Teams_Insert @Name, @StadiumId";

                await connection.ExecuteAsync(executeString, listToAdd);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Teams_DeleteById @Id";

                await connection.ExecuteAsync(executeString, new { Id = id });
            }
        }

        public async Task<IEnumerable<Team>> GetAllAsync()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var teamDtos = await connection.QueryAsync<TeamDto>("dbo.Teams_GetAll");

                var teams = new List<Team>();

                foreach (var team in teamDtos)
                {
                    var stadium = await _commonDataProvider.GetStadiumAsync(team.StadiumId);
                    var newTeam = new Team(stadium, team.Name);
                    newTeam.SetId(team.Id);

                    teams.Add(newTeam);
                }

                return teams;
            }
        }

        public async Task<Team> GetByIdAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var queryResult = await connection.QueryAsync<TeamDto>("dbo.Teams_GetById @Id", new { Id = id });
                var teamDto = queryResult.ToList();                

                if (teamDto == null)
                {
                    return null;
                }

                if (teamDto.Count == 0)
                {
                    return null;
                }

                if (teamDto.Count > 1)
                {
                    throw new InvalidDataException($"More than one team with id '{ id }' found.");
                }

                var stadium = await _commonDataProvider.GetStadiumAsync(teamDto[0].StadiumId);

                var team = new Team(stadium, teamDto[0].Name);
                team.SetId(teamDto[0].Id);

                return team;
            }
        }

        public async Task<IEnumerable<Match>> GetMatchesAsync(int teamId)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var queryResult = await connection.QueryAsync<MatchDto>("dbo.Teams_GetMatches @Id", new { Id = teamId });
                var matchDtos = queryResult.ToList();

                var resultList = new List<Match>();

                foreach (var match in matchDtos)
                {
                    var hostTeam = await _commonDataProvider.GetTeamAsync(match.HostTeamId);
                    var guestTeam = await _commonDataProvider.GetTeamAsync(match.GuestTeamId);
                    var stadium = await _commonDataProvider.GetStadiumAsync(match.StadiumId);

                    var newMatch = new Match(hostTeam, guestTeam, stadium, match.StartTime);
                    newMatch.SetId(match.Id);

                    resultList.Add(newMatch);
                }

                return resultList;
            }
        }

        public async Task UpdateAsync(Team team)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Teams_Update @Id, @Name, @StadiumId";

                await connection.ExecuteAsync(executeString, new { Id = team.Id, Name = team.Name, StadiumId = team.Stadium.Id });
            }
        }     
    }
}

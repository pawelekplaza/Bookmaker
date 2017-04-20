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
        public async Task CreateAsync(Team team)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var listToAdd = new List<TeamDto>
                {
                    new TeamDto { Id = team.Id, Name = team.Name, StadiumId = team.Stadium.Id }
                };

                var executeString = "dbo.Teams_Insert @Name, @StadiumId";

                await Task.Factory.StartNew(()
                    => connection.Execute(executeString, listToAdd));
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Teams_DeleteById @Id";

                await Task.Factory.StartNew(()
                    => connection.Execute(executeString, new { Id = id }));
            }
        }

        public async Task<IEnumerable<Team>> GetAllAsync()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var teamsDto = await Task.Factory.StartNew(()
                    => connection.Query<TeamDto>("dbo.Teams_GetAll"));

                var teams = new List<Team>();

                foreach (var team in teamsDto)
                {
                    var stadium = await GetStadiumAsync(team.StadiumId);
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
                var teamDto = await Task.Factory.StartNew(()
                    => connection.Query<TeamDto>("dbo.Teams_GetById @Id", new { Id = id }).ToList());

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

                var stadium = await GetStadiumAsync(teamDto[0].StadiumId);

                var team = new Team(stadium, teamDto[0].Name);
                team.SetId(teamDto[0].Id);

                return team;
            }
        }

        public async Task UpdateAsync(Team team)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Teams_Update @Id, @Name, @StadiumId";

                await Task.Factory.StartNew(()
                    => connection.Execute(executeString, new { Id = team.Id, Name = team.Name, StadiumId = team.Stadium.Id }));
            }
        }

        private async Task<Stadium> GetStadiumAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var stadiumDto = await Task.Factory.StartNew(()
                    => connection.Query<StadiumDto>("dbo.Stadiums_GetById @Id", new { Id = id }).ToList());

                if (stadiumDto == null)
                {
                    return null; 
                }

                if (stadiumDto.Count == 0)
                {
                    return null;
                }

                if (stadiumDto.Count > 1)
                {
                    return null;
                }

                var country = await GetCountryAsync(stadiumDto[0].CountryId);
                var city = await GetCityAsync(stadiumDto[0].CityId);

                var stadium = new Stadium(country, city, stadiumDto[0].Name);
                stadium.SetId(stadiumDto[0].Id);

                return stadium;
            }
        }

        private async Task<Country> GetCountryAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var countryDto = await Task.Factory.StartNew(()
                    => connection.Query<CountryDto>("dbo.Countries_GetById @Id", new { Id = id }).ToList());

                if (countryDto == null)
                {
                    return null;
                }

                if (countryDto.Count == 0)
                {
                    return null;
                }

                if (countryDto.Count > 1)
                {
                    return null;
                }

                var country = new Country(countryDto[0].Name);
                country.SetId(countryDto[0].Id);

                return country;
            }
        }

        private async Task<City> GetCityAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var cityDto = await Task.Factory.StartNew(()
                    => connection.Query<CityDto>("dbo.Cities_GetById @Id", new { Id = id }).ToList());

                if (cityDto == null)
                {
                    return null;
                }

                if (cityDto.Count == 0)
                {
                    return null;
                }

                if (cityDto.Count > 1)
                {
                    return null;
                }

                var country = await GetCountryAsync(cityDto[0].CountryId);

                var city = new City(cityDto[0].Name, country);
                city.SetId(cityDto[0].Id);

                return city;
            }
        }
    }
}

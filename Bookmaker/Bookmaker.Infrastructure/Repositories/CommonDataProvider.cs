using Bookmaker.Core.Domain;
using Bookmaker.Infrastructure.DTO;
using Bookmaker.Infrastructure.Helpers;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Infrastructure.Repositories
{
    public class CommonDataProvider : ICommonDataProvider
    {
        public async Task<Country> GetCountryAsync(int id)
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

        public async Task<City> GetCityAsync(int id)
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

        public async Task<Score> GetScoreAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var scoreDto = await Task.Factory.StartNew(()
                    => connection.Query<ScoreDto>("dbo.Scores_GetById @Id", new { Id = id }).ToList());

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
                    return null;
                }

                var score = new Score(scoreDto[0].Goals, scoreDto[0].Shots);
                score.SetId(scoreDto[0].Id);

                return score;
            }
        }

        public async Task<Team> GetTeamAsync(int id)
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
                    return null;
                }

                var stadium = await GetStadiumAsync(teamDto[0].StadiumId);

                var newTeam = new Team(stadium, teamDto[0].Name);
                newTeam.SetId(teamDto[0].Id);

                return newTeam;
            }
        }

        public async Task<Stadium> GetStadiumAsync(int id)
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

        public async Task<Result> GetResultAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var resultDto = await Task.Factory.StartNew(()
                    => connection.Query<ResultDto>("dbo.Results_GetById @Id", new { Id = id }).ToList());

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
                    return null;
                }

                var hostScore = await GetScoreAsync(resultDto[0].HostScoreId);
                var guestScore = await GetScoreAsync(resultDto[0].GuestScoreId);

                var result = new Result(hostScore, guestScore);
                result.SetId(resultDto[0].Id);

                return result;
            }
        }
    }
}

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
    public class DbStadiumRepository : IStadiumRepository
    {
        private readonly ICommonDataProvider _commonDataProvider;

        public DbStadiumRepository()
        {
            _commonDataProvider = new CommonDataProvider();
        }

        public async Task CreateAsync(Stadium stadium)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var listToAdd = new List<StadiumDto>
                {
                    new StadiumDto { Name = stadium.Name, CityId = stadium.City.Id, CountryId = stadium.Country.Id }
                };

                var executeString = "dbo.Stadiums_Insert @Name, @CityId, @CountryId";

                await connection.ExecuteAsync(executeString, listToAdd);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Stadiums_DeleteById @Id";

                await connection.ExecuteAsync(executeString, new { Id = id });
            }
        }

        public async Task<IEnumerable<Stadium>> GetAllAsync()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var listOfDtos = await connection.QueryAsync<StadiumDto>("dbo.Stadiums_GetAll");

                var resultList = new List<Stadium>();

                foreach (var stadium in listOfDtos)
                {                    
                    var country = await _commonDataProvider.GetCountryAsync(stadium.CountryId);
                    var city = await _commonDataProvider.GetCityAsync(stadium.CityId);

                    var stadiumToAdd = new Stadium(country, city, stadium.Name);
                    stadiumToAdd.SetId(stadium.Id);

                    resultList.Add(stadiumToAdd);
                }

                return resultList;
            }
        }

        public async Task<Stadium> GetAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var queryResult = await connection.QueryAsync<StadiumDto>("dbo.Stadiums_GetById @Id", new { Id = id });
                var stadiumDto = queryResult.ToList();                

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
                    throw new InvalidDataException($"More than one stadium with id'{ id }' found.");
                }

                var country = await _commonDataProvider.GetCountryAsync(stadiumDto[0].CountryId);
                var city = await _commonDataProvider.GetCityAsync(stadiumDto[0].CityId);

                var result = new Stadium(country, city, stadiumDto[0].Name);
                result.SetId(stadiumDto[0].Id);

                return result;
            }
        }

        public async Task<IEnumerable<Match>> GetMatchesAsync(int stadiumId)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var queryResult = await connection.QueryAsync<MatchDto>("dbo.Stadiums_GetMatches @Id", new { Id = stadiumId });
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

        public async Task UpdateAsync(Stadium stadium)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Stadiums_UpdateById @Id, @Name, @CityId, @CountryId";

                await connection.ExecuteAsync(executeString, new { Id = stadium.Id, Name = stadium.Name, CityId = stadium.City.Id, CountryId = stadium.Country.Id });
            }
        }
    }
}

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
    public class DbCityRepository : ICityRepository
    {
        private readonly ICommonDataProvider _commonDataProvider;

        public DbCityRepository()
        {
            _commonDataProvider = new CommonDataProvider();
        }

        public async Task CreateAsync(City city)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var listToAdd = new List<CityCreateDto>
                {
                    new CityCreateDto { CountryId = city.Country.Id, Name = city.Name }
                };

                var executeString = "dbo.Cities_Insert @CountryId, @Name";

                await connection.ExecuteAsync(executeString, listToAdd);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var stadiumsInCity = await GetStadiumsAsync(id);
                if (stadiumsInCity != null)
                {
                    if (stadiumsInCity.Count() > 0)
                    {
                        throw new InvalidDataException($"Cannot delete the city with id '{ id }' if there is a stadium.");
                    }
                }

                var executeString = "dbo.Cities_DeleteById @Id";

                await connection.ExecuteAsync(executeString, new { Id = id });
            }
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var listOfDtos = await connection.QueryAsync<CityDto>("dbo.Cities_GetAll");

                var resultList = new List<City>();

                foreach (var city in listOfDtos)
                {                    
                    var country = await _commonDataProvider.GetCountryAsync(city.CountryId);

                    var newCity = new City(city.Name, country);
                    newCity.SetId(city.Id);

                    resultList.Add(newCity);
                }

                return resultList;
            }
        }

        public async Task<City> GetAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var queryResult = await connection.QueryAsync<CityDto>("dbo.Cities_GetById @Id", new { Id = id });
                var cityDtos = queryResult.ToList();                

                if (cityDtos == null)
                {
                    return null;
                }

                if (cityDtos.Count == 0)
                {
                    return null;
                }

                if (cityDtos.Count > 1)
                {
                    throw new InvalidDataException($"More than one city with id'{ id }' found.");
                }
                
                var country = await _commonDataProvider.GetCountryAsync(cityDtos[0].CountryId);

                var resultCity = new City(cityDtos[0].Name, country);
                resultCity.SetId(cityDtos[0].Id);

                return resultCity;
            }
        }


        public async Task<IEnumerable<City>> GetAsync(string name)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var queryResult = await connection.QueryAsync<CityDto>("dbo.Cities_GetByName @Name", new { Name = name });
                var cityDtos = queryResult.ToList();

                var resultList = new List<City>();

                foreach (var city in cityDtos)
                {                    
                    var country = await _commonDataProvider.GetCountryAsync(city.CountryId);

                    var newCity = new City(city.Name, country);
                    newCity.SetId(city.Id);

                    resultList.Add(newCity);
                }

                return resultList;
            }
        }

        public async Task<IEnumerable<Stadium>> GetStadiumsAsync(int cityId)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var queryResult = await connection.QueryAsync<StadiumDto>("dbo.Cities_GetStadiums @Id", new { Id = cityId });
                var stadiumDtos = queryResult.ToList();                  

                var resultList = new List<Stadium>();

                foreach (var stadium in stadiumDtos)
                {
                    var city = await GetAsync(stadium.CityId);                    
                    var country = await _commonDataProvider.GetCountryAsync(stadium.CountryId);

                    var newStadium = new Stadium(country, city, stadium.Name);
                    newStadium.SetId(stadium.Id);

                    resultList.Add(newStadium);
                }

                return resultList;
            }
        }

        public async Task UpdateAsync(City city)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Cities_UpdateById @Id, @CountryId, @Name";

                await connection.ExecuteAsync(executeString, new { Id = city.Id, CountryId = city.Country.Id, Name = city.Name });
            }
        }
    }
}

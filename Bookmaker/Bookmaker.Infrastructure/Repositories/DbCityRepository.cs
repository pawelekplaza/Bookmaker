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
        private readonly ICountryRepository _countryRepository;

        public DbCityRepository()
        {
            _countryRepository = new DbCountryRepository();
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

                await Task.Factory.StartNew(()
                    => connection.Execute(executeString, listToAdd));
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Cities_DeleteById @Id";

                await Task.Factory.StartNew(()
                    => connection.Execute(executeString, new { Id = id }));
            }
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var listOfDtos = await Task.Factory.StartNew(()
                    => connection.Query<CityDto>("dbo.Cities_GetAll"));

                var resultList = new List<City>();

                foreach (var city in listOfDtos)
                {
                    var country = await _countryRepository.GetAsync(city.CountryId);

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
                List<CityDto> output = new List<CityDto>();

                await Task.Factory.StartNew(()
                    => output = connection.Query<CityDto>("dbo.Cities_GetById @Id", new { Id = id }).ToList());

                if (output == null)
                {
                    return null;
                }

                if (output.Count == 0)
                {
                    return null;
                }

                if (output.Count > 1)
                {
                    throw new InvalidDataException($"More than one city with id'{ id }' found.");
                }

                var country = await _countryRepository.GetAsync(output[0].CountryId);

                var resultCity = new City(output[0].Name, country);
                resultCity.SetId(output[0].Id);

                return resultCity;
            }
        }


        public async Task<IEnumerable<City>> GetAsync(string name)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                List<CityDto> output = new List<CityDto>();

                await Task.Factory.StartNew(()
                    => output = connection.Query<CityDto>("dbo.Cities_GetByName @Name", new { Name = name }).ToList());

                var resultList = new List<City>();

                foreach (var city in output)
                {
                    var country = await _countryRepository.GetAsync(city.CountryId);

                    var newCity = new City(city.Name, country);
                    newCity.SetId(city.Id);

                    resultList.Add(newCity);
                }

                return resultList;
            }
        }

        public async Task UpdateAsync(City city)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                var executeString = "dbo.Cities_UpdateById @Id, @CountryId, @Name";

                await Task.Factory.StartNew(()
                    => connection.Execute(executeString, new { Id = city.Id, CountryId = city.Country.Id, Name = city.Name }));
            }
        }
    }
}

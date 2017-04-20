using Bookmaker.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Infrastructure.Repositories
{
    public interface ICommonDataProvider
    {
        Task<Country> GetCountryAsync(int id);
        Task<City> GetCityAsync(int id);
        Task<Score> GetScoreAsync(int id);
        Task<Team> GetTeamAsync(int id);
        Task<Stadium> GetStadiumAsync(int id);
        Task<Result> GetResultAsync(int id);
    }
}

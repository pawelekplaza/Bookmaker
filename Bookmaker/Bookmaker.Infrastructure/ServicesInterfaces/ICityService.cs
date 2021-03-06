﻿using Bookmaker.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Infrastructure.ServicesInterfaces
{
    public interface ICityService
    {
        Task CreateAsync(CityCreateDto city);
        Task<CityDto> GetAsync(int id);
        Task<IEnumerable<CityDto>> GetAsync(string name);
        Task<IEnumerable<CityDto>> GetAllAsync();
        Task<IEnumerable<StadiumDto>> GetStadiumsAsync(int cityId);
        Task UpdateAsync(CityUpdateDto city);
        Task DeleteAsync(int id);

    }
}

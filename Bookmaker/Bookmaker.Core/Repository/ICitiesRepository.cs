﻿using Bookmaker.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Core.Repository
{
    public interface ICitiesRepository
    {
        Task<City> GetAsync(Guid id);        
        Task<IEnumerable<City>> GetAllAsync();
        Task AddAsync(City city);
        Task UpdateAsync(City city);
        Task RemoveAsync(Guid id);
    }
}

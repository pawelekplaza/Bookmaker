using AutoMapper;
using Bookmaker.Core.Domain;
using Bookmaker.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<City, CityDTO>(); // TODO: jak to mapować?
                    //.ForMember(x => x.CountryName, m => m.MapFrom(p => p.Country.Name));
            })
            .CreateMapper();
    }
}

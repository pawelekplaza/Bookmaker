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
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<User, UserUpdateDto>();
                cfg.CreateMap<City, CityDto>();
                cfg.CreateMap<Country, CountryDto>();
                cfg.CreateMap<Score, ScoreDto>();


                // TODO: jak to mapować?
                //.ForMember(x => x.CountryName, m => m.MapFrom(p => p.Country.Name));
            })
            .CreateMapper();
    }
}

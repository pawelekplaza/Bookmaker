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
                cfg.CreateMap<Country, CountryDto>();
                cfg.CreateMap<Score, ScoreDto>();

                cfg.CreateMap<City, CityDto>()
                    .ForMember(x => x.CountryId, m => m.MapFrom(p => p.Country.Id))
                    .ForMember(x => x.CountryName, m => m.MapFrom(p => p.Country.Name));

                cfg.CreateMap<Stadium, StadiumDto>()
                    .ForMember(x => x.CityId, m => m.MapFrom(p => p.City.Id))
                    .ForMember(x => x.CountryId, m => m.MapFrom(p => p.Country.Id));

                cfg.CreateMap<Result, ResultDto>()
                    .ForMember(x => x.HostScoreId, m => m.MapFrom(p => p.HostScore.Id))
                    .ForMember(x => x.GuestScoreId, m => m.MapFrom(p => p.GuestScore.Id));

                cfg.CreateMap<Team, TeamDto>()
                    .ForMember(x => x.StadiumId, m => m.MapFrom(p => p.Stadium.Id));

                cfg.CreateMap<Match, MatchDto>()
                    .ForMember(x => x.HostTeamId, m => m.MapFrom(p => p.HostTeam.Id))
                    .ForMember(x => x.GuestTeamId, m => m.MapFrom(p => p.GuestTeam.Id))
                    .ForMember(x => x.StadiumId, m => m.MapFrom(p => p.Stadium.Id))
                    .ForMember(x => x.ResultId, m => m.MapFrom(p => p.Result.Id))
                    .ForMember(x => x.HostTeamName, m => m.MapFrom(p => p.HostTeam.Name))
                    .ForMember(x => x.GuestTeamName, m => m.MapFrom(p => p.GuestTeam.Name));

                cfg.CreateMap<Bet, BetDto>()
                    .ForMember(x => x.MatchId, m => m.MapFrom(p => p.Match.Id))
                    .ForMember(x => x.ScoreId, m => m.MapFrom(p => p.Score.Id))
                    .ForMember(x => x.TeamId, m => m.MapFrom(p => p.Score.Id))
                    .ForMember(x => x.UserId, m => m.MapFrom(p => p.User.Id));
            })
            .CreateMapper();
    }
}

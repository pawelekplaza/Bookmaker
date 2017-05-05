using Bookmaker.Infrastructure.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Bookmaker.Infrastructure.DTO;
using System.Threading.Tasks;
using Bookmaker.Core.Repository;
using AutoMapper;
using Bookmaker.Core.Utils;
using Bookmaker.Core.Domain;

namespace Bookmaker.Infrastructure.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IStadiumRepository _stadiumRepository;
        private readonly IResultRepository _resultRepository;
        private readonly IMapper _mapper;

        public MatchService(IMatchRepository matchRepository, ITeamRepository teamRepository, IStadiumRepository stadiumRepository, IResultRepository resultRepository, IMapper mapper)
        {
            _matchRepository = matchRepository;
            _teamRepository = teamRepository;
            _stadiumRepository = stadiumRepository;
            _resultRepository = resultRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(MatchDto match)
        {
            var hostTeam = await _teamRepository.GetByIdAsync(match.HostTeamId);
            
            if (hostTeam == null)
            {
                throw new InvalidDataException($"Host team with id '{ match.HostTeamId }' does not exist.");
            }

            var guestTeam = await _teamRepository.GetByIdAsync(match.GuestTeamId);

            if (guestTeam == null)
            {
                throw new InvalidDataException($"Guest team with id '{ match.GuestTeamId }' does not exist.");
            }

            var stadium = await _stadiumRepository.GetAsync(match.StadiumId);

            if (stadium == null)
            {
                throw new InvalidDataException($"Stadium with id '{ match.StadiumId }' does not exist.");
            }

            var matchesList = await _matchRepository.GetAllAsync();

            foreach (var value in matchesList)
            {
                if (hostTeam.Id == value.HostTeam.Id
                    && guestTeam.Id == value.GuestTeam.Id
                    && stadium.Id == value.Stadium.Id
                    && match.StartTime.CompareTo(value.StartTime) == 0)
                {
                    throw new InvalidDataException("Provided match already exists.");
                }
            }

            var newMatch = new Match(hostTeam, guestTeam, stadium, match.StartTime);
            await _matchRepository.CreateAsync(newMatch);
        }

        public async Task DeleteAsync(int id)
        {
            var matchToDelete = await _matchRepository.GetByIdAsync(id);

            if (matchToDelete == null)
            {
                throw new InvalidDataException($"Match with id '{ id }' does not exist.");
            }

            await _matchRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<MatchDto>> GetAllAsync()
        {
            var matches = await _matchRepository.GetAllAsync();
            var matchesDto = new HashSet<MatchDto>();

            foreach (var match in matches)
            {
                matchesDto.Add(_mapper.Map<Match, MatchDto>(match));
            }

            return matchesDto;
        }

        public async Task<IEnumerable<BetDto>> GetBetsAsync(int matchId)
        {
            var bets = await _matchRepository.GetBetsAsync(matchId);
            var betDtos = new HashSet<BetDto>();

            foreach (var bet in bets)
            {
                betDtos.Add(_mapper.Map<Bet, BetDto>(bet));
            }

            return betDtos;
        }

        public async Task<MatchDto> GetByIdAsync(int id)
        {
            var match = await _matchRepository.GetByIdAsync(id);

            return _mapper.Map<Match, MatchDto>(match);
        }

        public async Task UpdateAsync(MatchUpdateDto match)
        {
            var matchToUpdate = await _matchRepository.GetByIdAsync(match.Id);

            if (matchToUpdate == null)
            {
                throw new InvalidDataException($"Match with id '{ match.Id }' does not exist.");
            }

            if (match.HostTeamId != null)
            {
                var hostTeam = await _teamRepository.GetByIdAsync(match.HostTeamId.Value);
                matchToUpdate.SetHostTeam(hostTeam);
            }

            if (match.GuestTeamId != null)
            {
                var guestTeam = await _teamRepository.GetByIdAsync(match.GuestTeamId.Value);
                matchToUpdate.SetGuestTeam(guestTeam);
            }

            if (match.StadiumId != null)
            {
                var stadium = await _stadiumRepository.GetAsync(match.StadiumId.Value);
                matchToUpdate.SetStadium(stadium);
            }

            if (match.ResultId != null)
            {
                var result = await _resultRepository.GetByIdAsync(match.ResultId.Value);
                matchToUpdate.SetResult(result);
            }

            if (match.StartTime != null)
            {
                matchToUpdate.SetStartTime(match.StartTime.Value);
            }

            await _matchRepository.UpdateAsync(matchToUpdate);
        }
    }
}

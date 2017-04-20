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
    public class BetService : IBetService
    {
        private readonly IBetRepository _betRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IScoreRepository _scoreRepository;
        private readonly IMapper _mapper;

        public BetService(IBetRepository betRepository, IUserRepository userRepository, IMatchRepository matchRepository, 
            ITeamRepository teamRepository, IScoreRepository scoreRepository, IMapper mapper)
        {
            _betRepository = betRepository;
            _userRepository = userRepository;
            _matchRepository = matchRepository;
            _teamRepository = teamRepository;
            _scoreRepository = scoreRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(BetDto bet)
        {
            var user = await _userRepository.GetByIdAsync(bet.UserId);

            if (user == null)
            {
                throw new InvalidDataException($"User with id '{ bet.UserId }' does not exist.");
            }

            var match = await _matchRepository.GetByIdAsync(bet.MatchId);

            if (match == null)
            {
                throw new InvalidDataException($"Match with id '{ bet.MatchId }' does not exist.");
            }

            var team = await _teamRepository.GetByIdAsync(bet.TeamId);

            if (team == null)
            {
                throw new InvalidDataException($"Team with id '{ bet.TeamId }' does not exist.");
            }

            var score = await _scoreRepository.GetAsync(bet.ScoreId);

            if (score == null)
            {
                throw new InvalidDataException($"Score with id '{ bet.ScoreId }' does not exist.");
            }

            var bets = await GetAllAsync();

            foreach (var value in bets)
            {
                if (value.CreatedAt == bet.CreatedAt)
                {
                    throw new InvalidDataException($"Provided bet already exists.");
                }
            }

            var newBet = new Bet(bet.Price, user, match, team, score);
            newBet.ResetCreationDate();

            await _betRepository.CreateAsync(newBet);
        }

        public async Task DeleteAsync(int id)
        {
            var betToDelete = await GetByIdAsync(id);

            if (betToDelete == null)
            {
                throw new InvalidDataException($"Bet with id '{ id }' does not exist.");
            }

            await _betRepository.DeleteAsync(id);                
        }

        public async Task<IEnumerable<BetDto>> GetAllAsync()
        {
            var bets = await _betRepository.GetAllAsync();
            var betsDto = new HashSet<BetDto>();

            foreach (var bet in bets)
            {
                betsDto.Add(_mapper.Map<Bet, BetDto>(bet));
            }

            return betsDto;
        }

        public async Task<BetDto> GetByIdAsync(int id)
        {
            var bet = await _betRepository.GetByIdAsync(id);

            return _mapper.Map<Bet, BetDto>(bet);
        }

        public async Task UpdateAsync(BetUpdateDto bet)
        {
            var betToUpdate = await _betRepository.GetByIdAsync(bet.Id);

            if (betToUpdate == null)
            {
                throw new InvalidDataException($"Bet with id '{ bet.Id }' does not exist.");
            }

            if (bet.CreatedAt != null)
            {
                betToUpdate.SetCreatedAt(bet.CreatedAt.Value);
            }

            if (bet.LastUpdate != null)
            {
                betToUpdate.SetLastUpdate(bet.LastUpdate.Value);
            }

            if (bet.MatchId != null)
            {
                var match = await _matchRepository.GetByIdAsync(bet.MatchId.Value);
                betToUpdate.SetMatch(match);
            }

            if (bet.Price != null)
            {
                betToUpdate.SetPrice(bet.Price.Value);
            }

            if (bet.ScoreId != null)
            {
                var score = await _scoreRepository.GetAsync(bet.ScoreId.Value);
                betToUpdate.SetScore(score);
            }

            if (bet.TeamId != null)
            {
                var team = await _teamRepository.GetByIdAsync(bet.TeamId.Value);
                betToUpdate.SetTeam(team);
            }

            if (bet.UserId != null)
            {
                var user = await _userRepository.GetByIdAsync(bet.UserId.Value);
                betToUpdate.SetUser(user);
            }

            await _betRepository.UpdateAsync(betToUpdate);
        }
    }
}

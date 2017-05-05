using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bookmaker.Infrastructure.DTO;
using Bookmaker.Core.Repository;
using AutoMapper;
using Bookmaker.Core.Domain;
using Bookmaker.Core.Utils;
using Bookmaker.Infrastructure.ServicesInterfaces;

namespace Bookmaker.Infrastructure.Services
{
    public class ScoreService : IScoreService
    {
        private readonly IScoreRepository _scoreRepository;
        private readonly IMapper _mapper;

        public ScoreService(IScoreRepository scoreRepository, IMapper mapper)
        {
            _scoreRepository = scoreRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(ScoreCreateDto score)
        {
            var newScore = new Score(score.Goals, score.Shots);
            await _scoreRepository.CreateAsync(newScore);
        }

        public async Task DeleteAsync(int id)
        {
            var scoreToDelete = await _scoreRepository.GetAsync(id);

            if (scoreToDelete == null)
            {
                throw new InvalidDataException($"Score with id '{ id }' does not exist.");
            }

            await _scoreRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ScoreDto>> GetAllAsync()
        {
            var scores = await _scoreRepository.GetAllAsync();
            var scoresDto = new HashSet<ScoreDto>();

            foreach(var score in scores)
            {
                scoresDto.Add(_mapper.Map<Score, ScoreDto>(score));
            }

            return scoresDto;
        }

        public async Task<ScoreDto> GetAsync(int id)
        {
            var score = await _scoreRepository.GetAsync(id);
            return _mapper.Map<Score, ScoreDto>(score);
        }

        public async Task<IEnumerable<BetDto>> GetBetsAsync(int scoreId)
        {
            var bets = await _scoreRepository.GetBetsAsync(scoreId);
            var betDtos = new HashSet<BetDto>();

            foreach (var bet in bets)
            {
                betDtos.Add(_mapper.Map<Bet, BetDto>(bet));
            }

            return betDtos;
        }

        public async Task UpdateAsync(ScoreUpdateDto score)
        {
            var scoreToUpdate = await _scoreRepository.GetAsync(score.Id);

            if (scoreToUpdate == null)
            {
                throw new InvalidDataException($"Score with id'{ score.Id } does not exist.");
            }

            if (score.Goals != null)
            {
                scoreToUpdate.SetGoals(score.Goals);
            }

            if (score.Shots != null)
            {
                scoreToUpdate.SetShots(score.Shots);
            }

            await _scoreRepository.UpdateAsync(scoreToUpdate);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bookmaker.Infrastructure.DTO;
using Bookmaker.Core.Repository;
using AutoMapper;
using Bookmaker.Core.Utils;
using Bookmaker.Core.Domain;
using Bookmaker.Infrastructure.ServicesInterfaces;

namespace Bookmaker.Infrastructure.Services
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _resultRepository;
        private readonly IScoreRepository _scoreRepository;
        private readonly IMapper _mapper;

        public ResultService(IResultRepository resultRepository, IScoreRepository scoreRepository, IMapper mapper)
        {
            _resultRepository = resultRepository;
            _scoreRepository = scoreRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(ResultDto result)
        {
            var hostScore = await _scoreRepository.GetAsync(result.HostScoreId);

            if (hostScore == null)
            {
                throw new InvalidDataException($"Score with id '{ result.HostScoreId }' does not exist.");
            }

            var guestScore = await _scoreRepository.GetAsync(result.GuestScoreId);

            if (guestScore == null)
            {
                throw new InvalidDataException($"Score with id '{ result.GuestScoreId }' does not exist.");
            }

            var newResult = new Result(hostScore, guestScore);
            await _resultRepository.CreateAsync(newResult);
        }

        public async Task DeleteAsync(int id)
        {
            var resultToDelete = await _resultRepository.GetByIdAsync(id);

            if (resultToDelete == null)
            {
                throw new InvalidDataException($"Result with id '{ id }' does not exist.");
            }

            await _resultRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ResultDto>> GetAllAsync()
        {
            var results = await _resultRepository.GetAllAsync();
            var resultsDto = new HashSet<ResultDto>();

            foreach (var result in results)
            {
                resultsDto.Add(_mapper.Map<Result, ResultDto>(result));
            }

            return resultsDto;
        }

        public async Task<ResultDto> GetByIdAsync(int id)
        {
            var result = await _resultRepository.GetByIdAsync(id);

            return _mapper.Map<Result, ResultDto>(result);
        }

        public async Task<IEnumerable<MatchDto>> GetMatchesAsync(int resultId)
        {
            var matches = await _resultRepository.GetMatchesAsync(resultId);
            var matchDtos = new HashSet<MatchDto>();

            foreach(var match in matches)
            {
                matchDtos.Add(_mapper.Map<Match, MatchDto>(match));
            }

            return matchDtos;
        }

        public async Task UpdateAsync(ResultUpdateDto result)
        {
            var resultToUpdate = await _resultRepository.GetByIdAsync(result.Id);

            if (resultToUpdate == null)
            {
                throw new InvalidDataException($"Result with id '{ result.Id }' does not exist.");
            }

            if (result.HostScoreId != null)
            {
                var hostScore = await _scoreRepository.GetAsync(result.HostScoreId.Value);            
                resultToUpdate.SetHostScore(hostScore);
            }

            if (result.GuestScoreId != null)
            {
                var guestScore = await _scoreRepository.GetAsync(result.GuestScoreId.Value);
                resultToUpdate.SetGuestScore(guestScore);
            }

            await _resultRepository.UpdateAsync(resultToUpdate);
        }
    }
}

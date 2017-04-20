using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bookmaker.Infrastructure.DTO;
using Bookmaker.Core.Repository;
using AutoMapper;
using Bookmaker.Core.Domain;
using Bookmaker.Core.Utils;

namespace Bookmaker.Infrastructure.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IStadiumRepository _stadiumRepository;
        private readonly IMapper _mapper;

        public TeamService(ITeamRepository teamRepository, IStadiumRepository stadiumRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _stadiumRepository = stadiumRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(TeamDto team)
        {
            var stadium = await _stadiumRepository.GetAsync(team.StadiumId);

            if (stadium == null)
            {
                throw new InvalidDataException($"Stadium with id '{ team.StadiumId }' does not exist.");
            }

            var newTeam = new Team(stadium, team.Name);            
            await _teamRepository.CreateAsync(newTeam);
        }

        public async Task DeleteAsync(int id)
        {
            var teamToDelete = await _teamRepository.GetByIdAsync(id);

            if (teamToDelete == null)
            {
                throw new InvalidDataException($"Team with id '{ id }' does not exist.");
            }

            await _teamRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TeamDto>> GetAllAsync()
        {
            var teams = await _teamRepository.GetAllAsync();
            var teamsDto = new HashSet<TeamDto>();

            foreach (var team in teams)
            {
                teamsDto.Add(_mapper.Map<Team, TeamDto>(team));
            }

            return teamsDto;
        }

        public async Task<TeamDto> GetByIdAsync(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);

            return _mapper.Map<Team, TeamDto>(team);
        }

        public async Task UpdateAsync(TeamUpdateDto team)
        {
            var teamToUpdate = await _teamRepository.GetByIdAsync(team.Id);

            if (teamToUpdate == null)
            {
                throw new InvalidDataException($"Team with id '{ team.Id }' does not exist.");
            }

            if (team.StadiumId != null)
            {
                var stadium = await _stadiumRepository.GetAsync(team.StadiumId.Value);
                teamToUpdate.SetStadium(stadium);
            }

            if (!string.IsNullOrWhiteSpace(team.Name))
            {
                teamToUpdate.SetName(team.Name);
            }

            await _teamRepository.UpdateAsync(teamToUpdate);
        }
    }
}

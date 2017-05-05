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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var usersDTO = new HashSet<UserDto>();

            foreach (var user in users)
            {
                usersDTO.Add(_mapper.Map<User, UserDto>(user));
            }

            return usersDTO;
        }

        public async Task<UserDto> GetAsync(string email)
        {
            var user = await _userRepository.GetAsync(email);

            return _mapper.Map<User, UserDto>(user);
        }

        public async Task RegisterAsync(UserCreateDto user)
        {
            var newUser = await _userRepository.GetAsync(user.Email);
            if (newUser != null)
            {
                throw new InvalidDataException($"User with email '{ user.Email }' already exists.");
            }

            // #ask3
            var salt = Guid.NewGuid().ToString("N");
            newUser = new User(user.Email, user.Username, user.Password, salt);

            await _userRepository.AddAsync(newUser);
        }

        public async Task UpdateUserAsync(UserUpdateDto user)
        {
            // #ask4

            if (user == null)
                return;

            var userToUpdate = await _userRepository.GetAsync(user.Email);

            if (userToUpdate == null)
            {
                throw new InvalidDataException($"User with email '{ user.Email }' does not exist.");
            }

            if (!string.IsNullOrWhiteSpace(user.FullName))
                userToUpdate.SetFullName(user.FullName);

            if (!string.IsNullOrWhiteSpace(user.Password))
                userToUpdate.SetPassword(user.Password);

            if (!string.IsNullOrWhiteSpace(user.Username))
                userToUpdate.SetUsername(user.Username);

            await _userRepository.UpdateAsync(userToUpdate);
        }

        public async Task RemoveAsync(string email)
        {
            await _userRepository.RemoveAsync(email);            
        }

        public async Task<IEnumerable<BetDto>> GetBetsAsync(string email)
        {
            var bets = await _userRepository.GetBetsAsync(email);
            var betDtos = new HashSet<BetDto>();

            foreach (var bet in bets)
            {
                betDtos.Add(_mapper.Map<Bet, BetDto>(bet));
            }

            return betDtos;
        }
    }
}

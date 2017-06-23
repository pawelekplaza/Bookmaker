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
        private readonly IEncrypter _encrypter;    
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IEncrypter encrypter, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _encrypter = encrypter;
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
            var salt = _encrypter.GetSalt(user.Password);
            var hash = _encrypter.GetHash(user.Password, salt);

            //System.Diagnostics.Debug.WriteLine($"USR:{user.Email}, PWD:{user.Password}, SALT:{salt}, HASH:{hash}");


            newUser = new User(user.Email, user.Username, salt, hash, "user");
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
            {
                var salt = _encrypter.GetSalt(user.Password);
                var hash = _encrypter.GetHash(user.Password, salt);
                userToUpdate.SetSalt(salt);
                userToUpdate.SetHash(hash);
            }

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

        public async Task LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                throw new InvalidDataException("Invalid credentials.");
            }

            var salt = _encrypter.GetSalt(password);
            var hash = _encrypter.GetHash(password, salt);

            if (user.Hash == hash)
            {
                return;
            }

            throw new InvalidDataException("Invalid credentials.");
        }
    }
}

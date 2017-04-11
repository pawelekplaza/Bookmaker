using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bookmaker.Infrastructure.DTO;
using Bookmaker.Core.Repository;
using AutoMapper;
using Bookmaker.Core.Domain;

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

        public async Task<UserUpdateDto> GetForUpdateAsync(string email)
        {
            var user = await _userRepository.GetAsync(email);

            return _mapper.Map<User, UserUpdateDto>(user);
        }

        public async Task RegisterAsync(string email, string username, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
            {
                throw new Exception($"User with email '{email}' already exists.");
            }

            var salt = Guid.NewGuid().ToString("N"); // todo: jak powinno generować się sól?
            user = new User(email, username, password, salt);

            await _userRepository.AddAsync(user);
        }

        public async Task UpdateUserAsync(string email, string username, string password)
        {
            var userUpdate = new User(email, username, password, Guid.NewGuid().ToString("N"));
            await _userRepository.UpdateAsync(userUpdate);
        }

        public async Task RemoveAsync(string email)
        {
            await _userRepository.RemoveAsync(email);
            await Task.CompletedTask;
        }
    }
}

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

        public async Task RegisterAsync(string email, string username, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
            {
                throw new Exception($"User with email '{email}' already exists.");
            }

            // #ask3
            var salt = Guid.NewGuid().ToString("N");
            user = new User(email, username, password, salt);

            await _userRepository.AddAsync(user);
        }

        public async Task UpdateUserAsync(UserUpdateDto newUserData)
        {
            // #ask4

            if (newUserData == null)
                return;

            var userToUpdate = await _userRepository.GetAsync(newUserData.Email);

            if (!string.IsNullOrWhiteSpace(newUserData.FullName))
                userToUpdate.SetFullName(newUserData.FullName);

            if (!string.IsNullOrWhiteSpace(newUserData.Password))
                userToUpdate.SetPassword(newUserData.Password);

            if (!string.IsNullOrWhiteSpace(newUserData.Username))
                userToUpdate.SetUsername(newUserData.Username);

            await _userRepository.UpdateAsync(userToUpdate);
        }

        public async Task RemoveAsync(string email)
        {
            await _userRepository.RemoveAsync(email);
            await Task.CompletedTask;
        }
    }
}

using Bookmaker.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Infrastructure.ServicesInterfaces
{
    public interface IUserService
    {
        Task<UserDto> GetAsync(string email);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<IEnumerable<BetDto>> GetBetsAsync(string email);
        Task RemoveAsync(string email);
        Task RegisterAsync(UserCreateDto user);
        Task UpdateUserAsync(UserUpdateDto newUserData);
        Task LoginAsync(string email, string password);
    }
}

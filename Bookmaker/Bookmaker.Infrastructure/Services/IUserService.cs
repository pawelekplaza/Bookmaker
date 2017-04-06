using Bookmaker.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Infrastructure.Services
{
    public interface IUserService
    {
        Task<UserDTO> GetAsync(string email);
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task RegisterAsync(string email, string username, string password);
    }
}

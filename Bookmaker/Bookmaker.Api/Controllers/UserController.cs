using Bookmaker.Infrastructure.Commands.Users;
using Bookmaker.Infrastructure.DTO;
using Bookmaker.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookmaker.Api.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{email}")]
        public async Task<UserDTO> GetAsync(string email)
            => await _userService.GetAsync(email);

        [HttpGet("{all}")]
        public async Task<IEnumerable<UserDTO>> GetAllAsync()
            => await _userService.GetAllAsync();

        [HttpPost]
        public async Task CreateUserAsync([FromBody]CreateUser request)
            => await _userService.RegisterAsync(request.Email, request.Username, request.Password);
    }
}
